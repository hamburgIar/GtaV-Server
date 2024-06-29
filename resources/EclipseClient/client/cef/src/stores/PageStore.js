import { defineStore } from 'pinia'

export const usePageStore = defineStore('pageStore', {
  state: () => (
    { 
      openStart: false,
      openAuth: false,
      openCharacterCreation: false,
    }
  )
})