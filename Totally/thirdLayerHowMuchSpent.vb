Public Class thirdLayerHowMuchSpent

    Dim invalidControls As New Collection
    Dim numberOfIterations As Integer

    Private Sub thirdLayerHowMuchSpent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        labAmount.Text = editSecondLayer.txtCurrentAmount.Text
    End Sub

    Private Sub txtAmountSpent_Enter(sender As Object, e As EventArgs) Handles txtAmountSpent.Enter
        If txtAmountSpent.Text = "Amount" Then
            txtAmountSpent.Text = ""
            txtAmountSpent.ForeColor = Color.Black
        End If
        txtAmountSpent.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub txtAmountSpent_Leave(sender As Object, e As EventArgs) Handles txtAmountSpent.Leave
        If txtAmountSpent.Text = "" Then
            txtAmountSpent.Text = "Amount"
            txtAmountSpent.ForeColor = System.Drawing.SystemColors.GrayText
        End If
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim allFieldsValid As Boolean = True
        If Not System.Text.RegularExpressions.Regex.IsMatch(txtAmountSpent.Text, "^\d+(?:\.\d{2})?$") Then
            txtAmountSpent.BackColor = Color.Red
            allFieldsValid = False
            invalidControls.Add(txtAmountSpent)
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(txtAmountSpent.Text, "^\d+(?:\.\d{2})?$") Then 'If total and current amounts are valid
            If CSng(txtAmountSpent.Text) > CSng(labAmount.Text) Then 'but current amount is bigger than total amount
                txtAmountSpent.BackColor = Color.Red
                allFieldsValid = False
                invalidControls.Add(txtAmountSpent)
            End If
        End If

        If allFieldsValid = True Then
            Dim tempRecord As Form1.secondLayerItemHolder
            tempRecord.endDate = "null"
            tempRecord.id = Form1.getNumberInSecondLayer("") + 1
            tempRecord.totalAmount = editSecondLayer.txtTotalAmount.Text
            tempRecord.currentAmount = CSng(labAmount.Text) - CSng(txtAmountSpent.Text)
            tempRecord.nameOfItem = editSecondLayer.txtNameOfItem.Text
            tempRecord.description = editSecondLayer.txtDescription.Text
            editSecondLayer.updateItemInDatabase(tempRecord)
            Form1.secondLayerBeingEdited = False
            Form1.secondLayerItemToEdit.id = -1
            'addItemToDatabase(tempRecord)
            Form1.loadSecondLayerDatabase()
            Form1.redrawAllGraphics()
            editSecondLayer.Close()
            Me.Close()
        Else
            fadeTimer.Start()
        End If

    End Sub

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
End Class