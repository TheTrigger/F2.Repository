# Guide

## Setup Docker Sql Server

- <https://hub.docker.com/_/microsoft-mssql-server>

```sh
docker run -e POSTGRES_USER=develop -e POSTGRES_PASSWORD=develop -p 1440:5432 --name OibiRepositoryDemo -d postgres:14-alpine

# The password should follow the SQL Server default password policy, otherwise the container can not setup SQL server and will stop working. By default, the password must be at least 8 characters long and contain characters from three of the following four sets: Uppercase letters, Lowercase letters, Base 10 digits, and Symbols. You can examine the error log by executing the docker logs command.
```


```sh
Add-Migration UpgradeNET605 -Project Oibi.Repository.Demo.Models
```