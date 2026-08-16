# Hot Tub Tri Machine

Hot Tub Tri Machine is a Blazor WebAssembly website with an Azure Functions backend providing contact functionality.

> **Project Status:** ✅ POC Complete

## Overview

Hot Tub Tri Machine is a website created for the Hot Tub Tri Machine project.

The project originally began as a simple web page built free of charge for a personal friend. It subsequently provided practical experience across frontend development, serverless APIs, third-party service integration and cloud deployment.

The site is designed to be:

* Simple
* Responsive
* Easy to maintain
* Suitable for static hosting

## Features

* Blazor WebAssembly frontend
* Responsive web design
* Azure Functions contact API
* Contact form
* SendGrid transactional email integration
* Static assets served from `wwwroot`
* Search engine indexing
* Google Search Console integration
* .NET 8

## Technology Stack

### Frontend

* Blazor WebAssembly
* .NET 8
* HTML / CSS
* Responsive web design

### Backend

* Azure Functions
* .NET 8
* HTTP API
* SendGrid

### Hosting & Services

* Microsoft Azure
* Azure Static Web Apps
* Azure Functions
* SendGrid
* Google Search Console

## Requirements

* .NET 8 SDK
* Azure Functions Core Tools
* Visual Studio 2022 or another compatible .NET development environment
* A modern browser with WebAssembly support
* SendGrid account for testing email delivery locally

## Quick Start

From the repository root:

1. Clone the repository:

```bash
git clone https://github.com/davidcassdigital/HotTubTriMachine.git
cd HotTubTriMachine
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Configure local secrets.

Create `HotTubTriMachine.Api/local.settings.json`:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "SendGridApiKey": "<your-sendgrid-api-key>",
    "SendGridFromEmail": "<your-from-email>",
    "SendGridToEmail": "<your-to-email>"
  }
}
```

Do not commit this file or any API keys, connection strings or other secrets to source control.

4. Start the Azure Functions backend:

```bash
cd HotTubTriMachine.Api
func start
```

5. In a separate terminal, start the Blazor WebAssembly client:

```bash
cd HotTubTriMachine
dotnet run
```

6. Open the URL shown by the development server.

## Project Structure

```text
HotTubTriMachine.sln

HotTubTriMachine/
+-- Components/
+-- Layout/
+-- Pages/
+-- wwwroot/

HotTubTriMachine.Api/
+-- Functions/
+-- ...
```

The exact project structure may vary as the application evolves.

## Configuration

The Blazor WebAssembly client communicates with the Azure Functions API.

For local development, ensure the API URL configured in the client points to the locally running Azure Functions host.

SendGrid configuration is required when testing the contact form and email delivery locally.

Production configuration should be managed through the appropriate Azure application settings rather than committed to source control.

## Deployment

The Blazor WebAssembly client and Azure Functions API are deployed as separate components.

The frontend is suitable for static hosting, while the Azure Functions application provides the backend contact functionality.

Production configuration, including SendGrid credentials and other secrets, is managed outside of source control.

## Development

This project began as a small website but provided practical experience with the complete development and deployment lifecycle.

Areas of practical experience include:

* Blazor WebAssembly development
* Azure Functions and serverless APIs
* Third-party API integration
* Transactional email with SendGrid
* Azure deployment and configuration
* Production troubleshooting
* Search engine indexing
* Google Search Console
* Responsive web development

## Notes

Hot Tub Tri Machine is a personal project created for a friend and is not intended as a commercial product.

The project is maintained primarily as a practical example of building, integrating and deploying a small .NET application using Microsoft Azure and third-party services.

## License

This project is licensed under the MIT License.
