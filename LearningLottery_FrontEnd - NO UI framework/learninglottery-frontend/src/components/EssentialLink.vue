<template>
  <q-item clickable @click="$router.replace({ name: routeName })">
    <q-item-section v-if="icon" avatar>
      <q-icon :name="icon" />
    </q-item-section>

    <q-item-section>
      <q-item-label>{{ label }}</q-item-label>
      <q-item-label caption>{{ caption }}</q-item-label>
    </q-item-section>
  </q-item>
</template>

<script setup lang="ts">
export interface EssentialLinkProps {
  label: string;
  caption: string;
  routeName: string;
  icon: string;
}
withDefaults(defineProps<EssentialLinkProps>(), {
  label: '',
  caption: '',
  routeName: '#',
  icon: '',
});
</script>
