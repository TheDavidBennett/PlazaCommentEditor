<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SubmitButton = New System.Windows.Forms.Button()
        Me.textboxMessage = New System.Windows.Forms.TextBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblProfileUsername = New System.Windows.Forms.Label()
        Me.textboxProfileUsername = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'SubmitButton
        '
        Me.SubmitButton.Location = New System.Drawing.Point(12, 53)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(100, 23)
        Me.SubmitButton.TabIndex = 0
        Me.SubmitButton.Text = "Submit"
        Me.SubmitButton.UseVisualStyleBackColor = True
        '
        'textboxMessage
        '
        Me.textboxMessage.Location = New System.Drawing.Point(133, 25)
        Me.textboxMessage.MaxLength = 15000
        Me.textboxMessage.Multiline = True
        Me.textboxMessage.Name = "textboxMessage"
        Me.textboxMessage.Size = New System.Drawing.Size(139, 51)
        Me.textboxMessage.TabIndex = 5
        Me.textboxMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(130, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(53, 13)
        Me.lblMessage.TabIndex = 6
        Me.lblMessage.Text = "Message:"
        '
        'lblProfileUsername
        '
        Me.lblProfileUsername.AutoSize = True
        Me.lblProfileUsername.Location = New System.Drawing.Point(12, 9)
        Me.lblProfileUsername.Name = "lblProfileUsername"
        Me.lblProfileUsername.Size = New System.Drawing.Size(90, 13)
        Me.lblProfileUsername.TabIndex = 8
        Me.lblProfileUsername.Text = "Profile Username:"
        '
        'textboxProfileUsername
        '
        Me.textboxProfileUsername.Location = New System.Drawing.Point(12, 25)
        Me.textboxProfileUsername.MaxLength = 16
        Me.textboxProfileUsername.Name = "textboxProfileUsername"
        Me.textboxProfileUsername.Size = New System.Drawing.Size(100, 20)
        Me.textboxProfileUsername.TabIndex = 7
        Me.textboxProfileUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 85)
        Me.Controls.Add(Me.lblProfileUsername)
        Me.Controls.Add(Me.textboxProfileUsername)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.textboxMessage)
        Me.Controls.Add(Me.SubmitButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Main"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "3DSPlaza Comments Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SubmitButton As Button
    Friend WithEvents textboxMessage As TextBox
    Friend WithEvents lblMessage As Label
    Friend WithEvents lblProfileUsername As Label
    Friend WithEvents textboxProfileUsername As TextBox
End Class
