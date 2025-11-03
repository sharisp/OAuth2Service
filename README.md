````markdown
# Andrew.CommonUse.OAuth

A lightweight and extensible **OAuth 2.0 integration framework** for .NET applications.  
Provides a unified interface to handle multiple OAuth providers such as **Google**, **GitHub**, **Microsoft**, and more.  
(Currently supports **Google**, with more providers coming soon.)

---

## 🚀 Features

- ✅ Simple and clean API for **Google OAuth 2.0**
- 🧩 Unified OAuth interface across multiple providers
- ⚙️ Configuration-driven via `appsettings.json`
- 🧠 Fully supports **Dependency Injection** and `IOptions<T>`
- 🔒 Built-in state validation and secure token exchange
- 🧪 Designed for easy unit testing and mocking
- 🔧 Easily extendable for future providers (GitHub, Microsoft, Facebook, etc.)

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package Andrew.CommonUse.OAuth
````

---

## ⚙️ Configuration

Add your OAuth settings to `appsettings.json`:

```json
{
  "OAuth": {
    "Google": {
      "AuthUrl": "https://accounts.google.com/o/oauth2/v2/auth",
      "TokenUrl": "https://oauth2.googleapis.com/token",
      "ClientId": "YOUR_CLIENT_ID",
      "ClientSecret": "YOUR_CLIENT_SECRET",
      "RedirectUrl": "https://localhost:5001/api/auth/callback",
      "Scope": "profile email"
    }
  }
}
```

---

## 🧩 Usage Example

### 1️⃣ Register OAuth service in DI

```csharp
builder.Services.AddOAuthService(builder.Configuration);
```

### 2️⃣ Inject and use the service

```csharp
public class OAuthController(
    [FromKeyedServices(OAuthProviderEnum.Google)] IOAuthService googleOAuthService
) : ControllerBase
{
  
//get GetAuthorizationUrl
        var state = Guid.NewGuid().ToString();
        var authUrl = googleOAuthService.GetAuthorizationUrl(state);
 
 // get callback userinfo
    // GET: api/auth/callback
    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string state, string code, string error = "")
    {
        var response = await googleOAuthService.OAuthCallBack(state, code, error);
        if (!response.Success)
            return BadRequest(response.ErrorMsg);

        return Ok(response.UserInfo);
    }
}
```

### 3️⃣ Example output

```
Visit this URL to authorize: https://accounts.google.com/o/oauth2/v2/auth?...
User Info: {"Id":"12345","Email":"user@gmail.com","Name":"John Doe"}
```

---

## 🧰 Extend to Other Providers

Implement your own provider by inheriting from `IOAuthService` and configuring new endpoints in `appsettings.json`.
Each provider can define its own:

* Authorization URL
* Token URL
* User Info URL
* Client credentials and scopes

---

## 🌐 Links

* **NuGet:** [Andrew.CommonUse.OAuth](https://www.nuget.org/packages/Andrew.CommonUse.OAuth)
* **GitHub:** [https://github.com/sharisp/OAuth2Service](https://github.com/sharisp/OAuth2Service)

---

⭐ **If you find this project helpful, please give it a star on GitHub!**

````

---

## 💡 Optional Additions

You could add:
1. **Badges** (NuGet, build status, license, etc.)
   ```markdown
   [![NuGet](https://img.shields.io/nuget/v/Andrew.CommonUse.OAuth.svg)](https://www.nuget.org/packages/Andrew.CommonUse.OAuth)
   [![License](https://img.shields.io/github/license/sharisp/OAuth2Service.svg)](LICENSE)
````

2. **Roadmap or To-Do section**

   ```markdown
   ## 🛣️ Roadmap
   - [x] Google OAuth support  
   - [ ] GitHub OAuth support  
   - [ ] Microsoft OAuth support  
   - [ ] Facebook OAuth support  
   ```


