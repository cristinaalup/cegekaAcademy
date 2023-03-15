namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundraiserRegistry;
    private int donationsInRon = 0;

    public PetShelter(IRegistry<Pet> petRegistry, IRegistry<Person> donorRegistry, IRegistry<Fundraiser> fundraiserRegistry)
    {
        petRegistry = petRegistry ?? throw new ArgumentNullException(nameof(petRegistry));
        donorRegistry = donorRegistry ?? throw new ArgumentNullException(nameof(donorRegistry));
        fundraiserRegistry = fundraiserRegistry ?? throw new ArgumentNullException(nameof(fundraiserRegistry));
    }

    public void RegisterPet(Pet pet)
    {
        if (pet == null)
        {
            throw new ArgumentNullException(nameof(pet));
        }
        petRegistry.Register(pet);
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        if (fundraiser == null)
        {
            throw new ArgumentNullException(nameof(fundraiser));
        }
        fundraiserRegistry.Register(fundraiser);
    }

    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRegistry.GetAll().Result; 
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result;
    }

    public Fundraiser GetFundraiserByName(string name)
    {
        return fundraiserRegistry.GetByName(name).Result;
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, int amountInRon)
    {
        if (donor == null)
        {
            throw new ArgumentNullException(nameof(donor));
        }

        donorRegistry.Register(donor);
        donationsInRon += amountInRon;
    }

    public int GetTotalDonationsInRON()
    {
        return donationsInRon;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }
}