# Definições de variáveis globais
$nugetApiKey = $env:NugetApiKey
$nugetSource = "https://api.nuget.org/v3/index.json"

# Gera os números de versão com base na data/hora atual
$dataAtual = Get-Date -Format "yyyy.MM.dd.HHmm"
$dataVersao = Get-Date -Format "yyyyMMdd.HHmm"
$revisao = (Get-Date).Second.ToString("00")

# Novos valores de versão
$assemblyVersion = "$dataAtual"
$packageVersion = "$dataVersao.$revisao"

Write-Host "Nova versão gerada: AssemblyVersion=$assemblyVersion, Version=$packageVersion"

function Start-Test {
    $testProjectPath = "Unimake.Test\Unimake.Test.csproj"

    try {
        Write-Host "Executando testes unitários..."
        $null = & dotnet test $testProjectPath /p:Configuration=Debug --no-build --verbosity normal

        if ($LASTEXITCODE -ne 0) {
            throw "Testes falharam com código $LASTEXITCODE. Verifique o log."
        }
    }
    catch {
        Write-Host "Erro nos testes: $_" -ForegroundColor Red
        throw  
    }

    return $true
}

function Publish-NuGetPackage {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [string]$projectName
    )

    $projectFile = Join-Path $projectName "$projectName.csproj"

    # Atualiza os valores no arquivo .csproj
    Write-Host "Atualizando versões no arquivo do projeto..."
    (Get-Content $projectFile) -replace "<AssemblyVersion>.*?</AssemblyVersion>", "<AssemblyVersion>$assemblyVersion</AssemblyVersion>" `
        -replace "<Version>.*?</Version>", "<Version>$packageVersion</Version>" |
    Set-Content $projectFile

    # Compila o projeto
    Write-Host "Compilando o projeto..."
    & dotnet build $projectFile /p:Configuration=Release
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Erro na compilação!" -ForegroundColor Red
        exit 1
    }

    # Empacota o projeto
    Write-Host "Empacotando o projeto..."
    & dotnet pack $projectFile /p:Configuration=Release --no-build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Erro ao empacotar!" -ForegroundColor Red
        exit 1
    }

    # Encontra o último pacote gerado na pasta bin\Release
    Write-Host "Procurando o último pacote NuGet..."
    $package = Get-ChildItem -Path (Join-Path (Split-Path -Parent $projectFile) "bin\Release") -Filter "*.nupkg" |
    Sort-Object LastWriteTime -Descending |
    Select-Object -First 1

    if (-not $package) {
        Write-Host "Nenhum pacote NuGet encontrado!" -ForegroundColor Red
        exit 1
    }

    Write-Host "Pacote encontrado: $($package.FullName)"

    # Publica o pacote no NuGet
    Write-Host "Publicando pacote no NuGet..."
    & dotnet nuget push $package.FullName -k $nugetApiKey -s $nugetSource --skip-duplicate

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Erro ao publicar o pacote no NuGet!" -ForegroundColor Red
        exit 1
    }

    Write-Host "Publicação concluída com sucesso!" -ForegroundColor Green
    Start-Process "https://www.nuget.org/packages/$projectName/$packageVersion"
}

# Verifica se a variável de ambiente NugetApiKey foi definida
if (-not $env:NugetApiKey) {
    Write-Host "Variável de ambiente NugetApiKey não existe ou está vazia." -ForegroundColor Red
    exit 1
}

if (-not (Start-Test)) {
    Write-Host "Os Testes falharam. Interrompendo publicação." -ForegroundColor Red
    exit 1
}

Write-Host "Publicando projetos ..." -ForegroundColor Blue

Publish-NuGetPackage -projectName "Unimake.Cryptography"
Publish-NuGetPackage -projectName "Unimake.Extensions"
Publish-NuGetPackage -projectName "Unimake.Primitives"
Publish-NuGetPackage -projectName "Unimake.Utils"

Write-Host "Os projetos foram publicados com sucesso!" -ForegroundColor Blue