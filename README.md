# Projeto Web Gerador de Testes

![]( https://i.imgur.com/hXfn0BS.gif )
## Projeto

Desenvolvido durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2025

## Descrição

Este projeto tem como objetivo desenvolver um sistema de geração de testes. O sistema possibilita a geração de testes com seleção aleatória de questões, incluindo funcionalidades como duplicação, visualização detalhada e exportação em PDF, tanto do teste quanto do gabarito.

## Detalhes e Funcionalidades

### Disciplinas

- Cadastro, visualização, edição e exclusão de disciplinas.
- Validação de campos obrigatórios:
  - Nome
- Deve armazenar informações sobre as matérias que serão relacionadas à ela posteriormente
- Não permite disciplinas com nomes duplicados.

### Matérias

- Cadastro, visualização, edição e exclusão de matérias.
- Campos obrigatórios:
  - Nome 
  - Disciplina
  - Série
- Não permite registrar uma matéria com o mesmo nome

### Questões

- Cadastro, visualização, edição e exclusão de questões.
- Campos obrigatórios:
  - Matéria
  - Enunciado
  - Alternativas (no mínimo duas, e obrigatoriamente uma correta)

### Testes

- Geração, visualização, duplicação e exclusão de testes.
- Campos obrigatórios:
  - Título
  - Disciplina
  - Matéria
  - Quantidade de Questões
- A quantidade de questões informada deve ser menor ou igual a quantidade de questões cadastradas.

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

## Como Usar

#### Clone o Repositório
```
https://github.com/Carlos-Henrique-Rafaeli/gerador-de-testes
```

#### Navegue até a pasta raiz da solução
```
cd GeradorDeTestes
```

#### Restaure as dependências
```
dotnet restore
```

#### Navegue até a pasta do projeto
```
cd GeradorDeTestes
```

#### Execute o projeto
```
dotnet run
```
