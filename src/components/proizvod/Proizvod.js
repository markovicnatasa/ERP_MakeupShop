import React, { useEffect, useState } from 'react'
import styles from "./Proizvod.module.scss"
import ListaProizvoda from "./ListaProizvoda/ListaProizvoda";
import {variables} from "../../ApiRoutes";
import axios from 'axios';
import { FaCogs } from "react-icons/fa";
//import loading from "../../assets/loading.gif";

const Proizvod = () => {
  const [data, setData] = useState([]);
  const [brend, setBrend] = useState([]);
  const [selectedBrend, setSelectedBrend] = useState(null);
  const [proizvodi, setProizvodi] = useState([]);

  useEffect(()=>{
    async function fetchBrend(){
      try{
        const br = await axios.get(variables.ApiRoutes_BREND);
        setBrend(br.data);
      }catch(error){
        console.log(error);
      }
    };
    fetchBrend();
  }, []);

  useEffect(() => {
    const fetchProizvodByBrend = async () => {
      try {
        const response = await axios.get(variables.ApiRoutes_BREND+`${selectedBrend}`);
        setProizvodi(response.data);
      } catch (error) {
        console.error('Greška pri preuzimanju proizvoda po brendu:', error);
      }
    };

    if (selectedBrend) {
      fetchProizvodByBrend();
    }
  }, [selectedBrend]);

  const handleBrendSelect = (imeBrenda) => {
    const lowercaseBrend = imeBrenda.toLowerCase();
    setSelectedBrend(lowercaseBrend);
  };
  useEffect(()=>{
    async function fetchData(){
     try{ const response = await axios.get(variables.ApiRoutes_PROIZVOD);
      setData(response.data)
      ;}catch(error){
        console.error(error);
      }
    }
    fetchData();
  },[]);

  return (
    
    <section>
      <div className={`container ${styles.product}`}>
      <aside>
            <ul>
            {brend.map((bre)=>(
              <li key={bre.brendID} onClick={()=>handleBrendSelect(bre.imeBrenda)} style={{cursor:"pointer"}}>{bre.imeBrenda}</li>
            ))}
          </ul>
        
        </aside>
        <div className={styles.content}>
            <img
             // src={spinnerImg}
             // alt="Loading.."
             // style={{ width: "50px" }}
              //className="--center-all"
            />{selectedBrend ? (<ListaProizvoda proizvodi={proizvodi}/>):( <ListaProizvoda proizvodi={data} />)}
          
          <div className={styles.icon}>
            <FaCogs size={20} color="#854d40" />
           
            <p>
            </p>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Proizvod;