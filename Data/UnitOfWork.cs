using Core.Domain;
using Data.Repository;
using System;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IDisposable,IUnitOfWork
    {
        #region Private member variables...

        private NonProfitCrmDbContext _context;

        private IRepository<Organization> organizationRepository;  // object is named  
        private IRepository<Users> usersRepository;
        private IRepository<UserRole> userRoleRepository;
        private IRepository<UserRoleMapping> userRoleMappingRepository;
        private IRepository<Contact> contactRepository ;
        private IRepository<ContactType> contactTypeRepository ;
        private IRepository<State> stateRepository;
        private IRepository<Country> countryRepository;
        private IRepository<Donation> donationRepository;

        #endregion


        public UnitOfWork(NonProfitCrmDbContext nonProfitCrmDbContext)
        {
            _context = nonProfitCrmDbContext;
        }

        public IRepository<Organization> OrganizationRepository
        {
            get
            {
                if (this.organizationRepository == null)    // singleton pattern 
                    this.organizationRepository = new Repository<Organization>(_context);     // object is created is here 
                return organizationRepository;
            }
        }

        public IRepository<Users> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                    this.usersRepository = new Repository<Users>(_context);
                return usersRepository;
            }
        }

        public IRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (this.userRoleRepository == null)
                    this.userRoleRepository = new Repository<UserRole>(_context);
                return userRoleRepository;
            }
        }

        public IRepository<UserRoleMapping> UserRoleMappingRepository
        {
            get
            {
                if (this.userRoleMappingRepository == null)
                    this.userRoleMappingRepository = new Repository<UserRoleMapping>(_context);
                return userRoleMappingRepository;
            }
        }


        public IRepository<Contact> ContactRepository
        {
            get
            {
                if (this.contactRepository == null)
                    this.contactRepository = new Repository<Contact>(_context);
                return contactRepository;
            }
        }

        public IRepository<ContactType> ContactTypeRepository
        {
            get
            {
                if (this.contactTypeRepository == null)
                    this.contactTypeRepository = new Repository<ContactType>(_context);
                return contactTypeRepository;
            }
        }

        public IRepository<State> StateRepository
        {
            get
            {
                if (this.stateRepository == null)
                    this.stateRepository = new Repository<State>(_context);
                return stateRepository;
            }
        }

        public IRepository<Country> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    this.countryRepository = new Repository<Country>(_context);
                return countryRepository;
            }
        }

        public IRepository<Donation> DonationRepository
        {
            get
            {
                if (this.donationRepository == null)
                    this.donationRepository = new Repository<Donation>(_context);
                return donationRepository;
            }
        }
        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            int v = _context.SaveChanges();

            ////try
            ////{
            ////    _context.SaveChanges();
            ////}
            ////catch (DbEntityValidationException e)
            ////{

            ////    var outputLines = new List<string>();
            ////    foreach (var eve in e.EntityValidationErrors)
            ////    {
            ////        outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
            ////        foreach (var ve in eve.ValidationErrors)
            ////        {
            ////            outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
            ////        }
            ////    }
            ////    System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

            ////    throw e;
            //}

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        //public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        //{
        //    return _context.Database.SqlQuery<T>(query, parameters);
        //}

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
