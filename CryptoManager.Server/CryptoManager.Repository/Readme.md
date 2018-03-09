#  How to update or create database using entity framework code first

## 1 - Open Windows powerShell
## 2 - Look for project repository folder
## 3 - Write a command --> "dotnet ef migrations add {migrationName}" | will create a class {migrationName} in migrations folder
## 4 - Write a command --> "dotnet ef database update" | Will execute the script in database
