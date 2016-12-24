<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class crsListItem
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CheckBox_Visible = New System.Windows.Forms.CheckBox()
        Me.Label_Checkbox = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CheckBox_Visible
        '
        Me.CheckBox_Visible.AutoSize = True
        Me.CheckBox_Visible.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBox_Visible.Location = New System.Drawing.Point(0, 0)
        Me.CheckBox_Visible.Margin = New System.Windows.Forms.Padding(0)
        Me.CheckBox_Visible.Name = "CheckBox_Visible"
        Me.CheckBox_Visible.Size = New System.Drawing.Size(123, 14)
        Me.CheckBox_Visible.TabIndex = 0
        Me.CheckBox_Visible.UseVisualStyleBackColor = True
        '
        'Label_Checkbox
        '
        Me.Label_Checkbox.AutoSize = True
        Me.Label_Checkbox.Location = New System.Drawing.Point(21, 0)
        Me.Label_Checkbox.Margin = New System.Windows.Forms.Padding(0)
        Me.Label_Checkbox.Name = "Label_Checkbox"
        Me.Label_Checkbox.Size = New System.Drawing.Size(76, 13)
        Me.Label_Checkbox.TabIndex = 1
        Me.Label_Checkbox.Text = "CheckboxText"
        Me.Label_Checkbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'crsListItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Label_Checkbox)
        Me.Controls.Add(Me.CheckBox_Visible)
        Me.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.Name = "crsListItem"
        Me.Size = New System.Drawing.Size(123, 15)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CheckBox_Visible As CheckBox
    Friend WithEvents Label_Checkbox As Label
End Class
