dotnet sonarscanner begin /k:"CartingServiceBusinessLogic" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_e3bbb759aaa311d543ca605f1de33b50ec1ad704"
dotnet build 
dotnet sonarscanner end /d:sonar.token="sqp_e3bbb759aaa311d543ca605f1de33b50ec1ad704" 