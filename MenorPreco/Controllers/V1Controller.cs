using MenorPreco.Database;
using MenorPreco.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using SQLitePCL;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace MenorPreco.Controllers
{
    public class V1Controller : Controller
    {
        private DatabaseContext _context;
        private string _logImportacao = "LogImportacao.txt";
        private string _logAcessoProdutos = "LogAcessoProdutos.txt";

        public V1Controller(DatabaseContext db)
        {
            _context = db;
        }

        public string importar()
        {
            if (System.IO.File.Exists(_logImportacao))
            {
                //Se existe arquivo, delata para fazer um novo
                System.IO.File.Delete(_logImportacao);
            }

            //Senão, cria um espaço ma menoria para o arquivo de log
            StreamWriter writer = new StreamWriter(_logImportacao);

            writer.WriteLine("Arquivo de log iniciado as " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            //Importação do CSV
            using (var reader = new StreamReader(@"dataset-processo-seletivo-2019.csv"))
            {
                writer.WriteLine("Aberto o arquivo CSV para importação as " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                List<ProdutoModel> produtos = new List<ProdutoModel>();
                List<EstabelecimentoModel> estabelecimentos = new List<EstabelecimentoModel>();
                ProdutoModel produto = new ProdutoModel();
                EstabelecimentoModel estabelecimento = new EstabelecimentoModel();
                int produtoadd = 0;
                int estabelecimentoadd = 0;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] palavras = line.Split(',');
                    //if (!_context.Produtos.Any(p => p.COD_GTIN == palavras[0]))
                    if (palavras[0] != "COD_GTIN")
                    {
                        produto = new ProdutoModel();
                        estabelecimento = new EstabelecimentoModel();

                        produto.COD_GTIN = (palavras[0]);
                        produto.DAT_EMISSAO = DateTime.Parse(palavras[1]);

                        if (palavras[2].Contains("|"))
                            writer.WriteLine(palavras[0] + " - Encontrado inconsistencia na importação do produto. Campo COD_TIPO_PAGAMENTO contêm caracter invalido. Corrigido!");

                        produto.COD_TIPO_PAGAMENTO = int.Parse(palavras[2].Contains("|") ? "99" : palavras[2]);
                        produto.COD_PRODUTO = (palavras[3]);
                        produto.COD_NCM = int.Parse(palavras[4]);
                        produto.COD_UNIDADE = (palavras[5]);
                        produto.DSC_PRODUTO = palavras[6];
                        produto.VLR_UNITARIO = (palavras[7]);
                        produto.ID_ESTABELECIMENTO = int.Parse(palavras[8]);

                        if (!produtos.Exists(p => p.COD_GTIN == palavras[0]))
                        {
                            produtos.Add(produto);
                            _context.Add(produto);
                        }
                        else
                        {
                            writer.WriteLine(palavras[0] + " - Tentativa de duplicidade. Linha removida.");
                        }

                        produtoadd++;

                        if (estabelecimentos.Count == 0 || !estabelecimentos.Exists(p => p.ID_ESTABELECIMENTO == produto.ID_ESTABELECIMENTO))
                        {
                            estabelecimento.ID_ESTABELECIMENTO = int.Parse(palavras[8]);
                            estabelecimento.NME_ESTABELECIMENTO = palavras[9];
                            estabelecimento.NME_LOGRADOURO = palavras[10];
                            estabelecimento.COD_NUMERO_LOGRADOURO = (palavras[11]);
                            estabelecimento.NME_COMPLEMENTO = (palavras[12]);
                            estabelecimento.NME_BAIRRO = (palavras[13]);
                            estabelecimento.COD_MUNICIPIO_IBGE = int.Parse(palavras[14]);
                            estabelecimento.NME_MUNICIPIO = (palavras[15]);
                            estabelecimento.NME_SIGLA_UF = (palavras[16]);
                            estabelecimento.COD_CEP = int.Parse(palavras[17]);
                            estabelecimento.NUM_LATITUDE = palavras[18];
                            estabelecimento.NUM_LONGITUDE = palavras[19];

                            estabelecimentos.Add(estabelecimento);
                            _context.Add(estabelecimento);
                            estabelecimentoadd++;
                        }
                    }
                }

                reader.Close();

                //_context.AddRange(produtos);
                _context.SaveChanges();

                writer.WriteLine("Finalizado o arquivo CSV para importação as " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                writer.Close();

            }

            return ("Dados importados com sucesso.");
        }

        public ActionResult ListaProdutos()
        {
            var produtos = _context.Produtos.ToList();

            return View(produtos);
        }

        [HttpGet("{GTIN}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult produtos(string GTIN)
        {
            StreamWriter writer = new StreamWriter(_logAcessoProdutos);
            ProdutoModel produto = new ProdutoModel();

            writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - Feito acesso ao GTIN " + GTIN);

            if (GTIN == null)
            {
                writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - O GTIN está nulo");

                return BadRequest();
            }

            produto = _context.Produtos.First(produto => produto.COD_GTIN == GTIN);

            if (produto.COD_GTIN == null || produto.COD_GTIN == "")
            {
                writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - O GTIN não retornou dados");

                return NotFound();
            }

            writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - O GTIN retornou " + GTIN);
            writer.Close();

            return View(produto);
        }

        [HttpGet]
        public ActionResult GPS(int ID)
        {
            var estabelecimento = _context.Estabelecimentos.Find(ID);

            string linkGoogle = "https://www.google.com/maps/search/" + estabelecimento.NME_LOGRADOURO + "," + estabelecimento.COD_NUMERO_LOGRADOURO + "," + estabelecimento.NME_BAIRRO + "?hl=pt-BR&source=opensearch";

            return Redirect(linkGoogle);
        }

    }
}
