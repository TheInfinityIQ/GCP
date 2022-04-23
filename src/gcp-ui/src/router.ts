import { createRouter, createWebHistory } from "vue-router"

import UserGameList from "./components/UserGameList.vue";
import CreateGameList from "./components/CreateGameList.vue";
import SearchGameList from "./components/SearchGameList.vue";
import HelloWorld from "./components/HelloWorld.vue"
import UserAccount from "./components/UserAccount.vue"
import SignIn from "./components/SignIn.vue";
import SignUp from "./components/SignUp.vue";

const routes = [
    { path: "/search-games", component: SearchGameList },
    { path: "/user-games", component: UserGameList },
    { path: "/user-games/create", component: CreateGameList },
    { path: "/hello-world", component: HelloWorld },
    { path: "/user-account", component: UserAccount },
    { path: "/sign-in", component: SignIn },
    { path: "/sign-up", component: SignUp },
    { path: "/create-gamelist", component: CreateGameList },
    { path: "/", component: SearchGameList }
];

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes, // short for `routes: routes`
});

export { router, routes };