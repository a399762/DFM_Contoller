using PhotronWrapper.Enums;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace PhotronWrapper.Models
{
    // Controller for opening and closing camera
    public class OpenCloseCamera : IOpenCloseCamera
    {
        [DllImport("pdclib.dll")]
        static extern UInt32 PDC_Init(out UInt32 errorCode);
        [DllImport("pdclib.dll")]
        static extern UInt32 PDC_DetectDeviceLV(UInt32 interfaceCode, ref UInt32 detectNo, UInt32 detectNum, UInt32 detectParam, out UInt32 deviceNum, out UInt32 deviceCode, out UInt32 tmpDeviceNo, out UInt32 interfaceCodeOut, out UInt32 errorCode);
        [DllImport("pdclib.dll")]
        static extern UInt32 PDC_OpenDeviceLV(UInt32 deviceCode, UInt32 tmpDeviceNo, UInt32 interfaceCode, out UInt32 deviceNo, out UInt32 errorCode);
        [DllImport("pdclib.dll")]
        static extern UInt32 PDC_CloseDevice(UInt32 deviceNo, out UInt32 errorCode);

        private bool isInit = false;
        private bool isOpen = false;

        private UInt32 deviceNo;
        public OpenCloseCamera()
        {
        }
        
        /// <summary>
        /// Attempts to connect to a camera at a given IP address. 
        /// </summary>
        /// <param name="IPAdress"></param>
        public void OpenCamera(String IPAdress)
        {
            //convert 
            UInt32 IPConverted = ToInt(IPAdress);

            UInt32 ret;
            UInt32 errorCode;

            // Create token before detecting camera
            cancelTokenSource = new CancellationTokenSource();

            try
            {
                // Initial PDCLIB
                if (!isInit)
                {
                    ret = PDC_Init(out errorCode);
                    if (ret == 0)
                    {
                        ErrorCodes errorEnum = (ErrorCodes)errorCode;
                        throw new Exception("PDC_Init: " + errorEnum);
                    }
                    isInit = true;
                }
         
                cancelTokenSource.Token.ThrowIfCancellationRequested();

                // Detect device
                UInt32 deviceNum;
                UInt32 deviceCode;
                UInt32 tmpDeviceNo;
                UInt32 interfaceCode;
                ret = PDC_DetectDeviceLV(2, ref IPConverted, 1, 0, out deviceNum, out deviceCode, out tmpDeviceNo, out interfaceCode, out errorCode);
                if (ret == 0)
                {
                    ErrorCodes errorEnum = (ErrorCodes)errorCode;
                    throw new Exception("PDC_DetectDeviceLV: " + errorEnum.ToString());
                }
                cancelTokenSource.Token.ThrowIfCancellationRequested();

                // Open device
                ret = PDC_OpenDeviceLV(deviceCode, tmpDeviceNo, interfaceCode, out deviceNo, out errorCode);
                if (ret == 0)
                {
                    ErrorCodes errorEnum = (ErrorCodes)errorCode;
                    throw new Exception("PDC_OpenDeviceLV: " + errorEnum.ToString());
                }
                isOpen = true;
                cancelTokenSource.Token.ThrowIfCancellationRequested();

                // Create camera controller
                camera = new ControlCameraBase(deviceNo); 
                if (!camera.Init())
                {
                    throw new Exception("Camera Init Error: " + camera.ErrorMessage);
                }
            }

            // When camera detecting is cancelled
            catch (OperationCanceledException)
            {
                // If device has been opened already, close it
                if (isOpen)
                {
                    Close();
                }

                throw new Exception(Result.Canceled.ToString());
            }
        }

        static UInt32 ToInt(string addr)
        {
            var ipuint32 = BitConverter.ToUInt32(IPAddress.Parse(addr).GetAddressBytes(), 0);
            return ipuint32;
        }

        static string ToAddr(UInt32 address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
        }

        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        // Cancel camera detecting
        public void Abort()
        {
            cancelTokenSource.Cancel();
        }

        /// <summary>
        /// Close connection to camera if open
        /// </summary>
        public void Close()
        {
            string message = string.Empty;
            UInt32 ret;
            UInt32 errorCode;
            if (isOpen)
            {
                ret = PDC_CloseDevice(deviceNo, out errorCode);
                if (ret == 0)
                {
                    ErrorCodes errorEnum = (ErrorCodes)errorCode;
                    message = "PDC_OpenDeviceLV " + errorEnum.ToString();
                    throw new Exception(message);
                }
            }
        }

        IControlCamera camera = null;
        public IControlCamera Camera
        {
            get { return camera; }
        }

    }
}
