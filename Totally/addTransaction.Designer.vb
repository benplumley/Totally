<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class addTransaction
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
        Me.labThisTransWas = New System.Windows.Forms.Label()
        Me.labOfPounds = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.labPaidTo = New System.Windows.Forms.Label()
        Me.txtPayee = New System.Windows.Forms.TextBox()
        Me.labThisHappenedWhen = New System.Windows.Forms.Label()
        Me.comboDate = New System.Windows.Forms.ComboBox()
        Me.customDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.comboTime = New System.Windows.Forms.ComboBox()
        Me.txtCustomTimePicker = New System.Windows.Forms.TextBox()
        Me.comboTransType = New System.Windows.Forms.ComboBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.comboCategory = New System.Windows.Forms.ComboBox()
        Me.picAdd = New System.Windows.Forms.PictureBox()
        Me.fadeTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labThisTransWas
        '
        Me.labThisTransWas.AutoSize = True
        Me.labThisTransWas.Location = New System.Drawing.Point(13, 13)
        Me.labThisTransWas.Name = "labThisTransWas"
        Me.labThisTransWas.Size = New System.Drawing.Size(119, 13)
        Me.labThisTransWas.TabIndex = 0
        Me.labThisTransWas.Text = "This transaction was an"
        '
        'labOfPounds
        '
        Me.labOfPounds.AutoSize = True
        Me.labOfPounds.Location = New System.Drawing.Point(209, 13)
        Me.labOfPounds.Name = "labOfPounds"
        Me.labOfPounds.Size = New System.Drawing.Size(25, 13)
        Me.labOfPounds.TabIndex = 3
        Me.labOfPounds.Text = "of £"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmount.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtAmount.Location = New System.Drawing.Point(231, 10)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(50, 20)
        Me.txtAmount.TabIndex = 4
        Me.txtAmount.Text = "Amount"
        '
        'labPaidTo
        '
        Me.labPaidTo.AutoSize = True
        Me.labPaidTo.Location = New System.Drawing.Point(287, 13)
        Me.labPaidTo.Name = "labPaidTo"
        Me.labPaidTo.Size = New System.Drawing.Size(39, 13)
        Me.labPaidTo.TabIndex = 5
        Me.labPaidTo.Text = "paid to"
        '
        'txtPayee
        '
        Me.txtPayee.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtPayee.Location = New System.Drawing.Point(332, 10)
        Me.txtPayee.Name = "txtPayee"
        Me.txtPayee.Size = New System.Drawing.Size(123, 20)
        Me.txtPayee.TabIndex = 6
        Me.txtPayee.Text = "Name of Payee"
        '
        'labThisHappenedWhen
        '
        Me.labThisHappenedWhen.AutoSize = True
        Me.labThisHappenedWhen.Location = New System.Drawing.Point(455, 13)
        Me.labThisHappenedWhen.Name = "labThisHappenedWhen"
        Me.labThisHappenedWhen.Size = New System.Drawing.Size(84, 13)
        Me.labThisHappenedWhen.TabIndex = 7
        Me.labThisHappenedWhen.Text = ". This happened"
        '
        'comboDate
        '
        Me.comboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.comboDate.FormattingEnabled = True
        Me.comboDate.Items.AddRange(New Object() {"today", "yesterday", "two days ago", "three days ago", "four days ago", "five days ago", "six days ago", "one week ago", "pick a custom date..."})
        Me.comboDate.Location = New System.Drawing.Point(546, 10)
        Me.comboDate.Name = "comboDate"
        Me.comboDate.Size = New System.Drawing.Size(127, 21)
        Me.comboDate.TabIndex = 8
        '
        'customDatePicker
        '
        Me.customDatePicker.Location = New System.Drawing.Point(546, 10)
        Me.customDatePicker.Name = "customDatePicker"
        Me.customDatePicker.Size = New System.Drawing.Size(127, 20)
        Me.customDatePicker.TabIndex = 9
        Me.customDatePicker.Visible = False
        '
        'comboTime
        '
        Me.comboTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.comboTime.FormattingEnabled = True
        Me.comboTime.Items.AddRange(New Object() {"at the current time.", "in the morning.", "at midday.", "in the afternoon.", "in the evening.", "at midnight.", "pick a custom time..."})
        Me.comboTime.Location = New System.Drawing.Point(680, 10)
        Me.comboTime.Name = "comboTime"
        Me.comboTime.Size = New System.Drawing.Size(121, 21)
        Me.comboTime.TabIndex = 10
        '
        'txtCustomTimePicker
        '
        Me.txtCustomTimePicker.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtCustomTimePicker.Location = New System.Drawing.Point(680, 10)
        Me.txtCustomTimePicker.Name = "txtCustomTimePicker"
        Me.txtCustomTimePicker.Size = New System.Drawing.Size(121, 20)
        Me.txtCustomTimePicker.TabIndex = 11
        Me.txtCustomTimePicker.Text = "HH:MM (24hr)"
        Me.txtCustomTimePicker.Visible = False
        '
        'comboTransType
        '
        Me.comboTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboTransType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.comboTransType.FormattingEnabled = True
        Me.comboTransType.Items.AddRange(New Object() {"income", "expense"})
        Me.comboTransType.Location = New System.Drawing.Point(139, 10)
        Me.comboTransType.Name = "comboTransType"
        Me.comboTransType.Size = New System.Drawing.Size(64, 21)
        Me.comboTransType.TabIndex = 12
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtDescription.Location = New System.Drawing.Point(13, 37)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(660, 20)
        Me.txtDescription.TabIndex = 14
        Me.txtDescription.Text = "Enter a brief description of the transaction"
        '
        'comboCategory
        '
        Me.comboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.comboCategory.FormattingEnabled = True
        Me.comboCategory.Items.AddRange(New Object() {"Pick a category..."})
        Me.comboCategory.Location = New System.Drawing.Point(680, 37)
        Me.comboCategory.Name = "comboCategory"
        Me.comboCategory.Size = New System.Drawing.Size(121, 21)
        Me.comboCategory.TabIndex = 15
        '
        'picAdd
        '
        Me.picAdd.Image = Global.Totally.My.Resources.Resources.greyadd
        Me.picAdd.Location = New System.Drawing.Point(807, 10)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Size = New System.Drawing.Size(48, 48)
        Me.picAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picAdd.TabIndex = 16
        Me.picAdd.TabStop = False
        '
        'fadeTimer
        '
        Me.fadeTimer.Interval = 20
        '
        'addTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(868, 68)
        Me.Controls.Add(Me.picAdd)
        Me.Controls.Add(Me.comboCategory)
        Me.Controls.Add(Me.comboTransType)
        Me.Controls.Add(Me.txtCustomTimePicker)
        Me.Controls.Add(Me.comboTime)
        Me.Controls.Add(Me.customDatePicker)
        Me.Controls.Add(Me.comboDate)
        Me.Controls.Add(Me.labThisHappenedWhen)
        Me.Controls.Add(Me.txtPayee)
        Me.Controls.Add(Me.labPaidTo)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.labOfPounds)
        Me.Controls.Add(Me.labThisTransWas)
        Me.Controls.Add(Me.txtDescription)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "addTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add a Recent Transaction"
        CType(Me.picAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labThisTransWas As System.Windows.Forms.Label
    Friend WithEvents labOfPounds As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents labPaidTo As System.Windows.Forms.Label
    Friend WithEvents txtPayee As System.Windows.Forms.TextBox
    Friend WithEvents labThisHappenedWhen As System.Windows.Forms.Label
    Friend WithEvents comboDate As System.Windows.Forms.ComboBox
    Friend WithEvents customDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents comboTime As System.Windows.Forms.ComboBox
    Friend WithEvents txtCustomTimePicker As System.Windows.Forms.TextBox
    Friend WithEvents comboTransType As System.Windows.Forms.ComboBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents comboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents picAdd As System.Windows.Forms.PictureBox
    Friend WithEvents fadeTimer As System.Windows.Forms.Timer
End Class
