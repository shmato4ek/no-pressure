import { Token } from "@angular/compiler";
import { UserDTO } from "./user-dto";

export interface AuthUser {
    user: UserDTO;
    token: Token;
}