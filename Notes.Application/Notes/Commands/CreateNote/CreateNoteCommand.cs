using System;
using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Используется как запрос для создания заметки и возвращает результат определенного типа (Id заметки).
    /// Содержит только информацию о том, что необходимо для создания заметки.
    /// </summary>
    public class CreateNoteCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Details { get; set; } = null!;
    }
}
