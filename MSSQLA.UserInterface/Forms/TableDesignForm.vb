Imports System.Data.SqlClient
Imports MSSQLA.BusinessLogicLayer

Public Class TableDesignForm

    Private ReadOnly Property Database As String
    Private Property TableName As String
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property Alter As Boolean
    Private ReadOnly Property PrimaryKeyName As String
    Private ReadOnly Property OriginalPrimaryKeyList As New List(Of String)
    Private ReadOnly Property RemovedColumns As New List(Of String)
    Public Property NewPrimaryKeyList As List(Of String)

    Private ReadOnly DictionaryOfQueries As New Dictionary(Of String, String) From
    {
        {
        "GetColumnDescriptionsQuery",
        "WITH CTE AS
        (
            SELECT 
                COLUMN_NAME 
            FROM 
                INFORMATION_SCHEMA.KEY_COLUMN_USAGE
            WHERE ('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']') = '{0}' 
            AND CONSTRAINT_NAME LIKE 'PK%'
        )

        SELECT 
                CASE 
                WHEN (SELECT COLUMN_NAME FROM CTE WHERE COLUMN_NAME = SC.NAME) = SC.NAME THEN
                    'True'
                ELSE
                    'False'
                END AS IS_PRIMARYKEY
            ,SC.NAME
            ,CASE 
                WHEN IC.CHARACTER_MAXIMUM_LENGTH > 0 THEN
                    IC.DATA_TYPE + '(' + CONVERT(VARCHAR, IC.CHARACTER_MAXIMUM_LENGTH) + ')'
                WHEN IC.CHARACTER_MAXIMUM_LENGTH = -1 THEN
                    IC.DATA_TYPE + '(max)'
                ELSE
                    IC.DATA_TYPE
            END AS TYPE
            ,SC.IS_NULLABLE 
        FROM INFORMATION_SCHEMA.COLUMNS IC
        INNER JOIN SYS.COLUMNS SC
            ON IC.COLUMN_NAME = SC.NAME
        WHERE ('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']') = '{0}'
        AND SC.OBJECT_ID = OBJECT_ID('{0}')"
        },
        {
        "GetPrimaryKeyName",
        "SELECT C.CONSTRAINT_NAME FROM  
	        INFORMATION_SCHEMA.TABLE_CONSTRAINTS C
        WHERE ('[' + C.TABLE_SCHEMA + '].[' + C.TABLE_NAME + ']') = '{0}'
        AND C.CONSTRAINT_TYPE='PRIMARY KEY'"
        },
        {
        "CheckIfHasColumn",
        "SELECT 
            COLUMN_NAME 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE ('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']') = '{0}' AND ('[' + COLUMN_NAME + ']') = '{1}'"
        }
    }

    Public Sub New(tableName As String, database As String, alter As Boolean)
        InitializeComponent()
        lblTableName.Text = tableName
        tbTableName.Visible = False
        btnOk.Enabled = True
        btnOk.IconColor = Color.White

        Me.Text = tableName
        Me.Database = database
        Me.TableName = tableName
        Me.Alter = alter

        Dim sqlQuery = String.Format(DictionaryOfQueries("GetColumnDescriptionsQuery"), tableName)

        Try
            Dim dataTable As DataTable = DatabaseLogic.ExecuteSqlCode(sqlQuery, database)(0)(0)
            DataGridView.AutoGenerateColumns = False

            For Each row As DataRow In dataTable.Rows
                DataGridView.Rows.Add(row.ItemArray)

                If row.Item(0) = True Then
                    OriginalPrimaryKeyList.Add("[" & row.Item(1) & "]")
                End If
            Next

            sqlQuery = String.Format(DictionaryOfQueries("GetPrimaryKeyName"), tableName)
            Me.PrimaryKeyName = DatabaseLogic.GetScalar(sqlQuery, database)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End Try
    End Sub

    Public Sub New(databaseName As String, alter As Boolean)
        InitializeComponent()
        lblTableName.Text = "Table Designer"
        tbTableName.Visible = True
        btnOk.Enabled = False
        btnOk.IconColor = Color.Gray

        Me.Database = databaseName
        Me.Alter = alter
    End Sub

    Private Function GetNewPrimaryKey() As List(Of String)
        Dim newPrimaryKeyList As New List(Of String)()

        For i = 0 To DataGridView.Rows.Count - 2
            Dim row As DataGridViewRow = DataGridView.Rows(i)
            Dim isPrimaryKey As Boolean = row.Cells("PK_Column").Value
            Dim columnName As String = "[" & row.Cells("Name_Column").Value.ToString() & "]"

            If isPrimaryKey Then
                newPrimaryKeyList.Add(columnName)
            End If
        Next

        Return newPrimaryKeyList
    End Function

    Private Sub CreateTable()
        Dim sqlQuery As String = "CREATE TABLE " & TableName & "("
        Dim HasColumns As Boolean

        For i = 0 To DataGridView.Rows.Count - 2
            Dim row As DataGridViewRow = DataGridView.Rows(i)
            Dim columnName As String = "[" & row.Cells("Name_Column").Value.ToString() & "]"
            Dim type As String = row.Cells("Type_Column").Value.ToString()
            Dim isNull As Boolean = row.Cells("AllowNulls_Columns").Value
            Dim allowNulls As String = If(isNull = True, "NULL", "NOT NULL")

            sqlQuery &= columnName & " " & type & " " & allowNulls & ","
            HasColumns = True
        Next

        sqlQuery &= ");"

        Try
            DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
        Catch ex As Exception
            If HasColumns Then
                MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("Cannot create a table without columns", "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Try
    End Sub

    Private Sub AlterOrAddColumns()
        Dim sqlQuery As String

        For i = 0 To DataGridView.Rows.Count - 2
            Dim row As DataGridViewRow = DataGridView.Rows(i)
            Dim columnName As String = "[" & row.Cells("Name_Column").Value.ToString() & "]"
            Dim type As String = row.Cells("Type_Column").Value.ToString()
            Dim isNull As Boolean = row.Cells("AllowNulls_Columns").Value
            Dim allowNulls As String = If(isNull = True, "NULL", "NOT NULL")

            sqlQuery = String.Format(DictionaryOfQueries("CheckIfHasColumn"), TableName, columnName)
            Dim hasColumn As Boolean = ("[" & DatabaseLogic.GetScalar(sqlQuery, Database) & "]" = columnName)

            Try
                sqlQuery = "ALTER TABLE " & TableName & If(hasColumn, " ALTER COLUMN ", " ADD ") & columnName & " " & type & " " & allowNulls
                DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Continue For
            End Try
        Next
    End Sub

    Private Sub DropColumns()
        Dim sqlQuery As String

        For Each columnName As String In RemovedColumns
            Try
                sqlQuery = String.Format(DictionaryOfQueries("CheckIfHasColumn"), TableName, columnName)
                Dim hasColumn As Boolean = ("[" & DatabaseLogic.GetScalar(sqlQuery, Database) & "]" = columnName)

                If hasColumn Then
                    sqlQuery = "ALTER TABLE " & TableName & " DROP COLUMN " & columnName

                    DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        Next
    End Sub

    Private Sub DropPrimaryKey()
        Dim sqlQuery As String

        If Not String.IsNullOrEmpty(PrimaryKeyName) Then
            Try
                sqlQuery = "ALTER TABLE " & TableName & " DROP CONSTRAINT " & PrimaryKeyName
                DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Private Sub AddNewPrimaryKey()
        Dim sqlQuery As String

        Try
            If NewPrimaryKeyList.Count > 0 Then
                sqlQuery = "ALTER TABLE " & TableName & " ADD CONSTRAINT PK_" & TableName.Replace(".", "_").Replace("]", "").Replace("[", "").Replace(" ", "_") & " PRIMARY KEY (" & Join(NewPrimaryKeyList.ToArray, ",") & ")"
                DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If Alter Then RestorePrimaryKey()
        End Try
    End Sub

    Private Sub RestorePrimaryKey()
        Dim sqlQuery As String

        Try
            sqlQuery = "ALTER TABLE " & TableName & " ADD CONSTRAINT " & PrimaryKeyName & " PRIMARY KEY (" & Join(OriginalPrimaryKeyList.ToArray, ",") & ")"
            DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
        Catch
            Return
        End Try
    End Sub

    Private Sub BtnExecuteProcedure_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim nullCells = (From row As DataGridViewRow In DataGridView.Rows
                         Where row.Index < DataGridView.Rows.Count - 1
                         From cell As DataGridViewCell In row.Cells
                         Where cell.Value Is Nothing And (cell.ColumnIndex = 1 Or cell.ColumnIndex = 2)
                         Select cell).ToList

        If nullCells.Count > 0 Then
            nullCells.ForEach(Sub(c)
                                  If c.ColumnIndex = 1 Then
                                      c.ErrorText = "Input a column name"
                                  ElseIf c.ColumnIndex = 2 Then
                                      c.ErrorText = "Input a type"
                                  End If
                              End Sub)
            Return
        End If

        ' Get the new primary key
        NewPrimaryKeyList = GetNewPrimaryKey()

        ' Check if the primary has been modified
        Dim hasNewPrimaryKey As Boolean
        If NewPrimaryKeyList.Count > OriginalPrimaryKeyList.Count Then
            hasNewPrimaryKey = NewPrimaryKeyList.Except(OriginalPrimaryKeyList).Any()
        Else
            hasNewPrimaryKey = OriginalPrimaryKeyList.Except(NewPrimaryKeyList).Any()
        End If

        If Alter Then
            ' Drop primary key if exist
            If hasNewPrimaryKey Then DropPrimaryKey()

            ' Alter or add columns
            AlterOrAddColumns()

            ' Drop columns if they exist
            DropColumns()

            ' Add new primary key
            If hasNewPrimaryKey Then AddNewPrimaryKey()
        Else
            ' Create the new table
            CreateTable()

            ' Add new primary key
            AddNewPrimaryKey()
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dispose()
    End Sub

    Private Sub DataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView.RowsAdded
        DataGridView.Rows(e.RowIndex).Height = 28
    End Sub

    Private Sub DataGridView_RowsRemoved(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridView.UserDeletingRow
        RemovedColumns.Add("[" & e.Row.Cells("Name_Column").Value.ToString() & "]")
    End Sub

    Private Sub DataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellValueChanged

    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
        If e.RowIndex = -1 Then Return
        Dim cell As DataGridViewCell = DataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Dim isChecked As Boolean

        If e.ColumnIndex = 0 Then
            isChecked = cell.EditedFormattedValue

            If isChecked Then
                DataGridView.Rows(e.RowIndex).Cells(3).Value = False
            End If

        ElseIf e.ColumnIndex = 3 Then
            isChecked = cell.EditedFormattedValue

            If isChecked Then
                DataGridView.Rows(e.RowIndex).Cells(0).Value = False
            End If
        End If
    End Sub

    Private Sub DataGridView_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView.CellValidating
        If e.RowIndex = -1 Then Return
        Dim cell As DataGridViewCell = DataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)

        If cell.IsInEditMode And (e.ColumnIndex = 1 Or e.ColumnIndex = 2) Then
            If String.IsNullOrEmpty(e.FormattedValue.ToString()) Then
                e.Cancel = True
            End If
        End If

        If e.RowIndex = -1 Or e.RowIndex = DataGridView.RowCount - 1 Then Return
        cell = DataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Dim cellValue = cell.Value
        Dim cellFormattedValue = e.FormattedValue

        If cell.ColumnIndex = 1 And cellValue IsNot Nothing And cellFormattedValue IsNot Nothing AndAlso Not cellValue.ToString() = cellFormattedValue.ToString() Then
            cellValue = "[" & cellValue.ToString() & "]"
            cellFormattedValue = "[" & cellFormattedValue.ToString() & "]"

            RemovedColumns.Add(cellValue)
            If RemovedColumns.Contains(cellFormattedValue) Then
                RemovedColumns.Remove(cellFormattedValue)
            End If
        End If
    End Sub

    Private Sub DataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellEndEdit
        If e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
            Dim cell As DataGridViewCell = DataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)

            If Not String.IsNullOrEmpty(cell.EditedFormattedValue.ToString()) Then
                DataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex).ErrorText = ""
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles tbTableName.TextChanged
        If Alter Then Return
        TableName = tbTableName.Text

        If Not tbTableName.Text = "Introduce a name..." And Not tbTableName.Text = String.Empty Then
            btnOk.IconColor = Color.White
            btnOk.Enabled = True
        Else
            btnOk.IconColor = Color.Gray
            btnOk.Enabled = False
        End If
    End Sub

    Private Sub TbTableName_Enter(sender As Object, e As EventArgs) Handles tbTableName.Enter
        If Alter Then Return

        If tbTableName.Text = "Introduce a name..." Then
            tbTableName.Text = String.Empty
            tbTableName.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub TbTableName_Leave(sender As Object, e As EventArgs) Handles tbTableName.Leave
        If Alter Then Return

        If String.IsNullOrEmpty(tbTableName.Text) Then
            tbTableName.Text = "Introduce a name..."
            tbTableName.ForeColor = Color.Gray
        End If
    End Sub

End Class