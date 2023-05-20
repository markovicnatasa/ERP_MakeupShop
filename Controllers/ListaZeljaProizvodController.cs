using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/listaZeljaProizvod")]
    public class ListaZeljaProizvodController : ControllerBase
    {
        private readonly IListaZeljaProizvodRepository listaZeljaProizvodRepository;
        private readonly IListaZeljaRepository listaZeljaRepository;
        private readonly IMapper mapper;

        public ListaZeljaProizvodController(IListaZeljaProizvodRepository listaZeljaProizvodRepository, IListaZeljaRepository listaZeljaRepository, IMapper mapper)
        {
            this.listaZeljaProizvodRepository = listaZeljaProizvodRepository;
            this.listaZeljaRepository = listaZeljaRepository;
            this.mapper = mapper;
        }

        [Authorize(Policy = "Access")]
        [HttpGet]
        public ActionResult<List<ListaZeljaProizvodDto>> GetAllListaZeljaProizvod()
        {
            int trenutniKorisnikID = int.Parse(User.FindFirst("korisnikID").Value);
            if (trenutniKorisnikID == null)
            {
                return Forbid();
            }
            if (User.IsInRole("Zaposleni"))
            {
                return Forbid();
            }

            List<ListaZeljaProizvod> listaZeljaProizvods = listaZeljaProizvodRepository.GetAllListaZeljaProizvod(trenutniKorisnikID);
            if (listaZeljaProizvods == null || listaZeljaProizvods.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Ne postoji lista zelja sa datim ID-jem");
            }
            return Ok(mapper.Map<List<ListaZeljaProizvodDto>>(listaZeljaProizvods));
        }

        [Authorize(Policy = "Access")]
        [HttpGet("{listaProID}")]
        public ActionResult<ListaZeljaProizvodDto> GetListaZeljaProizvodByID(int listaProID)
        {
            int trenutniKorisnikID = int.Parse(User.FindFirst("korisnikID").Value);
            if (trenutniKorisnikID == null)
            {
                return Forbid();
            }
            ListaZeljaProizvod listaZeljaProizvod = listaZeljaProizvodRepository.GetListaZeljaProizvodById(listaProID);
            ListaZelja listaZelja = listaZeljaRepository.GetListaZeljaById(listaZeljaProizvod.listaZeljaID);
            if (listaZeljaProizvod == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Ne postoji stavka liste zelja sa datim ID-jem");
            }
            if (listaZelja.korisnikID == trenutniKorisnikID)
            {
                return Ok(mapper.Map<ListaZeljaProizvodDto>(listaZeljaProizvod));

            }
            return Forbid();
        }
        [Authorize(Policy = "Kupac")]
        [HttpPost]
        public ActionResult<ListaZeljaProizvodCreateDto> CreateListaZeljaProizvod([FromBody] ListaZeljaProizvodCreateDto listaZeljaProizvod)
        {
            try
            {
                ListaZeljaProizvod listaZeljaProizvod1 = mapper.Map<ListaZeljaProizvod>(listaZeljaProizvod);
                ListaZelja listaZelja = listaZeljaRepository.GetListaZeljaById(listaZeljaProizvod1.listaZeljaID);
                if (listaZelja == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji stavka liste zelja sa datim ID-jem");
                }
                if (listaZelja.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                ListaZeljaProizvod listaZeljaProizvod2 = listaZeljaProizvodRepository.CreateListaZeljaProizvod(listaZeljaProizvod1);
                listaZeljaProizvodRepository.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, mapper.Map<ListaZeljaProizvod>(listaZeljaProizvod2));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
        [Authorize(Policy = "Access")]
        [HttpPut]
        public ActionResult<ListaZeljaProizvodDto> UpdateListaZeljaProizvod(ListaZeljaProizvodUpdateDto listaZeljaProizvod)
        {
            try
            {
                ListaZeljaProizvod OldListaZeljaProizvod = listaZeljaProizvodRepository.GetListaZeljaProizvodById(listaZeljaProizvod.listaProID);
                if (OldListaZeljaProizvod == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Stavka liste zelja ne postoji");
                }
                ListaZelja listaZelja1 = listaZeljaRepository.GetListaZeljaById(listaZeljaProizvod.listaZeljaID);
                if(listaZelja1 == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji stavka liste zelja sa datim ID-jem");
                }

                if (listaZelja1.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                ListaZeljaProizvod NovaListaZeljaProizvod = mapper.Map<ListaZeljaProizvod>(listaZeljaProizvod);
                mapper.Map(NovaListaZeljaProizvod, OldListaZeljaProizvod);

                listaZeljaProizvodRepository.SaveChanges();

                return Ok(mapper.Map<ListaZeljaProizvod>(NovaListaZeljaProizvod));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Greska prilikom izmene stavke liste zelja" + ex);
            }
        }
        [Authorize(Policy = "Access")]
        [HttpDelete("{listaProID}")]
        public IActionResult DeleteListaZeljaProizvod(int listaProID)
        {
            try
            {
                ListaZeljaProizvod listaZeljaProizvod = listaZeljaProizvodRepository.GetListaZeljaProizvodById(listaProID);
                if (listaZeljaProizvod == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjena stavka liste zelja sa tim ID-jem");
                }
                ListaZelja listaZelja = listaZeljaRepository.GetListaZeljaById(listaZeljaProizvod.listaZeljaID);
                if (listaZelja == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji stavka liste zelja sa datim ID-jem");
                }

                if (listaZelja.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                listaZeljaProizvodRepository.DeleteListaZeljaProizvod(listaProID);
                listaZeljaProizvodRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Stavka liste zelja je obrisana");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}