Imports System.IO
Imports SQLite
Imports System.Data.SQLite
Imports System.Windows.Input
Imports System.Environment

Public Class Form1

    Public databaseAddress As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Totally\Budget.db"

#Region "Structures"
    Public Structure recordHolder
        Dim id As Integer
        Dim expense As Boolean
        Dim dDate As String
        Dim time As String
        Dim category As String
        Dim shop As String
        Dim description As String
        Dim amount As Single
        Dim amountLeft As Single
        Dim balance As Single
        Dim layer As Integer
    End Structure

    Public Structure secondLayerItemHolder
        Dim id As Integer
        Dim endDate As String
        Dim nameOfItem As String
        Dim description As String
        Dim totalAmount As Double
        Dim currentAmount As Double
    End Structure

    Structure categoryHolder
        Dim text As String
        Dim name As String
        Dim totalAmount As Single
        Dim spentAmount As Single
    End Structure
#End Region

#Region "Constants"
    Const categoryBarLeft As Integer = 63
    Const categoryBarTop As Integer = 12
    Const categoryBarBetween As Integer = 20
    Dim categoryBarBlue As Color = System.Drawing.Color.FromArgb(255, 0, 74, 166) 'Hex value #004AA6
    Dim categoryBarDarkBlue As Color = System.Drawing.Color.FromArgb(255, 0, 48, 107) 'Hex value #00306B
    Dim categoryBarGrey As Color = System.Drawing.Color.FromArgb(255, 207, 207, 207) 'Hex value #CFCFCF

#End Region

#Region "Declarations"

    Public record(numberOfRecords) As recordHolder
    Public category(13) As categoryHolder
    Public secondLayerItem(100 + getNumberInSecondLayer("")) As secondLayerItemHolder
    Dim applicationPath As String = (Mid(Application.ExecutablePath, 1, (Application.ExecutablePath.Length - 11)))
    Public numberOfRecords As Integer = 200
    Public numberOfCategories As Integer = getNumberOfCategoriesSelected()
    Dim numberInSecondLayer = getNumberInSecondLayer("")
    Dim dateOfLastReset As Date
    Dim detailOverlayDrawn, detailOverlayDrawn2 As Boolean
    Public secondLayerBeingEdited As Boolean = False
    Public secondLayerItemToEdit As secondLayerItemHolder
    Dim clickableAreas(numberOfCategories + numberInSecondLayer) As Rectangle

#End Region

#Region "On Load"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not My.Computer.FileSystem.FileExists(databaseAddress) Then
            MsgBox("Totally hasn't yet been installed. Install this application using the bundled installer.")
            Me.Close()
        End If


        secondLayerItemToEdit.id = -1 'This is the state that says no second layer item is being edited

        loadTransactionDatabase()
        loadCategoryDatabase()
        loadSecondLayerDatabase()

        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                Dim reader As SQLiteDataReader

                cmd.CommandText = "SELECT date FROM dateoflastreset"
                reader = cmd.ExecuteReader
                reader.Read()
                dateOfLastReset = reader.Item("date") 'Read the date of last time this section ran from the database
                reader.Close()
                If Today.Month <> dateOfLastReset.Month Then 'If the current month isn't equal to the month this section last ran on
                    firstDayOfMonth() 'Run the section
                    dateOfLastReset = Today
                    cmd.CommandText = "UPDATE dateoflastreset SET date='" & dateOfLastReset & "' WHERE id=1"
                    cmd.ExecuteNonQuery() 'Set the date in the database to today's date
                End If
            End Using
        End Using

    End Sub



    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        redrawAllGraphics()
    End Sub

#End Region

