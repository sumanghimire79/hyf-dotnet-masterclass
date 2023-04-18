import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import { Home } from './components/Home';
import { Navigation } from './components/Navigation';

import { About } from './components/About';

import { Meals } from './components/meals/Meals';
import { MealSpecific } from './components/meals/MealSpecific';
import { AddMeal } from './components/meals/AddMeal';
import { EditMeal } from './components/meals/EditMeal';
import { DeleteMeal } from './components/meals/DeleteMeal';

import { Reservations } from './components/reservation/Reservations';
import { ReservationSpecific } from './components/reservation/ReservationSpecific';
import { AddReservation } from './components/reservation/AddReservation';
import { EditReservation } from './components/reservation/EditReservation';
import { DeleteReservation } from './components/reservation/DeleteReservation';

import { Reviews } from './components/review/Reviews';
import { ReviewSpecific } from './components/review/ReviewSpecific';
import { AddReview } from './components/review/AddReview';
import { EditReview } from './components/review/EditReview';
import { DeleteReview } from './components/review/DeleteReview';

import { Contact } from './components/Contact';
import { Footer } from './components/Footer';

import { NotFound } from './components/NotFound';
import { Login } from './components/login/Login';
import { Register } from './components/login/Register';

function App() {
  return (
    <Router>
      <div className="App">
        <Navigation />
        <Switch>
          <Route path="/login" component={Login} />
          <Route path="/register" component={Register} />
          <Route path="/about" component={About} />

          <Route exact path="/meals/:id" component={MealSpecific} />
          <Route exact path="/editMeal/:id" component={EditMeal} />
          <Route exact path="/deleteMeal/:id" component={DeleteMeal} />
          <Route exact path="/meals" component={Meals} />
          <Route exact path="/addMeal" component={AddMeal} />

          <Route exact path="/reviews/:id" component={ReviewSpecific} />
          <Route exact path="/editReview/:id" component={EditReview} />
          <Route exact path="/deleteReview/:id" component={DeleteReview} />
          <Route exact path="/reviews" component={Reviews} />
          <Route exact path="/addReview" component={AddReview} />

          <Route
            exact
            path="/reservations/:id"
            component={ReservationSpecific}
          />
          <Route exact path="/editReservaion/:id" component={EditReservation} />
          <Route
            exact
            path="/DeleteReservation/:id"
            component={DeleteReservation}
          />
          <Route exact path="/reservations" component={Reservations} />
          <Route exact path="/addReservaion" component={AddReservation} />

          <Route exact path="/contact" component={Contact} />
          <Route exact path="/" component={Home} />
          <Route path="*" component={NotFound} />
        </Switch>

        <Footer />
      </div>
    </Router>
  );
}

export default App;
