<template>
    <router-view />
</template>

<script setup lang="ts">
    import { defineProps } from "vue";
    defineProps<{ isAthlete: Boolean, hasChosen: Boolean}>();
</script>


