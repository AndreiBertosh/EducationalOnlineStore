dotnet sonarscanner begin /k:"CartingServiceWEBAPI" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_afd280a4b00148d542d001082caf80f7b375b2c0"
dotnet build 
dotnet sonarscanner end /d:sonar.token="sqp_afd280a4b00148d542d001082caf80f7b375b2c0" 