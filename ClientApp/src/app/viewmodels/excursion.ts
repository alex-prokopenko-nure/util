import { Sight } from './sight';
import { Tour } from './tour';
import { ExcursionSight } from './excursionsight';

export class Excursion {
  id: string;
  name: string;
  tours: Tour[];
  excursionSights: ExcursionSight[]
}
