import axios, {AxiosResponse} from "axios";

export namespace AuthenticationApi {

    export interface AuthenticateUserRequest {
        email: string,
        password: string
    }

    interface AuthenticationUserResponse{
        authToken?: string,
        authorizationResult: AuthenticationResult
    }

    interface RegisterUserResponse{
        registrationResult: RegistrationResult,
        authToken?: string
    }

    export enum AuthenticationResult{
        UserNotFound,
        WrongPassword,
        Success
    }

    export enum RegistrationResult{
        AlreadyExist,
        Success
    }

    export function authenticate(request: AuthenticateUserRequest): Promise<AxiosResponse<AuthenticationUserResponse, any>> {
        return axios.get("http://localhost:5031/Authentication/Authenticate", { params: request});
    }

    export function register(request: AuthenticateUserRequest): Promise<AxiosResponse<RegisterUserResponse, any>> {
        return axios.post("http://localhost:5031/Authentication/Register", {email: request.email, password: request.password});
    }

    export function isAuthenticated(): Promise<AxiosResponse<boolean, any>> {
        return axios.get("http://localhost:5031/Authentication/IsAuthenticated", { withCredentials: true });
    }
}