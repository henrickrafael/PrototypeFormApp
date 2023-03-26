Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FrmMain
    Private _pathFile As String = GetPathFromFile()
    Private ReadOnly _firstFileContent As New List(Of String)
    Private ReadOnly _secondFileContent As New List(Of String)

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
        Await GetFileContentToList(_firstFileContent)
    End Sub

    Private Async Sub SecondFile_ClickAsync(sender As Object, e As EventArgs) Handles SecondFile.Click
        Await GetFileContentToList(_secondFileContent)
    End Sub

    Private Sub ClearGlobals()
        _firstFileContent.Clear()
        _secondFileContent.Clear()
    End Sub

    Private Shared Function ReturnFullPathFileName(path As String) As String
        Return IO.Path.Combine(path, String.Concat(Now.ToString("yyyyMMddHHmmss"), ".txt"))
    End Function

    Private Shared Function GetPathFromFile() As String
        Dim isFirstConfigFileCreated As Boolean = CreateFileIfNotExists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"))

        If isFirstConfigFileCreated Then
            SaveConfigurationFile(String.Empty, True)
        End If

        Dim json As String = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"))
        Dim jsonObject As JObject = JObject.Parse(json)

        Return jsonObject.SelectToken("defaultPath").ToString()
    End Function

    Private Function ValidateListContent() As Boolean
        Dim isValid As Boolean = _firstFileContent.Any AndAlso _secondFileContent.Any

        If Not isValid Then
            MessageBox.Show("Um dos arquivos não foi selecionado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ClearGlobals()

            Return False
        End If

        Return isValid
    End Function

    Private Sub WriteTextFile(result As List(Of String))
        Dim pathFile As String = ReturnFullPathFileName(_pathFile)
        CreateFileIfNotExists(pathFile)

        Using sw As New StreamWriter(pathFile)
            For Each line In result
                sw.WriteLine(line)
            Next

        End Using

    End Sub

    Private Shared Function CreateFileIfNotExists(path As String) As Boolean
        If Not File.Exists(path) Then
            Using fileStream As FileStream = File.Create(path)
            End Using

            Return True
        End If

        Return False
    End Function

    Private Shared Function GenerateJsonConfigFile(obj As Object) As String
        Return JsonConvert.SerializeObject(obj)
    End Function

    Private Shared Sub SaveConfigurationFile(selectedPath As String, Optional isFirstBuild As Boolean = False)

        If String.IsNullOrWhiteSpace(selectedPath) AndAlso Not isFirstBuild Then
            Return
        End If

        Dim localPath As String = If(isFirstBuild, String.Empty, selectedPath)
        Dim jsonString As String = GenerateJsonConfigFile(New With {.defaultPath = localPath})

        Using sw As New StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"))
            sw.WriteLine(jsonString)
        End Using

    End Sub

    Private Sub BtnRegerar_Click(sender As Object, e As EventArgs) Handles BtnRegerar.Click

        If String.IsNullOrWhiteSpace(_pathFile) Then
            LinkLabelOutput_Click(Nothing, Nothing)
            _pathFile = GetPathFromFile()
        End If

        Dim numericInputValue As Integer = Integer.Parse(NumericInput.Value)
        Dim result As New List(Of String)
        Dim lineIndex As Integer

        Try
            If Not ValidateListContent() Then
                Return
            End If

            For Each searchString In _secondFileContent

                For Each linha In _firstFileContent

                    If linha.Contains(searchString) Then
                        lineIndex += 1

                        linha = Replace(linha, linha.Substring(3, 3), String.Format("{0:000}", lineIndex))
                        linha = Replace(linha, linha.Substring(25, 9), String.Format("{0:000000000}", numericInputValue))
                        result.Add(linha)
                    End If

                Next

            Next

            WriteTextFile(result)
            ClearGlobals()

            result.Clear()
            MessageBox.Show("Arquivo gerado com sucesso!", "Processo concluído", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Sub LinkLabelOutput_Click(sender As Object, e As EventArgs) Handles LinkLabelOutput.Click
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
