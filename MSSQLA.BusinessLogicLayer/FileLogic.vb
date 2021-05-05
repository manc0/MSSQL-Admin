Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Public Class FileLogic

    ''' <summary>
    ''' Exports the given DataGridView to CSV.
    ''' </summary>
    ''' <param name="dgv">DataGridView to be exported.</param>
    ''' <param name="path">Path where it's going to save the file.</param>
    Public Sub ToCsv(dgv As DataGridView, path As String)
        Dim columnCount As Integer = dgv.Columns.Count - 1
        Dim rowCount As Integer = dgv.Rows.Count - 1
        Dim columnNames As String = ""
        Dim outputCsv As New List(Of String)

        For c = 0 To columnCount
            columnNames += dgv.Columns(c).HeaderText.ToString() + ","
        Next
        outputCsv.Add(columnNames)

        For i = 0 To rowCount - 1
            Dim rowItems As String = ""

            For j = 0 To columnCount
                rowItems += dgv.Rows(i).Cells(j).Value.ToString() + ","
            Next

            outputCsv.Add(rowItems)
        Next

        File.WriteAllLines(path, outputCsv, Encoding.UTF8)
    End Sub

    ''' <summary>
    ''' Exports the given DataTable to XML.
    ''' </summary>
    ''' <param name="dataTable">DataTable to be exported.</param>
    ''' <param name="path">Path where it's going to save the file.</param>
    Public Sub ToXml(dataTable As DataTable, path As String)
        Dim sw = New StringWriter()
        dataTable.TableName = "Row"
        dataTable.WriteXml(sw, XmlWriteMode.IgnoreSchema)
        dataTable.TableName = ""

        File.WriteAllText(path, sw.ToString())
    End Sub

    ''' <summary>
    ''' Saves the given text as SQL.
    ''' </summary>
    ''' <param name="text">Text to write.</param>
    ''' <param name="path">Path where it's going to save the file.</param>
    Public Sub ToSql(text As String, path As String)
        Dim sw As New StreamWriter(path)
        sw.Write(text)
        sw.Close()
    End Sub
End Class
