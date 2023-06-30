
namespace Notes.Persistence
{
    /// <summary>
    /// Создаёт базу данных, если таковой нет.
    /// </summary>
    public class DbInitializer
    {
        public static void Initialize(NotesDbContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}
