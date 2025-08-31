import { Component, ElementRef, ViewChild } from '@angular/core';
import { DataSet, Network, Node, Edge } from 'vis-network/standalone';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Concept } from '@models/concept.model';

@Component({
  selector: 'app-concepts',
  imports: [CommonModule],
  templateUrl: './concepts.component.html',
  styleUrl: './concepts.component.css',
})
export class ConceptsComponent {
  @ViewChild('network', { static: true }) networkEl!: ElementRef;
  concepts: Concept[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<Concept[]>('data.json').subscribe((concepts: Concept[]) => {
      const nodes = new DataSet<Node>(
        concepts.map(c => ({
          id: c.id,
          label: c.name,
        }))
      );
      const edgesRaw = concepts.flatMap(c =>
        c.dependencies.map(dep => ({
          from: c.id,
          to: dep.id,
          arrows: 'from',
        }))
      );
      console.log(edgesRaw);
      const edges = new DataSet<Edge>(edgesRaw);
      const data = { nodes, edges };
      const options = {
        nodes: {
          shape: 'dot',
          size: 40, // bigger dots
          font: {
            size: 40, // label font size
            color: '#333',
          },
          borderWidth: 2,
        },
        physics: {
          solver: 'repulsion',
          repulsion: {
            nodeDistance: 250, // increase distance between nodes
            springLength: 200, // edge length
            springConstant: 0.01, // smaller → looser
          },
        },
      };
      new Network(this.networkEl.nativeElement, data, options);
    });
  }

  findConceptName(id: number) {
    const concept = this.concepts.find(c => c.id === id);
    return concept ? concept.name : 'Unknown';
  }
}
