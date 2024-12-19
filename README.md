# Book Search API

[![Build image, push & deploy](https://github.com/TerrenceMM2/books-api/actions/workflows/build-deploy.yaml/badge.svg)](https://github.com/TerrenceMM2/books-api/actions/workflows/build-deploy.yaml)

A .NET API server to support the [Book Search UI](https://github.com/TerrenceMM2/books-react).

## Installation & Setup
1. Clone the book-search repo.
```bash
$ git clone https://github.com/TerrenceMM2/books-api.git
```

2. In the `appsettings.Development.json` file, add the following configurations:
```jsonc
{
  //...
  "GOOGLE_BOOKS_API": {
    "Key": "" // Obtain development key from GCP admin.
  },
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "" // Obtain connection string from Azure admin.
  }
}
```

3. Build & run using IDE or CLI

### Helpful Resources
- [Create Web APIs with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-9.0)
- [Deploy to Azure](https://learn.microsoft.com/en-us/azure/app-service/app-service-sql-github-actions)
- [Google Books SDK](https://developers.google.com/api-client-library/dotnet/get_started)