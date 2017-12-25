namespace QuanLyNongTrai.UI.Entity
{
    public class ResponseMessageModel
    {
        public MessageCode Code { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}