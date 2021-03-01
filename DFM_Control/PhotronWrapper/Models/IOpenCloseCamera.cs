using System;


namespace PhotronWrapper.Models
{
    /// <summary>
    /// Result of camera initialization 
    /// </summary>
    public enum Result
    {
        Completed,
        Canceled,
        Error,
    };

    /// <summary>
    /// Interface for camera detecting and close
    /// </summary>
    public interface IOpenCloseCamera
    {
        /// <summary>
        /// Detect camera
        /// </summary>
        /// <param name="IPAdress">IP address（fixed）</param>
        /// <param name="message">message when camera detecting failed</param>
        /// <returns></returns>
        void OpenCamera(String IPAdress);
        
        /// <summary>
        /// Abort camera detecting
        /// </summary>
        void Abort();

        /// <summary>
        /// Close camera
        /// </summary>
        void Close();

        /// <summary>
        /// Interface for camera controlling
        /// </summary>
        IControlCamera Camera { get; }

    }
}
