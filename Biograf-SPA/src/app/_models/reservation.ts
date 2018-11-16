import { Show } from './show';
import { User } from './user';

export interface Reservation {
  id: number;
  created: Date;
  row: number;
  seat: number;
  bookingState: number;
  show: Show;
  user: User;
}
