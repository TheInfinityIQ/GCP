import {createWebHashHistory, createRouter} from 'vue-router';
import { createApp } from 'vue';
import 'vue-select/dist/vue-select.css';

// Routes
import App from './App.vue'
import userGameList from './components/UserGameList.vue';
import searchGameList from './components/SearchGameList.vue';
import hellowWorld from './components/HelloWorld.vue'

const routes = [
    // { path: '/', component: App},
    { path: '/searchGames', component: userGameList },
    { path: '/userGames', component: searchGameList },
    { path: '/hellowWorld', component: hellowWorld }
  ]
  
  const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHashHistory(),
    routes, // short for `routes: routes`
  })
  
  const app = createApp(App)

  app.use(router)

  app.mount('#app')
