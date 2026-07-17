# Hot Tub Tri Machine

Hot Tub Tri Machine is a Blazor WebAssembly site (client) with an Azure Functions backend (contact API).

## Background
This project began as a simple web page built free of charge for a personal friend. During development I learnt practical deployment and integration tasks such as configuring SendGrid for transactional email delivery and registering the site with Google Search / Search Console for improved indexing.

## Contents
- `HotTubTriMachine/` — Blazor WebAssembly client
- `HotTubTriMachine.Api/` — Azure Functions backend (contact endpoint)
- `wwwroot/` — static assets

## Requirements
- .NET 8 SDK
- Azure Functions Core Tools (for local function host)
- (Optional) SendGrid account for email delivery

## Local development
1. Clone:
   git clone https://github.com/davidcassdigital/HotTubTriMachine.git
   cd HotTubTriMachine

2. Configure secrets (do not commit):
- Create `HotTubTriMachine.Api/local.settings.json` (example):