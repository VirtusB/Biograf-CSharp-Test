import { Movie } from './movie';


export interface Show {
    id: number;
    startDate: Date;
    endDate: Date;
    ticketPrice: number;
    hallNumber: number;
    movie: Movie;
}



