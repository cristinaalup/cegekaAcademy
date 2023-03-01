

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal DonationTarget { get; set; }
    public decimal TotalDonations { get; set; }
    public int? DonorId { get; set; }
   // public int DonationId { get; set; }
    public ICollection<Person> Donors { get; set; }
 //   public Donation donation { get; set; }

}
