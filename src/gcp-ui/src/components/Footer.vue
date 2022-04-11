<script setup lang="ts">
    import { ref } from "vue";
    
</script>

<template>
    <footer>
    <nav>
      <ul class="mobile-nav">
        <li class="circle">L</li>
        <li class="circle">C</li>
        <li class="circle">A</li>
      </ul>
    </nav>
  </footer>
</template>

<style  lang="scss" scoped>
footer {
    position: fixed;
    left: 0;

    height: 10vh;
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
</style>