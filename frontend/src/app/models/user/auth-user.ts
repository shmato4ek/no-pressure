import { Token } from "../token/token";
import { UserDTO } from "./user-dto";

export interface AuthUser {
    user: UserDTO;
    token: Token;
}