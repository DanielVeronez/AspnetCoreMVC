# AspnetCoreMVC

O arquivo de importa��o de dados deve se encontra no diret�rio raiz do projeto com o nome dataset-processo-seletivo-2019.csv;

Deve ser instalado o SQLLite usando o comando Install-Package Microsoft.EntityFrameworkCore.Sqlite;

O CSV s� pode ser importado apenas uma vez!

Foi criado uma tela HOME com 2 bot�es. Um para fazer a importa��o com o link /V1/Importar como solicitado, e um link /V1/produtos/GTIN com as especifica��es solicitadas.
	- Deve usar o HTTPGet para esta requisi��o;
	- Caso n�o for nenhuma ID, deve retornar BadRequest;
	- Caso n�o encontrar a ID solicitada, deve retornar NotFound;
	- Para qualquer tipo de acesso, deve ser logado em arquivo (LogAcessoProdutos.txt) na raiz do projeto com as informa��es:
		- Data e hora do acesso;
		- C�digo GTIN;
		- Resultado;
	- O retorno do dado bruto deve acontecer se tudo acontecer corretamente.

Foi adicionado um bot�o e uma rota para Listar Todos os Produtos e um link Google para buscar a localiza��o do estabelecimento.

O arquivo de log de importa��o � deletado todas as vezes que for iniciado uma nova importa��o.

O arquivo de log de busca por produto � sempre adicionado quando o link � acionado.

O link para o Google maps foi adicionado na listagem de produtos.