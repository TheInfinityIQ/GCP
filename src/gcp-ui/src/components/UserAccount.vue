<script  setup lang="ts">
import logo from './Logo.vue'
import footerNav from './footer.vue'
import { BgType } from '../enums';
import { onMounted } from 'vue';

const emit = defineEmits<{
    (e: "bg-change", type?: BgType): void
}>()

onMounted(() => {
    emit("bg-change", BgType.NoBackgrounds);
    // emit("bg-change", BgType.NoBlurBackground);
    // emit("bg-change", BgType.NoBackgroundPicture);
})

</script>

<template>
    <form action="">
        <!-- UpdateAPI and return to home -->
        <section class="user-information big-input">
            <h1>User Information</h1>
            <div class="big-input">
                <label for="username">UserName:</label>
                <input type="text" id="username">
            </div>
            <div class="big-input">
                <label for="email">Email:</label>
                <input type="email" id="email">
            </div>
            <div class="big-input">
                <label for="languages">Languages:</label>
                <input type="text" id="languages">
            </div>
        </section>
        <!-- Inline -->
        <section class="inline">
            <div class="small-input">
                <label for="pronouns">Pronouns</label>
                <input type="text" id="pronouns">
            </div>
            <div class="small-input">
                <label for="gender">Gender</label>
                <input type="text" id="gender">
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
        <input type="submit" value="Upload Steam Games" class="apply-button">
    </form>
</template>

<!-- <style>
</style> -->

<style scoped>

.big-input, .small-input {
    /* border: 1px solid white; */
    
    margin: 0.5em 0;
}

.small-input>input {
    width: 90%;
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

    overflow: scroll;

    max-height: 95%;
}

form>section {
    width: 100%;
}
</style>