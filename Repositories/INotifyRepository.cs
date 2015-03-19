using System;
using System.Collections.Generic;

namespace FirmwarePacking.Repositories
{
    /// <summary>Репозиторий, поддерживающий уведомления об изменении содержимого</summary>
    public interface INotifyRepository : IRepository, IDisposable
    {
        /// <summary>Событие, возникающее при изменении содержимого репозитория</summary>
        event EventHandler<RepositoryUpdatedEventArgs> Updated;
    }

    /// <summary>Аргумент события изменения содержимого репозитория</summary>
    public class RepositoryUpdatedEventArgs : EventArgs
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.EventArgs" />.</summary>
        public RepositoryUpdatedEventArgs(ICollection<IRepositoryElement> AddedElements, ICollection<IRepositoryElement> RemovedElements)
        {
            this.AddedElements = AddedElements;
            this.RemovedElements = RemovedElements;
        }

        public ICollection<IRepositoryElement> AddedElements { get; private set; }
        public ICollection<IRepositoryElement> RemovedElements { get; private set; }

        public static RepositoryUpdatedEventArgs CreateAddedEventArgs(params IRepositoryElement[] Elements)
        {
            return new RepositoryUpdatedEventArgs(Elements, new IRepositoryElement[0]);
        }

        public static RepositoryUpdatedEventArgs CreateRemovedEventArgs(params IRepositoryElement[] Elements)
        {
            return new RepositoryUpdatedEventArgs(new IRepositoryElement[0], Elements);
        }
    }
}
