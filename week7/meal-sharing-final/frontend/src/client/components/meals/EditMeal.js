import React, { useEffect, useState } from 'react';
import { SubmitFormFancyCSS } from '../SubmitFormFancyCSS';
import { useParams, useHistory } from 'react-router-dom';

export const EditMeal = () => {
  const { id } = useParams();
  const ID = Number(id);
  const history = useHistory();

  const [mealId, setMealId] = useState();
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [location, setLocation] = useState('');
  const [when, setWhen] = useState('');
  const [maxReservation, setMaxReservation] = useState();
  const [price, setPrice] = useState();
  const [createdDate, setCreatedDate] = useState('');
  const [editing, setEditing] = useState(false);

  const today = new Date();
  const editedDate =
    today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

  const fetchMealsById = async () => {
    const data = await fetch(`api/meals/${ID}`);
    const jsonData = await data.json();
    const result = jsonData.map((editMeal) => {
      return (
        <div>
          {setMealId(editMeal.id)}
          {setTitle(editMeal.title)}
          {setDescription(editMeal.description)}
          {setLocation(editMeal.location)}
          {setWhen(editMeal.when.slice(0, 10))}
          {setMaxReservation(editMeal.max_reservations)}
          {setCreatedDate(editMeal.created_date.slice(0, 10))}
          {setPrice(editMeal.price)}
        </div>
      );
    });
  };
  useEffect(() => {
    fetchMealsById();
  }, []);

  const putMeal = async (e) => {
    e.preventDefault();
    const putMealData = {
      title: title,
      description: description,
      location: location,
      when: when,
      max_reservations: Number(maxReservation),
      price: Number(price),
      created_date: createdDate,
    };
    const putMealOptions = {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(putMealData),
    };
    const putReviewById = await fetch(`api/meals/${ID}`, putMealOptions);
    const jsonData = await putReviewById.json();

    if (putReviewById.ok) {
      setEditing(true);
      alert('meal is updated');
      history.push(`/meals/${mealId}`);
    }
  };

  return (
    <div>
      <h1>Edit Meal</h1>

      {
        <SubmitFormFancyCSS>
          <label> Meal ID </label>
          <input
            type="number"
            name="mealId"
            value={mealId}
            onChange={(e) => setMealId(e.target.value)}
            placeholder="Meal id..."
            disabled
          />
          <label> Meal title </label>
          <input
            type="text"
            name="title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Meal title..."
          />
          <label> Meal Description</label>
          <input
            type="text"
            name="description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Meal description..."
          />
          <label> Meal Location </label>
          <input
            type="text"
            name="location"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
            placeholder="location..."
          />
          <label> Meal available Date /when? </label>
          <input
            type="datetime"
            name="when"
            value={when}
            onChange={(e) => setWhen(e.target.value)}
          />
          <label> Reservation Capacity </label>
          <input
            type="number"
            name="max_reservation"
            value={maxReservation}
            onChange={(e) => setMaxReservation(e.target.value)}
            placeholder="max reservation..."
          />
          <label> Meal Price </label>
          <input
            type="number"
            name="price"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            placeholder=" set meal price..."
          />
          <label> Meal crated date </label>
          <input
            type="datetime"
            name="createdDate"
            value={createdDate}
            onChange={(e) => setCreatedDate(e.target.value)}
          />
          <div className="submit-form">
            {!editing ? (
              <button
                type="submit"
                onClick={(e) => {
                  setEditing(true);
                  putMeal(e);
                }}
              >
                Update meal
              </button>
            ) : (
              <button disabled>Updatating...</button>
            )}
          </div>
        </SubmitFormFancyCSS>
      }
    </div>
  );
};
