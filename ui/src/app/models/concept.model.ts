export interface Concept {
  id: number;
  name: string;
  description: string;
  dependencies: Dependency[];
}

export interface Dependency {
  id: number;
  level: number;
}
