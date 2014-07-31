Imports System.IO
Imports SQLite
Imports System.Data.SQLite
Imports System.Environment

Public Class Form1

    'Text file syntax:
    'Date Time
    'Expense:1:Amount:2:Shop:3:Description:4:Category:5:Layer:6:

    Public databaseAddress As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Totally\Budget.db"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Threading.Thread.Sleep(60 * 1000) 'Sleep for one minute to allow dropbox to sync



        Dim base64Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\host.db") 'Gets the location of the dropbox folder
        Dim lines As String() = System.IO.File.ReadAllLines(base64Path)
        Dim dbBase64Text As Byte() = Convert.FromBase64String(lines(1))
        Dim dropboxBasePath As String = System.Text.ASCIIEncoding.ASCII.GetString(dbBase64Text) & "\Totally\New\" 'Turns the path into a string
        Dim dropboxPath As String = dropboxBasePath

        Dim counter = My.Computer.FileSystem.GetFiles(dropboxBasePath)
        Dim numberOfFilesToAdd As Integer = counter.Count - 1 'The number of files that have been added via mobile since last run

        Dim expense As String
        Dim amount As Single
        Dim shop As String
        Dim description As String
        Dim category As String
        Dim layer As Integer

        Dim dDate As String
        Dim month As String
        Dim day As String
        Dim year As String
        Dim time As String
        For i = 1 To numberOfFilesToAdd
            dropboxPath = dropboxBasePath & "file (" & i & ").txt"
            Dim textFileReader As New StreamReader(dropboxPath)
            Dim firstLine As String = textFileReader.ReadLine
            Try
                Select Case Mid(firstLine, 1, InStr(firstLine, " ") - 1) 'Takes the first word of the first line and turns it from a month word to a month number
                    Case "January"
                        month = "01"
                    Case "February"
                        month = "02"
                    Case "March"
                        month = "03"
                    Case "April"
                        month = "04"
                    Case "May"
                        month = "05"
                    Case "June"
                        month = "06"
                    Case "July"
                        month = "07"
                    Case "August"
                        month = "08"
                    Case "September"
                        month = "09"
                    Case "October"
                        month = "10"
                    Case "November"
                        month = "11"
                    Case "December"
                        month = "12"
                    Case Else
                        MsgBox("Parsing error adding Dropbox/Totally/New/file (" & i & ").txt to the Totally budget database. Open the file and check the format, then save and close. If this error happens again, add the data manually in the Totally application then delete the text file. If this error persists between text files, make sure they are the correct format which is specified in Dropbox/Budget/New/file.txt.")
                End Select
            Catch ex As Exception
                MsgBox("Parsing error adding Dropbox/Totally/New/file (" & i & ").txt to the Totally budget database. Open the file and check the format, then save and close. If this error happens again, add the data manually in the Totally application then delete the text file. If this error persists between text files, make sure they are the correct format which is specified in Dropbox/Budget/New/file.txt.")
            End Try
            day = Mid(firstLine, InStr(firstLine, " ") + 1, 2)
            year = Mid(firstLine, InStr(firstLine, ", ") + 2, 4)
            dDate = day & "/" & month & "/" & year
            Select Case Mid(firstLine, InStr(firstLine, "at ") + 8, 2)
                Case "AM"
                    time = Mid(firstLine, InStr(firstLine, "at " + 3), 4)
                Case "PM"
                    time = Mid(firstLine, InStr(firstLine, "at ") + 3, 5)
                    Dim hours As Integer = CInt(Mid(time, 1, 2))
                    hours += 12
                    time = hours & (Mid(time, 3, 3))
                Case Else
                    MsgBox("Parsing error adding Dropbox/Totally/New/file (" & i & ").txt to the Totally budget database. Open the file and check the format, then save and close. If this error happens again, add the data manually in the Totally application then delete the text file. If this error persists between text files, make sure they are the correct format which is specified in Dropbox/Budget/New/file.txt.")
            End Select
            Try
                expense = textFileReader.ReadLine

                textFileReader.ReadLine()
                Dim amountString As String = textFileReader.ReadLine
                If Mid(amountString, 1, 1) = "£" Then 'If they've included the pound sign
                    amount = Mid(amountString, 2)
                Else
                    amount = amountString
                End If
                textFileReader.ReadLine()
                shop = textFileReader.ReadLine
                textFileReader.ReadLine()
                description = textFileReader.ReadLine
                textFileReader.ReadLine()
                category = textFileReader.ReadLine
                textFileReader.ReadLine()
                layer = 1

                writeToDatabase(expense, amount, shop, description, dDate, time, category, layer)
                textFileReader.Close()
                My.Computer.FileSystem.DeleteFile(dropboxPath)
            Catch ex As Exception
                MsgBox("Parsing error adding Dropbox/Totally/New/file (" & i & ").txt to the Totally budget database. Open the file and check the format, then save and close. If this error happens again, add the data manually in the Totally application then delete the text file. If this error persists between text files, make sure they are the correct format which is specified in Dropbox/Budget/New/file.txt.")
            End Try


        Next
    End Sub

    Sub writeToDatabase(expense, amount, shop, description, dDate, time, category, layer)
        Using conn As New SQLiteConnection("Data Source=" & databaseAddress)
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "INSERT INTO transactions(expense, amount, shop, description, dDate, tTime, category, layer) values(1, " & amount & ", '" & shop & "', '" & description & "', '" & dDate & "', '" & time & "', '" & category & "', " & layer & ")"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Close() 'Closes the form as soon as all its load events finish
    End Sub
End Class
