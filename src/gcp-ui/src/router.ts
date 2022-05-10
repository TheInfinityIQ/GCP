import { createRouter, createWebHistory } from "vue-router";

import UserGameList from "./pages/UserGameList.vue";
import CreateGameList from "./pages/CreateGameList.vue";
import SearchGameList from "./pages/SearchGameList.vue";
import GameListDetails from "./pages/GameListDetails.vue";
import CreateGame from "./pages/CreateGame.vue";
import HelloWorld from "./components/HelloWorld.vue";
import UserAccount from "./pages/UserAccount.vue";
import SignIn from "./pages/SignIn.vue";
import SignUp from "./pages/SignUp.vue";

const routes = [
    { path: "/user-games/create", component: CreateGameList },
    { path: "/create-game", component: CreateGame },
    { path: "/hello-world", component: HelloWorld },
    { path: "/search-games", component: SearchGameList },
    { path: "/user-games", component: UserGameList },
    { path: "/user-account", component: UserAccount },
    { path: "/game-details", component: GameListDetails },
    { path: "/sign-in", component: SignIn },
    { path: "/sign-up", component: SignUp },
    { path: "/", component: SearchGameList },
];

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes, // short for `routes: routes`
});

export { router, routes };
