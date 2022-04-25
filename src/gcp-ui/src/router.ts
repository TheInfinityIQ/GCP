import { createRouter, createWebHistory } from "vue-router"

import UserGameList from "./components/Pages/UserGameList.vue";
import CreateGameList from "./components/Pages/CreateGameList.vue";
import SearchGameList from "./components/Pages/SearchGameList.vue";
import HelloWorld from "./components/HelloWorld.vue"
import UserAccount from "./components/Pages/UserAccount.vue"
import SignIn from "./components/Pages/SignIn.vue";
import SignUp from "./components/Pages/SignUp.vue";
import GameListDetails from "./components/Pages/GameListDetails.vue";

const routes = [
    { path: "/search-games", component: SearchGameList },
    { path: "/user-games", component: UserGameList },
    { path: "/user-games/create", component: CreateGameList },
    { path: "/hello-world", component: HelloWorld },
    { path: "/user-account", component: UserAccount },
    { path: "/sign-in", component: SignIn },
    { path: "/sign-up", component: SignUp },
    { path: "/game-details", component: GameListDetails },
    { path: "/", component: SearchGameList }
];

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes, // short for `routes: routes`
});

export { router, routes };