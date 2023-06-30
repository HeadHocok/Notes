using AutoMapper;
using System.Reflection;

namespace Notes.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        //ищет все типы в указанной сборке, которые реализуют обобщенный интерфейс IMapWith<T> и выполняет метод Mapping на каждом из них.
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingsFromAssembly(assembly);

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            //получается список всех типов в указанной сборке, которые реализуют интерфейс IMapWith<T> и сохраняются в переменную types.
            var types = assembly.GetExportedTypes() 
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                //Перебор каждого типа в списке types, создание объекта данного типа с помощью рефлексии
                //Вызов метода Mapping на созданном объекте, передавая ему текущий экземпляр AssemblyMappingProfile
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
