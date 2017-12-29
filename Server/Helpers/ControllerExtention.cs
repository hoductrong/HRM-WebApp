using QuanLyNongTrai.UI.Entity;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtention
    {
        /// <summary>
        /// Response with success ResponseMessage object status and data
        /// </summary>
        /// <param name="data">Response data</param>
        public static ResponseMessageModel Message(this Controller controller, object data)
        {
            return ResponseMessageModel.CreateResponse(data);
        }
        /// <summary>
        /// Response with ResponseMessage object with error
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="errorMessage">Error details</param>
        public static ResponseMessageModel Message(this Controller controller, MessageCode code, string errorMessage = null)
        {
            return ResponseMessageModel.CreateResponse(code,errorMessage);
        }
    }
}