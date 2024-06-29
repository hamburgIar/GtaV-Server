<template>
  <div class="content" id="auth_app">
    <div class="mainLogin" v-if="page === 0">
      <div class="main-form">
        <h1 class="auth-text">Авторизация</h1>
        <input type="text" placeholder="Введите логин" v-model="loginData.login" />
        <input type="password" placeholder="Введите пароль" v-model="loginData.pass" />
        <p class="error-text">{{ errorText }}</p>
        <button class="main-button" @click="tryAuth()">Войти</button>
        <div class="reg-texts">
          <p class="no-acc-text">Нет аккаунта? 
            <span class="create-acc-text">
              <a href="#" class="create-acc" @click="switchPage(1)">Создать сейчас</a>
            </span>
          </p>
        </div>
      </div>
    </div>

    <div class="mainRegister" v-if="page === 1">
      <div class="main-form">
        <h1 class="auth-text">Регистрация</h1>
        <input type="text" placeholder="Придумайте логин" v-model="regData.login" />
        <input type="text" placeholder="Введите ваш E-mail" v-model="regData.email" />
        <input type="password" placeholder="Придумайте пароль" v-model="regData.pass" />
        <input type="text" placeholder="Введите промокод" v-model="regData.promo" />
        <p class="error-text">{{ errorText }}</p>
        <button class="main-button" @click="tryReg()">Зарегистрироваться</button>
        <div class="reg-texts">
          <p class="no-acc-text">Уже есть аккаунт? 
            <span class="create-acc-text">
              <a href="#" class="create-acc" @click="switchPage(0)">Войти</a>
            </span>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'
import '../assets/colors.css'

const errorText = ref("")
const page = ref(0)

const loginData = ref({
  login: '',
  pass: ''
})
const regData = ref({
  login: '',
  email: '',
  pass: '',
  promo: ''
})

function setErrorText(text) {
  errorText.value = text;
}

function switchPage(index) {
  errorText.value = "";
  page.value = index;
}

function isEmpty(str) {
  return str.trim() === '';
}

function tryAuth() {
  if (isEmpty(loginData.value.login) && isEmpty(loginData.value.pass)) {
    setErrorText("Заполните все поля")
    return;
  } else if (!loginData.value.login.length) {
    setErrorText("Введите логин")
    return;
  } else if (!loginData.value.pass.length) {
    setErrorText("Введите пароль")
    return;
  }

  if ('alt' in window) alt.emit('CEF::CLIENT::SendToServer', "CLIENT::SERVER::TryLogin", loginData.value.login, loginData.value.pass);
}

function tryReg() {
  if (
    isEmpty(regData.value.login) &&
    isEmpty(regData.value.email) &&
    isEmpty(regData.value.pass) &&
    isEmpty(regData.value.rPass)
  ) {
    setErrorText("Заполните все поля");
    return;
  } else if (regData.value.login.length < 3) {
    setErrorText("Логин должен содержать минимум 3 символа")
    return;
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(regData.value.email)) {
    setErrorText("Неверный формат E-mail");
    return; 
  } else if (regData.value.pass.length < 4) {
    setErrorText("Пароль должен содержать минимум 4 символа")
    return;
  } else if (!/^(?!\d+$)[a-zA-Z0-9]+$/.test(regData.value.login)) {
    setErrorText("Логин должен содержать буквы и цифры, и не состоять только из цифр");
    return;
  } else if (regData.value.login.length > 15) {
    setErrorText("Логин не должен содержать больше 15 символов")
    return;
  } else if (regData.value.pass.length > 20) {
    setErrorText("Пароль не должен содержать больше 20 символов")
    return;
  }
            
  if ('alt' in window) alt.emit('CEF::CLIENT::SendToServer', 'CLIENT::SERVER::TryReg', 
    regData.value.login, 
    regData.value.email, 
    regData.value.pass, 
    regData.value.promo
  );
}

watch(loginData, (newValue) => {
  loginData.value.login = newValue.login.trim();
  loginData.value.pass = newValue.pass.trim();
}, { deep: true })

watch(regData, (newValue) => {
  regData.value.login = newValue.login.trim();
  regData.value.email = newValue.email.trim();
  regData.value.pass = newValue.pass.trim();
  regData.value.promo = newValue.promo.trim();
}, { deep: true })

onMounted(async () => {
  alt.on("CLIENT::CEF::AuthError", (error) => {
    errorText.value = error;
  });
});
</script>

<style scoped>
.content {
  width: 530px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  user-select: none;
}

.mainLogin {
  height: 360px;
  border-radius: 15px;
  padding: 20px;
  background-color: var(--main-bg-color);
}
  
.mainRegister { 
  height: 500px;
  border-radius: 15px;
  padding: 20px;
  background-color: var(--main-bg-color);
}
  
.main-form {
  width: 100%;
  height: 100%;
}
  
.auth-text {
  font-weight: 700;
  color: rgb(236, 232, 232);
  margin: 0 0 30px;
}
  
input {
  width: calc(100% - 34px);
  height: 50px;
  border: 2px solid #616161;
  border-radius: 7px;
  outline: none;
  background-color: rgb(53, 51, 51);
  transition: 0.4s;
  padding: 0 15px;
  margin: 0 0 22px 0;
}
  
input:hover { 
  border-color: #838383;
}
  
input:focus {
  border-color: var(--main-color);
}
  
::-webkit-input-placeholder {
  color: rgb(185, 180, 180);
}
  
input[type="text"] {
  font-size: 16px;
  color: rgb(236, 232, 232);
  letter-spacing: 0.07em;
}
  
input[type="password"] {
  font-size: 16px;
  color: rgb(236, 232, 232);
  letter-spacing: 0.07em; 
}
  
.main-button {
  width: 100%;
  background-color: var(--main-color);
  height: 47px;
  outline: none;
  border: none;
  border-radius: 7px;
  margin-top: 5px;
  color: rgb(236, 232, 232);
  font-size: 16px;
  font-weight: 700;
  letter-spacing: 0.1em;
}
  
.reg-texts {
  width: 100%;
  height: 50px;
  margin-top: 13px;
  color: rgb(138, 135, 135);
  padding: 10px 0 10px;
  text-align: center;
}
  
.no-acc-text {
  font-size: 16px;
  font-weight: 600;
  width: 100%;
}
  
.create-acc-text {
  color: var(--main-color);
}
  
.create-acc {
  text-decoration: none;
  color: var(--main-color);
}
  
.error-text {
  color: rgba(255, 0, 0, 0.991);
  font-size: 15px;
  font-weight: 400;
  padding-left: 2px;
  letter-spacing: 0.5px;
  padding-bottom: 10px;
  font-family: "Roboto", sans-serif;
}
</style>