import styles from "./promo.module.css";
import logo from "../assets/group.png";
import React from "react";
import {PromoLightbox} from "./PromoLightbox";

export function PromoHeader(){
    const [loginLightboxOpened, setLoginLightboxOpened] = React.useState(false);
    const [registerLightboxOpened, setRegisterLightboxOpened] = React.useState(false);

    function openLoginLightbox() {
        setLoginLightboxOpened(true);
    }

    function onLoginLightboxClose() {
        setLoginLightboxOpened(false);
    }

    function openRegisterLightbox() {
        setRegisterLightboxOpened(true);
    }

    function onRegisterLightboxClose() {
        setRegisterLightboxOpened(false);
    }
    return (
        <div>
            {registerLightboxOpened && (
                <PromoLightbox isRegistration={true} onSubmit={() => void 0} onClose={onRegisterLightboxClose}/>
            )}
            {loginLightboxOpened && (
                <PromoLightbox isRegistration={false} onSubmit={() => void 0} onClose={onLoginLightboxClose}/>
            )}
            <div className={styles.PromoHeader}>
                <div className={styles.Logo}>
                    <img src={logo} alt='logo' width="80" height="80" />
                </div>

                <div className={styles.HeaderButtonsContainer}>
                    <button className={styles.ContactButton}>
                        <div className={styles.ContactText}>
                            связаться с нами
                        </div>
                    </button>
                    <button onClick={openLoginLightbox} className={styles.LoginButton}>
                        <div className= {styles.LoginText}>
                            вход
                        </div>
                    </button>
                    <button onClick={openRegisterLightbox} className={styles.SignUpButton}>
                        <div className={styles.SignUpText}>
                            регистрация
                        </div>
                    </button>
                </div>
            </div>
        </div>
    )
}