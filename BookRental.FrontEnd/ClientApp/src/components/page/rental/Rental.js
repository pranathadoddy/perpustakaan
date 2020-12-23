import React, { Component } from 'react';
import { Route } from 'react-router-dom';

import RentalApiService from '../../../api/Rental/RentalApiService';

class Rental extends Component {
    constructor(props) {
        super(props);
        this.state = { rentals: [], loading: true };
    }

    componentDidMount() {
        this.rentalApiService = new RentalApiService();
        this.populateRentalData();
    }

    static renderRentalsTable(rentals) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Member</th>
                        <th>Book</th>
                        <th>Return Date</th>
                    </tr>
                </thead>
                <tbody>
                    {rentals.map(rental =>
                        <tr key={rental.customerName}>
                            <td>{rental.customerName}</td>
                            <td>{rental.bookTitle}</td>
                            <td>{rental.returnDate}</td>
                        </tr>
                    )}
                </tbody>
                <tfoot>
                    <tr>
                        <th colSpan="3">
                            <div className="btn-group">
                                <Route render={({ history }) => (
                                    <button
                                        type='button'
                                        className="btn btn-primary"
                                        onClick={() => { history.push('/borrow-book') }}
                                    >
                                        <span>Borrow Book</span>
                                    </button>
                                )} />
                            </div>
                        </th>
                    </tr>
                </tfoot>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Rental.renderRentalsTable(this.state.rentals);

        return (
            <div>
                <h1 id="tabelLabel" >Book Borrow List</h1>
                {contents}
            </div>
        );
    }

    populateRentalData() {
        const successResponse = response => {
            let result = [];
            if (Array.isArray(response.value)) {
                result = response.value;
            }

            this.setState(prevState => ({
                ...prevState,
                rentals: result,
                loading: false
            }));
        };

        this.rentalApiService
            .getList()
            .then(response => successResponse(response));
    }
}

export default Rental;