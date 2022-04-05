<script setup lang="ts">
import { ref } from "vue";
const msg = ref("Hello");
const count = ref(0);
</script>

<template>
  <header class="inline">
    <img src="./assets/Logo.svg" alt="Logo" />
    <h1>Game Chooser Program</h1>
  </header>

  <main>
    <article class=".frosted-container">
      <section class="lists-search">
        <label for="list-search">Search Lists</label>
        <input type="text" id="list-search" />
      </section>
    </article>
  </main>

  <footer>
    <div class="circle">L</div>
    <div class="circle">C</div>
    <div class="circle">A</div>
  </footer>
</template>

<style lang="scss">
@import "./assets/style.scss";

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #a9b5c1;
  margin-top: 60px;
}

// Header

header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 10vh;

  display: flex;
  justify-content: center;
  align-items: center;

  z-index: 1;

  border: 1px solid white;
}

// Main content

main {
  position: fixed;
  top: 0;

  width: 100vw;
  height: 90vh;

  background-image: url("./assets/backsplash.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  background-origin: border-box;
  background-position-x: 0;
  background-position-y: center;

  border: 1px solid blue;

  display: flex;
  align-items: center;
  justify-content: center;
}

.search-menu {
  margin: 0 1.5em;
}

.game-listing {
  border: 1px solid white;
}

// Footer

footer {
  height: 10vh;
  width: 100%;

  background-color: black;

  position: fixed;

  bottom: 0;
  left: 0;
  border: 1px solid white;

  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  align-items: center;
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

// Default stylings
h1,
h2,
h3,
h4,
h5,
p {
  font-family: "Inter", sans-serif;
}

html,
body {
  padding: 0;
  margin: 0;
}

h1 {
  font-size: 1.3em;
  color: white;
  margin-left: 0.25em;
}

h2 {
  font-size: 1.15em;
}

h3 {
  font-size: 1.05em;
}

.circle {
  display: flex;

  border: 2px solid white;
  width: 3rem;
  height: 3rem;
  border-radius: 23px;

  align-items: center;
  justify-content: center;

  user-select: none;

  &:hover {
    cursor: pointer;
    color: #42b983;
  }
}

a {
  color: #42b983;

  &:hover {
    color: black;
  }
}

input, label {
  display: block;
}
</style>
