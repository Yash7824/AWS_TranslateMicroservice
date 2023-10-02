# AWS_TranslateMicroservice

A Mulilingual-Microservice used to translate a web application into another language.

## Brief Description

### Cloud-Side Implementation (AWS)
1) Create an `AWS` account if you don't have one.
2) Log in to the AWS Management Console.
3) Navigate to the IAM service and create a new `IAM user` with programmatic access.
4) Attach the `AmazonTranslateFullAccess` policy to the IAM user to grant Translate API access.
5) Fetch the `access-key` and `secret-key` of the user and mention it in the `appsettings.json` file of the backend project.

### Server-Side Implementation (Asp.NET Core)
1) Create a Asp.NET Core web API project in **.NET 5.0** version.
2) Refer the `server` folder for code.
3) Make sure to install all the packages used in the project.
   - Right click the Project and select `Manage Nuget Packages` option
   - Click on the installed option and verify whether all the packages are installed
   - If all are installed, you may proceed.
   - If not, kindly install it on pressing `Alt + Enter` on red squiggly lines.
4) Make sure you have any API Testing software installed in your system to check all the end-points (I have used `Postman`).
5) You may also use openAPI support for this project by clicking on <br>
   <b>Properties --> launchsettings.json --> (change launchBrowser value to `true`)</b>

### Database-Side Implementation

#### Postgres
1) Made use of `Entity-Framework-Core` package in .NET to handle all the CRUD operations.
2) All Regional Languages table has to be created and then the business logic would be applied to them.
3) ID, Key, Value : Columns

### Client-Side Implementation (Angular)
1) Created an angular project and tried to make a replica of the `ICICI Lombard` Motor Insurance front-page.
2) Making use of `HttpClientModule` and `Rxjs` for calling server apis.

## Technology Used:
```
Cloud-Provider : Amazon Web Service (AWS).
Backend : Asp.NET Core.
Database : Postgres/Firebase.
Frontend : Angular.
```
