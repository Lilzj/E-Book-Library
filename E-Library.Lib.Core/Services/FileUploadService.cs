using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_Library.Lib.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _config;
            

        public FileUploadService(IConfiguration configuration)
        {
            _config = configuration;

            Account account = new Account
            {
                Cloud = _config["CloudinarySecret:CloudName"],
                ApiKey = _config["CloudinarySecret:ApiKey"],
                ApiSecret = _config["CloudinarySecret:ApiSecret"],
            };
            _cloudinary = new Cloudinary(account);
        }

        public string UploadImage(IFormFile file)
        {
            var imageUploadResult = new ImageUploadResult();


            if (file.Length <= 0)
                throw new InvalidOperationException("Invalid file size");

            using (var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, fs),
                    Transformation = new Transformation().Width(300).Height(300).Crop("fill")
                };
                imageUploadResult = _cloudinary.Upload(imageUploadParams);
            }

            return imageUploadResult.Url.ToString();
        }

    }
}
