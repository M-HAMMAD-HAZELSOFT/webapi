namespace webapi.Models
{
    public class ServiceResponse<T>
    {
        public T Items { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
