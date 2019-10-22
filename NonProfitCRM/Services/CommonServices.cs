using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}


