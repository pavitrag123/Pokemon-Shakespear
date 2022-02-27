import styled from 'styled-components';

const StyledPokemonCard = styled.div`
  display: flex;
  background-color: white;
  width: 50%;
  margin:auto;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0px 5px 15px rgba(59, 59, 59, 0.05);
  margin-bottom: 20px;
  box-sizing: border-box;
`


export default function PokemonDataCard({
  id,
  name,
  description
}: {
  id: string
  name: string
  description: string
}) {
  return (
    <StyledPokemonCard>
      <div>
        <img
          src={`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${id}.png`}
          height={80}
          style={{ marginRight: 10,minWidth:80 }}
        />
      </div>
      <div style={{ width: '100%' }}>
        <div style={{ fontWeight: 'bold', color: '#403E3D', marginBottom: 10 }}        >
          <span style={{ marginRight: 5 }}>{name}</span>
        </div>
        <div style={{ color: 'darkgrey', marginBottom: 20 }}>{description}</div>
      </div>
    </StyledPokemonCard>
  )
}
