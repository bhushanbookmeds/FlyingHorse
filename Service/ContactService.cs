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
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService (IUnitOfWork unitOfWork)             // initialising the constructor 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Contact> GetId(int id)                       // get the id to return the contact details 
        {
            var iD = _unitOfWork.ContactRepository.GetByIDAsync(id);
            return await iD;
        }

        public async Task Update(Contact contact)                   // getId will be called first to check if the id exists & then pass the object to update the contact details 
        {
            _unitOfWork.ContactRepository.Update(contact);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Contact contact)                     // getId will be called first to check if the id exists & pass the object to delete the contact details 
        {
            _unitOfWork.ContactRepository.Delete(contact);
            await _unitOfWork.SaveAsync();
        }

        public async Task Insert(Contact contact)
        {
            await _unitOfWork.ContactRepository.InsertAsync(contact);          // await is used whenever async is used .
            await _unitOfWork.SaveAsync();
        }
    }
}
