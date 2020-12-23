import React, { Component } from 'react';
import { Route } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import Book from './components/page/book/Book';
import CreateBookForm from './components/page/book/CreateBookForm';
import EditBookForm from './components/page/book/EditBookForm';

import Customer from './components/page/customer/Customer';
import CreateCustomerForm from './components/page/customer/CreateCustomerForm';
import EditCustomerForm from './components/page/customer/EditCustomerForm';

import Rental from './components/page/rental/Rental';
import CreateRental from './components/page/rental/CreateRental';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <BrowserRouter>
                <Layout>
                    <Route exact path='/' component={Home} />
                    <Route path='/book' component={Book} />
                    <Route path='/create-book' component={CreateBookForm} />
                    <Route path='/edit-book/:id' component={EditBookForm} />
                    <Route path='/customer' component={Customer} />
                    <Route path='/create-customer' component={CreateCustomerForm} />
                    <Route path='/edit-customer/:id' component={EditCustomerForm} />
                    <Route path='/borrowing-book' component={Rental} />
                    <Route path='/borrow-book' component={CreateRental} />
                </Layout>
            </BrowserRouter>
        );
    }
}
