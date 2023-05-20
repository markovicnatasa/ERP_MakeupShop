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
    [Route("/listaZelja")]
    public class ListaZeljaController : ControllerBase
    {
        private readonly IListaZeljaRepository listaZeljaRepository;
        private readonly IMapper mapper;

        public ListaZeljaController(IListaZeljaRepository listaZeljaRepository, IMapper mapper)
        {
            this.listaZeljaRepository = listaZeljaRepository;
            this.mapper = mapper;
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpGet]
        public ActionResult<List<ListaZeljaDto>> GetListaZeljaList()
        {
            List<ListaZelja> listaZeljas = listaZeljaRepository.GetListaZeljaList();
            if (listaZeljas == null || listaZeljas.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(mapper.Map<List<ListaZeljaDto>>(listaZeljas));
        }

        [Authorize(Policy = "Access")]
        [HttpGet("{listaZeljaID}")]
        public ActionResult<ListaZeljaDto> GetListaZeljaByID(int listaZeljaID)
        {
            
            ListaZelja listaZelja = listaZeljaRepository.GetListaZeljaById(listaZeljaID);
            if (listaZelja == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Ne postoji lista zelja sa tim ID-jem");
            }
            if (listaZelja.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
            {
                return Forbid();
            }
            return Ok(mapper.Map<ListaZeljaDto>(listaZelja));
        }

        [Authorize(Policy = "Kupac")]
        [HttpPost]
        public ActionResult<ListaZeljaCreateDto> CreateListaZelja([FromBody] ListaZeljaCreateDto listaZelja)
        {
            try
            {
                ListaZelja listaZelja1 = mapper.Map<ListaZelja>(listaZelja);
                ListaZelja listaZelja2 = listaZeljaRepository.CreateListaZelja(listaZelja1);
                listaZeljaRepository.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, mapper.Map<ListaZelja>(listaZelja2));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
        [Authorize(Policy = "Access")]
        [HttpPut]
        public ActionResult<ListaZeljaDto> UpdateListaZelja(ListaZeljaUpdateDto listaZelja)
        {
            try
            {
                ListaZelja OldListaZelja = listaZeljaRepository.GetListaZeljaById(listaZelja.listaZeljaID);
                if (OldListaZelja == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji lista zelja");
                }

                if (listaZelja.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                ListaZelja NovaOldListaZelja = mapper.Map<ListaZelja>(listaZelja);
                mapper.Map(NovaOldListaZelja, OldListaZelja);

                listaZeljaRepository.SaveChanges();

                return Ok(mapper.Map<ListaZelja>(NovaOldListaZelja));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Greska prilikom izmene liste zelja" + ex);
            }
        }
        [Authorize(Policy = "Access")]
        [HttpDelete("{listaZeljaID}")]
        public IActionResult DeleteListaZelja(int listaZeljaID)
        {
            try
            {
                ListaZelja listaZelja = listaZeljaRepository.GetListaZeljaById(listaZeljaID);
                if (listaZelja == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji lista zelja");
                }

                if (listaZelja.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                listaZeljaRepository.DeleteListaZelja(listaZeljaID);
                listaZeljaRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Lista zelja je obrisana");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}