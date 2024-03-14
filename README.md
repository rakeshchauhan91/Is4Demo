# Is4Demo

This demo using Duende IdentityServer 4 with in memory clients,scopes users and 2 client (console & Web) 

Visual studio solution has 4 projects. 

**Client**  https://localhost:7047/
  Console Client is console application in .net 7.0,  which uses client credentials to procure token from identity server and then call the API method 
  Web client is web application in .net 7.0, which uses Duenede identity server for IDP to allow users to login into the web application and consume API. 
  
**API**  https://localhost:7003/swagger/index.html
  It is .net core 7.0 API which is guarded by identity server 4
  
**IdentityServer** https://localhost:5101/
  It is .net core 6.0 web project referencing the duende identity server with UI  with configured Test users 

**How To Run**
Run the solution by starting IdentityServer > API > Web Client using multiple startup projects in Visual studio

Identity server should be run before API so that it can guard the API. 

Login into web Client using the following credentials.
**Credentials**
   Username = "rakesh",
   Password = "test@123",
