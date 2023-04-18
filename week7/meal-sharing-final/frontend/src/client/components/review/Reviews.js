import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Stars from 'react-stars-display';

export const Reviews = () => {
  const [reviews, setReviews] = useState([]);

  const fetchItem = async () => {
    const data = await fetch('/api/reviews');
    const jsonData = await data.json();
    console.log(jsonData);
    setReviews(jsonData);
  };
  useEffect(() => {
    fetchItem();
  }, []);

  return (
    <div>
      <h1> All {reviews.length} Reviews</h1>
      <>
        <Link exact to={'/AddReview'}>
          {<button>Add Review</button>}
        </Link>
      </>
      <hr></hr>
      <section className="display-container">
        {reviews.map((review, index) => {
          return (
            <>
              <section className="display-item" key={index}>
                <Link
                  exact
                  to={`/reviews/${review.meal_id}`}
                  title="click to view the meal"
                >
                  <h1>
                    {review.id}
                    <span>. </span>
                    {review.title}
                  </h1>
                  <p>{review.description} </p>
                  <Stars stars={review.stars} size={30} />
                  <p> Meal: {review.meal_id} </p>
                  <p> Review Date: {review.created_date.slice(0, 10)} </p>
                </Link>
              </section>
            </>
          );
        })}
      </section>
    </div>
  );
};
