<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class editSecondLayer
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
        Me.txtNameOfItem = New System.Windows.Forms.TextBox()
        Me.labImSavingFor = New System.Windows.Forms.Label()
        Me.labINeedToSave = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.comboByDateOrNot = New System.Windows.Forms.ComboBox()
        Me.endDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.picAdd = New System.Windows.Forms.PictureBox()
        Me.fadeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.labAlreadySaved = New System.Windows.Forms.Label()
        Me.txtCurrentAmount = New System.Windows.Forms.TextBox()
        Me.labInTotal = New System.Windows.Forms.Label()
        Me.btnBoughtThis = New System.Windows.Forms.Button()
        Me.btnNeedThisMoney = New System.Windows.Forms.Button()
        Me.btnMoneySpent = New System.Windows.Forms.Button()
        CType(Me.picAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtNameOfItem
        '
        Me.txtNameOfItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtNameOfItem.Location = New System.Drawing.Point(93, 6)
        Me.txtNameOfItem.Name = "txtNameOfItem"
        Me.txtNameOfItem.Size = New System.Drawing.Size(189, 20)
        Me.txtNameOfItem.TabIndex = 1
        Me.txtNameOfItem.Text = "Name of Item"
        '
        'labImSavingFor
        '
        Me.labImSavingFor.AutoSize = True
        Me.labImSavingFor.Location = New System.Drawing.Point(9, 9)
        Me.labImSavingFor.Name = "labImSavingFor"
        Me.labImSavingFor.Size = New System.Drawing.Size(78, 13)
        Me.labImSavingFor.TabIndex = 2
        Me.labImSavingFor.Text = "I'm saving for a"
        '
        'labINeedToSave
        '
        Me.labINeedToSave.AutoSize = True
        Me.labINeedToSave.Location = New System.Drawing.Point(450, 9)
        Me.labINeedToSave.Name = "labINeedToSave"
        Me.labINeedToSave.Size = New System.Drawing.Size(105, 13)
        Me.labINeedToSave.TabIndex = 4
        Me.labINeedToSave.Text = "and I need to save £"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtTotalAmount.Location = New System.Drawing.Point(553, 6)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(58, 20)
        Me.txtTotalAmount.TabIndex = 5
        Me.txtTotalAmount.Text = "Amount"
        '
        'comboByDateOrNot
        '
        Me.comboByDateOrNot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboByDateOrNot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.comboByDateOrNot.FormattingEnabled = True
        Me.comboByDateOrNot.Items.AddRange(New Object() {"This is a second layer item which I need by:", "This is a third layer item."})
        Me.comboByDateOrNot.Location = New System.Drawing.Point(12, 32)
        Me.comboByDateOrNot.Name = "comboByDateOrNot"
        Me.comboByDateOrNot.Size = New System.Drawing.Size(236, 21)
        Me.comboByDateOrNot.TabIndex = 6
        '
        'endDatePicker
        '
        Me.endDatePicker.Location = New System.Drawing.Point(254, 33)
        Me.endDatePicker.Name = "endDatePicker"
        Me.endDatePicker.Size = New System.Drawing.Size(126, 20)
        Me.endDatePicker.TabIndex = 7
        Me.endDatePicker.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtDescription.Location = New System.Drawing.Point(386, 33)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(272, 20)
        Me.txtDescription.TabIndex = 8
        Me.txtDescription.Text = "Enter a brief description of the item"
        '
        'picAdd
        '
        Me.picAdd.Image = Global.Totally.My.Resources.Resources.greyadd
        Me.picAdd.Location = New System.Drawing.Point(664, 5)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Size = New System.Drawing.Size(48, 48)
        Me.picAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picAdd.TabIndex = 17
        Me.picAdd.TabStop = False
        '
        'fadeTimer
        '
        Me.fadeTimer.Interval = 20
        '
        'labAlreadySaved
        '
        Me.labAlreadySaved.AutoSize = True
        Me.labAlreadySaved.Location = New System.Drawing.Point(283, 9)
        Me.labAlreadySaved.Name = "labAlreadySaved"
        Me.labAlreadySaved.Size = New System.Drawing.Size(108, 13)
        Me.labAlreadySaved.TabIndex = 18
        Me.labAlreadySaved.Text = ". I've already saved £"
        '
        'txtCurrentAmount
        '
        Me.txtCurrentAmount.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtCurrentAmount.Location = New System.Drawing.Point(388, 6)
        Me.txtCurrentAmount.Name = "txtCurrentAmount"
        Me.txtCurrentAmount.Size = New System.Drawing.Size(58, 20)
        Me.txtCurrentAmount.TabIndex = 19
        Me.txtCurrentAmount.Text = "Amount"
        '
        'labInTotal
        '
        Me.labInTotal.AutoSize = True
        Me.labInTotal.Location = New System.Drawing.Point(617, 9)
        Me.labInTotal.Name = "labInTotal"
        Me.labInTotal.Size = New System.Drawing.Size(41, 13)
        Me.labInTotal.TabIndex = 20
        Me.labInTotal.Text = "in total."
        '
        'btnBoughtThis
        '
        Me.btnBoughtThis.Enabled = False
        Me.btnBoughtThis.Location = New System.Drawing.Point(249, 59)
        Me.btnBoughtThis.Name = "btnBoughtThis"
        Me.btnBoughtThis.Size = New System.Drawing.Size(227, 23)
        Me.btnBoughtThis.TabIndex = 21
        Me.btnBoughtThis.Text = "I've bought this item now."
        Me.btnBoughtThis.UseVisualStyleBackColor = True
        '
        'btnNeedThisMoney
        '
        Me.btnNeedThisMoney.Enabled = False
        Me.btnNeedThisMoney.Location = New System.Drawing.Point(12, 59)
        Me.btnNeedThisMoney.Name = "btnNeedThisMoney"
        Me.btnNeedThisMoney.Size = New System.Drawing.Size(227, 23)
        Me.btnNeedThisMoney.TabIndex = 22
        Me.btnNeedThisMoney.Text = "I need this money for first layer items."
        Me.btnNeedThisMoney.UseVisualStyleBackColor = True
        '
        'btnMoneySpent
        '
        Me.btnMoneySpent.Enabled = False
        Me.btnMoneySpent.Location = New System.Drawing.Point(485, 59)
        Me.btnMoneySpent.Name = "btnMoneySpent"
        Me.btnMoneySpent.Size = New System.Drawing.Size(227, 23)
        Me.btnMoneySpent.TabIndex = 23
        Me.btnMoneySpent.Text = "I've spent some of this money."
        Me.btnMoneySpent.UseVisualStyleBackColor = True
        '
        'editSecondLayer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 87)
        Me.Controls.Add(Me.btnMoneySpent)
        Me.Controls.Add(Me.btnNeedThisMoney)
        Me.Controls.Add(Me.btnBoughtThis)
        Me.Controls.Add(Me.labInTotal)
        Me.Controls.Add(Me.txtCurrentAmount)
        Me.Controls.Add(Me.labAlreadySaved)
        Me.Controls.Add(Me.picAdd)
        Me.Controls.Add(Me.endDatePicker)
        Me.Controls.Add(Me.comboByDateOrNot)
        Me.Controls.Add(Me.txtTotalAmount)
        Me.Controls.Add(Me.labINeedToSave)
        Me.Controls.Add(Me.labImSavingFor)
        Me.Controls.Add(Me.txtNameOfItem)
        Me.Controls.Add(Me.txtDescription)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "editSecondLayer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add a Second or Third Layer Item"
        CType(Me.picAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNameOfItem As System.Windows.Forms.TextBox
    Friend WithEvents labImSavingFor As System.Windows.Forms.Label
    Friend WithEvents labINeedToSave As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents comboByDateOrNot As System.Windows.Forms.ComboBox
    Friend WithEvents endDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents picAdd As System.Windows.Forms.PictureBox
    Friend WithEvents fadeTimer As System.Windows.Forms.Timer
    Friend WithEvents labAlreadySaved As System.Windows.Forms.Label
    Friend WithEvents txtCurrentAmount As System.Windows.Forms.TextBox
    Friend WithEvents labInTotal As System.Windows.Forms.Label
    Friend WithEvents btnBoughtThis As System.Windows.Forms.Button
    Friend WithEvents btnNeedThisMoney As System.Windows.Forms.Button
    Friend WithEvents btnMoneySpent As System.Windows.Forms.Button
End Class
