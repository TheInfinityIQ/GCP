<script setup lang="ts">
let games: [string] = ["GameExample"];

for (let index = 0; index < 15; index++) {
    games.push("GameExample")
}
</script>

<template>
    <div class="modal-container">
        <h1>Oops! Log in to use this feature.</h1>
        <section class="table-header">
            <h2>Game</h2>
            <h2>Selected</h2>
        </section>
        <section class="table-content">
            <ul>
                <li v-for="game in games" class="content-list">
                    <div class="inline content-row">
                        <p>{{ game }}</p>
                        <input type="checkbox">
                    </div>
                </li>
            </ul>
        </section>
        <div class="column add-game-nav">
            <button>Add Selected to Game List</button>
            <button>Create Game Manually</button>
        </div>
    </div>
</template>

<style scoped>
button {
    margin: 0.5em 0;
}
</style>