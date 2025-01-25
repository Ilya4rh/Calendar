import {AuthenticationApi} from "../Apis/AuthenticationApi";
import {Button} from "@skbkontur/react-ui";
import React from "react";
import {useNavigate} from "react-router-dom";

export function ExitButton() {

    const navigate = useNavigate();


    return (
        <Button style={{position: "absolute", top: "40px", right: "40px", width: "200px"}}
                use="danger"
                onClick={ async () => {
                    await AuthenticationApi.logOut();
                    navigate("/");
                }}
        >
            Выход
        </Button>
    )
}