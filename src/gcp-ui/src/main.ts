import { createWebHashHistory, createRouter, createWebHistory } from 'vue-router';
import { createApp } from 'vue';
import 'vue-select/dist/vue-select.css';

// Routes
import App from './App.vue'
import userGameList from './components/UserGameList.vue';
import searchGameList from './components/SearchGameList.vue';
import helloWorld from './components/HelloWorld.vue'
import userAccount from './components/UserAccount.vue'
import auth from './components/Auth.vue';

const routes = [
  { path: '/searchGames', component: searchGameList },
  { path: '/userGames', component: userGameList },
  { path: '/hellowWorld', component: helloWorld },
  { path: '/userAccount', component: userAccount },
  { path: '/auth', component: auth },
  { path: '/', component: searchGameList }
]

const router = createRouter({
  // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
  history: createWebHistory(),
  routes, // short for `routes: routes`
})

const app = createApp(App)

app.use(router)

app.mount('#app')
