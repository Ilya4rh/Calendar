import {Button, Input, Modal, Select, Toggle} from '@skbkontur/react-ui';
import React, {useState} from "react";
import {InputView} from "../Common/InputView";
import {EventApi} from "../Apis/EventApi";

interface AddEventModalProps {
    onSave: (event: EventApi.EventDto) => void;
    onClose: () => void;
    onDelete?: () => void;
    currentEvent?: EventApi.EventDto;
}

export function EventModal(props: AddEventModalProps){
    const {currentEvent} = props;
    const addTrailingZeroIfNeed = (num: number) => {
        return num < 10 ? "0" + num : num;
    }

    const validateEmpty = (value: string) => {
        if (value.length === 0)
            return ["Введите значение"];
        return [];
    }

    const validateStartDate = () => {
        const splitStartTime = startTime.split(":");
        const splitEndTime = endTime.split(":");
        const startHour = Number(splitStartTime[0]);
        const startMinutes = Number(splitStartTime[1]);
        const endHour = Number(splitEndTime[0]);
        const endMinutes = Number(splitEndTime[1]);

        if (isNaN(startHour) || isNaN(startMinutes))
            return ["Введите значение"]

        if ((!isNaN(endHour) && !isNaN(endMinutes))
            && startHour > endHour
            || startHour === endHour && startMinutes >= endMinutes)
            return ["Время начала должно быть раньше времени конца"]
        return [];
    }

    const validateEndDate = () => {
        const splitStartTime = startTime.split(":");
        const splitEndTime = endTime.split(":");
        const startHour = Number(splitStartTime[0]);
        const startMinutes = Number(splitStartTime[1]);
        const endHour = Number(splitEndTime[0]);
        const endMinutes = Number(splitEndTime[1]);

        if (isNaN(endHour) || isNaN(endMinutes))
            return ["Введите значение"]

        if ((!isNaN(startHour) && !isNaN(startMinutes))
            && startHour > endHour
            || startHour === endHour && startMinutes >= endMinutes)
            return ["Время конца должно быть позже времени конца"]
        return [];
    }

    const [date, setDate] = useState<string>( currentEvent ? `${addTrailingZeroIfNeed(currentEvent.endDateTime.getDate())}.${addTrailingZeroIfNeed(currentEvent.endDateTime.getMonth() + 1)}.${currentEvent.endDateTime.getFullYear()}` : "");
    const [startTime, setStartTime] = useState<string>(currentEvent ? `${addTrailingZeroIfNeed(currentEvent.startDateTime.getHours())}:${addTrailingZeroIfNeed(currentEvent.startDateTime.getMinutes())}` : "");
    const [endTime, setEndTime] = useState<string>(currentEvent ? `${addTrailingZeroIfNeed(currentEvent.endDateTime.getHours())}:${addTrailingZeroIfNeed(currentEvent.endDateTime.getMinutes())}` : "");
    const [name, setName] = useState<string>(currentEvent?.title ?? "");
    const [showRepeatInterval, setShowRepeatInterval] = useState(false);
    const [repeatPeriod, setRepeatPeriod] = useState<string>("Дней");
    const periodRepeatItems = ['Дней', 'Недель', 'Месяцев'];

    function reset() {
        setDate("");
        setName("");
        setStartTime("");
        setEndTime("");
    }

    return (
        <Modal onClose={props.onClose} style={{display: "block"}}>
            <Modal.Header>{
                currentEvent ? "О событии" : "Новое событие"
            }
            </Modal.Header>
            <Modal.Body>
                <p>Заголовок:</p>
                <InputView validationMessages={validateEmpty(name)} value={name} onValueChange={setName}/>
                <p>Дата события:</p>
                <InputView isDate={true} validationMessages={validateEmpty(date)} value={date} onValueChange={setDate}/>
                <p>Начало события:</p>
                <InputView validationMessages={validateStartDate()} isTime={true} value={startTime} onValueChange={setStartTime}/>
                <p>Конец события:</p>
                <InputView validationMessages={validateEndDate()} isTime={true} value={endTime} onValueChange={setEndTime}/>
                <p>
                    <Toggle
                        onValueChange={setShowRepeatInterval}
                    >
                        Добавить интервал повторений
                    </Toggle>
                </p>
                {showRepeatInterval
                    &&
                    <>
                        Повторять с интервалом <Input width={30}/> <Select items={periodRepeatItems} value={repeatPeriod} onValueChange={setRepeatPeriod}/>
                    </>}
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={() => {
                    if (validateStartDate().length > 0
                        || validateEndDate().length > 0
                        || validateEmpty(name).length > 0
                        || validateEmpty(date).length > 0)
                        return;
                    const splitDate = date.split(".");
                    const splitStartTime = startTime.split(":");
                    const splitEndTime = endTime.split(":");
                    props.onSave({
                        title: name,
                        startDateTime: new Date(Number(splitDate[2]), Number(splitDate[1]) - 1, Number(splitDate[0]), Number(splitStartTime[0]), Number(splitStartTime[1])),
                        endDateTime: new Date(Number(splitDate[2]), Number(splitDate[1]) - 1, Number(splitDate[0]), Number(splitEndTime[0]), Number(splitEndTime[1])),
                        id: props.currentEvent?.id ?? crypto.randomUUID().toString(),
                        repeat: props.currentEvent?.repeat ?? null
                    });
                    reset();
                }}>{currentEvent ? "Сохранить" : "Создать"}</Button>
                {currentEvent &&
                    <Button onClick={props.onDelete} style={{marginLeft: "60px"}} use={"danger"}>
                    Удалить событие
                    </Button>
                }
            </Modal.Footer>
        </Modal>
    );
}