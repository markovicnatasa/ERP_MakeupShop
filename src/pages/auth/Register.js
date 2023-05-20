import styles from "./auth.module.scss";
import registerImg from "../../assets/register.png"
import { Link } from 'react-router-dom';
import { useState } from "react";
import axios from "axios";
import { variables } from "../../ApiRoutes";

const Register = () => {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [imeiPrezime, setImeiPrezime] = useState('');
  const [adresa, setAdresa] = useState('');
  const [kontakt, setKontakt ] = useState('');
  const [grad, setGrad] = useState('');
  const [email,setEmail ] = useState('');
  

  const handleKorisnickoImeChange = (value) => {
    setKorisnickoIme(value);
  };
  const handleLozinkaChange = (value) => {
    setLozinka(value);
  };
  const handleImeiPrezimeChange = (value) => {
    setImeiPrezime(value);
  };
  const handleAdresaChange = (value) => {
    setAdresa(value);
  };
  const handleKontaktChange = (value) => {
    setKontakt(value);
  };
  const handleGradChange = (value) => {
    setGrad(value);
  };
  const handleEmailChange = (value) => {
    setEmail(value);
  };
  const handleRegistrujSe = () => {
    const data = {
      imePrezime : imeiPrezime,
      adresa : adresa,
      username : korisnickoIme,
      password : lozinka,
      kontakt : kontakt,
      grad : grad,
      email : email
    };
  
    axios.post(variables.ApiRoutes_REGISTER,data).then((result) =>{
      if(result.data);
      alert('Uspesno ste registrovani!')
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
  }
  return (
    <section className={`container ${styles.auth}`}>
         <div className={styles.form}> 
            <h2>Registruj se</h2>
            <form>
                <input type="text" placeholder="Korisnicko ime" onChange={(e) => handleKorisnickoImeChange(e.target.value)}
                required/>
                <input type="password" placeholder="Lozinka" onChange={(e) => handleLozinkaChange(e.target.value)}
                required/>
                <input type="text" placeholder="Ime i Prezime" onChange={(e) => handleImeiPrezimeChange(e.target.value)}
                required/>
                <input type="text" placeholder="Adresa" onChange={(e) => handleAdresaChange(e.target.value)}
                required/>
                <input type="text" placeholder="Kontakt" onChange={(e) => handleKontaktChange(e.target.value)}
                required/>
                <input type="text" placeholder="Grad" onChange={(e) => handleGradChange(e.target.value)}
                required/>
                <input type="text" placeholder="Email" onChange={(e) => handleEmailChange(e.target.value)}
                required/>
                <button className="--btn --btn-primary
                --btn-block" onClick={()=> handleRegistrujSe()}>Registruj se</button>
            </form>
                <span className={styles.register}>
                    <p>Imate nalog?</p>
                    <Link to="/login" style = {{color:"#EEC591"}}>Ulogujte se</Link>
                </span>
         </div>
        <div className={styles.img}>
          <img src={registerImg} alt="Register" width="400"/>
        </div>
      </section>
  );
};

export default Register;