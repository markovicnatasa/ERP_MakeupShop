using AutoMapper;
using MakeupShop.Data;
using MakeupShop.Entities;
using MakeupShop.Models;
using MakeupShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeupShop.Controllers
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    [Authorize]
    public class LoginAndRegisterController : ControllerBase
    { 
        private readonly IKorisnikService korisnikService;
        private readonly MakeupShopContext context;

        public LoginAndRegisterController(IKorisnikService korisnikService, MakeupShopContext context)
        {
            this.korisnikService = korisnikService;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (login.username == "" || login.password == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Morate popuniti sva polja");
            }

            var korisnik = context.Korisnik.SingleOrDefault(k => k.username == login.username && k.password == login.password);
            var jwt = korisnikService.Authentication(korisnik);

            if(jwt == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Neispravno korisnicko ime ili lozinka, molim vas pokusajte ponovo!");
            }

            return StatusCode(StatusCodes.Status200OK, new { tokenUloga = jwt });
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public IActionResult Register([FromBody] KorisnikCreateDto korisnik)
        {
            var korisnikIzBaze = context.Korisnik.SingleOrDefault(k => k.username == korisnik.username);

            if(korisnikIzBaze != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Koirsnik sa istim korisnickim imenom vec postoji");
            }

            var noviKorisnik = new Korisnik()
            {
                uloga = "Kupac",
                imePrezime = korisnik.imePrezime,
                username = korisnik.username,
                password = korisnik.password,
                adresa = korisnik.adresa,
                kontakt = korisnik.kontakt,
                grad = korisnik.grad,
                email = korisnik.email
            };

            context.Korisnik.Add(noviKorisnik);

            if (context.SaveChanges() < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Niste uspeli da se registrujete, pokusajte ponovo");
            }

            var jwt = korisnikService.Authentication(noviKorisnik);

            if(jwt == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Neuspela registracija");
            }

            return StatusCode(StatusCodes.Status200OK, new { tokenUloga = jwt });
        }
    }
}
