import React, { useState, useEffect } from 'react';
import '../mealSharing.css';
import { Link } from 'react-router-dom';

export const Reservations = () => {
  const [reservations, setReservations] = useState([]);

  const fetchReservations = async () => {
    const data = await fetch('/api/reservations');
    const jsonData = await data.json();
    setReservations(jsonData);
  };

  useEffect(() => {
    fetchReservations();
  }, []);

  return (
    <>
      {<h1> All {reservations.length} reservations </h1>}
      <Link exact to="/addReservaion">
        <button>Add Reservation</button>
      </Link>
      <hr></hr>
      <div className="display-container">
        {reservations.map((reservation, index) => (
          <div key={index} className="display-item">
            <Link
              exact
              to={`/reservations/${reservation.meal_id}`}
              title="click to view other reservations for this meal"
            >
              <h5> {reservation.contact_name}</h5>
              <p> {reservation.contact_email}</p>
              <p> Phone : {reservation.contact_phonenumber}</p>
              <p> Number of Guests : {reservation.number_of_guests}</p>
              <p> Event Date: {reservation.created_date.slice(0, 10)}</p>
              <p> meal ID : {reservation.meal_id}</p>
            </Link>
          </div>
        ))}
      </div>
    </>
  );
};
