import React, { useState } from 'react';
import { useParams, useHistory } from 'react-router-dom';

export const DeleteReview = () => {
  const { id } = useParams();
  const ID = Number(id);
  const history = useHistory();
  const [deleteNotDEleteMessage, setDeleteNotDEleteMessage] = useState('');

  async function handleClickDeleteReview() {
    const deletebyID = await fetch(`/api/reviews/${ID}`, {
      method: 'DELETE',
    });

    if (deletebyID.status === 200) {
      alert(`Review ${ID} deleted successfully`);
      setDeleteNotDEleteMessage(`Review ${ID} deleted successfully`);
      history.push(`/reviews`);
    } else if (deletebyID.status === 404) {
      setDeleteNotDEleteMessage(`Id: ${ID} not found`);
    } else {
      setDeleteNotDEleteMessage(`Some  error occured in Review id: ${ID}`);
    }
  }

  return (
    <div>
      <h1>Delete Review</h1>
      {deleteNotDEleteMessage}
      <button onClick={() => handleClickDeleteReview(ID)}>Delete Review</button>
    </div>
  );
};
