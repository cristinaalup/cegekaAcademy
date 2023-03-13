using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DomainModel;


namespace PetShelter.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task CreatePersonAsync(string name, string id)
        {
            var person = await _personRepository.GetPersonByIdNumber(id);
           
            await _personRepository.Update(person);
        }

        public async Task<IReadOnlyList<Person>> GetAllPersonsAsync()
        {
            var persons = await _personRepository.GetAll();
            return persons.Select(p=>p.ToDomainModel()).ToList();
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            var person = await _personRepository.GetPersonByIdNumber(id);

            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            return person.ToDomainModel();
        }
    }

}
