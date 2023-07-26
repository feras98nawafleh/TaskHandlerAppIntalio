<template>
    <div>
        <v-form v-on:submit="handleSubmit" ref="taskForm">
            <v-text-field color="green darken-1" v-model="TaskName" :rules="taskNameRules" ref="taskName" label="Task Name"></v-text-field>
            <v-text-field color="green darken-1" v-model="Description" :rules="taskNameRules" label="Description"></v-text-field>
            <v-text-field color="green darken-1" v-model="ExtendedInfo" label="Extended Info"></v-text-field>
            <v-text-field color="green darken-1" v-model="Location" :rules="taskNameRules" label="Location"></v-text-field>
            <v-select label="User" v-model="User" :rules="taskNameRules" :items="['User 1', 'User 2', 'User 3']"></v-select>
            <div class="mb-6 text-center">
                <v-btn color="green" type="submit">Add Task</v-btn>
            </div>
        </v-form>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    created(){console.log(this.task);},
    mounted() {
        this.$refs.taskName.focus();
    },
    data() {
        return {
            taskNameRules: [
                v => !!v || 'Field is required',
                v => (v && v.length <= 50) || 'Field input must be less than 50 characters',
            ],
            TaskName: "",
            Description: "",
            ExtendedInfo: "",
            Location: "",
            User: "",
            Completed: false
        }
    },
    props: {task: Object},
    methods: {
        handleSubmit() {
            this.$refs.taskForm.validate().then(validity => {
                if (validity.valid) {
                    axios.post('http://localhost:5075/api/v1/Task/AddTask', {
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