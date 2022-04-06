<script setup lang="ts">
import HelloWorld from "./components/HelloWorld.vue";
import { ref } from "vue";
const stuff = ref([1, 2]);
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
        <p>Search for games:</p>
        <input type="text" id="search" />
      </section>
      <section class="pill-menus">
        <div class="pill-menu" id="sort"><p>Sort: Newest</p></div>
        <div class="pill-menu" id="platform"><p>Sort: Platform</p></div>
      </section>
      <section class="pill-menu-contents">
        <div class="pill-menu-content pill-menu-sort">
          <ul>
            <li>Newest</li>
            <li>Oldest</li>
            <li>Most People</li>
            <li>Most Active</li>
          </ul>
        </div>
        <div class="pill-menu-content pill-menu-platform">
          <ul>
            <li>Steam</li>
            <li>Xbox</li>
            <li>Playstation</li>
            <li>Web</li>
          </ul>
        </div>
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
@import "./assets/style.scss";

//Header
header {
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
  grid-template-rows: 2fr 1fr 3fr;
}

.search-field {
  display: grid;
  grid-template-rows: repeat(2, 1fr);
}

.search-field > p {
  margin-bottom: 0.25em;
  padding: 0;

  color: white;

  font-weight: 900;
  font-size: 1.15em;

  justify-self: start;
  margin-left: 12vw;
}

.search-field > input {
  width: 80vw;
  height: 5vh;

  border: 0;
  border-radius: 5px;

  justify-self: center;
  background-color: #437096;
}

.search-menu {
  margin-top: 20vh;
}

.frosted-background {
  border: 1px solid white;

  height: 100vh;
}

.pill-menus {
  display: inline-flex;

  justify-content: space-between;
  justify-self: center;
  width: 80vw;
  height: 5vh;

  border: 1px solid white;
}

.pill-menu {
  border: 1px solid green;
  border-radius: 5px;

  background-color: #0e3d63;

  width: 20vw;
  height: 4vh;
}

.pill-menu > p {
  color: white;
  font-size: 0.5em;
}

.pill-menu-contents {
  display: none;
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