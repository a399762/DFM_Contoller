using System;
using System.Runtime.InteropServices;


namespace PhotronWrapper.Models
{

    public class CameraModeManager
    {
        public CameraModeManager(UInt32 deviceNo, UInt32 mode)
        {
            _deviceNo = deviceNo;
            _mode = mode;
        }

        [DllImport("pdclib.dll")]
        static extern UInt32 PDC_SetStatus(UInt32 deviceNo, UInt32 mode, out UInt32 errorCode);

        UInt32 _deviceNo;
        UInt32 _mode;

        public void SetStatus()
        {
            UInt32 ret;
            UInt32 errorCode;

            // Set camera mode
            ret = PDC_SetStatus(_deviceNo, _mode, out errorCode);
            if (ret == 0)
            {
                var ex = new PdclibException("PDC_SetStatus " + errorCode.ToString());
                throw ex;
            }
        }

        
    }
}
