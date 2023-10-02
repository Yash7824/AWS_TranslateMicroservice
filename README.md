# AWS_TranslateMicroservice

A Mulilingual-Microservice used to translate a web application into another language.

## Architecture

In this project, we are making use of `AWS-Translate` Package to create a POST API which would take a **list-of-words** and a **target-language-code** as a parameter. The List of words contains all the keys which has to be translated into different language whose code is also passed as a parameter. Upon calling the API, all the words present in the list get translated to the desired language and are stored in the respective table in Postgres. The table contains the keys and its corresponding value i.e the translated version of the key. The API is called only once and the data gets inserted into its respective table.
After the Database is ready, any CRUD operation can be performed according to the business requirement. Some of the operations are:
- Adding More Key-Value Pair to the table.
- Updating any Key's Value.
- Updating the key.
- Deletion of any Key-Value tuple from the table.
- Fetching All or some specific data based on **Id**, **Keys** or **Values**.


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

#### Firebase
1) Create a Project in Firebase and then create a realtime database.
2) Storing the Data as HashMap or Dictionary in the database (Key-Value Pair).
3) CRUD operations can be performed in this aswell through .NET

### Client-Side Implementation (Angular)
1) Created an angular project and tried to make a replica of the `ICICI Lombard` Motor Insurance front-page.
2) Making use of `HttpClientModule` and `Rxjs` for calling Server APIs.
3) Implementing the Translation Functionality by making a Dropdown-Menu to select the language in which the web-page should get translated.
4) Made use of bootstrap and custom css for designing purposes.

## Technology Used:

- Cloud-Provider : Amazon Web Service (AWS).
- Backend : Asp.NET Core.
- Database : Postgres/Firebase.
- Frontend : Angular.

