import React, { Component } from 'react';
import { withRouter } from 'react-router';

import BookApiService from '../../../api/Book/BookApiService';


class CreateBookForm extends Component {
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
    }

    
    render() {
        return (
            <form
                >
                <h1>Add Book</h1>
                <div className="form-group">
                    <label>Title:</label>
                    <input type="text"
                        className="form-control"
                        name="title"
                        onChange={this.handleChangeTitle.bind(this)} />
                </div>
                <div className="form-group">
                    <label>Description:</label>
                    <input type="textarea"
                        className="form-control"
                        name="textValue"
                        onChange={this.handleChangeDescription.bind(this)} />
                </div>
                <button className="btn btn-primary" onClick={this.onSubmit.bind(this)}>Submit</button>
            </form>    
        );
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

        this.bookApiService.post({
            Title: title,
            Description: description
        }).then(response => {
            this.props.history.push('/book');
        });
    }
}

export default withRouter(CreateBookForm);