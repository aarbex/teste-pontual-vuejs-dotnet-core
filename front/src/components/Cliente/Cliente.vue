<template>
  <div>
    <v-row class="my-5">
      <h2 class="mx-auto">Clientes</h2>
    </v-row>
    <div>
      <v-data-table
        :headers="headers"
        :items="clientes"
        sort-by="nomeFantasia"
        item-key="id"
        class="elevation-1 px-5"
        :search="search"
        :custom-filter="filterOnlyCapsText"
        mobile-breakpoint="0"
      >
        <template v-slot:top>
          <v-col cols="12">
            <v-text-field
              v-model="search"
              label="Pesquisar em clientes cadastrados"
              prepend-inner-icon="mdi-magnify"
              filled
              clearable
            ></v-text-field>
          </v-col>
        </template>
        <template v-slot:item.dataCadastro="{ item }">
          <span>{{ new Date(item.dataCadastro).toLocaleString() }}</span>
        </template>
        <template v-slot:item.action="{ item }">
          <v-menu>
            <template v-slot:activator="{ on }">
              <v-btn light icon v-on="on">
                <v-icon primary>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>

            <v-list bottom>
              <router-link :to="'/cliente/' + item.id" tag="button">
                <v-list-item>
                  <v-item-list-title xSmall>Detalhes</v-item-list-title>
                </v-list-item>
              </router-link>
              <v-list-item
                @click="
                  dialog = true;
                  cliente = item;
                  carregaCliente(item);
                  verificaClienteVinculado();
                "
              >
                <v-item-list-title>Editar</v-item-list-title>
              </v-list-item>
              <v-list-item
                @click="
                  dialog1 = true;
                  cliente = item;
                "
              >
                <v-item-list-title>Excluir</v-item-list-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </template>
        <template v-slot:body.append>
          <tr>
            <td></td>

            <td colspan="4"></td>
          </tr>
        </template>
      </v-data-table>
    </div>
    <v-dialog v-model="dialog" width="80%" persistent>
      <template v-slot:activator="{ on }">
        <v-btn
          style="position: fixed; z-index: 100; right: 10pt; bottom: 1pt;"
          color="primary"
          class="ms-5 mb-5"
          fab
          dark
          v-on="on"
          @click="limparFormulario()"
        >
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-icon dark v-on="on">mdi-plus</v-icon>
            </template>
            <span>Adicionar</span>
          </v-tooltip>
        </v-btn>
      </template>
      <v-card>
        <v-toolbar v-if="this.id" dark color="primary"
          >Editar Cliente</v-toolbar
        >
        <v-toolbar v-else dark color="primary">Cadastrar Cliente</v-toolbar>
        <v-card-text>
          <v-container>
            <v-row align="baseline" dense>
              <v-col cols="12" sm="12" md="12">
                <v-text-field
                  v-model="nomeFantasia"
                  hint="* Preenchimento Obrigatório"
                  persistent-hint
                  prepend-inner-icon="mdi-account-group"
                  label="Nome Fantasia"
                  auto-select-first
                  class="text-capitalized"
                  required
                  :rules="[rules.required]"
                ></v-text-field>
              </v-col>

              <v-col cols="12" sm="12" md="12">
                <v-text-field
                  v-model="razaoSocial"
                  hint="* Preenchimento Obrigatório"
                  persistent-hint
                  prepend-inner-icon="mdi-account-group"
                  label="Razão Social"
                  class="text-capitalized"
                  required
                  :rules="[rules.required]"
                ></v-text-field>
              </v-col>

              <v-col cols="12" sm="12" md="12">
                <v-text-field
                  v-model="email"
                  prepend-inner-icon="mdi-email"
                  label="E-mail"
                  hint="* Preenchimento Obrigatório"
                  persistent-hint
                  required
                  :rules="[rules.required, rules.email]"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field
                  v-model="cnpj"
                  prepend-inner-icon="mdi-account-details"
                  v-mask="cnpjMask"
                  label="CNPJ"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field
                  name="telefone"
                  prepend-inner-icon="mdi-phone"
                  v-model="telefone"
                  v-mask="telMask"
                  label="Telefone Fixo"
                  value
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <div class="flex-grow-1"></div>
          <v-btn
            color="black"
            text
            @click="
              limparFormulario();
              dialog = false;
            "
            >Cancelar</v-btn
          >
          <v-btn
            v-if="this.id"
            color="blue darken-1"
            text
            @click="verificaClienteEditar(cliente)"
            >Salvar</v-btn
          >
          <v-btn v-else color="blue darken-1" text @click="verificaCliente()"
            >Salvar</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialog1" max-width="290">
      <v-card>
        <v-card-title class="headline mx-auto">Atenção!</v-card-title>

        <v-card-text>
          Deseja realmente excluir o cliente
          <br />
          {{ cliente.nomeFantasia }}?
        </v-card-text>

        <v-card-actions>
          <div class="flex-grow-1"></div>

          <v-btn color="black" text @click="dialog1 = false">Cancelar</v-btn>
          <v-btn
            color="primary"
            text
            @click="
              removerCliente(cliente);
              dialog1 = false;
            "
            >Excluir</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialog0" max-width="290" persistent>
      <v-card>
        <v-card-title class="headline">Atenção!</v-card-title>

        <v-card-text justify="center"
          >Já existe um cliente cadastrado com esse CNPJ!</v-card-text
        >

        <v-card-actions>
          <div class="flex-grow-1"></div>

          <v-btn color="black" text @click="dialog0 = false">Fechar</v-btn>
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
    <div class="flex-grow-1 mx-auto" align="center">
      <small class="mx-5">Arbex © 2021</small>
      <br />
      <small class="mx-5">Todos os direitos reservados</small>
    </div>
  </div>
