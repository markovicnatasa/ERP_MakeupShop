import React from "react";
import { useCart } from "react-use-cart";
import styles from "./Cart.module.scss";
import { Link, useNavigate } from "react-router-dom";

const Cart = () => {
  const {
    isEmpty,
    totalUniqueItems,
    items,
    totalItems,
    updateItemQuantity,
    removeItem,
    emptyCart,
    cartTotal,
  } = useCart();
  const navigate = useNavigate();

  const checkout = () => {
    const logedin = localStorage.getItem("token");
    if (logedin !== null) {
      navigate("/checkout");
    } else {
      navigate("/login");
    }
  };

  return (
    <section>
      <div className={`container ${styles.table}`}>
        {isEmpty ? (
          <>
            <h1>
              Tvoja korpa je prazna
              <br />
              <Link
                to="/#proizvodi"
                style={{ textDecoration: "none", color: "#854d40" }}
              >
                &larr;Nastavi kupovinu
              </Link>
            </h1>
            <br />
          </>
        ) : (
          <div>
            <div>
              <h5>Korpa</h5>
              <table>
                <thead>
                  <tr className={styles.head}>
                    <th>Broj proizvoda</th>
                    <th>Proizvod</th>
                    <th>Tip proizvoda</th>
                    <th>Serijski broj proizvoda</th>
                    <th>Cena</th>
                    <th>Dostupnost</th>
                    <th>Kolicina na stanju</th>
                  </tr>
                </thead>
                <tbody>
                  {items.map((item, index) => {
                    return (
                      <tr key={item.proizvodID}>
                        <td>{index + 1}</td>
                        <td>
                          <img src={item.slika} style={{ height: "10rem" }} />
                        </td>
                        <td>{item.tipProizvoda}</td>
                        <td>{item.serijskiBroj}</td>
                        <td> {item.cena} dinara</td>
                        <td>{item.dostupnost ? "Dostupno" : "Nedostupno"}</td>
                        <td>{item.kolicinaNaStanju}</td>
                        <td>
                          <button
                            className="btn"
                            onClick={() =>
                              updateItemQuantity(
                                item.proizvodID,
                                item.kolicinaNaStanju - 1
                              )
                            }
                          >
                            -
                          </button>
                          <button
                            className="btn"
                            onClick={() =>
                              updateItemQuantity(
                                item.proizvodID,
                                item.kolicinaNaStanju + 1
                              )
                            }
                          >
                            +
                          </button>
                          <button
                            className="btn"
                            onClick={() => removeItem(item.proizvodID)}
                          >
                            Ukloni proizvod
                          </button>
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
              <div className={styles.summary}>
                <button className="btn" onClick={() => emptyCart()}>
                  Očisti korpu
                </button>
                <div className={styles.checkout}>
                  <div>
                    <Link
                      to="/#proizvodi"
                      style={{
                        textDecoration: "none",
                        color: "#854d40",
                        fontSize: "15px",
                      }}
                    >
                      &larr; Nastavite kupovinu!
                    </Link>
                  </div>
                  <br />
                  <div>
                    <p>
                      <b>{`Stavke u korpi: ${totalItems}`}</b>
                      <br />
                      <b>{`Jednistveni artikli u korpi: ${totalUniqueItems}`}</b>
                    </p>
                    <div className={styles.text}>
                      <h4>Ukupna cena:</h4>
                      <h3>{`${cartTotal} dinara`}</h3>
                    </div>
                    <p>Taksa i poštarina su uračunati u proveru</p>
                    <button className="btn" onClick={checkout} color="#854d40">
                      Provera
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
    </section>
  );
};

export default Cart;
