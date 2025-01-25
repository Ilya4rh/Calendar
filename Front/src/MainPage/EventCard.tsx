import React, {useState} from "react";
import styles from './mainpage.module.css';
import {EventModal} from "./EventModal";
import {EventApi} from "../Apis/EventApi";

interface EventCardProps{
    event: EventApi.EventDto
    onDelete: (id: string) => Promise<void>;
    onChange: (event: EventApi.EventDto) => Promise<void>;
}

export function EventCard(props: EventCardProps){
    const millisecondsDiff = props.event.endDateTime.getTime() - props.event.startDateTime.getTime();
    const diffMins = millisecondsDiff / 60000;
    const x = diffMins / 60;
    const randomColor = "#"+((1<<24)*(props.event.id.charCodeAt(10) / 100)|0).toString(16);
    const [backgroundColor, setBackgroundColor] = useState(randomColor);
    const [showEditModal, setShowEditModal] = useState(false);
    return (
        <>
            {showEditModal &&
                <EventModal onClose={() => setShowEditModal(false)}
                            onSave={ async (event) => {
                                await props.onChange(event);
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
                {props.event.title}
            </button>
        </>
    );
}