# Define SQL Server connection details
$server = "localhost"
$database = "Budget"
$username = "sa"
$password = "1234qwer"
$tablesPath = ".\Tables\*.sql"
$storedProceduresPath = ".\Stored Procedures\*.sql"

# Function to execute a raw SQL query
function Execute-SqlQuery {
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
        Write-Host "Executed SQL query successfully." -ForegroundColor Green
    } catch {
        Write-Host "Error executing SQL query: $_" -ForegroundColor Red
    } finally {
        $connection.Close()
    }
}

function Execute-TableScript {
    param (
        [string]$filePath
    )
    $tableName = [System.IO.Path]::GetFileNameWithoutExtension($filePath)  # Extract table name from file name
    $tableScript = Get-Content -Path $filePath -Raw

    # Wrap table creation in IF NOT EXISTS
    $checkQuery = @"
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '$tableName')
    BEGIN
        EXEC sp_executesql N'$($tableScript.Replace("'", "''"))'
    END
"@
    
    Execute-SqlQuery -query $checkQuery
    Write-Host "Checked & Created Table (if not exists): $tableName" -ForegroundColor Cyan
}

# Function to execute a SQL script file for stored procedures (always recreate)
function Execute-StoredProcedureScript {
    param (
        [string]$filePath
    )
    $query = Get-Content -Path $filePath -Raw
    Execute-SqlQuery -query $query
    Write-Host "Recreated Stored Procedure: $filePath" -ForegroundColor Cyan
}

# Step 1: Process tables (Only create if they do not exist)
$tables = Get-ChildItem -Path $tablesPath
foreach ($table in $tables) {
    Execute-TableScript -filePath $table.FullName
}

# Step 2: Recreate Stored Procedures
$dropProceduresQuery = @"
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'DROP PROCEDURE IF EXISTS [' + name + '];' + CHAR(13)
FROM sys.procedures;
EXEC sp_executesql @sql;
"@
Execute-SqlQuery -query $dropProceduresQuery
Write-Host "Dropped all existing stored procedures." -ForegroundColor Yellow

$storedProcedures = Get-ChildItem -Path $storedProceduresPath
foreach ($sp in $storedProcedures) {
    Execute-StoredProcedureScript -filePath $sp.FullName
}

Write-Host "✅ SQL tables checked (only new tables created), and stored procedures recreated!" -ForegroundColor Cyan
