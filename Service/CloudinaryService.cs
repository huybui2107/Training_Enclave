using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
namespace BE.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService()
        {
        
            var account = new Account(
               configuration["Cloudinary:dzdfqqdxs"],
               configuration["Cloudinary:211321398327785"],
               configuration["Cloudinary:lf7If7e_4hRGcF55kMrQ"]);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> uploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null; // Handle invalid file

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    // Optional: Additional upload parameters
                    // e.g., Folder = "your_folder_name"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUri.AbsoluteUri;
            }
        }
    }
}
