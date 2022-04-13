<script  setup lang="ts">
import logo from './Logo.vue'
import footerNav from './footer.vue'

</script>

<template>
    <form action="">
        <!-- UpdateAPI and return to home -->
        <h1>User Information</h1>
        <label for="username">UserName:</label>
        <input type="text" id="username">
        <label for="email">Email:</label>
        <input type="email" id="email">
        <label for="languages">Languages:</label>
        <input type="text" id="languages">
        <!-- Inline -->
        <div class="inline">
            <label for="Pronouns"></label>
            <input type="text" id="">
            <label for="Age"></label>
            <input type="number" id="">
        </div>
        <!-- End of inline -->
        <h1>Linked Accounts</h1>
        <label for="steam">Steam:</label>
        <input type="text" id="steam">
        <label for="discord">Discord</label>
        <input type="text" id="discord">

        <input type="submit" value="Apply">
    </form>
</template>


<style scoped>

</style>
