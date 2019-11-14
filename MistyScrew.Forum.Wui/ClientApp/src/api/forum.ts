export interface Area {
    name: string;
    boards: Board[];
}

export interface Board {
    name: string;
    title?: string;
    description?: string;
}