using PetShelter.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.DataAccessLayer.Repository
{
    public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
    {
        public FundraiserRepository(PetShelterContext context) : base(context)
        {
        }

        public decimal GetCurrentRaisedAmount(int fundraiserId)
        {
            var fundraiser = _context.Fundraisers.Find(fundraiserId);

            if (fundraiser == null || fundraiser.Donors == null)
            {
                return 0;
            }

            return fundraiser.Donors.Sum(d => d.Donations.Sum(dd => dd.Amount));
        }
    }
}
