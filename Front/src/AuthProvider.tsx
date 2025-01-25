import {useContext, createContext, ReactNode, useState, useEffect, Children} from "react";
import {AuthenticationApi} from "./Apis/AuthenticationApi";
import {Navigate} from "react-router-dom";
import {Spinner} from "@skbkontur/react-ui";
import {Center} from "@skbkontur/react-ui";

interface IAuthContext{
    authenticated: boolean
}

const initialContext = {
    authenticated: false
}
const AuthContext = createContext<IAuthContext>(initialContext);

interface AuthProviderProps{
    children: ReactNode;
}
function AuthProvider(props: AuthProviderProps) {
    const [authenticated, setAuthenticated] = useState<boolean | undefined>(undefined);
    const onAuth = (x: boolean) => {
        setAuthenticated(x);
    }

    useEffect(() => {
        AuthenticationApi.isAuthenticated()
            .then((x) => onAuth(x.data))
            .catch((err) => console.log(err));
    }, [])

    if (authenticated === undefined)
        return <Center><Spinner type={"big"}></Spinner></Center>;

    if (authenticated == false)
        return <Navigate to={"/"}/>;

    if (authenticated === true)
        return<AuthContext.Provider value={{authenticated}}>{props.children}</AuthContext.Provider>;
    else
        return <></>;
}

export default AuthProvider;

export const useAuth = () => {
    return useContext(AuthContext);
};