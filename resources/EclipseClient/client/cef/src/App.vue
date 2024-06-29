<template>
  <Start v-if="pageStore.openStart"></Start>
  <Auth v-if="pageStore.openAuth"></Auth>
  <CharacterCreation v-if="pageStore.openCharacterCreation"></CharacterCreation>
</template>

<script setup>
import { onMounted } from 'vue'
import { usePageStore } from './stores/PageStore'

import Start from './components/Start.vue'
import Auth from './components/Auth.vue'
import CharacterCreation from './components/CharacterCreation.vue'

const pageStore = usePageStore()

onMounted(async () => {
  alt.on("CLIENT::CEF::OpenPage", (pageName, isOpen) => {
    switch (pageName) {
      case "Auth":
        if (isOpen) pageStore.openStart = false;
        pageStore.openAuth = isOpen;
        break;
      case "CharacterCreation":
        pageStore.openCharacterCreation = isOpen;
        break;
      default:
        console.log(`[PAGE OPEN] Unknown page: ${pageName}`);
        break;
    }
  });
});
</script>
