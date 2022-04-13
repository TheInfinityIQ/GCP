<script  setup lang="ts">
import gameList from './game-list.vue';

let searchedGames: { title: string, desc: string }[] = [];

//To be got from API
for (let index = 0; index < 3; index++) {
    searchedGames.push({
        title: 'SearchedGameList',
        desc: 'This is the RimWorld game descriptions'
    });
}

</script>

<template>
    <game-list :items="searchedGames" />
</template>