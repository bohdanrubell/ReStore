import {useController, UseControllerProps} from "react-hook-form";
import {FormControlLabel, Checkbox} from "@mui/material";

interface Props extends UseControllerProps{
    label: string;
    disabled: boolean;
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
                    disabled={props.disabled}
                />
            }
            label={props.label}
        />
    )
}