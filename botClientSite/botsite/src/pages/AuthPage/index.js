import React, { useState, useContext } from 'react';
import { Redirect } from 'react-router-dom';
import { AuthContext } from '../../context/AuthContext';
import { request } from '../../hooks/http.hook';
import './style.css';

export const AuthPage = () => {
    const { isAuth, login } = useContext(AuthContext);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const loginUpdateHandler = event => {
        setUsername(event.target.value);
    }
    const passwordUpdateHandler = event => {
        setPassword(event.target.value);
    }
    const authClickHandler = async event => {
        const req = await request('/api/Users/Auth', {method: 'POST', body: JSON.stringify({Username: username, Password: password}), headers: {"Accept": "application/json", "Content-Type" : "application/json"}});
        const res = await req.json();
        if(req.ok === true){
            login(res.acces_token);
        }
        else
            setError("Неверный логин или пароль.");
    }
    if(isAuth === true)
        return ( <Redirect to="/chats"/> )

    return (
        <div className="pages">
            <div className="auth-window">
                <p style={{color: 'red'}}>{error}</p>
                <p>Введите логин:</p>
                <input type="text" name="login" onChange={loginUpdateHandler} value={username}/>
                <p>Введите пароль:</p>
                <input type="password" name="password" onChange={passwordUpdateHandler} value={password}/>
                <button onClick={authClickHandler}>Authorize</button>
            </div>
        </div>
    )
}