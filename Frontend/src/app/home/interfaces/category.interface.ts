export interface Category {
    title: string;
    subtitle: string;
    icon: string;
    
    /** Signal o computed que devuelve el total */
    total: () => number;
    
    /** Ruta a la que navegar al hacer click */
    route: string;
  }