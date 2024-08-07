import {Box, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from "@mui/material";
import {LoadingButton} from "@mui/lab";
import {addBasketItemAsync, removeBasketItemAsync} from "./basketSlice.ts";
import {Add, Delete, Remove} from "@mui/icons-material";
import {BasketItem} from "../../app/models/basket.ts";
import {useAppDispatch} from "../../app/store/configureStore.ts";

interface Props{
    items: BasketItem[];
    isBasket? : boolean;
}

export default function BasketTable({items, isBasket = true}: Props) {
    const dispatch = useAppDispatch();
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Product</TableCell>
                        <TableCell align="right">Price</TableCell>
                        <TableCell align="center">Quantity</TableCell>
                        <TableCell align="right">Subtotal</TableCell>
                        {isBasket &&
                        <TableCell align="right"></TableCell>
                        }
                    </TableRow>
                </TableHead>
                <TableBody>
                    {items.map((item) => (
                        <TableRow
                            key={item.productId}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                <Box display='flex' alignItems='center'>
                                    <img style={{ height: 50, marginRight: 20 }} src={item.pictureUrl} alt={item.name} />
                                    <span>{item.name}</span>
                                </Box>
                            </TableCell>
                            <TableCell align="right">${(item.price / 100).toFixed(2)}</TableCell>
                            <TableCell align="center">
                                {isBasket &&
                                <LoadingButton
                                    color='error'
                                    loading={status === 'pendingRemoveItem' + item.productId + 'rem'}
                                    onClick={() => dispatch(removeBasketItemAsync({
                                        productId: item.productId, quantity: 1, name: 'rem'
                                    }))}
                                >
                                    <Remove />
                                </LoadingButton>
                                }
                                {item.quantity}
                                {isBasket &&
                                <LoadingButton
                                    loading={status === 'pendingAddItem' + item.productId}
                                    onClick={() => dispatch(addBasketItemAsync({productId: item.productId}))}
                                    color='secondary'
                                >
                                    <Add />
                                </LoadingButton>
                                }
                            </TableCell>
                            <TableCell align="right">${((item.price / 100) * item.quantity).toFixed(2)}</TableCell>
                            <TableCell align="right">
                                {isBasket &&
                                <LoadingButton
                                    loading={status === 'pendingRemoveItem' + item.productId + 'del'}
                                    onClick={() => dispatch(removeBasketItemAsync({
                                        productId: item.productId, quantity: item.quantity, name: 'del'
                                    }))}
                                    color='error'
                                >
                                    <Delete />
                                </LoadingButton>
                                }
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}