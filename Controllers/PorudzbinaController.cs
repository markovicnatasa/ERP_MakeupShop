using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/porudzbina")]
    [Produces("application/json")]
    [Authorize]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PorudzbinaController(IPorudzbinaRepository porudzbinaRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "Zaposleni")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PorudzbinaDto>> GetPorudzbinaList()
        {
            var porudzbinas = porudzbinaRepository.GetPorudzbinaList();

            if (porudzbinas == null || porudzbinas.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PorudzbinaDto>>(porudzbinas));
        }
        
        [HttpGet]
        [Authorize(Policy = "Access")]
        [Route("/poruzbinas/porudzbina")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<PorudzbinaDto> GetPorudzbina()
        {
            var korisnikIDs = HttpContext.User.FindFirst("korisnikID").Value;
            if (!int.TryParse(korisnikIDs, out int korisnikID))
            {
                return BadRequest("Nevazeci ID korisnika.");

            }
            List<Porudzbina> porudzbinas = porudzbinaRepository.GetAllPorudzbinaList(korisnikID);
            if (porudzbinas == null || porudzbinas.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(porudzbinas);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{porudzbinaID}")]
        public ActionResult<PorudzbinaDto> GetPorudzbinaById(int porudzbinaID)
        {
            Porudzbina porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaID);

            if (porudzbina == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Porudzbina sa unetim ID-jem ne postoji");
            }
            return Ok(mapper.Map<PorudzbinaDto>(porudzbina));
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Policy = "Kupac")]
        public ActionResult<PorudzbinaCreateDto> CreatePorudzbina([FromBody] PorudzbinaCreateDto porudzbina)
        {
            try
            {

                Porudzbina p = mapper.Map<Porudzbina>(porudzbina);
                Porudzbina porudzbina1 = porudzbinaRepository.CreatePorudzbina(p);

                string location = linkGenerator.GetPathByAction("GetPorudzbinaList", "Porudzbina", new { porudzbinaID = porudzbina1.porudzbinaID });
                return Created(location, mapper.Map<PorudzbinaCreateDto>(porudzbina1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex.Message);
            }
        }

        [Authorize(Policy = "Access")]
        [HttpDelete("{porudzbinaID}")]
        public IActionResult DeletePorudzbina(int porudzbinaID)
        {
            try
            {
                Porudzbina porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaID);
                if (porudzbina == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Porudzbina nije pronadjena");
                }

                if (porudzbina.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                porudzbinaRepository.DeletePorudzbina(porudzbinaID);
                porudzbinaRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Porudzbina je obrisana!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize(Policy = "Access")]
        public ActionResult<PorudzbinaDto> UpdatePorudzbina(PorudzbinaUpdateDto porudzbina)
        {
            try
            {
                var oldPorudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbina.korisnikID);
                if (oldPorudzbina == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjena porudzbina sa tim ID-jem");
                }

                if (porudzbina.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                Porudzbina NovaPorudzbina = mapper.Map<Porudzbina>(porudzbina);
                porudzbinaRepository.SaveChanges();
                mapper.Map(NovaPorudzbina, oldPorudzbina);

                return Ok(mapper.Map<PorudzbinaDto>(NovaPorudzbina));
                
                
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
