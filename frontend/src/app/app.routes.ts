import { Routes } from '@angular/router';
import { ConceptsComponent } from './concepts/concepts.component';
import { ConceptEditComponent } from './concept-edit/concept-edit.component';
import { RoadmapComponent } from './roadmap/roadmap.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'concepts' },
  { path: 'concepts', component: ConceptsComponent },
  { path: 'concepts/new', component: ConceptsComponent },
  { path: 'concepts/:id', component: ConceptEditComponent },
  { path: 'roadmap', component: RoadmapComponent },
  { path: '**', redirectTo: 'concepts' },
];
