using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using S3Train.Domain;
using X.PagedList;

namespace S3Train
{
    public interface IGenenicServiceBase<T> where T : EntityBase
    {
        /// <summary>
        /// Select all data
        /// </summary>
        /// <returns></returns>
        List<T> SelectAll();

        IPagedList<T> Gets(int? pageIndex, int pageSize = 20, Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        /// <summary>
        /// Select all data Type IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> SelectAllTypeIQueryable();

        /// <summary>
        /// Get entity by Id, return null if not found
        /// </summary>
        /// <param name="id">The identifier.</param>
        T GetById(Guid id);

        /// <summary>
        /// Add New Item In entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// Update Item In entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Change status
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        void ChangeStatus(T entity, bool status);

        /// <summary>
        /// Delete Item In entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Send mail To One Email
        /// </summary>
        /// <param name="entity"></param>
        void SendOneEmail(string to, string from, string subject, string body);

        /// <summary>
        /// Send mail to multy email
        /// </summary>
        /// <param name="entity"></param>
        void SendMultyEmail(string to, string from, string subject, string body);


        string UpFile(HttpPostedFileBase a, string url);

        void UpManyFile(IEnumerable<HttpPostedFileBase> a, string url);

    }
}