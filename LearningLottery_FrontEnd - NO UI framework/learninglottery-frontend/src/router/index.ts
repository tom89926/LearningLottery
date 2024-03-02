import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
// import HomeView from '../views/HomeView.vue'
import useUserStore from '@/vuex-stores/user-store';
const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('@/layouts/MainLayout.vue'),
    children: [
      { path: '', redirect: { name: 'home' } },
      {
        path: 'home',
        name: 'home',
        component: () => import('@/pages/HomePage.vue'),
      },
      {
        path: 'lottery',
        name: 'lottery',
        component: () => import('@/pages/LotteryPage.vue'),
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('@/pages/ErrorNotFound.vue'),
  },
];


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: routes
})

router.beforeEach(async (to, from) => {
  await useUserStore.dispatch("updateUserList");
});

export default router
