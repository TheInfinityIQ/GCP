# EF Commands

### Package Manager Console (Visual Studio only)
open *package manager console* and run the following to:

- #### add a new migration
    ```powershell
    Add-Migration -Name <DESIRED-NAME>
    ```

- #### undo a migration
    ```powershell
    Remove-Migration
    ```

- #### apply migrations to database
    ```powershell
    Update-Database
    ```

- #### drop/delete database
    ```powershell
    Drop-Database
    ```

### Terminal
open your desired terminal and ensure you are in the `/src/GCP.Api/` folder.

Then run the following to:

- #### add a new migration
    ```powershell
    dotnet ef migrations add <DESIRED-NAME>
    ```

- #### undo a migration
    ```powershell
    dotnet ef migrations remove
    ```

- #### apply migrations to database
    ```powershell
    dotnet ef database update
    ```

- #### drop/delete database
    ```powershell
    dotnet ef database drop
    ```

