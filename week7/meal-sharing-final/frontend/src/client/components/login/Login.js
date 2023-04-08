import React, { useState, useEffect } from 'react';
import { SubmitFormFancyCSS } from '../SubmitFormFancyCSS';
import { Link, useHistory } from 'react-router-dom';

export const Login = () => {
  const history = useHistory();
  // React States

  const [errorMessages, setErrorMessages] = useState({});
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [userFetched, setUserFetched] = useState([]);
  const [user, setUSer] = useState({ email: '', password: '' });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUSer({ ...user, [name]: value });
  };
  const errors = {
    email: 'invalid email',
    password: 'invalid password',
  };
  // Generate JSX code for error message
  const renderErrorMessage = (name) =>
    name === errorMessages.name && (
      <div className="error">{errorMessages.message}</div>
    );

  const fetchItem = async () => {
    const data = await fetch('/api/login');
    const jsonData = await data.json();
    console.log(jsonData);
    setUserFetched(jsonData);
  };

  useEffect(() => {
    fetchItem();
  }, []);

  const handleLogin = (e) => {
    //Prevent page reload
    e.preventDefault();

    // Find user login info
    const userData = userFetched.find(
      (userd) => userd.user_email === user.email,
    );

    console.log(userData);
    // Compare user info
    if (userData) {
      if (userData.user_password !== user.password) {
        // Invalid password
        setErrorMessages({ name: 'password', message: errors.password });
      } else {
        setIsSubmitted(true);
      }
    } else {
      // Username not found
      setErrorMessages({ name: 'email', message: errors.email });
    }
  };

  const renderFormLogin = (
    <div>
      <input
        type="text"
        name="email"
        value={user.email}
        placeholder="type your email"
        onChange={handleChange}
        required
      />
      {renderErrorMessage('email')}
      <input
        type="password"
        name="password"
        value={user.password}
        placeholder="type your password"
        onChange={handleChange}
        required
      />
      {renderErrorMessage('password')}
      <div className="login-button">
        <button onClick={handleLogin}> Login </button>
        <Link to="/register">
          <button>Register</button>
        </Link>
      </div>
    </div>
  );
  return (
    <SubmitFormFancyCSS>
      <div>
        {console.log(user)}

        <h1>Login</h1>
        {isSubmitted ? (
          <div>
            {alert(` login success !`)}
            {history.push('/')}
          </div>
        ) : (
          renderFormLogin
        )}
      </div>
    </SubmitFormFancyCSS>
  );
};
