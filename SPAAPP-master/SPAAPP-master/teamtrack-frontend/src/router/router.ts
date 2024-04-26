import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import EventsVue from '@/views/Events.vue';
import AddEventVue from '@/views/AddEvent.vue';
import UpdateEventVue from '@/views/UpdateEvent.vue'
import HomePageViewVue from '@/views/HomePageView.vue';
import EventPeopleVue from '@/views/EventPeople.vue';
import PeopleVue from '@/views/EventPeople.vue';
import EventPeopleStore from '@/views/PeopleStore.vue';


const routes: Array<RouteRecordRaw> = [
  { 
    path: '/',
    name: 'Avaleht',
    component: HomePageViewVue,
  },
  {
    path: '/athleteEvents',
    name: 'Sportlase Sündmused',
    component: EventsVue,
    props: { title: 'Sportlase Sündmused', isAthlete: true},
  },
  {
    path: '/coachEvents',
    name: 'Treeneri Sündmused',
    component: EventsVue,
    props: { title: 'Treeneri Sündmused', isAthlete: false},
  },
  {
    path: '/eventPeople',
    name: 'Isikute sündmused',
    component: PeopleVue,
    props: { title: 'Isikute sündmused'},
  },
  {
    path: '/peopleStore',
    name: 'Isikud',
    component: EventPeopleStore,
    props: { title: 'Inimesed'},
  },
  {
    path: '/newevent',
    name: 'Lisa uus sündmus',
    component: AddEventVue,
  },
  {
    path: '/update/:id',
    name: 'Uuenda harjutust',
    component: UpdateEventVue,
  },

];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
