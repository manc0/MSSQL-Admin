Imports MSSQLA.BusinessLogicLayer

Public Class TableDesignForm

    Private ReadOnly Property Database As String
    Private ReadOnly Property TableName As String
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property Alter As Boolean
    Private ReadOnly Property PrimaryKeyName As String
    Private ReadOnly Property OriginalPrimaryKeyList As New List(Of String)
    Private ReadOnly Property RemovedColumns As New List(Of String)

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
            WHERE (TABLE_SCHEMA + '.' + TABLE_NAME) = '{0}' 
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
        WHERE (TABLE_SCHEMA + '.' + TABLE_NAME) = '{0}'
        AND SC.OBJECT_ID = OBJECT_ID('{0}')"
        },
        {
        "GetPrimaryKeyName",
        "SELECT C.CONSTRAINT_NAME FROM  
	        INFORMATION_SCHEMA.TABLE_CONSTRAINTS C
        WHERE (C.TABLE_SCHEMA + '.' + C.TABLE_NAME) = '{0}'
        AND C.CONSTRAINT_TYPE='PRIMARY KEY'"
        },
        {
        "CheckIfHasColumn",
        "SELECT 
            COLUMN_NAME 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE (TABLE_SCHEMA + '.' + TABLE_NAME) = '{0}' AND COLUMN_NAME = '{1}'"
        }
    }

    Public Sub New(tableName As String, database As String, alter As Boolean)
        InitializeComponent()
        lblTableName.Text = tableName
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
                    OriginalPrimaryKeyList.Add(row.Item(1))
                End If
            Next

            sqlQuery = String.Format(DictionaryOfQueries("GetPrimaryKeyName"), tableName)
            Me.PrimaryKeyName = DatabaseLogic.GetScalar(sqlQuery, database)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End Try
    End Sub

    Private Sub BtnExecuteProcedure_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If Alter Then
            Dim newPrimaryKeyList As New List(Of String)()
            Dim sqlQuery As String = String.Empty

            ' Get the new primary key
            For i = 0 To DataGridView.Rows.Count - 2
                Dim row As DataGridViewRow = DataGridView.Rows(i)
                Dim isPrimaryKey As Boolean = row.Cells("PK_Column").Value
                Dim columnName As String = row.Cells("Name_Column").Value.ToString()

                If isPrimaryKey Then
                    newPrimaryKeyList.Add(columnName)
                End If
            Next

            ' Handle primary keys
            If Not String.IsNullOrEmpty(PrimaryKeyName) Then
                Try
                    sqlQuery = "ALTER TABLE " & TableName & " DROP CONSTRAINT " & PrimaryKeyName
                    DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)

                    If newPrimaryKeyList.Count > 0 Then
                        sqlQuery = "ALTER TABLE " & TableName & " ADD CONSTRAINT PK_" & TableName.Replace(".", "_") & " PRIMARY KEY (" & Join(newPrimaryKeyList.ToArray, ",") & ")"
                        DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                    End If
                Catch ex1 As Exception
                    ' If adding a new primary key, restore the previous one.
                    Try
                        sqlQuery = "ALTER TABLE " & TableName & " ADD CONSTRAINT " & PrimaryKeyName & " PRIMARY KEY (" & Join(OriginalPrimaryKeyList.ToArray, ",") & ")"
                        DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                    Catch ex2 As Exception
                        MessageBox.Show(ex1.Message + vbNewLine + ex2.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Try
            Else
                Try
                    If newPrimaryKeyList.Count > 0 Then
                        sqlQuery = "ALTER TABLE " & TableName & " ADD CONSTRAINT PK_" & TableName.Replace(".", "_") & " PRIMARY KEY (" & Join(newPrimaryKeyList.ToArray, ",") & ")"
                        DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            ' Alter or add columns
            For i = 0 To DataGridView.Rows.Count - 2
                Dim row As DataGridViewRow = DataGridView.Rows(i)
                Dim columnName As String = row.Cells("Name_Column").Value.ToString()
                Dim type As String = row.Cells("Type_Column").Value.ToString()
                Dim allowNulls As String = If(row.Cells("AllowNulls_Columns").Value = True, "NULL", "NOT NULL")

                sqlQuery = String.Format(DictionaryOfQueries("CheckIfHasColumn"), TableName, columnName)
                Dim hasColumn As Boolean = (DatabaseLogic.GetScalar(sqlQuery, Database) = columnName)

                Try
                    sqlQuery = "ALTER TABLE " & TableName & If(hasColumn, " ALTER COLUMN ", " ADD ") & columnName & " " & type & " " & allowNulls

                    DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Continue For
                End Try
            Next

            ' Drop columns if they exist
            For Each columnName As String In RemovedColumns
                Try
                    sqlQuery = String.Format(DictionaryOfQueries("CheckIfHasColumn"), TableName, columnName)
                    Dim hasColumn As Boolean = (DatabaseLogic.GetScalar(sqlQuery, Database) = columnName)

                    If hasColumn Then
                        sqlQuery = "ALTER TABLE " & TableName & " DROP COLUMN " & columnName

                        DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            Next


        Else

        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dispose()
    End Sub

    Private Sub DataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView.RowsAdded
        DataGridView.Rows(e.RowIndex).Height = 28
    End Sub

    Private Sub DataGridView_RowsRemoved(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridView.UserDeletingRow
        RemovedColumns.Add(e.Row.Cells("Name_Column").Value.ToString())
    End Sub

End Class