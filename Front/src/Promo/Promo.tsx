import styles from './promo.module.css';
import React from "react";
import {PromoHeader} from "./PromoHeader";

export function Promo() {
    return (
        <div>
            <div className={styles.Promo}>
                <PromoHeader/>
                <div className={styles.PromoBody}>
                    <div className={styles.UpperText}>
                        Электронный календарь<br />
                    </div>
                    <div className={styles.BottomText}>
                        <div className={styles.FirstBottom}>
                            планируйте дела
                        </div>
                        <div className={styles.SecondBottom}>
                            создавайте события
                        </div>
                        <div className={styles.ThirdBottom}>
                            управляйте своим временем
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}