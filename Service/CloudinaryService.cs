using AutoMapper;
using BE.Databases;
using BE.Databases.Entities;
using BE.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
namespace BE.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CloudinaryService(DataContext context, IMapper mapper)
        {

            Account account = new Account(
                  "dbtam9pnc",
                  "885239162821427",
                  "q91OrEcu4ock7e6MdDKn_BwTlrI");
            _cloudinary = new Cloudinary(account);
            _context = context;
            _mapper = mapper;
        }

        public string getVersion(string name)
        {
            string[] parts = name.Split('.');
            string version = "";
            if (parts.Length > 1)
            {
                version = string.Join(".", parts.Take(parts.Length - 1));
            }
            version = version.Split("_")[2];
            return version;
        }

        public async Task<FileDto> uploadFile(IFormFile file)

        {
            if (file == null || file.Length == 0)
                return null; // Handle invalid file
            var version = getVersion(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new RawUploadParams
                {
                    
                    File = new FileDescription(file.FileName, stream),
                    Folder = "FileTraining",
                    PublicId = file.FileName

                };
                
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                var Url = uploadResult.SecureUri.AbsoluteUri;
                var fileDto = new FileDto
                {
                    Version = version,
                    Url = Url
                };
               var fileUpload =  _mapper.Map<FileUpload>(fileDto);
                await _context.Files.AddAsync(fileUpload);
                _context.SaveChanges();

                return fileDto;
            }
            
        }
    }
}
