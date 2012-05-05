@echo off
set path=%path%;C:/Windows/Microsoft.NET/Framework/v4.0.30319;

echo Building project...
msbuild src/Anodyne.sln /nologo /v:q /p:Configuration=Release /t:Clean
msbuild src/Anodyne.sln /nologo /v:q /p:Configuration=Release /clp:ErrorsOnly

echo Copying assemblies...
if exist output rmdir /s /q output
mkdir output
mkdir output\bin

copy src\main\Anodyne-CommandBus\bin\Release\Anodyne-CommandBus.* output\bin
copy src\main\Anodyne-Common\bin\Release\Anodyne-Common.* output\bin
copy src\main\Anodyne-DataAccess\bin\Release\Anodyne-DataAccess.* output\bin
copy src\main\Anodyne-Node\bin\Release\Anodyne-Node.* output\bin
copy src\main\Anodyne-Domain\bin\Release\Anodyne-Domain.* output\bin
copy src\main\Anodyne-Wiring\bin\Release\Anodyne-Wiring.* output\bin
copy src\main\Anodyne-Windsor\bin\Release\Anodyne-Windsor.* output\bin
copy src\main\Anodyne-MongoDb\bin\Release\Anodyne-MongoDb.* output\bin
copy src\main\Anodyne-Log4Net\bin\Release\Anodyne-Log4Net.* output\bin

echo Done.