<template>
    <div>
        <v-theme-provider v-if="!showEditForm" theme="dark" with-background class="pa-10"
            style="cursor: pointer">
            <v-card class="mx-auto" max-width="344">
                <v-img src="https://cdn.vuetifyjs.com/images/cards/sunshine.jpg" height="200px" cover></v-img>

                <v-card-title>
                    {{ task.taskName }}
                </v-card-title>
                <div class="d-flex justify-space-between mb-6">
                    <div>
                        <v-switch color="primary" :model-value="task.completed" :label="toggleButtonLabel()"
                            @click="changeStatus(task.id)"></v-switch>
                    </div>
                    <div>
                        <v-btn color="red-darken-4" @click="deleteTask(task.id)">
                            Delete Task
                        </v-btn>

                    </div>
                </div>

                <v-card-subtitle>
                    {{ task.description }} by {{ task.createdBy }}
                </v-card-subtitle>

                <v-card-subtitle>
                    Location: {{ task.location }}
                </v-card-subtitle>
                <v-sheet class="d-flex flex-wrap justify-space-around mb-6">
                    <v-card-actions color="orange-lighten-2">
                        <v-btn color="orange-lighten-2" variant="text" @click="show = !show">
                            Read More
                        </v-btn>
                        <v-btn color="red-lighten-2" variant="text" @click="viewEditForm()">
                            Edit Task Info
                        </v-btn>
                    </v-card-actions>
                </v-sheet>

                <v-expand-transition>
                    <div v-show="show">
                        <v-divider></v-divider>

                        <v-card-text :v-show="task.extendedInfo">
                            {{ task.extendedInfo }}
                        </v-card-text>
                    </div>
                </v-expand-transition>
                <br />
            </v-card>
        </v-theme-provider>
        <div v-else class="edit-form">
            <EditTaskForm :task="task"/>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import EditTaskForm from './EditTaskForm.vue';
import {
    mdiDelete,
} from '@mdi/js'
export default {
    data: () => ({
        show: false,
        showEditForm: false,
        mdiDelete
    }),
    components: {EditTaskForm},
    created() { },
    props: { task: Object },
    methods: {
        toggleButtonLabel() {
            return this.task.completed ? "completed" : "incomplete"
        },
        changeStatus(id) {
            axios.put(`http://localhost:5075/api/v1/Task/EditTaskStatus/${id}`)
                .then(location.reload())
                .catch(err => console.log(err))
        },
        deleteTask(id) {
            axios.delete(`http://localhost:5075/api/v1/Task/DeleteTask/${id}`)
                .then(location.reload())
                .catch(err => console.log(err))
        },
        viewEditForm(){
            this.showEditForm = !this.showEditForm;
        }
    }
}
</script>

<style lang="scss" scoped></style>