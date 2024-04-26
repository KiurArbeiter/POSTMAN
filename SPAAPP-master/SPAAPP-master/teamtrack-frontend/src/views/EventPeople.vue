<template> 
    <navbar />
    <EventList :title="title"/>
</template>
  
<script setup lang="ts">
import EventList from "../components/EventPeopleStore.vue"
    import { defineProps } from "vue";
    import navbar from "../components/Navbar.vue"
  defineProps<{ title: String}>();
  </script>