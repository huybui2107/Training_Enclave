namespace BE.DTOs
{
    public class ResFileDto
    {
        public string StatusCode { get; set; } = null!;

        public string Message { get; set; } = null!;
        public object File { get; set; } = null!;
    }
}
