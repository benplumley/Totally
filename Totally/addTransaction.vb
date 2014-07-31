Imports SQLite
Imports System.Data.SQLite

Public Class addTransaction

    Dim invalidControls As New Collection
    Dim numberOfIterations As Integer

    Private Sub addTransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        comboTransType.SelectedItem = "expense"
        comboDate.SelectedItem = "today"
        comboTime.SelectedItem = "at the current time."
        comboCategory.SelectedItem = "Pick a category..."
        For i = 0 To Form1.numberOfCategories - 1
            comboCategory.Items.Add(Form1.category(i).name) 'Adds all selected categories to the combo box
        Next
    End Sub

#Region "Amount"
    Private Sub txtAmount_Enter(sender As Object, e As EventArgs) Handles txtAmount.Enter
        If txtAmount.Text = "Amount" Then
            txtAmount.Text = ""
            txtAmount.ForeColor = Color.Black
        End If
        txtAmount.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub txtAmount_Leave(sender As Object, e As EventArgs) Handles txtAmount.Leave
        If txtAmount.Text = "" Then
            txtAmount.Text = "Amount"
            txtAmount.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

#Region "Payee"
    Private Sub txtPayee_Enter(sender As Object, e As EventArgs) Handles txtPayee.Enter
        If txtPayee.Text = "Name of Payee" Or txtPayee.Text = "Name of Payer" Then
            txtPayee.Text = ""
            txtPayee.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtPayee_Leave(sender As Object, e As EventArgs) Handles txtPayee.Leave
        If txtPayee.Text = "" Then
            If comboTransType.SelectedItem = "income" Then
                txtPayee.Text = "Name of Payer"
            Else
                txtPayee.Text = "Name of Payee"
            End If
            txtPayee.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

#Region "Description"
    Private Sub txtDescription_Enter(sender As Object, e As EventArgs) Handles txtDescription.Enter
        If txtDescription.Text = "Enter a brief description of the transaction" Then
            txtDescription.Text = ""
            txtDescription.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtDescription_Leave(sender As Object, e As EventArgs) Handles txtDescription.Leave
        If txtDescription.Text = "" Then
            txtDescription.Text = "Enter a brief description of the transaction"
            txtDescription.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

#Region "Custom Time"
    Private Sub txtCustomTimePicker_Enter(sender As Object, e As EventArgs) Handles txtCustomTimePicker.Enter
        If txtCustomTimePicker.Text = "HH:MM (24hr)" Then
            txtCustomTimePicker.Text = ""
            txtCustomTimePicker.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtCustomTimePicker_Leave(sender As Object, e As EventArgs) Handles txtCustomTimePicker.Leave
        If txtCustomTimePicker.Text = "" Then
            txtCustomTimePicker.Text = "HH:MM (24hr)"
            txtCustomTimePicker.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

    Private Sub comboDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboDate.SelectedIndexChanged
        If comboDate.SelectedItem = "pick a custom date..." Then
            comboDate.Hide()
            customDatePicker.Show()
        End If
    End Sub

    Private Sub comboTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboTime.SelectedIndexChanged
        If comboTime.SelectedItem = "pick a custom time..." Then
            comboTime.Hide()
            txtCustomTimePicker.Show()
        End If
    End Sub


    Private Sub comboTransType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboTransType.SelectedIndexChanged
        If comboTransType.SelectedItem = "income" Then
            labPaidTo.Text = "paid by"
            txtPayee.Text = "Name of Payer"
            comboCategory.Hide()
            txtDescription.Width = 788
        ElseIf comboTransType.SelectedItem = "expense" Then
            labPaidTo.Text = "paid to"
            txtPayee.Text = "Name of Payee"
            txtDescription.Width = 660
            comboCategory.Show()
        End If
    End Sub

