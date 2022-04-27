<script setup lang="ts">
import gameList from '../components/GameList.vue';

import { onMounted } from "vue";
import { BgType } from "../enums";

import AuthModal from "../modal/AuthModal.vue";

let searchedGames: { title: string, desc: string }[] = [];

//To be got from API
for (let index = 0; index < 20; index++) {
    searchedGames.push({
        title: 'SearchedGameList',
        desc: 'This is the RimWorld game descriptions'
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

</script>

<template>
    <auth-modal />
    <game-list :items="searchedGames" />
</template>