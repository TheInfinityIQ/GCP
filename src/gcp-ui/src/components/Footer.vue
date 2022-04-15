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
      <li><button class="circle" @click="onClick('')">L</button></li>
      <li><button class="circle" @click="onClick('')">A</button></li>
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

button {
  border-radius: 25px;
  border: 1px solid white;

  width: 3rem;
  height: 3rem;
}

button:hover {
  cursor: pointer;
  background-color: green;
}

// .circle {
//   display: flex;

//   border: 2px solid white;
//   width: 3rem;
//   height: 3rem;
//   border-radius: 25px;

//   align-items: center;
//   justify-content: center;

//   user-select: none;

//   &:hover {
//     cursor: pointer;
//     color: #42b983;
//   }
// }
</style>