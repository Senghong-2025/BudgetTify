# Define SQL Server connection details
$server = "localhost"
$database = "Budget"
$username = "sa"
$password = "1234qwer"
$tablesPath = ".\Tables\*.sql"
$storedProceduresPath = ".\Stored Procedures\*.sql"
$functionsPath = ".\Functions\*.sql"
$alterTablesPath = ".\Scripts\*.sql"

# Function to execute a raw SQL query
function Invoke-SqlQuery {
    param (
        [string]$query
    )
    $connectionString = "Server=$server;Database=$database;User Id=$username;Password=$password;"
    $connection = New-Object System.Data.SqlClient.SqlConnection
    $connection.ConnectionString = $connectionString
    $command = $connection.CreateCommand()
    $command.CommandText = $query

    try {
        $connection.Open()
        $command.ExecuteNonQuery()
        Write-Host "✅ Executed SQL query successfully." -ForegroundColor Green
    } catch {
        Write-Host "❌ Error executing SQL query: $_" -ForegroundColor Red
    } finally {
        $connection.Close()
    }
}

# Function to execute a CREATE TABLE script safely
function Invoke-TableScript {
    param (
        [string]$filePath
    )
    $tableName = [System.IO.Path]::GetFileNameWithoutExtension($filePath)
    $tableScript = Get-Content -Path $filePath -Raw

    $checkQuery = @"
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '$tableName')
    BEGIN
        EXEC sp_executesql N'$($tableScript.Replace("'", "''"))'
    END
"@
    
    Invoke-SqlQuery -query $checkQuery
    Write-Host "✅ Checked & Created Table (if not exists): $tableName" -ForegroundColor Cyan
}

# Function to execute a SQL script file for stored procedures (always recreate)
function Invoke-StoredProcedureScript {
    param (
        [string]$filePath
    )
    $query = Get-Content -Path $filePath -Raw
    Invoke-SqlQuery -query $query
    Write-Host "✅ Recreated Stored Procedure: $filePath" -ForegroundColor Cyan
}

# Function to execute a SQL script file for functions (always recreate)
function Invoke-FunctionScript {
    param (
        [string]$filePath
    )
    $query = Get-Content -Path $filePath -Raw
    Execute-SqlQuery -query $query
    Write-Host "✅ Recreated Function: $filePath" -ForegroundColor Cyan
}

# Function to execute ALTER TABLE scripts safely
function Invoke-AlterTableScript {
    param (
        [string]$filePath
    )
    $query = Get-Content -Path $filePath -Raw
    Execute-SqlQuery -query $query
    Write-Host "✅ Executed ALTER TABLE script: $filePath" -ForegroundColor Cyan
}

# Step 1: Process tables (Only create if they do not exist)
$tables = Get-ChildItem -Path $tablesPath
foreach ($table in $tables) {
    Invoke-TableScript -filePath $table.FullName
}

# Step 2: Process ALTER TABLE scripts
$alterTables = Get-ChildItem -Path $alterTablesPath
foreach ($alter in $alterTables) {
    Invoke-AlterTableScript -filePath $alter.FullName
}

# Step 3: Recreate Stored Procedures
$dropProceduresQuery = @"
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'DROP PROCEDURE IF EXISTS [' + name + '];' + CHAR(13)
FROM sys.procedures;
EXEC sp_executesql @sql;
"@
Invoke-SqlQuery -query $dropProceduresQuery
Write-Host "✅ Dropped all existing stored procedures." -ForegroundColor Yellow

$storedProcedures = Get-ChildItem -Path $storedProceduresPath
foreach ($sp in $storedProcedures) {
    Invoke-StoredProcedureScript -filePath $sp.FullName
}

# Step 4: Recreate Functions
$dropFunctionsQuery = @"
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'DROP FUNCTION IF EXISTS [' + name + '];' + CHAR(13)
FROM sys.objects
WHERE type = 'FN';
EXEC sp_executesql @sql;
"@
Invoke-SqlQuery -query $dropFunctionsQuery
Write-Host "✅ Dropped all existing functions." -ForegroundColor Yellow

# Get function script files
$functions = Get-ChildItem -Path $functionsPath -Filter "*.sql"

# Check if there are any function scripts
if ($functions.Count -eq 0) {
    Write-Host "⚠️ No function scripts found in '$functionsPath'. Skipping function creation." -ForegroundColor Yellow
} else {
    foreach ($function in $functions) {
        Invoke-FunctionScript -filePath $function.FullName
    }
    Write-Host "✅ All function scripts executed successfully." -ForegroundColor Green
}


Write-Host "✅ SQL tables checked (only new tables created), ALTER TABLE scripts applied safely, stored procedures recreated, and functions recreated!" -ForegroundColor Cyan