﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public interface IAddDonation
    {
        void AddDonation(int donationValue, Person person);
    }
}