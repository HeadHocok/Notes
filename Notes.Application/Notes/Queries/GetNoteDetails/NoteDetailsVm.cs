using System;
using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    /// <summary>
    /// Запрос на получение списка заметок.
    /// View-модель. Класс который описывает то, что будет возвращаться пользователю, запрашиваемый детали заметки.
    /// </summary>
    public class NoteDetailsVm : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Details { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteDetailsVm>()
                .ForMember(noteVm => noteVm.Title,
                    opt => opt.MapFrom(note => note.Title))
                .ForMember(noteVm => noteVm.Details,
                    opt => opt.MapFrom(note => note.Details))
                .ForMember(noteVm => noteVm.Id,
                    opt => opt.MapFrom(note => note.Id))
                .ForMember(noteVm => noteVm.CreationDate,
                    opt => opt.MapFrom(note => note.CreationDate))
                .ForMember(noteVm => noteVm.EditDate,
                    opt => opt.MapFrom(note => note.EditDate));
        }
    }
}

//Перегруженный метод ForMember принимает два аргумента: первый аргумент - лямбда-выражение,
//которое указывает на свойство, к которому применяется правило маппинга,
//и второй аргумент - объект типа Action<IMemberConfigurationExpression<Note, NoteDetailsVm>>.
//Второй аргумент позволяет настраивать дополнительные параметры маппинга,
//такие как сопоставление значений между свойствами.