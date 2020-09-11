using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MenorPreco.Model
{
    public class ProdutoModel : DbContext
    {
        [Key]
        public string COD_GTIN { get; set; }
        public DateTime DAT_EMISSAO { get; set; }
        public int COD_TIPO_PAGAMENTO { get; set; }
        public string COD_PRODUTO { get; set; }
        public int COD_NCM { get; set; }
        public string COD_UNIDADE { get; set; }
        public string DSC_PRODUTO { get; set; }
        public string VLR_UNITARIO { get; set; }

        public int ID_ESTABELECIMENTO { get; set; }

        //Referencia externa
        //[ForeignKey("Estabelecimentos")]
        //public ICollection<EstabelecimentoModel> ESTABELECIMENTOS { get; set; }
    }
}
