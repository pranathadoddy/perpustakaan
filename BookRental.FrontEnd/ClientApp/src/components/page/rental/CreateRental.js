import React, { Component } from 'react';
import { withRouter } from 'react-router';
import DatePicker from "react-datepicker";

import RentalApiService from '../../../api/Rental/RentalApiService';
import BookApiService from '../../../api/Book/BookApiService';
import CustomerApiService from '../../../api/Customer/CustomerApiService';

import "react-datepicker/dist/react-datepicker.css";

class CreateRental extends Component {
    constructor(props) {
        super(props);

        this.state = {
            customers: [],
            books: [],
            customerId: null,
            bookId: null,
            returnDate: new Date(),
            isLoading : true
        };
    }

    componentDidMount() {
        this.customerApiService = new CustomerApiService();
        this.bookApiService = new BookApiService();
        this.rentalApiService = new RentalApiService();
        this.populateCustomerData();
        this.populateBookData();
    }

    render() {
        return (
            <form>
                <h1>Borrow Book</h1>
                <div className="form-group">
                    <label>Member:</label>
                    <select className="form-control" onChange={this.handleChangeMember.bind(this)}>
                        {this.state.customers.map((option) => <option key={option.id} value={option.id}>{option.name}</option>)}
                    </select>
                </div>
                <div className="form-group">
                    <label>Book:</label>
                    <select className="form-control" onChange={this.handleChangeBook.bind(this)}>
                        {this.state.books.map((book) => <option key={book.id} value={book.id}>{book.title}</option>)}
                    </select>
                </div>
                <div className="form-group">
                    <label>Return Date:</label>
                    <DatePicker selected={this.state.returnDate} className="form-control" onChange={this.handleChangeReturnDate.bind(this)} />
                </div>
                <button className="btn btn-primary" onClick={this.onSubmit.bind(this)}>Submit</button>
            </form>
        );
    }

    handleChangeMember(event) {
        const { value } = event.target;

        this.setState(prevState => ({
                ...prevState,
                customerId: value
        }));
    }

    handleChangeBook(event) {
        const { value } = event.target;

        this.setState(prevState => ({
            ...prevState,
            bookId: value
        }));
    }

    handleChangeReturnDate(date) {
        this.setState(prevState => ({
            ...prevState,
            returnDate: date
        }));
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
                customerId: parseInt(result[0].id),
                loading: false
            }));
        };

        this.customerApiService
            .getList()
            .then(response => successResponse(response));
    }

    populateBookData() {
        const successResponse = response => {
            let result = [];
            if (Array.isArray(response.value)) {
                result = response.value;
            }

            this.setState(prevState => ({
                ...prevState,
                books: result,
                bookId: parseInt(result[0].id),
                loading: false
            }));
        };

        this.bookApiService
            .getList()
            .then(response => successResponse(response));
    }

    onSubmit(event) {
        event.preventDefault();
        const { bookId, customerId, returnDate } = this.state;

        if (!bookId || !customerId) return;

        
        this.rentalApiService.post({
            BookId: bookId,
            CustomerId: customerId,
            ReturnDate: returnDate.toJSON()
        }).then(response => {
            this.props.history.push('/borrowing-book');
        });
    }
}

export default withRouter(CreateRental);