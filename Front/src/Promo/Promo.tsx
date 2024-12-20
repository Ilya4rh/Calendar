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
                        готовы найти проект или коллег<br />за пару кликов?<br />
                    </div>
                    <div className={styles.BottomText}>
                        <div className={styles.FirstBottom}>
                            ищите единомышленников
                        </div>
                        <div className={styles.SecondBottom}>
                            присоединяйтесь к команде
                        </div>
                        <div className={styles.ThirdBottom}>
                            создавайте собственный проект
                        </div>
                    </div>
                    <button className={styles.ButtonBg}>
                        <div className={styles.MainButtonText}>
                            найти команду
                        </div>
                    </button>
                </div>
            </div>
        </div>
    );
}