#Region "Drawing"

    Sub redrawAllGraphics()
        If Me.WindowState <> FormWindowState.Minimized Then
            Dim numberOfRects As Integer = clickableAreas.Count
            Array.Clear(clickableAreas, 0, numberOfRects)
            getCurrentTotals()
            Dim numberInSecondLayer As Integer = getNumberInSecondLayer("")
            Me.Refresh()
            For i = 0 To numberOfCategories - 1
                redrawCategoryBar(i)
            Next
            For i = 0 To numberInSecondLayer - 1
                drawCircles(i)
            Next
            drawDayOfMonthLine()
        End If
    End Sub

    Sub redrawCategoryBar(categoryToUpdate)
        Dim maxBarWidth As Integer = Me.Size.Width - (2 * categoryBarLeft)
        Dim percentage As Integer = (category(categoryToUpdate).spentAmount / category(categoryToUpdate).totalAmount) * 100
        Dim blueBrush As New System.Drawing.SolidBrush(categoryBarBlue)
        Dim darkBlueBrush As New System.Drawing.SolidBrush(categoryBarDarkBlue)
        Dim greyBrush As New System.Drawing.SolidBrush(categoryBarGrey)

        

        Dim labelBrush As New System.Drawing.SolidBrush(System.Drawing.Color.White)
        Dim labelFont As New System.Drawing.Font("Microsoft Sans Serif", 7)
        Dim drawFormat As New System.Drawing.StringFormat

        Dim categoryBarGraphics As System.Drawing.Graphics
        categoryBarGraphics = Me.CreateGraphics()

        Dim labelPosition As Integer = (categoryBarLeft - 22 + ((percentage / 100) * maxBarWidth)) 'Position of percentage text is just to the left of the bar foreground▶
        If labelPosition < categoryBarLeft Then 'If this means it overhangs the left of the bar background▶
            labelPosition = categoryBarLeft + 2 'Move it back inside the bar■
            If percentage = 0 Then
                labelBrush.Color = System.Drawing.Color.Black
            End If
        End If

        categoryBarGraphics.FillRectangle(greyBrush, New Rectangle(categoryBarLeft, categoryBarTop + (categoryBarBetween * categoryToUpdate), maxBarWidth, 10)) 'Draw the background

        If percentage > 100 Then
            categoryBarGraphics.FillRectangle(darkBlueBrush, New Rectangle(categoryBarLeft, categoryBarTop + (categoryBarBetween * categoryToUpdate), maxBarWidth, 10)) 'Draw the foreground if the percentage is greater than 100
            labelPosition = categoryBarLeft + maxBarWidth - 22
        Else
            categoryBarGraphics.FillRectangle(blueBrush, New Rectangle(categoryBarLeft, categoryBarTop + (categoryBarBetween * categoryToUpdate), (percentage / 100) * maxBarWidth, 10)) 'Draw the foreground if the percentage is between 0 and 100
        End If

        categoryBarGraphics.DrawString(percentage & "%", labelFont, labelBrush, labelPosition, (categoryBarTop - 2 + (categoryBarBetween * categoryToUpdate)), drawFormat) 'Draw the percentage text

        labelBrush.Color = System.Drawing.Color.Black
        categoryBarGraphics.DrawString(category(categoryToUpdate).text, labelFont, labelBrush, categoryBarLeft - 60, (categoryBarTop - 2 + (categoryBarBetween * categoryToUpdate)), drawFormat) 'Draw the category name
        categoryBarGraphics.DrawString("of £" & category(categoryToUpdate).totalAmount, labelFont, labelBrush, categoryBarLeft + maxBarWidth + 2, (categoryBarTop - 2 + (categoryBarBetween * categoryToUpdate)), drawFormat) 'Draw the category total amount

        clickableAreas(categoryToUpdate) = New Rectangle(categoryBarLeft, categoryBarTop + (categoryBarBetween * categoryToUpdate), maxBarWidth, 10) 'Add this rectangle to the collection of areas that will display an info bubble when clicked

        blueBrush.Dispose()
        greyBrush.Dispose()
        labelBrush.Dispose()
        categoryBarGraphics.Dispose()
    End Sub

    Sub drawCircles(circleToUpdate)
        Const distanceBetweenCircles As Integer = 60
        Const distanceBetweenRows As Integer = 50
        Dim numberOfRows As Integer
        Dim numberOfCirclesPerRow As Integer
        Dim blueBrush As New System.Drawing.SolidBrush(categoryBarBlue)
        Dim greyBrush As New System.Drawing.SolidBrush(categoryBarGrey)
        Dim circleGraphics As System.Drawing.Graphics
        Dim labelBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Black)
        Dim labelFont As New System.Drawing.Font("Microsoft Sans Serif", 7)
        Dim drawFormat As New System.Drawing.StringFormat
        Dim alignCenter As New StringFormat
        alignCenter.Alignment = StringAlignment.Center
        circleGraphics = Me.CreateGraphics()

        Dim percentage As Integer = secondLayerItem(circleToUpdate).currentAmount / secondLayerItem(circleToUpdate).totalAmount * 100

        Dim usableWidth As Integer = Me.Size.Width - (2 * categoryBarLeft) 'The window width minus the width of the borders
        numberOfRows = Int(((circleToUpdate + 1) * distanceBetweenCircles) / usableWidth) 'The number of rows of circles that will be needed based on the number of circles and usable width of window
        numberOfCirclesPerRow = Int(usableWidth / distanceBetweenCircles) 'The number of circles that will fit on a full row

        dgvRecords.Top = categoryBarTop + (categoryBarBetween * numberOfCategories) + ((numberOfRows + 1) * distanceBetweenRows) + distanceBetweenRows - 40 'Move the data grid view down to make room for the rows of circles▶
        dgvRecords.Height = (Me.Size.Height - dgvRecords.Top - 67) 'And change its height so its lower edge doesn't move■
        Dim horizontalPosition As Integer = categoryBarLeft + ((circleToUpdate Mod numberOfCirclesPerRow) * distanceBetweenCircles) 'Sets the horizontal position of the circle based on the number of circles per row and the number of circles already drawn
        Dim verticalPosition As Integer = categoryBarTop + (categoryBarBetween * numberOfCategories) + (numberOfRows * distanceBetweenRows) 'Sets the vertical position of the circle based on the number of circles per row and the number of circles already drawn

        circleGraphics.FillEllipse(greyBrush, New Rectangle(horizontalPosition, verticalPosition, 40, 40)) 'Draw the circle background
        circleGraphics.FillPie(blueBrush, New Rectangle(horizontalPosition, verticalPosition, 40, 40), 270, (percentage / 100) * 360) 'Draw the circle foreground
        circleGraphics.DrawString(secondLayerItem(circleToUpdate).nameOfItem, labelFont, labelBrush, horizontalPosition + 20, verticalPosition + 40, alignCenter) 'Draw the item name
        If percentage > 50 Then
            labelBrush.Color = System.Drawing.Color.White
        End If
        circleGraphics.DrawString(percentage & "%", labelFont, labelBrush, horizontalPosition + 20, verticalPosition + 25, alignCenter) 'Draw the percentage text

        clickableAreas(numberOfCategories + circleToUpdate) = New Rectangle(horizontalPosition, verticalPosition, 40, 40) 'Add this circle to the collection of areas that will display an info bubble when clicked
    End Sub

    Sub drawDayOfMonthLine()
        Dim maxBarWidth As Integer = Me.Size.Width - (2 * categoryBarLeft) '
        Dim redLine As System.Drawing.Graphics
        Dim redBrush As New System.Drawing.Pen(Color.SaddleBrown)
        redLine = Me.CreateGraphics
        Dim topPoint, bottomPoint As System.Drawing.Point
        Dim thisMonth As String = Today.Month.ToString
        If System.Text.RegularExpressions.Regex.IsMatch(thisMonth, "1|3|5|7|8|10|12") Then 'Months with 31 days
            topPoint.X = ((Today.Day / 31) * maxBarWidth) + categoryBarLeft 'Set the top of the line
            bottomPoint.X = ((Today.Day / 31) * maxBarWidth) + categoryBarLeft 'Set the bottom of the line
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(thisMonth, "4|6|9|11") Then 'Months with 30 days
            topPoint.X = ((Today.Day / 30) * maxBarWidth) + categoryBarLeft 'Set the top of the line
            bottomPoint.X = ((Today.Day / 30) * maxBarWidth) + categoryBarLeft 'Set the bottom of the line
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(thisMonth, "2") Then 'Months with 28 days
            topPoint.X = ((Today.Day / 28) * maxBarWidth) + categoryBarLeft 'Set the top of the line
            bottomPoint.X = ((Today.Day / 28) * maxBarWidth) + categoryBarLeft 'Set the bottom of the line
        End If
        topPoint.Y = categoryBarTop 'Set the Y coord of the top of the line so that it starts at the top of the category bars
        bottomPoint.Y = categoryBarBetween * numberOfCategories 'Set the Y coord of the bottom of the line so that it ends at the bottom of the category bars


        redLine.DrawLine(redBrush, topPoint, bottomPoint) 'Draw the line
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        Dim overlaysForm, overlaysDgv As System.Drawing.Graphics
        Dim labelBrush As New System.Drawing.SolidBrush(System.Drawing.Color.White)
        Dim labelFont As New System.Drawing.Font("Microsoft Sans Serif", 11)
        Dim detailStringLine1, detailStringLine2, detailStringLine3 As String
        Dim overlaysFormBounds As New Rectangle(e.Location.X - 100, e.Location.Y, 200, 100)
        Dim overlaysDgvBounds As New Rectangle(e.Location.X - 100 - dgvRecords.Left, e.Location.Y - dgvRecords.Top, 200, 100)
        Dim outside As Boolean
        overlaysForm = Me.CreateGraphics
        overlaysDgv = dgvRecords.CreateGraphics
        Dim indexOfDrawnItem As Integer = 0
        detailOverlayDrawn = False

        Do Until indexOfDrawnItem = numberOfCategories + numberInSecondLayer Or detailOverlayDrawn = True 'Run either until all bars and circles have been cycled through or the details panel is drawn
            If clickableAreas(indexOfDrawnItem).Contains(e.Location) Then 'If the mouse click was inside the nth (first thru last) bar/circle
                dgvRecords.Enabled = False 'Disables the records view so that it doesn't draw over the overlay if it gets moused over
                If indexOfDrawnItem < numberOfCategories Then 'If one of the category bars was clicked
                    detailStringLine1 = "Category: " & category(indexOfDrawnItem).name
                    detailStringLine2 = "£" & category(indexOfDrawnItem).spentAmount & " spent of £" & category(indexOfDrawnItem).totalAmount
                    detailStringLine3 = "£" & category(indexOfDrawnItem).totalAmount - category(indexOfDrawnItem).spentAmount & " remaining this month"
                Else 'if it's one of the second layer circles
                    detailStringLine1 = "Item: " & secondLayerItem(indexOfDrawnItem - numberOfCategories).nameOfItem
                    detailStringLine2 = "£" & secondLayerItem(indexOfDrawnItem - numberOfCategories).currentAmount & " saved of £" & secondLayerItem(indexOfDrawnItem - numberOfCategories).totalAmount
                    If secondLayerItem(indexOfDrawnItem - numberOfCategories).endDate <> "null" Then
                        detailStringLine3 = "Needed by " & secondLayerItem(indexOfDrawnItem - numberOfCategories).endDate
                    Else
                        detailStringLine3 = ""
                    End If
                    secondLayerItemToEdit.id = secondLayerItem(indexOfDrawnItem - numberOfCategories).id 'Marks the item clicked on as the item being edited, in case editSecondLayer is opened
                End If
                redrawAllGraphics()
                With overlaysForm 'Draws over the background of the form
                    .DrawImage(My.Resources.details_panel, overlaysFormBounds)
                    .DrawString(detailStringLine1, labelFont, labelBrush, overlaysFormBounds.X + 8, overlaysFormBounds.Y + 22)
                    .DrawString(detailStringLine2, labelFont, labelBrush, overlaysFormBounds.X + 8, overlaysFormBounds.Y + 42)
                    .DrawString(detailStringLine3, labelFont, labelBrush, overlaysFormBounds.X + 8, overlaysFormBounds.Y + 62)
                End With
                With overlaysDgv 'Draws over the data grid view
                    .DrawImage(My.Resources.details_panel, overlaysDgvBounds)
                    .DrawString(detailStringLine1, labelFont, labelBrush, overlaysDgvBounds.X + 8, overlaysDgvBounds.Y + 22)
                    .DrawString(detailStringLine2, labelFont, labelBrush, overlaysDgvBounds.X + 8, overlaysDgvBounds.Y + 42)
                    .DrawString(detailStringLine3, labelFont, labelBrush, overlaysDgvBounds.X + 8, overlaysDgvBounds.Y + 62)
                End With
                detailOverlayDrawn = True
                detailOverlayDrawn2 = True 'This is necessary to edit detailOverlayDrawn's value whilst preserving its old value
                outside = False
            Else
                outside = True
            End If
            indexOfDrawnItem += 1
        Loop
        Dim inside As Boolean
        For i = 0 To numberOfCategories + numberInSecondLayer 'For every bar and pie▶
            If clickableAreas(i).Contains(e.Location) Then 'was the mouseclick inside?■
                inside = True
            End If
        Next
        outside = Not inside 'If inside was true, outside is false and vice versa
        If outside = True Then
            If detailOverlayDrawn2 Then
                detailOverlayDrawn = False
                detailOverlayDrawn2 = False
                dgvRecords.Enabled = True
                secondLayerItemToEdit.id = -1 'No bar or pie is selected
                redrawAllGraphics()
            End If
        End If
    End Sub
