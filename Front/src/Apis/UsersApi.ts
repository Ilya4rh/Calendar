import axios, {AxiosResponse} from "axios";

export namespace UsersApi {

    export interface AuthorizeUserRequest{
        email: string,
        password: string
    }

    interface AuthorizeUserResponse{
        authToken?: string,
        authorizationResult: AuthorizationResult
    }

    interface RegisterUserResponse{
        registrationResult: RegistrationResult,
        authToken?: string
    }

    export enum AuthorizationResult{
        UserNotFound,
        WrongPassword,
        Success
    }

    export enum RegistrationResult{
        AlreadyExist,
        Success
    }

    export function authorize(request: AuthorizeUserRequest): Promise<AxiosResponse<AuthorizeUserResponse, any>> {
        return axios.get("http://localhost:5031/Users/Authorize", { params: request});
    }

    export function register(request: AuthorizeUserRequest): Promise<AxiosResponse<RegisterUserResponse, any>> {
        return axios.post("http://localhost:5031/Users/Register", {email: request.email, password: request.password});
    }

    export function isAuthorized(): Promise<AxiosResponse<boolean, any>> {
        return axios.get("http://localhost:5031/Users/IsAuthorized", { withCredentials: true });
    }
}