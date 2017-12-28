namespace QuanLyNongTrai.UI.Entity
{
    public class ResponseMessageModel
    {
        public MessageCode Code { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public ResponseMessageModel(){}
        /// <summary>
        /// Create error ResponseMessage object
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="errorMessage">Error details</param>
        private ResponseMessageModel(MessageCode code, string errorMessage){
            Code = code;
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Create success ResponseMessage object and data
        /// </summary>
        /// <param name="data">Response data</param>
        private ResponseMessageModel(object data){
            Code = MessageCode.SUCCESS;
            Data = data;
        }
        /// <summary>
        /// Create ResponseMessage object with error
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="errorMessage">Error details</param>
        public static ResponseMessageModel CreateResponse(MessageCode code, string errorMessage = null){
            return new ResponseMessageModel(code, errorMessage);
        }
        /// <summary>
        /// Create success ResponseMessage object status and data
        /// </summary>
        /// <param name="data">Response data</param>
        public static ResponseMessageModel CreateResponse(object data){
            return new ResponseMessageModel(data);
        }
    }
}