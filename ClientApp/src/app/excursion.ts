import { Sight } from './sight';
import { Tour } from './tour';
import { ExcursionSight } from './excursionsight';

export class Excursion {
  id: number;
  name: string;
  tours: Tour[];
  excursionSights: ExcursionSight[]
}
