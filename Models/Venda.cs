using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tech_test_payment_api.Models
{

    public class Venda
    {
        public Venda()
        {
        }

        public Venda(int vendedor, List<Produto> produtos){
            Produtos = new List<Produto>();
            this.Produtos = produtos;
            this.IdVendedor = vendedor;
            DataDeVenda = DateTime.Now;
            Status = "AguardandoPagamento";

        }

        [Key]
        public int IdVenda { get; set; }
        public int IdVendedor { get; set; }
        public DateTime DataDeVenda { get; set; }
        public List<Produto> Produtos { get; set; }
        public string Status { get; set; }

    }

    public class LigaçãoVendedorProduto{
        [Key, Column(Order=0), ForeignKey("IdProduto")]
        public int IdProduto { get; set; }
        [Key, Column(Order=1), ForeignKey("IdVendedor")]
        public int IdVendedor { get; set; }

        public virtual Venda Venda { get; set;}
        public virtual Produto Produto { get; set; }
    }
}