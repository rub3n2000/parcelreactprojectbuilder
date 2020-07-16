import React, { useState } from "react";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAt, faKey } from "@fortawesome/free-solid-svg-icons";
import myApi from "../myApi";

import "./Login.scss";

type Props = {
	setLoggedInTrue: () => void;
	setRegisterModeTrue: () => void;
};

const Login = ({ setLoggedInTrue, setRegisterModeTrue }: Props) => {
	//#region State definitions
	const [feedback, setFeedback] = useState(<div></div>);
	const [username, setUsername] = useState("");
	const [password, setPassword] = useState("");
	//#endregion

	const OKSTATUSCODE = 200;

	//#region Handler Methods
	const login = async (evt: any) => {
		evt.preventDefault();
		try {
			var response = myApi.post("/user/login", {
				username: username,
				password: password,
			});
			setPassword("");
			setUsername("");
			if ((await response).status === OKSTATUSCODE) {
				setLoggedInTrue();
				setFeedback(<div className="Success">Welcome!</div>);
			} else {
				setFeedback(
					<div className="Failure">
						{(await response).data.message}
					</div>
				);
			}
		} catch {
			setFeedback(
				<div className="Failure">
					Something went wrong! Wrong password?
				</div>
			);
		}
	};

	const usernameChangeHandler = (evt: any) => {
		setUsername(evt.target.value);
	};

	const passwordChangeHandler = (evt: any) => {
		setPassword(evt.target.value);
	};
	//#endregion

	return (
		<div className="LoginDiv">
			{feedback}
			<div className="LoginHeader">
				<h1>Login</h1>
			</div>
			<div className="LoginForm">
				<form onSubmit={login}>
					<label>
						<FontAwesomeIcon icon={faAt} /> Username
						<input
							onChange={usernameChangeHandler}
							value={username}
							required
							placeholder="username"
							type="text"
						></input>
					</label>
					<label>
						<FontAwesomeIcon icon={faKey} /> Password
						<input
							onChange={passwordChangeHandler}
							value={password}
							required
							placeholder="password"
							type="password"
						></input>
					</label>
					<div className="LoginButton">
						<button>login</button>
					</div>
				</form>
				<div onClick={setRegisterModeTrue}>Register</div>
			</div>
		</div>
	);
};

export default Login;
