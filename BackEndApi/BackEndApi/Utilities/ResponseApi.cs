namespace BackEndApi.Utilities
{
    public class ResponseApi<T>
    {
        public bool Status { get; set; }
        public string? Msg { get; set; }
        public T? Value { get; set; }
    }
}
