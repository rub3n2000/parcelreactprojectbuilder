import React, { useState, useEffect } from "react";
import "./App.scss";
import Login from "./Login/Login";
import myApi from "./myApi";
import Register from "./Register/Register";
import AuthenticationService from "./helpers/AuthenticationService";
import jwt_decode from "jwt-decode";

const App = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [loggedinUser, setLoggedInUser] = useState<null | undefined | {}>(
        null
    );
    const [registerMode, setRegisterMode] = useState<boolean>(false);

    const setLoggedInTrue = () => {
        setIsLoggedIn(true);
    };
    const setRegisterModeFalse = () => {
        setRegisterMode(false);
    };
    const setRegisterModeTrue = () => {
        setRegisterMode(true);
    };

    const OKSTATUSCODE = 200;
    const INDEXTIMEZONESTARTSAT = 10;

    const getLoggedInUser = async () => {
        let response = await AuthenticationService.fetchCurrentUser();
        if (response !== undefined) {
            setLoggedInUser(response);
            setIsLoggedIn(true);
        } else {
            setIsLoggedIn(false);
            setLoggedInUser(null);
        }
    };

    if (localStorage.getItem("access_token") !== null) {
        if (
            (jwt_decode(localStorage.getItem("access_token") as string) as any)
                .exp <
            new Date().getTime().toString().substr(0, INDEXTIMEZONESTARTSAT)
        ) {
            localStorage.removeItem("access_token");
        }
    }

    useEffect(() => {
        getLoggedInUser();
    }, [isLoggedIn]);

    if (isLoggedIn) {
        return (
            <div className="App">
                <div>App</div>
                <div
                    className="LogoutButton"
                    onClick={async () => {
                        await AuthenticationService.logout();
                        window.location.reload();
                    }}
                >
                    Logout
                </div>
            </div>
        );
    } else if (registerMode) {
        return (
            <Register
                setLoggedInTrue={setLoggedInTrue}
                setRegisterModeFalse={setRegisterModeFalse}
            />
        );
    } else {
        return (
            <Login
                setLoggedInTrue={setLoggedInTrue}
                setRegisterModeTrue={setRegisterModeTrue}
            />
        );
    }
};

export default App;
