<script  setup lang="ts">
import logo from './Logo.vue'
import footerNav from './footer.vue'

</script>

<template>
    <form action="">
        <!-- UpdateAPI and return to home -->
        <section class="user-information big-input">
            <h1>User Information</h1>
            <label for="username">UserName:</label>
            <input type="text" id="username">
            <label for="email">Email:</label>
            <input type="email" id="email">
            <label for="languages">Languages:</label>
            <input type="text" id="languages">
        </section>
        <!-- Inline -->
        <section class="inline">
            <div class="small-input">
                <label for="pronouns">Pronouns</label>
                <input type="text" id="pronouns">
            </div>
            <div class="small-input">
                <label for="age">Age</label>
                <input type="number" id="age">
            </div>
        </section>
        <!-- End of inline -->
        <section class="linked-accounts big-input">
            <h1>Linked Accounts</h1>
            <label for="steam">Steam:</label>
            <input type="text" id="steam">
            <label for="discord">Discord</label>
            <input type="text" id="discord">
        </section>
        <input type="submit" value="Apply" class="apply-button">
    </form>
</template>

<!-- <style>
</style> -->

<style scoped>
input,
label {
    margin: 0;
}

input {
    width: 95%;
}

.small-input > input {
    width: 45%;
}

.small-input {
    display: flex;
    flex-direction: column;
    align-items: center;
}

form {
    width: 100%;

    display: flex;
    flex-direction: column;
    align-items: center;

    padding: 0;
}

.inline {
    display: inline-flex;
}

form > section {
    width: 100%;
}

.apply-button {
    width: 30%;
    
    min-width: 60px;
}


</style>