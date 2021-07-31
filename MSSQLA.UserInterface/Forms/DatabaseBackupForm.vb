Imports MSSQLA.BusinessLogicLayer

Public Class DatabaseBackupForm

    Private ReadOnly Property Database As String
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Public Property ErrorMessage As String
    Public Property Path As String

    Public Sub New(database As String)
        InitializeComponent()
        tbDatabase.Text = database
        btnBackup.Enabled = False
        btnBackup.IconColor = Color.Gray

        Me.Database = database
    End Sub

    Private Sub BtnChoosePath_Click(sender As Object, e As EventArgs) Handles btnChoosePath.Click
        Dim fbd As New FolderBrowserDialog()

        If fbd.ShowDialog() = DialogResult.OK Then
            tbPath.Text = fbd.SelectedPath
        End If
    End Sub

    Private Sub TbPath_TextChanged(sender As Object, e As EventArgs) Handles tbPath.TextChanged
        Path = tbPath.Text

        If Not String.IsNullOrEmpty(tbPath.Text) Then
            btnBackup.Enabled = True
            btnBackup.IconColor = Color.LightGray
        Else
            btnBackup.Enabled = False
            btnBackup.IconColor = Color.Gray
        End If
    End Sub

    Private Sub BtnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Dim sqlQuery As String = "BACKUP DATABASE " & Database & " TO DISK = '" & Path & "'"

        If chbDifferentialMode.Checked Then
            sqlQuery &= " WITH DIFFERENTIAL"
        End If

        Try
            DatabaseLogic.ExecuteSqlCode(sqlQuery, Database)
            DialogResult = DialogResult.OK
        Catch ex As Exception
            ErrorMessage = ex.Message
            DialogResult = DialogResult.Abort
        End Try

        Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub
End Class