import {DatePicker, Hint, Input, InputProps, MaskedInput, PasswordInput} from "@skbkontur/react-ui";
import React, {useEffect, useState} from "react";

interface InputViewProps extends InputProps{
    validationMessages?: string[],
    isPassword?: boolean,
    isDate?: boolean,
    isTime?: boolean
}

export function InputView(props: InputViewProps){
    const {validationMessages, isPassword, ...otherProps} = props;
    const [showValidation, setShowValidation] = useState(validationMessages !== undefined && validationMessages.length > 0);
    useEffect(() => {setShowValidation(validationMessages !== undefined && validationMessages?.length > 0)},
        [validationMessages]);
    return (
        <Hint text={validationMessages?.at(0)}
              pos={"right"}
              manual={true}
              opened={showValidation}>
            {isPassword &&
                <PasswordInput {...otherProps} error={showValidation}/>
            }
            {props.isTime &&
                <MaskedInput
                    onValueChange={otherProps.onValueChange}
                    value={otherProps.value}
                    mask="Hh:Mm"
                    alwaysShowMask
                    error={showValidation}
                    formatChars={{
                        H: '[0-2]',
                        h: props.value!.startsWith('2') ? '[0-3]' : '[0-9]',
                        M: '[0-5]',
                        m: '[0-9]',
                    }}
                />
            }
            {props.isDate && <DatePicker error={showValidation} value={otherProps.value} onValueChange={otherProps.onValueChange!} enableTodayLink />}
            {(!props.isTime && !props.isPassword && !props.isDate) &&
                <Input {...otherProps} error={showValidation}/>
            }
        </Hint>
    )
}