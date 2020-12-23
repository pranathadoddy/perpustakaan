import React, { Component } from 'react';
import { Route } from 'react-router-dom'

import CustomerApiService from '../../../api/Customer/CustomerApiService';

class Customer extends Component {

    constructor(props) {
        super(props);
        this.state = { customers: [], loading: true };
    }

    componentDidMount() {
        this.customerApiService = new CustomerApiService();
        this.populateCustomerData();
    }

    static renderCustomerTable(customers) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Address</th>

                    </tr>
                </thead>
                <tbody>
                    {customers.map(customer =>
                        <tr key={customer.name}>
                            <td><a href={`/edit-customer/${customer.id}`}>{customer.name}</a></td>
                            <td>{customer.address}</td>
                        </tr>
                    )}
                </tbody>
                <tfoot>
                    <tr>
                        <th colSpan="2">
                            <div className="btn-group">
                                <Route render={({ history }) => (
                                    <button
                                        type='button'
                                        className="btn btn-primary"
                                        onClick={() => { history.push('/create-customer') }}
                                    >
                                        <span>Add Customer</span>
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
            : Customer.renderCustomerTable(this.state.customers);

        return (
            <div>
                <h1 id="tabelLabel" >Member</h1>
                {contents}
            </div>
        );
    }

    populateCustomerData() {
        const successResponse = response => {
            let result = [];
            if (Array.isArray(response.value)) {
                result = response.value;
            }

            this.setState(prevState => ({
                ...prevState,
                customers: result,
                loading: false
            }));
        };

        this.customerApiService
            .getList()
            .then(response => successResponse(response));
    }
}

export default Customer;
