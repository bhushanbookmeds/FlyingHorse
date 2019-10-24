using System;
using System.Collections.Generic;
using System.Linq;
using NonProfitCRM.Data;
using NonProfitCRM.Models;


namespace NonProfitCRM.Services
{
    public class CommonServices : ICommonServices
    {
        private readonly UnitOfWork _unitOfWork;
        public CommonServices()
        {
            _unitOfWork = new UnitOfWork();

        }
        public IEnumerable<Country> GetCountries()
        {
            var countries = _unitOfWork.CountryRepository.GetAll();
            return countries;
        }

        public IEnumerable<State> GetStates(int CountryId)
        {
            var states = _unitOfWork.StateRepository.GetMany(s => s.CountryId == CountryId);
            return states;
        }
        public IEnumerable<PhoneCodeModel> GetPhoneCode()
        {
            var country = _unitOfWork.CountryRepository.GetAll().ToList();

            var phoneCodes = new List<PhoneCodeModel>();

            country.ForEach(c =>
            {
                phoneCodes.Add(new PhoneCodeModel { Id = c.PhoneCode.Value, PhoneCode = c.PhoneCode.Value });
            });

            return phoneCodes;
          
        }
    }
}


