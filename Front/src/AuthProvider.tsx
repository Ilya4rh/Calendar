import {useContext, createContext, ReactNode, useState, useEffect} from "react";
import {UsersApi} from "./Apis/UsersApi";
import {Navigate} from "react-router-dom";
import {Spinner} from "@skbkontur/react-ui";

interface IAuthContext{
    authenticated: boolean
}

const initialContext = {
    authenticated: false
}
const AuthContext = createContext<IAuthContext>(initialContext);

interface AuthProviderProps{
    component: ReactNode
}
function AuthProvider(props: AuthProviderProps) {
    const [authenticated, setAuthenticated] = useState<boolean | undefined>(undefined);
    const onAuth = (x: boolean) => {
        setAuthenticated(x);
    }

    useEffect(() => {
        UsersApi.isAuthorized()
            .then((x) => onAuth(x.data))
            .catch((err) => console.log(err));
    }, [])

    if (authenticated === undefined)
        return <Spinner type={"big"}></Spinner>;

    if (authenticated == false)
        return <Navigate to={"/"}/>;

    if (authenticated === true)
        return <AuthContext.Provider value={{authenticated}}>{props.component}</AuthContext.Provider>;
    else
        return <></>;
}

export default AuthProvider;

export const useAuth = () => {
    return useContext(AuthContext);
};