#End Region

#Region "Move and Resize"

    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If Me.Visible = True Then
            moveEnd.Enabled = True
            moveEnd.Start()
        End If
    End Sub

    Private Sub Form1_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        moveEnd.Enabled = True
        moveEnd.Start()
    End Sub

    Private Sub moveEnd_Tick(sender As Object, e As EventArgs) Handles moveEnd.Tick
        If Not MouseButtons = Windows.Forms.MouseButtons.Left Then 'If the window has been released
            If Me.Visible = True Then
                redrawAllGraphics()
            End If
            moveEnd.Stop()
            moveEnd.Enabled = False
        Else
            moveEnd.Start()
        End If
    End Sub

#End Region

#Region "SQL"
#Region "Create"
    ''This region contains code to create a blank database. 
    ''As the program distributes with both a blank database to be added to and a template of the same database to be copied in case the database in use was lost, this section will never run in normal use and has been commented out accordingly.
    ''It has been left in the code so the database structure is preserved if the original databases are lost.

    'Sub createCategoryDatabase()
    '    Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
    '        conn.Open()
    '        Using cmd As New SQLiteCommand(conn)
    '            cmd.CommandText = "create table category(name VARCHAR(12) PRIMARY KEY, total DOUBLE)"
    '            cmd.ExecuteNonQuery()
    '        End Using
    '        conn.Close()
    '    End Using
    'End Sub

    'Sub createTransactionDatabase()
    '    Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
    '        conn.Open()
    '        Using cmd As New SQLiteCommand(conn)
    '            cmd.CommandText = "CREATE TABLE transactions(id INTEGER PRIMARY KEY, expense BOOLEAN, amount DOUBLE, shop VARCHAR(100), description VARCHAR(300), dDate VARCHAR(10), tTime VARCHAR(5), category VARCHAR(12), layer INTEGER)"
    '            cmd.ExecuteNonQuery()
    '        End Using
    '        conn.Close()
    '    End Using
    'End Sub

    'Sub createSecondLayerDatabase()
    '    Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
    '        conn.Open()
    '        Using cmd As New SQLiteCommand(conn)
    '            cmd.CommandText = "CREATE TABLE secondlayer(id INTEGER PRIMARY KEY, totalamount DOUBLE, currentamount DOUBLE, nameofitem VARCHAR(100), description VARCHAR(300), enddate VARCHAR(10))"
    '            cmd.ExecuteNonQuery()
    '        End Using
    '        conn.Close()
    '    End Using
    'End Sub
