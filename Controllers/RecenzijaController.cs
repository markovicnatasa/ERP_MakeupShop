using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("/recenzija")]
    [Produces("application/json")]
    [Authorize]
    public class RecenzijaController : ControllerBase
    {
        private readonly IRecenzijaRepository recenzijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public RecenzijaController(IRecenzijaRepository recenzijaRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.recenzijaRepository = recenzijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<RecenzijaDto>> GetRecenzijaList()
        {
            var recenzijas = recenzijaRepository.GetRecenzijaList();

            if (recenzijas == null || recenzijas.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<RecenzijaDto>>(recenzijas));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        [HttpGet("{recenzijaID}")]
        public ActionResult<RecenzijaDto> GetRecenzijaById(int recenzijaID)
        {
            Recenzija recenzija = recenzijaRepository.GetRecenzijaById(recenzijaID);

            if (recenzija == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Recenzija sa unetim ID-jem ne postoji");
            }
            return Ok(mapper.Map<RecenzijaDto>(recenzija));
        }

        [HttpPost]
        [Authorize(Policy = "Kupac")]
        [Produces("application/json")]
        public ActionResult<RecenzijaCreateDto> CreateRecenzija([FromBody] RecenzijaCreateDto recenzija)
        {
            try
            {

                Recenzija r = mapper.Map<Recenzija>(recenzija);
                Recenzija recenzija1 = recenzijaRepository.CreateRecenzija(r);
             
                return Ok(mapper.Map<RecenzijaCreateDto>(recenzija1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        [HttpDelete("{recenzijaID}")]
        [Authorize(Policy = "Access")]
        public IActionResult DeleteRecenzija(int recenzijaID)
        {
            try
            {
                Recenzija recenzija = recenzijaRepository.GetRecenzijaById(recenzijaID);
                if (recenzija == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Recenzija nije pronadjena");
                }

                if (recenzija.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                recenzijaRepository.DeleteRecenzija(recenzijaID);
                recenzijaRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Recenzija je obrisana!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize(Policy = "Access")]
        public ActionResult<RecenzijaUpdateDto> UpdateRecenzija(Recenzija recenzija)
        {
            try
            {
                var oldRecenzija = recenzijaRepository.GetRecenzijaById(recenzija.korisnikID);
                if (oldRecenzija == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nije pronadjena recenzija sa tim ID-jem");
                }

                if (recenzija.korisnikID != int.Parse(User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }
                Recenzija NovaRecenzija = mapper.Map<Recenzija>(recenzija);
                recenzijaRepository.SaveChanges();
                mapper.Map(NovaRecenzija, oldRecenzija);

                return Ok(mapper.Map<RecenzijaDto>(NovaRecenzija));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Postoji greska" + ex);
            }
        }
    }
}
