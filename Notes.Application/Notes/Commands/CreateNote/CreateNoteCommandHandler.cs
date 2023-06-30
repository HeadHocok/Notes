using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Cодержит логику создания на основе информации из унаследованной CreateNoteCommand.
    /// </summary>
    public class CreateNoteCommandHandler 
        : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _dbcontext;

        public CreateNoteCommandHandler(INotesDbContext dbcontext) =>
            _dbcontext = dbcontext;

        public async Task<Guid> Handle(CreateNoteCommand request, //Логика обработки команды. Формирует заметку и возвращает Id.
            CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _dbcontext.Notes.AddAsync(note, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
