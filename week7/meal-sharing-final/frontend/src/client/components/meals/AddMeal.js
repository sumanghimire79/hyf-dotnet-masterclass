import React, { useState } from 'react';

import { SubmitFormFancyCSS } from '../SubmitFormFancyCSS';

export const AddMeal = () => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [location, setLocation] = useState('');
  const [when, setWhen] = useState('');
  const [maxReservation, setMaxReservation] = useState();
  const [price, setPrice] = useState();
  const [message, setMessage] = useState('');
  const [isDone, setIsDone] = useState(false);

  const today = new Date();
  const date =
    today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

  let handleSubmitMeal = async (e) => {
    e.preventDefault();
    const mealPost = {
      title: title,
      description: description,
      location: location,
      when: when,
      max_reservations: Number(maxReservation),
      price: Number(price),
      created_date: date,
    };

    try {
      setIsDone(true);
      let res = await fetch('api/meals', {
        method: 'POST',
        headers: { 'Content-Type': 'Application/json' },
        body: JSON.stringify(mealPost),
      });

      if (res.status === 201) {
        setMessage(`Meal ${title} added successfully`);
        setIsDone(false);
      } else {
        setMessage('Some error occured');
      }
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <>
      <h1>Add a meal</h1>
      {
        <form onSubmit={handleSubmitMeal}>
          <SubmitFormFancyCSS>
            <label>Meal Title</label>
            <input
              type="text"
              name="title"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              placeholder="Meal title..."
            />
            <label>Meal Description</label>
            <input
              type="text"
              name="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Meal description..."
            />
            <label>Meal Branch Name</label>
            <input
              type="text"
              name="location"
              value={location}
              onChange={(e) => setLocation(e.target.value)}
              placeholder="location..."
            />
            <label>Meal Available Date </label>
            <input
              type="date"
              name="when"
              value={when}
              onChange={(e) => setWhen(e.target.value)}
            />
            <label>Max Reservation Capacity</label>
            <input
              type="number"
              name="max_reservation"
              value={maxReservation}
              onChange={(e) => setMaxReservation(e.target.value)}
              placeholder="max reservation..."
            />
            <label>Meal Price</label>
            <input
              type="number"
              name="price"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              placeholder=" set meal price..."
            />

            <div className="submit-form">
              {!isDone && <button type="submit">Add Meal</button>}
              {isDone && (
                <button type="submit" disabled>
                  Adding Meal...
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
