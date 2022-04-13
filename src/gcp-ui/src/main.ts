import {createWebHashHistory, createRouter, createWebHistory} from 'vue-router';
import { createApp } from 'vue';
import 'vue-select/dist/vue-select.css';

// Routes
import App from './App.vue'
import userGameList from './components/UserGameList.vue';
import searchGameList from './components/SearchGameList.vue';
import hellowWorld from './components/HelloWorld.vue'
import userAccount from './components/UserAccount.vue'

const routes = [
    { path: '/searchGames', component: userGameList },
    { path: '/userGames', component: searchGameList },
    { path: '/hellowWorld', component: hellowWorld },
    { path: '/userAccount', component: userAccount },
    { path: '/', component: userAccount }
  ]
  
  const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes, // short for `routes: routes`
  })
  
  const app = createApp(App)

  app.use(router)

  app.mount('#app')
