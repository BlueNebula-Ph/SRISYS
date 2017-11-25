FROM microsoft/aspnetcore:1.1.2

COPY ./SRISYS.Web /SRISYS
WORKDIR /SRISYS

EXPOSE 5000
ENTRYPOINT ["dotnet", "Srisys.Web.dll"]