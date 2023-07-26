<template>
    <div>
        <v-form v-on:submit="handleSubmit(task.id)" ref="taskForm">
            <v-text-field color="green darken-1" v-model="TaskName" :rules="taskFormRules" label="Task Name"></v-text-field>
            <v-text-field color="green darken-1" v-model="Description" :rules="taskFormRules" label="Description"></v-text-field>
            <v-text-field color="green darken-1" v-model="ExtendedInfo" label="Extended Info"></v-text-field>
            <v-text-field color="green darken-1" v-model="Location" :rules="taskFormRules" label="Location"></v-text-field>
            <v-select label="User" v-model="User" :rules="taskFormRules" :items="['User 1', 'User 2', 'User 3']"></v-select>
            <div class="mb-6 text-center">
                <v-btn color="green" type="submit">Confirm</v-btn>
            </div>
        </v-form>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    created(){console.log(this.task);},
    data() {
        return {
            taskFormRules: [
                v => !!v || 'Field is required',
                v => (v && v.length <= 50) || 'Field input must be less than 50 characters',
            ],
            TaskName: this.task.taskName,
            Description: this.task.description,
            ExtendedInfo: this.task.extendedInfo,
            Location: this.task.location,
            selectedOption: this.task.user, 
            User: this.task.user,
            Completed: this.task.completed
        }
    },
    props: {task: Object},
    methods: {
        handleSubmit(id) {
            this.$refs.taskForm.validate().then(validity => {
                if (validity.valid) {
                    axios.put(`http://localhost:5075/api/v1/Task/EditTask/${id}`, {
                        TaskName: this.TaskName,
                        Description: this.Description,
                        ExtendedInfo: this.ExtendedInfo,
                        CreatedBy: this.User,
                        Location: this.Location,
                        Completed: false
                    }, {
                        headers: {
                            'Access-Control-Allow-Origin': '*',
                            'Content-Type': 'application/json',
                        },
                    })
                        .then(function (response) {
                            location.reload();
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                }
            })
        }
    }
}
</script>

<style lang="scss" scoped></style>