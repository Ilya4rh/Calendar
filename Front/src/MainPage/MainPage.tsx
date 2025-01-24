import React, {useEffect, useState} from 'react';
import styles from './mainpage.module.css';
import {Button, Calendar, Center, ScrollContainer, Spinner} from "@skbkontur/react-ui";
import {DateHelpers} from "../Common/DateHelpers";
import {EventCard} from "./EventCard";
import {EventModal} from "./EventModal";
import {EventApi} from "../Apis/EventApi";

export function MainPage() {

    const [isLoading, setIsLoading] = useState(true);

    const [events, setEvents] = useState<EventApi.EventDto[]>([]);

    const [currentDate, setCurrentDate] = useState(new Date());

    const daysOfWeek = ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'];

    useEffect(() => {
        EventApi.getEventsByCreatorIdForYear()
            .then(x => {
                setEvents(x.data);
                setIsLoading(false);
            })
            .catch(e => console.log(e));
    }, [])

    const timeConversion = (num: number) => {
        return num > 9 ? num + ":00" : "0" + num + ":00";
    }

    const addTrailingZeroIfNeed = (num: number) => {
        return num < 10 ? "0" + num : num;
    }

    const addDays = (date: Date, days: number) => {
        const result = new Date(date);
        result.setDate(result.getDate() + days);
        return result;
    }

    const [showModal, setShowModal] = useState(false);

    const firstWeekDay = DateHelpers.getWeekFirstDay(currentDate);
    const [date, setDate] = React.useState(`${currentDate.getDate()}.${currentDate.getMonth() + 1}.${currentDate.getFullYear()}`);

    const calculatePadding = (x: EventApi.EventDto) => {
        const eventsInThisHourEndingBefore = events.filter(z => z.startDateTime.getHours() == x.startDateTime.getHours() && z.endDateTime <= x.startDateTime);
        const minutesTakenInThisHour = Math.max(...eventsInThisHourEndingBefore.map(z => z.endDateTime.getMinutes()), 0);
        return x.startDateTime.getMinutes() / 60 * 100 - minutesTakenInThisHour / 60 * 100;
    }

    const getEventsStartsInThisAndDayHour = (day: number, hour: number) => {
        const z = events[0];
        const t = z.startDateTime.getTime();
        const d = z.endDateTime.getTime();
        return events.sort((a, b) => a.endDateTime.getTime() - b.endDateTime.getTime())
            .filter(x => x.startDateTime.getDate() == addDays(firstWeekDay, day).getDate()
                && x.startDateTime.getMonth() == addDays(firstWeekDay, 0).getMonth()
                && x.startDateTime.getFullYear() == addDays(firstWeekDay, 0).getFullYear()
                && x.startDateTime.getHours() == hour);
    }

    return (
        <div>
            {isLoading && <Center><Spinner type={"big"}></Spinner></Center>}
            {!isLoading &&
                <div className={styles.MainPageContainer}>
                    <div className={styles.CalendarContainer}>
                        {showModal && <EventModal onClose={() => setShowModal(false)} onSave={(x) =>{
                            setEvents(events.concat(x));
                            setShowModal(false);
                        }}/>}
                        <Calendar
                            value={date}
                            onValueChange={(x) => {
                                const splitDate = x.split(".").map(x => Number(x));
                                setDate(x);
                                setCurrentDate(new Date(splitDate[2], splitDate[1] - 1, splitDate[0]));
                            }}
                        />
                        <Button onClick={() => setShowModal(true)}>Создать событие</Button>
                    </div>
                    <ScrollContainer className={styles.ScheduleContainer} showScrollBar='hover'>
                        <table className={styles.Schedule}>
                            <tr>
                                <th></th>
                                {daysOfWeek.map((day, i) => {
                                    return daysOfWeek[DateHelpers.getWeekDayIndex(currentDate)] == day ? (
                                        <th className={styles.CurrentWeekDay}>
                                            {day}<br/>
                                            {addTrailingZeroIfNeed(addDays(firstWeekDay, i).getDate())}.{addTrailingZeroIfNeed(addDays(firstWeekDay, i).getMonth() + 1)}
                                        </th>
                                    ) : (
                                        <th>
                                            {day}<br/>
                                            {addTrailingZeroIfNeed(addDays(firstWeekDay, i).getDate())}.{addTrailingZeroIfNeed(addDays(firstWeekDay, i).getMonth() + 1)}
                                        </th>
                                    )
                                })}
                            </tr>
                            {[...Array(24)].map((_, hour) =>
                                (
                                    <tr>
                                        <td className={styles.ScheduleHourCell}>
                                            <div className={styles.Hour}>
                                                {timeConversion(hour)}
                                            </div>
                                        </td>
                                        {[...Array(7)].map((_, day) => {
                                            return (
                                                <td>
                                                    <div style={{height: "100px", width: "100%"}}>
                                                        {getEventsStartsInThisAndDayHour(day, hour)
                                                            .map(x => {return(
                                                                <div style={{paddingTop: `${calculatePadding(x)}px`}}>
                                                                    <EventCard onChange={(event) => {
                                                                        setEvents(events.map(x => {
                                                                            if (x.id == event.id)
                                                                                return {startDateTime: event.startDateTime, endDateTime: event.endDateTime, title: event.title, repeat: event.repeat, id: event.id};
                                                                            return x;
                                                                        }))}
                                                                    } onDelete={() => setEvents(events.filter(z => z.id != x.id))} event={x}/>
                                                                </div>
                                                            )})}
                                                    </div>
                                                </td>
                                            )
                                        })}
                                    </tr>
                                )
                            )}
                        </table>
                    </ScrollContainer>
                </div>
            }
        </div>
    );
}