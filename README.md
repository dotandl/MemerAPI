# MemerAPI - API with memes!

This API allows you to easily get your favorite memes from some most popular
(at this time mainly Polish; other will be added in next releases) services.

## Supported services

Currently _MemerAPI_ supports the following services:

- https://komixxy.pl
- https://memy.jeja.pl
- https://kwejk.pl
- https://jbzd.com.pl

Support for new services will be added in next releases. You can also suggest
ones by opening new GitHub Issue or add ones by opening new GitHub Pull Request.

## Build

This API is written in ASP.NET Core 3.1 (C#). To run a development version type:

```sh
# Development profile - listening on https://localhost:5001
$ dotnet run -p MemerAPI
```

To build and run the release version type:

```sh
# Release profile - listening on http://localhost:80
$ dotnet publish  MemerAPI -o out -c Release
$ dotnet out/MemerAPI.dll
```

This project also has own
[Docker image](https://hub.docker.com/r/dotandl/memer-api) - to run type:

```sh
# Docker image - listening on http://localhost:80
$ docker run -p 80:80 -d dotandl/memer-api
```

## Available endpoints

See [endpoints.md](docs/endpoints.md)

## Notes

- Due to the layout of the _Kwejk_'s random image page, when you call `/kwejk`
  the `uri` field is equal to the `viewURI` field.
- Because of the _Jbzd_'s constraints (you must be logged in to get a random
  image), the algorithm gets the random image by itself - thus it can get one
  of the 400 images in all the service.
- If you run the API with `Development` configuration (using `dotnet run`; see
  [Build](#Build)), you will be able to see extended error info.

## Contribution

If you've seen an error or you want me to implement other service, feel free to
open new GitHub Issue.  
You can also open new GitHub Pull Request if you've fixed an error or
implemented new service.

Thank you for your help.
