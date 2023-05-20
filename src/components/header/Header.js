import { useState, useContext,useEffect } from 'react';
import { Link, NavLink } from "react-router-dom";
import styles from "./Header.module.scss";
import { AiOutlineShoppingCart } from "react-icons/ai";
import { FaTimes } from "react-icons/fa";
import { TfiMenuAlt } from "react-icons/tfi";
import { UserContext } from '../../pages/auth/UserContext';

const activeLink = ({ isActive }) => 
(isActive ? `${styles.active}` : "")

const logo = (
  <div className={styles.logo}>
    <Link to="/" style = {{color:"transparent"}}>
      <h1>
        Make<span>MeGorgeous</span>
      </h1>
    </Link>
  </div>
);
const cart = (
  <span className={styles.cart}>
    <NavLink to="/cart" className={activeLink}>
      Korpa
      <AiOutlineShoppingCart size={20} />
      <p>0</p>
    </NavLink>
  </span>
);

const Header = () => {
  const[showMenu,setShowMenu] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  const { isLoggedIn, setIsLoggedIn } = useContext(UserContext);

  useEffect(()=>{
    const ulogaKorisnika = localStorage.getItem("ulogaKorisnika");
    if(ulogaKorisnika==="Zaposleni"  || ulogaKorisnika==="zaposleni"){
      setIsAdmin(true);
    } else{
      setIsAdmin(false);
    }
  });
  useEffect(() => {
    const korisnikToken = localStorage.getItem("korisnikToken");
    if (korisnikToken) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);
  const toggleMenu = () => {
    setShowMenu(!showMenu);
  };

  const hideMenu = () => {
    setShowMenu(false);
  };

  const handleLogout = () => {
    localStorage.removeItem("korisnikToken");
    localStorage.removeItem("ulogaKorisnika");
    setIsLoggedIn(false);
  };

  return (
    <header>
      <div className={styles.header}>
        {logo}
        <nav 
          className={
            showMenu ? `${styles["show-nav"]}`
            : `${styles["hide-nav"]}`
          }
        >

          <div
           className={
              showMenu ? `${styles
              ["nav-wrapper"]} ${styles["show-nav-wrapper"]}`
              : `${styles["nav-wrapper"]}`
            }
            onClick={hideMenu}
          ></div>
          <ul onClick={hideMenu}>
            <li className={styles["logo-mobile"]}>
              {logo}
              <FaTimes size={22} color="#fff" onClick={hideMenu} />
            </li>
            <li>
              <NavLink to="/" className={activeLink}>
                Pocetna
              </NavLink>
            </li>
            <li>
              <NavLink to="/contact" className={activeLink}>
                Kontakt
              </NavLink>
            </li>

          </ul>
          <div className={styles["header-right"]} onClick={hideMenu}> 
          <span className={styles.links}>
            {isLoggedIn ? (
                <>
                 {!isAdmin && <NavLink
                    to="/order-history"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Moje porudzine
                  </NavLink>}
                  {isAdmin && <NavLink
                    to="/admin"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Admin
                  </NavLink>}
                  <NavLink to="/" onClick={handleLogout}>
                    Izloguj se
                  </NavLink>
                 
                </>
              ) : (
                <>
                 <NavLink
                    to="/login"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Uloguj se
                  </NavLink>
                 <NavLink
                    to="/register"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Registruj se
                  </NavLink>
                </>
              )}
            </span>
            {cart}
          </div>
        </nav>

        <div className={styles["menu-icon"]}>
            <TfiMenuAlt size={28} onClick={toggleMenu}/>
        </div>
      </div>
    </header>
  );
};

export default Header;