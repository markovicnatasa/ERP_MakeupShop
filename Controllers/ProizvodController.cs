using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/proizvod")]
    [Produces("application/json")]
    [Authorize]
    public class ProizvodController : ControllerBase
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProizvodController(IProizvodRepository proizvodRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.proizvodRepository = proizvodRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ProizvodDto>> GetProizvodList()
        {
            var proizvods = proizvodRepository.GetProizvodList();

            if (proizvods == null || proizvods.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ProizvodDto>>(proizvods));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        [HttpGet("{proizvodID}")]
        public ActionResult<ProizvodDto> GetProizvodById(int proizvodID)
        {
            Proizvod proizvod = proizvodRepository.GetProizvodById(proizvodID);

            if (proizvod == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Proizvod sa unetim ID-jem ne postoji" );
            }
            return Ok(mapper.Map<ProizvodDto>(proizvod));
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Policy = "Zaposleni")]
        public ActionResult<ProizvodCreateDto> CreateProizvod([FromBody] ProizvodCreateDto proizvod)
        {
            try
            {

                Proizvod p = mapper.Map<Proizvod>(proizvod);
                Proizvod proizvod1 = proizvodRepository.CreateProizvod(p);

                string location = linkGenerator.GetPathByAction("GetProizvodList", "Proizvod", new { proizvodID = proizvod1.proizvodID });
                return Created(location, mapper.Map<ProizvodCreateDto>(proizvod1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        [HttpDelete("{proizvodID}")]
        [Authorize(Policy = "Zaposleni")]
        public IActionResult DeleteProizvod(int proizvodID)
        {
            try
            {
                Proizvod proizvod = proizvodRepository.GetProizvodById(proizvodID);
                if (proizvod == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Proizvod koji pokusavate da obrisete ne postoji");
                }

                proizvodRepository.DeleteProizvod(proizvodID);
                proizvodRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Proizvod je obrisan!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, "Proizvod koji pokusavate da obrisete ne postoji");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize (Policy = "Zaposleni")]
        public ActionResult<ProizvodUpdateDto> UpdateProizvod(Proizvod proizvod)
        {
            try
            {
                var oldProizvod = proizvodRepository.GetProizvodById(proizvod.proizvodID);
                if (oldProizvod == null)
                {
                    return NotFound();
                }
                Proizvod p = mapper.Map<Proizvod>(proizvod);

                mapper.Map(p, oldProizvod);

                proizvodRepository.SaveChanges();
                return Ok(mapper.Map<ProizvodUpdateDto>(p));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
