FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /TasktowerBuild
COPY Tasktower.ProjectService/bin/TasktowerBuild/ .
EXPOSE 5001
EXPOSE 5000
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["dotnet", "Tasktower.ProjectService.dll"]