export interface Category {
    id: number;
    title: string;
    subtitle: string;
    icon: string;
    route: string;
    
    total: () => number;
  }