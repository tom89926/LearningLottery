import { defineStore } from 'pinia';
import { getUsers, updateUser, deleteUser, axiosAddUser } from 'src/js/api';
import { errorNotify, successNotify } from 'src/js/notify';
import { User } from 'src/models/user-model';
import randomInt from 'src/js/random-int';
import { computed, ref } from 'vue';
export const useUserStore = defineStore('userState', () => {
  const userList = ref<User[]>([]);

  // const getUserList = computed(() => userList);

  async function AddUser(name: string | undefined) {
    if (name == null || name.replace(/\s/g, '') === '') return;
    const res = await axiosAddUser({ name: name });
    if (res == true) {
      await updateUserList();
      successNotify();
      return;
    }
    errorNotify();
  }

  async function deleteUserByIndex(index: number) {
    const user: User = userList.value[index];
    const res = await deleteUser({ uid: user.id });
    if (res == true) {
      userList.value.splice(index, 1);
      successNotify();
      return;
    }
    errorNotify();
  }

  async function updateUserList() {
    const users = await getUsers();
    userList.value = users;
  }

  async function updateUserData(index: number, oldData: User | undefined) {
    if (oldData == null) return;
    const user: User = await userList.value[index];
    const res = await updateUser(user);

    if (res == false) {
      userList.value[index] = oldData;
      errorNotify();
      return;
    }
    successNotify();
  }

  return {
    updateUserData,
    updateUserList,
    deleteUserByIndex,
    AddUser,
    userList,
  };
});
