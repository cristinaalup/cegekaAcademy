

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
    public void Configure(EntityTypeBuilder<Fundraiser> builder)
    {
        // Primary key
        builder.HasKey(x => x.Id);

        //builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        //builder.Property(x=>x.Description).IsRequired().HasMaxLength(255);
        //builder.Property(x => x.DonationTarget).IsRequired();
        builder.Property(f => f.Title).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Description).HasMaxLength(1000);
        builder.Property(f => f.DonationTarget).IsRequired();

        //  builder.HasOne(p=>p.donation).WithOne(p=>p.Donor);
        builder.HasMany(x => x.Donors)
              .WithOne(f => f.Fundraiser)
              .HasForeignKey(d => d.FundraiserId).IsRequired();
    }
}
