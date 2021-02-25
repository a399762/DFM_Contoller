using PhotronWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotronWrapper
{
    public class PhotronCamera
    {
        OpenCloseCamera openCloseCamera = new OpenCloseCamera();
        String IP = String.Empty;
    
        public PhotronCamera(String ip)
        {
            IP = ip;
        }


        /// <summary>
        ///  Connect to the camera at the specified IP Address. 
        /// </summary>
        /// <returns>instance of the camera if found, throws exception if not with returned c++ dll error as message</returns>
        public IControlCamera connectCamera()
        {
            String errorMessage;
            Result result = openCloseCamera.DetectCamera(IP, out errorMessage);

            if (result == Result.Error)
            {
                throw new Exception(errorMessage);
            }

            return openCloseCamera.Camera;
        }

    }
}
