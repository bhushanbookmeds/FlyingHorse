using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public interface IImageService
    {
        Task<string> ImageUpload(IFormFile file, string folderName = null);
    }
}