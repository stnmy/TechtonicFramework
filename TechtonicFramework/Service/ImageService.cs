using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TechtonicFramework.Extensions;

namespace TechtonicFramework.Services
{
    public class ImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService()
        {
            var settings = new CloudinarySettings
            {
                CloudName = ConfigurationManager.AppSettings["Cloudinary:CloudName"],
                ApiKey = ConfigurationManager.AppSettings["Cloudinary:ApiKey"],
                ApiSecret = ConfigurationManager.AppSettings["Cloudinary:ApiSecret"]
            };

            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddImageAsync(HttpPostedFileBase file)
        {
            var uploadResult = new ImageUploadResult();

            if (file != null && file.ContentLength > 0)
            {
                using (var stream = file.InputStream)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = "Techtonic"
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
