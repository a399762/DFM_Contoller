using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace PhotronWrapper.Models
{
    public interface IMemoryImageController
    {
        /// <summary>
        /// Get information of memory image
        /// </summary>
        void GetMemImageInfo();

        /// <summary>
        /// Get frame information of memory image
        /// </summary>
        PDC_FRAME_INFO FrameInfo { get; }

        /// <summary>
        /// Get memory image
        /// </summary>
        Bitmap GetMemImageData(int frameNo);

        /// <summary>
        /// Save image file
        /// </summary>
        void SaveFile(string fileName, int startFrameNo, int endFrameNo);

        /// <summary>
        /// Cancel saving image file
        /// </summary>
        void CancelSaveFile();

        /// <summary>
        /// Get record rate of memory images
        /// </summary>
        UInt32 RecordRate { get; }

        /// <summary>
        /// Get shutter speed of memory images
        /// </summary>
        UInt32 RecordShutterSpeed { get; }

        /// <summary>
        /// Get resolution of memory images
        /// </summary>
        Resolution RecordResolution { get; }

    }
}
