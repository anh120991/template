using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.SQLData.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {

        //IUserRepository<Author> AuthorRepository { get; }

        /// <summary>
        /// Asynchronously commits all changes
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// Synchronously commits all changes
        /// </summary>
        void Commit();
    }
}