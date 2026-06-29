import {createRouter, createWebHistory} from 'vue-router'

import HomeView from "@/views/HomeView.vue";
import JourneysView from "@/views/JourneysView.vue";

const routes = [
    { path: '/', name: 'home', component: HomeView },
    { path: '/journeys', name: 'journeys', component: JourneysView },
]

export default createRouter({ history: createWebHistory(process.env.BASE_URL), routes})