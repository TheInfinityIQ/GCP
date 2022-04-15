<script  setup lang="ts">

</script>

<template>
    <form action="">
        <!-- UpdateAPI and return to home -->
        <div class="block">
            <label for="email">Email Address:</label>
            <input type="text" id="email">
        </div>
        <div class="block">
            <label for="password">Password: </label>
            <input type="text" id="password">
        </div>
        <div class="block">
            <label for="cPassword">Confirm Password: </label>
            <input type="text" id="cPassword">
        </div>
        <input type="submit" value="submit">
    </form>
</template>

<style scoped>
form {
    margin-top: 1em;

    width: 100%;

    display: flex;
    flex-direction: column;
    align-items: center;

    padding: 0;
    padding-bottom: 100%;
}

.block {
    width: 100%;
    margin: 1em;

    display: inline-flex;
    flex-direction: column;
    align-items: center;
}

.block > input {
    width: 95%;
    max-width: 250px;
}
</style>