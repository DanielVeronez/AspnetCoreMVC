# AspnetCoreMVC

O arquivo de importação de dados deve se encontra no diretório raiz do projeto com o nome dataset-processo-seletivo-2019.csv;

Deve ser instalado o SQLLite usando o comando Install-Package Microsoft.EntityFrameworkCore.Sqlite;

O CSV só pode ser importado apenas uma vez!

Foi criado uma tela HOME com 2 botões. Um para fazer a importação com o link /V1/Importar como solicitado, e um link /V1/produtos/GTIN com as especificações solicitadas.
	- Deve usar o HTTPGet para esta requisição;
	- Caso não for nenhuma ID, deve retornar BadRequest;
	- Caso não encontrar a ID solicitada, deve retornar NotFound;
	- Para qualquer tipo de acesso, deve ser logado em arquivo (LogAcessoProdutos.txt) na raiz do projeto com as informações:
		- Data e hora do acesso;
		- Código GTIN;
		- Resultado;
	- O retorno do dado bruto deve acontecer se tudo acontecer corretamente.

Foi adicionado um botão e uma rota para Listar Todos os Produtos e um link Google para buscar a localização do estabelecimento.

O arquivo de log de importação é deletado todas as vezes que for iniciado uma nova importação.

O arquivo de log de busca por produto é sempre adicionado quando o link é acionado.

O link para o Google maps foi adicionado na listagem de produtos.