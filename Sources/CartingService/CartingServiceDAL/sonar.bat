dotnet sonarscanner begin /k:"CartingServiceDAL" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_f452244b540aab2d28a9d2e43e0da6ad862113b8"
dotnet build 
dotnet test --collect "Code Coverage"
dotnet sonarscanner end /d:sonar.token="sqp_f452244b540aab2d28a9d2e43e0da6ad862113b8" 