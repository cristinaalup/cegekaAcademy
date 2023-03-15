using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelterDemo.Domain.Models;

namespace PetShelterDemo.Domain.Interfaces
{
    public interface IAddDonation
    {
        void AddDonation(int donationValue, Person person);
    }
}
