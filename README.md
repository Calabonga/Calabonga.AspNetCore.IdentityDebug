# Calabonga.AspNetCore.IdentityDebug for ASP.NET Core

Simple but very helpful library that's allow you to attach required identity (ClaimIrentity) for View or PageModel debuging.

## Why

Sometime authentication server is far away, and you need to design a views for some roles. You may not have an account (login+pass) to view how IO changed. That's why this nuget-package.

## v.1.0.0 2025-10-29

* First release.

## How to use

1. Install nuget package

    ```powershell
    dotnet add package Calabonga.AspNetCore.IdentityDebug
    ```

2. Create a text-file, for example `Administrator.identity`. This file contains a claims will loaded when you start debug you application. The content should be like below:

    ```json
    {
      "items": [
        {
          "key": "givenname",
          "value": "DEBUG"
        },
        {
          "key": "surname",
          "value": "Adminstator"
        },
        {
          "key": "nameidentifier",
          "value": "0231bd57-6d45-4086-bc16-99e77978e0f5"
        },
        {
          "key": "name",
          "value": "Administrator"
        },
        {
          "key": "role",
          "value": "Administrator"
        },
        {
          "key": "role",
          "value": "MyService"
        },
        {
          "key": "role",
          "value": "Supervisor"
        },
        {
          "key": "role",
          "value": "Manager"
        },
        {
          "key": "role",
          "value": "Editor"
        }
      ]
    }
    ```

3. Replace you real `.AddAuthentication()` method with new one from this nuget-package^
    ```csharp
    builder.Services
        .AddAuthenticationDebug("c:\\Temp\\Administrator.identity")
        //.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/connect/login";
            options.LogoutPath = "/connect/logout";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.SlidingExpiration = false;
        });
    ```

4. Done. When you start your application it load file from the path you set and attach into context user with claims you define in file.
  
=screenshot=

5. You can use identity-files as you wish. Something like this:
    ```csharp
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
    #if ADMINISTRATOR
                .AddAuthenticationDebug("c:\\Temp\\Administrator.identity")
    #elif MANAGER
                .AddAuthenticationDebug("c:\\Temp\\Manager.identity")
    #elif VIEWER
                .AddAuthenticationDebug("c:\\Temp\\Viewer.identity")
    #else
                .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
    #endif
                .AddCookie(options =>
                {
                    options.LoginPath = "/connect/login";
                    options.LogoutPath = "/connect/logout";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = false;
                });
        }
    ```

    In this case you should just select a run configuration for VisualStudio, for example.

6. Have a nice DEBUG!
