namespace BE.Service
{
    public interface ICloudinaryService
    {
        Task<string> uploadFile(IFormFile file);
    }
}
