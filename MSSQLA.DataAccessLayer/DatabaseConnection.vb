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
    ''' Returns a SqlDataReader object.
    ''' </summary>
    ''' <param name="sqlQuery">Statement used to build the reader.</param>
    ''' <returns></returns>
    Public Shared Function GetDataReader(sqlQuery As String) As SqlDataReader
        Dim conn As New SqlConnection(_connectionString)
        conn.Open()

        Using cmd As SqlCommand = New SqlCommand(sqlQuery, conn)
            Return cmd.ExecuteReader()
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
