
# OAuth Integration for .NET

A lightweight and extensible OAuth 2.0 integration framework for .NET applications.  
Provides a unified interface to handle multiple OAuth providers such as **Google**, **GitHub**, **Microsoft**, and more,currently ,only Google

---

## 🚀 Features

-  Simple and clean API for Google OAuth 2.0 flow
-  Unified OAuth 2.0 interface across multiple providers  
-  Easily extendable for Google, GitHub, Microsoft, Facebook, etc.  
-  Configuration-driven with `appsettings.json`  
-  Built-in support for dependency injection and `IOptions<T>`  
-  Simple to test and integrate with unit testing frameworks  
-  Secure state validation and token exchange handling  

---

## 📦 Installation

Install from NuGet:

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

```csharp
// Generate authorization URL
var state = Guid.NewGuid().ToString();
var authUrl = service.GetAuthorizationUrl(state);
Console.WriteLine($"Visit this URL to authorize: {authUrl}");

// Handle callback
var code = "CODE_FROM_GOOGLE";
var userInfo = await service.OAuthCallBack(state, code);
Console.WriteLine($"User Info: {JsonConvert.SerializeObject(userInfo)}");
```

## 🌐 Links

* **NuGet:** Andrew.CommonUse.OAuth
* **GitHub:** [https://github.com/sharisp/OAuth2Service](https://github.com/sharisp/OAuth2Service)

---

⭐ **If you find this project helpful, please give it a star on GitHub!**

```