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
        <div class="field">
          <span class="label">Origin</span>
          <select v-model="selectedOrigin" class="value-select">
            <option v-for="city in cities" :key="city">{{ city }}</option>
          </select>
        </div>

        <button class="swap-btn" @click="swapCities" aria-label="Swap cities">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <path d="M7 16V4m0 0L3 8m4-4l4 4"/><path d="M17 8v12m0 0l4-4m-4 4l-4-4"/>
          </svg>
        </button>

        <div class="field">
          <span class="label">Destination</span>
          <select v-model="selectedDestination" class="value-select">
            <option v-for="city in cities" :key="city">{{ city }}</option>
          </select>
        </div>
      </div>

      <div class="departure-row">
        <div class="field type-field">
          <span class="label">When</span>
          <select v-model="timeSelection" class="value-select">
            <option>Departure</option>
            <option>Arrival</option>
          </select>
        </div>
        <div class="field date-field">
          <Datepicker v-model="state.date" />
        </div>
        <div class="field time-field">
          <input type="time" v-model="selectedTime" class="time-input" />
        </div>
      </div>

      <button class="cta-btn" @click="findJourneys">Find train journeys</button>
    </div>
  </div>
</template>

<style scoped>
/* One shared design language for every control on this page:
   same surface, same border, same radius, same focus ring. */
.page {
  --accent: #7c3aed;
  --accent-strong: #6d28d9;
  --accent-soft: #ede9fe;
  --surface: #ffffff;
  --border: #e2ddec;
  --text: #1c1830;
  --text-muted: #6b6580;
  --radius: 14px;
  --control-height: 60px;

  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px 16px;
  background: #f4f2f8;
  color-scheme: light;
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
  color: var(--text);
  text-align: center;
  line-height: 1.25;
  margin: 0 0 8px;
}

/* Shared control shell */
.field {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  min-height: var(--control-height);
  padding: 0 18px;
  border: 1.5px solid var(--border);
  border-radius: var(--radius);
  background: var(--surface);
  transition: border-color 0.2s, box-shadow 0.2s;
}

.field:hover {
  border-color: #cfc6e4;
}

.field:focus-within {
  border-color: var(--accent);
  box-shadow: 0 0 0 3px var(--accent-soft);
}

.label {
  color: var(--text-muted);
  font-size: 0.9rem;
  font-weight: 600;
  letter-spacing: 0.01em;
  white-space: nowrap;
}

.value-select,
.time-input {
  background: transparent;
  border: none;
  outline: none;
  color: var(--text);
  font-family: inherit;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  padding: 0;
}

.value-select {
  text-align: right;
  appearance: auto;
  -webkit-appearance: auto;
}

.time-input {
  width: 100%;
  text-align: center;
}

/* Route group */
.route-group {
  position: relative;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.swap-btn {
  position: absolute;
  right: -12px;
  top: 50%;
  transform: translateY(-50%);
  width: 42px;
  height: 42px;
  border-radius: 50%;
  background: var(--accent);
  border: none;
  color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  z-index: 1;
  transition: background 0.2s;
  box-shadow: 0 2px 10px rgba(124, 58, 237, 0.35);
}

.swap-btn:hover {
  background: var(--accent-strong);
}

/* Departure row */
.departure-row {
  display: flex;
  align-items: stretch;
  gap: 8px;
  flex-wrap: wrap;
}

.type-field {
  flex: 1 1 100%;
}

.date-field,
.time-field {
  flex: 1 1 0;
  min-width: 140px;
  justify-content: center;
}

/* The datepicker ships its own bordered box and a green accent —
   strip the box so `.field` is the only shell, and repaint it purple. */
.date-field :deep(.vuejs3-datepicker) {
  display: block;
  width: 100%;
}

.date-field :deep(.vuejs3-datepicker__value) {
  border: none;
  border-radius: 0;
  min-width: 0;
  width: 100%;
  padding: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  color: var(--text);
  font-size: 1rem;
  font-weight: 600;
}

.date-field :deep(.vuejs3-datepicker__content) {
  margin-left: 8px;
  font-size: 1rem;
}

.date-field :deep(.vuejs3-datepicker__icon svg) {
  color: var(--text-muted);
}

.date-field :deep(.vuejs3-datepicker__calendar) {
  border: 1.5px solid var(--border);
  border-radius: var(--radius);
  box-shadow: 0 8px 24px rgba(28, 24, 48, 0.12);
  overflow: hidden;
}

.date-field :deep(.vuejs3-datepicker__calendar-topbar) {
  background-color: var(--accent);
  border-radius: 0;
}

.date-field :deep(.vuejs3-datepicker__calendar .cell.selected),
.date-field :deep(.vuejs3-datepicker__calendar .cell.selected:hover),
.date-field :deep(.vuejs3-datepicker__calendar .cell.highlighted) {
  background: var(--accent);
  color: #fff;
}

.date-field :deep(.vuejs3-datepicker__calendar .cell:not(.blank):not(.disabled).day:hover),
.date-field :deep(.vuejs3-datepicker__calendar .cell:not(.blank):not(.disabled).month:hover),
.date-field :deep(.vuejs3-datepicker__calendar .cell:not(.blank):not(.disabled).year:hover) {
  border: 1px solid var(--accent);
}

/* CTA — same radius as the fields, filled with the accent */
.cta-btn {
  width: 100%;
  min-height: var(--control-height);
  padding: 16px;
  border-radius: var(--radius);
  background: var(--accent);
  color: #ffffff;
  font-family: inherit;
  font-size: 1.05rem;
  font-weight: 700;
  border: 1.5px solid var(--accent);
  cursor: pointer;
  transition: background 0.2s, transform 0.1s;
  margin-top: 4px;
}

.cta-btn:hover {
  background: var(--accent-strong);
  border-color: var(--accent-strong);
}

.cta-btn:active {
  transform: scale(0.98);
}

.cta-btn:focus-visible {
  outline: none;
  box-shadow: 0 0 0 3px var(--accent-soft);
}

@media (min-width: 640px) {
  .card {
    gap: 24px;
  }
  .field {
    padding: 0 22px;
  }
  .type-field {
    flex: 0 0 auto;
  }
}
</style>
