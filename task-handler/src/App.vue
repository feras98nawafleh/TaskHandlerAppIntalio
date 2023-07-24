<template>
  <div>
    <Tasks :name='title' />
  </div>
</template>

<script>
import Tasks from './components/Tasks.vue'
export default {
  data() {
    return {
      title: "Task Tracker App",
    }
  },
  components: {
    Tasks
  }
}
</script>

<style lang="scss" scoped></style>