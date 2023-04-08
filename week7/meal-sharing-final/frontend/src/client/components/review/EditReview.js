import React, { useEffect, useState } from 'react';
import { SubmitFormFancyCSS } from '../SubmitFormFancyCSS';
import { useParams } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
export const EditReview = () => {
  const { id } = useParams();
  console.log(id);
  const history = useHistory();

  const [reviewID, setReviewID] = useState();
  const [reviewTitle, setReviewTitle] = useState('');
  const [reviewDescription, setReviewDescription] = useState('');
  const [reviewStars, setReviewStars] = useState();
  const [reviewCreatedDate, setReviewCreatedDate] = useState('');
  const [reviewMealId, setReviewMealId] = useState();
  const [editing, setEditing] = useState(false);

  const today = new Date();
  const reviewDate =
    today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

  const reviewById = async () => {
    const data = await fetch(`/api/reviews/${id}`);
    const jsonData = await data.json();
    await jsonData.map((editReview) => {
      return (
        <div>
          {setReviewID(editReview.id)}
          {setReviewDescription(editReview.description)}
          {setReviewStars(editReview.stars)}
          {setReviewCreatedDate(editReview.created_date.split('T')[0])}
          {setReviewMealId(editReview.meal_id)}
        </div>
      );
    });
  };

  const titleforReviewToEdit = async () => {
    const data = await fetch(`/api/meals/${reviewMealId}`);
    const jsonData = await data.json();
    const mealTitles = jsonData.map((mealTitle, index) => (
      <option key={index}> {mealTitle.title} </option>
    ));
  };
  useEffect(() => {
    titleforReviewToEdit();
  }, []);

  useEffect(() => {
    reviewById();
  }, []);

  async function putReview(e) {
    e.preventDefault();
    const putReviewData = {
      title: reviewTitle,
      description: reviewDescription,
      stars: reviewStars,
      created_date: reviewDate,
      meal_id: reviewMealId,
    };
    const putReviewOptions = {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(putReviewData),
    };
    const putReviewById = await fetch(`/api/reviews/${id}`, putReviewOptions);
    const jsonData = await putReviewById.json();
    if (putReviewById.ok) {
      setEditing(false);
      alert('review is updated');
      history.push(`/reviews/${reviewMealId}`);
    }
  }

  return (
    <div>
      <h1>Edit Review</h1>

      {
        <SubmitFormFancyCSS>
          <label> Review ID </label>
          <input
            type="number"
            name="review id"
            value={reviewID}
            onChange={() => setReviewID(reviewID)}
            placeholder="review id ..."
            disabled
          />

          <label> Review Title </label>
          <input
            type="text"
            name="review title"
            value={reviewTitle}
            onChange={(e) => setReviewTitle(e.target.value)}
            placeholder="Review Title..."
          />
          <label> Review Description </label>
          <input
            type="text"
            name="review Description"
            value={reviewDescription}
            onChange={(e) => setReviewDescription(e.target.value)}
            placeholder="Review Description..."
          />
          <label> Review Stars </label>
          <input
            type="number"
            name="review stars"
            value={reviewStars}
            onChange={(e) => setReviewStars(e.target.value)}
            placeholder="Review Stars..."
          />
          <label> Review Created Date </label>
          <input
            type="date"
            name="created_date"
            value={reviewCreatedDate}
            onChange={() => setDate(reviewDate)}
            disabled
          />
          <label> Meal ID </label>
          <input
            type="number"
            name="meal id"
            value={reviewMealId}
            onChange={() => setDate(reviewMealId)}
            disabled
          />

          {!editing ? (
            <button
              type="submit"
              onClick={(e) => {
                setEditing(true);
                putReview(e);
              }}
            >
              Update
            </button>
          ) : (
            <button disabled>Updatating...</button>
          )}
          <div className="submit-form-button"></div>
        </SubmitFormFancyCSS>
      }
    </div>
  );
};
