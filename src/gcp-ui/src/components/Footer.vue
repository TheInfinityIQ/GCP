<script setup lang="ts">
    import { ref } from "vue";
    
</script>

<template>
    <nav>
      <ul class="mobile-nav">
        <li class="circle">L</li>
        <li class="circle">C</li>
        <li class="circle">A</li>
      </ul>
    </nav>
</template>

<style  lang="scss" scoped>

nav > ul {
    display: flex;
    justify-content: space-evenly;
    background-color: #051522;

    height: 3.5em;
    width: 100vw;

    padding: 1em 0rem;
    margin: 0px;
}
</style>