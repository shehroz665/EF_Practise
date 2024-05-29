using EF_Practise.Data;
using EF_Practise.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Practise.Controllers
{
    [Route("api/villaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<Villa> GetVillas()
        {
            
            return VillaStore.villas;
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> GetVilla([FromQuery] int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villas.FirstOrDefault(item => item.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> DeleteVilla([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villas.FirstOrDefault(item => item.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villas.Remove(villa);
            return Ok(villa);
        }

        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<Villa>>> SearchVilla([FromQuery] string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Ok(VillaStore.villas.ToList());  
            }
            var villa= VillaStore.villas
                .Where(item=> item.Name.Contains(search,StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (villa == null || villa.Count==0)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Villa>> CreateVilla(Villa villa)
        {
            if(villa== null)
            {
                return BadRequest();
            }
            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villa.Id=VillaStore.villas.OrderByDescending(item => item.Id).FirstOrDefault().Id+1;
            VillaStore.villas.Add(villa);
            return Ok(villa);
        }
    }
}