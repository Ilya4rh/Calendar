import React, {useState} from "react";
import styles from './mainpage.module.css';
import {UserEvent} from "./MainPage";
import {EventModal} from "./EventModal";

interface EventCardProps{
    event: UserEvent
    onDelete: () => void;
    onChange: (event: UserEvent) => void;
}

export function EventCard(props: EventCardProps){
    const millisecondsDiff = props.event.dateTo.getTime() - props.event.dateFrom.getTime();
    const diffMins = millisecondsDiff / 60000;
    const x = diffMins / 60;
    const randomColor = "#"+((1<<24)*Math.random()|0).toString(16);
    const [backgroundColor, setBackgroundColor] = useState(randomColor);
    const [showEditModal, setShowEditModal] = useState(false);
    return (
        <>
            {showEditModal &&
                <EventModal onClose={() => setShowEditModal(false)}
                            onSave={(event) => {
                                props.onChange(event);
                                setShowEditModal(false);
                            }}
                            currentEvent={props.event}
                            onDelete={props.onDelete}
                />}
            <button onMouseUp={() => setBackgroundColor("lightgray")}
                    onMouseDown={() => setBackgroundColor("gray")}
                    onMouseLeave={() => setBackgroundColor(randomColor)}
                    onMouseEnter={() => setBackgroundColor("lightgray")}
                    onClick={() => setShowEditModal(true)}
                    style={{backgroundColor: backgroundColor, height: `${100 * x}px`}}
                    className={styles.EventCard}>
                {props.event.caption}
            </button>
        </>
    );
}