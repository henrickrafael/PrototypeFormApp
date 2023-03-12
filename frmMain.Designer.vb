<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SelectFile = New System.Windows.Forms.Button()
        Me.SecondFile = New System.Windows.Forms.Button()
        Me.BtnRegerar = New System.Windows.Forms.Button()
        Me.NumericInput = New System.Windows.Forms.NumericUpDown()
        Me.LabelInputNumber = New System.Windows.Forms.Label()
        Me.LinkLabelOutput = New System.Windows.Forms.LinkLabel()
        CType(Me.NumericInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SelectFile
        '
        Me.SelectFile.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.SelectFile.Location = New System.Drawing.Point(48, 91)
        Me.SelectFile.Name = "SelectFile"
        Me.SelectFile.Size = New System.Drawing.Size(123, 22)
        Me.SelectFile.TabIndex = 0
        Me.SelectFile.Text = "Primeiro arquivo"
        Me.SelectFile.UseVisualStyleBackColor = True
        '
        'SecondFile
        '
        Me.SecondFile.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.SecondFile.Location = New System.Drawing.Point(48, 119)
        Me.SecondFile.Name = "SecondFile"
        Me.SecondFile.Size = New System.Drawing.Size(123, 22)
        Me.SecondFile.TabIndex = 1
        Me.SecondFile.Text = "Segundo arquivo"
        Me.SecondFile.UseVisualStyleBackColor = True
        '
        'BtnRegerar
        '
        Me.BtnRegerar.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.BtnRegerar.Location = New System.Drawing.Point(425, 291)
        Me.BtnRegerar.Name = "BtnRegerar"
        Me.BtnRegerar.Size = New System.Drawing.Size(103, 23)
        Me.BtnRegerar.TabIndex = 2
        Me.BtnRegerar.Text = "Regerar"
        Me.BtnRegerar.UseVisualStyleBackColor = True
        '
        'NumericInput
        '
        Me.NumericInput.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.NumericInput.Location = New System.Drawing.Point(48, 191)
        Me.NumericInput.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NumericInput.Name = "NumericInput"
        Me.NumericInput.Size = New System.Drawing.Size(114, 22)
        Me.NumericInput.TabIndex = 3
        '
        'LabelInputNumber
        '
        Me.LabelInputNumber.AutoSize = True
        Me.LabelInputNumber.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.LabelInputNumber.Location = New System.Drawing.Point(48, 175)
        Me.LabelInputNumber.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelInputNumber.Name = "LabelInputNumber"
        Me.LabelInputNumber.Size = New System.Drawing.Size(83, 13)
        Me.LabelInputNumber.TabIndex = 4
        Me.LabelInputNumber.Text = "Valor desejado"
        '
        'LinkLabelOutput
        '
        Me.LinkLabelOutput.AutoSize = True
        Me.LinkLabelOutput.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.LinkLabelOutput.Location = New System.Drawing.Point(48, 301)
        Me.LinkLabelOutput.Name = "LinkLabelOutput"
        Me.LinkLabelOutput.Size = New System.Drawing.Size(114, 13)
        Me.LinkLabelOutput.TabIndex = 5
        Me.LinkLabelOutput.TabStop = True
        Me.LinkLabelOutput.Text = "Alterar local de saída"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 332)
        Me.Controls.Add(Me.LinkLabelOutput)
        Me.Controls.Add(Me.LabelInputNumber)
        Me.Controls.Add(Me.NumericInput)
        Me.Controls.Add(Me.BtnRegerar)
        Me.Controls.Add(Me.SecondFile)
        Me.Controls.Add(Me.SelectFile)
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        CType(Me.NumericInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SelectFile As Button
    Friend WithEvents SecondFile As Button
    Friend WithEvents BtnRegerar As Button
    Friend WithEvents NumericInput As NumericUpDown
    Friend WithEvents LabelInputNumber As Label
    Friend WithEvents LinkLabelOutput As LinkLabel
End Class
