<script  setup lang="ts">import { onMounted } from 'vue';
import { BgType } from '../enums';


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
    
</template>

<style scoped>

</style>