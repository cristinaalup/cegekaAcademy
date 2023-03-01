

using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context)
    {
    }
    public async Task<decimal> GetRaisedAmount(int fundraiserId)
    {
        IReadOnlyList<Donation> donations = await _context.Set<Donation>()
            .Where(d => d.FundraiserId == fundraiserId)
            .ToListAsync();

        return donations.Sum(d => d.Amount);
    }

}
