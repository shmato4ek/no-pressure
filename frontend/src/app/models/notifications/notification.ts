export interface Notification {
    id: number;
    title: string;
    text: string;
    date: string;
    link?: string;
    isRead: boolean;
}