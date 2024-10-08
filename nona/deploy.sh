dotnet build -c Release
dotnet publish -c Release
vercel --prod bin/Release/net8.0/publish/wwwroot/