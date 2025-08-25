import { Routes } from '@angular/router';
import { NotesComponent } from './features/notes/notes.component';
import { ConceptsComponent } from './features/concepts/concepts.component';

export const routes: Routes = [
  { path: 'notes', component: NotesComponent },
  { path: 'concepts', component: ConceptsComponent },
  { path: '**', redirectTo: 'notes', pathMatch: 'full' },
];
