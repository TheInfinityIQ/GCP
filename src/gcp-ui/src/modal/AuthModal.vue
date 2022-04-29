<script setup lang="ts">
import client from "../api"
import { RouterLink } from "vue-router";
import { ref } from "vue";

let authPath = ref("/sign-up");
let authPathText = ref("Sign Up");
localStorage.clear();

console.log(client.IsAuthenticated());

// client.GetLoginAsync("mod@gcp.com", "Password-1");

if (client.IsAuthenticated()) {
    console.log("Is authed");
    authPath = ref("/sign-in");
    authPathText = ref("Sign In");
}
</script>

<template>
    <div class="modal-container">
        <figure>
            <img src="src/assets/TempOops.png" alt="">
            <figcaption>Oops! You need to be logged in to use this feature.</figcaption>
        </figure>
        <router-link :to="authPath" class="">{{authPathText}}</router-link>
    </div>
</template> 

<style>
figure {}

figure>img {
    max-width: 100%;
}

figure>figcaption {
    color: white;
}
</style>