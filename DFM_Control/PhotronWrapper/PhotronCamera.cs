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
        /// <returns>true of the camera if can connect, false if no heads?, throws exception if not with returned c++ dll error as message</returns>
        public bool ConnectCamera()
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
                }
                else
                {
                    return false;
                }

            return true;
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

        public void SetRecordRate(UInt32 frameRate)
        {
            _controlLiveImage.SetRecordRate(frameRate);
        }

        public IList<Resolution> GetResolutionList()
        {
            return _controlLiveImage.GetResolutionList();
        }
    }
}
