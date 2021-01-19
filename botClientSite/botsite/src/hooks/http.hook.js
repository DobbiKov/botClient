import React from 'react';

export const request = async (url, object = {}) => {
    let _url = `http://botclientapi.dobbikov.ga${url}`;
    const request = await fetch(_url, object);
    return request;
}