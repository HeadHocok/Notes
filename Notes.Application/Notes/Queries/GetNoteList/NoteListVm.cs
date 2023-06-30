using System.Collections.Generic;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// View-модель. Класс который описывает то, что будет возвращаться пользователю, запрашиваемый лист заметок.
    /// </summary>
    public class NoteListVm
    {
        public IList<NoteLookupDto> Notes { get; set; } = null!;
    }
}