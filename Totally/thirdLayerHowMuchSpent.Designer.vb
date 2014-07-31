<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class thirdLayerHowMuchSpent
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAmountSpent = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labAmount = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.fadeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "I've spent £"
        '
        'txtAmountSpent
        '
        Me.txtAmountSpent.Location = New System.Drawing.Point(74, 10)
        Me.txtAmountSpent.Name = "txtAmountSpent"
        Me.txtAmountSpent.Size = New System.Drawing.Size(84, 20)
        Me.txtAmountSpent.TabIndex = 1
        Me.txtAmountSpent.Text = "Amount"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(165, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "of the available £"
        '
        'labAmount
        '
        Me.labAmount.AutoSize = True
        Me.labAmount.Location = New System.Drawing.Point(250, 13)
        Me.labAmount.Name = "labAmount"
        Me.labAmount.Size = New System.Drawing.Size(42, 13)
        Me.labAmount.TabIndex = 3
        Me.labAmount.Text = "amount"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(298, 8)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(47, 23)
        Me.btnSubmit.TabIndex = 4
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'fadeTimer
        '
        Me.fadeTimer.Interval = 20
        '
        'thirdLayerHowMuchSpent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(356, 34)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.labAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtAmountSpent)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "thirdLayerHowMuchSpent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAmountSpent As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labAmount As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents fadeTimer As System.Windows.Forms.Timer
End Class
