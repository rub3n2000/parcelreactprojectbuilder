import axios from "../myApi";
import jwt_decode from "jwt-decode";

import type { userFilter, User } from "./Types";

const OKSTATUSCODE = 200;

const AuthenticationService = {
	login: async function (data: any) {
		try {
			let theResponse = { status: 401 };
			let response = await axios.post("/auth/login", data);
			if (response.status === OKSTATUSCODE && response.data.token) {
				let jwt = response.data.token;
				theResponse.status = response.status;

				localStorage.setItem("access_token", jwt);
			}

			if (theResponse && theResponse.status === OKSTATUSCODE) {
				return true;
			} else {
				return false;
			}
		} catch {
			return false;
		}
	},
	register: async function (data: any) {
		try {
			let response = await axios({
				method: "post",
				url: "/auth/register",
				data: data,
				responseType: "json",
			});
			return response;
		} catch {
			return undefined;
		}
	},
	logout: async function () {
		localStorage.removeItem("access_token");
	},
	fetchCurrentUser: async function () {
		try {
			let response = await axios.get<User>(
				"/users/" +
					(
						jwt_decode(
							localStorage.getItem("access_token") as string
						) as any
					).sub,
				{
					headers: {
						Authorization: `Bearer ${localStorage.getItem(
							"access_token"
						)}`,
					},
				}
			);
			if (response.status == OKSTATUSCODE) {
				return response.data;
			}
			return undefined;
		} catch {
			return undefined;
		}
	},
};

export default AuthenticationService;
