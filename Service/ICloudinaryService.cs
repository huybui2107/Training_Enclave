using BE.DTOs;

namespace BE.Service
{
    public interface ICloudinaryService
    {
        Task<FileDto> uploadFile(IFormFile file);
        string getVersion(string name);
    }
}
