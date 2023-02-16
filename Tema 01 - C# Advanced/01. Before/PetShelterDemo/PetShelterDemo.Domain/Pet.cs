﻿namespace PetShelterDemo.Domain
{
    public class Pet
    {
        public string Name { get; }
        public string Description { get; }

        public Pet(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
