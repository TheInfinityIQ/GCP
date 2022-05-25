<script setup lang="ts">
import client from "../api"
import { RouterLink } from "vue-router";
import { ref } from "vue";

//TODO: Remove when auth fully implemented.
localStorage.clear();
console.log("AUTH MODAL: REMOVE LOCALSTORAGE.CLEAR WHEN AUTH FULLY IMPLEMENTED");
</script>

<template>
    <div class="modal-container">
        <figure>
            <img src="src/assets/TempOops.png" alt="">
            <figcaption>Oops! You need to be logged in to use this feature.</figcaption>
        </figure>
        <router-link to="/sign-up" class="">Log In or Sign up</router-link>
    </div>
</template>

<style>
figure>img {
    max-width: 100%;
}

figure>figcaption {
    color: white;
}
</style>