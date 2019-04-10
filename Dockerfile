FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
EXPOSE 80
ADD ConfigServer/obj/Docker/publish /app
RUN cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
ENTRYPOINT ["dotnet", "ConfigServer.dll"]
