using PhotronWrapper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotronWrapper
{
    public class PhotronCamera
    {
        public delegate void LiveFeedHandler(object sender, LiveFeedEventArgs e);
        public event LiveFeedHandler OnLiveFeedNewFrame;

        private OpenCloseCamera openCloseCamera;
        private IControlCamera cameraControl = null;
        private ILiveImageController controlLiveImage;
        private IMemoryImageController controlMemImage;
        private IControlCameraHead selectedCameraHead;
     
        private Resolution selectedResolution;
        private UInt32 selectedFrameRate;
        IList<UInt32> shutterSpeedList;
        IList<Resolution> resolutionList;

        CancellationTokenSource liveFeedCancellationTokenSource;
       
        private String IP = String.Empty;
    
        public PhotronCamera()
        {
         
        }

        /// <summary>
        ///  Connect to the camera at the specified IP Address. 
        /// </summary>
        /// <returns>true of the camera if can connect, false if no heads?, throws exception if not with returned c++ dll error as message</returns>
        public void ConnectCamera(String ip)
        {

            this.IP = ip;

            //make sure if we have a connection,,.. to close it.
            try
            {
                CloseCamera();
            }
            catch (Exception)
            {  
            }


            openCloseCamera = new OpenCloseCamera();
            openCloseCamera.OpenCamera(IP);
            cameraControl = openCloseCamera.Camera;

            if (cameraControl.CameraHeads.Count > 0)
            {
                // Choose the first camera head in head list
                selectedCameraHead = cameraControl.CameraHeads[0];
             
                // Create controller of live image and memory image
                controlLiveImage = new LiveImageController(selectedCameraHead.DeviceNo, selectedCameraHead.ChildNo, selectedCameraHead.ColorType);
                controlMemImage = new MemoryImageController(selectedCameraHead.DeviceNo, selectedCameraHead.ChildNo, selectedCameraHead.ColorType, selectedCameraHead.DeviceName);

                selectedResolution = GetResolutionList()[0];//default to first in list,.. allow user to override later...

                String f = "";

                //this next should pick from settings?
                //and be in own method

                //   controlLiveImage.SetRecordRate(selectedFrameRate);

                //get shutte speed list from camera

                //get shutter speed list from camera 



            }
        }

        private void RealtimeLoadImageCancellableWork(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancelled work before start");
                cancellationToken.ThrowIfCancellationRequested();
            }

            //set resolution before we start... may need to be try/catch?
            controlLiveImage.SetResolution(selectedResolution);

            //set frame rate here too?


            while (true)
            {
                Thread.Sleep(10);
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                try
                {
                    //ask device to load image
                    Bitmap result = controlLiveImage.GetLiveImageData(selectedResolution, selectedCameraHead.ColorType);

                    //send out frame event.
                    if (result != null)
                    {
                        if (OnLiveFeedNewFrame == null) return;

                        LiveFeedEventArgs args = new LiveFeedEventArgs(result);
                        OnLiveFeedNewFrame(this, args);
                    }
                }
                catch (PdclibException ex)
                {
                    String t = "";
                }

            }
        }

        /// <summary>
        /// call this to start, periodic frame query
        /// task will create events to allow folks who want data to get data when it changes.
        /// </summary>
        public void StartLiveFeed()
        {
            liveFeedCancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() => RealtimeLoadImageCancellableWork(liveFeedCancellationTokenSource.Token), liveFeedCancellationTokenSource.Token);
            return;
        }

        public void StopLiveFeed()
        {
            try
            {
                liveFeedCancellationTokenSource.Cancel();
            }
            catch (Exception e)
            {

            }
        }

        public void CloseCamera()
        {
            StopLiveFeed();
            openCloseCamera.Close();
        }

        public void SetRecordRate(UInt32 frameRate)
        {
            controlLiveImage.SetRecordRate(frameRate);
        }

        public IList<Resolution> GetResolutionList()
        {
            return controlLiveImage.GetResolutionList();
        }

        public void StartRecording()
        {
            controlLiveImage.RecordStart();
        }

        public void StopRecording()
        {
            controlLiveImage.RecordStop();
        }

        public void GetCameraVideoMemoryStatus()
        {
            //make an complex object to return all this data?
            controlMemImage.GetMemImageInfo();
            //TotalFrameNo,RecordRate,RecordShutterSpeed,RecordResolution should be available now

            //// Show the first frame of memory images in initial status 
            //controlMemImage.GetMemImageData(controlMemImage.FrameInfo.m_nStart);
           
            
            //RaisePropertyChanged(() => MemImageSource);

            //startFrameNo = (UInt32)_controlMemImage.FrameInfo.m_nStart;
            //RaisePropertyChanged(() => StartFrameNo);

            //currentFrameNo = (UInt32)_controlMemImage.FrameInfo.m_nStart;
            //RaisePropertyChanged(() => CurrentFrameNo);

            //endFrameNo = (UInt32)_controlMemImage.FrameInfo.m_nEnd;
            //RaisePropertyChanged(() => EndFrameNo);

            //// Set the range of file saving to all frames in initial status
            //saveFileStartFrameNo = _controlMemImage.FrameInfo.m_nStart;
            //RaisePropertyChanged(() => SaveFileStartFrameNo);

            //saveFileEndFrameNo = _controlMemImage.FrameInfo.m_nEnd;
            //RaisePropertyChanged(() => SaveFileEndFrameNo);

        }


    }

    public class LiveFeedEventArgs : EventArgs
    {
        public LiveFeedEventArgs(Bitmap frame)
        {
            Frame = frame;
        }

        public Bitmap Frame { get; set; }
    }
}
