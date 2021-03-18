<template>
  <div>
    <v-row class="mx-5" flex bottom>
      <v-col cols-12 sm="6" md="12">
        <p class="display-2 mt-5">{{ cliente.nomeFantasia }}</p>
      </v-col>
      <v-col cols="12" sm="6" md="12">
        <v-text-field
          v-model="cliente.razaoSocial"
          label="Razão Social"
          filled
          readonly
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="12">
        <v-text-field
          v-model="cliente.email"
          label="E-mail"
          filled
          readonly
        ></v-text-field>
      </v-col>
      <v-col cols="12" md="6">
        <v-text-field
          v-model="cliente.cnpj"
          label="CNPJ"
          filled
          readonly
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="6">
        <v-text-field
          v-model="cliente.telefone"
          label="Telefone Fixo"
          readonly
          filled
        ></v-text-field>
      </v-col>
    </v-row>
    <div style="padding-right: 20px; padding-left: 20px; display: flex; ">
      <div
        style="width: 100%; margin: 0 auto; margin-bottom: 20px; text-align: center;"
      >
        <router-link to="/clientes" tag="button">
          <v-btn text style="margin: 5px" color="black">Voltar</v-btn>
        </router-link>
        <v-dialog v-model="dialog1" width="80%" persistent>
          <template v-slot:activator="{ on }">
            <v-btn
              @click="carregaCliente(cliente)"
              color="primary"
              class="ms-5"
              text
              dark
              v-on="on"
              >Editar</v-btn
            >
          </template>
          <v-card>
            <v-toolbar dark color="primary">Editar Cliente</v-toolbar>
            <v-card-text>
              <v-container>
                <v-row align="baseline" dense>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field
                      v-model="nomeFantasia"
                      label="Nome Fantasia"
                      prepend-inner-icon="mdi-account-group"
                      class="text-capitalized"
                      hint="* Preenchimento Obrigatório"
                      persistent-hint
                      required
                      :rules="[rules.required]"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field
                      v-model="razaoSocial"
                      label="Razão Social"
                      prepend-inner-icon="mdi-account-group"
                      class="text-capitalized"
                      hint="* Preenchimento Obrigatório"
                      persistent-hint
                      required
                      :rules="[rules.required]"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field
                      v-model="email"
                      prepend-inner-icon="mdi-email"
                      label="E-mail"
                      required
                      :rules="[rules.required, rules.email]"
                      persistent-hint
                      hint="* Preenchimento Obrigatório"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-mask="cnpjMask"
                      v-model="cnpj"
                      prepend-inner-icon="mdi-account-details"
                      label="CNPJ"
                      hint="* Preenchimento Obrigatório"
                      required
                      :rules="[rules.required]"
                      persistent-hint
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      name="telefone"
                      prepend-inner-icon="mdi-phone"
                      v-model="telefone"
                      hint="* Preenchimento Obrigatório"
                      label="Telefone Fixo"
                      required
                      persistent-hint
                      value
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <div class="flex-grow-1"></div>
              <v-btn color="black" text @click="dialog1 = false"
                >Cancelar</v-btn
              >
              <v-btn
                color="blue darken-1"
                text
                @click="verificaClienteEditar(cliente)"
                >Salvar</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialog2" max-width="290" persistent>
          <v-card>
            <v-card-title class="headline">Atenção!</v-card-title>

            <v-card-text justify="center"
              >Já existe um cliente cadastrado com o CNPJ "{{
                this.cnpjVerificado
              }}"!</v-card-text
            >

            <v-card-actions>
              <div class="flex-grow-1"></div>

              <v-btn color="black" text @click="dialog2 = false">Fechar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialog3" max-width="290">
          <v-card>
            <v-card-title class="headline">Atenção!</v-card-title>

            <v-card-text justify="center"
              >Há campos obrigatórios que não foram preenchidos!!</v-card-text
            >

            <v-card-actions>
              <div class="flex-grow-1"></div>

              <v-btn color="black" text @click="dialog3 = false">Fechar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialog4" max-width="290">
          <v-card>
            <v-card-title class="headline">Atenção!</v-card-title>

            <v-card-text justify="center"
              >O CNPJ inserido é inválido!!!</v-card-text
            >

            <v-card-actions>
              <div class="flex-grow-1"></div>

              <v-btn color="black" text @click="dialog4 = false">Fechar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </div>
    </div>
  </div>
