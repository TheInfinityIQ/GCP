<script setup lang="ts">
import { ref } from 'vue'
</script>

<template>
    <header class="inline">
        <img src="../assets/Logo.svg" alt="Logo" />
        <h1>Game Chooser Program</h1>
    </header>
</template>

<style lang="scss" scoped>
header {
    position: absolute;
    top: 5vh;
    left: 0;

    width: 100vw;
    height: 10vh;

    display: flex;
    justify-content: center;
    align-items: center;
}
</style>
