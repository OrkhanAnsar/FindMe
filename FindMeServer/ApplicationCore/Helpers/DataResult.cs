namespace ApplicationCore.Helpers
{
    public enum Message
    {
        Successful,
        IncorrectPassword,
        ServerError
    }

    public class DataResult
    {
        public DataResult(object data, Message message)
        {
            this.Data = data;
            this.Message = message;
        }

        public object Data { get; set; }
        public Message Message { get; set; }
    }
}