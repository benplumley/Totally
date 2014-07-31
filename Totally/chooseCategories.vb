Public Class chooseCategories
    Dim categoryName As String
    Dim invalidControls As New Collection
    Dim numberOfIterations As Integer

    Private Sub chooseCategories_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        For i = 0 To Form1.numberOfCategories - 1
            categoryName = Form1.category(i).name
            For Each categoryCheckBox As CheckBox In Me.Controls.OfType(Of CheckBox)()
                If Mid(categoryCheckBox.Name, 6) = categoryName Then
                    categoryCheckBox.Checked = True
                    For Each categoryTextBox As TextBox In Me.Controls.OfType(Of TextBox)()
                        categoryName = Form1.category(i).name
                        If Mid(categoryTextBox.Name, 4) = categoryName Then
                            categoryTextBox.ReadOnly = False
                            categoryTextBox.Text = Form1.category(i).totalAmount
                            Exit For
                        End If
                    Next
                End If
            Next
        Next
        matchTextBoxesToChecks()
        countCategories()
    End Sub

    Private Sub checkBoxes_CheckedChanged(sender As Object, e As EventArgs) Handles checkGroceries.CheckedChanged, checkBabysitting.CheckedChanged, checkBooks.CheckedChanged, checkClothes.CheckedChanged, checkEatingOut.CheckedChanged, checkFilm.CheckedChanged, checkFuel.CheckedChanged, checkGaming.CheckedChanged, checkHobby.CheckedChanged, checkMusic.CheckedChanged, checkParking.CheckedChanged, checkShoes.CheckedChanged, checkDrinks.CheckedChanged
        matchTextBoxesToChecks()
        countCategories()
    End Sub

    Sub matchTextBoxesToChecks()
        For Each categoryCheckBox As CheckBox In Me.Controls.OfType(Of CheckBox)()
            If categoryCheckBox.Checked = False Then
                categoryName = Mid(categoryCheckBox.Name, 6)
                For Each categoryTextBox As TextBox In Me.Controls.OfType(Of TextBox)()
                    If Mid(categoryTextBox.Name, 4) = categoryName Then
                        categoryTextBox.ReadOnly = True
                        Exit For
                    End If
                Next
            End If
            If categoryCheckBox.Checked = True Then
                categoryName = Mid(categoryCheckBox.Name, 6)
                For Each categoryTextBox As TextBox In Me.Controls.OfType(Of TextBox)()
                    If Mid(categoryTextBox.Name, 4) = categoryName Then
                        categoryTextBox.ReadOnly = False
                        Exit For
                    End If
                Next
            End If
        Next
        categoryName = ""
    End Sub

    Sub countCategories()
        Form1.numberOfCategories = 0
        For Each categoryCheckBox As CheckBox In Me.Controls.OfType(Of CheckBox)()
            If categoryCheckBox.Checked = True Then
                Form1.numberOfCategories += 1
            End If
        Next
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        invalidControls.Clear()
        numberOfIterations = 0
        Dim allFieldsValid As Boolean = True
        btnOK.Enabled = False
        btnOK.Text = "Saving..."
        btnOK.Refresh()
        Dim i As Integer = 0
        For Each categoryCheckBox As CheckBox In Me.Controls.OfType(Of CheckBox)()
            If categoryCheckBox.Checked = True Then
                categoryName = Mid(categoryCheckBox.Name, 6)
                For Each categoryTextBox As TextBox In Me.Controls.OfType(Of TextBox)()
                    If Mid(categoryTextBox.Name, 4) = categoryName Then
                        With Form1.category(i)
                            If System.Text.RegularExpressions.Regex.IsMatch(categoryTextBox.Text, "^\d+(?:\.\d{2})?$") Then 'Validates for a number with 0 or 2 decimal places
                                .name = categoryName
                                .text = categoryCheckBox.Text
                                .totalAmount = CSng(categoryTextBox.Text)
                            Else
                                categoryTextBox.BackColor = Color.Red
                                allFieldsValid = False
                                invalidControls.Add(categoryTextBox)
                            End If

                        End With
                        i += 1
                    End If
                Next
            End If
        Next
        If allFieldsValid = True Then
            Form1.updateCategoryDatabase()
            Me.Close()
            Form1.redrawAllGraphics()
        Else
            fadeTimer.Start()
            btnOK.Enabled = True
            btnOK.Text = "OK"
            btnOK.Refresh()
        End If
    End Sub


    Private Sub chooseCategories_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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