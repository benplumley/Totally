Imports System.Data.SQLite
Imports SQLite

Public Class editSecondLayer

    Dim invalidControls As New Collection
    Dim numberOfIterations As Integer

    Private Sub editSecondLayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        comboByDateOrNot.SelectedItem = "This is a second layer item which I need by:"
        If Form1.secondLayerItemToEdit.id <> -1 Then
            If Form1.secondLayerBeingEdited = True Then
                txtCurrentAmount.Text = Form1.secondLayerItemToEdit.currentAmount
                txtDescription.Text = Form1.secondLayerItemToEdit.description
                txtNameOfItem.Text = Form1.secondLayerItemToEdit.nameOfItem
                txtTotalAmount.Text = Form1.secondLayerItemToEdit.totalAmount
                If Form1.secondLayerItemToEdit.endDate <> "null" Then
                    endDatePicker.Value = Form1.secondLayerItemToEdit.endDate
                    comboByDateOrNot.SelectedItem = "This is a second layer item which I need by:"
                    btnBoughtThis.Enabled = True
                Else
                    comboByDateOrNot.SelectedItem = "This is a third layer item."
                    btnMoneySpent.Enabled = True
                End If

                txtCurrentAmount.ForeColor = Color.Black
                txtTotalAmount.ForeColor = Color.Black
                txtNameOfItem.ForeColor = Color.Black
                txtDescription.ForeColor = Color.Black
                'btnBoughtThis.Enabled = True
                btnNeedThisMoney.Enabled = True
            End If
        End If


    End Sub

#Region "Name of Item"
    Private Sub txtNameOfItem_Enter(sender As Object, e As EventArgs) Handles txtNameOfItem.Enter
        If txtNameOfItem.Text = "Name of Item" Then
            txtNameOfItem.Text = ""
            txtNameOfItem.ForeColor = Color.Black
        End If
        txtNameOfItem.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub txtNameOfItem_Leave(sender As Object, e As EventArgs) Handles txtNameOfItem.Leave
        If txtNameOfItem.Text = "" Then
            txtNameOfItem.Text = "Name of Item"
            txtNameOfItem.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub

    Private Sub txtNameOfItem_TextChanged(sender As Object, e As EventArgs) Handles txtNameOfItem.TextChanged
        Dim firstLetter As String = LCase(Mid(txtNameOfItem.Text, 1, 1)).ToString
        If System.Text.RegularExpressions.Regex.IsMatch(firstLetter, "a|e|i|o|u") Then
            labImSavingFor.Text = "I'm saving for an"
        Else
            labImSavingFor.Text = "I'm saving for a"
        End If
    End Sub
#End Region

#Region "Total Amount"
    Private Sub txtTotalAmount_Enter(sender As Object, e As EventArgs) Handles txtTotalAmount.Enter
        If txtTotalAmount.Text = "Amount" Then
            txtTotalAmount.Text = ""
            txtTotalAmount.ForeColor = Color.Black
        End If
        txtTotalAmount.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub txtTotalAmount_Leave(sender As Object, e As EventArgs) Handles txtTotalAmount.Leave
        If txtTotalAmount.Text = "" Then
            txtTotalAmount.Text = "Amount"
            txtTotalAmount.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

#Region "Current Amount"
    Private Sub txtCurrentAmount_Enter(sender As Object, e As EventArgs) Handles txtCurrentAmount.Enter
        If txtCurrentAmount.Text = "Amount" Then
            txtCurrentAmount.Text = ""
            txtCurrentAmount.ForeColor = Color.Black
        End If
        txtCurrentAmount.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub txtCurrentAmount_Leave(sender As Object, e As EventArgs) Handles txtCurrentAmount.Leave
        If txtCurrentAmount.Text = "" Then
            txtCurrentAmount.Text = "Amount"
            txtCurrentAmount.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

    Private Sub comboByDateOrNot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboByDateOrNot.SelectedIndexChanged
        If comboByDateOrNot.SelectedItem = "This is a second layer item which I need by:" Then
            endDatePicker.Visible = True
            txtDescription.Left = 386
            txtDescription.Width = 272
            btnBoughtThis.Enabled = True
            btnMoneySpent.Enabled = False
        Else
            endDatePicker.Visible = False
            txtDescription.Left = 254
            txtDescription.Width = 404
            btnBoughtThis.Enabled = False
            btnMoneySpent.Enabled = True
        End If

    End Sub

