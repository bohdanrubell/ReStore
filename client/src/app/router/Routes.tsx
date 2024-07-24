import { Navigate, createBrowserRouter } from 'react-router-dom';
import App from '../layout/App';
import HomePage from '../../features/home/HomePage';
import Catalog from '../../features/catalog/Catalog';
import ProductDetails from '../../features/catalog/ProductDetails';
import AboutPage from '../../features/about/AboutPage';
import ContactPage from '../../features/contact/ContactPage';
import ServerError from '../errors/ServerError';
import NotFound from '../errors/NotFound';
import BasketPage from '../../features/basket/BasketPage';
import Register from "../../features/account/Register.tsx";
import Login from "../../features/account/Login.tsx";
import RequireAuth from "./RequireAuth.tsx";
import Orders from "../../features/orders/Orders.tsx";
import CheckoutWrapper from "../../features/checkout/CheckoutWrapper.tsx";
import Inventory from "../../features/admin/Inventory.tsx";

export const router = createBrowserRouter(([
    {
        path: '/',
        element: <App />,
        children: [
            // auth roots
            {element: <RequireAuth />, children:[
                { path: '/checkout', element: <CheckoutWrapper /> },
                { path: 'orders', element: <Orders /> },
                { path: 'inventory', element: <Inventory /> },
            ]},
            // admin roots
            {element: <RequireAuth roles={['Admin']} />, children:[
                    { path: 'inventory', element: <Inventory /> },
                ]},
            { path: '', element: <HomePage /> },
            { path: 'catalog', element: <Catalog /> },
            { path: 'catalog/:id', element: <ProductDetails /> },
            { path: 'about', element: <AboutPage /> },
            { path: 'contact', element: <ContactPage /> },
            { path: '/server-error', element: <ServerError /> },
            { path: '/not-found', element: <NotFound /> },
            { path: '/basket', element: <BasketPage /> },

            { path: '/login', element: <Login /> },
            { path: '/register', element: <Register /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },
        ]
    }
]))