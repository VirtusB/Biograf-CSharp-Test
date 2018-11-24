import { Role } from './role';

export interface User {
    id: number;
    username: string;
    lastActive: Date;
    created: Date;
    city: string;
    country: string;
    enabled: boolean;
    phoneNumber: number;
    email: string;
    role: Role;
    name: string;
    lifetimeSavedAmount: number;
}
