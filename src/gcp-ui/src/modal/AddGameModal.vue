<script>

</script>

<template>
    <div class="modal-container">
        <h1>Oops! Log in to use this feature.</h1>
        <section class="game-table-header">
            <h2>Game</h2>
            <h2>Selected</h2>
        </section>
        <section class="game-table-content">
            <ul>
                <li v-for="game in games" class="game-list">
                    <div class="inline game-row">
                        <p>{{ user }}</p>
                        <input type="checkbox">
                    </div>
                </li>
            </ul>
        </section>
        <button>Add Selected to Game List</button>
        <button>Create Game Manually</button>
    </div>
</template>

<style>
</style>