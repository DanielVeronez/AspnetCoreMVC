using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MenorPreco.Model
{
    public class EstabelecimentoModel : DbContext
    {
        [Key]
        public int ID_ESTABELECIMENTO { get; set; }
        public string NME_ESTABELECIMENTO { get; set; }
        public string NME_LOGRADOURO { get; set; }
        public string COD_NUMERO_LOGRADOURO { get; set; }
        public string NME_COMPLEMENTO { get; set; }
        public string NME_BAIRRO { get; set; }
        public int COD_MUNICIPIO_IBGE { get; set; }
        public string NME_MUNICIPIO { get; set; }
        public string NME_SIGLA_UF { get; set; }
        public int COD_CEP { get; set; }
        public double NUM_LATITUDE { get; set; }
        public double NUM_LONGITUDE { get; set; }

        public int COD_GTIN { get; set; }

        //[ForeignKey("Produto")]
        //public ProdutoModel PRODUTO { get; set; }
    }
}
