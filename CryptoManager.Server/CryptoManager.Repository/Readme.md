#  How to update or create database using entity framework code first

## 1 - Open Windows powerShell
## 2 - Look for project repository folder
## 3 - Write a command --> "dotnet ef migrations add {migrationName} --context {DBContextName}" | will create a class {migrationName} in migrations folder
## 4 - Write a command --> "dotnet ef database update --context {DBContextName}" | Will execute the script in database
