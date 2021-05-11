Imports MSSQLA.BusinessLogicLayer

Public Class ProcedureExecutionForm

    Private ReadOnly Property Query As String
    Private ReadOnly Property Database As String
    Private ReadOnly Property ProcedureName As String
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Public Property ParamsConfig As New Dictionary(Of Dictionary(Of String, SqlDbType), Object)

    Public Sub New(procedureName As String, database As String)
        InitializeComponent()
        paramsTable.RowCount = 0
        lblProcedureName.Text = procedureName
        Me.Text = procedureName
        Me.Database = database
        Me.ProcedureName = procedureName

        Me.Query = "SELECT 
                         name AS ParamName
                        ,type_name(user_type_id) AS ParamType
                        ,max_length AS ParamLength
                    FROM sys.parameters
                    WHERE object_id = object_id('" & procedureName & "')"
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        Dim dataTable As DataTable = DatabaseLogic.ExecuteSqlCode(Query, Database)(0)(0)

        paramsTable.Visible = dataTable.Rows.Count > 0
        lblNoParams.Visible = dataTable.Rows.Count = 0

        If dataTable.Rows.Count > 0 Then
            lblProcedureName.Dock = DockStyle.Top
            lblProcedureName.SendToBack()
        Else
            lblProcedureName.Dock = DockStyle.Fill
            lblProcedureName.BringToFront()
        End If

        For Each row As DataRow In dataTable.Rows
            AddNewRow(row(0).ToString(), row(1).ToString(), row(2))
        Next

        MyBase.OnLoad(e)
    End Sub

    Private Sub AddNewRow(paramName As String, paramType As String, paramLength As Integer)
        paramsTable.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        paramsTable.RowCount += 1

        paramsTable.Controls.Add(
            New Label() With
                        {
                            .Text = paramName & " " & If(paramLength = Nothing Or paramLength <= 0, paramType, paramType & "(" & paramLength & ")"),
                            .ForeColor = Color.Gainsboro,
                            .Dock = DockStyle.Fill,
                            .Font = New Font("Segoe UI", 9.75!, FontStyle.Bold, GraphicsUnit.Point, 0),
                            .TextAlign = ContentAlignment.MiddleLeft
                        },
            0,
            paramsTable.RowCount - 1)

        paramsTable.Controls.Add(
            New TextBox() With
                          {
                            .Name = paramName,
                            .Tag = paramType,
                            .ForeColor = Color.LightGray,
                            .BorderStyle = BorderStyle.FixedSingle,
                            .BackColor = Color.FromArgb(40, 44, 52),
                            .Dock = DockStyle.Fill,
                            .Font = New Font("Segoe UI", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0),
                            .WordWrap = True
                          },
            1,
            paramsTable.RowCount - 1)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dispose()
    End Sub

    Private Sub BtnExecuteProcedure_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        For Each textBox In paramsTable.Controls.OfType(Of TextBox).ToList()
            Dim paramOptions As New Dictionary(Of String, SqlDbType)
            Dim paramName As String = textBox.Name
            Dim paramType As SqlDbType

            Try
                paramType = [Enum].Parse(GetType(SqlDbType), textBox.Tag.ToString, True)
            Catch ex As Exception
                paramType = SqlDbType.NVarChar
            End Try

            paramOptions.Add(textBox.Name, paramType)
            ParamsConfig.Add(paramOptions, textBox.Text)
        Next

        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class