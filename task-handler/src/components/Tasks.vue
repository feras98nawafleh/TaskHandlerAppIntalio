<template>
  <div class="d-flex align-center flex-column pa-2 rounded-card">
    <v-card class="bg-green-accent-2" width="400" :title="name" subtitle="use it to manage your todos">
      <div class="mb-6 text-center">
        <v-btn @click="showForm()" :color="this.formIsVisible ? 'red' : 'green'">
          {{ this.formIsVisible ? "Cancel" : "Add New" }}
        </v-btn>
      </div>
      <div v-if="this.formIsVisible">
        <TaskForm />
      </div>
      <div v-else v-for="task in tasks" :key="task">
        <Task :task="task" />
      </div>
    </v-card>
  </div>
</template>

<script>
import Task from './Task.vue';
import TaskForm from './TaskForm.vue';
import axios from 'axios';
export default {
  data() {
    return {
      tasks: [],
      formIsVisible: false,
    }
  },
  components: { Task, TaskForm },
  props: { name: String },
  created() {
    axios
      .get("http://localhost:5075/api/v1/Task/GetAll")
      .then((response) => {
        if (response.status === 200)
          this.tasks = response.data;
      });
  },
  methods: {
    showForm() {
      this.formIsVisible = !this.formIsVisible;
    }
  },
}
</script>

<style lang="css" scoped></style>