#Region "Description"
    Private Sub txtDescription_Enter(sender As Object, e As EventArgs) Handles txtDescription.Enter
        If txtDescription.Text = "Enter a brief description of the item" Then
            txtDescription.Text = ""
            txtDescription.ForeColor = Color.Black
        End If
        txtNameOfItem.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub txtDescription_Leave(sender As Object, e As EventArgs) Handles txtDescription.Leave
        If txtDescription.Text = "" Then
            txtDescription.Text = "Enter a brief description of the item"
            txtDescription.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub
#End Region

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

        If Not System.Text.RegularExpressions.Regex.IsMatch(txtTotalAmount.Text, "^\d+(?:\.\d{2})?$") Then
            txtTotalAmount.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtTotalAmount)
        End If

        If Not System.Text.RegularExpressions.Regex.IsMatch(txtCurrentAmount.Text, "^\d+(?:\.\d{2})?$") Then
            txtCurrentAmount.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtCurrentAmount)
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(txtTotalAmount.Text, "^\d+(?:\.\d{2})?$") And System.Text.RegularExpressions.Regex.IsMatch(txtCurrentAmount.Text, "^\d+(?:\.\d{2})?$") Then 'If total and current amounts are valid
            If CSng(txtCurrentAmount.Text) > CSng(txtTotalAmount.Text) Then 'but current amount is bigger than total amount
                txtCurrentAmount.BackColor = Color.Red
                allFieldsValid = False
                invalidControls.Add(txtCurrentAmount)
            End If
        End If

        If txtNameOfItem.Text = "Name of Item" Or txtNameOfItem.Text = "" Or txtNameOfItem.Text.Contains("'") Then
            txtNameOfItem.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtNameOfItem)
        End If

        If txtDescription.Text = "Enter a brief description of the item" Or txtDescription.Text = "" Or txtDescription.Text.Contains("'") Then
            txtDescription.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtDescription)
        End If

        If allFieldsValid = True Then
            Dim tempRecord As Form1.secondLayerItemHolder
            If comboByDateOrNot.SelectedItem = "This is a second layer item which I need by:" Then
                tempRecord.endDate = endDatePicker.Value.Date
            Else
                tempRecord.endDate = "null"
            End If

            tempRecord.id = Form1.getNumberInSecondLayer("") + 1
            tempRecord.totalAmount = txtTotalAmount.Text
            tempRecord.currentAmount = txtCurrentAmount.Text
            tempRecord.nameOfItem = txtNameOfItem.Text
            tempRecord.description = txtDescription.Text
            Dim test As String = "INSERT INTO secondlayer(id, totalamount, currentamount, nameofitem, description, enddate) values(" & tempRecord.id & ", " & tempRecord.totalAmount & ", 0, '" & tempRecord.nameOfItem & "', '" & tempRecord.description & "', '" & tempRecord.endDate & "')"
            Me.Hide()
            If Form1.secondLayerBeingEdited Then
                updateItemInDatabase(tempRecord)
            Else
                addItemToDatabase(tempRecord)
            End If
            Form1.secondLayerBeingEdited = False
            Form1.secondLayerItemToEdit.id = -1
            'addItemToDatabase(tempRecord)
            Form1.loadSecondLayerDatabase()
            Form1.redrawAllGraphics()
            Me.Close()
        Else
            fadeTimer.Start()
        End If

    End Sub


#End Region

