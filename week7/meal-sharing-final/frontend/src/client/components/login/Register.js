import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { SubmitFormFancyCSS } from '../SubmitFormFancyCSS';
export const Register = () => {
  const [message, setMessage] = useState('');
  const [isDone, setIsDone] = useState(false);
  const [user, setUSer] = useState({
    name: '',
    email: '',
    password: '',
    reEnterPassword: '',
    role: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUSer({ ...user, [name]: value });
  };

  let handlRegisterUser = async (e) => {
    e.preventDefault();
    const loginPost = {
      user_name: user.name,
      user_email: user.email,
      user_password: user.password,
      user_reEnteredPassword: user.reEnterPassword,
      role: user.role,
    };

    try {
      setIsDone(true);
      let res = await fetch('http://localhost:3000/api/login', {
        method: 'POST',
        headers: { 'Content-Type': 'Application/json' },
        body: JSON.stringify(loginPost),
      });

      if (res.status === 201) {
        setMessage(`User ${user.name} registered successfully`);
        setIsDone(false);
      } else {
        setMessage('Some error occured');
      }
    } catch (err) {
      console.log(err);
    }
  };

  const renderFormRegister = (
    <div>
      {console.log(user)}
      <h1>Register</h1>
      {message}
      <input
        type="text"
        name="name"
        value={user.name}
        placeholder="type your name"
        onChange={handleChange}
      />
      <input
        type="text"
        name="email"
        value={user.email}
        placeholder="type your email"
        onChange={handleChange}
      />
      <select
        type="text"
        name="role"
        value={user.role}
        placeholder="choose your role"
        onChange={handleChange}
        required
      >
        <option value="" disabled selected>
          your role ...
        </option>
        <option value="developer"> developer</option>
        <option value="owner"> owner</option>
        <option value="chef"> chef</option>
        <option value="waiter"> waiter</option>
      </select>

      <input
        type="password"
        name="password"
        value={user.password}
        placeholder="type your password"
        onChange={handleChange}
      />
      <input
        type="password"
        name="reEnterPassword"
        value={user.reEnterPassword}
        placeholder="retype your password"
        onChange={handleChange}
      />
      <div className="login-button">
        <Link to="/login">
          <button>Login</button>
        </Link>
        <button onClick={handlRegisterUser}>Register</button>
      </div>
    </div>
  );
  return <SubmitFormFancyCSS>{renderFormRegister}</SubmitFormFancyCSS>;
};
