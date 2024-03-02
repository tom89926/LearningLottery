<!-- <script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
import { useStore } from 'vuex';
import {key, type Context } from '@/vuex-stores/test-store';
const store = useStore(key);
console.log(store.getters.doneTodos);
const increment = () => {
  store.commit('increment', 5); // 將數值 10 作為 payload 傳遞給 mutations
  console.log('Updated count:', store.state.count); // 印出更新後的 count 值
};
increment();
</script>

<template>
  
  <RouterView />
</template>

<style scoped>
</style> -->
<template>
  <router-view />
</template>

<script setup lang="ts">

</script>
