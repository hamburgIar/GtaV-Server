<template>
    <div class="content">
        <h1 class="main-text">Добро пожаловать</h1>
        <h1 class="main-text top">Нажмите Enter</h1>
    </div>
</template>

<script setup>

</script>

<style scoped>
.content {
    padding: 100px;
    height: calc(100vh - 200px);
    position: relative;
}

.main-text {
    font-size: 50px;
    color: white;
}

.top {
    position: absolute;
    bottom: 100px;  
    left: 100px;
}
</style>