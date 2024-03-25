# Market Demo Backend
This is a demo project representing backend part of an imaginary Marketplace web application.

## It uses:
- ASP.NET Core 8.0.0;
- Entity Framework Core;
- Postgre SQL.

## It features: 
- User registration;
- User authentication and authorization using JWT Bearer;
- Simple password encryption using MD5;
- Creation of store items for authorized users;
- Data validation;
- Purchase history;
- Xunit tests using Moq and In-Memory EFCore;
- Postman integration tests.

## What can be done or changed:
- Saving of auth token to allow user log out;
- More secure encryption;
- Separate API Service for user-related actions;
- Implementation of payment service connection and notifications;
- Handling of multiple currencies;
- More tests.
