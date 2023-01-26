using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tech_test_payment_api.Models
{
    public class Produto
    {
        public Produto(string Nome)
        {
            this.Nome = Nome;
        }
        [Key]
        public int IdProduto { get; set; }
        public string Nome { get; set; }
    }
}