import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'
import { quasar, transformAssetUrls } from '@quasar/vite-plugin'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
     proxy: {
        '/api': {
          target: 'https://localhost:44315',
          changeOrigin: true,
          secure: false,
        },
      },
  },
  plugins: [
    vue({
      template: { transformAssetUrls }
    }),
    vueJsx(),
    quasar({
      sassVariables: 'src/quasar-variables.sass'
    })
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  }
})
