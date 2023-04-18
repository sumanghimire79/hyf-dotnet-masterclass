import React, { useEffect, useState } from 'react';

import { SubmitFormFancyCSS } from '../SubmitFormFancyCSS';

export const AddReview = () => {
  const [meals, setMeals] = useState([]);
  const [title, setTitle] = useState();
  const [description, setDescription] = useState('');
  const [stars, setStars] = useState('');

  const [mealId, setMealId] = useState('');

  const [message, setMessage] = useState('');
  const [isDone, setIsDone] = useState(false);
  console.log(typeof mealId);

  const today = new Date();
  const date =
    today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

  const fetchReview = async () => {
    const data = await fetch('api/meals');
    const jsonData = await data.json();
    setMeals(jsonData);
  };

  useEffect(() => {
    fetchReview();
  }, []);

  let handleSubmit = async (e) => {
    e.preventDefault();
    const reviewPost = {
      title: title,
      description: description,
      stars: Number(stars),
      created_date: date,
      meal_id: Number(mealId),
    };

    try {
      setIsDone(true);
      let res = await fetch('/api/reviews', {
        method: 'POST',
        headers: { 'Content-Type': 'Application/json' },
        body: JSON.stringify(reviewPost),
      });

      let resJson = await res.json();
      console.log(resJson);
      if (res.status === 201) {
        setMessage('Review added successfully');
        setIsDone(false);
      } else {
        setMessage('Some error occured');
      }
    } catch (err) {
      console.log(err);
    }
  };

  const mealTitle = meals.map((meal, index) => (
    <option key={index} value={meal.id} defaultValue={'select meal'}>
      {meal.title}
    </option>
  ));
  console.log(mealId);
  return (
    <>
      <h1>Add Review</h1>
      {
        <form onSubmit={handleSubmit}>
          <SubmitFormFancyCSS>
            <label>Review Title</label>
            <input
              type="text"
              name="title"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              placeholder="Review title..."
            />
            <label>Review Description</label>
            <input
              type="text"
              name="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Review description..."
            />
            <label>Review Stars</label>
            <input
              type="number"
              name="stars"
              value={stars}
              onChange={(e) => setStars(e.target.value)}
              placeholder="Review stars..."
            />
            <label>Meal id/Title</label>
            <select
              type="number"
              name="meal_id"
              value={Number(mealId)}
              onChange={(e) => setMealId(e.target.value)}
              placeholder="meal id/title..."
            >
              {mealTitle}
            </select>

            <div className="submit-form">
              {!isDone && <button type="submit">Add Review</button>}
              {isDone && (
                <button type="submit" disabled>
                  Adding Review...
                </button>
              )}
            </div>
            <div className="message">{message ? <p>{message}</p> : null}</div>
          </SubmitFormFancyCSS>
        </form>
      }
    </>
  );
};
