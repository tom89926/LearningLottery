<template>
  <div style="width: 90%; margin: 0 auto">
    <div style="margin-top: 40px">
      <div style="margin-bottom: 20px">
        <input v-model="addInputValue" />
        <button
          style="margin-left: 20px"
          @click="
            addUser();
          "
        >
          Add
        </button>
      </div>
      <div>
        <new-table
          v-bind:table-title="'User List'"
          v-bind:columns="columnsNewTable"
          v-bind:rows="userStore.userList"
          v-bind:show-table-order="true"
          @item-edit-on-save="
            (rowIndex, rowData) =>
            useUserStore.dispatch('updateUserData', { index: rowIndex, oldData: rowData.old })
              
          "
          @item-edit-on-cancel="
            (rowIndex, rowData) => (userStore.userList[rowIndex] = rowData.old)
          "
          ><template v-slot:actions="{ rowInfo }">
            <div style="text-align: center">
              <button
                :disable="deleteLoading"
                @click="deleteHandler(rowInfo.rowIndex)"
              >
                Delete
              </button>
            </div>
          </template>
        </new-table>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import useUserStore from '@/vuex-stores/user-store';
// import { useUserStore } from '@/stores/user-store';
import { ref, watch, watchEffect } from 'vue';

import NewTable, {
  type Column as NewTableColumn,
} from '@/components/newTable/NewTable.vue';
import { TextAlign } from '@/enum/text-align';

//add input
const addInputValue = ref<string>('');

//user data
const userStore = useUserStore.state;

const columnsNewTable: NewTableColumn[] = [
  { name: 'id', align: TextAlign.Center, label: 'UID', isCanEdit: false },
  {
    name: 'name',
    align: TextAlign.Center,
    label: 'Name',
    isCanEdit: true,
  },
  {
    name: 'addTime',
    align: TextAlign.Center,
    label: 'Add Time',
    isCanEdit: false,
  },
];


const deleteLoading = ref<boolean>(false);
const deleteLoadingSwitch = () => {
  deleteLoading.value = !deleteLoading.value;
};

const addUser = () => {
  useUserStore.dispatch('addUser', addInputValue.value);
  addInputValue.value = '';
};

async function deleteHandler(rowIndex: number): Promise<void> {
  deleteLoadingSwitch();
  try {
    await useUserStore.dispatch('deleteUserByIndex', rowIndex);
  } catch (e: unknown) {
    console.error(e);
  } finally {
    deleteLoadingSwitch();
  }
}

// watch( [deleteLoading, addInputValue]
//   , 
//   ([nValue1, nValue2],[oValue1, oValue2]) => {
//   console.log(nValue1, oValue1, nValue2, oValue2)
// }, { immediate: true })

watchEffect(() => {
  if(!deleteLoading.value) return;
  console.log(deleteLoading.value);
})


</script>
<style>
table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td,
th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}
</style>
