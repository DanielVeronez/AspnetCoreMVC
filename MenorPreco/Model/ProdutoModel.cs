using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenorPreco.Model
{
    public class ProdutoModel : DbContext
    {
        [Key]
        public int COD_GTIN { get; set; }
        public DateTime DAT_EMISSAO { get; set; }
        public int COD_TIPO_PAGAMENTO { get; set; }
        public int COD_PRODUTO { get; set; }
        public int COD_NCM { get; set; }
        public int COD_UNIDADE { get; set; }
        public string DSC_PRODUTO { get; set; }
        public double VLR_UNITARIO { get; set; }

        //Referencia externa
        public int ID_ESTABELECIMENTO { get; set; }
    }
}
