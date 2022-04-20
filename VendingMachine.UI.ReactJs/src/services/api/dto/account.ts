import { IBaseResponse } from "./common";

export interface ILoginRequest {
    email: string;
    password: string;
}

export interface ILoginResponse extends IBaseResponse {
    token: string;
    expiresIn: number;
}


export interface IRegisterRequest {
    email: string;
    password: string;
}

export interface IRegisterResponse extends IBaseResponse {
    token: string;
    expiresIn: number;
}
