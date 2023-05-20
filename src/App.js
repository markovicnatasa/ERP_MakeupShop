import { BrowserRouter, Routes, Route, useLocation} 
from "react-router-dom";
import { Home, Contact, Login, Register} from "./pages";
import { Header, Footer } from "./components";
import { UserProvider } from "./pages/auth/UserContext";
import DetaljiProizvoda from "./components/proizvod/DetaljiProizvoda/DetaljiProizvoda";
import Proizvod from "./components/proizvod/Proizvod";
import Cart from "./pages/cart/Cart";
import { CartProvider } from "react-use-cart";
import Checkout from "./pages/placanje/Checkout";
import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import { useEffect } from "react";

const stripePromise = loadStripe('pk_test_51N9vYyKIYfS8AH7ONi7Pc7LpCEx8azJ3WBQIKin0JdU5Pbyyg9SEqnTfAE0Z8oB0YHtsGeF8wSsyzujGb250xtzQ0063vsQKpa');


const ScrollToTop =()=>{
  const {pathname} = useLocation();

  useEffect(()=>{
    window.scrollTo(0,0);
  },[pathname])

  return null;
}

function App() {
  
  return(
    <>
    <CartProvider>
      <UserProvider>
      <BrowserRouter>
      <ScrollToTop/>
      <Header/>
        <Routes>
          <Route path="/" element={ <Home/> }/>
          <Route path="/contact"/>
          <Route path="/login" element={ <Login/> }/>
          <Route path="/register" element={ <Register/> }/>

          <Route path="#proizvod" element={<Proizvod />} />
          <Route path="/proizvod-detalji/:id" element={<DetaljiProizvoda />} />
          <Route path='/cart' element={<Cart/>}/>
          <Route path='/checkout' element={<Elements stripe={stripePromise}><Checkout/></Elements>}/>
        </Routes>
      <Footer/>
      </BrowserRouter>
      </UserProvider>
      </CartProvider>
    </>
  );

}

export default App;




