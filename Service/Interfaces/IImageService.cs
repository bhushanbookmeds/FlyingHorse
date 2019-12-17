using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    interface IImageService
    {
        Task<string> ImageUpload(IFormFile file);
    }
}
