import lotteryAxios from '@/js/lotteryAxios';
import { type User } from '@/models/user-model';

export async function getUsers(): Promise<User[]> {
  try {
    const res = await lotteryAxios.get<User[]>(
      '/api/UserAccess/Get/GetAllUser'
    );
    const users = res.data.map((user) => ({
      ...user,
      paired: false,
    }));
    return users;
  } catch (error) {
    console.error(error);
    throw error;
  }
}

export async function getUserByUid(uid: number): Promise<User[]> {
  try {
    const res = await lotteryAxios.get<User[]>(
      '/api/UserAccess/Get/GetUserById',
      {
        params: { id: uid },
      }
    );
    return res.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
}

interface apiUser {
  id: number;
  name: string;
}

export async function updateUser(user: User): Promise<boolean> {
  try {
    const apiUser: apiUser = {
      id: user.id,
      name: user.name,
    };
    await lotteryAxios.patch<string>(
      '/api/UserAccess/Patch/UpdateUser',
      apiUser
    );
    return true;
  } catch (error) {
    console.error(error);
    return false;
  }
}

export async function deleteUser({ uid }: { uid: number }): Promise<boolean> {
  try {
    await lotteryAxios.delete<string>('/api/UserAccess/Delete/DeleteUserById', {
      params: { id: uid },
    });
    return true;
  } catch (error) {
    console.error(error);
    return false;
  }
}

export async function axiosAddUser({
  name,
}: {
  name: string;
}): Promise<boolean> {
  try {
    const apiUser: apiUser = {
      id: 0,
      name: name,
    };
    await lotteryAxios.post<string>('/api/UserAccess/Post/AddUser', apiUser);
    return true;
  } catch (error) {
    console.error(error);
    return false;
  }
}
