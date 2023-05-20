using MakeupShop.Entities;
using MakeupShop.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MakeupShop.Services
{
    public class KorisnikService : IKorisnikService
    {
        public readonly IConfiguration configuration;

        public KorisnikService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public LoginOdgovorDto Authentication(Korisnik korisnik)
        {
            if(korisnik != null)
            {
                var odgovor = GenerateJwtToken(korisnik);
                return odgovor;
            }
            return null;
        }

        private LoginOdgovorDto GenerateJwtToken(Korisnik korisnik)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();

            claims.Add(new Claim("korisnikID",korisnik.korisnikID.ToString()));

            var uloga = korisnik.uloga;
            var korisnikID = korisnik.korisnikID;

            claims.Add(new Claim(ClaimTypes.Role, uloga));

            var token = new JwtSecurityToken("MakeupShop", null, claims, DateTime.Now, DateTime.Now.AddDays(5), signingCredentials);

            LoginOdgovorDto odgovor = new LoginOdgovorDto();
            odgovor.token = tokenHandler.WriteToken(token);
            odgovor.uloga = uloga;
            odgovor.korisnikID = korisnikID;

            return odgovor;
        }

    }
}
