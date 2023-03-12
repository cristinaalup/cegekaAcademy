using PetShelter.DataAccessLayer.Models;


namespace PetShelter.DataAccessLayer.Repository
{
    public interface IFundraiserRepository:IBaseRepository<Fundraiser>
    {
        decimal GetCurrentRaisedAmount(int fundraiserId);
    }
}
