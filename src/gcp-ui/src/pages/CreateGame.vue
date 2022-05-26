<script setup lang="ts">
import { onMounted } from "vue";
import { BgType } from "../enums";
import client from "../api"

const emit = defineEmits<{
    (e: "bg-change", type?: BgType): void;
}>();

onMounted(() => {
    emit("bg-change", BgType.NoBackgrounds);
    // emit("bg-change", BgType.NoBlurBackground);
    // emit("bg-change", BgType.NoBackgroundPicture);
});

let games = client.GetGames();
localStorage.setItem("games", JSON.stringify(games));

</script>

<template>
    <form action="">
        <div class="big-input">
            <label for="game-name">Game Name</label>
            <input type="text" id="game-name" />
        </div>
        <section class="table-content">
            <ul>
                <li v-for="game in games" class="content-list">
                    <div class="inline content-row">
                        <p>{{ game.name }}</p>
                        <input type="checkbox">
                    </div>
                </li>
            </ul>
        </section>
        <input type="submit" value="Create Game"/>
    </form>
</template>

<style scoped>
</style>
