# Homework

## How to deliver homework 

Open this template repository  https://github.com/HackYourFuture-CPH/dotnet-masterclass and click on ![image](https://user-images.githubusercontent.com/6642037/115988976-3796da80-a5bc-11eb-9184-554a2218b2ae.png) and then create a copy of this structure on your own GitHub profile with the name ``hyf-dotnet-masterclass``

Create a PR to add your homework to the respective week folder like you are used to do in the web development course, and if you don't remember how to do hand in homework using Pull Requests, please check here https://github.com/HackYourFuture-CPH/JavaScript/blob/master/javascript1/week1/homework.md

## Homework exercises for Week #4
We will now start to create the foundation for the Meal sharing app.
A lot of the steps we will take are very similar to what we did in class with the CarRepository example, so I recommend looking at the [examples](examples.md).

### 1. Start the new project
Following the steps in [Week 3's homework (1)](../week3/homework.md), start a new webapi project (from template) in a new folder called something like "MealsharingNET".

### 2. Create models
In the models folder, create the model classes for a meal and a reservation.
They should at least have the following properties, but feel free to add more you feel are relevant:

*Meal*
- ID
- Title
- Description
- Cost
- ImageUrl
- Location
- MaxReservations

*Reservation*
- ID
- MealID
- Name
- Email
- Date
- No of persons

### 3. Create interface describing repositories
Define the interfaces to 2 repositories (suggested names could be "IMealRepository" and "IReservationRepository") providing the necessary functionality to work with the data.
These are the functionality that as a minimum are needed (but feel free to add more):
- Add a meal
- List meals
- Return a specific meal (by ID)
- Create a new reservation
- List all reservations
- List reservations for a meal


### 4. Create simple in-memory repositories to test it
Create an *InMemoryMealRepository* class and *InMemoryReservationRepository* class.
Ensure that both repository classes implement the respective interface.
In each of the repositories you can create a private List<Meal> (or List<Reservation>) to hold the objects in memory.
Look at how it's done in "MyCarRepository" in the examples and use that approach.

It's a good idea to create a couple of meals and reservations hardcoded to test with.


### 5. Register the services in program.cs
In the program.cs, below the line ```builder.Services.AddControllers();````add registrations like this: 
```builder.Services.AddSingleton<IMealRepository,InMemoryMealRepository>();``` for both services.

### 6. Create Controllers
Create a MealController and a ReservationController.
For both, inject the needed service in the constructor, and then create methods with proper routing that calls the repository for functionality.
Again - much like in the Car example

### 7. Test controllers in Swagger.
Finally, run your project and go to /swagger to test out the functionality in swagger.