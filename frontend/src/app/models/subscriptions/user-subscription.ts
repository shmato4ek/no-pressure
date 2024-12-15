import { UserDTO } from "../user/user-dto";

export interface UserSubscription {
    user: UserDTO;
    date: string;
}