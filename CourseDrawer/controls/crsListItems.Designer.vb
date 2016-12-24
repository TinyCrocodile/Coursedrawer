<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class crsListItems
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.FlowPanelCourses = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'FlowPanelCourses
        '
        Me.FlowPanelCourses.AutoScroll = True
        Me.FlowPanelCourses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FlowPanelCourses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowPanelCourses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowPanelCourses.Location = New System.Drawing.Point(0, 0)
        Me.FlowPanelCourses.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowPanelCourses.Name = "FlowPanelCourses"
        Me.FlowPanelCourses.Size = New System.Drawing.Size(248, 248)
        Me.FlowPanelCourses.TabIndex = 0
        Me.FlowPanelCourses.WrapContents = False
        '
        'crsListItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowPanelCourses)
        Me.Name = "crsListItems"
        Me.Size = New System.Drawing.Size(248, 248)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowPanelCourses As FlowLayoutPanel
End Class
