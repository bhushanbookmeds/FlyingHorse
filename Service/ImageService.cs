using Bogus.Premium;
using Data;
using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using ContextHelper = Core.Domain.ContextHelper;

namespace Service
{
    public class ImageService : IImageService
    {
        public async Task<string> ImageUpload(IFormFile file, string folderName = null)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                folderName = "Banners";
            //Upload Image
            string FileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            FileName = FileName + DateTime.Now.ToString("yymmssfff") + extension;
            var newFolderName = "wwwroot\\" + folderName;
            var mediaFolderPath = Path.Combine(Directory.GetCurrentDirectory(), newFolderName);
            var path = Path.Combine(mediaFolderPath, FileName);
            bool exists = Directory.Exists(mediaFolderPath);
            if (!exists)
                Directory.CreateDirectory(mediaFolderPath);
            using (var bits = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }
            return Path.Combine(ContextHelper.GetHostingUrl(), folderName, FileName);
        }
    }
}