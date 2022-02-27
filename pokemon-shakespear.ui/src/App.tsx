import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import SearchBar from '../src/components/search-bar';
import PokemonDataCard from '../src/components/pokemon-data-card';
import loadingSymbol from '../src/images/loading.gif';

function App() {
  const baseURL = "http://localhost:2531";
  const [searchText, setSearchText] = useState<string>('')
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(true);
  const [pokemon, setPokemon] = useState();
  const [errorCode, setErrorCode] = useState<number>();

  async function translatePokemon() {
    try {
      setIsLoaded(false);
      const res = await fetch(`${baseURL}/pokemon/${searchText.toLowerCase()}`);
      setErrorCode(res.status);

      if (!res.ok) {
        const message = `An error has occured: ${res.status} - ${res.statusText}`;
        throw new Error(message);
      }
      const data = await res.json();
      setIsLoaded(true);
      setPokemon(data);
    } catch (err: any) {
      setPokemon(undefined);
      setIsLoaded(true);
      setError(err);
    }
  }

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <h3>Pokemon's Shakespear</h3>
      </header>
      <SearchBar onChange={(e) => setSearchText(e.target.value)}
        placeholder={'Type pokemonname and press enter'}
        onKeyUp={({ key }) => {
          if (key === 'Enter') {
            translatePokemon()
          }
        }}></SearchBar>

      {isLoaded == false && (
        <div className="loadingSymbol" >
          <img src={loadingSymbol} />
        </div>
      )}

      {(pokemon || error) && isLoaded && (
        <div style={{ marginBottom: 40 }}>
          <h1>Results</h1>
          {pokemon != undefined ? (
            <PokemonDataCard
              key={pokemon['id']}
              id={pokemon['id']}
              name={pokemon['name']}
              description={pokemon['translatedDescription']}
            />
          ) : (
            <div style={{ textAlign: 'center', color: 'lightgrey' }}>
              {errorCode == 404 && (<p>No Pokemon Found</p>)}
              {errorCode == 500 && (<p>Sorry something went wrong!! Please try again later</p>)}
            </div>
          )}
        </div>
      )}
    </div>



  );
}

export default App;