</template>

<script>
import { mask } from "vue-the-mask";
export default {
  components: {},
  directives: {
    mask,
  },
  data() {
    return {
      id: this.$route.params.id,
      nomeFantasia: "",
      razaoSocial: "",
      email: "",
      cnpj: "",
      cnpjAtual: "",
      cnpjVerificado: "",
      telefone: "",
      dataCadastro: "",
      cliente: {},
      clientes: [],
      clienteID: "",
      clienteVerificado: "",
      search: "",
      menu: false,
      menu2: false,
      dialog: false,
      dialog1: false,
      dialog2: false,
      dialog3: false,
      dialog4: false,
      dialog0: false,
      cnpjValidation: false,
      cnpjMask: "##.###.###/####-##",
      dataMask: "##/##/####",
      telMask: "(##) ####-####",
      date: new Date().toISOString().substr(0, 10),
      rules: {
        required: (value) => !!value || "Preenchimento obrigatório.",
        email: (value) => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "E-mail inválido.";
        },
      },
    };
  },
  created() {
    this.$http
      .get("clientes/" + this.id)
      .then((res) => res.json())
      .then((cliente) => {
        this.cliente = cliente;
        this.carregaCliente(cliente);
      });
    this.$http
      .get("clientes")
      .then((res) => res.json())
      .then((clientes) => (this.clientes = clientes));
  },

  methods: {
    validarCpfCnpj(obj) {
      // recebe um objeto
      var s = obj.replace(/\D/g, "");
      var tam = s.length; // removendo os caracteres nãoo numéricos
      if (!(tam == 11 || tam == 14 || tam == 0)) {
        // validando o tamanho
        //alert("'" + s + "' Não é um CPF ou um CNPJ Válido!"); // tamanho inválido
        //obj.focus();
        return false;
      }

      // se for CPF
      if (tam == 11) {
        if (!this.validaCPF(s)) {
          // chama a função que valida o CPF
          //alert("'" + s + "' Não é um CPF Válido!"); // se quiser mostrar o erro
          //obj.select(); // se quiser selecionar o campo em questão
          //obj.focus();
          return false;
        }
        obj = this.maskCPF(s); // se validou o CPF mascaramos corretamente
        return true;
      }

      // se for CNPJ
      if (tam == 14) {
        if (!this.validaCNPJ(s)) {
          // chama a função que valida o CNPJ
          //alert("'" + s + "' Não é um CNPJ Válido!"); // se quiser mostrar o erro
          //obj.select(); // se quiser selecionar o campo enviado
          return false;
        }
        obj = this.maskCNPJ(s); // se validou o CNPJ mascaramos corretamente
        return true;
      }
    },

    validaCPF(s) {
      var c = s.substr(0, 9);
      var dv = s.substr(9, 2);
      var d1 = 0;
      for (var i = 0; i < 9; i++) {
        d1 += c.charAt(i) * (10 - i);
      }
      if (d1 == 0) return false;
      d1 = 11 - (d1 % 11);
      if (d1 > 9) d1 = 0;
      if (dv.charAt(0) != d1) {
        return false;
      }
      d1 *= 2;
      for (var j = 0; j < 9; j++) {
        d1 += c.charAt(j) * (11 - j);
      }
      d1 = 11 - (d1 % 11);
      if (d1 > 9) d1 = 0;
      if (dv.charAt(1) != d1) {
        return false;
      }
      return true;
    },

    validaCNPJ(CNPJ) {
      var a = new Array();
      var b = new Number();
      var c = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
      var x = 0;
      var y = 0;
      var i = 0;
      for (i = 0; i < 12; i++) {
        a[i] = CNPJ.charAt(i);
        b += a[i] * c[i + 1];
      }
      if ((x = b % 11) < 2) {
        a[12] = 0;
      } else {
        a[12] = 11 - x;
      }
      b = 0;
      for (y = 0; y < 13; y++) {
        b += a[y] * c[y];
      }
      if ((x = b % 11) < 2) {
        a[13] = 0;
      } else {
        a[13] = 11 - x;
      }
      if (CNPJ.charAt(12) != a[12] || CNPJ.charAt(13) != a[13]) {
        return false;
      }
      return true;
    },
    maskCPF(CPF) {
      return (
        CPF.substring(0, 3) +
        "." +
        CPF.substring(3, 6) +
        "." +
        CPF.substring(6, 9) +
        "-" +
        CPF.substring(9, 11)
      );
    },
    maskCNPJ(CNPJ) {
      return (
        CNPJ.substring(0, 2) +
        "." +
        CNPJ.substring(2, 5) +
        "." +
        CNPJ.substring(5, 8) +
        "/" +
        CNPJ.substring(8, 12) +
        "-" +
        CNPJ.substring(12, 14)
      );
    },
    verificaClienteCNPJ() {
      this.clienteVerificado = this.clientes.filter(
        (x) => x.cnpj === this.cnpj
      )[0];
      if (this.clienteVerificado) {
        this.dialog0 = true;
      }
    },
    verificaClienteEditar(cliente) {
      this.cnpjValidation = this.validarCpfCnpj(this.cnpj);
      if (!this.cnpjValidation) {
        this.dialog4 = true;
        this.cnpjValidation = false;
        console.log("CNPJ Inválido" + false);
      } else {
        this.clienteVerificado = this.clientes.filter(
          (x) => x.cnpj === this.cnpj
        )[0];
        if (this.clienteVerificado && this.clienteVerificado.id != this.id) {
          this.clienteVerificado = "";
          this.dialog2 = true;
        } else {
          this.editarCliente(cliente);
          this.clienteVerificado = "";
        }
      }
    },
    formatDate(date) {
      if (!date) return null;
      const [year, month, day] = date.split("-");
      return `${day}/${month}/${year}`;
    },
    filterOnlyCapsText(value, search) {
      return (
        value != null &&
        search != null &&
        typeof value === "string" &&
        (value.toString().indexOf(search) !== -1 ||
          value
            .toString()
            .toLowerCase()
            .indexOf(search) !== -1 ||
          value
            .toString()
            .toUpperCase()
            .indexOf(search) !== -1)
      );
    },

    limparFormulario() {
      (this.nomeFantasia = ""),
        (this.razaoSocial = ""),
        (this.email = ""),
        (this.cnpj = ""),
        (this.telefone = ""),
        (this.dataCadastro = "");
    },
    editarCliente(cliente) {
      let _clienteEditar = {
        id: cliente.id,
        nomeFantasia: this.nomeFantasia,
        razaoSocial: this.razaoSocial,
        email: this.email,
        cnpj: this.cnpj,
        telefone: this.telefone,
        dataCadastro: cliente.dataCadastro,
      };
      if (
        this.nomeFantasia.length == 0 ||
        this.telefone.length == 0 ||
        this.email.length == 0 ||
        this.razaoSocial.length == 0 ||
        this.cnpj.length == 0
      ) {
        this.dialog3 = true;
      } else {
        this.$http
          .put(`clientes/${_clienteEditar.id}`, _clienteEditar)
          .then(
            (window.location.href =
              window.location.origin + "/cliente/" + _clienteEditar.id)
          );
      }
    },

    carregaCliente(cliente) {
      (this.nomeFantasia = cliente.nomeFantasia),
        (this.razaoSocial = cliente.razaoSocial),
        (this.email = cliente.email),
        (this.dataCadastro = this.formatDate(cliente.dataCadastro)),
        (this.cnpj = cliente.cnpj),
        (this.telefone = cliente.telefone);
    },
  },
};
</script>

<style scoped>
.div-main {
  display: flex;
  width: 100%;
  padding-top: 20px;
}
</style>
