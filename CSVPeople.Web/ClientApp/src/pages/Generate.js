import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Generate = () => {

    const [amt, setAmt] = useState(0);

    const onGenerateClick = () => {
        window.location = (`peoplefile/download?quantity=${amt}`);
        setAmt(0);
    }

    return (
        <>
            <div className="d-flex vh-100" style={{ marginTop: -70 }}>
                <div className="d-flex w-100 justify-content-center align-self-center">
                    <div className="row">
                        <input type="number" className="form-control-lg" placeholder="Amount" value={amt} onChange={e => setAmt(e.target.value)} style={{ width: 150    }} />
                    </div><div className="row">
                        <div className="col-md-4">
                            <button className="btn btn-primary btn-lg" onClick={onGenerateClick}>Generate</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Generate;