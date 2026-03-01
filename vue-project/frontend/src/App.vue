<script setup>
import { ref } from 'vue'

const timeSelection = ref('Departure')
const selectedOrigin = ref('Hamburg')
const selectedDestination = ref('Berlin')
import Datepicker from 'vuejs3-datepicker';
const state = ref({
  date: new Date()
})
</script>

<template>
  <h2 class="row">Enter your travel details and find out if any fans are taking your train</h2>
  
  <div class="row">
    <h4 class="block">Origin</h4>
    <select class="block" v-model="selectedOrigin">
      <option>Hamburg</option>
      <option>Berlin</option>
      <option>Munich</option>
      <option>Cologne</option>
    </select>
  </div>

  <div class="row">
    <h4 class="block">Destination</h4>
    <select class="block" v-model="selectedDestination">
      <option>Hamburg</option>
      <option>Berlin</option>
      <option>Munich</option>
      <option>Cologne</option>
    </select>
  </div>

  <div class="row">
    <select class="block" v-model="timeSelection">
      <option>Departure</option>
      <option>Arrival</option>
    </select>
    <Datepicker v-model="state.date"></Datepicker>
  </div>
  
</template>

<style scoped>
.row {
  width: 100%;
  text-align: center;
}
.block {
  width: 100px;
  display: inline-block;
}

</style>