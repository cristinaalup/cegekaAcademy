﻿using PetShelterDemo.Domain.Interfaces;

namespace PetShelterDemo.Domain.Models
{
    public class Person : INamedEntity
    {
        public string Name { get; }
        public string IdNumber { get; }

        public Person(string name, string idNumber)
        {
            Name = name;
            IdNumber = idNumber;
        }
    }
}
