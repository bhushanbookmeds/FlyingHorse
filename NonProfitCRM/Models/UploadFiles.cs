using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NonProfitCRM.Models
{
    public class UploadFiles
    {
        public IList<IFormFile> Picture{ get; set; }
        public int Id { get; set; }
    }
}
