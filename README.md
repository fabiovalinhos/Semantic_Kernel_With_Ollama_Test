# Teste com Semantic Kernel e Ollama via Docker

Este projeto é um ambiente de teste para explorar a integração entre o Semantic Kernel e o Ollama, com o Ollama rodando em um contêiner Docker.

## Visão Geral

O objetivo principal é experimentar e entender como utilizar as funcionalidades do Semantic Kernel para interagir com modelos de linguagem (LLMs) disponibilizados localmente através do Ollama.

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Ollama](https://ollama.ai/)

## Como executar

1.  **Clone o repositório:**
    ```bash
    git clone <url-do-repositorio>
    cd <nome-do-repositorio>
    ```

2.  **Inicie o Ollama com Docker:**
    O arquivo `docker-compose.yml` está configurado para subir o serviço do Ollama.
    ```bash
    docker-compose up -d
    ```

3.  **Baixe um modelo (ex: Llama3):**
    ```bash
    docker exec -it ollama ollama pull llama3
    ```

4.  **Execute a aplicação .NET:**
    ```bash
    dotnet run
    ```
