import React, { useContext } from 'react';
import { Redirect, Route } from 'react-router-dom';
import { AuthContext } from './context/AuthContext';

import { AuthPage } from './pages/AuthPage/index';
import { ChatsPage } from './pages/ChatsPage/index';
import { ChatPage } from './pages/ChatPage/index';

export const useRoutes = () => {
    const { isAuth } = useContext(AuthContext);
    return(
        <>
            { isAuth == false ? <Redirect to="/auth"/> : <Redirect to="/chats"/> }
            <Route path="/auth" component={AuthPage}/>
            <Route path="/chats" component={ChatsPage}/>
            <Route path="/chat/:id" component={ChatPage}/>
        </>
    )
}