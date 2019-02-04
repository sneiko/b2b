FROM microsoft/dotnet:2.1-sdk

WORKDIR /app

COPY *.csproj ./

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

WORKDIR /app

CMD ["dotnet", "out/B2BApi.dll"]