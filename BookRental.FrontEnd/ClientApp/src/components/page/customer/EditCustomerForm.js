import React, { Component } from 'react';
import { withRouter } from 'react-router';

import CustomerApiService from '../../../api/Customer/CustomerApiService';


class EditCustomerForm extends Component {
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
        this.readCustomerData();
    }


    render() {
        return (
            <form
            >
                <h1>Edit Member</h1>
                <div className="form-group">
                    <label>Name:</label>
                    <input type="text"
                        className="form-control"
                        name="title"
                        value={this.state.customer.name}
                        onChange={this.handleChangeName.bind(this)} />
                </div>
                <div className="form-group">
                    <label>Address:</label>
                    <input type="textarea"
                        className="form-control"
                        name="textValue"
                        value={this.state.customer.address}
                        onChange={this.handleChangeAddress.bind(this)} />
                </div>
                <div className="col-sm-8 col-offset-4">
                    <button className="btn btn-primary" onClick={this.onSubmit.bind(this)}>Submit</button>
                    <button className="btn btn-danger" onClick={this.onDelete.bind(this)}>Delete</button>
                </div>
            </form>
        );
    }

    readCustomerData() {
        const successResponse = response => {

            this.setState(prevState => ({
                ...prevState,
                customer: {
                    name: response.value.name,
                    address: response.value.address
                }
            }));
        };

        const { id } = this.props.match.params;

        this.customerApiService
            .get(id)
            .then(response => successResponse(response));
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
        const { id } = this.props.match.params;

        this.customerApiService.put(id, {
            Name: name,
            Address: address
        }).then(response => {
            this.props.history.push('/customer');
        });
    }

    onDelete(event) {
        event.preventDefault();
        const { id } = this.props.match.params;

        this.customerApiService.delete(id)
            .then(response => {
                this.props.history.push('/customer');
            });
    }
}

export default withRouter(EditCustomerForm);