#Region "Add"

    Private Sub picAdd_MouseEnter(sender As Object, e As EventArgs) Handles picAdd.MouseEnter
        picAdd.Image = My.Resources.add_hover
    End Sub

    Private Sub picAdd_MouseLeave(sender As Object, e As EventArgs) Handles picAdd.MouseLeave
        picAdd.Image = My.Resources.greyadd
    End Sub

    Private Sub picAdd_Click(sender As Object, e As EventArgs) Handles picAdd.Click


        Dim allFieldsValid As Boolean = True
        invalidControls.Clear()
        numberOfIterations = 0

        If Not System.Text.RegularExpressions.Regex.IsMatch(txtAmount.Text, "^\d+(?:\.\d{2})?$") Then 'Validates for a number with 0 or 2 decimal places
            txtAmount.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtAmount)
        End If

        If txtPayee.Text = "Name of Payee" Or txtPayee.Text = "Name of Payer" Or txtPayee.Text = "" Or txtPayee.Text.Contains("'") Then
            txtPayee.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtPayee)
        End If

        If txtDescription.Text = "Enter a brief description of the transaction" Or txtDescription.Text = "" Or txtDescription.Text.Contains("'") Then
            txtDescription.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtDescription)
        End If

        If comboCategory.Visible = True And comboCategory.SelectedItem = "Pick a category..." Then
            comboCategory.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(comboCategory)
        End If

        If txtCustomTimePicker.Visible = True And Not System.Text.RegularExpressions.Regex.IsMatch(txtCustomTimePicker.Text, "^([0-1][0-9]|[2][0-3]):([0-5][0-9])$") Then 'Validates for a 24 hour time
            txtCustomTimePicker.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtCustomTimePicker)
        End If


        If allFieldsValid = True Then
            Dim tempRecord As Form1.recordHolder
            getDateTime(tempRecord.dDate, tempRecord.time)

            tempRecord.id = Form1.numberOfRecords + 1
            tempRecord.expense = CInt(CBool(comboTransType.SelectedItem = "expense"))
            tempRecord.amount = txtAmount.Text
            tempRecord.shop = txtPayee.Text
            tempRecord.description = txtDescription.Text
            tempRecord.category = comboCategory.SelectedItem
            tempRecord.layer = 1
            Me.Hide()
            If tempRecord.expense = True Then
                addExpenditureToDatabase(tempRecord)
            Else
                addIncomeToDatabase(tempRecord)
            End If
            Me.Close()
        Else
            fadeTimer.Start()
        End If
    End Sub

    Sub addExpenditureToDatabase(tempRecord)
        Using conn As New SQLiteConnection("Data Source=" & Form1.databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "INSERT INTO transactions(expense, amount, shop, description, dDate, tTime, category, layer) values(" & 1 & ", " & tempRecord.amount & ", '" & tempRecord.shop & "', '" & tempRecord.description & "', '" & tempRecord.dDate & "', '" & tempRecord.time & "', '" & tempRecord.category & "', " & tempRecord.layer & ")"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Sub addIncomeToDatabase(tempRecord)
        Using conn As New SQLiteConnection("Data Source=" & Form1.databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "INSERT INTO transactions(expense, amount, shop, description, dDate, tTime) values(" & 0 & ", " & tempRecord.amount & ", '" & tempRecord.shop & "', '" & tempRecord.description & "', '" & tempRecord.dDate & "', '" & tempRecord.time & "')"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

    Sub getDateTime(ByRef dDate, ByRef time)
        Select Case comboDate.SelectedItem
            Case "today"
                dDate = System.DateTime.Today
            Case "yesterday"
                dDate = System.DateTime.Today.AddDays(-1)
            Case "two days ago"
                dDate = System.DateTime.Today.AddDays(-2)
            Case "three days ago"
                dDate = System.DateTime.Today.AddDays(-3)
            Case "four days ago"
                dDate = System.DateTime.Today.AddDays(-4)
            Case "five days ago"
                dDate = System.DateTime.Today.AddDays(-5)
            Case "six days ago"
                dDate = System.DateTime.Today.AddDays(-6)
            Case "one week ago"
                dDate = System.DateTime.Today.AddDays(-7)
            Case "pick a custom date..."
                dDate = customDatePicker.Value.Date
            Case Else
                MsgBox("Date Error")
        End Select

        Select Case comboTime.SelectedItem
            Case "at the current time."
                time = System.DateTime.Now.ToShortTimeString
            Case "in the morning."
                time = "08:00"
            Case "at midday."
                time = "12:00"
            Case "in the afternoon."
                time = "16:00"
            Case "in the evening."
                time = "20:00"
            Case "at midnight."
                time = "00:00"
            Case "pick a custom time..."
                time = txtCustomTimePicker.Text
            Case Else
                MsgBox("Time Error")
        End Select
    End Sub

    Private Sub fadeTimer_Tick(sender As Object, e As EventArgs) Handles fadeTimer.Tick
        For Each controlToFade As Control In invalidControls
            controlToFade.BackColor = Color.FromArgb(255, (numberOfIterations / 10) * 255, (numberOfIterations / 10) * 255) 'Sets the background colour of each invalid control to a shade of red.
            'The darkest shade is used when numberOfIterations is 0, and gets lighter each iteration, giving the appearance of a control turning red and then fading quickly back to white.
        Next

        If numberOfIterations < 9 Then
            fadeTimer.Start()
        Else
            For Each controlToFade As Control In invalidControls
                controlToFade.BackColor = System.Drawing.SystemColors.Window
            Next
            fadeTimer.Enabled = False
        End If
        numberOfIterations += 1
    End Sub

    Private Sub addTransaction_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.loadTransactionDatabase()
        Form1.redrawAllGraphics()
    End Sub


End Class