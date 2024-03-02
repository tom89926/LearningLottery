import { type InjectionKey } from 'vue';
import { createStore, Store, type GetterTree, type MutationTree, type ActionTree } from 'vuex';
// 定義 store 狀態的類型
export interface State {
  count: number;
}

// 定義 getters 函數的類型
export interface Getters {
  doneTodos(state: State): number;
}

// 定義 mutations 函數的類型
export interface Mutations {
  increment(state: State, payload: number): number; // 定義 mutations 的返回類型
}

// 定義 actions 函數的類型
export interface Actions {
  incrementAsync(context: Context): void;
}

// 定義 Context 類型，包含 state，getters，mutations 和 actions
export interface Context {
  state: State;
  getters: Getters;
  commit: (mutation: string, payload?: any) => void; // 定義 commit 函數
}

// 定義 store 狀態和 getters
const state: State = {
  count: 0,
};

const getters: GetterTree<State, State> & Getters = {
  doneTodos: (state) => state.count + 1000,
};

// 定義 mutations
const mutations: MutationTree<State> & Mutations = {
  increment: (state, payload) => {
    state.count += payload;
    return state.count; // 返回更新後的 count 值
  },
};

// 定義 actions
const actions: ActionTree<State, State> & Actions = {
  incrementAsync: ({ commit }) => {
    // 在這裡處理異步操作
    setTimeout(() => {
      commit('increment');
    }, 1000); // 1秒後執行 mutations 的 increment 函數
  },
};

// 定義 injection key
export const key: InjectionKey<Store<State>> = Symbol();

// 創建 store
export const store = createStore<State>({
  state: state,
  getters: getters,
  mutations: mutations,
  actions: actions,
});