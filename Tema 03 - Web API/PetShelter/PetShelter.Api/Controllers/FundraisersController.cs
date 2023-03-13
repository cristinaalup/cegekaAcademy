using Microsoft.AspNetCore.Mvc;
using PetShelter.Api.Resources;
using PetShelter.Domain.Services;
using PetShelter.Api.Resources.Extensions;
using System.Collections.Immutable;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundraisersController : ControllerBase
    {
        private readonly IFundraiserService _fundraiserService;

        public FundraisersController(IFundraiserService fundraiserService)
        {
            this._fundraiserService = fundraiserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<Fundraiser>>> GetFundraisers()
        {
            var data = await this._fundraiserService.GetAllFundraisersAsync();
            return this.Ok(data.Select(f => f.AsResource()).ToImmutableArray());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Fundraiser>> GetFundraiser(int id)
        {
            var fundraiser = await this._fundraiserService.GetFundraiserAsync(id);
            if (fundraiser is null)
            {
                return this.NotFound();
            }

            return this.Ok(fundraiser.AsResource());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFundraiser([FromBody] Fundraiser resource)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var id = await _fundraiserService.CreateFundraiserAsync(resource.Name, resource.Description, resource.DonationTarget, resource.Owner.AsDomainModel(), resource.DueDate);
            return this.CreatedAtRoute(nameof(GetFundraiser), new { id }, null);
        }

        [HttpPost("{id}/donate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DonateToFundraiserAsync(int id, Person donor, int donationValue)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var donorDomainModel = donor.AsDomainModel();
            await this._fundraiserService.DonateToFundraiserAsync(id, donor, donationValue);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFundraiser(int id)
        {
            var result = await this._fundraiserService.DeleteFundraiserAsync(id);
            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
