using BOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;

namespace PRN231PE_FA23_665511_taipdse172357_API_Odata.Controllers
{
    public class SilverJewelriesController : ODataController
    {
        private readonly IJewelryRepo _jewelryRepo;

        public SilverJewelriesController(IJewelryRepo jewelryRepo)
        {
            _jewelryRepo = jewelryRepo;
        }

        [EnableQuery]
        [Authorize(Policy = "AdminOrStaff")]
        [HttpGet("/odata/SilverJewelries")]
        public async Task<ActionResult<IEnumerable<SilverJewelry>>> GetAll()
        {
            try
            {
                return Ok(await _jewelryRepo.GetJewelries());
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOrStaff")]
        [HttpGet("/api/Category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCatergory()
        {
            try
            {
                return Ok(await _jewelryRepo.GetCategories());
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }

        }

        [Authorize(Policy = "AdminOrStaff")]
        [HttpGet("/api/Jewelry/{id}")]
        public async Task<ActionResult<SilverJewelry>> Details(string id)
        {
            try
            {
                return Ok(await _jewelryRepo.GetJewelry(id));
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/api/Jewelry")]
        public async Task<ActionResult<SilverJewelry>> Create([FromBody] SilverJewelry silverJewelry)
        {
            try
            {
                return Ok(await _jewelryRepo.AddSilverAsync(silverJewelry));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/api/Jewelry/{id}")]
        public async Task<ActionResult<SilverJewelry>> Edit(string id, [FromBody] SilverJewelry silverJewelry)
        {

            try
            {
                return Ok(await _jewelryRepo.UpdateSilverAsync(silverJewelry));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/api/Jewelry/{id}")]
        public async Task<ActionResult<SilverJewelry>> Delete(string id)
        {
            try
            {
                return Ok(await _jewelryRepo.DeleteJewelry(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
