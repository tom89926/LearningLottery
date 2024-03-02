// import './assets/main.css'
import { Quasar, Notify } from 'quasar'

// Import icon libraries
import '@quasar/extras/material-icons/material-icons.css'

// Import Quasar css
import 'quasar/src/css/index.sass'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { store, key } from '@/vuex-stores/test-store';
const app = createApp(App)
app.use(Quasar, {
    plugins: {
      Notify
    }
  })
app.use(router)
app.use(store, key)

app.mount('#app')
