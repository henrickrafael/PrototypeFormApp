Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FrmMain
    Private ReadOnly firstFileContent As New List(Of String)
    Private ReadOnly secondFileContent As New List(Of String)
    Private pathFile As String = GetPathFromFile()

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
    End Sub

    Private Shared Function ReturnFullPathFileName(path As String) As String
        Return IO.Path.Combine(path, String.Concat(Now.ToString("yyyyMMddHHmmss"), ".txt"))
    End Function

    Private Shared Function GetPathFromFile() As String
        Dim json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration\config.json"))
        Dim jsonObject = JObject.Parse(json)

        Return jsonObject.SelectToken("defaultPath").ToString()
    End Function

    Private Function ValidateListContent() As Boolean

        If Not firstFileContent.Any OrElse Not secondFileContent.Any Then
            MessageBox.Show("Um dos arquivos não foi selecionado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearGlobals()

            Return False
        End If

        Return True
    End Function

    Private Sub CreateTextFile(result As List(Of String))
        pathFile = ReturnFullPathFileName(pathFile)

        If Not File.Exists(pathFile) Then
            Using fileStream As FileStream = File.Create(pathFile)
            End Using
        End If

        Using sw As New StreamWriter(pathFile)

            For Each line In result
                sw.WriteLine(line)
            Next
        End Using

    End Sub

    Private Sub SaveConfigurationFile(selectedPath As String)

        If String.IsNullOrWhiteSpace(selectedPath) Then
            Return
        End If

        Dim configFile = New With {
            .defaultPath = selectedPath
        }

        Dim jsonString As String = JsonConvert.SerializeObject(configFile)

        Using sw As New StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration\config.json"))
            sw.WriteLine(jsonString)
        End Using

        pathFile = configFile.defaultPath
    End Sub

    Private Sub BtnRegerar_Click(sender As Object, e As EventArgs) Handles BtnRegerar.Click
        Dim numericInputValue As Integer = Integer.Parse(NumericInput.Value)
        Dim result As New List(Of String)
        Dim lineIndex As Integer

        Try
            If Not ValidateListContent() Then
                Return
            End If

            For Each searchString In secondFileContent

                For Each linha In firstFileContent

                    If linha.Contains(searchString) Then
                        lineIndex += 1

                        linha = Replace(linha, linha.Substring(3, 3), String.Format("{0:000}", lineIndex))
                        linha = Replace(linha, linha.Substring(25, 9), String.Format("{0:000000000}", numericInputValue))
                        result.Add(linha)
                    End If

                Next

            Next

            CreateTextFile(result)
            ClearGlobals()
            result.Clear()

            MessageBox.Show("Arquivo gerado com sucesso!", "Processo concluído", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabelOutput_Click(sender As Object, e As EventArgs) Handles LinkLabelOutput.Click
        Try
            Using fbd As New FolderBrowserDialog
                Dim result As DialogResult = fbd.ShowDialog()

                If result = DialogResult.OK AndAlso Not String.IsNullOrWhiteSpace(fbd.SelectedPath) Then
                    SaveConfigurationFile(fbd.SelectedPath)
                End If

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
