using AutoMapper;
using NonProfitCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public class AutoMapperIntializer
    {
        public static void Intializer()
        {
            Mapper.Initialize(cfg =>
           {
               cfg.CreateMap<RegistrationModel, Users>();

               cfg.CreateMap<Users, RegistrationModel>();
           });
        }
    }
}
