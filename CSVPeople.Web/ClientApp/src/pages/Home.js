import React, { useState, useEffect } from 'react';
import axios from 'axios';
import PersonRow from '../components/PersonRow';

const HomePage = () => {

    const [people, setPeople] = useState([])

    const getPeople = async () => {
        const { data } = await axios.get('/api/people/getpeople');
        setPeople(data);
    }

    useEffect(() => {  
        getPeople();
    }, []);

    const onDeleteClick = async () => {
        await axios.post('/api/people/deletepeople');
        getPeople();
    }

    return (
        <>
            <div className="row">
                <div className="col-md-6  offset-md-3">
                    <button className="btn btn-danger btn-block" onClick={ onDeleteClick}>Delete All</button>
                </div>
            </div>
            <br/>
            <table className="table table-hover table-bordered table-stripe">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map(p => (<PersonRow person={p}/>))}
                </tbody>
            </table>
        </>
    )
}

export default HomePage;