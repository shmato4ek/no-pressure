import { Statistic } from "../statistic/statistic";
import { UserDTO } from "./user-dto";

export interface UserShared {
    user: UserDTO;
    statistic: Statistic;
    isFollowed: boolean;
}