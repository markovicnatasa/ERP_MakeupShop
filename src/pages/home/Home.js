import React, { useEffect } from 'react'
import Slider from '../../components/slider/Slider';
import Proizvod from '../../components/proizvod/Proizvod';


const Home = () => {
  const url = window.location.href;

  useEffect(()=>{
    const scrollToProizvodi=()=>{
      if(url.includes("#proizvod")){
        window.scrollTo({
          top:700,
          behavior: "smooth"
        });
        return;
      }
    };
    scrollToProizvodi();
  },[url]);


  return (
    <div>
      <Slider/>
      <Proizvod />
    </div>
  )
}

export default Home

//