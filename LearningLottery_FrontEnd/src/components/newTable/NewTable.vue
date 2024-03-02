<template>
  <div>
    <h4 style="margin: 0">{{ tableTitle }}</h4>
    <table>
      <thead>
        <tr>
          <th v-if="showTableOrder">
            <div :style="{ 'text-align': 'center' }">#</div>
          </th>
          <th v-for="(column, index) in columns" :key="index + '_column_label'">
            <div :style="{ 'text-align': column.align }">
              {{ column.label }}
            </div>
          </th>
          <th v-if="$slots.actions">
            <div :style="{ 'text-align': 'center' }">Action</div>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, rowIndex) in rows" :key="rowIndex + '_row'">
          <td v-if="showTableOrder">
            <div :style="{ 'text-align': 'center' }">{{ rowIndex + 1 }}</div>
          </td>
          <template
            v-for="(item, itemKey, itemIndex) in row"
            :key="itemIndex + '_row_item'"
          >
            <td
              v-if="
                columns.findIndex((v) => v.name === (itemKey as string).toString()) > -1
              "
            >
              <div
                v-if="columns[itemIndex].isCanEdit == false"
                :style="{ 'text-align': columns[itemIndex].align }"
              >
                {{
                  isDateColumn((itemKey as string).toString())
                    ? FormatDateTime({
                        dateTimeStr: (item as string).toString(),
                      })
                    : item
                }}
              </div>
              <div
                v-else
                style="text-align: center"
                @click="
                  addOldData(rowIndex, itemIndex, deepCopy(row));
                  addEditingItem(rowIndex, itemIndex);
                  $nextTick(() => {
                    letInputFocus(rowIndex, itemIndex);
                  });
                "
              >
                <div v-if="!isEditing(rowIndex, itemIndex)">
                  {{
                    isDateColumn((itemKey as string).toString())
                      ? FormatDateTime({
                          dateTimeStr: (item as string).toString(),
                        })
                      : item
                  }}
                </div>
                <div v-else>
                  <input
                    :ref="
                      (DOM) => {
                        addInputDOM(rowIndex, itemIndex, DOM as HTMLInputElement);
                      }
                    "
                    v-model="row[itemKey]"
                  />
                  <br />
                  <button
                    @click.prevent.stop="
                      $emit('itemEditOnSave', rowIndex, {
                        new: row,
                        old: getOldData(rowIndex, itemIndex),
                      });
                      removeEditingItem(rowIndex, itemIndex);
                    "
                  >
                    save
                  </button>
                  <button
                    @click.prevent.stop="
                      $emit('itemEditOnCancel', rowIndex, {
                        new: row,
                        old: getOldData(rowIndex, itemIndex),
                      });
                      removeEditingItem(rowIndex, itemIndex);
                    "
                  >
                    cancel
                  </button>
                </div>
              </div>
            </td>
          </template>
          <td v-if="$slots.actions">
            <slot
              name="actions"
              :rowInfo="{ rowIndex: rowIndex, rowData: row }"
            ></slot>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { deepCopy } from 'src/js/deep-copy';
import { PropType, defineComponent, ref } from 'vue';
import { FormatDateTime } from 'src/js/format-date-time';
import { TextAlign } from 'src/enum/text-align';
import IEditingItem from 'src/components/newTable/interface/IEditingItem';
//TODO 不要any

// defineComponent({
//   name: 'NewTable',
//   props: {
//     tableTitle: {
//       type: String,
//       default: 'Default Table Title',
//     },
//     showTableOrder: {
//       type: Boolean,
//       default: true,
//     },
//     columns: {
//       type: Array as () => Column[],
//       required: true,
//     },
//     rows: {
//       type: Array as () => object[],
//       default: () => [],
//     },
//   },
//   setup(props, ctx) {
//     function getDictKey(rowIndex: number, itemIndex: number): string {
//       return rowIndex + '_' + itemIndex;
//     }

//     return { getDictKey };
//   },
// });

function getDictKey(rowIndex: number, itemIndex: number): string {
  return rowIndex + '_' + itemIndex;
}
const editingItems = ref<IEditingItem[]>([]);
function isEditing(rowIndex: number, itemIndex: number): boolean {
  return editingItems.value.some(
    (item) => item.rowIndex === rowIndex && item.itemIndex === itemIndex
  );
}
function addEditingItem(rowIndex: number, itemIndex: number): void {
  const item = {
    rowIndex: rowIndex,
    itemIndex: itemIndex,
  };
  editingItems.value.push(item);
}
function removeEditingItem(rowIndex: number, itemIndex: number): void {
  editingItems.value = editingItems.value.filter(
    (item) => item.rowIndex !== rowIndex || item.itemIndex !== itemIndex
  );
}

const inputDOM = ref<Record<string, HTMLInputElement | null>>({});
function addInputDOM(
  rowIndex: number,
  itemIndex: number,
  DOM: HTMLInputElement | null
): void {
  inputDOM.value[getDictKey(rowIndex, itemIndex)] = DOM;
}
function getInputDOM(
  rowIndex: number,
  itemIndex: number
): HTMLInputElement | null {
  return inputDOM.value[getDictKey(rowIndex, itemIndex)];
}

function letInputFocus(rowIndex: number, itemIndex: number): void {
  const dom = getInputDOM(rowIndex, itemIndex);
  if (dom == null) return;
  dom.focus();
}

const oldData = ref<Record<string, object>>({});
function addOldData(rowIndex: number, itemIndex: number, data: object): void {
  oldData.value[getDictKey(rowIndex, itemIndex)] = data;
}
function getOldData(rowIndex: number, itemIndex: number): object {
  return oldData.value[getDictKey(rowIndex, itemIndex)];
}

function isDateColumn(columnLabel: string): boolean {
  return (
    columnLabel.toLocaleLowerCase().includes('time') ||
    columnLabel.toLocaleLowerCase().includes('date')
  );
}

export interface Column {
  name: string;
  align: TextAlign;
  label: string;
  isCanEdit: boolean;
}

// type TableColumnArray<T> = T[];
defineProps({
  tableTitle: {
    type: String,
    default: 'Default Table Title',
  },
  showTableOrder: {
    type: Boolean,
    default: true,
  },
  columns: {
    type: Array as PropType<Column[]>,
    required: true,
  },
  rows: { type: Array as PropType<object[]> },
  //as PropType<TableColumnArray<string>>
  // actions: {
  //   type: Array as PropType<Action[]>,
  //   default: [] as Action[],
  // },
});
</script>
