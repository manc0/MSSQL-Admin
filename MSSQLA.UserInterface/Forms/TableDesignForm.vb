Imports MSSQLA.BusinessLogicLayer

Public Class TableDesignForm

    Private Property Query As String
    Private ReadOnly Property Database As String
    Private ReadOnly Property TableName As String
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property Alter As Boolean

    Public Sub New(tableName As String, database As String, alter As Boolean)
        InitializeComponent()
        lblTableName.Text = tableName
        Me.Text = tableName
        Me.Database = database
        Me.TableName = tableName
        Me.Alter = alter

        Me.Query = "WITH CTE AS
                    (
                        SELECT 
                            COLUMN_NAME 
                        FROM 
                            INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                        WHERE (TABLE_SCHEMA + '.' + TABLE_NAME) = '" & tableName & "' 
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
                            ELSE
                                IC.DATA_TYPE
                        END AS TYPE
                        ,SC.IS_NULLABLE 
                    FROM INFORMATION_SCHEMA.COLUMNS IC
                    INNER JOIN SYS.COLUMNS SC
                        ON IC.COLUMN_NAME = SC.NAME
                    WHERE (TABLE_SCHEMA + '.' + TABLE_NAME) = '" & tableName & "'
                    AND SC.OBJECT_ID = OBJECT_ID('" & tableName & "')"


        Dim dataTable As DataTable = DatabaseLogic.ExecuteSqlCode(Query, database)(0)(0)
        DataGridView.AutoGenerateColumns = False

        For Each row As DataRow In dataTable.Rows
            DataGridView.Rows.Add(row.ItemArray)
        Next

    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
    End Sub


    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dispose()
    End Sub

    Private Sub BtnExecuteProcedure_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If Alter Then
            Me.Query = String.Empty
            Dim primaryKeyList As New List(Of String)()

            For i = 0 To DataGridView.Rows.Count - 2
                Dim row As DataGridViewRow = DataGridView.Rows(i)
                Dim isPrimaryKey As Boolean = row.Cells("PK_Column").Value
                Dim columnName As String = row.Cells("Name_Column").Value.ToString()
                Dim type As String = row.Cells("Type_Column").Value.ToString()
                Dim allowNulls As String = If(row.Cells("AllowNulls_Columns").Value = True, "NULL", "NOT NULL")

                Dim getColumnQuery As String = "SELECT 
                                                    COLUMN_NAME 
                                                FROM INFORMATION_SCHEMA.COLUMNS 
                                                WHERE (TABLE_SCHEMA + '.' + TABLE_NAME) = '" & TableName & "' AND COLUMN_NAME = '" & columnName & "'"
                Dim hasColumn As Boolean = (DatabaseLogic.GetScalar(getColumnQuery, Database) = columnName)

                If isPrimaryKey Then
                    primaryKeyList.Add(columnName)
                End If

                Try
                    Dim sqlQuery As String = "ALTER TABLE " & TableName & If(hasColumn, " ALTER COLUMN ", " ADD ") & columnName & " " & type & " " & allowNulls

                    DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Continue For
                End Try
            Next

            'If primaryKeyList.Count > 0 Then
            '    DatabaseLogic.ExecuteSqlCode("ALTER TABLE " & TableName & " DROP PRIMARY KEY", Database)
            '    DatabaseLogic.ExecuteSqlCode("ALTER TABLE " & TableName & " ADD PRIMARY KEY(" & Join(primaryKeyList.ToArray, ",") & ")", Database)
            'Else

            'End If
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub DataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView.RowsAdded
        DataGridView.Rows(e.RowIndex).Height = 28
    End Sub
End Class