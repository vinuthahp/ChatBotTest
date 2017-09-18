cd C:\Users\Administrator\aspnetlexchatbot

iisreset /start

start /b cmd /c C:\Users\Administrator\AppData\Local\Microsoft\dotnet\dotnet.exe dotnetLexChatBot.dll > service.log

echo "finished starting"

exit 0
