using Core.Domain;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork
    {
        IRepository<Organization> OrganizationRepository { get; }    // property is defined here 
        IRepository<Users> UsersRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IRepository<UserRoleMapping> UserRoleMappingRepository { get; }
        IRepository<Contact> ContactRepository { get; }
        IRepository<ContactType> ContactTypeRepository { get; }
        IRepository<State> StateRepository { get; }
        IRepository<Country> CountryRepository { get; }

        void Save();
        Task SaveAsync();

    }
}
