import { BaseResult } from './baseResult';

export class TokenResult extends BaseResult {
    token: string;
    expiresIn: number;
}
