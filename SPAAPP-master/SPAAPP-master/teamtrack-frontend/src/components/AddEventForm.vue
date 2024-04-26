<template>
  <div class="ads">
    <h1 class="font-bold">Lisa uus sündmus</h1>

    <div class="min-h-full flex items-center justify-center py-0 px- sm:px-6 lg:px-8">
      <form class="max-w-md w-full space-y-9">
        <div class="mt-5 space-y-6">
          <div class="rounded-md shadow-sm -space-y-px">
            <div>
              <label for="name">Sündmuse tüüp</label>
              <input
                id="name"
                name="name"
                v-model="event.type"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Nimetus"
              />
            </div>
            <br />
            <div>
              <label for="name">Kirjeldus</label>
              <input
                id="description"
                name="description"
                v-model="event.description"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Kirjeldus"
              />
            </div>
            <br />
            <div>
              <label for="location">Toimumise koht</label>
              <input
                id="location"
                name="location"
                v-model="event.location"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Toimumise koht"
              />
            </div>
            <br />
            <div>
              <label for="date">Kuupäev</label>
              <input
                id="date"
                name="date"
                v-model="event.date"
                type="date"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Kuupäev"
              />
            </div>
            <br />
            <div>
              <label for="time">Kellaaeg</label>
              <input
                id="time"
                name="time"
                v-model="time"
                type="time"
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
              />
            </div>
            <br />
            <button
              @click.prevent="submitForm"
              class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              Lisa sündmus
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watchEffect } from 'vue';
import { useRouter } from 'vue-router';
import { useEventsStore } from "@/stores/eventsStore";
import { Event } from '@/models/event';
import { defineProps } from 'vue';

const props = defineProps({
  event: {
    type: Object as () => Event,
    required: true
  }
});

const localEvent = ref({...props.event});
const time = ref('');

const { addEvent, updateEvent } = useEventsStore();
const router = useRouter();

watchEffect(() => {
  localEvent.value = { ...props.event };
  // Split the ISO date to date and time
  const dateTime = props.event.date.split('T');
  localEvent.value.date = dateTime[0];
  time.value = dateTime[1]?.substring(0, 5) || '';
});

const submitForm = () => {
  // Combine the date and time to a full ISO string
  const isoDate = `${localEvent.value.date}T${time.value}:00.000Z`;
  const eventData = { ...localEvent.value, date: isoDate };

  if (Number(localEvent.value.id) === 0) {
    addEvent(eventData);
  } else {
    updateEvent(eventData);
  }

  // Navigate to the events page after submitting
  router.push({ name: "Treeneri Sündmused" });
};
</script>

<style>
/* General styles */
.min-h-full {
  background-color: lightblue; /* A softer shade of grey */
  color: #111827; /* Dark grey for better readability */
}

.ads {
  background-color: lightblue; /* A softer shade of grey */
  color: #111827; /* Dark grey for better readability */
}


.ring{
  color: lightskyblue ;
}

.ring:hover{
  animation: colorchange1 1s infinite;
}
.delete{

  font-weight: bold;
  color: white;
  background-color: rgb(255, 0, 0);
  padding: 0.00000005rem 0.5rem;
}
.delete:hover{
  animation: colorchange2 1s infinite;
}
.details{
font-weight: bold;
color: white;
background-color: rgb(37, 179, 37);
padding: 0.00000005rem 0.5rem;
}
.details:hover{
  animation: colorchange3 1s infinite;
}
/* Typography and spacing */
h1 {
  padding: 1rem 0; /* More vertical padding */
  font-size: 2.25rem; /* Larger size for title */
  line-height: 2.5rem;
  margin-bottom: 2rem; /* Space below the title */
}

/* Buttons and links */
button, .router-link {
  padding: 0.5rem 1rem; /* Increased padding for larger clickable area */
  margin: 0 0.25rem; /* Slight separation between buttons */
  border: none; /* Remove default borders */
  font-weight: 600; /* Semi-bold font for better legibility */
  transition: background-color 0.3s; /* Smooth background transition for hover effect */
}

