import {React, useEffect, useContext} from 'react';
import styles from "./auth.module.scss"
import loginImg from "../../assets/login.png"
import { Link } from 'react-router-dom';
import { variables } from '../../ApiRoutes';
import axios from 'axios';
import { useState } from "react";
import { UserContext } from './UserContext';


const Login = () => {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [lozinka, setLozinka] = useState('');
  const { setIsLoggedIn } = useContext(UserContext);

  useEffect(() => {
    const korisnikToken = localStorage.getItem("korisnikToken");
    if (korisnikToken!==null) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const handleKorisnickoImeChange = (value) => {
    setKorisnickoIme(value);
  };
  const handleLozinkaChange = (value) => {
    setLozinka(value);
  };
  const handleUlogujSe = () => {
    const korisnikData = {
      username : korisnickoIme,
      password : lozinka,
    };

    axios
        .post(variables.ApiRoutes_LOGIN,korisnikData)
        .then((result) =>{
            if(result.data);
            alert('Uspesno ste ulogovani!')
            localStorage.setItem("korisnikToken", result.data.tokenUloga.token);
            const ulogaKorisnika = result.data.tokenUloga.uloga;
            localStorage.setItem("ulogaKorisnika", ulogaKorisnika);
            const korisnikID = result.data.tokenUloga.korisnikID;
            localStorage.setItem("korisnikID", korisnikID);
            if (ulogaKorisnika === "Zaposleni") {
                console.log("Zaposleni");
            }else if (ulogaKorisnika === "Kupac") {
            console.log("Korisnik je kupac");
            }
    }).catch((error) => {
        if (error.response.status === 400) {
            alert('Molimo unesite sva polja.');
          } 
        else if (error.response.status === 404) {
            alert('Neispravno korisničko ime ili lozinka.');
        }
        else {
            alert('Došlo je do greške prilikom prijave.');
          }
    });
    setIsLoggedIn(true);
};

  return ( <section className={`container ${styles.auth}`}>
        <div className={styles.img}>
         <img src={loginImg} alt="Login" width="400"/>
         </div>
           
         <div className={styles.form}> 
            <h2>Uloguj se</h2>
            <form>
                <input type="text" id="txtkorisnickoIme" placeholder="Korisnicko ime" onChange={(e) => handleKorisnickoImeChange(e.target.value)}
                required/>
                <input type="password" id="txtLozinka" placeholder="Lozinka" onChange={(e) => handleLozinkaChange(e.target.value)}
                required/>
                <button className="--btn --btn-primary
                --btn-block" onClick={()=> handleUlogujSe()}>Uloguj se</button>
            </form>
                <span className={styles.register}>
                    <p>Nemate nalog?</p>
                    <Link to="/register" style = {{color:"#EEC591"}}>Registrujte se</Link>
                </span>
         </div>
        </section>
  )
};

export default Login;