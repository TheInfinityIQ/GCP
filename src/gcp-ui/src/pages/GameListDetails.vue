<script  setup lang="ts">
import { onMounted } from 'vue';
import { BgType } from '../enums';
import AddGameModal from "../modal/AddGameModal.vue";
import AuthModal from "../modal/AuthModal.vue";

const emit = defineEmits<{
    (e: "bg-change", type?: BgType): void
}>()

onMounted(() => {
    emit("bg-change", BgType.NoBackgrounds);
    // emit("bg-change", BgType.NoBlurBackground);
    // emit("bg-change", BgType.NoBackgroundPicture);
})

let user: string = "testUser";
let users: [string] = [user];

for (let index = 0; index < 5; index++) {
    users.push(user);
}
</script>

<template>
    <auth-modal />
    <add-game-modal />
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
    <div class="big-input">
        <label for="add-user">Add User</label>
        <input type="text" id="add-user">
    </div>
    <article class="user-table">
        <section class="user-table-header">
            <h2>Username</h2>
            <h2>Selected</h2>
        </section>
        <section class="user-table-content">
            <ul>
                <li v-for="user in users" class="user-list">
                    <div class="inline user-row">
                        <p>{{ user }}</p>
                        <input type="checkbox">
                    </div>
                </li>
            </ul>
        </section>
        <div class="big-input">
            <label for="search-user">Search User</label>
            <input type="text" id="search-user">
        </div>
        <div class="user-row found-user">
            <p>TestUserName</p>
            <input type="checkbox">
        </div>
        <input type="submit" value="Remove Selected">
    </article>
    <section>
    </section>
    <input type="submit" value="Apply Changes">
</template>

<style>

.user-table-content > ul {
    max-height: 15em;
    min-height: 5em;
    overflow: scroll;
    list-style: none;
    padding: 0;
}

.user-row {
    display: flex;
    justify-content: space-between;
    width: 100%;
    padding: 0;
}

.column {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.user-table-header > h2 {
    font-size: 1em;
}

.user-table-header {
    display: flex;
    justify-content: space-between;
}
</style>