interface User {
  id: number;
  name: string;
  addTime: Date;
  paired: boolean | undefined | null;
}

interface GetUsersResponse {
  data: User[];
}

export type { User, GetUsersResponse };
