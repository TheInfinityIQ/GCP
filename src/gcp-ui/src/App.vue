<script setup lang="ts">
import { ref } from "vue";
import vSelect from "vue-select";

let sort: string[] = ["Newest", "Activity", "Population"];
let platform: string[] = ["Windows", "Linux", "Mac"];

const stuff = ref([1, 2]);

let games: { title: string, desc: string }[] = [];

for (let index = 0; index < 15; index++) {
  games.push({
    title: 'RimWorld',
    desc: 'This is the RimWorld game descriptions'
  });
}
</script>

<template>
  <!-- Header start -->
  <header class="inline">
    <img src="./assets/Logo.svg" alt="Logo" />
    <h1>Game Chooser Program</h1>
  </header>
  <!-- Header end -->

  <!-- Content in body should adjust image so that there is no whitespace -->
  <section class="search-menu">
    <section class="search-field">
      <p class="search-games-text">Search for games:</p>
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
      </form> -->
      <ul>
        <li v-for="game in games" class="game-list">
          <h3>{{ game.title }}</h3>
          <p>{{ game.desc }}</p>
        </li>
      </ul>
    </section>
  </section>
  <!-- Body end -->

  <!-- Footer Start -->
  <footer>
    <nav>
      <ul class="mobile-nav">
        <li class="circle">L</li>
        <li class="circle">C</li>
        <li class="circle">A</li>
      </ul>
    </nav>
  </footer>
  <!-- Footer End -->
</template>

<style lang="scss">
// @import "./assets/style.scss";


.vs__clear,
.vs__open-indicator {
  // display: none;
  scale: 50%;
  margin: 0;
}

.pill-menus {
  display: flex;
  justify-content: space-around;

}

.pill-menu {
  font-size: 0.6em;
}

.vs__dropdown-toggle {
  height: 3em;
  background: #fff !important; 
}

.vs__actions {
  display: inline-flex;
}

header header {
  position: absolute;
  top: 5vh;
  left: 0;

  width: 100vw;
  height: 10vh;

  display: flex;
  justify-content: center;
  align-items: center;
}

// Search Menu

.search-menu {
  display: grid;
  grid-template-rows: 15vh 10vh 3fr;
}

.search-field {
  display: grid;
  grid-template-rows: repeat(2, 1fr);
}

.search-field > p {
  margin-bottom: 0.25em;
  padding: 0;
  padding-left: 1.25em;

  color: white;

  font-weight: 900;
  font-size: 1.15em;

  justify-self: start;
  margin-left: 0;
}

.search-field > input {
  width: 79vw; // 80vw would stick out a bit
  height: 5vh;

  margin-bottom: 0.5em;

  border: 0;
  border-radius: 5px;

  justify-self: center;
  background-color: #437096;
}

.search-menu {
  margin-top: 20vh;

  background-color: #051522;
  border-radius: 15px;
  width: 80vw;
  height: 80vh;
  margin: 20vh 10vw 0 10vw;
}

.frosted-background {
  border: 1px solid white;

  height: 100vh;
}

.pill-menu {
  display: inline-flex;
}

.search-results {
  margin-top: 1em;

  width: 80vw;
  height: 60vh;

  display: flex;
  flex-direction: column;

  overflow: scroll;
}

.search-results .game-list {
  text-align: start;

  padding: 0 2em;

  max-height: 10em;
}

.search-results ul {
  margin: 0;
  padding: 0;
}

.search-results .game-list > p {
  font-size: 0.75em;
}

.search-results .game-list h3 {
  font-weight: 900;
}

//Footer
footer > nav > ul {
  display: flex;
  justify-content: space-evenly;

  position: fixed;
  bottom: 0;
  background-color: #051522;

  height: 3.5em;
  width: 100vw;

  padding: 1em 0rem;
  margin: 0px;
}

//Defaults
@import url("https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400&display=swap");

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #a9b5c1;
  margin-top: 60px;
}

body {
  background-image: url("./assets/backsplash.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  // background-size: 100vw 80vh;

  background-origin: border-box;
  background-position-x: center;
  background-position-y: center;
}

html,
body {
  margin: 0px;
  padding: 0px;
}

h1 {
  font-size: 1.3em;
  color: white;
  margin-left: 0.25em;
}

h1,
h2,
h3,
h4,
h5,
p {
  font-family: "Inter", sans-serif;
  color: white;
}

ul {
  list-style-type: none;
}

.circle {
  display: flex;

  border: 2px solid white;
  width: 3rem;
  height: 3rem;
  border-radius: 25px;

  align-items: center;
  justify-content: center;

  user-select: none;

  &:hover {
    cursor: pointer;
    color: #42b983;
  }
}

.inline {
  display: inline-flex;
}

a {
  color: #42b983;

  &:hover {
    color: black;
  }
}

label {
  margin: 0 0.5em;
  font-weight: bold;
}

code {
  background-color: #eee;
  padding: 2px 4px;
  border-radius: 4px;
  color: #304455;
}
</style>