using PetShelterDemo.DAL;
using PetShelterDemo.Domain.Interfaces;
using PetShelterDemo.Domain.Models;
using PetShelterDemo.Domain.Registries;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Fundraiser> fundraiserRegistry;
    private int donationsInRon = 0;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        fundraiserRegistry = new Registry<Fundraiser>(new Database());
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