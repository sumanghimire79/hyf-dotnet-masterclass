fetch = require("node-fetch-commonjs");

const fetchPromise = fetch("https://reqres.in/api/users");

fetchPromise
  .then((response) => response.json())
  .then((data) => {
    const users = data.data;
    return users;
  })
  .then((users) => {
    users
      .slice(0, 3)
      .map((user) => user.first_name)
      .forEach((firstName) => console.log(firstName));
  });
