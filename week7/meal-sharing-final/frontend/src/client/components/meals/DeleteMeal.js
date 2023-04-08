import React, { useState } from 'react';
import { useParams, useHistory } from 'react-router-dom';

export const DeleteMeal = () => {
  const { id } = useParams();
  const ID = Number(id);
  const history = useHistory();

  const [deleteNotDEleteMessage, setDeleteNotDEleteMessage] = useState('');

  const handleClickdeleteMealbyID = async (ID) => {
    const deleteReservationbyID = await fetch(`/api/meals/${ID}`, {
      method: 'DELETE',
    });

    const resJson = await deleteReservationbyID.json();
    console.log(resJson);
    if (deleteReservationbyID.status === 200) {
      alert(`Meal ${ID} deleted successfully`);
      setDeleteNotDEleteMessage(`Meal ${ID} deleted successfully`);
      history.push(`/meals`);
    } else {
      setDeleteNotDEleteMessage(`Some  error occured in meal id: ${ID}`);
    }
  };

  return (
    <div>
      <h1>Delete Meal</h1>
      {deleteNotDEleteMessage}
      <button onClick={() => handleClickdeleteMealbyID(ID)}>Delete Meal</button>
    </div>
  );
};
