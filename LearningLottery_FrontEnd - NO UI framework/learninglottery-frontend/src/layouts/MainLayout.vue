<template>
  <div style="display: flex">
    <div style="width: 10%; padding: 2% 0 0 1%">
      <template v-for="essentialLink in essentialLinks" :key="essentialLink">
        <router-link :to="{ name: essentialLink.routeName }">
          {{ essentialLink.caption }}</router-link
        ><br />
      </template>
    </div>
    <div style="width: 90%"><router-view /></div>
  </div>
  <!-- <q-layout view="fHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
        />

        <q-toolbar-title> Lottery </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen" show-if-above bordered>
      <q-list>
        <EssentialLink
          v-for="link in essentialLinks"
          :key="link.label"
          v-bind="link"
        />
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout> -->
</template>

<script setup lang="ts">
import { ref } from 'vue';
import EssentialLink, {
  EssentialLinkProps,
} from 'components/EssentialLink.vue';

const essentialLinks: EssentialLinkProps[] = [
  {
    label: 'Home',
    caption: 'User List',
    routeName: 'home',
    icon: 'home',
  },
  {
    label: 'Lottery',
    caption: 'Draw straws',
    routeName: 'lottery',
    icon: 'home',
  },
];

const leftDrawerOpen = ref(false);

function toggleLeftDrawer() {
  leftDrawerOpen.value = !leftDrawerOpen.value;
}
</script>
