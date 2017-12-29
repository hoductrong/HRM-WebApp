using System.Collections.Generic;

namespace QuanLyNongTrai.Service
{
    /// <summary>
    /// Notify for user errors about validate or other error when insert or update entity
    /// </summary>
    public class ChangeDataResult
    {
        /// <summary>
        /// Create Success ChangeDataResult
        /// </summary>
        public ChangeDataResult()
        {
            Succeeded = true;
        }
        private ChangeDataResult(params ChangeDataError[] errorList)
        {
            Succeeded = false;
            Errors = errorList;
        }
        /// <summary>
        /// Insert or Update entity don't occurs error
        /// </summary>
        /// <returns></returns>
        public bool Succeeded { get; protected set; }
        /// <summary>
        /// Detail list error occurs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChangeDataError> Errors { get; }
        /// <summary>
        /// Create object of QuanLyNongTrai.Service.ChangeDataResult with errors
        /// </summary>
        /// <param name="errors"></param>
        /// <returns>ChangeDataResult with errors</returns>
        public static ChangeDataResult Fails(params ChangeDataError[] errors)
        {
            return new ChangeDataResult(errors);
        }
        /// <summary>
        /// Return list error with message text
        /// </summary>
        /// <returns>Error message text list</returns>
        public string GetError()
        {
            if (Errors == null)
                return base.ToString();
            string errorString = "";
            foreach (var error in Errors)
            {
                errorString += error.Description + "\r\n";
            }
            return errorString;
        }
    }
}