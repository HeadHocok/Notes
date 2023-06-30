using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    /// <summary>
    /// Создаёт конфигурацию из <T> в тип назначения.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
