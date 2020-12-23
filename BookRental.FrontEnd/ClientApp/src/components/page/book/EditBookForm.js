import React, { Component } from 'react';
import { withRouter } from 'react-router';

import BookApiService from '../../../api/Book/BookApiService';


class EditBookForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            book: {
                title: "",
                description: ""
            },
            isLoading: false
        };
    }

    componentDidMount() {
        this.bookApiService = new BookApiService();
        this.readBookData();
    }


    render() {
        return (
            <form
            >
                <h1>Edit Book</h1>
                <div className="form-group">
                    <label>Title:</label>
                    <input type="text"
                        className="form-control"
                        name="title"
                        value={this.state.book.title}
                        onChange={this.handleChangeTitle.bind(this)} />
                </div>
                <div className="form-group">
                    <label>Description:</label>
                    <input type="textarea"
                        className="form-control"
                        name="textValue"
                        value={this.state.book.description}
                        onChange={this.handleChangeDescription.bind(this)} />
                </div>
                <div className="col-sm-8 col-offset-4">
                    <button className="btn btn-primary" onClick={this.onSubmit.bind(this)}>Submit</button>
                    <button className="btn btn-danger" onClick={this.onDelete.bind(this)}>Delete</button>
                </div>
            </form>
        );
    }

    readBookData() {
        const successResponse = response => {
            this.setState(prevState => ({
                ...prevState,
                book: {
                    title: response.value.title,
                    description: response.value.description
                }
            }));
        };

        const { id } = this.props.match.params;

        this.bookApiService
            .get(id)
            .then(response => successResponse(response));
    }

    handleChangeTitle(event) {
        const { value } = event.target;

        this.setState(prevState => ({
            book: {
                ...prevState.book,
                title: value
            }
        }));
    }

    handleChangeDescription(event) {
        const { value } = event.target;

        this.setState(prevState => ({
            book: {
                ...prevState.book,
                description: value
            }
        }));
    }

    onSubmit(event) {
        event.preventDefault();
        const { title, description } = this.state.book;
        if (!title || !description) return;
        const { id } = this.props.match.params;

        this.bookApiService.put(id, {
            Title: title,
            Description: description
        }).then(response => {
            this.props.history.push('/book');
        });
    }

    onDelete(event) {
        event.preventDefault();
        const { id } = this.props.match.params;

        this.bookApiService.delete(id)
            .then(response => {
            this.props.history.push('/book');
        });
    }
}

export default withRouter(EditBookForm);