#Region "SQL"

    Sub addItemToDatabase(tempRecord)
        Using conn As New SQLiteConnection("Data Source=" & Form1.databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                'cmd.CommandText = "INSERT INTO secondlayer(id, totalamount, currentamount, nameofitem, description, enddate) values(" & tempRecord.id & ", " & tempRecord.totalAmount & ", " & tempRecord.currentAmount & ", '" & tempRecord.nameofitem & "', '" & tempRecord.description & "', '" & tempRecord.endDate & "')"
                cmd.CommandText = "INSERT INTO secondlayer(totalamount, currentamount, nameofitem, description, enddate) values(" & tempRecord.totalAmount & ", " & tempRecord.currentAmount & ", '" & tempRecord.nameofitem & "', '" & tempRecord.description & "', '" & tempRecord.endDate & "')"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Sub updateItemInDatabase(tempRecord)
        Using conn As New SQLiteConnection("Data Source=" & Form1.databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "UPDATE secondlayer SET totalamount = " & tempRecord.totalAmount & ", currentamount = " & tempRecord.currentAmount & ", nameofitem = '" & tempRecord.nameOfItem & "', description = '" & tempRecord.description & "', enddate = '" & tempRecord.endDate & "' WHERE id = " & Form1.secondLayerItemToEdit.id
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

    Private Sub fadeTimer_Tick(sender As Object, e As EventArgs) Handles fadeTimer.Tick
        For Each controlToFade As Control In invalidControls
            controlToFade.BackColor = Color.FromArgb(255, (numberOfIterations / 10) * 255, (numberOfIterations / 10) * 255)
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

    Private Sub btnNeedThisMoney_Click(sender As Object, e As EventArgs) Handles btnNeedThisMoney.Click
        Dim amount As Single = Form1.secondLayerItemToEdit.currentAmount
        Using conn As New SQLiteConnection("Data Source=" & Form1.databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "INSERT INTO transactions(expense, amount, shop, description, dDate, tTime, layer) values(0, " & amount & ", '" & Form1.secondLayerItemToEdit.nameOfItem & " savings', 'Money previously kept in savings', '" & Now.Date & "', '" & Now.Hour.ToString & ":" & Now.Minute.ToString & "', '" & 1 & "')"
                cmd.ExecuteNonQuery()
            End Using
        End Using
        Dim tempRecord As Form1.secondLayerItemHolder
        If comboByDateOrNot.SelectedItem = "This is a second layer item which I need by:" Then
            tempRecord.endDate = endDatePicker.Value.Date
        Else
            tempRecord.endDate = "null"
        End If

        tempRecord.id = Form1.getNumberInSecondLayer("") + 1
        tempRecord.totalAmount = txtTotalAmount.Text
        tempRecord.currentAmount = 0
        tempRecord.nameOfItem = txtNameOfItem.Text
        tempRecord.description = txtDescription.Text
        updateItemInDatabase(tempRecord)
        Form1.secondLayerBeingEdited = False
        Form1.secondLayerItemToEdit.id = -1
        'addItemToDatabase(tempRecord)
        Form1.loadSecondLayerDatabase()
        Form1.redrawAllGraphics()
        Me.Close()
    End Sub

    Private Sub btnBoughtThis_Click(sender As Object, e As EventArgs) Handles btnBoughtThis.Click
        Using conn As New SQLiteConnection("Data Source=" & Form1.databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "UPDATE secondlayer SET totalamount=0, currentamount=0 WHERE id=" & Form1.secondLayerItemToEdit.id
                cmd.ExecuteNonQuery()
            End Using
        End Using
        Form1.secondLayerBeingEdited = False
        Form1.secondLayerItemToEdit.id = -1
        Form1.loadSecondLayerDatabase()
        Form1.redrawAllGraphics()
        Me.Close()
    End Sub

    Private Sub btnMoneySpent_Click(sender As Object, e As EventArgs) Handles btnMoneySpent.Click
        thirdLayerHowMuchSpent.Show()
    End Sub
End Class