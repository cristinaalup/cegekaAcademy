//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;
using System.Collections.Generic;

var shelter = new PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!");

var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Break our database connection", BreakDatabaseConnection },
                { "Add fundraiser", AddFundraiser },
                { "See our fundraisers", SeeFR },
                { "Donate to our fundraisers", DonateToFR },
                { "See our fundraisers' donations", SeeFRDonations },
                { "Leave:(", Leave }
            }
        ) ;
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}

void AddFundraiser()
{
    //var name = ReadString("Title?");
    //var id = ReadString("Id?");
    //var description = ReadString("Description?");
    //var target = ReadString("Target?");
    //var fundraiser=new Fundraiser(title, id, description, name);
    //// var total = ReadString("Description?");
    //// var Donors = ReadString("Description?");

    Console.WriteLine("Let's create a new fundraiser!");
    Console.Write("Title: ");
    string title = Console.ReadLine();
    Console.Write("Description: ");
    string description = Console.ReadLine();
    Console.Write("Donation target: ");
    int donationTarget = int.Parse(Console.ReadLine());

    var fundraiser = new Fundraiser(title, description, donationTarget);
    shelter.RegisterFundraiser(fundraiser);

}

void DonateToFR()
{
    Console.WriteLine("Your name:");
    var name = ReadString();
    Console.WriteLine("Your id:");
    var id = ReadString();
    Console.WriteLine("Amount of money you want to donate:");
    var sum= ReadInteger();
    Console.WriteLine("What fundraiser would you like to donate to?:");
    var title= ReadString();
    var fr = shelter.GetFundraiserByName(title);
    var person = new Person(name, id);
    fr.AddDonation(sum,person );
}

void SeeFR()
{
    
    Console.WriteLine("Fundraisers:");
    var fundraisers = shelter.GetAllFundraisers();
    foreach (var fr in fundraisers)
    {
        Console.WriteLine(fr.Name);
    }

}

void SeeFRDonations()
{
    Console.WriteLine("what fundraiser do you want to see the donations?");
  //  var title = ReadString();
    var fundraisers = shelter.GetAllFundraisers();
    
    Console.WriteLine("donations:");
    foreach (var fr in fundraisers)
    {
        
        if (fr.Donors != null)
        {
            Console.WriteLine($"Fundraiser: {fr.Name}");
            Console.WriteLine("Donors:\n");
            fr.Donors.ForEach(i => Console.WriteLine("{0}\t", i.Name));
            Console.WriteLine($"TotalDonations: {fr.TotalDonations}");
        }
        else
        {
            Console.WriteLine("there are not donations available");
        }
        
    }

}
void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate? (RON)");
    var amountInRon = ReadInteger();
    shelter.Donate(person, amountInRon);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonationsInRON()}RON");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}

int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}