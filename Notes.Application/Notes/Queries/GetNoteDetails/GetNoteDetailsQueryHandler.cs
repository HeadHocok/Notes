using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Notes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    /// <summary>
    /// Обработчик деталей заметок.
    /// </summary>
    public class GetNoteDetailsQueryHandler
        : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm> //Наводимся и читаем. Вообще полезная привычка.
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;

        //Присваивание значений выше кортежом.
        //Это позволяет использовать зависимости, не зная их конкретную реализацию. (DI)
        public GetNoteDetailsQueryHandler(INotesDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        //Нахождение деталей заметки.
        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes
                .FirstOrDefaultAsync(note =>
                note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}