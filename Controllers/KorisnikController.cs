using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/korisnik")]
    [Produces("application/json")]
    [Authorize]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KorisnikController(IKorisnikRepository korisnikRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.korisnikRepository = korisnikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [Authorize(Policy = "Zaposleni")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KorisnikDto>> GetKorisnikList()
        {
            var korisniks = korisnikRepository.GetKorisnikList();

            if (korisniks == null || korisniks.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KorisnikDto>>(korisniks));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{korisnikID}")]
        public ActionResult<KorisnikDto> GetKorisnikById(int korisnikID)
        {
            Korisnik korisnik = korisnikRepository.GetKorisnikById(korisnikID);

            if (korisnik == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Korisnik sa unetim ID-jem ne postoji");
            }
            return Ok(mapper.Map<KorisnikDto>(korisnik));
        }


        [HttpDelete("{korisnikID}")]
        [Authorize(Policy = "Access")]
        public IActionResult DeleteKorisnik(int korisnikID)
        {
            try
            {
                Korisnik korisnik = korisnikRepository.GetKorisnikById(korisnikID);
                if (korisnik == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjen korisnik sa tim ID-jem");
                }

                if(korisnik.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }

                korisnikRepository.DeleteKorisnik(korisnikID);
                korisnikRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Uspesno ste obrisali svoj nalog");
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, "Greska prilikom brisanja korisnika");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize(Policy = "Access")]
        public ActionResult<KorisnikDto> UpdateKorisnik(KorisnikUpdateDto korisnik)
        {
            try
            {
                var oldKorisnik = korisnikRepository.GetKorisnikById(korisnik.korisnikID);
                if (oldKorisnik == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjen korisnik sa tim ID-jem");
                }

                if(korisnik.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                Korisnik NoviKorisnik = mapper.Map<Korisnik>(korisnik);
                NoviKorisnik.uloga = "Kupac"; 
                mapper.Map(NoviKorisnik, oldKorisnik);        

                korisnikRepository.SaveChanges();
                return Ok(mapper.Map<KorisnikDto>(NoviKorisnik));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
