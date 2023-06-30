using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Application.Interfaces
{
    /// <summary>
    /// Сохранение изменений контекста в БД.
    /// </summary>
    public interface INotesDbContext
    {
        public DbSet<Note> Notes { get; set; } //!!!абстракция в Application слое зависит от DbSet. Application слой напрямую вызывает методы взаимодействия с сущностью бд, что нарушает абстракцию. Изъян.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
