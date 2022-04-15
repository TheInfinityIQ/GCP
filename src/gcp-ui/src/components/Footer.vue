<script setup lang="ts">
import { routerKey, RouterLink, useRouter} from 'vue-router';

const router = useRouter();

const onClick = (path: string) => {
  router.push(path);
}

</script>

<template>
  <nav>
    <ul class="mobile-nav">
      <li><router-link to="/search-games" class="circle">SG</router-link></li>
      <li><router-link to="/user-games" class="circle">UG</router-link></li>
      <li><router-link to="/user-account" class="circle">A</router-link></li>
    </ul>
  </nav>
</template>

<style  lang="scss" scoped>
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
</style>