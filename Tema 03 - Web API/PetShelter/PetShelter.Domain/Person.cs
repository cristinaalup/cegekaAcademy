using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain
{
    public class Person:INamedEntity
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; }

        public string IdNumber { get; }

        public Person(string idNumber, string name, DateTime? dateOfBirth = null)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            IdNumber = idNumber;
        }
    }
}
