import React, { useState } from "react";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAt, faKey } from "@fortawesome/free-solid-svg-icons";
import myApi from "../myApi";

import "./RegisterStyle.scss";

type Props = {
	setLoggedInTrue: () => void;
	setRegisterModeFalse: () => void;
};

const Register = ({ setLoggedInTrue, setRegisterModeFalse }: Props) => {
	//#region State definitions
	const [feedback, setFeedback] = useState(<div></div>);
	const [username, setUsername] = useState("");
	const [password, setPassword] = useState("");
	//#endregion

	const CREATEDSTATUSCODE = 201;

	//#region Handler Methods
	const Register = async (evt: any) => {
		evt.preventDefault();
		try {
			var response = myApi.post("/user/register", {
				username: username,
				password: password,
			});
			setPassword("");
			setUsername("");
			if ((await response).status === CREATEDSTATUSCODE) {
				setRegisterModeFalse();
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
		<div className="RegisteringDiv">
			{feedback}
			<div className="RegisteringHeader">
				<h1>Register</h1>
			</div>
			<div className="RegisteringForm">
				<form onSubmit={Register}>
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
					<div className="RegisteringButton">
						<button>Register</button>
					</div>
				</form>
				<div onClick={setRegisterModeFalse}>Login</div>
			</div>
		</div>
	);
};

export default Register;
