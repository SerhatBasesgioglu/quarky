import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Concept } from '@models/concept.model';

@Component({
  selector: 'app-concepts',
  imports: [CommonModule],
  templateUrl: './concepts.component.html',
  styleUrl: './concepts.component.css',
})
export class ConceptsComponent {
  private apiUrl = 'http://localhost:8080/api/concepts';
  private tempId = -1;

  concepts: Concept[] = [];
  selectedConcept: Concept | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.http.get<Concept[]>(this.apiUrl).subscribe(concepts => {
      this.concepts = concepts.map(c => ({ ...c, hasChanged: false }));
    });
  }

  createTemplate() {
    const draft: Concept = {
      id: this.tempId--,
      name: 'New Concept',
      description: '',
    };

    this.concepts.push(draft);
    this.selectedConcept = draft;
  }
}
