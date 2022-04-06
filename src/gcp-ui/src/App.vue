<script setup lang="ts">
import { ref } from "vue";
const msg = ref("Hello");
const count = ref(0);
</script>

<template>
    <!-- Header start -->
    <header class="inline">
        <img src="./assets/Logo.svg" alt="Logo" />
        <h1>Game Chooser Program</h1>
    </header>
    <!-- Header end -->

    <!-- Content in body should adjust image so that there is no whitespace -->
    <section>
        <img alt="Vue logo" src="./assets/logo.png" />
        <h1>{{ msg }}</h1>

        <p>
            Recommended IDE setup:
            <a href="https://code.visualstudio.com/" target="_blank">VSCode</a>
            +
            <a href="https://github.com/johnsoncodehk/volar" target="_blank">Volar</a>
        </p>

        <p>See <code>README.md</code> for more information.</p>

        <p>
            <a href="https://vitejs.dev/guide/features.html" target="_blank"> Vite Docs </a>
            |
            <a href="https://v3.vuejs.org/" target="_blank">Vue 3 Docs</a>
        </p>

        <button type="button" @click="count++">count is: {{ count }}</button>
        <p>
            Edit
            <code>components/HelloWorld.vue</code> to test hot module replacement.
        </p>
    </section>
    <section>
        <img alt="Vue logo" src="./assets/logo.png" />
        <h1>{{ msg }}</h1>

        <p>
            Recommended IDE setup:
            <a href="https://code.visualstudio.com/" target="_blank">VSCode</a>
            +
            <a href="https://github.com/johnsoncodehk/volar" target="_blank">Volar</a>
        </p>

        <p>See <code>README.md</code> for more information.</p>

        <p>
            <a href="https://vitejs.dev/guide/features.html" target="_blank"> Vite Docs </a>
            |
            <a href="https://v3.vuejs.org/" target="_blank">Vue 3 Docs</a>
        </p>

        <button type="button" @click="count++">count is: {{ count }}</button>
        <p>
            Edit
            <code>components/HelloWorld.vue</code> to test hot module replacement.
        </p>
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

  display: flex;
  justify-content: center;
  align-items: center;

  z-index: 1;

  border: 1px solid white;
}

// Main content

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
    margin: 0px;
    padding: 0px;
}

h1 {
  font-size: 1.30em;
  color: white;
  margin-left: 0.25em
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

header {
    position: fixed;
    top: 0.75em;
    left: 0;
    width: 100vw;
    
    display: flex;
    justify-content: center;
    align-items: center;
}

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

main {
  position: fixed;
  top: 0;
  left: 0;

  padding: 0px;
  margin: 0;

  width: 100vw;
  height: 90vh;

  background-image: url("./assets/backsplash.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  background-origin: border-box;
  background-position-x: 0;
  background-position-y: center;

  border: 1px solid blue;
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
