import BasketTable from "../basket/BasketTable.tsx";
import {
    Button,
    Grid,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableRow,
    Typography
} from "@mui/material";
import {currencyFormat} from "../../app/util/util.ts";
import {Order} from "../../app/models/order.ts";
import {BasketItem} from "../../app/models/basket.ts";

interface Props{
    item?: Order;
    setSelectedOrder: (value: number) => void
}

export default function OrderDetail({item, setSelectedOrder}: Props) {
    if(item) {
        return (
            <>
                <Grid container spacing={1}>
                    <Grid item xs={8}>
                        <Typography gutterBottom variant={'h3'}>
                            Order #{item.id} - Status: {item.orderStatus}
                        </Typography>
                    </Grid>
                    <Grid item xs={4}>
                        <Button onClick={() => setSelectedOrder(0)}>
                            Back to main page
                        </Button>
                    </Grid>
                </Grid>
                <BasketTable items={item.orderItems as BasketItem[]} isBasket={false} />
                <Grid container>

                    <Grid item xs={6}></Grid>
                    <Grid item xs={6}>
                        <TableContainer component={Paper} variant={'outlined'}>
                            <Table>
                                <TableBody>
                                    <TableRow>
                                        <TableCell colSpan={2}>Subtotal</TableCell>
                                        <TableCell align="right">{currencyFormat(item.subtotal)}</TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell colSpan={2}>Delivery fee*</TableCell>
                                        <TableCell align="right">{currencyFormat(item.deliveryFee)}</TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell colSpan={2}>Total</TableCell>
                                        <TableCell align="right">{currencyFormat(item.total)}</TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell>
                                            <span style={{fontStyle: 'italic'}}>*Orders over $100 qualify for free delivery</span>
                                        </TableCell>
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Grid>
                </Grid>
            </>
        )
    }
}
