import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import '../mealSharing.css';

export const ReservationSpecific = ({ match }) => {
  const id = Number(match.params.id);
  const [reservations, setReservations] = useState([]);

  const fetchReservations = async () => {
    const data = await fetch('/api/reservations');
    const jsonData = await data.json();
    setReservations(jsonData);
  };

  useEffect(() => {
    fetchReservations();
  }, []);

  const reservationSpecific = reservations.filter(
    (reservation) => reservation.meal_id === id,
  );

  return (
    <div className="specificReturnDiv">
      <h1>Reservation Details</h1>
      <>
        <i>
          <b>This meal has got {reservationSpecific.length} reservations</b>
        </i>
      </>
      {reservationSpecific.length === 0 ? (
        <div className="specificReturnDiv">
          <Link exact to={'/addReservaion'}>
            {<button> Add Reservation</button>}
          </Link>
          <Link exact to={'/reservations'}>
            {<button> View All Reservations</button>}
          </Link>
        </div>
      ) : (
        <section className="reservationSpeceficSection">
          <div className="specificReturnDiv">
            <Link exact to={'/addReservaion'}>
              {<button> Add Reservation</button>}
            </Link>
            <Link exact to={'/reservations'}>
              {<button> View All Reservations</button>}
            </Link>
          </div>
          <section className="display-container">
            {reservationSpecific.map((reservation, index) => (
              <section key={index} className="display-item">
                <Link
                  exact
                  to={`/meals/${reservation.meal_id}`}
                  title="click to view the specific meal for this reservation "
                >
                  <h3>Number of Guests: {reservation.number_of_guests}</h3>
                  <p>Created Date: {reservation.created_date.slice(0, 10)}</p>
                  <p>Phone: {Number(reservation.contact_phonenumber)}</p>
                  <p> Contact Name: {reservation.contact_name}</p>
                  <p> Contact Email: {reservation.contact_email}</p>
                  <p> Meal ID: {reservation.meal_id}</p>
                </Link>
                <div className="detailspecificPagebuttonSpan">
                  <Link
                    exact
                    to={`/editReservaion/${reservation.id}`}
                    title="click to edit this reservation "
                  >
                    <button> Edit</button>
                  </Link>
                  <Link
                    exact
                    to={`/deleteReservation/${reservation.id}`}
                    title="click to edit this reservation "
                  >
                    <button> Delete</button>
                  </Link>
                </div>
              </section>
            ))}
          </section>
        </section>
      )}
    </div>
  );
};
