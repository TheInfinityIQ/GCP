<script setup lang="ts">

</script>

<template>
  <nav>
    <ul class="mobile-nav">
      <li class="circle left-circle" v-on:click="">L</li>
      <!-- <li class="circle">C</li> -->
      <li class="circle right-circle" v-on:click="">A</li>
    </ul>
  </nav>
</template>

<style  lang="scss" scoped>
nav>ul {
  display: flex;
  justify-content: space-evenly;
  background-color: #051522;
  align-items: center;

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