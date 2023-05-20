//import React, { useContext, useState } from "react";
import { Link } from "react-router-dom";
import Card from "../../card/Card";
import styles from "./StavkaProizvoda.module.scss";
import { useCart } from "react-use-cart";

const StavkaProizvoda = ({
  proizvod,
  grid,
  proizvodID,
  cena,
  imeProizvoda,
  tipProizvoda,
  brendID,
  dostupnost,
  kolicinaNaStanju,
  slika,
}) => {
  const { addItem } = useCart();
  const shortenText = (text, n) => {
    if (text.length > n) {
      const shortenedText = text.substring(0, n).concat("...");
      return shortenedText;
    }
    return text;
  };

  return (
    <Card cardClass={grid ? `${styles.grid}` : `${styles.list}`}>
      <Link to={`/proizvod-detalji/${proizvodID}`}>
        <div className={styles.img}>
          <img src={slika} alt={imeProizvoda} />
        </div>
      </Link>
      <div className={styles.content}>
        <div className={styles.details}>
          <p>{`${cena} dinara`}</p>
          <h4>{shortenText(imeProizvoda, 20)}</h4>
        </div>
        {!grid && (
          <p className={styles.desc}>{shortenText(tipProizvoda, 30)}</p>
        )}

        <button
          className="btn"
          onClick={() =>
            addItem({ id: proizvodID, price: cena, ...proizvod })
          }
        >
          Dodaj u korpu
        </button>
      </div>
    </Card>
  );
};

export default StavkaProizvoda;
