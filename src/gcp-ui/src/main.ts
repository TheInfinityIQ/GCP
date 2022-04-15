import { createWebHashHistory, createRouter, createWebHistory } from 'vue-router';
import { createApp } from 'vue';
import 'vue-select/dist/vue-select.css';

// Routes
import App from './App.vue'
import userGameList from './components/UserGameList.vue';
import createGameList from './components/CreateGameList.vue';
import searchGameList from './components/SearchGameList.vue';
import helloWorld from './components/HelloWorld.vue'
import userAccount from './components/UserAccount.vue'
import SignIn from './components/SignIn.vue';
import SignUp from './components/SignUp.vue';

const routes = [
  { path: '/search-games', component: searchGameList },
  { path: '/user-games', component: userGameList },
  { path: '/user-games/create', component: createGameList },
  { path: '/hello-world', component: helloWorld },
  { path: '/user-account', component: userAccount },
  { path: '/sign-in', component: SignIn },
  { path: '/sign-up', component: SignUp },
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
