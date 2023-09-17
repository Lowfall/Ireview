using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Web.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> AddPhotoResult(IFormFile file);
        Task<DeletionResult> DeletePhotoResult(string publicId);
    }
}
