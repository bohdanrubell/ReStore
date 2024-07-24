import {Elements} from "@stripe/react-stripe-js";
import CheckoutPage from "./CheckoutPage.tsx";
import {loadStripe} from "@stripe/stripe-js";
import {useAppDispatch} from "../../app/store/configureStore.ts";
import {useEffect, useState} from "react";
import agent from "../../app/api/agent.ts";
import {setBasket} from "../basket/basketSlice.ts";
import LoadingComponent from "../../app/layout/LoadingComponent.tsx";

const stripePromise = loadStripe('pk_test_51PbLjZKWJwsmwbhPIsrHw5yZjZCRpVuCJ32wiYd7FxaUhURRtSNeIY4ZnDDujlltKSz6xZ2YE4GHTCqzYPECc4uf00LZtDD5lY');


export default function CheckoutWrapper() {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent.Payments.createPaymentIntent()
            .then(basket => dispatch(setBasket(basket)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }, [dispatch])

    if (loading) return <LoadingComponent message={'Loading checkout...'}/>

    return (
        <Elements stripe={stripePromise}>
            <CheckoutPage />
        </Elements>
    )
}