using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public interface IImageService
    {
        Task<string> ImageUpload(IFormFile file, string folderName = null);
    }
}

