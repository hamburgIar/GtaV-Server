<template>
    <div class="content">
       <div class="main-content">
            <div class="gender-selection">
                <h1 class="section-name">ВЫБОР ПОЛА И НАСЛЕДСТВЕННОСТИ</h1>
                <div class="parameters">
                    <div class="parameter">
                        <h2 class="parameter-name">Пол персонажа</h2>
                        <div class="parameter-value">
                            <button class="gender-button" :class="{ active: gender === data.gender['male'] }" @click="gender = data.gender['male']">Мужской</button>
                            <button class="gender-button right" :class="{ active: gender === data.gender['female'] }" @click="gender = data.gender['female']">Женский</button>
                        </div>
                    </div>
                    <div class="parameter mother">
                        <h2 class="m-parameter-name">Мама</h2>
                        <h2 class="mother-name">Mother</h2>
                        <div class="parameter-value">
                            <button class="mother-button" style="float: right;">></button>
                        </div>
                    </div>
                </div>
            </div>
       </div>
    </div>
</template>
  
<script setup>
import '../assets/colors.css'
import * as data from '../data/characters.js'
import {ref} from 'vue'

const gender = ref(data.gender['male'])
</script>

<style scoped>
.content {
    height: calc(100vh - 80px);
    padding: 40px;
}
.main-content {
    width: 500px;
    height: 100%;
    float: right; 
    background-color: var(--main-bg-color);
    border-radius: 20px;
}
.gender-selection {
    padding: 25px;
    max-width: calc(100% - 50px);
    max-height: calc(100% - 50px);
    height: 100%;
}
.section-name {
    color: white;
    font-size: 22px;
    padding-bottom: 20px
}
.parameters {
    height: calc(100% - 50px);
}
.parameter {
    background-color: rgb(53, 51, 51);
    border-radius: 5px;
    padding: 10px;
    margin-bottom: 15px;
}
.parameter-name {
    color: white;
    font-size: 18px;
    padding-bottom: 10px;
    font-weight: 300;
    padding: 0;
    margin-bottom: 7px
}
.m-parameter-name {
    color: white;
    font-size: 18px;
    padding-bottom: 10px;
    font-weight: 300;
    padding: 0;
    margin-top: 3px;
}
.mother-name {
    color: white;
    font-size: 18px;
    padding-bottom: 10px;
    font-weight: 300;
    padding: 0;
    margin-top: 3px;
}
.gender-button {
    width: calc(50% - 5px);
    height: 35px;
    outline: none;
    border: none;
    border-radius: 5px;
    background-color: #4c4c4c;
    color: rgb(188, 188, 188);
    font-size: 15px;
    font-weight: 500;
    transition: 0.3s;
}
.right {
    float: right;
}
.active {
    background-color: var(--main-color);
    color: white;
}
.mother {
    display: flex;
}
</style>