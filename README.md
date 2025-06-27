# emotionsAPI

API REST en ASP.NET Core que analiza comentarios y clasifica emociones como `positivo`, `negativo` o `neutral`.

## Características

## 🛠️ Tecnologías utilizadas

- .NET 7/8
- Entity Framework Core
- SQL Server / SQLite (configurable)
- Swagger (UI para pruebas)
- C#

 ## 📌Endpoints principales
- POST /api/comment/comments
```
{
  "idProducto"
  "idUser": 1
  "message": "Tu trabajo es genial"
}
```
- GET /api/comment/comments?filter=positivo / GET /api/comment/comments?filter=1
(Filtra comentarios por emoción o id.)

-GET /api/comment/sentiment-sumary
```
{
  "total_comments": 100,
  "sentiment_counts": {
    "positivo": 60,
    "negativo": 20,
    "neutral": 20
  }
}
```

## 🧪 Pruebas
https://localhost:5001/swagger

## 📦 Instalación

 - Requisitos
    * [Docker](https://www.docker.com/) instalado
    * [Git](https://git-scm.com/) instalado

1. Clona el repositorio:

```bash
git clone https://github.com/ManuelSPersonal/emotionsAPI.git
cd emotionsAPI
```

2.Construir e iniciar los contenedores
```
docker-compose up --build   //Esto construirá la imagen y levantará los servicios (API, DB, etc.)
```

