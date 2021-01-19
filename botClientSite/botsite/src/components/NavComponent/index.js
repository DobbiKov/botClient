import React, { useContext } from 'react';
import { AuthContext } from '../../context/AuthContext';
import { Link } from 'react-router-dom';
import './style.css';

export const NavComponent = () => {
    const { isAuth, logout } = useContext(AuthContext);
    if(isAuth === false)
        return null;
    else{
        return (
            <header>
                <Link to="/chats" className="link">Чаты</Link>
                <Link onClick={logout}>Выйти</Link>
            </header>
        )
    }
}