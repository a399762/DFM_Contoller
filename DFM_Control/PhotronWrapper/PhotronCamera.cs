using PhotronWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotronWrapper
{
    public class PhotronCamera
    {
        private OpenCloseCamera openCloseCamera;
        private IControlCamera _cameraControl = null;
        private ILiveImageController _controlLiveImage;
        private IMemoryImageController _controlMemImage;
        private IControlCameraHead selectedCameraHead;

        private String IP = String.Empty;
    
        public PhotronCamera(String ip)
        {
            IP = ip;
            openCloseCamera = new OpenCloseCamera();
        }

        public IControlCamera CameraControl
        {
            get { return _cameraControl; }
        }

        /// <summary>
        ///  Connect to the camera at the specified IP Address. 
        /// </summary>
        /// <returns>instance of the camera if found, throws exception if not with returned c++ dll error as message</returns>
        public IControlCamera ConnectCamera()
        {
            openCloseCamera.OpenCamera(IP);
            _cameraControl = openCloseCamera.Camera;

            if (_cameraControl.CameraHeads.Count > 0)
            {
                // Choose the first camera head in head list
                selectedCameraHead = _cameraControl.CameraHeads[0];

                // Create controller of live image and memory image
                _controlLiveImage = new LiveImageController(selectedCameraHead.DeviceNo, selectedCameraHead.ChildNo, selectedCameraHead.ColorType);
                _controlMemImage = new MemoryImageController(selectedCameraHead.DeviceNo, selectedCameraHead.ChildNo, selectedCameraHead.ColorType, selectedCameraHead.DeviceName);

                Task.Factory.StartNew(RealtimeLoadImage);
            }
            else
            {
                throw new Exception("");
            }


            return _cameraControl;
        }

        /// <summary>
        /// call this each time you want the latest real time image, has to be called ~30/s periodically to make video
        /// </summary>
        public void RealtimeLoadImage(Resolution SelectedResolution)
        {
            _controlLiveImage.GetLiveImageData(SelectedResolution, selectedCameraHead.ColorType);
        }

        public void CloseCamera()
        {

            openCloseCamera.Close();

        }

        UInt32 selectedFrameRate;
        public UInt32 SelectedFrameRate
        {
            get { return selectedFrameRate; }
            set
            {
                selectedFrameRate = value;
                try
                {
                    _controlLiveImage.SetRecordRate(selectedFrameRate);
                }
                catch (PdclibException ex)
                {
              
                }
            }
        }

        public IControlCameraHead SelectedCameraHead
        {
            get { return selectedCameraHead; }
            set
            {
                selectedCameraHead = value;
            }
        }

    }
}
