﻿using PetShelterDemo.DAL;

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
        petRegistry.Register(pet);
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundraiserRegistry.Register(fundraiser);
    }

    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Fundraiser GetFRByName(string name)
    {
        return fundraiserRegistry.GetByName(name).Result;
    }
    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, int amountInRON)
    {
        donorRegistry.Register(donor);
        donationsInRon += amountInRON;
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