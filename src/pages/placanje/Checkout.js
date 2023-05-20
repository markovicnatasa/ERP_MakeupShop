import React, { useState } from "react";
import { CardElement, useStripe, useElements } from "@stripe/react-stripe-js";
import { useCart } from "react-use-cart";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { variables } from "../../ApiRoutes";
import "./Checkout.module.scss";
import loading from "../../assets/loading.gif";

const Checkout = () => {
  const { cartTotal, emptyCart, items} = useCart();
  const ukupnaCena = cartTotal;
  const stripe = useStripe();
  const elements = useElements();
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return;
    }
    setIsLoading(true);
    const cardElement = elements.getElement(CardElement);

    const { error, paymentMethod } = await stripe.createPaymentMethod({
      type: "card",
      card: cardElement,
    });
    const data = {
      kolicina: ukupnaCena,
      paymentMethodId: paymentMethod.placanjeID,
    };
    axios.post(variables.ApiRoutes_PLACANJE, data).then((result) => {
      if (result.status === 200) {
        alert("Plaćanje je uspešno!");
        const porudzbinaData = {
          ukupnaCena: cartTotal,
          korisnikID: localStorage.getItem("korisnikID"),
          //TotalProizvodi: totalItems,
        };
        const token = localStorage.getItem("token");
        axios
          .post(variables.ApiRoutes_PORUDZBINA, porudzbinaData, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          })
          .then((result) => {
            if (result.status === 201) {
              const stavkePorudzbine = items.map((item) => {
                return {
                  proizvodID: item.proizvodID,
                  porudzbinaID: result.data.porudzbinaID,
                  Kolicina: item.kolicinaProizvoda,
                };
              });
              axios
                .post(variables.ApiRoutes_STAVKA_PORUDZBINE, stavkePorudzbine, {
                  headers: {
                    Authorization: `Bearer ${token}`,
                  },
                })
                .then((result) => {
                  console.log("Uspesno ste izvrsili porudzbinu");
                });
            }
          });
        emptyCart();
        setIsLoading(false);
        setTimeout(() => navigate("/"), 5000);
      } else {
        alert(
          "Ups! Nešto nije u redu. Proverite svoje informacije i pokušajte ponovo!"
        );
      }
    });
  };

  return (
    <form>
      <div>
        <label htmlFor="card-element">Detalji kartice</label>
        <p>Molim Vas unesite broj kartice da biste nastavili placanje:</p>
        <CardElement
          id="card-element"
          options={{ style: { base: { fontSize: "20px" } } }}
        />
      </div>
      <button onClick={handleSubmit} disabled={!stripe}>
        {isLoading ? (
          <img src={loading} alt="Loading" />
        ) : (
          "Nastavite s plaćanjem"
        )}
      </button>
    </form>
  );
};

export default Checkout;
