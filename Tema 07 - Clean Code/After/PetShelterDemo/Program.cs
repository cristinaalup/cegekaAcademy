using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

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
                { "See our fundraisers", GetFundraiser },
                { "Donate to our fundraisers", DonateToFundraiser },
                { "See our fundraisers' donations", GetFundraisersDonations },
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
    Fundraiser fundraiser=WriteFundraiser();
    shelter.RegisterFundraiser(fundraiser);
}

Fundraiser WriteFundraiser()
{
    Console.WriteLine("Let's create a new fundraiser!");

    Console.Write("Title: ");
    string title = Console.ReadLine();

    Console.Write("Description: ");
    string description = Console.ReadLine();

    int donationTarget = ValidateInteger();

    Fundraiser fundraiser=new Fundraiser(title, description, donationTarget);
    return fundraiser;
}

int ValidateInteger()
{
    int donationTarget;
    while (true)
    {
        Console.Write("Donation target: ");
        if (int.TryParse(Console.ReadLine(), out donationTarget))
        {
            break;
        }
        Console.WriteLine("Invalid input. Please enter a valid integer.");
    }
    return donationTarget;
}

void DonateToFundraiser()
{
    Person donor = WritePerson();

    Console.Write("What fundraiser would you like to donate to?: ");
    string title = Console.ReadLine();

    Fundraiser fundraiser = shelter.GetFundraiserByName(title);
    CheckIfFundraiserExist(fundraiser);

    int sum = ValidateInteger();

    fundraiser.AddDonation(sum, donor);
}

Person WritePerson()
{
    Console.WriteLine("Please provide the following information:");

    Console.Write("Your name: ");
    string name = Console.ReadLine();

    Console.Write("Your ID: ");
    string id = Console.ReadLine();

    return new Person(name, id);
}

void CheckIfFundraiserExist(Fundraiser fundraiser)
{
    if (fundraiser == null)
    {
        Console.WriteLine("No fundraiser found with that title.");
        return;
    }

}

void GetFundraiser()
{
    
    Console.WriteLine("Fundraisers:");
    var fundraisers = shelter.GetAllFundraisers();
    foreach (var fr in fundraisers)
    {
        Console.WriteLine(fr.Name);
    }

}

void GetFundraisersDonations()
{
    Console.WriteLine("what fundraiser do you want to see the donations?");
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
    Pet pet = WritePet();
    shelter.RegisterPet(pet);
}

Pet WritePet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    return new Pet(name, description);
}

void Donate()
{
    Person donor = WritePerson();

    Console.WriteLine("How much would you like to donate? (RON)");
    var amountInRon = ReadInteger();
    shelter.Donate(donor, amountInRon);
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