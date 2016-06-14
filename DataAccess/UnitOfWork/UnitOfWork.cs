#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataAccess.GenericRepository;
using System.Reflection.Emit;



#endregion

namespace DataAccess.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...

        private readonly IdtDnaDataEntities _context = null;
        private GenericRepository<SPEC_OLIGO> _specOligoRepository;
        private GenericRepository<SPEC_DELIVERY> _specDeliveryRepository;
       
        #endregion

        public UnitOfWork()
        {
            _context = new IdtDnaDataEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        public GenericRepository<SPEC_DELIVERY> SpecDeliveryRepository
        {
            get
            {
                if (this._specDeliveryRepository == null)
                    this._specDeliveryRepository = new GenericRepository<SPEC_DELIVERY>(_context);
                return _specDeliveryRepository;
            }
        }

        /// <summary>
        /// Gets/Sets Property for user repository.
        /// </summary>
        public GenericRepository<SPEC_OLIGO> SpecOligoRepository
        {
            get
            {
                if (this._specOligoRepository == null)
                    this._specOligoRepository = new GenericRepository<SPEC_OLIGO>(_context);
                return _specOligoRepository;
            }
        }

        /// <summary>
        /// Gets data after executing query  passed to it
        /// </summary>
        /// <param name="resultType">Custom type</param>
        /// <param name="query">Custom query to execute</param>
        /// <returns></returns>
        public dynamic GetdataByProcedure(Type resultType, string query)
        {
            // List<List<object>> obj = new List<List<object>>();
            //query = "SELECT TOP 5 CAST(SPEC_DELIVERY.SPEC_DELIVERY_ID AS VARCHAR(100)) AS SPEC_DELIVERY_ID , CAST(SPEC_OLIGO.SPEC_OLIGO_ID AS VARCHAR(100)) AS SPEC_OLIGO_ID,CAST(SPEC_DELIVERY.REF_ID AS VARCHAR(100)) AS REF_ID  FROM SPEC_DELIVERY INNER JOIN" +
            //    " SPEC_OLIGO ON SPEC_DELIVERY.SPEC_DELIVERY_ID = SPEC_OLIGO.SPEC_DELIVERY_ID WHERE SPEC_DELIVERY.SPEC_DELIVERY_ID >= 309";
            dynamic queryResult = _context.Database.SqlQuery(resultType, query);
            //var list = _context.Database.SqlQuery<BusinessEntities.CompositeType>(query);
            ////var dynamicResults = list..Select(o => o.AsDynamic()).ToList();
            return queryResult;
        }


        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                // Calling database savechanges
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //Logs all the errors in save operation
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

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
                    Debug.WriteLine("UnitOfWork is being disposed");
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