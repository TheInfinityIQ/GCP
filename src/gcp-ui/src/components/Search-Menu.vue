<script setup lang="ts">
import vSelect from "vue-select";

let sort: string[] = ["New", "Hot", "Users"];
let platform: string[] = ["Windows", "Linux", "Mac"];

let games: { title: string, desc: string }[] = [];

for (let index = 0; index < 10; index++) {
    games.push({
        title: 'RimWorld',
        desc: 'This is the RimWorld game descriptions'
    });
}
</script>

<template>
    <div class="container-main">
        <section class="search-menu">
            <section class="search-field">
                <p class="search-games-text">Search for games</p>
                <input type="text" id="search" />
                <section class="pill-menus">
                    <v-select label="title" :options="sort" class="pill-menu"></v-select>
                    <v-select label="title" :options="platform" class="pill-menu"></v-select>
                </section>
            </section>
            <section class="search-results">
                <!-- <form action="https://localhost:5001/steam/parse-vdf" method="post">
          <label for="fname">File:</label>
          <input type="file" id="fname" name="fname" />
                </form>-->
                <ul>
                    <li v-for="game in games" class="game-list">
                        <h3>{{ game.title }}</h3>
                        <p>{{ game.desc }}</p>
                    </li>
                </ul>
            </section>
        </section>
    </div>
</template>

<style lang="scss" scoped>


.pill-menus {
    display: flex;
    justify-content: space-between;
}

.pill-menu {
    font-size: 0.6em;
}

// Search Menu

.container-main {
    min-height: 80vh;

    background-color: #051522;
    border-radius: 15px;

    margin: 2vh 5vw;

    padding: 0 1em;

    overflow: hidden;
}

.search-menu {
    display: grid;
    grid-template-rows: 20% 10%;
}

.search-field > p {
    margin-bottom: 0.25em;

    color: white;

    font-weight: 900;
    font-size: 1.15em;

    justify-self: center;
    margin-left: 0;
}

.search-field > input {
    width: 100%;
    height: 5vh;

    margin-bottom: 0.5em;

    border: 0;
    border-radius: 5px;
    background-color: #437096;

    justify-self: center;
}

.search-results {
    margin-top: 1em;
    max-height: 60vh; // Enables overflow scroll because it will resize without max height

    display: flex;
    flex-direction: column;

    overflow: scroll;
}

.search-results .game-list {
    text-align: start;
}

.search-results > ul {
    list-style-type: none;

    margin: 0;
    padding: 0;
}

.search-results .game-list > p {
    font-size: 0.75em;
}

.search-results .game-list h3 {
    font-weight: 900;
}
</style>
