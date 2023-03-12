using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain
{
    public class Fundraiser: INamedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DonationTarget { get; set; }
        public int TotalDonations { get; set; }
        public List<Person> Donors { get; set; }
    }
}
