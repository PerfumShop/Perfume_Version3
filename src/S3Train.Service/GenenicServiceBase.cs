using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Web;
using S3Train.Domain;
using X.PagedList;

namespace S3Train
{
    /// <summary>
    /// Base generic class for Service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenenicServiceBase<T> : IGenenicServiceBase<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext DbContext;

        protected DbSet<T> EntityDbSet => DbContext.Set<T>();

        public string errorMessage = string.Empty;

        protected GenenicServiceBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Select all data
        /// </summary>
        /// <returns></returns>
        public List<T> SelectAll()
        {
            return this.EntityDbSet.ToList();
        }
        public List<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return orderBy(EntityDbSet).ToList();
        }
        public IQueryable<T> Gets(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var iQueryableDbSet = @where != null ? EntityDbSet.Where(@where) : EntityDbSet;
            return orderBy != null ? orderBy(iQueryableDbSet) : iQueryableDbSet;
        }
        public IPagedList<T> Gets(int? pageIndex, int pageSize = 20, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var page = pageIndex ?? 1;

            var iQueryableDbSet = @where != null ? EntityDbSet.Where(@where) : EntityDbSet;

            return orderBy != null ? orderBy(iQueryableDbSet).ToPagedList(page, pageSize) : iQueryableDbSet.ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get entity by Id, return null if not found
        /// </summary>
        /// <param name="id">The identifier.</param>
        public T GetById(Guid id)
        {
            return this.EntityDbSet.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Select all data and resuilt is IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> SelectAllTypeIQueryable()
        {
            return this.EntityDbSet.AsQueryable();
        }

        /// <summary>
        /// Add new entity and save entity
        /// </summary>
        /// <param name="entity">model</param>
        public void Insert(T entity)
        {
            try
            {
                if(entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.EntityDbSet.Add(entity);
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        /// <summary>
        /// Update entity and save change of entity
        /// </summary>
        /// <param name="entity">model</param>
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">model</param>
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.EntityDbSet.Remove(entity);
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        /// <summary>
        /// Send mail from your email to One email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendOneEmail(string to, string from, string subject, string body)
        {
            try
            {
                var senderEmail = new MailAddress(from, "Xuan Son");
                var receiverEmail = new MailAddress(to, "Receiver");
                var password = "long1234";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Send mail from your email to multy email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendMultyEmail(string to, string from, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from, "Xuan Son");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                var pass = "long1234";

                string[] Multi = to.Split(',');
                foreach (string item in Multi)
                {
                    mailMessage.To.Add(new MailAddress(item));
                }

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailMessage.From.Address, pass)
                };

                smtp.Send(mailMessage);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public string UpFile(HttpPostedFileBase a, string url)
        {
            string fileName = "";
            if (a != null && a.ContentLength > 0)
            {
                fileName = Path.GetFileName(a.FileName).ToString();
                string path = Path.Combine(url, fileName);
                a.SaveAs(path);
                return fileName;
            }
            else
            {
                return fileName;
            }
        }

        public void ChangeStatus(T entity, bool status)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                entity.UpdatedDate = DateTime.Now;
                entity.IsActive = status;
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void UpManyFile(IEnumerable<HttpPostedFileBase> a, string url)
        {
            foreach(var file in a)
            {
                string fileName = "";
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName).ToString();
                    string path = Path.Combine(url, fileName);
                    file.SaveAs(path);
                }
            }
        }
    }
}
