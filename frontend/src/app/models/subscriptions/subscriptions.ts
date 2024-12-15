import { UserSubscription } from "./user-subscription";

export interface Subscriptions {
    followers: UserSubscription[];
    followings: UserSubscription[];
}