#End Region
#Region "Update"
    Sub updateCategoryDatabase()
        Dim numberOfCats As Integer = getNumberOfCategoriesSelected()

        Using connDelete As New SQLiteConnection("Data Source=" & databaseAddress)
            connDelete.Open()
            Using cmdDelete As New SQLiteCommand(connDelete)

                If numberOfCats <> 0 Then
                    cmdDelete.CommandText = "DELETE FROM category" 'Clear the table
                    Try
                        cmdDelete.ExecuteNonQuery()
                    Catch ex As Exception 'Just in case two operations happen so quickly the first one didn't complete (unlikely)
                        MsgBox("Database already in use! Try again in a few seconds.")
                    End Try
                End If
            End Using
            connDelete.Close()
        End Using

        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                For i = 0 To numberOfCategories - 1
                    cmd.CommandText = "INSERT INTO category(name, total) values('" & category(i).name & "', " & category(i).totalAmount & ")" 'Add all items to the table
                    cmd.ExecuteNonQuery()
                Next
            End Using
            conn.Close()
        End Using
    End Sub

    Sub updateSecondLayerDatabase()
        Dim numberInSecond As Integer = getNumberInSecondLayer("")
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                For i = 1 To numberInSecond
                    Try
                        cmd.CommandText = "UPDATE secondlayer SET currentAmount= " & secondLayerItem(i).currentAmount & " WHERE id=" & i
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Next
            End Using
            conn.Close()
        End Using
    End Sub
