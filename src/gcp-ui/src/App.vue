<script setup lang="ts">
import logo from './components/Logo.vue';
import searchMenu from './components/Search-Menu.vue';
import footerNav from './components/Footer.vue';

</script>

<template>
  <logo />
  <!-- Body end -->
  <searchMenu />
  <!-- Footer Start -->
  <footerNav />
  
  <!-- Footer End -->
</template>

<style lang="scss">
// @import "./assets/style.scss";

// Positioning of comps

//Defaults
@import url("https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400&display=swap");

// Search menu - Won't work if put into styles tag of Search-menu.vue
.vs__clear,
.vs__open-indicator {
    // display: none;
    scale: 50%;
    margin: 0;
}
.vs__dropdown-toggle {
    height: 3em;
    background: #fff !important;
}

.vs__actions {
    display: inline-flex;
}

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
  padding: 0;

  display: flex;
  justify-content: center;
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