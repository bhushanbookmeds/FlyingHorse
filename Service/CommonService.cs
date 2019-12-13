using Core.Domain;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Data;
using System.Threading.Tasks;
using System.Linq;

namespace Service
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<Country>> GetCountries()
        {
            var countries = await _unitOfWork.CountryRepository.GetAllAsync();
            return countries.ToList();
        }

        public async Task<IList<State>> GetStates(int countryId)
        {
            var states = await _unitOfWork.StateRepository.GetManyAsync(State => State.CountryId == countryId);
            return states.ToList();
        }

        public async Task<IList<ContactType>> GetTypes()
        {
            var types = await _unitOfWork.ContactTypeRepository.GetAllAsync();
            return types.ToList();
        }
    }
}