</template>

<script>
import { mask } from "vue-the-mask";
import { validator } from "cpf-cnpj-validator";
export default {
  components: {},
  directives: {
    mask,
    validator,
  },
  data() {
    return {
      nomeFantasia: "",
      razaoSocial: "",
      email: "",
      cnpj: "",
      cnpjVerificado: "",
      telefone: "",
      dataCadastro: "",
      cliente: {},
      clientes: [],
      clienteID: "",
      clienteVerificado: "",
      search: "",
      id: this.$route.params.id,
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
  computed: {
    headers() {
      return [
        {
          text: "Nome Fantasia",
          align: "center",
          value: "nomeFantasia",
        },
        {
          text: "Razão Social",
          align: "center",
          value: "razaoSocial",
        },
        { text: "CNPJ", value: "cnpj" },
        { text: "E-mail", value: "email" },
        { text: "Telefone", value: "telefone" },
        { text: "Data de Cadastro", value: "dataCadastro" },
        { text: "Ações", value: "action", align: "center", sortable: false },
      ];
    },
  },
  created() {
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
    verificaCliente() {
      this.cnpjValidation = this.validarCpfCnpj(this.cnpj);
      if (!this.cnpjValidation) {
        this.dialog4 = true;
        this.cnpj = "";
        this.cnpjValidation = false;
        console.log("CNPJ Inválido" + false);
      } else {
        this.clienteVerificado = this.clientes.filter(
          (x) => x.cnpj === this.cnpj
        )[0];
        if (this.clienteVerificado) {
          if (this.clienteVerificado.cnpj != this.cliente.cnpj) {
            this.dialog0 = true;
          } else {
            this.addCliente();
            this.clienteVerificado = "";
          }
        } else {
          this.addCliente();
          this.clienteVerificado = "";
        }
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
          this.dialog0 = true;
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
    addCliente() {
      this.dataCadastro = this.formatDate(this.date);
      let _cliente = {
        nomeFantasia: this.nomeFantasia,
        razaoSocial: this.razaoSocial,
        email: this.email,
        cnpj: this.cnpj,
        telefone: this.telefone,
      };
      if (
        this.nomeFantasia.length == 0 ||
        this.razaoSocial.length == 0 ||
        this.email.length == 0 ||
        this.cnpj.length == 0 ||
        this.telefone.length == 0
      ) {
        this.dialog3 = true;
      } else {
        this.$http.post("clientes", _cliente).then((res) => {
          res.json();
          window.location.href = window.location.origin + "/clientes";
        });
      }
    },
    editarCliente() {
      let _clienteEditar = {
        id: this.id,
        nomeFantasia: this.nomeFantasia,
        razaoSocial: this.razaoSocial,
        email: this.email,
        cnpj: this.cnpj,
        telefone: this.telefone,
        dataCadastro: this.dataCadastro,
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
          .then((window.location.href = window.location.origin + "/clientes"));
      }
    },
    removerCliente(cliente) {
      this.$http.delete(`clientes/${cliente.id}`).then(() => {
        let indice = this.clientes.indexOf(cliente);
        this.clientes.splice(indice, 1);
      });
    },
    carregaCliente(cliente) {
      (this.id = cliente.id),
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

<style>
h1 {
  text-align: center;
  padding: 20px 0px;
}
table {
  width: 100%;
  padding: 20px opx;
  margin: 0 auto;
}
.div-add {
  text-align: right;
  padding-bottom: 10px;
}
.text-capitalized input {
  text-transform: capitalize;
}
</style>
