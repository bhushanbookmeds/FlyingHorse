using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NonProfitCRM.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public int EntityId { get; set; }

        public string EntityType { get; set; }
        public string PictureUrl { get; set; }

    }
}