#End Region
#Region "Load"
    Sub loadTransactionDatabase()
        Dim failed As Boolean = False
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                Dim reader As SQLiteDataReader

                cmd.CommandText = "SELECT COUNT(*) AS RETURNCOUNT FROM transactions WHERE amount <> 0"
                numberOfRecords = cmd.ExecuteScalar

                cmd.CommandText = "SELECT transactions.id, transactions.expense, transactions.amount, transactions.shop, transactions.description, transactions.dDate, transactions.tTime, transactions.category FROM transactions WHERE transactions.amount <> 0"
                'Only selects where amount <> 0 because the way to delete a transaction is to set total to 0, which will make it disappear from the list but remain in the database
                reader = cmd.ExecuteReader()

                Try
                    dgvRecords.Rows.Clear()
                Catch ex As Exception 'This is thrown because the user clicked on the dgv whilst editing it
                    failed = True
                    MsgBox("Press enter after editing a cell rather than clicking outside, otherwise your changes can't commit.")
                End Try
                If failed = False Then
                    Dim incomePositive As String
                    For i = 1 To numberOfRecords
                        reader.Read()
                        Select Case reader.Item("expense")
                            Case True
                                incomePositive = "-"
                            Case Else
                                incomePositive = "+"
                        End Select
                        dgvRecords.Rows.Add(incomePositive & "£" & reader.Item("amount"), reader.Item("shop"), reader.Item("description"), reader.Item("dDate") & " " & reader.Item("tTime"), reader.Item("category"), reader.Item("id")) 'Add the item from the database to the dgv
                    Next
                End If
                reader.Close()
            End Using
            conn.Close()
        End Using
        If failed = False Then
            dgvRecords.Sort(dgvColDateTime, System.ComponentModel.ListSortDirection.Descending) 'Sort the data grid view by date from most recent at the top to oldest at the bottom
        End If
    End Sub

    Sub loadCategoryDatabase()
        Dim numberOfCats As Integer = getNumberOfCategoriesSelected()
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                Dim reader As SQLiteDataReader

                cmd.CommandText = "SELECT name, total FROM category WHERE total != 0" 'Only selects categories with total not 0 because the database deselects categories by setting their total to 0
                reader = cmd.ExecuteReader
                For i = 0 To numberOfCats - 1
                    reader.Read()
                    category(i).totalAmount = reader.Item("total")
                    category(i).name = reader.Item("name")
                    category(i).text = reader.Item("name") 'Some parts of the system require separate name and text, but they both have the same value
                Next
                reader.Close()
            End Using
        End Using
        numberOfCategories = numberOfCats 'global variable numberOfCategories = temporary value used in this sub


    End Sub

    Sub loadSecondLayerDatabase()
        Dim numberInSecondLayer As Integer = getNumberInSecondLayer("") 'This improves efficiency by not calling the function throughout the sub. The empty string argument is so every item in the second layer is counted
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                Dim reader As SQLiteDataReader
                cmd.CommandText = "SELECT id, totalamount, currentamount, nameofitem, description, enddate FROM secondlayer WHERE totalamount != 0" ' because items are deleted by setting totalamount = 0
                reader = cmd.ExecuteReader
                For i = 0 To numberInSecondLayer - 1
                    Try
                        reader.Read()
                        secondLayerItem(i).id = reader.Item("id")
                        secondLayerItem(i).totalAmount = reader.Item("totalamount")
                        secondLayerItem(i).currentAmount = reader.Item("currentamount")
                        secondLayerItem(i).nameOfItem = reader.Item("nameofitem")
                        secondLayerItem(i).description = reader.Item("description")
                        secondLayerItem(i).endDate = reader.Item("enddate")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Next
                reader.Close()
            End Using
        End Using
    End Sub
