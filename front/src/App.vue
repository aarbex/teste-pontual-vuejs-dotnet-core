<template>
  <v-app id="app">
    <v-main>
      <Nav/>
      <router-view :key="$route.fullPath">        
      </router-view>
    </v-main>
  </v-app>
</template>

<script>
import Nav from './components/Nav/Nav' 

export default {
  name: 'App',

  components: {
    Nav
  },

  data: () => ({
    //
  }),
};
</script>
