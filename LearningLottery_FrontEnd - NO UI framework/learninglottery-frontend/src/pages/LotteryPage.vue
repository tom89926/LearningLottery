<template>
  <div style="width: 80%; margin: 0 auto">
    <!-- <q-table
      :rows-per-page-options="[10, 20, 30]"
      flat
      bordered
      title="Unpaired User List"
      :rows="unpairedRows"
      :columns="columns"
      row-key="name"
      binary-state-sort
    >
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td key="#" :props="props">
            {{ props.rowIndex + 1 }}
          </q-td>
          <q-td key="name" :props="props">
            {{ props.row.name }}
          </q-td>
          <q-td key="addTime" :props="props">
            {{ FormatDateTime({ dateTimeStr: props.row.addTime }) }}
          </q-td>
          <q-td key="action" :props="props">
            <q-btn
              :loading="deleteLoading"
              text-color="red"
              padding="xs"
              flat
              round
              icon="delete"
              @click="
                deleteLoading = true;
                userStore
                  .deleteUserByIndex(props.rowIndex)
                  .finally(() => (deleteLoading = false));
              "
            />
          </q-td>
        </q-tr>
      </template>
    </q-table> -->
    <!-- <q-table
      :rows-per-page-options="[10, 20, 30]"
      flat
      bordered
      title="Paired User List"
      :rows="pairedRows"
      :columns="columns"
      row-key="name"
      binary-state-sort
    >
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td key="#" :props="props">
            {{ props.rowIndex + 1 }}
          </q-td>
          <q-td key="name" :props="props">
            {{ props.row.name }}
          </q-td>
          <q-td key="addTime" :props="props">
            {{ FormatDateTime({ dateTimeStr: props.row.addTime }) }}
          </q-td>
        </q-tr>
      </template>
    </q-table> -->
    <div style="margin-top: 20px">
      <div style="margin-bottom: 40px">
        <h4 class="q-ma-none">Unpaired User List</h4>
        <table class="table" border="1">
          <thead>
            <tr>
              <th
                v-for="(column, index) in columns"
                :key="index + '_column_label'"
              >
                <div :class="`text-${column.align}`">
                  {{ column.label }}
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(row, rowIndex) in unpairedRows" :key="row.name">
              <td>
                <div style="text-align: center">{{ rowIndex + 1 }}</div>
              </td>
              <td>
                <div style="text-align: center">{{ row.name }}</div>
              </td>
              <td>
                <div style="text-align: center">
                  {{ FormatDateTime({ dateTimeStr: row.addTime.toString() }) }}
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div>
        <h4 style="margin: 0">Paired User List</h4>
        <table class="table" border="1">
          <thead>
            <tr>
              <th
                v-for="(column, index) in columns"
                :key="index + '_column_label'"
              >
                <div :class="`text-${column.align}`">
                  {{ column.label }}
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            <template v-for="(row, rowIndex) in pairedRows" :key="row.name">
              <tr>
                <td>
                  <div style="text-align: center">{{ rowIndex + 1 }}</div>
                </td>
                <td>
                  <div style="text-align: center">{{ row.name }}</div>
                </td>
                <td>
                  <div style="text-align: center">
                    {{
                      FormatDateTime({ dateTimeStr: row.addTime.toString() })
                    }}
                  </div>
                </td>
              </tr>
              <tr v-if="rowIndex % 2 == 1">
                <td></td>
              </tr>
            </template>
          </tbody>
        </table>
      </div>
      <div style="margin-top: 40px">
        <div style="text-align: center">
          <span v-for="(result, index) in results" v-bind:key="result.id">
            {{ index + 1 + '.  ' + result.name }}<br
          /></span>
        </div>
        <div style="text-align: center">
          <button @click="pair()">paired</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { type User } from '@/models/user-model';
// import { useUserStore } from '@/stores/user-store';
import useUserStore from '@/vuex-stores/user-store';
import { defineComponent, onMounted, ref } from 'vue';
import { FormatDateTime } from '@/js/format-date-time';
import randomInt from '@/js/random-int';


// defineComponent({
//   setup(props, ctx) {
//     const results = ref<User[]>();
//       const userStore = useUserStore.state
//     return {
//       results,
//       userStore
//     }
//   },
// })

const results = ref<User[]>();
//user data
const userStore = useUserStore.state

const pair = (): void => {
  const res = pairUser();
  if (res.length == 0) {
    return;
  }

  results.value = res as User[];
  unpairedRows.value = getUnpairedList() as User[];
  pairedRows.value = getPairedList() as User[];
};

function getUnpairedList() {
  return userStore.userList.filter((val) => val.paired == false);
}

function getPairedList() {
  return userStore.userList.filter((val) => val.paired == true);
}

function pairUser() {
  const unPairedList = getUnpairedList();
  if (unPairedList.length == 0) return [];
  if (unPairedList.length == 1) {
    const user: User = unPairedList[0];
    user.paired = true;
    const modal = {
      name: 'None',
    } as User;
    return [user, modal];
  }
  const random = randomInt({
    max: unPairedList.length - 1,
    min: 0,
    count: 2,
  });

  return random.map((val) => {
    unPairedList[val].paired = true;
    if (val < unPairedList.length) {
      return unPairedList[val];
    }
  });
}

const columns = [
  { name: '#', align: 'center', label: '#', field: null, sortable: true },
  {
    name: 'name',
    align: 'center',
    label: 'Name',
    field: 'name',
    sortable: true,
  },
  {
    name: 'addTime',
    align: 'center',
    label: 'Add Time',
    field: 'addTime',
    sortable: true,
  },
];
const unpairedRows = ref<User[]>([]);
const pairedRows = ref<User[]>([]);

onMounted(() => {
  unpairedRows.value = userStore.userList;
});
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
