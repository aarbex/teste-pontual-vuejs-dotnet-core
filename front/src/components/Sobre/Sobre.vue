<template>
  <main>
    <v-row align="left" justify="left" style="padding: 0 150px">
      <v-col class="text-left" cols="12">
        <h1>
          Sobre
        </h1>
        <h2>Teste de Programação</h2>
        <br />
        <p>
          Minha expectativa com esse teste é apresentar meu nível de
          conhecimento como candidato em relação às tecnologias necessárias para
          o bom andamento das atividades no dia a dia na Pontual Systems. <br />
          O teste é um dos requisitos avaliados durante o processo seletivo.
          Abaixo descrevo o que apresento com mais detalhes.
        </p>
        <h3>Tecnologias Esperadas</h3>
        <br />
        <ul align="left">
          <li>Web API em C# e .NET Core;</li>
          <li>Entity Framework Core;</li>
          <li>Base de Dados em SQL Server;</li>
          <li>Front-end em VueJS, preferencialmente com Vuetify;</li>
        </ul>
        <br />
        <p>
          O teste consiste na criação de um HOT SITE para Cadastro de Clientes.
          <br />
          Para tal, a aplicação possui uma página de boas vindas, uma página que
          lista os clientes, permitindo a visualização de todos os clientes
          cadastrados em forma de lista e com filtros, novos cadastros, edição e
          remoção de clientes e redirecionamento para uma tela de detalhes de
          clientes (pode ser útil no futuro, caso o cliente insira fotos,
          imagens, anexos, etc). E obviamente essa página, que explana sobre o
          teste em si.<br />
          O cadastro tem os campos indicados na tabela abaixo:
        </p>

        <table align="left" style="margin: 25px 0 25px 0 ; border: 1px solid black;">
            <thead >
                
                <th>Propriedade</th>
                <th>Tipo</th>
                <th>Tamanho</th>
                <th>Obrigatório?</th>
            </thead>
            <tbody>
                <tr>
                    <td>Id (PK)</td>
                    <td>LONG</td>
                    <td>-</td>
                    <td>SIM</td>
                </tr>
                <tr>
                    <td>NomeFantasia</td>
                    <td>STRING</td>
                    <td>250</td>
                    <td>SIM</td>
                </tr>
                <tr>
                    <td>RazaoSocial</td>
                    <td>STRING</td>
                    <td>250</td>
                    <td>SIM</td>
                </tr>
                <tr>
                    <td>CNPJ</td>
                    <td>STRING</td>
                    <td>20</td>
                    <td>SIM</td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>STRING</td>
                    <td>150</td>
                    <td>SIM</td>
                </tr>
                <tr>
                    <td>Telefone</td>
                    <td>STRING</td>
                    <td>30</td>
                    <td>SIM</td>
                </tr>
                <tr>
                    <td>DataCadastro</td>
                    <td>DATETIME</td>
                    <td>-</td>
                    <td>SIM</td>
                </tr>
                
            </tbody>
        </table>

        <br>
        <h3>Observações</h3>
        <br>
        <ul align="left">
          <li>Foi criada uma API como BACK END e utilizado VueJS no FRONT para criação das telas;</li>
          <li>A entrega será feita em 2 projetos separados (API e Front End);</li>
          <li>Para criação da Base de Dados foi utilizado Entity Framework Code First;</li>
          <li>Os campos em tela respeitam suas obrigatoriedades;</li>
          <li>O campo CNPJ tem máscara para preenchimento e validação;</li>
        </ul>
        <br>
        <p>Coloco-me à disposição para quaisquer dúvidas através do e-mail <a href="mailto:arbex.andre@gmail.com">arbex.andre@gmail.com</a></p>
        <br>        
      </v-col>
      
    </v-row>
    
    
        <div class="flex-grow-1 mx-auto" align="center">
      <small class="mx-5">Arbex © 2021</small>
      <br />
      <small class="mx-5">Todos os direitos reservados</small>
    </div>
    
  </main>
</template>

<script>
export default {};
</script>

<style scoped></style>
