import React, { Component } from 'react';
import { withRouter } from 'react-router';

import CustomerApiService from '../../../api/Customer/CustomerApiService';


class CreateCustomerForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            customer: {
                name: "",
                address: ""
            },
            isLoading: false
        };
    }

    componentDidMount() {
        this.customerApiService = new CustomerApiService();
    }


    render() {
        return (
            <form
            >
                <h1>Add Member</h1>
                <div className="form-group">
                    <label>Name:</label>
                    <input type="text"
                        className="form-control"
                        name="title"
                        onChange={this.handleChangeName.bind(this)} />
                </div>
                <div className="form-group">
                    <label>Address:</label>
                    <input type="textarea"
                        className="form-control"
                        name="textValue"
                        onChange={this.handleChangeAddress.bind(this)} />
                </div>
                <button className="btn btn-primary" onClick={this.onSubmit.bind(this)}>Submit</button>
            </form>
        );
    }

    handleChangeName(event) {
        const { value } = event.target;

        this.setState(prevState => ({
            ...prevState,
            customer: {
                ...prevState.customer,
                name: value
            }
        }));
    }

    handleChangeAddress(event) {
        const { value } = event.target;

        this.setState(prevState => ({
            ...prevState,
            customer: {
                ...prevState.customer,
                address: value
            }
        }));
    }

    onSubmit(event) {
        event.preventDefault();
        const { name, address } = this.state.customer;
        if (!name || !address) return;

        this.customerApiService.post({
            Name: name,
            Address: address
        }).then(response => {
            this.props.history.push('/customer');
        });
    }
}

export default withRouter(CreateCustomerForm);