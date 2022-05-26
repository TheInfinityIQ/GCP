<script setup lang="ts">
import { routerKey, RouterLink, useRouter } from 'vue-router';
import client from '../api';

const router = useRouter();

const onClick = (path: string) => {
  router.push(path);
}

let isAuthenticated = client.IsAuthenticated();
client.GetLoginAsync("mod@gcp.com", "Password-1");
// console.log(`FOOTER - Authenticated: ${isAuthenticated}`);
</script>

<template>
  <nav>
    <ul class="mobile-nav">
      <li>
        <router-link to="/search-games" class="circle default-border">SG</router-link>
      </li>
      <li>
        <router-link to="/user-games" class="circle default-border">UG</router-link>
      </li>
      <li>
        <router-link to="/user-account" class="circle logged-in" v-if="isAuthenticated">A</router-link>
        <router-link to="/user-account" class="circle logged-out" v-else>A</router-link>
      </li>
    </ul>
  </nav>
</template>

<style  lang="scss" scoped>
.logged-in {
  border: 2px solid green;
}

.logged-out {
  border: 2px solid red;
}

.default-border {
  border: 2px solid white;
}

nav>ul {
  display: flex;
  justify-content: space-evenly;
  background-color: #051522;
  align-items: center;

  list-style-type: none;

  width: 100vw;

  padding: 1em 0;
  margin: 0px;

  color: white;
}

.circle {
  display: flex;
  
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
</style>