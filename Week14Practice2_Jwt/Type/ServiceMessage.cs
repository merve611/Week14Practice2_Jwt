namespace Week14Practice2_Jwt.Type
{
    public class ServiceMessage
    {
        public bool IsSucced { get; set; }
        public string Message { get; set; }
    }
    public class ServiceMessage<T>
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }

}