Imports System.IO

Public Class FrmMain
    Private ReadOnly firstFileContent As New List(Of String)
    Private ReadOnly secondFileContent As New List(Of String)
    Private ReadOnly maxLenghtValue As Integer = 9
    Private pathFile As String = "D:\"

    Private Shared Function GetFileContentToList(fileContent As List(Of String)) As Task
        Try
            Dim ofd As New OpenFileDialog With {
                .Filter = "Text files (*.txt) | *.txt"
            }

            ofd.ShowDialog()

            Using sr As New StreamReader(ofd.FileName)

                While Not sr.EndOfStream
                    fileContent.Add(sr.ReadLine.Trim())
                End While

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Task.CompletedTask
    End Function

    Private Async Sub SelectFile_Click(sender As Object, e As EventArgs) Handles SelectFile.Click
        Await GetFileContentToList(firstFileContent)
    End Sub

    Private Async Sub SecondFile_ClickAsync(sender As Object, e As EventArgs) Handles SecondFile.Click
        Await GetFileContentToList(secondFileContent)
    End Sub

    Private Sub ClearGlobals()
        firstFileContent.Clear()
        secondFileContent.Clear()
        pathFile = "D:\"
    End Sub

    Private Shared Function ReturnFullPathFileName(path As String) As String
        Return IO.Path.Combine(path, String.Concat(Now.ToString("yyyyMMddHHmmss"), ".txt"))
    End Function

    Private Sub BtnRegerar_Click(sender As Object, E As EventArgs) Handles BtnRegerar.Click
        pathFile = ReturnFullPathFileName(pathFile)

        Dim result As New List(Of String)
        Dim lineIndex As Integer

        Try

            If Not firstFileContent.Any OrElse Not secondFileContent.Any Then
                MessageBox.Show("Um dos arquivos não foi selecionado!", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)

                ClearGlobals()
                Return
            End If

            If NumericInput.Value.ToString.Length > maxLenghtValue Then
                MessageBox.Show($"O valor digitado ultrapassa o limite permitido de {maxLenghtValue} caracteres!", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)

                ClearGlobals()
                Return
            End If

            For Each item In secondFileContent

                For Each linha In firstFileContent

                    If linha.Contains(item) Then
                        lineIndex += 1

                        linha = Replace(linha, linha.Substring(3, 3), String.Format("{0:000}", lineIndex))
                        linha = Replace(linha, linha.Substring(25, 9), String.Format("{0:000000000}", NumericInput.Value))

                        result.Add(linha)
                    End If

                Next

            Next

            If Not File.Exists(pathFile) Then
                Using fileStream As FileStream = File.Create(pathFile)
                End Using
            End If

            Using sw As New StreamWriter(pathFile)

                For Each resultLine In result
                    sw.WriteLine(resultLine)
                Next

                result.Clear()
            End Using

            MessageBox.Show("Arquivo gerado com sucesso!", "Processo concluído", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ClearGlobals()
    End Sub

    Private Sub LinkLabelOutput_Click(sender As Object, e As EventArgs) Handles LinkLabelOutput.Click
        Try
            Using fbd As New FolderBrowserDialog
                Dim result As DialogResult = fbd.ShowDialog()

                If result = DialogResult.OK Then
                    pathFile = fbd.SelectedPath
                End If

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
