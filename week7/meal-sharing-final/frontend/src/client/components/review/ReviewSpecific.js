import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import '../mealSharing.css';

export const ReviewSpecific = ({ match }) => {
  const id = Number(match.params.id);

  const [reviews, setReviews] = useState([]);

  const fetchItem = async () => {
    const data = await fetch('/api/reviews');
    const jsonData = await data.json();
    setReviews(jsonData);
  };
  useEffect(() => {
    fetchItem();
  }, []);

  const reviewSpecific = reviews.filter((review) => review.meal_id === id);

  return (
    <div className="specificReturnDiv">
      <h1>Review Details</h1>
      <>
        <i>
          <b>This meal has got {reviewSpecific.length} reviews</b>
        </i>
      </>
      {reviewSpecific.length === 0 ? (
        <>
          <Link exact to={'/addReview'}>
            {<button> Add Review for this meal</button>}
          </Link>
          <Link exact to={'/reviews'}>
            {<button> View All Reviews</button>}
          </Link>
        </>
      ) : (
        <>
          <Link exact to={'/reviews'} title="view all reviews">
            {<button> View All Reviews</button>}
          </Link>
          <Link exact to={'/addReview'}>
            {<button> Add View </button>}
          </Link>
          <section className="display-container">
            {reviewSpecific.map((review, index) => (
              <section key={index} className="display-item">
                <Link
                  exact
                  to={`/meals/${review.meal_id}`}
                  title="click to view the specific meal for this review "
                >
                  <h3>Review: {review.title}</h3>
                  <p>Ratings: {review.stars}</p>
                  <p>{review.description}</p>
                  <p> Review Date: {review.created_date.slice(0, 10)}</p>
                </Link>
                <div className="detailspecificPagebuttonSpan">
                  <Link
                    exact
                    to={`/editReview/${review.id}`}
                    title="click to edit this review "
                  >
                    <button>Edit</button>
                  </Link>
                  <Link
                    exact
                    to={`/deleteReview/${review.id}`}
                    title="click to delete this review "
                  >
                    <button>Delete </button>
                  </Link>
                </div>
              </section>
            ))}
          </section>
        </>
      )}
    </div>
  );
};
