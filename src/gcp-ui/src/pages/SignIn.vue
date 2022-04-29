<script setup lang="ts">
import { onMounted } from "vue";
import { BgType } from "../enums";

const emit = defineEmits<{
    (e: "bg-change", type?: BgType): void
}>();

onMounted(() => {
    // emit("bg-change", BgType.NoBackgrounds);
    // emit("bg-change", BgType.NoBlurBackground);
    // emit("bg-change", BgType.NoBackgroundPicture);
    emit("bg-change", BgType.Default);
})
</script>

<template>
    <form action="">
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
        <router-link to="/sign-up" class="">Create a new Account</router-link>
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

.block>input {
    width: 95%;
    max-width: 250px;
}
</style>