#End Region
#Region "Get"
    Function getNumberOfCategoriesSelected()
        Dim oldCatNumber As Integer
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "SELECT COUNT(*) AS RETURNCOUNT FROM category"
                oldCatNumber = cmd.ExecuteScalar
            End Using
            conn.Close()
        End Using
        getNumberOfCategoriesSelected = oldCatNumber 'function returns getNumberOfCategoriesSelected = temporary value used in this sub
    End Function

    Sub getCurrentTotals()
        Dim tempTotal As Single
        Dim numberOfTransactionPerCategory As Integer
        Dim reader As SQLiteDataReader
        For i = 0 To numberOfCategories
            Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
                conn.Open()
                Using cmd As New SQLiteCommand(conn)
                    cmd.CommandText = "SELECT COUNT(*) AS RETURNCOUNT FROM transactions WHERE category = '" & category(i).name & "'"
                    numberOfTransactionPerCategory = cmd.ExecuteScalar

                End Using
                conn.Close()
            End Using


            Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
                conn.Open()
                Using cmd As New SQLiteCommand(conn)


                    cmd.CommandText = "SELECT amount FROM transactions WHERE category = '" & category(i).name & "'"
                    reader = cmd.ExecuteReader

                    For j = 1 To numberOfTransactionPerCategory
                        reader.Read()
                        tempTotal += reader.Item("amount") 'sums amount field of every transaction in the database with a certain category assigned
                    Next

                    category(i).spentAmount = tempTotal 'global variable category(i).spentAmount = temporary value used in this sub
                    tempTotal = 0
                    reader.Close()
                End Using
                conn.Close()
            End Using
        Next
    End Sub

    Public Function getNumberInSecondLayer(args As String)
        'args is a string like "WHERE currentamount = 0" etc. This allows it to be called with arguments depending on exactly what data is needed
        Dim numberOfTransactions As Integer
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "SELECT COUNT(*) AS RETURNCOUNT FROM secondlayer WHERE totalamount != 0" & args
                numberOfTransactions = cmd.ExecuteScalar
            End Using
            conn.Close()
        End Using
        getNumberInSecondLayer = numberOfTransactions 'function returns getNumberInSecondLayer = temporary value used in this sub
    End Function
#End Region
#End Region

#Region "Buttons"
#Region "Main Buttons"
    Private Sub picAdd_Click(sender As Object, e As EventArgs) Handles picAdd.Click
        addTransaction.Show()
    End Sub

    Private Sub picCats_Click(sender As Object, e As EventArgs) Handles picCats.Click
        chooseCategories.Show()
    End Sub

    Private Sub picSecond_Click(sender As Object, e As EventArgs) Handles picSecond.Click
        secondLayerItemToEdit.id = -1
        editSecondLayer.Show()
    End Sub

    Private Sub picEdit_Click(sender As Object, e As EventArgs) Handles picEdit.Click
        If secondLayerItemToEdit.id <> -1 Then 'If an item is selected by having its pie clicked
            secondLayerBeingEdited = True
            Dim arrayIndex As Integer
            For i = 0 To getNumberInSecondLayer("")
                If secondLayerItem(i).id = secondLayerItemToEdit.id Then 'Cycle through the items comparing IDs until they match
                    arrayIndex = i
                End If
            Next

            secondLayerItemToEdit.currentAmount = secondLayerItem(arrayIndex).currentAmount
            secondLayerItemToEdit.totalAmount = secondLayerItem(arrayIndex).totalAmount
            secondLayerItemToEdit.description = secondLayerItem(arrayIndex).description
            secondLayerItemToEdit.endDate = secondLayerItem(arrayIndex).endDate
            secondLayerItemToEdit.nameOfItem = secondLayerItem(arrayIndex).nameOfItem
            editSecondLayer.Show()
        Else 'If no items are selected
            MsgBox("Select one of the second layer pie charts before attempting to edit them.")
        End If

    End Sub

#End Region
#Region "Menu Buttons"
    ''This region contains code for buttons that have been removed in the end product. This is because they are only needed for debugging.
    ''If this code is debugged in future, add the buttons described in this region and uncomment it

    '    Private Sub btnRandomise_Click(sender As Object, e As EventArgs)
    '        Dim rand As New Random
    '        For i = 0 To numberOfCategories - 1
    '            category(i).spentAmount = category(i).totalAmount * rand.NextDouble
    '        Next
    '        btnDraw_Click(Me, Nothing)

    '        loadTransactionDatabase()
    '    End Sub

    '    Private Sub btnCreateCategoryDB_Click(sender As Object, e As EventArgs)
    '        'createCategoryDatabase()
    '    End Sub

    '    Private Sub btnSelectCategories_Click(sender As Object, e As EventArgs)
    '        chooseCategories.Show()
    '    End Sub

    '    Private Sub btnDraw_Click(sender As Object, e As EventArgs)
    '        Me.Refresh()
    '        For i = 0 To numberOfCategories - 1
    '            redrawCategoryBar(i)
    '        Next
    '    End Sub

    '    Private Sub btnCreateRecordsDB_Click(sender As Object, e As EventArgs)
    '        'createTransactionDatabase()
    '    End Sub

    '    Private Sub btnCreate2ndLayerDB_Click(sender As Object, e As EventArgs)
    '        'createSecondLayerDatabase()
    '    End Sub
