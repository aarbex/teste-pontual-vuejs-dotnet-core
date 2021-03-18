using System;

namespace Api.Models
{
    public class Cliente
    {
        public long Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        
        public Cliente()
        {
            DataCadastro = DateTime.Now;
        }
        public DateTime DataCadastro { get; private set; }
        

    }    
}