import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders searchBox', () => {
  render(<App />);
  const searchBoxElement = screen.getByText(/Type pokemonname and press eggnter/i);
  expect(searchBoxElement).toBeInTheDocument();
});
