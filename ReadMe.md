# What is this for?
Simple dotnet application that returns a 200 response if multiple targets respond with a successful web response. 

# How to use?
Configure the LocalProbe property in the appsettings.json and host application on platform that supports net7.0

Example:
```
"LocalProbe": {    
    "paths": [
      {
        "ip": "74.125.136.105",
        "hostname": "www.google.com",
        "path": "/",
        "port": 443,
        "ssl": true
      },
      {
        "ip": "23.62.217.177",
        "hostname": "www.microsoft.com",
        "path": "/",
        "port": 443,
        "ssl": true
      }
    ]
  }
```