Imports MSSQLA.BusinessLogicLayer

Public Class UserTable

    Friend Property DataTable As DataTable
    Friend Property CanBeUpdated As Boolean
    Friend Property TableName As String
    Friend Property DatabaseName As String
    Friend ReadOnly Property RowCount As String
        Get
            Return DataGridView.RowCount
        End Get
    End Property
    Friend ReadOnly Property Changes As DataTable
        Get
            Return DataTable.GetChanges()
        End Get
    End Property


#Region "Constructors"

    Friend Sub New(dataTable As DataTable, canBeUpdated As Boolean, tableName As String, databaseName As String)
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

#End Region

#Region "Friend Methods"

    Friend Sub UpdateTable()
        Dim logic As New DatabaseLogic
        DataTable = logic.GetTableFromDataBase(TableName, DatabaseName)
        DataGridView.DataSource = DataTable
        DataGridView.Refresh()
    End Sub

    Friend Sub EndEdit()
        DataTable.EndInit()
    End Sub

#End Region

#Region "Events"

    Friend Sub DataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView.DataError
        e.ThrowException = False
        DataGridView.Rows(e.RowIndex).ErrorText = e.Exception.Message
    End Sub

    Friend Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellEndEdit
        DataGridView.Rows(e.RowIndex).ErrorText = String.Empty
    End Sub

#End Region

End Class
