<script  setup lang="ts">
import gameList from '../components/GameList.vue';
import AuthModal from "../modal/AuthModal.vue";

import { onMounted } from "vue";
import { BgType } from "../enums";
import client from '../api';

let userGames: { title: string, desc: string }[] = [];

//To be got from API
for (let index = 0; index < 13; index++) {
    userGames.push({
        title: 'YourGamesList',
        desc: 'YadaYadaYada'
    });
}

const emit = defineEmits<{
    (e: "bg-change", type?: BgType): void
}>();

onMounted(() => {
    // emit("bg-change", BgType.NoBackgrounds);
    // emit("bg-change", BgType.NoBlurBackground);
    // emit("bg-change", BgType.NoBackgroundPicture);
    emit("bg-change", BgType.Default);
})

let isAuthenticated = client.IsAuthenticated();
console.log("UserGameList: " + isAuthenticated);
</script>

<template>
    <auth-modal v-show="!isAuthenticated"/>
    <game-list :items="userGames" :create="true"/>
</template>