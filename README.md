# URL Shortener Coding Task

- [How to build and run locally](#how-to-build-and-run-locally)
  - [Running it in Development Mode/Locally](#running-it-in-development-modelocally)
  - [Running the back end locally](#running-the-back-end-locally)
  - [Running the front end locally](#running-the-front-end-locally)
- [Example Usage](#example-usage)
  - [Frontend](#frontend)
  - [Backend API](#backend-api)
    - [API cURL commands](#api-curl-commands)
- [Notes & Assumptions](#notes--assumptions)

This is a simple **URL shortener** written in C#, ASP.NET Core and Next.js, using a SQLite database.

## How to build and run locally

To get this app to run locally, all you need is docker/docker desktop. 
If you don't have docker desktop, please download it [here](https://www.docker.com/).

If you are runnning this on a Windows machine, please ensure that WSL 2 is enabled.

Before following the steps, please ensure that:
- Docker Desktop is installed
- Docker Desktop is running
- `docker` is on your PATH (should have been set up by Docker Desktop)

1. Download the project
```powershell
git clone https://github.com/poodlehead/code-test-instructions.git
```
2. navigate to the project in powershell/cmd `cd path\to\code-test-instructions`
3. enter
```powershell
docker-compose up --build
```
Note: You can add `-d` after `--build` to run the container in the background.

You should now see it compile. once it's finished navigate to [http://localhost:3000/](http://localhost:3000/) to see the app.

You can also see the swagger API for the back end by navigating to [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

### Running it in Development Mode/Locally

The following things need to be installed on your machine before you can run locally:
- **Backend**
  - C# / .NET 10
  - SQLite
  - Entity Framework Core
- **Frontend**
  - Next.js 16
  - React 19
  - Typescript
  - npm
- **General**
  - Visual Studio Code / Visual Studio 2026
  - Docker

### Running the back end locally

**If you are using Visual Studio Code:**
1. Open this project in VS Code, and navigate to the URLSortnerAPI.csproj in the terminal
```powershell
cd .\backend\UrlShortnerAPI\UrlShortnerAPI>
```
2. Run the entity migrations in order to create the SQLite Database
```powershell
dotnet ef database update
```
3. Run the project
```powershell
dotnet run
```

**If you are using Visual Studio 2026**
1. Open the URLShortnerAPI.sln located in `backend\URLShortnerAPI`

2. In the Package Manager Console (View > Other Windows > Package Manager Console) enter:
```powershell
Update-Database
```
**Note**: Make sure that the Default project in the Package Manager Console is `URLShortnerAPI`

3. Set the startup project to be `URLShortnerAPI`

4. In the Start/Debug Toolbar select **IIS Express** on the Debug Target selector, and click on the Debug Arrow to start the application.

### Running the front end locally

1. Open up this project in VS Code and navigate to the frontend project in the terminal
```powershell
 cd .\frontend\url-shortner\
```

2. run the following npm command to start the front end:
```powershell
npm run dev
```
## Example Usage

### Frontend

Once the Docker container has finished running, navigate to `http://localhost:3000` in your browser.

1. Enter the full URL that you'd like to be shortened in the first field titles "Enter URL to be shortened"

2. (Optional) Enter a custom alias in the "Enter a custom alias field". If you don't provide one, the alias will be a randomly generated GUID instead.

3. Click the "Shorten URL" button.

4. A short URL will be generated and displayed below the "Shorten URL" button. You can copy and paste it for later or click on it and it'll redirect you to your original URL.

### Backend API

You can see the swagger documentation for all the cURL commands below by navigating to [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html).

### API cURL commands

You can use `curl` or any other API client (like postman) to interact with the backend API directly.

#### Shorten a URL

-   **With a custom alias:**
    ```sh
    curl -X POST http://localhost:8080/shorten \
         -H "Content-Type: application/json" \
         -d '{"fullUrl": "https://example.com/a/very/long/url", "customAlias": "my-link"}'
    ```
    *Response:*
    ```json
    {
      "alias": "http://localhost:3000/my-link"
    }
    ```
    This will return a *400 Error Response* stating `"Invalid input or alias already taken"` if the alias has been taken or the input is wrong.


-   **With a random alias:**
    ```sh
    curl -X POST http://localhost:8080/shorten \
         -H "Content-Type: application/json" \
         -d '{"fullUrl": "https://www.google.com"}'
    ```
    *Response (alias will be a randomly generated GUID):*
    ```json
    {
      "alias": "http://localhost:3000/3c8c27a9221b445388c3a9f5d1b7e0d3"
    }
    ```
    This will return a *400 Error Response* stating `"Invalid input or alias already taken"` if the input is wrong.

#### Redirect to the original URL

Open `http://localhost:8080/my-link` in your browser, or use `curl`:

```sh
curl -L http://localhost:8080/my-link
```
This will result in a `302 redirect` to the original URL, and a `404 not found` error if the alias is incorrect.

You can also open `http://localhost:3000/my-link` in your browser, or use `curl`:

```sh
curl -L http://localhost:3000/my-link
```
This will result in a `307 temporary redirect` to the original URL, and an "Error: Not Found" page if there is an error.

#### List all shortened URLs

```sh
curl http://localhost:8080/urls
```
*Response:*
```json
{
  "urlList": [
    {
      "alias": "my-link",
      "fullUrl": "https://example.com/a/very/long/url",
      "shortUrl": "http://localhost:3000/my-link"
    },
    {
      "alias": null,
      "fullUrl": "https://www.google.com",
      "shortUrl": "http://localhost:3000/3c8c27a9221b445388c3a9f5d1b7e0d3"
    }
  ]
}
```

#### Delete a shortened URL

```sh
curl -X DELETE http://localhost:8080/my-link
```
This will return a `204 No Content` status on successful deletion, and a `404 Not Found` if the alias is not found in the database.

## Notes & Assumptions

### OS Expectations
Tested on Windows and Linux (Ubuntu), not tested on macOS or other Linux Kernels.

### Assumptions
- Back end API listens on port 8080 when ran in docker
- Front end listens on port 3000
- This tool is intended for local development only
- Data is stored for each docker instance and is not shared amoungst deployments

### Security Assumptions
- No authentication is implemented in this service
- All data is stored locally in docker containers/local files
- No data is encrypted
- No PII is processed or saved
- Service is not secure enough to be deployed to a PaaS

### Accessibility
- This project has not been designed or tested for accessibility.
- This project has not been evaluated for WCAG (Web Content Accessibility Guidelines) compliance
- Accessibility is not a current goal of this project
- Client-side interactions may require JavaScript to be enabled

### Prerequisites
**Backend**
  - C# / .NET 10
  - SQLite
  - Entity Framework Core

**Frontend**
  - Next.js 16
  - React 19
  - Typescript
  - npm

**General**
  - Visual Studio Code
  - Visual Studio 2026 (optional)
  - Docker