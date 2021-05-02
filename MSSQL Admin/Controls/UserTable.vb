Imports MSSQLA.BusinessLogicLayer

Public Class UserTable

    Public Property DataTable As DataTable
    Public Property CanBeUpdated As Boolean
    Public Property TableName As String
    Public Property DatabaseName As String

    Public Sub New(dataTable As DataTable, canBeUpdated As Boolean, tableName As String, databaseName As String)
        InitializeComponent()
        Me.DataTable = dataTable
        Me.CanBeUpdated = canBeUpdated
        Me.TableName = tableName
        Me.DatabaseName = databaseName
        DataGridView.DataSource = dataTable
        DataGridView.Refresh()
        DataGridView.ReadOnly = Not Me.CanBeUpdated
        DataGridView.AllowUserToDeleteRows = Me.CanBeUpdated
        DataGridView.AllowUserToAddRows = Me.CanBeUpdated
    End Sub

    Friend Sub UpdateTable()
        Dim logic As New DatabaseLogic
        DataTable = logic.GetTableFromDataBase(TableName, DatabaseName)
        DataGridView.DataSource = DataTable
        DataGridView.Refresh()
    End Sub

    Private Sub DataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView.DataError
        e.ThrowException = False
        DataGridView.Rows(e.RowIndex).ErrorText = e.Exception.Message
    End Sub

    Private Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellEndEdit
        DataGridView.Rows(e.RowIndex).ErrorText = String.Empty
    End Sub

End Class
