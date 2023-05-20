using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/placanje")]
    [Produces("application/json")]
    [Authorize]
    public class PlacanjeController : ControllerBase
    {

        private readonly IPlacanjeRepository placanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PlacanjeController(IPlacanjeRepository placanjeRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.placanjeRepository = placanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [Authorize(Policy = "Zaposleni")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PlacanjeDto>> GetPlacanjeList()
        {
            var placanjes = placanjeRepository.GetPlacanjeList();

            if (placanjes == null || placanjes.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PlacanjeDto>>(placanjes));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{placanjeID}")]
        public ActionResult<PlacanjeDto> GetPlacanjeById(int placanjeID)
        {
            Placanje placanje = placanjeRepository.GetPlacanjeById(placanjeID);

            if (placanje == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Placanje sa unetim ID-jem ne postoji");
            }
            return Ok(mapper.Map<PlacanjeDto>(placanje));
        }

        /*[HttpGet]
        [Authorize(Policy = "Access")]
        [Route("/api/placanjes/placanje")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<PlacanjeDto> GetPlacanje()
        {
            var korisnikIDs = HttpContext.User.FindFirst("korisnikID").Value;
            if (!int.TryParse(korisnikIDs, out int korisnikID))
            {
                return BadRequest("Nevazeci ID korisnika.");

            }
            List<Placanje> placanjes =  placanjeRepository.GetAllPlacanjeList(korisnikID);
            if ( placanjes == null ||  placanjes.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjenobplacanje sa tim ID-jem");
            }
            return Ok( placanjes);
        }*/

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Policy = "Kupac")]
        public ActionResult<PlacanjeCreateDto> CreatePlacanje([FromBody] PlacanjeCreateDto placanje)
        {
            try
            {

                Placanje p = mapper.Map<Placanje>(placanje);
                Placanje placanje1 = placanjeRepository.CreatePlacanje(p);

                return Ok(mapper.Map<PlacanjeCreateDto>(placanje1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        [HttpDelete("{placanjeID}")]
        [Authorize(Policy = "Zaposleni")]
        public IActionResult DeletePlacanje(int placanjeID)
        {
            try
            {
                Placanje placanje = placanjeRepository.GetPlacanjeById(placanjeID);
                if (placanje == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Placanje koje pokusavate da obrisete ne postoji");
                }

                placanjeRepository.DeletePlacanje(placanjeID);
                placanjeRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Uspesno ste obrisali placanje");
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, "Greska prilikom brisanja placanja");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize(Policy = "Zaposleni")]
        public ActionResult<PlacanjeUpdateDto> UpdatePlacanje(Placanje placanje)
        {
            try
            {
                var oldPlacanje = placanjeRepository.GetPlacanjeById(placanje.placanjeID);
                if (oldPlacanje == null)
                {
                    return NotFound();
                }
                Placanje p = mapper.Map<Placanje>(placanje);

                mapper.Map(p, oldPlacanje);

                placanjeRepository.SaveChanges();
                return Ok(mapper.Map<PlacanjeUpdateDto>(p));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
