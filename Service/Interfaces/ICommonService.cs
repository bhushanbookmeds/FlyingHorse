using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces 
{
    public interface ICommonService
    {
        Task<IList<State>> GetStates(int countryId);
        Task<IList<Country>> GetCountries();
        Task<IList<ContactType>> GetTypes();
    }
}
