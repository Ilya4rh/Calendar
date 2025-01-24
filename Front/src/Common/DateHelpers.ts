export namespace DateHelpers{
    export function getWeekDayIndex(date: Date){
        const day = date.getDay();
        if (day == 0)
            return 6;
        return day - 1;
    }

    export function addDays(date: Date, days: number) {
        const result = new Date(date);
        result.setDate(result.getDate() + days);
        return result;
    }

    export function getWeekFirstDay(date: Date) {
        date = new Date(date);
        const day = date.getDay(),
            diff = date.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
        return new Date(date.setDate(diff));
    }
}