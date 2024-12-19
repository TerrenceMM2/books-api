# Book Search API

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/terrencemm2/books-api/build-deploy.yaml?style=for-the-badge&logo=github-actions&logoColor=white&label=Build%20%26%20Deploy&link=https%3A%2F%2Fgithub.com%2FTerrenceMM2%2Fbooks-api%2Factions)
![GitHub Issues or Pull Requests](https://img.shields.io/github/issues/terrencemm2/books-api?style=for-the-badge&logo=github)

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