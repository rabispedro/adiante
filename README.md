# adiante
Projeto de Antecipação de Recebível para o desafio técnico da Size Fintech.


# Observações

## Padrões de Projeto

**Faturamento** parece alinhar-se com o padrão de estados e/ou estratégia[^padroes-de-projeto]:

- **Faturamento Irrisório:** abaixo de R$ 10.000,00;
- **Faturamento Baixo:** entre R$ 10.000,00 e R$ 50.000,00;
- **Faturamento Médio:** entre R$ 50.001,00 e R$ 100.000,00;
- **Faturamento Alto:** acima de R$ 100.001,00

Assim, o limite de antecipação pode ser delegado à estratégia de cada estado ao invés da utilização excessiva de `if-else` ou `switch-cases`, o que deixa a solução mais voltada à orientação à objetos e menos imperativa.

## Carrinho

A implementação por carrinho pode seguir duas abordagens simples:

- Uso de Dicionário/Mapa:
	- Retém a informação na memória do sistema;
	- Perde a informação em caso de falha do servidor;
	- Fácil de implementar, pois usa estrutura de dados interna do C#;
- Uso de Banco de Dados NoSQL, chave-valor, como Redis ou Valkey:
	- Uma camada a mais de complexidade;
	- Retém a informação mesmo com falha no servidor;
	- Pode ser utilizado em microsserviços por não depender da API;

## Testes Unitários

É imprescindível fazer os testes unitários com as regras abordadas, tanto para verificar a corretude dos serviços quanto para entender melhor as regras de negócio propostas.

## Cálculo de Antecipação

- Taxa fixa: $4.65\%$ ao mês
- Deságio: $\dfrac{valor_{NF}}{(1 + taxa)^{\frac{prazo}{30}}}$
- Valor Líquido: $valor_{NF} - deságio$

## Arquitetura

Arquitetura em camadas não parece ser muito adequada, pois haverão poucas funcionalidades, o que causa sobrecarga cognitiva desnecessária.

Arquitetura em domínios, ou monolito modular, parece estar mais adequada ao desafio pela alta coesão entre os Domínios do desafio e por estar aberta à possibilidade de criar comunicação entre microsserviços.

## Instruções

É necessário deixar um README conciso para correta utilização e implantação do sistema.

## Classes Anémicas

Para evitar sobreuso indevido de tipos primários, principalmente **string**, e, portanto, evitando a criação de classes anémicas, utilizaremos representações aprimoradas de **CNPJ**, **Ramo**.

## Problemas

1. O que acontece caso o faturamento esteje abaixo de R$ 10.000,00?
2. O que acontece caso o faturamento esteje entre R$ 50.000,01 e R$ 50.000,99?
3. O que acontece caso o faturamento esteje entre R$ 100.000,01 e R$ R$ 100.000,99?
4. Quantas casas decimais utilizar?
5. Qual nome de projeto utilizar?
	1. Antecipa Já, passa a ideia
	2. **Adiante**, passa a ideia de "adiantar" o crédito, além de "estar na vanguarda, à frente do tempo"


[^padroes-de-projeto]: https://refactoring.guru/
