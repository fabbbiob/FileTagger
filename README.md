# FileTagger
[![Build status](https://ci.appveyor.com/api/projects/status/2cj78i8wsrk4tuyk?svg=true)](https://ci.appveyor.com/project/baracchande/filetagger)

Web application that manages tags on arbitrary files.

## Technologies used

### Server-side

* SQLite
* ASP.NET Web API
* Simple Injector
* Json.NET
* nunit
* Moq
* log4net
* RestSharp

### Client-side

* bootstrap
* jquery
* jquery.validate
* jstree
* selectize.js

## Install instructions

1. In Web.Config of FileTaggerService solution, set the connection string for SQLite, `<add key="sqliteconnectionstring" value="connection string here" />`
2. Build and deploy the FileTaggerService solution to the machine where the files are.
3. In Web.Config of FileTaggerMVC solution, set the service url to where the FileTaggerService solution was deployed, `<add key="FileTaggerServiceUrl" value="link here" />`
··1. Optionally, configure log4net.
4. Build and deploy the FileTaggerMVC solution to any machine.
··1. Optionally, build the FileTaggerLauncher solution and execute it on the machine running the service solution.