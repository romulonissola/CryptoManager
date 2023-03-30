#  How to update or create database using entity framework code first

## 1 - Open Windows powerShell
## 2 - Look for project repository folder
## 3 - Write a command --> "dotnet ef migrations add {migrationName} --context ApplicationIdentityDBContext" | will create a class {migrationName} in migrations folder
## 4 - Write a command --> "dotnet ef database update --context ApplicationIdentityDBContext" | Will execute the script in database

# How to run migrations on azure dbs

## 1 - Open Windows powerShell
## 2 - Look for project repository folder
## 3 - Write a command --> "dotnet ef migrations script --context ApplicationIdentityDBContext" | it will create scripts to run in DB
## 4 - copy the script and run manually in the desired DB