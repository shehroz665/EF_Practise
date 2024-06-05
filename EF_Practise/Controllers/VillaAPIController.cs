using EF_Practise.Data;
using EF_Practise.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_Practise.Controllers
{
    [Route("api/db/")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db= db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  ActionResult<IEnumerable<Villa>> GetVillas()
        {
            var villa =  _db.Villas.ToList();
            return Ok(villa);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(item => item.Id == id);
            if (villa == null)
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
            var villa = _db.Villas.FirstOrDefault(item => item.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
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
                var villaList = await _db.Villas.ToListAsync();
                return Ok(villaList);
            }
            search = search.ToLower();
            var villa = await _db.Villas
                .Where(item => item.Name.ToLower().Contains(search)
                || item.Ocupancy.ToString().ToLower().Contains(search)
                || item.Sqft.ToString().ToLower().Contains(search)
                ).ToListAsync();
              ;
            if (villa == null || villa.Count== 0)
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
            var villaExist = await _db.Villas.FirstOrDefaultAsync((item) => item.Name.ToLower() == villa.Name.ToLower());
            if (villaExist != null)
            {
                ModelState.AddModelError("Custom Error", "Villa must be unique");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest();
            }
            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _db.Villas.AddAsync(villa);
            await _db.SaveChangesAsync();
            return Ok(villa);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Villa>> UpdateVilla(Villa villa)
        {
            if (villa.Id == 0)
            {
                return BadRequest();
            }
            var villaObj = _db.Villas.FirstOrDefault((item) => item.Id == villa.Id);
            if (villaObj == null)
            {
                return NotFound();
            }
            villaObj.Name = villa.Name;
            villaObj.Ocupancy = villa.Ocupancy;
            villaObj.Sqft = villa.Sqft;
            _db.Villas.Update(villaObj);
            await _db.SaveChangesAsync();
            return Ok(villaObj);
        }

    }
}
