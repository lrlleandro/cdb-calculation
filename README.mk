Instruções para execução do projeto:

Certifique-se de ter o .NET 7 SDK instalado em sua máquina.
Certifique-se de ter o Node.js instalado em sua máquina.

Abra o prompt de comando, ou terminal, e navegue até o diretório raiz do projeto.

Execute o seguinte comando para instalar as dependências do projeto .NET:

- dotnet restore

Execute o seguinte comando para instalar as dependências do projeto Angular:

- npm install

Execute o seguinte comando para iniciar a API:

dotnet run --project <caminho-para-o-projeto-api>

Abra outro terminal ou prompt de comando e navegue até o diretório raiz do projeto novamente.

Execute o seguinte comando para iniciar o servidor do Angular:

- ng serve

Abra seu navegador e navegue até http://localhost:4200 para acessar o projeto Angular.

Instruções de Teste
Navegue até o diretório raiz do projeto em seu terminal ou prompt de comando.

Execute o seguinte comando para executar os testes da API:

- dotnet test <caminho-para-o-projeto-api>

Execute o seguinte comando para executar os testes do projeto Angular:

- ng test
