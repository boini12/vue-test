<script setup>
  import {ref, onMounted} from "vue";
  import { useRouter } from "vue-router";
  
  const router = useRouter();
  const journeys = ref([]);
  const loading = ref(false);
  const error = ref(false);
  
  onMounted(async() => {
    loading.value = true;
    try{
      const res = await fetch("http://localhost:5025/api/train/journeys",
          {
      method : "POST",
      headers:{"Content-Type":"application/json"},
      body:JSON.stringify({
        from: router.query.from,
        to: router.query.to,
        journeyTimeSelection: router.query.timeSelection,
        travelDate: router.query.date,
    }),
  })
  if(!res.ok) throw new Error(`HTPP ${res.status}`)
  journeys.value = await res.json()
  }catch(e){
    error.value = e.message;
  }finally{
    loading.value = false;
  }
  })
</script>

<template>
  <li v-for="(journey, index) in journeys" :key="index">
    {{journey}}
  </li>
</template>

<style scoped>

</style>