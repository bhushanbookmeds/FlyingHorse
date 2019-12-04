using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IContactService
    {
        Task<Contact> GetId(int id);
        Task Delete(Contact contact);
        Task Update(Contact contact);
        Task Insert(Contact contact);

    }
}
