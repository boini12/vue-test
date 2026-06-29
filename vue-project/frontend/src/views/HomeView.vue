<script setup>
import { ref } from 'vue'

import { useRouter } from "vue-router";
const router = useRouter()
const findJourneys = () => {
  router.push({
    name: 'journeys',
    query: {
      from: selectedOrigin.value,
      to: selectedDestination.value,
      timeSelection: timeSelection.value,
      date: state.value.date.toISOString(),
      time: selectedTime.value,
    }
  })
}

import Datepicker from 'vuejs3-datepicker'

const timeSelection = ref('Departure')
const selectedOrigin = ref('Hamburg')
const selectedDestination = ref('Hannover')
const selectedTime = ref('16:47')
const state = ref({ date: new Date() })

const cities = ['Hamburg', 'Berlin', 'Munich', 'Cologne', 'Hannover', 'Frankfurt']

const swapCities = () => {
  const tmp = selectedOrigin.value
  selectedOrigin.value = selectedDestination.value
  selectedDestination.value = tmp
}
</script>

<template>
  <div class="page">
    <div class="card">
      <h1 class="heading">Enter your travel details and find out if any fans are taking your train!</h1>

      <div class="route-group">
        <div class="route-row">
          <span class="label">Origin</span>
          <select v-model="selectedOrigin" class="city-select">
            <option v-for="city in cities" :key="city">{{ city }}</option>
          </select>
        </div>

        <button class="swap-btn" @click="swapCities" aria-label="Swap cities">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <path d="M7 16V4m0 0L3 8m4-4l4 4"/><path d="M17 8v12m0 0l4-4m-4 4l-4-4"/>
          </svg>
        </button>

        <div class="route-row">
          <span class="label">Destination</span>
          <select v-model="selectedDestination" class="city-select">
            <option v-for="city in cities" :key="city">{{ city }}</option>
          </select>
        </div>
      </div>

      <div class="departure-row">
        <select v-model="timeSelection" class="time-type-select">
          <option>Departure</option>
          <option>Arrival</option>
        </select>
        <div class="date-time-pills">
          <Datepicker v-model="state.date" class="date-pill" />
          <input type="time" v-model="selectedTime" class="time-pill" />
        </div>
      </div>

      <button class="cta-btn" @click="findJourneys">Find train journeys</button>
    </div>
  </div>
</template>

<style scoped>
.page {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px 16px;
  background: #0a0a0a;
}

.card {
  width: 100%;
  max-width: 480px;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.heading {
  font-family: Georgia, 'Times New Roman', serif;
  font-size: clamp(1.6rem, 5vw, 2.2rem);
  font-weight: 800;
  color: #ffffff;
  text-align: center;
  line-height: 1.25;
  margin: 0 0 8px;
}

/* Route group */
.route-group {
  position: relative;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.route-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  border: 1.5px solid #6366f1;
  border-radius: 12px;
  padding: 18px 20px;
  background: #111111;
  gap: 12px;
}

.label {
  color: #e5e7eb;
  font-size: 1rem;
  font-weight: 500;
  white-space: nowrap;
}

.city-select {
  background: transparent;
  border: none;
  color: #e5e7eb;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  outline: none;
  text-align: right;
  appearance: auto;
  -webkit-appearance: auto;
}

.city-select option {
  background: #1a1a2e;
  color: #e5e7eb;
}

.swap-btn {
  position: absolute;
  right: -12px;
  top: 50%;
  transform: translateY(-50%);
  width: 42px;
  height: 42px;
  border-radius: 50%;
  background: #6366f1;
  border: none;
  color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  z-index: 1;
  transition: background 0.2s;
  box-shadow: 0 2px 8px rgba(99, 102, 241, 0.5);
}

.swap-btn:hover {
  background: #4f46e5;
}

/* Departure row */
.departure-row {
  display: flex;
  align-items: center;
  gap: 12px;
  flex-wrap: wrap;
}

.time-type-select {
  background: transparent;
  border: none;
  color: #e5e7eb;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  outline: none;
  appearance: auto;
  -webkit-appearance: auto;
  padding: 2px 0;
  flex-shrink: 0;
}

.time-type-select option {
  background: #1a1a2e;
  color: #e5e7eb;
}

.date-time-pills {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
}

.date-pill :deep(input),
.time-pill {
  background: #1e1e1e;
  border: none;
  border-radius: 999px;
  color: #e5e7eb;
  font-size: 0.95rem;
  padding: 10px 18px;
  cursor: pointer;
  outline: none;
}

.date-pill :deep(input):focus,
.time-pill:focus {
  outline: 2px solid #6366f1;
}

.time-pill {
  color-scheme: dark;
}

/* CTA */
.cta-btn {
  width: 100%;
  padding: 16px;
  border-radius: 999px;
  background: #6366f1;
  color: #ffffff;
  font-size: 1.05rem;
  font-weight: 700;
  border: none;
  cursor: pointer;
  transition: background 0.2s, transform 0.1s;
  margin-top: 4px;
}

.cta-btn:hover {
  background: #4f46e5;
}

.cta-btn:active {
  transform: scale(0.98);
}

@media (min-width: 640px) {
  .card {
    gap: 24px;
  }
  .route-row {
    padding: 20px 24px;
  }
}
</style>
