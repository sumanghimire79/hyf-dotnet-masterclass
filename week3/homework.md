# Homework

## How to deliver homework 

Open this template repository  https://github.com/HackYourFuture-CPH/dotnet-masterclass and click on ![image](https://user-images.githubusercontent.com/6642037/115988976-3796da80-a5bc-11eb-9184-554a2218b2ae.png) and then create a copy of this structure on your own GitHub profile with the name ``hyf-dotnet-masterclass``

Create a PR to add your homework to the respective week folder like you are used to do in the web development course, and if you don't remember how to do hand in homework using Pull Requests, please check here https://github.com/HackYourFuture-CPH/JavaScript/blob/master/javascript1/week1/homework.md

## Homework exercises for Week #3

### 1. Make a new WebAPI project with a ConverterController
Either reuse an existing WebAPI project, or create a new.
You create a new using the command ```dotnet new webapi``` in a new and empty folder in your repository.

When you project is created, make a new controller, called "ConverterController" controller, with the base route "converter".

Look at the included "WeatherForecast" controller to see how a basic controller is created - or check the [examples](examples.md).


### 2. Make a controller action to convert Gallons to Litres
Make a controller action that takes an int Gallons and returns an int in litres. (Multiply gallons with 3.785412 to get it in liters).
The return value should be a 'double' and input should be "int liters".
Suggested signature: ```public double GallonsToLitres(int liters)```.
Test it using Swagger.


### 3. Make an action that converts Miles to kilometres
Input should be an int, in miles. Output should be an object with both the miles and the kilometers.
km=m*1.609.
Create the model to output as well, call it "Distance" and make sure it can hold both Miles and Kilometers..
Suggested signature: ```public Distance ConvertMiles(int miles)```


```csharp
//To return an object, do like this:

return new Distance(){Miles=miles, Kilometers=km};
```
When done, test it via Swagger.


### 4. (Optional) Make an action that accepts HTTP Post
Make an action that accepts an HTTP Post, with a body that is a ConversionRequest.
A ConversionRequest is a model you have do define as well, but it should contain: 
```csharp
 public double Value {get;set;}
 public ValueType TypeToConvert {get;set;}
````
where ValueType is an enum like this:

```csharp
public enum ValueType{
    Miles,
    Kilometeres,
    Gallons,
    Liters
}

```
The suggested signature of the action could be like this: ```public ConversionResponse ConvertValues([FromBody]ConversionRequest Request)```
And the ConversionResponse model should contain both the a list of the converted values like:
 ```
 [
     {
         ValueType="Miles",
         Value=1
     }, 
     {
         ValueType="Kilometers",
         Value=1.609
     }
 ]
 ```
 The method should be able to convert between kilometers and miles - and between gallons and liters - depending on the request.


### 5. (Optional) Additional conversions
Add additional conversions - pounds to kg, yards to meters, feet to cm and so on to the Action in number 4.

### 6. Codingame.com
Try to compete in a couple of "Clash of Code" events on Codingame.com. It's fine to start of with Javascript if you are most comfortable with it, but try also to challenge yourself with c#. And it doesn't matter how well you do - it's just good exercise.