#End Region
#Region "Button Effects"

    Private Sub picAdd_MouseEnter(sender As Object, e As EventArgs) Handles picAdd.MouseEnter
        picAdd.Image = My.Resources.add_hover
    End Sub

    Private Sub picAdd_MouseLeave(sender As Object, e As EventArgs) Handles picAdd.MouseLeave
        picAdd.Image = My.Resources.greyadd
    End Sub

    Private Sub picCats_MouseEnter(sender As Object, e As EventArgs) Handles picCats.MouseEnter
        picCats.Image = My.Resources.cats_hover
    End Sub

    Private Sub picCats_MouseLeave(sender As Object, e As EventArgs) Handles picCats.MouseLeave
        picCats.Image = My.Resources.grey_choose_categories
    End Sub

    Private Sub picSecond_MouseEnter(sender As Object, e As EventArgs) Handles picSecond.MouseEnter
        picSecond.Image = My.Resources._2_and_3_hover
    End Sub

    Private Sub picSecond_MouseLeave(sender As Object, e As EventArgs) Handles picSecond.MouseLeave
        picSecond.Image = My.Resources.grey2and3
    End Sub

    Private Sub picEdit_MouseEnter(sender As Object, e As EventArgs) Handles picEdit.MouseEnter
        picEdit.Image = My.Resources.edit_hover
    End Sub

    Private Sub picEdit_MouseLeave(sender As Object, e As EventArgs) Handles picEdit.MouseLeave
        picEdit.Image = My.Resources.greyedit
    End Sub

#End Region
#End Region

#Region "First day of the month"

    Sub firstDayOfMonth()
        Dim disposableIncome As Double
        Dim earned As Double
        Dim spent As Double
        Dim reader As SQLiteDataReader
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                If Today.Month > 9 Then 'October thru December
                    cmd.CommandText = "SELECT TOTAL(amount) FROM transactions WHERE dDate LIKE '__/" & Today.Month & "/" & Today.Year & "' AND expense = 1"
                    reader = cmd.ExecuteReader
                    reader.Read()
                    spent = reader.GetDouble(0)
                    reader.Close()
                    cmd.CommandText = "SELECT TOTAL(amount) FROM transactions WHERE dDate LIKE '__/" & Today.Month & "/" & Today.Year & "' AND expense = 0"
                    reader = cmd.ExecuteReader
                    reader.Read()
                    earned = reader.GetDouble(0)
                    reader.Close()
                Else 'January thru September. Difference must be made because system reports month number as "6" etc rather than "06"
                    cmd.CommandText = "SELECT TOTAL(amount) FROM transactions WHERE dDate LIKE '__/0" & Today.Month & "/" & Today.Year & "' AND expense = 1"
                    reader = cmd.ExecuteReader
                    reader.Read()
                    spent = reader.GetDouble(0)
                    reader.Close()
                    cmd.CommandText = "SELECT TOTAL(amount) FROM transactions WHERE dDate LIKE '__/0" & Today.Month & "/" & Today.Year & "' AND expense = 0"
                    reader = cmd.ExecuteReader
                    reader.Read()
                    earned = reader.GetDouble(0)
                    reader.Close()
                End If
            End Using
            conn.Close()
        End Using
        disposableIncome = earned - spent
        If disposableIncome < 0 Then
            MsgBox("Bad news! Last month, you spent £" & disposableIncome * -1 & " more than you earned. This means you can't put any money towards things you're saving for, and you can't recharge your emergency funds. Consider decreasing budget limits or making sure you stick to them better.")
        Else
            Do Until disposableIncome = 0 'Loop through second and third layer items
                Dim indexOfShortestEndDate As Integer
                fillNextSecondLayerItem(indexOfShortestEndDate)
                secondLayerItem(indexOfShortestEndDate).currentAmount += disposableIncome 'Adds all the disposable income to the current amount, even if current amount > total amount afterwards
                If secondLayerItem(indexOfShortestEndDate).currentAmount > secondLayerItem(indexOfShortestEndDate).totalAmount Then 'If it did spill over,
                    disposableIncome = secondLayerItem(indexOfShortestEndDate).currentAmount - secondLayerItem(indexOfShortestEndDate).totalAmount 'Put the difference back into disposable income
                Else
                    disposableIncome = 0
                End If
            Loop
        End If
        updateSecondLayerDatabase()
        redrawAllGraphics()
    End Sub

    Sub fillNextSecondLayerItem(ByRef index)
        Dim numberInSecond As Integer = getNumberInSecondLayer("")
        Dim shortestEndDate As String = Date.Today.AddYears(100).ToString("yyyyMMdd") 'A date sufficiently far in the future that no item's end date will be set to after this in normal use

        For i = 0 To numberInSecond - 1
            If secondLayerItem(i).endDate <> "null" And secondLayerItem(i).currentAmount <= secondLayerItem(i).totalAmount Then 'Items that are in the second layer and aren't full
                Dim thisEndDate As String = Mid(secondLayerItem(i).endDate, 7, 4) & Mid(secondLayerItem(i).endDate, 4, 2) & Mid(secondLayerItem(i).endDate, 1, 2) 'Gets the date in a format that can be directly compared
                If thisEndDate <= shortestEndDate Then 'These can be compared because they have been got in the format yyyyMMdd so alphabetical order = chronological order
                    shortestEndDate = thisEndDate
                    index = i
                End If
            End If
        Next
    End Sub

