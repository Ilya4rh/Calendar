import axios, {AxiosResponse} from "axios";

export namespace EventApi{

    export enum IntervalTypes{
        Day,
        Week,
        Month,
        Year
    }

    export interface EventDto {
        id: string,
        title: string,
        startDateTime: Date,
        endDateTime: Date,
        repeat: RepeatDto | null
    }

    export interface RepeatDto{
        id: string,
        dateStart: Date,
        dateEnd: Date | null,
        interval: number | null,
        intervalType: IntervalTypes
    }

    export function getEventsByCreatorIdForYear(): Promise<AxiosResponse<EventDto[]>> {
        return axios.get("http://localhost:5031/Event/GetEventsForYear", { withCredentials: true });
    }

    export function createEvent(request: EventDto): Promise<AxiosResponse<EventDto[], EventDto>> {
        return axios.post("http://localhost:5031/Event/CreateEvent", request, { withCredentials: true });
    }

    export function changeEvent(request: EventDto): Promise<AxiosResponse<EventDto[], EventDto>> {
        return axios.put("http://localhost:5031/Event/ChangeEvent", request, { withCredentials: true });
    }

    export function deleteEvent(request: string): Promise<AxiosResponse<boolean, string>> {
        return axios.delete("http://localhost:5031/Event/DeleteEvent", { params: request, withCredentials: true });
    }
}