import { User } from "./user";
import { Movie } from "./movie";


export interface Favorite {
    likerId: number;
    likeeId: number;
    liker?: User;
    likee?: Movie;
}