#End Region

#Region "Edit"

    Private Sub dgvRecords_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRecords.CellEndEdit
        Dim columnName As String = dgvRecords.Columns.Item(e.ColumnIndex).HeaderText
        Dim updatedCell As String = ""
        If dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
            updatedCell = dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value.ToString
            Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
                conn.Open()
                Using cmd As New SQLiteCommand(conn)
                    Try
                        If updatedCell <> "" Or Not updatedCell.Contains("'") Then 'If not empty or containing an SQL breaking apostrophe (they wouldn't have been allowed these when they added the transaction
                            'TODO: Should this ^ be an and?
                            If columnName = "Shop" Or columnName = "Description" Then
                                cmd.CommandText = "UPDATE transactions SET " & LCase(columnName) & "= '" & updatedCell & "' WHERE id=" & CInt(dgvRecords.Item("index", e.RowIndex).Value)
                            ElseIf columnName = "Amount" Then
                                If updatedCell.Contains("-£") Then 'If they've left the minus sign and the pound sign in ▶
                                    Try
                                        updatedCell = CSng(Mid(updatedCell, 3)) 'Set updatedCell to just the number ■
                                    Catch ex As Exception
                                        MsgBox("Invalid format.")
                                    End Try
                                ElseIf updatedCell.Contains("£") Or updatedCell.Contains("-") Then 'If they've only left the pound or minus sign in ▶
                                    Try
                                        updatedCell = CSng(Mid(updatedCell, 2)) 'Set updatedCell to just the number ■
                                    Catch ex As Exception
                                        MsgBox("Invalid format.")
                                    End Try
                                End If
                                If System.Text.RegularExpressions.Regex.IsMatch(updatedCell, "^\d+(?:\.\d{2})?$") Then
                                    cmd.CommandText = "UPDATE transactions SET " & LCase(columnName) & "= '" & updatedCell & "' WHERE id=" & CInt(dgvRecords.Item("index", e.RowIndex).Value)
                                Else
                                    MsgBox("Invalid format.")
                                End If
                            ElseIf columnName = "Category" Then
                                Dim found As Boolean = False
                                For i = 0 To numberOfCategories - 1 'Cycle through the selected categories ▶
                                    If category(i).name = updatedCell Then 'If the category they typed is one of the selected category names ▶
                                        found = True 'Then allow this edit ■
                                    End If
                                Next
                                If found = False Then
                                    MsgBox("This category has to be one of the ticked categories. Check your spelling and try again.")
                                    dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value = ""
                                Else
                                    cmd.CommandText = "UPDATE transactions SET " & LCase(columnName) & "= '" & updatedCell & "' WHERE id=" & CInt(dgvRecords.Item("index", e.RowIndex).Value)
                                End If
                            ElseIf columnName = "Date and Time" Then
                                Dim tempDate, tempTime As String
                                tempDate = Mid(updatedCell, 1, 10) 'Split the string into date ▶
                                tempTime = Mid(updatedCell, 12, 5) 'And time ■
                                If System.Text.RegularExpressions.Regex.IsMatch(tempTime, "^([0-1][0-9]|[2][0-3]):([0-5][0-9])$") And System.Text.RegularExpressions.Regex.IsMatch(tempDate, "^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$") Then 'If both are valid
                                    cmd.CommandText = "UPDATE transactions SET dDate = '" & tempDate & "', tTime = '" & tempTime & "' WHERE id=" & CInt(dgvRecords.Item("index", e.RowIndex).Value)
                                Else
                                    MsgBox("Invalid date or time format.")
                                End If
                            End If
                            cmd.ExecuteNonQuery()
                        End If
                    Catch ex As Exception
                        MsgBox("For technical reasons, the database can't hold the apostrophe character. Consider using ` (below escape) instead.")
                    End Try
                End Using
                conn.Close()
            End Using
            loadTransactionDatabase()
            redrawAllGraphics()
        Else
            dgvRecords.CancelEdit()
            MsgBox("You can't leave the cell empty. Please put a value in.")
        End If
    End Sub

#End Region

    
End Class