

---

### Summary

Backend Developer Coding Test for Parcel Tracker

### Features

- API for CRUD operations of Notifications and Client / Rules mapping
- Data persisted using Sqlite
- OpenAPI (Swagger)
- Logging and error handling
- Email sending using SendGrid
- Logic to send email on a rule per Client ID
- Unit tests on `NotificationsController`, particularly for creating notifications (delivery/pickup/reminder)

### Start
To create the database, switch your directory to `.\ParcelTracker\API` and run the following:

```sh
dotnet ef database update -c NotificationsContext -p ..\ParcelTracker.Infrastructure\ParcelTracker.Infrastructure.csproj
dotnet ef database update -c RulesContext -p ..\ParcelTracker.Infrastructure\ParcelTracker.Infrastructure.csproj
```

Go to `appsettings.json` and edit the value of the config `SendGridApiKey`. Provide a valid SendGrid api key to make the email sending working.

Once done, run the webapi and go to: https://localhost:7169/swagger/index.html
to view the Swagger endpoint.

### Assumptions
For the email sending to work, a valid SendGrid api key is required. 