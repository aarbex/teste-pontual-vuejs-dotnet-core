import Vue from 'vue'
import App from './App.vue'
import router from "./router.js";
import vuetify from './plugins/vuetify';
import VueResource from "vue-resource";

Vue.config.productionTip = false

Vue.use(VueResource);
//String de Conexão
Vue.http.options.root = "http://127.0.0.1:5000/api";



new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
