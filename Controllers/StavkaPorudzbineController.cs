using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/stavkaPorudzbine")]
    [Produces("application/json")]
    [Authorize]
    public class StavkaPorudzbineController : ControllerBase
    {
        private readonly IStavkaPorudzbineRepository stavkaPorudzbineRepository;
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public StavkaPorudzbineController(IStavkaPorudzbineRepository stavkaPorudzbineRepository, IPorudzbinaRepository porudzbinaRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.stavkaPorudzbineRepository = stavkaPorudzbineRepository;
            this.porudzbinaRepository = porudzbinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "Access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StavkaPorudzbineDto>> GetStavkaPorudzbineList()
        {
            int trenutniKorisnikID = int.Parse(User.FindFirst("korisnikID").Value);
            if (trenutniKorisnikID == null)
            {
                return Forbid();
            }
            if(User.IsInRole("Zaposleni"))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Zaposleni nema pristup");
            }
            List<StavkaPorudzbine> stavkaPorudzbines = stavkaPorudzbineRepository.GetAllStavkaPorudzbine(trenutniKorisnikID);
            if (stavkaPorudzbines == null || stavkaPorudzbines.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Stavka porudzbine ne postoji");
            }
            return Ok(mapper.Map<List<StavkaPorudzbineDto>>(stavkaPorudzbines));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{stavkaPorudzbineID}")]
        public ActionResult<StavkaPorudzbineDto> GetStavkaPorudzbineById(int stavkaPorudzbineID)
        {
            StavkaPorudzbine stavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbineID);
            if (stavkaPorudzbine == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Stavka porudzbine sa unetim ID-jem ne postoji");
            }
            return Ok(mapper.Map<StavkaPorudzbineDto>(stavkaPorudzbine));
        }

        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        public ActionResult<StavkaPorudzbineCreateDto> CreateStavkaPorudzbine([FromBody] StavkaPorudzbineCreateDto stavkaPorudzbine)
        {
            try
            {

                StavkaPorudzbine s = mapper.Map<StavkaPorudzbine>(stavkaPorudzbine);
                StavkaPorudzbine stavkaPorudzbine1 = stavkaPorudzbineRepository.CreateStavkaPorudzbine(s);
                stavkaPorudzbineRepository.SaveChanges();

                return Ok(mapper.Map<StavkaPorudzbineCreateDto>(stavkaPorudzbine1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        
        [Authorize(Roles = "Access")]
        [HttpDelete("{stavkaPorudzbineID}")]
        public IActionResult DeleteStavkaPorudzbine(int stavkaPorudzbineID)
        {
            try
            {
                StavkaPorudzbine stavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbineID);
                
                if (stavkaPorudzbine == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Stavku porudzbine koju pokusavate da obrisete ne postoji");
                }
                Porudzbina porudzbina1 = porudzbinaRepository.GetPorudzbinaById(stavkaPorudzbine.porudzbinaID);
                if (porudzbina1 == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji stavka poruzbine sa datim ID-jem");
                }
                if (porudzbina1.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }

                stavkaPorudzbineRepository.DeleteStavkaPorudzbine(stavkaPorudzbineID);
                stavkaPorudzbineRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Stavka porudzbine je obrisana");
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Access")]
        [Produces("application/json")]
        public ActionResult<StavkaPorudzbineUpdateDto> UpdateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine)
        {
            try
            {
                var oldStavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbine.stavkaPorudzbineID);
                if (oldStavkaPorudzbine == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Stavku porudzbine koju pokusavate da izmenite ne postoji");
                }
                Porudzbina porudzbina1 = porudzbinaRepository.GetPorudzbinaById(stavkaPorudzbine.porudzbinaID);
                if (porudzbina1.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                StavkaPorudzbine s = mapper.Map<StavkaPorudzbine>(stavkaPorudzbine);

                mapper.Map(s, oldStavkaPorudzbine);

                stavkaPorudzbineRepository.SaveChanges();
                return Ok(mapper.Map<StavkaPorudzbineUpdateDto>(s));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
