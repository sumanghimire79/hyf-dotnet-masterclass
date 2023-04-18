import React, { useState } from 'react';
import { useParams, useHistory } from 'react-router-dom';

export const DeleteReservation = () => {
  const { id } = useParams();
  const ID = Number(id);
  const history = useHistory();

  const [deleteNotDEleteMessage, setDeleteNotDEleteMessage] = useState('');

  const handleClickdeleteReservationbyID = async (ID) => {
    const deleteReservationbyID = await fetch(`/api/reservations/${ID}`, {
      method: 'DELETE',
    });

    if (deleteReservationbyID.status === 200) {
      alert(`Reservation ${ID} deleted successfully`);
      setDeleteNotDEleteMessage(`Reservation ${ID} deleted successfully`);
      history.push(`/reservations`);
    } else {
      setDeleteNotDEleteMessage(`Some  error occured in reservation id: ${ID}`);
    }
  };

  return (
    <div>
      <h1>Delete Reservation</h1>
      {deleteNotDEleteMessage}
      <button onClick={() => handleClickdeleteReservationbyID(ID)}>
        Delete Review
      </button>
    </div>
  );
};
