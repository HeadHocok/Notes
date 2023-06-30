using System;
using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// Запрос на получение списка заметок.
    /// </summary>
    public class GetNoteListQuery : IRequest<NoteListVm>
    {
        public Guid UserId { get; set; }
    }
}