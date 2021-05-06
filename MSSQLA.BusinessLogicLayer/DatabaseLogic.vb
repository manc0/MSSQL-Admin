Imports System.Data.SqlClient
Imports MSSQLA.DataAccessLayer

Public Class DatabaseLogic

    ''' <summary>
    ''' Sets the connection to the server.
    ''' </summary>
    Public Sub SetConnection(server As String, user As String, pass As String, timeout As Integer)
        Dim ConnectionString As String = "Server=" & server & ";" & "User Id=" & user & ";" & "Password=" & pass & ";"
        DatabaseConnection.SetConnectionString(ConnectionString, timeout)
    End Sub

    ''' <summary>
    ''' Returns a list of all databases in the server.
    ''' </summary>
    Public Function GetDatabasesList() As List(Of String)
        Dim databaseList As New List(Of String)()
        Dim sqlQuery = "SELECT name FROM master.sys.databases"
        Dim reader As SqlDataReader = DatabaseConnection.GetDataReader(sqlQuery)

        While (reader.Read())
            databaseList.Add(reader.GetString(0))
        End While

        reader.Close()
        Return databaseList
    End Function

    ''' <summary>
    ''' Returns a list of all tables from the given database.
    ''' </summary>
    ''' <param name="database">Name of the database.</param>
    ''' <returns></returns>
    Public Function GetTablesListFromDatabase(database As String) As List(Of String)
        Dim tablesList As New List(Of String)()
        Dim sqlQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM " & database & ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME <> 'sysdiagrams'"
        Dim reader As SqlDataReader = DatabaseConnection.GetDataReader(sqlQuery)

        While (reader.Read())
            tablesList.Add(reader.GetString(0) + "." + reader.GetString(1))
        End While

        reader.Close()
        Return tablesList
    End Function

    Public Function GetProceduresListFromDatabase(database As String) As List(Of String)
        Dim proceduresList As New List(Of String)()
        Dim sqlQuery = "SELECT SPECIFIC_SCHEMA, SPECIFIC_NAME FROM " & database & ".INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'  "
        Dim reader As SqlDataReader = DatabaseConnection.GetDataReader(sqlQuery)

        While (reader.Read())
            proceduresList.Add(reader.GetString(0) + "." + reader.GetString(1))
        End While

        reader.Close()
        Return proceduresList
    End Function

    Public Function GetFunctionsListFromDatabase(database As String) As List(Of String)
        Dim functionsList As New List(Of String)()
        Dim sqlQuery = "SELECT SPECIFIC_SCHEMA, SPECIFIC_NAME FROM " & database & ".INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION'  "
        Dim reader As SqlDataReader = DatabaseConnection.GetDataReader(sqlQuery)

        While (reader.Read())
            functionsList.Add(reader.GetString(0) + "." + reader.GetString(1))
        End While

        reader.Close()
        Return functionsList
    End Function

    Public Function GetViewsListFromDatabase(database As String) As List(Of String)
        Dim viewsList As New List(Of String)()
        Dim sqlQuery = "SELECT TABLE_SCHEMA, TABLE_NAME FROM " & database & ".INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME <> 'sysdiagrams'"
        Dim reader As SqlDataReader = DatabaseConnection.GetDataReader(sqlQuery)

        While (reader.Read())
            viewsList.Add(reader.GetString(0) + "." + reader.GetString(1))
        End While

        reader.Close()
        Return viewsList
    End Function

    ''' <summary>
    ''' Returns a table from the database.
    ''' </summary>
    ''' <param name="table">Name of the table.</param>
    ''' <param name="database">Name of the database.</param>
    ''' <returns></returns>
    Public Function GetTableFromDataBase(table As String, database As String) As DataTable
        Return DatabaseConnection.GetDataTable(table, database)
    End Function

    ''' <summary>
    ''' Updates a table from the database.
    ''' </summary>
    ''' <param name="table">Name of the table.</param>
    ''' <param name="database"></param>
    ''' <param name="dataTable"></param>
    Public Sub UpdateDataTable(table As String, database As String, dataTable As DataTable)
        DatabaseConnection.UpdateDataTable(table, database, dataTable)
    End Sub

    ''' <summary>
    ''' Executes the given code on the server.
    ''' </summary>
    ''' <param name="sqlCode">Code to be executed on the server.</param>
    ''' <returns>Returns a new list of DataTables and the number of rows affected.</returns>
    Public Function ExecuteSqlCode(sqlCode As String) As Object
        Using reader As SqlDataReader = DatabaseConnection.GetDataReader(sqlCode)
            Dim rowsAffected As Integer = reader.RecordsAffected
            Dim dtList As New List(Of DataTable)()
            Dim dt As DataTable

            While Not reader.IsClosed
                dt = New DataTable()
                dt.Load(reader)
                dtList.Add(dt)
            End While

            dtList = (From table In dtList
                      Select table
                      Where table.Rows.Count <> 0 Or table.Columns.Count <> 0).ToList()

            Return {dtList, rowsAffected}
        End Using
    End Function

    ''' <summary>
    ''' Test the connection to the server.
    ''' </summary>
    ''' <param name="timeout">Connection timeout.</param>
    ''' <returns></returns>
    Public Async Function TestConnection(timeout As Integer) As Task
        Await DatabaseConnection.Open(timeout)
    End Function

End Class
