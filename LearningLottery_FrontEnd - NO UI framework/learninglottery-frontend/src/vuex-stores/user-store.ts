// store.ts
import { getUsers, updateUser, deleteUser, axiosAddUser } from '@/js/api';
import { errorNotify, successNotify } from '@/js/notify';
import { type User } from '@/models/user-model';
import { type InjectionKey } from 'vue';
import { Store, createStore } from 'vuex';


export interface UserState {
  userList: User[];

}

export default createStore<UserState>({
  state: {
    userList: [] as User[],
  },
  mutations: {
    setUserList(state: { userList: any; }, userList: any) {
      state.userList = userList;
    },
    addUser(state: { userList: any[]; }, user: any) {
      state.userList.push(user);
    },
    deleteUser(state: { userList: any[]; }, index: any) {
      state.userList.splice(index, 1);
    },
    updateUser(state: { userList: { [x: string]: any; }; }, { index, user }: any) {
      state.userList[index] = user;
    },
  },
  actions: {
    async addUser({ commit, dispatch }, name: string | undefined) {
      if (name == null || name.replace(/\s/g, '') === '') return;
      const res = await axiosAddUser({ name: name });
      if (res == true) {
        dispatch('updateUserList');
        successNotify();
      } else {
        errorNotify();
      }
    },
    async deleteUserByIndex({ commit }, index: number) {
      const res = await deleteUser({ uid: this.state.userList[index].id });
      if (res == true) {
        commit('deleteUser', index);
        successNotify();
      } else {
        errorNotify();
      }
    },
    async updateUserList({ commit }) {
      const users = await getUsers();
      commit('setUserList', users);
    },
    async updateUserData({ commit}, { index, oldData }) {
      if (oldData == null) return;
      const user: User = await this.state.userList[index];
      const res = await updateUser(user);

      if (res == false) {
        commit('updateUser', { index, user: oldData });
        errorNotify();
      } else {
        successNotify();
      }
    },
  },
});

export const userStateKey: InjectionKey<Store<UserState>> = Symbol();