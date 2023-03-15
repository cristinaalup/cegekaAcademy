using PetShelterDemo.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public class Fundraiser:INamedEntity, IAddDonation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DonationTarget { get; set; }
        public int TotalDonations { get; set; }
        public List<Person> Donors { get; set; }

        public Fundraiser(string title, string description, int donationTarget)
        {
            Name = title;
            Description = description;
            DonationTarget = donationTarget;
            TotalDonations = 0;
            Donors=new List<Person>() { };
        }

        public void AddDonation(int donationValue, Person person)
        {
            TotalDonations += donationValue;
            Donors.Add(person);
        }
    }
}
