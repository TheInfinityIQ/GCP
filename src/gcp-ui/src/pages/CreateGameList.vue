<script  setup lang="ts">
import { onMounted } from 'vue';
import { BgType } from '../../enums';

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
        <div class="big-input">
            <label for="title">Title</label>
            <input type="text" id="title">
        </div>
        <div class="big-input">
            <label for="description">Description (100 Character Max)</label>
            <input type="text" id="description">
        </div>
        <section class="inline">
            <div class="column small-input">
                <label for="vote">Vote Once Per Game</label>
                <input type="checkbox" id="vote">
            </div>
            <div class="column small-input">
                <label for="public">Public to all Players</label>
                <input type="checkbox" id="public">
            </div>
        </section>
        <div class="big-input">
            <label for="user-limit">User Limit (Max 9999)</label>
            <input type="number" id="user-limit">
        </div>
        <input type="submit" value="Apply Changes">
    </form>
</template>

<style scoped>
.big-input,
.small-input {
    /* border: 1px solid white; */

    margin: 0.5em 0;
}

.column {
    display: flex;
    flex-direction: column;
}

label {}
</style>