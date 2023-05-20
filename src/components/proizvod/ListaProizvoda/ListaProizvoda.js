import React, { useState } from "react";
import styles from "./ListaProizvoda.module.scss";
import { BsFillGridFill } from "react-icons/bs";
import { FaListAlt } from "react-icons/fa";
import Search from "../../search/Search";
import StavkaProizvoda from "../StavkaProizvoda/StavkaProizvoda";
import Pagination from "../../pagination/Pagination";

const ListaProizvoda = ({ proizvodi }) => {
  const [grid, setGrid] = useState(true);
  const [search, setSearch] = useState("");
  const [sort, setSort] = useState("latest");

  const [currentPage, setCurrentPage] = useState(1);
  const [proizvodiPerPage] = useState(9);
  const indexOfLastProizvod = currentPage * proizvodiPerPage;
  const indexOfFirstProizvod = indexOfLastProizvod - proizvodiPerPage;
  const currentProizvodi = proizvodi.slice(
    indexOfFirstProizvod,
    indexOfLastProizvod
  );
  return (
    <div className={styles["product-list"]} id="proizvod">
      <div className={styles.top}>
        <div className={styles.icons}>
          <BsFillGridFill
            size={22}
            color="#854d40"
            onClick={() => setGrid(true)}
          />

          <FaListAlt
            size={24}
            color="#854d40"
            onClick={() => setGrid(false)}
          />
        </div>
        <div>
          <Search value={search} onChange={(e) => setSearch(e.target.value)} />
        </div>
        <div className={styles.sort}>
          <label>Sortiraj po:</label>
          <select value={sort} onChange={(e) => setSort(e.target.value)}>
            <option value="najnovije">Najnovije</option>
            <option value="najeftinije">Najeftinije</option>
            <option value="najskuplje">Najskuplje</option>
            <option value="a-z">A - Z</option>
            <option value="z-a">Z - A</option>
          </select>
        </div>
      </div>
      <div className={grid ? `${styles.grid}` : `${styles.list}`}>
        {proizvodi === 0 ? (
          <p>Proizvod nije pronadjen</p>
        ) : (
          <>
            {currentProizvodi.map((proizvod) => {
              return (
                <div key={proizvod.proizvodID}>
                  <StavkaProizvoda {...proizvod} grid={grid} proizvod={proizvod}/>
                </div>
              );
            })}
          </>
        )}
        <Pagination
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
          proizvodiPerPage={proizvodiPerPage}
          totalProizvodi={proizvodi.length}
        />
      </div>
    </div>
  );
};

export default ListaProizvoda;
