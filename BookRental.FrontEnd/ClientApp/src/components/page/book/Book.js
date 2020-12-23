import React, { Component } from 'react';
import { Route } from 'react-router-dom'

import BookApiService from '../../../api/Book/BookApiService';

class Book extends Component {

    constructor(props) {
        super(props);
        this.state = { books: [], loading: true };
    }

    componentDidMount() {
        this.bookApiService = new BookApiService();
        this.populateBookData();
    }

    static renderBooksTable(books) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Description</th>
                        
                    </tr>
                </thead>
                <tbody>
                    {books.map(book =>
                        <tr key={book.title}>
                            <td><a href={`/edit-book/${book.id}`}>{book.title}</a></td>
                            <td>{book.description}</td>
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
                                        onClick={() => { history.push('/create-book') }}
                                    >
                                        <span>Add Book</span>
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
            : Book.renderBooksTable(this.state.books);

        return (
            <div>
                <h1 id="tabelLabel" >Books</h1>
                {contents}
            </div>
        );
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
                loading: false
            }));
        };

        this.bookApiService
            .getList()
            .then(response => successResponse(response));
    }
}

export default Book;
