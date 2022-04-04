import { IBaseResponse } from "./common";

export interface ILoginRequest {
    email: string;
    password: string;
}

export interface ILoginResponse extends IBaseResponse {
    token: string;
    expiresIn: number;
}
