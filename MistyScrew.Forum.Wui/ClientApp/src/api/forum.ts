export interface Area {
  name: string;
  boards: Board[];
}

export interface Board {
  name: string;
  title?: string;
  description?: string;
  moderators?: User_Name[];
  state?: BoardState;
}

export interface Board_Name {
  name: string;
}

interface BoardState {
  isFlashed?: boolean;
  threadCount?: number;
  postCount?: number;
  lastPost?: BoardLastPost;
}

interface BoardLastPost {
  id: string;
  user?: User_Name;
  time?: string;
}

interface User_Name {
  name: string;
}

export interface Thread {
  id: string;
  title?: string;
}