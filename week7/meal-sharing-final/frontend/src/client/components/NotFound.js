import { Link } from 'react-router-dom';
import React from 'react';
export function NotFound() {
  return (
    <div className="not-found">
      <p>Sorry !</p>
      <p>This page can not be found !! </p>
      <Link to="/"> go back to home page ... </Link>
    </div>
  );
}
