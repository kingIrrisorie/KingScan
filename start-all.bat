@echo off
setlocal

set "ROOT=%~dp0"
set "API_DIR=%ROOT%APIManga"
set "SITE_DIR=%ROOT%frontend\users"
set "SITE_URL=http://127.0.0.1:8080/index.html"

echo ========================================
echo KingScan - iniciar API e Frontend
echo ========================================
echo.

echo Atualizando banco de dados...
pushd "%API_DIR%"
dotnet ef database update
if errorlevel 1 (
    echo.
    echo Erro ao atualizar o banco. Verifique o SQL Server e tente novamente.
    popd
    pause
    exit /b 1
)
popd

echo.
echo Iniciando Backend/API em janela minimizada...
start "KingScan Backend API" /min cmd /k "cd /d ""%API_DIR%"" && dotnet run"

echo Aguardando a API iniciar...
timeout /t 6 /nobreak > nul

echo Iniciando Frontend em janela minimizada...
start "KingScan Frontend" /min cmd /k "cd /d ""%SITE_DIR%"" && python -m http.server 8080 --bind 127.0.0.1"

echo Abrindo site...
timeout /t 2 /nobreak > nul
start "" "%SITE_URL%"

echo.
echo Pronto.
echo API:  https://localhost:5215/swagger
echo Site: %SITE_URL%
echo.
echo Pode fechar esta janela. Para parar o projeto, feche as janelas minimizadas do Backend/API e do Frontend.
pause
