<template>
  <nav>
    <v-app-bar dark app color="blue darken-2" >
      <v-icon v-if="drawer" @click.stop="drawer = !drawer">mdi-arrow-collapse-left</v-icon>
      <v-icon v-else @click.stop="drawer = !drawer">mdi-menu</v-icon>           
        <v-app-bar-title
          cols="12"
          md="12"
          class="title text-uppercase font-weight-medium ps-5"
        >Pontual Systems</v-app-bar-title>
    </v-app-bar>
    <v-navigation-drawer
      dense
      dark
      app
      color="blue darken-2"
      :mobile-breakpoint="600"
      hide-overlay
      v-model="drawer"
      class="px-2"
    >
      <v-row>
        <h3 class="my-3 mx-auto"></h3>
      </v-row>
      <v-divider></v-divider>
      <v-list dense>
        <h4 class="ms-4">Cadastro de Clientes</h4>
        <router-link to="/" >
          <v-list-item class="mb-3" @click="drawer = !drawer">
            <v-list-item-icon>
              <v-icon small>mdi-home</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>Home</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </router-link>
        
       
        <router-link to="/clientes" >
          <v-list-item class="mb-2" @click="drawer = !drawer">
            <v-list-item-icon>
              <v-icon small>mdi-account-details</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>clientes</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </router-link>  

        <router-link to="/sobre" >
          <v-list-item class="mb-2" @click="drawer = !drawer">
            <v-list-item-icon>
              <v-icon small>mdi-information-variant</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>Sobre</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </router-link>         
      </v-list>
    </v-navigation-drawer>           
  </nav>
</template>

<script>
export default {
  data: () => ({    
    dialog: false,
    dialog1: false,
    dialog2: false,
    dialog3: false,
    dialog4: false,
    card: false,
    menu: false,
    drawer: false,
    group: null,
    disabled: false,
    openDelay: "0",
    closeDelay: "0",
    value: false,
    active: true,
    show1: false,
    show2: false,    
    clientes: {
      route: "/clientes"
    },
    items: [
      {
        title: "Cadastro de Clientes",
        items: [
          { title: "Home", route: "/" },
          { title: "Clientes", route: "/clientes" },
          { title: "Sobre", route: "/sobre" }
        ]
      }
    ]
  }),
  watch: {
    group() {
      this.drawer = false;
    }
  }, 
  methods: {    
  }
};
</script>

<style>
.exact-active-class {
  background-color: rgb(250, 250, 250);
}

.v-list-item:hover {
  background-color: rgb(20, 150, 255)
}


</style>
