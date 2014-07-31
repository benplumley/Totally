<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.dgvRecords = New System.Windows.Forms.DataGridView()
        Me.moveEnd = New System.Windows.Forms.Timer(Me.components)
        Me.picEdit = New System.Windows.Forms.PictureBox()
        Me.picSecond = New System.Windows.Forms.PictureBox()
        Me.picCats = New System.Windows.Forms.PictureBox()
        Me.picAdd = New System.Windows.Forms.PictureBox()
        Me.dgvColAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvColShop = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvColDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvColDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvColCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.index = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSecond, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCats, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvRecords
        '
        Me.dgvRecords.AllowUserToAddRows = False
        Me.dgvRecords.AllowUserToDeleteRows = False
        Me.dgvRecords.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvRecords.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRecords.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvRecords.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRecords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvRecords.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvColAmount, Me.dgvColShop, Me.dgvColDescription, Me.dgvColDateTime, Me.dgvColCategory, Me.index})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRecords.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvRecords.Location = New System.Drawing.Point(0, 12)
        Me.dgvRecords.Name = "dgvRecords"
        Me.dgvRecords.RowHeadersVisible = False
        Me.dgvRecords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRecords.ShowEditingIcon = False
        Me.dgvRecords.Size = New System.Drawing.Size(1120, 590)
        Me.dgvRecords.TabIndex = 2
        '
        'moveEnd
        '
        Me.moveEnd.Interval = 10
        '
        'picEdit
        '
        Me.picEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picEdit.Image = Global.Totally.My.Resources.Resources.greyedit
        Me.picEdit.Location = New System.Drawing.Point(1004, 608)
        Me.picEdit.Name = "picEdit"
        Me.picEdit.Size = New System.Drawing.Size(24, 24)
        Me.picEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEdit.TabIndex = 17
        Me.picEdit.TabStop = False
        '
        'picSecond
        '
        Me.picSecond.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSecond.Image = Global.Totally.My.Resources.Resources.grey2and3
        Me.picSecond.Location = New System.Drawing.Point(1064, 608)
        Me.picSecond.Name = "picSecond"
        Me.picSecond.Size = New System.Drawing.Size(24, 24)
        Me.picSecond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSecond.TabIndex = 15
        Me.picSecond.TabStop = False
        '
        'picCats
        '
        Me.picCats.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picCats.Image = Global.Totally.My.Resources.Resources.grey_choose_categories
        Me.picCats.Location = New System.Drawing.Point(1094, 608)
        Me.picCats.Name = "picCats"
        Me.picCats.Size = New System.Drawing.Size(24, 24)
        Me.picCats.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCats.TabIndex = 14
        Me.picCats.TabStop = False
        '
        'picAdd
        '
        Me.picAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picAdd.Image = Global.Totally.My.Resources.Resources.greyadd
        Me.picAdd.Location = New System.Drawing.Point(1034, 608)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Size = New System.Drawing.Size(24, 24)
        Me.picAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAdd.TabIndex = 13
        Me.picAdd.TabStop = False
        '
        'dgvColAmount
        '
        Me.dgvColAmount.HeaderText = "Amount"
        Me.dgvColAmount.Name = "dgvColAmount"
        '
        'dgvColShop
        '
        Me.dgvColShop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.dgvColShop.HeaderText = "Shop"
        Me.dgvColShop.Name = "dgvColShop"
        Me.dgvColShop.Width = 57
        '
        'dgvColDescription
        '
        Me.dgvColDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgvColDescription.HeaderText = "Description"
        Me.dgvColDescription.Name = "dgvColDescription"
        '
        'dgvColDateTime
        '
        Me.dgvColDateTime.HeaderText = "Date and Time"
        Me.dgvColDateTime.Name = "dgvColDateTime"
        '
        'dgvColCategory
        '
        Me.dgvColCategory.HeaderText = "Category"
        Me.dgvColCategory.Name = "dgvColCategory"
        '
        'index
        '
        Me.index.HeaderText = "index"
        Me.index.Name = "index"
        Me.index.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1121, 634)
        Me.Controls.Add(Me.picEdit)
        Me.Controls.Add(Me.picSecond)
        Me.Controls.Add(Me.picCats)
        Me.Controls.Add(Me.picAdd)
        Me.Controls.Add(Me.dgvRecords)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Totally"
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSecond, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCats, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvRecords As System.Windows.Forms.DataGridView
    'Friend WithEvents BudgetDataSet As Totally.budgetDataSet
    Friend WithEvents CategoryBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents CategoryTableAdapter As Totally.budgetDataSetTableAdapters.categoryTableAdapter
    Friend WithEvents CategoryBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CategoryBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents picAdd As System.Windows.Forms.PictureBox
    Friend WithEvents picCats As System.Windows.Forms.PictureBox
    Friend WithEvents moveEnd As System.Windows.Forms.Timer
    Friend WithEvents picSecond As System.Windows.Forms.PictureBox
    Friend WithEvents picEdit As System.Windows.Forms.PictureBox
    Friend WithEvents dgvColAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvColShop As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvColDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvColDateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvColCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents index As System.Windows.Forms.DataGridViewTextBoxColumn


End Class
