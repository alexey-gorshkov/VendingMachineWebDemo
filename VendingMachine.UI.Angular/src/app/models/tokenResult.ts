import { BaseResult } from './baseResult';

export interface TokenResult extends BaseResult {
    token: string;
    expiresIn: number;
}
