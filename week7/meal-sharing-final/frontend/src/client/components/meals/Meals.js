import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import '../mealSharing.css';

export const Meals = () => {
  const [meals, setMeals] = useState([]);
  const [data, setData] = useState([]);
  const [serchText, setSerchText] = useState();
  const [correctData, setCorrectData] = useState([]);
  const [isData, setIsData] = useState(false);

  const fetchItem = async () => {
    const data = await fetch('/api/meals');
    const jsonData = await data.json();
    console.log(jsonData);
    setMeals(jsonData);
  };

  useEffect(() => {
    fetchItem();
  }, []);

  const showData = correctData.map((meal) => {
    return (
      <section className="display-item" key={meal.id}>
        <Link exact to={`/meals/${meal.id}`} title="View Meal Details">
          <h1 key={meal.id}>{meal.title}</h1>
          <span> Price :{meal.price}</span>
        </Link>
      </section>
    );
  });

  const excludeSearch = [
    'id',
    'when',
    'created_date',
    'price',
    'max_reservations',
    'location',
  ];

  const filterData = (value) => {
    const lowerCaseValue = value.toLowerCase().trim();
    const filteredData = meals.filter((meal) => {
      return Object.keys(meal).some((key) => {
        return excludeSearch.includes(key)
          ? false
          : meal[key].toString().toLowerCase().includes(lowerCaseValue);
      });
    });

    setIsData(filteredData.length === 0 ? true : false);
    setData(filteredData);
  };
  const handleChange = (value) => {
    setSerchText(value);
    filterData(value);
  };

  useEffect(() => {
    setCorrectData(data.length === 0 ? meals : data);
  }, [handleChange]);

  return (
    <div>
      <div className="reserv_review">
        <Link exact to={'/reservations'}>
          {<button> Reservations</button>}
        </Link>
        <Link exact to={'/reviews'}>
          {<button> Reviews</button>}
        </Link>
      </div>

      <h3> All available {correctData.length} meals</h3>
      <Link exact to={'/addMeal'}>
        {<button> Add Meal</button>}
      </Link>
      <hr></hr>
      <form onSubmit={handleChange}>
        <input
          value={serchText}
          type="text"
          onChange={(e) => {
            handleChange(e.target.value);
          }}
          placeholder="search meal ..."
        />
      </form>

      <section className="display-container">
        {isData === true ? <span>No data found</span> : showData}
      </section>
    </div>
  );
};
