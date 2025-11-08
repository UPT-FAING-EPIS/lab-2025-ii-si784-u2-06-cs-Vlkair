#!/bin/bash

# Script para configurar el entorno de pruebas
# Este script instala Playwright y sus dependencias antes de ejecutar los tests

echo "🔧 Configurando entorno de pruebas..."

# Verificar si estamos en CI
if [ -n "$CI" ]; then
    echo "📦 Entorno CI detectado"
fi

# Restaurar dependencias
echo "📥 Restaurando dependencias de .NET..."
dotnet restore

# Compilar el proyecto
echo "🔨 Compilando el proyecto..."
dotnet build --no-restore

# Instalar navegadores de Playwright
echo "🎭 Instalando navegadores de Playwright..."
if [ -f "UPTSiteTests/bin/Debug/net8.0/playwright.ps1" ]; then
    pwsh UPTSiteTests/bin/Debug/net8.0/playwright.ps1 install --with-deps
    echo "✅ Playwright instalado correctamente"
else
    echo "⚠️  Advertencia: No se encontró el script de Playwright"
fi

echo "✅ Configuración completada"
