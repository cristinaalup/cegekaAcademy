namespace PetShelterDemo.Domain.Interfaces;

public interface ICustomRegistry<T>
{
    Task<IReadOnlyList<T>> GetAll();
    Task<T> GetByName(string name);
    Task Register(T item);
}
