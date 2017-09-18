iisreset /stop

taskkill /F /IM dotnet.exe /fi "memusage gt 05"
