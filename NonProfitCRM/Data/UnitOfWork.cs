using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitCRM.Models;

namespace NonProfitCRM.Data
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...

        private DB_3221_crmContext _context = new DB_3221_crmContext();

        private GenericRepository<Organization> organizationRepository;
        private GenericRepository<Users> usersRepository;
        private GenericRepository<UserRole> userRoleRepository;
        private GenericRepository<UserRoleMapping> userRoleMappingRepository;

        #endregion


        public UnitOfWork()
        {
            _context = new DB_3221_crmContext();
        }

        public GenericRepository<Organization> OrganizationRepository
        {
            get
            {
                if (this.organizationRepository == null)
                    this.organizationRepository = new GenericRepository<Organization>(_context);
                return organizationRepository;
            }
        }

        public GenericRepository<Users> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                    this.usersRepository = new GenericRepository<Users>(_context);
                return usersRepository;
            }
        }

        public GenericRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (this.userRoleRepository == null)
                    this.userRoleRepository = new GenericRepository<UserRole>(_context);
                return userRoleRepository;
            }
        }

        public GenericRepository<UserRoleMapping> UserRoleMappingRepository
        {
            get
            {
                if (this.userRoleMappingRepository == null)
                    this.userRoleMappingRepository = new GenericRepository<UserRoleMapping>(_context);
                return userRoleMappingRepository;
            }
        }

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();

            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{

            //    var outputLines = new List<string>();
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
            //        }
            //    }
            //    System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

            //    throw e;
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
