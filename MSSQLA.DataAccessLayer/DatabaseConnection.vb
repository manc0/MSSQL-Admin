Imports System.Data.SqlClient

Public Class DatabaseConnection
    Private Shared _connectionString As String = "Connection Timeout={0};"
    Private Shared _connectionLogin As String

    ''' <summary>
    ''' Sets the connection string for future connections.
    ''' </summary>
    ''' <param name="connectionLogin">String containing the login credentials.</param>
    ''' <param name="timeout">Connection timeout.</param>
    Public Shared Sub SetConnectionString(connectionLogin As String, timeout As Integer)
        _connectionLogin = connectionLogin
        _connectionString = String.Format(_connectionString, timeout)
        _connectionString &= _connectionLogin
    End Sub

    ''' <summary>
    ''' Returns the given table from the server as one DataTable object.
    ''' </summary>
    ''' <param name="table">Name of the table.</param>
    ''' <param name="database">Name of the database containing that table.</param>
    ''' <returns>A new DataTable object.</returns>
    Public Shared Function GetDataTable(table As String, database As String) As DataTable
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim cmd As SqlCommand = New SqlCommand("USE " & database, conn)
            cmd.ExecuteNonQuery()

            Dim dataAdapter As New SqlDataAdapter("SELECT * FROM " & table, conn)
            Dim dataTable As New DataTable
            dataAdapter.Fill(dataTable)

            Return dataTable
        End Using
    End Function

    ''' <summary>
    ''' Executes the given query on the server.
    ''' </summary>
    ''' <param name="sqlQuery">Query to execute.</param>
    ''' <param name="database">Database where the command is executed.</param>
    Public Shared Sub PerformAction(sqlQuery As String, database As String)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Using cmd As New SqlCommand("USE " & database & " " & sqlQuery, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Executes a query and returns the value of the first column of the first row.
    ''' </summary>
    ''' <param name="sqlQuery">Query to execute.</param>
    ''' <param name="database">Database where the command is executed.</param>
    ''' <returns></returns>
    Public Shared Function PerformScalar(sqlQuery As String, database As String) As Object
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Using cmd As New SqlCommand("USE " & database & " " & sqlQuery, conn)
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Updates the given table.
    ''' </summary>
    ''' <param name="table">Name of the table.</param>
    ''' <param name="database">Name of the database containing that table.</param>
    ''' <param name="dataTable">DataTable with the changes to be sent to the server.</param>
    Public Shared Sub UpdateDataTable(table As String, database As String, dataTable As DataTable)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim cmd As SqlCommand = New SqlCommand("USE " & database, conn)
            cmd.ExecuteNonQuery()

            Dim dataAdapter As New SqlDataAdapter("SELECT * FROM " & table, conn)
            Dim cmdBuilder As New SqlCommandBuilder(dataAdapter)
            dataAdapter.Update(dataTable)
        End Using
    End Sub

    ''' <summary>
    ''' Executes the given query on the server and returns the results.
    ''' </summary>
    ''' <param name="sqlQuery">Statement used to build the reader.</param>
    ''' <param name="database">Optional parameter that indicates what database to use. Ignore it if the query contains a USE statement.</param>
    ''' <returns>A SqlDataReader object.</returns>
    Public Shared Function GetDataReader(sqlQuery As String, Optional database As String = Nothing) As SqlDataReader
        Dim conn As New SqlConnection(_connectionString)
        conn.Open()

        If database IsNot Nothing Then
            Dim cmd As SqlCommand = New SqlCommand("USE " & database, conn)
            cmd.ExecuteNonQuery()
        End If

        Using cmd As SqlCommand = New SqlCommand(sqlQuery, conn)
            Return cmd.ExecuteReader()
        End Using
    End Function

    ''' <summary>
    ''' Execuetes the given procedure with the given parameters.
    ''' </summary>
    ''' <param name="procedureName">Name of the procedure.</param>
    ''' <param name="database">Name of the database.</param>
    ''' <param name="parameters">Dictionary of parameters with their respective values.</param>
    ''' <returns>A SqlDataReader object.</returns>
    Public Shared Function ExecuteStoredProcedures(procedureName As String, database As String, parameters As Dictionary(Of Dictionary(Of String, SqlDbType), Object)) As SqlDataReader
        Dim conn As New SqlConnection(_connectionString)
        conn.Open()

        Dim cmd1 As SqlCommand = New SqlCommand("USE " & database, conn)
        cmd1.ExecuteNonQuery()

        Using cmd2 As New SqlCommand(procedureName, conn)
            cmd2.CommandType = CommandType.StoredProcedure

            For Each param In parameters
                Dim paramName As String = param.Key.Keys(0)
                Dim type As SqlDbType = param.Key.Values(0)
                Dim value = param.Value

                cmd2.Parameters.Add(paramName, type).Value = value
            Next

            Return cmd2.ExecuteReader()
        End Using
    End Function


    ''' <summary>
    ''' Opens a connection to the database.
    ''' </summary>
    ''' <param name="timeout">Connection timeout</param>
    ''' <returns></returns>
    Public Shared Async Function Open(timeout As Integer) As Task
        SetConnectionString(_connectionLogin, timeout)

        Using conn As New SqlConnection(_connectionString)
            Await conn.OpenAsync()
        End Using
    End Function

End Class
