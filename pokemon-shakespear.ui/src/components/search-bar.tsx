import { InputHTMLAttributes } from 'react'
import styled from 'styled-components';


const StyledSearchBar = styled.input<{ error?: boolean }>`
  display: block;
  border-radius: 50px;
  border: 1px solid;
  line-height: 20px;
  font-size: 14px;
  height: 40px;
  width: 60%;
  padding: 0 10px 0 50px;
  outline: none;
  user-select: none;
  border-color: #dcdfe6;
  box-sizing: border-box;
  box-shadow: 0px 5px 15px rgb(59 59 59 / 5%);
  margin:auto;

  &:hover {
    border: 1px solid lightgrey;
  }

  &:focus {
    border: 1px solid '#409eff';
  }

  &::placeholder {
    color: lightgrey;
  }
`

export default function SearchBar({
  ...props
}: {  
} & InputHTMLAttributes<HTMLInputElement>) {
  return (
    <div style={{ position: 'relative', width: '100%', margin:'auto' }}>      
      <StyledSearchBar {...props} />
    </div>
  )
}
