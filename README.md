# MSSQL-Admin
Simple database administrator for SQL Server.

## How to use
To connect to your server you must specify the server address and instance, a user and a password.
After connecting to the server, you will be able to choose which database to use from the list. When a 
database is selected all of its tables will be shown.

![MSSQL-Admin](Screenshots/screenshot1.png)

## Features
- Advanced SQL editor powered by ScintillaNET, that also supports autocompletion
- Fast table visualization
- Manually insert, delete or update and submit to server
- Export tables as CSV and XML
- XPath evaluator
- Output log
- Find and replace

![MSSQL-Admin](Screenshots/screenshot2.png)

## NuGet packages
This project uses the following NuGet packages:
- [ScintillaNET](https://www.nuget.org/packages/jacobslusser.ScintillaNET/3.6.3?_src=template)
- [AutoCompleteMenu-ScintillaNET](https://www.nuget.org/packages/AutoCompleteMenu-ScintillaNET/1.6.2?_src=template)
- [ScintillaNET_FindReplaceDialog](https://www.nuget.org/packages/ScintillaNET_FindReplaceDialog/1.0.3.3?_src=template)
