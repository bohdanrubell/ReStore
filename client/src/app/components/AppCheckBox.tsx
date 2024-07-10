import {useController, UseControllerProps} from "react-hook-form";
import {FormControlLabel, Checkbox} from "@mui/material";
import {} from "@mui/material";

interface Props extends UseControllerProps{
    label: string;
}


export default function AppCheckBox(props: Props){
    const {field} = useController({...props, defaultValue: false});

    return (
        <FormControlLabel
            control={
                <Checkbox
                    {...field}
                    checked={field.value}
                    color='secondary'
                />
            }
            label={props.label}
        />
    )

}