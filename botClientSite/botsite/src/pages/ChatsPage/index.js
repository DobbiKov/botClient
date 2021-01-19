import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { request } from '../../hooks/http.hook';
import './style.css';

export const ChatsPage = () => {
    const [load, setLoad] = useState(false);
    const [data, setData] = useState([]);

    useEffect(async () => {
        const req = await request('/api/Chats', {method: 'GET', headers: {'Content-Types': 'application/json'}});
        const res = await req.json();
        if(req.ok === true)
        {
            setLoad(true);
            setData(res);
        }
    }, [])

    if(load == false)
        return( <h1 className="loading">Loading...</h1> )
    return(
        <div className="pages">
            <div className="container">
                {data.map(chat => 
                    <Link to={`/chat/${chat.telegramid}`} key={chat.id} className="chat">
                        <p>{chat.telegramid < 0 ? chat.username : ""} {chat.firstname} {chat.lastname}</p>
                    </Link>   
                )}
            </div>
        </div>
    )
}