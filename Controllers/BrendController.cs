using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/brend")]
    [Produces("application/json")]
    [Authorize]
    public class BrendController : ControllerBase
    {

        private readonly IBrendRepository brendRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public BrendController(IBrendRepository brendRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.brendRepository = brendRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<BrendDto>> GetBrendList()
        {
            var brends = brendRepository.GetBrendList();

            if (brends == null || brends.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<BrendDto>>(brends));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        [HttpGet("{brendID}")]
        public ActionResult<BrendDto> GetBrendById(int brendID)
        {
            Brend brend = brendRepository.GetBrendById(brendID);

            if (brend == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Brend sa unetim ID-jem ne postoji");
            }
            return Ok(mapper.Map<BrendDto>(brend));
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Policy = "Zaposleni")]
        public ActionResult<BrendCreateDto> CreateBrend([FromBody] BrendCreateDto brend)
        {
            try
            {
                Brend b = mapper.Map<Brend>(brend);
                Brend brend1 = brendRepository.CreateBrend(b);

                string location = linkGenerator.GetPathByAction("GetBrendList", "Brend", new { brendID = brend1.brendID });
                return Created(location, mapper.Map<BrendCreateDto>(brend1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        [HttpDelete("{brendID}")]
        [Authorize(Policy = "Zaposleni")]
        public IActionResult DeleteBrend(int brendID)
        {
            try
            {
                Brend brend = brendRepository.GetBrendById(brendID);
                if (brend == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Brend koji pokusavate da obrisete ne postoji");
                }

                brendRepository.DeleteBrend(brendID);
                brendRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Uspesno ste obrisali brend sa izabranim ID-jem");
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, "Greska prilikom brisanja brenda");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize(Policy = "Zaposleni")]
        public ActionResult<BrendUpdateDto> UpdateBrend(Brend brend)
        {
            try
            {
                var oldBrend = brendRepository.GetBrendById(brend.brendID);
                if (oldBrend == null)
                {
                    return NotFound();
                }
                Brend b = mapper.Map<Brend>(brend);

                mapper.Map(b, oldBrend);

                brendRepository.SaveChanges();
                return Ok(mapper.Map<BrendUpdateDto>(b));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
