import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { request } from '../../hooks/http.hook';
import './style.css';

export const ChatPage = () => {
    const [load, setLoad] = useState(false);
    const [data, setData] = useState([]);
    const [message, setMessage] = useState("");
    const idx = useParams().id;

    const populateMessages = async () => {
        const req = await request(`/api/Messages/ByChatId/${idx}`, {method: 'GET', headers: {'Content-Type': 'application/json'}});
        const res = await req.json();
        if(req.ok === true){
            setLoad(true);
            setData(res);
        }
    }

    const messageChangeHandler = event => {
        setMessage(event.target.value);
    }
    const mymessClickHandler = async event => {
        const req = await request('/api/Messages', {method: 'POST', body: JSON.stringify({"Telegramchatid": Number(idx), "Text": message, "Telegramuserid": 1495033650}), headers: {'Content-Type': 'application/json'}});
        const res = await req.json();
        if(req.ok === true)
            await populateMessages();
        setMessage("");
    }

    useEffect(async () => {
        await populateMessages();
    }, []);
    if(load === false)
        return( <h1>Loading...</h1>)
    
    return (
        <div className="pages">
            <div className="container">
                <div className="messages">
                    {data.map(mess =>
                        <div className={mess.telegramuserid === 1495033650 ? "you mess" : "not-you mess"} key={mess.id}>
                            <p className="names">{mess.firstname} {mess.lastname}</p>
                            <p>{mess.text}</p>
                        </div>
                    )}
                </div>
                <div className="inputs">
                    <input type="text" onChange={messageChangeHandler} value={message} className="inputs-mess"/><input type="submit" value="Отправить" className="inputs-submit" onClick={mymessClickHandler}/>
                </div>
            </div>
        </div>
    )
}
