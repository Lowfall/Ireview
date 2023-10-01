using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ireview.Web.Configuration;
using Ireview.Web.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Web.Services
{
    public class ImageService :IImageService
    {
        private readonly Cloudinary cloudinary;

        public ImageService(IOptions<CloudinarySettings> options)
        {
            var acc = new Account()
            {
                ApiKey = options.Value.ApiKey,
                ApiSecret = options.Value.ApiSecret,
                Cloud = options.Value.CloudName
            };

            cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoResult(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")

                };
                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
           
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoResult(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
