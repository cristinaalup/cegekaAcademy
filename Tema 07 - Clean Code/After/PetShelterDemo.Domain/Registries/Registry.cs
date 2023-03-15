using PetShelterDemo.DAL;
using PetShelterDemo.Domain.Interfaces;

namespace PetShelterDemo.Domain.Registries
{
    internal sealed class Registry<T> : IRegistry<T> where T : INamedEntity
    {
        private readonly Database database;
        public Registry(Database database)
        {
            this.database = database;
        }

        public async Task Register(T instance) 
        {
            await database.Insert(instance);
        }

        public async Task<IReadOnlyList<T>> GetAll() 
        {
            return await database.GetAll<T>();
        }

        public async Task<T> GetByName(string name)
        {
            return (await database.GetAll<T>()).Single(o => o.Name == name);
        }

        public async Task<IReadOnlyList<T>> Find(Func<T, bool> filter)
        {
            return (await database.GetAll<T>()).Where(filter).ToList();
        }
    }
}
