using PetShelterDemo.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public class Fundraiser:INamedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DonationTarget { get; set; }
        public int TotalDonations { get; set; }
        public List<Person> Donors { get; set; }
      //  public IRegistry<Person> donorRegistry;

        public Fundraiser(string title, string description, int donationTarget)
        {
            Name = title;
            Description = description;
            DonationTarget = donationTarget;
            TotalDonations = 0;
            Donors=new List<Person>() { };
          //  donorRegistry = new Registry<Person>(new Database());
        }

        public void AddDonation(int donation, Person person)
        {
            // Donations.Add(donation);
            TotalDonations += donation;
           // donorRegistry.Register(person);
            Donors.Add(person);
        }

    }
}
