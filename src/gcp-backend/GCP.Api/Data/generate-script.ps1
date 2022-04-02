
$alreadyInstalled = dotnet tool list -g | Select-String -Pattern "dotnet-ef" -Quiet

if (-not $alreadyInstalled) {
    echo "Installing dotnet-ef..."
    dotnet tool install -g dotnet-ef
    echo "Installed dotnet-ef."
}

echo "Generating database scripts for GCP.Api..."
dotnet ef migrations script -i -o "./Data/Scripts/migrations.sql" --no-build
echo "Scripts generated successfully."

cat "./Data/Scripts/migrations.sql" | Set-Content "./Data/Scripts/migrations1.sql"
cat "./Data/Scripts/migrations1.sql" | Set-Content "./Data/Scripts/migrations.sql"
rm "./Data/Scripts/migrations1.sql"
