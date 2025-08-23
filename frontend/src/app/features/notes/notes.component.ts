import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Note } from '../../shared/models/note.model';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';

@Component({
  selector: 'app-notes',
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    InputTextModule,
    TextareaModule,
  ],
  templateUrl: './notes.component.html',
  styleUrl: './notes.component.css',
})
export class NotesComponent {
  private apiUrl = 'http://localhost:8080/api/notes';
  private tempId = -1;

  notes: Note[] = [];
  selectedNote: Note | null = null;

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.http.get<Note[]>(this.apiUrl).subscribe(notes => {
      this.notes = notes.map(n => ({ ...n, hasChanged: false }));
    });
  }
  save() {
    console.log(this.notes);
    const updatedNotes = this.notes.filter(n => {
      return n.id > 0 && n.hasChanged;
    });
    updatedNotes.forEach(note => {
      this.http.put<void>(`${this.apiUrl}/${note.id}`, note).subscribe(() => {
        note.hasChanged = false;
      });
    });

    const newNotes = this.notes.filter(n => n.id < 0 && n.hasChanged);
    console.log(newNotes);
    newNotes.forEach(note => {
      this.http.post<void>(this.apiUrl, note).subscribe(() => {
        note.hasChanged = false;
      });
    });
  }

  delete(id: number) {
    if (id < 0) {
      console.log(id);
      this.notes = this.notes.filter(note => note.id !== id);
    } else {
      this.http.delete<void>(`${this.apiUrl}/${id}`).subscribe(() => {
        this.notes = this.notes.filter(note => note.id !== id);
      });
    }
  }

  createTemplate() {
    const draft: Note = {
      id: this.tempId--,
      title: 'New Note',
      content: '',
      hasChanged: true,
    };
    this.notes.push(draft);
    this.selectedNote = draft;
    console.log(this.notes);
  }

  updateTemplate() {}

  select(id: number) {
    this.selectedNote = this.notes.find(n => n.id === id) || null;
  }
}
