using NonProfitCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public interface ICommonServices
    {
        IEnumerable<Country> GetCountries();
        IEnumerable<State> GetStates(int CountryId);
        
    }
}
