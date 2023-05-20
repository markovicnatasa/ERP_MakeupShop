import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { variables } from "../../../ApiRoutes";
import loading from "../../../assets/loading.gif";
import styles from "./DetaljiProizvoda.module.scss";
//import { Container } from "react-bootstrap";

const DetaljiProizvoda = () => {
  const { proizvodID } = useParams();
  const [proizvod, setProizvod] = useState(null);
  const [brend, setBrend] = useState(null);

  useEffect(() => {
    const fetchProizvodDetails = async () => {
      try {
        const response = await axios.get(
          variables.ApiRoutes_PROIZVOD + `${proizvodID}`
        );
        setProizvod(response.data);
      } catch (error) {
        console.log(error);
      }
    };
    fetchProizvodDetails();
  }, [proizvodID]);
  useEffect(() => {
    const fetchBrendDetails = async () => {
      if (proizvod !== null) {
        try {
          const brendResponse = await axios.get(
            variables.ApiRoutes_BREND + `/${proizvod.brendID}`
          );
          setBrend(brendResponse.data);
        } catch (error) {
          console.log(error);
        }
      }
    };

    fetchBrendDetails();
  }, [proizvod]);

  /*useEffect(() => {
    const fetchBrend = async () => {
      if (proizvod !== null) {
        try {
          const brendResponse = await axios.get(
            variables.ApiRoutes_BREND + `${proizvod.brendID}`
          );
          if(brendResponse.data!==null){
            setBrend(brendResponse.data);
          }
          else{
            console.log(brendResponse.data);
          }
        } catch (error) {
          console.log(error);
        }
      }
    };

    fetchBrend();
  }, [proizvod]);*/

  return (
    <div className={`container ${styles.product}`}>
      <h2>Detalji proizvoda</h2>
      <div>
        <Link
          to="/#proizvodi"
          color="#854d40"
          style={{ textDecoration: "none", fontSize: "12px" }}
        >
          &larr; Nazad na proizvode
        </Link>
      </div>
      {proizvod === null ? (
        <img src={loading} alt="Loading" style={{ width: "50px" }} />
      ) : (
        <section>
          <div className={styles.details}>
            <div className={styles.img}>
              <img src={proizvod.slika} alt={proizvod.imeProizvoda} />
            </div>
            <div className={styles.content}>
              <h3>{proizvod.imeProizvoda}</h3>
              {brend && (
                <h2>
                  <b>Brend:</b> {brend.imeBrenda}
                </h2>
              )}
              <p className={styles.price}>
                <b>Cena: </b>
                {`${proizvod.cena} dinara`}
              </p>
              <p>{proizvod.imeProizoda}</p>
              <p>
                <b>Tip proizvoda</b> {proizvod.tipProizvoda}
              </p>
              <p>
                <b>Serijski broj</b> {proizvod.serijskiBroj}
              </p>
              <p>
                <b>Dostupnost</b> {proizvod.dostupnost}
              </p>
              <p>
                <b>Kolicina na stanju</b> {proizvod.kolicinaNaStanju}
              </p>
              <button
                className="btn"
                //className="--btn --btn-danger"
                //onClick={handleAddToCart}
              >
                Dodaj u korpu
              </button>
            </div>
          </div>
        </section>
      )}
    </div>
  );
};
export default DetaljiProizvoda;


             /* <p>
                <b>Brendovi: </b>
                {brend !== null &&
                  brend.length > 0 &&
                  brend.map((brend) => (
                    <span key={brend.brendID} className={styles.category}>
                      {brend.imeBrenda}&nbsp;
                    </span>
                  ))}
              </p>*/