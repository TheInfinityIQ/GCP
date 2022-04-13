<script setup lang="ts">
import vSelect from "vue-select";
import { PropType, ref } from 'vue'


// const searchGames = ref(true);

let sort: string[] = ["New", "Hot", "Users"];
let platform: string[] = ["Windows", "Linux", "Mac"];

interface ListContent {
    title: string,
    desc: string
}

const props = defineProps({
    items: {
        type: Object as PropType<ListContent[]>,
        required: true
    },
});
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
                <ul>
                    <li v-for="list in items" class="game-list">
                        <h3>{{ list.title }}</h3>
                        <p>{{ list.desc }}</p>
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
    border-radius: 15px;
    // background-color: rgba(5, 21, 34, 1.75);

    margin: 2vh 5vw;

    padding: 0 1em;

    overflow: hidden;


    position: relative;
    top: 0;


    &::before {
        content: '';
        border-radius: 15px;
        background-image: url("../assets/backsplash.jpg");
        background-repeat: no-repeat;
        background-size: cover;

        background-origin: border-box;
        background-position-x: center;
        background-position-y: center;

        filter: blur(2px);

        position: absolute;
        top: 0;
        left: 0;

        z-index: -1;

        display: inline-flex;
        justify-content: center;
        align-items: center;
        height: 100%;
        width: 100%;
    }
}

.search-field>p {
    margin-bottom: 0.25em;

    color: white;

    font-weight: 900;
    font-size: 1.15em;

    justify-self: center;
    margin-left: 0;
}

.search-field>input {
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
    max-height: 50vh; // Enables overflow scroll because it will resize without max height

    display: flex;
    flex-direction: column;

    overflow: scroll;
}

.search-results .game-list {
    text-align: start;
}

.search-results>ul {
    list-style-type: none;

    margin: 0;
    padding: 0;
}

.search-results .game-list>p {
    font-size: 0.75em;
}

.search-results .game-list h3 {
    font-weight: 900;
}
</style>
