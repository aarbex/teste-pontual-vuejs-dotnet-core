import Vue from "vue";
import Router from "vue-router";
import Home from "./components/Home/Home.vue"
import Sobre from "./components/Sobre/Sobre.vue"
import Cliente from "./components/Cliente/Cliente.vue"
import ClienteDetalhe from "./components/Cliente/ClienteDetalhe.vue"

Vue.use(Router);

export default new Router({
    mode: "history",
    routes: [
          {
            path: "/",
            name: "Home",
            component: Home
          },
          {
            path: "/clientes",
            name: "Clientes",
            component: Cliente
          },
          {
            path: "/cliente/:id",
            name: "ClienteDetalhe",
            component: ClienteDetalhe
          },
          {
            path: "/sobre",
            name: "Sobre",
            component: Sobre
          }
    ]
})