import {Hint, Input, InputProps, PasswordInput} from "@skbkontur/react-ui";
import React, {useEffect, useState} from "react";

interface InputViewProps extends InputProps{
    validationMessages?: string[],
    isPassword?: boolean
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
            {isPassword
                ? <PasswordInput {...otherProps} error={showValidation} onClick={() => setShowValidation(false)}/>
                : <Input {...otherProps} error={showValidation} onClick={() => setShowValidation(false)}/>}
        </Hint>
    )
}