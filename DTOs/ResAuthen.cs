namespace BE.DTOs
{
    public class ResAuthen
    {
        public string StatusCode { get; set; } = null!;

        public string Message { get; set; } = null!;
        public string Token { get; set; } = null!;
        public object User { get; set; } = null!;
    }
}
