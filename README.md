# VK Donors
DonorSearch.org web app demo for VKHackathon 2018.

Frontend repository is: https://github.com/DenisNP/DonorSearchFrontend

## Project setup
This project uses ASP.NET Core WebAPI 2.1 as main runtime.

To build from sources you need SDK.

To install it, follow official Microsoft guide: https://www.microsoft.com/net/download/dotnet-core/2.1

PostgreSQL database on localhost is required.

To install it, follow official PostgreSQL guide: https://www.postgresql.org/download/

## Running 
1. Clone this repo
```
git clone
```
2. Restore the dependencies and tools of a project.
```
dotnet restore
```
4. Check databse connections string, api keys and logging level in `appsettings.json` 

5. Run the application to get started. 
```
dotnet run
```
By default `kestrel` uses port `5000` fot HTTP and port `5001` for HTTPS.

For API documentation we use Swagger (https://swagger.io/)

To check the API use `localhost:5000/swagger`



