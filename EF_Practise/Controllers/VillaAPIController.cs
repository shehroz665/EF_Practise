using EF_Practise.Data;
using EF_Practise.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> GetVilla(int id)
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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> DeleteVilla(int id)
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
            search=search.ToLower();
            var villa= VillaStore.villas
                .Where(item=> item.Name.ToLower().Contains(search) 
                || item.Ocupancy.ToString().ToLower().Contains(search)
                || item.Sqft.ToString().ToLower().Contains(search)
                )
                .ToList();
            if (villa == null || villa.Count==0)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Villa>> CreateVilla(Villa villa)
        {
            if(VillaStore.villas.FirstOrDefault((item) => item.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                 ModelState.AddModelError("Custom Error", "Villa must be unique");
                return BadRequest(ModelState);
            }
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

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> UpdateVilla(Villa villa)
        {
            if(villa.Id == 0)
            {
                return BadRequest();
            }
            var villaObj = VillaStore.villas.FirstOrDefault((item) => item.Id==villa.Id); 
            if(villaObj==null){
                return NotFound();
            }
            villaObj.Name=villa.Name;
            villaObj.Ocupancy=villa.Ocupancy;
            villaObj.Sqft=villa.Sqft;
            return Ok(villaObj);
        }

        [HttpPatch("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> UpdatePatchVilla(int id,JsonPatchDocument<Villa> villa)
        {
            if (id == 0 || villa==null)
            {
                return BadRequest();
            }
            var villaObj = VillaStore.villas.FirstOrDefault((item) => item.Id == id);
            if (villaObj == null)
            {
                return NotFound();
            }
            villa.ApplyTo(villaObj,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        // id=2
        //        [
        //          {
        //             "path": "/sqft",
        //              "op": "replace",
        //              "value": "105"
        //           },
        //           {
        //              "path": "/ocupancy",
        //              "op": "replace",
        //              "value": "199"
        //              },
        //         ]

    }
}