<script  setup lang="ts">
import gameList from './game-list.vue';

let userGames: { title: string, desc: string }[] = [];

//To be got from API
for (let index = 0; index < 13; index++) {
    userGames.push({
        title: 'YourGamesList',
        desc: 'YadaYadaYada'
    });
}

</script>

<template>
    <game-list :items="userGames" />
</template>