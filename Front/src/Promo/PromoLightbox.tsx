import {Button, Modal} from "@skbkontur/react-ui";
import styles from "./promo.module.css";
import React, {useState} from "react";
import {UsersApi} from "../Apis/UsersApi";
import {InputView} from "../Common/InputView";
import * as EmailValidator from 'email-validator';
import {useNavigate} from "react-router-dom";
import Cookies from 'universal-cookie';

interface LoginLightboxProps{
    onSubmit: () => void;
    onClose: () => void;
    isRegistration: boolean;
}
export function PromoLightbox(props: LoginLightboxProps) {

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [emailValidations, setEmailValidations] = useState<string[]>([]);
    const [passwordValidations, setPasswordValidations] = useState<string[]>([]);
    const navigate = useNavigate();
    const cookies = new Cookies();

    const login = async () => {
        const newEmailValidations: string[] = [];
        const newPasswordValidations: string[] = [];

        if (!EmailValidator.validate(email))
            newEmailValidations.push("Введите корректный адрес электронной почты");

        if (password.length === 0)
            newPasswordValidations.push("Введите пароль");
        else if (password.length < 9)
            newPasswordValidations.push("Введен неверный пароль")

        if (newEmailValidations.length === 0 && newPasswordValidations.length === 0){
            const request: UsersApi.AuthorizeUserRequest = {
                email: email,
                password: password
            }
            const response = (await UsersApi.authorize(request)).data;

            if (response.authorizationResult == UsersApi.AuthorizationResult.Success){
                cookies.set("Auth", response.authToken);
                navigate("/MainPage");
            }

            if (response.authorizationResult === UsersApi.AuthorizationResult.UserNotFound)
                newEmailValidations.push("Пользователь с такой электронной почтой не найден");
            if (response.authorizationResult === UsersApi.AuthorizationResult.WrongPassword)
                newPasswordValidations.push("Введен неверный пароль");
        }
        setEmailValidations(newEmailValidations);
        setPasswordValidations(newPasswordValidations);
    }

    const register = async () => {
        const newEmailValidations: string[] = [];
        const newPasswordValidations: string[] = [];

        if (!EmailValidator.validate(email))
            newEmailValidations.push("Введите корректный адрес электронной почты");

        if (password.length < 9)
            newPasswordValidations.push("Пароль должен содержать больше 8 символов");

        if (newEmailValidations.length === 0 && newPasswordValidations.length === 0){
            const request: UsersApi.AuthorizeUserRequest = {
                email: email,
                password: password
            }
            const response = (await UsersApi.register(request)).data;

            if (response.registrationResult == UsersApi.RegistrationResult.Success){
                cookies.set("Auth", response.authToken);
                navigate("/MainPage");
            }

            if (response.registrationResult === UsersApi.RegistrationResult.AlreadyExist)
                newEmailValidations.push("Пользователь с такой электронной почтой уже существует");
        }

        setEmailValidations(newEmailValidations);
        setPasswordValidations(newPasswordValidations);
    }

    return (
        <Modal noClose={true}>
            <Modal.Header>
                <div style={{textAlign: "center"}}>
                    {props.isRegistration ? "Регистрация" : "Вход"}
                </div>
            </Modal.Header>
            <Modal.Body>
                <div className={styles.LightboxInputsContainer}>
                    <div>
                        Электронная почта<br/>
                        <InputView validationMessages={emailValidations} width={400} size="medium" type="email" value={email} onValueChange={setEmail}/>
                    </div>
                    <div>
                        Пароль<br/>
                        <InputView validationMessages={passwordValidations} width={400} size="medium" isPassword={true} value={password} onValueChange={setPassword}/>
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <div className={styles.LightboxButtonsContainer}>
                    <Button size="medium" className={styles.LightboxButtonMain} use="success" onClick={props.isRegistration ? register : login}>
                        {props.isRegistration ? "Зарегистрироваться" : "Войти"}
                    </Button>

                    <Button size="medium" className={styles.LightboxButton} use="text"
                            onClick={() => {
                                props.onClose();
                                setEmail("");
                                setPassword("");
                            }
                    }>
                        Отмена
                    </Button>
                </div>
            </Modal.Footer>
        </Modal>
    );
}