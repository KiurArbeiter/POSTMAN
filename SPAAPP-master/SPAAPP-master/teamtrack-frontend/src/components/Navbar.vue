<template>

    <a href="/">Koduleht</a>
    <a href="/coachEvents">Olen Treener</a>
    <a href="/athleteEvents">Olen Sportlane</a>
    <a href="/newevent">Lisa sündmus</a>
    <a href="/eventPeople">Isikute Sündmused</a>
    <a href="/peopleStore">Inimesed</a>
</template>

<script lang="ts">

export default {
  props: {
    isAthlete: Boolean,
    hasChosen: Boolean,
  },
};
</script>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
a{
  color: black;
  font-family: Comic Sans;
  padding-left: 2mm;
  padding-right: 5mm;
}

a:hover{
  color: black;
  animation: color-change 14ms infinite;
  font-weight: bold;
}

@keyframes color-change {
  0% { color: whitesmoke; }
  100% { color: black; }
}

.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}

.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
