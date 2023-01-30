# Payment-api

This project has the purpose of recording sales, listing the items sold and the responsible seller.
## Requirement
-	[.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
-	[Git  - Para clonar o projeto](https://git-scm.com/)
## how to run 
1.	Clone the project;
2.	Locate the project's root folder and from there launch your operating system's command prompt;
3.	Navigate to the project folder **Payment-api.Infra.Data** with the command below to perform database migration:
```
cd .\Payment-api.Infra.Data\ 
```
``` 
dotnet ef --startup-project ../Payment-api.WebAPI\Payment-api.WebAPI.csproj  database update
```
4.	Navigate to the **Payment-api.WebAPI** project folder with the command: 
```
cd ..
```

```
cd .\Payment-api.WebAPI\  
```
5.	Run the project : 
```
dotnet run
```

## Registration flow
```
Category --> Product --> Order --> Sale;
Seller --> Sale;
```