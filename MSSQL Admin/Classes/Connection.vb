Imports System.Data.SqlClient

Public Class Connection

    Private Sub New()
    End Sub

    Private Shared _instance As Connection
    Public Shared ReadOnly Property Instance() As Connection
        Get
            If _instance Is Nothing Then _instance = New Connection()
            Return _instance
        End Get
    End Property

    Private _conn As SqlConnection
    Public Property SqlConn() As SqlConnection
        Get
            Return _conn
        End Get
        Set(value As SqlConnection)
            _conn = value
        End Set
    End Property

    Public Sub Open(connectionString As String)
        SqlConn = New SqlConnection(connectionString)
        SqlConn.Open()
    End Sub

    Public Function IsOpen() As Boolean
        If IsNothing(SqlConn) Then Return False
        Return SqlConn.State = ConnectionState.Open
    End Function

    Public Sub Close()
        SqlConn?.Close()
    End Sub

End Class
