import React from 'react';
import './mealSharing.css';
import { Link } from 'react-router-dom';
export function Navigation() {
  return (
    <nav>
      <div className="logo-div">
        <img
          className="logo"
          src="https://static.vecteezy.com/system/resources/previews/000/095/259/original/restaurant-menu-vector.jpg"
          alt="logo"
        />
      </div>

      <ul>
        <Link to={'/'}>
          <li>Home</li>
        </Link>
        <Link to={'/about'}>
          <li>About</li>
        </Link>
        <Link to={'/meals'}>
          <li>Menu</li>
        </Link>

        <Link to={'/contact'}>
          <li>Contact</li>
        </Link>
        <Link to={'/login'}>
          <li>Login</li>
        </Link>
      </ul>
    </nav>
  );
}