button:hover, .router-link:hover {
  background-color: darken(bg-color, 10%); /* Darken button on hover for feedback */
}

.popup {
  /* ... existing styles ... */
  display: flex; /* Keep this to align the popup */
  align-items: center;
  justify-content: center;
  transition: opacity 0.3s ease-in-out; /* Smooth fade transition */
  z-index: 100; /* High z-index to ensure visibility above other content */
}

.popup-inner {
  background-color: rgb(76, 202, 76); /* White background for the popup */
  padding: 40px; /* More padding for a spacious look */
  border-radius: 15px; /* Rounded corners */
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2); /* Softer shadow for depth */
  position: relative; /* For absolute positioning of close button */
  width: 90%; /* Max width to support responsiveness */
  max-width: 500px; /* Maximum width of the popup */
  transition: transform 1.3s ease-in-out; /* Transform transition for a pop effect */
  transform: scale(1.05); /* Start slightly larger */
}

.popup-inner h2 {
  margin-top: 0; /* Remove margin at the top of the h2 */
  margin-bottom: 1rem; /* Space below the title */
  color: #333; /* Dark color for the title */
  font-size: 1.75rem; /* Larger size for the title */
}

.popup-inner ul {
  list-style: none; /* Remove list styles */
  padding: 0; /* Remove padding */
  margin: 0; /* Remove margins for a clean look */
}

.popup-inner ul li {
  padding: 0.5rem 0; /* Padding for each list item */
  border-bottom: 1px solid #eee; /* Separator for items */
}

.popup-inner ul li:last-child {
  border-bottom: none; /* No border for the last item */
}

.popupClose {
  position: absolute;
  top: 10px;
  right: 10px;
  width: 30px; /* Width and height should be the same for a circle */
  height: 30px;
  line-height: 30px; /* Line height equal to height to center the text vertically */
  text-align: center; /* Center the text horizontally */
  border-radius: 50%; /* Perfect circle */
  font-size: 16px; /* Adjust the font size as needed */
  transition: background-color 0.2s, transform 0.2s;
  cursor: pointer;
  display: flex; /* Use flexbox to center content */
  align-items: center; /* Center vertically */
  justify-content: center; /* Center horizontally */
  padding: 0; /* Remove padding to prevent misalignment */
}

.popupClose:hover,
.popupClose:focus {
  animation: colorchange 1s infinite; /* Darker shade for hover effect */
  outline: none; /* Remove outline on focus for a cleaner look */
}

/* Showing and hiding the popup */
.popup-enter-active, .popup-leave-active {
  transition: opacity 0.3s ease-in-out;
}

.popup-enter, .popup-leave-to /* .popup-leave-active below version 2.1.8 */ {
  opacity: 0;
  transform: scale(1);
}

/* DataTable styling */
.DataTable {
  /* Assuming you have a custom class to target */
  width: 100%; /* Full width */
  border-collapse: collapse; /* Collapsed borders for a clean look */
  margin-top: 1rem; /* Space above the table */
}

.Column {
  /* Assuming custom classnames for Column */
  text-align: right; /* Align text to the left for readability */
  padding: 0.75rem 1rem; /* Padding within cells */
}

/* Media queries for responsiveness */
@media (min-width: 768px) {
  .min-h-screen {
    padding: 2rem; /* More padding on larger screens */
  }
}
@keyframes colorchange{
  0% {color: red;}
  100% {color: rgb(76, 202, 76);}

}

@keyframes colorchange1{
  0% {color: blue}
  100% {color: lightskyblue}

}
@keyframes colorchange2{
  0% {color: red}
  100% {color: white;}

}
@keyframes colorchange3{
  0% {color: rgb(37, 179, 37)}
  100% {color: white;}

}
</style>

