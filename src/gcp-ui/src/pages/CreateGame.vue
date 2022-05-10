<script setup lang="ts">
import { onMounted } from "vue";
import { BgType } from "../enums";

const emit = defineEmits<{
    (e: "bg-change", type?: BgType): void;
}>();

onMounted(() => {
    emit("bg-change", BgType.NoBackgrounds);
    // emit("bg-change", BgType.NoBlurBackground);
    // emit("bg-change", BgType.NoBackgroundPicture);
});
</script>

<template>
    <form action="">
        <div class="big-input">
            <label for="game-name">Game Name</label>
            <input type="text" id="game-name" />
        </div>
        <div class="big-input">
            <label for="description">Description (100 char max)</label>
            <input type="text" id="description" />
        </div>
        <input type="submit" value="Create Game"/>
    </form>
</template>

<style scoped></style>
