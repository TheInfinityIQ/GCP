<script setup lang="ts">
import { ref, onMounted } from "vue";
import { RouterLink, RouterView } from 'vue-router'

import Logo from './components/Logo.vue';
import UserGameList from './components/UserGameList.vue';
// import searchGameList from './components/SearchGameList.vue';
import FooterNav from './components/Footer.vue';
import SearchGameList from './components/SearchGameList.vue';
import { BgType } from "./enums"; // Removed , Enum -> Causing errors
// import { GetLogin} from "./api";
// import helloWorld from './components/HelloWorld.vue'

const blurBackClasses = ref<{ [field: string]: boolean }>({});
const wrapperClasses = ref<{ [field: string]: boolean }>({});
const changeBg = (type?: BgType): void => {
  if (type === undefined || type === BgType.Default) {
    return;
  }
  const containsBg = (t: BgType) => (type & t) === t;

  wrapperClasses.value["no-bg"] = containsBg(BgType.NoBackgroundPicture);
  blurBackClasses.value["no-blur-bg"] = containsBg(BgType.NoBackgroundPicture);
}

onMounted(async () => {
  console.log(await (await fetch("https://localhost:5001/api/Secret/public")).json());
})
</script>

<template>
  <div :class="{ 'wrapper': true, ...wrapperClasses }">
    <logo />
    <div :class="{ 'container-main': true, ...blurBackClasses }">
      <router-view @bg-change="changeBg"></router-view>
    </div>
    <footer-nav />
  </div>
</template>

<style lang="scss">
// @import "./assets/style.scss";

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
  // color: #a9b5c1; White is more readable
}

.wrapper {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  height: 100vh;
  max-height: 100vh;

  background-image: url("./assets/backsplash.jpg");
  background-repeat: no-repeat;
  background-size: cover;

  background-origin: border-box;
  background-position-x: center;
  background-position-y: center;
}

.wrapper.no-bg {
  background-image: none;
  background-color: #051522;
}


.container-main {
  border-radius: 15px;
  // background-color: rgba(5, 21, 34, 1.75);

  margin: 2vh 5vw;

  padding: 0 1em;

  overflow: hidden;


  position: relative;
  top: 0;

  z-index: 1;

  &::before {
    content: '';
    border-radius: 15px;
    background-image: url("./assets/backsplash.jpg");
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

.container-main.no-blur-bg {
  &::before {
    display: none;
  }
}

input {
    width: 95%;
    height: 5vh;

    margin-bottom: 0.5em;

    border: 0;
    border-radius: 5px;
    background-color: #437096;
}

label {
  color: white;
}

[type='submit'] {
  background-color: #0E3D63;
  color: #a9b5c1;

  margin-bottom: 1em;
}

html,
body {
  margin: 0px;
  padding: 0;
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