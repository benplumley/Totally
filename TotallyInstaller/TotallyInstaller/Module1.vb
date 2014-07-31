Imports System.Environment
Imports System.IO

'The following files need to be in the same directory as this installer:
'Budget.db
'file.txt
'TotallyDaemon.exe
'Totally.exe

Module Module1

    Sub Main()
        createAppdataFiles()
        createStartupFiles()
        createDropboxFiles()
        runSetup()
        finished()
    End Sub

    Sub createAppdataFiles()
        Dim appdataPath As String = GetFolderPath(SpecialFolder.ApplicationData)
        Console.WriteLine("Creating appdata folder...")
        If Not Directory.Exists(appdataPath & "\Totally") Then
            Directory.CreateDirectory(appdataPath & "\Totally")
            Console.WriteLine("Appdata folder created successfully.")
        Else
            Console.WriteLine("Appdata folder already exists. Continuing...")
        End If

        Console.WriteLine("Copying blank database to appdata folder...") '### Budget
        If Not My.Computer.FileSystem.FileExists(appdataPath & "\Totally\Budget.db") Then
            Try
                File.Copy(My.Application.Info.DirectoryPath & "\Example Data.db", appdataPath & "\Totally\Budget.db")
                Console.WriteLine("Database copied successfully.")
            Catch ex As Exception
                fileNotFoundException(ex)
            End Try
        Else
            Console.WriteLine("Database already exists. Continuing...")
        End If

        Console.WriteLine("Copying files to Program Files folder...") '### Totally
        Try
            File.Copy(My.Application.Info.DirectoryPath & "\Totally.exe", appdataPath & "\Totally\Totally.exe", True)
            Console.WriteLine("Files copied successfully.")
        Catch ex As Exception
            fileNotFoundException(ex)
        End Try

        If Not My.Computer.FileSystem.FileExists(appdataPath & "\Totally\blank database template.db") Then '### Budget Template
            Try
                File.Copy(My.Application.Info.DirectoryPath & "\Budget.db", appdataPath & "\Totally\blank database template.db")
                Console.WriteLine("Files copied successfully.")
            Catch ex As Exception
                fileNotFoundException(ex)
            End Try
        Else
            Console.WriteLine("Files already exist. Continuing...")
        End If

        If Not My.Computer.FileSystem.FileExists(appdataPath & "\Totally\file.txt") Then '### Dropbox Placeholder
            Try
                File.Copy(My.Application.Info.DirectoryPath & "\file.txt", appdataPath & "\Totally\file.txt")
                Console.WriteLine("Files copied successfully.")
            Catch ex As Exception
                fileNotFoundException(ex)
            End Try
        Else
            Console.WriteLine("Files already exist. Continuing...")
        End If

        Try
            File.Copy(My.Application.Info.DirectoryPath & "\TotallyDaemon.exe", appdataPath & "\Totally\TotallyDaemon.exe", True)
            Console.WriteLine("Files copied successfully.")
        Catch ex As Exception
            fileNotFoundException(ex)
        End Try
    End Sub

    Sub createDropboxFiles()
        Try
            Dim base64Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\host.db")
            Dim lines As String() = System.IO.File.ReadAllLines(base64Path)
            Dim dbBase64Text As Byte() = Convert.FromBase64String(lines(1))
            Dim dropboxPath As String = System.Text.ASCIIEncoding.ASCII.GetString(dbBase64Text)

            Console.WriteLine("Creating dropbox folder...")
            If Not Directory.Exists(dropboxPath & "\Totally\New") Then
                Directory.CreateDirectory(dropboxPath & "\Totally\New")
                Console.WriteLine("Dropbox folder created successfully.")
            Else
                Console.WriteLine("Dropbox folder already exists. Continuing...")
            End If

            Console.WriteLine("Copying files to dropbox folder...")
            If Not My.Computer.FileSystem.FileExists(dropboxPath & "\Totally\New\file.txt") Then
                Try
                    File.Copy(My.Application.Info.DirectoryPath & "\file.txt", dropboxPath & "\Totally\New\file.txt")
                    Console.WriteLine("Files copied successfully.")
                Catch ex As Exception
                    fileNotFoundException(ex)
                End Try
            Else
                Console.WriteLine("Files already exist. Continuing...")
            End If
        Catch ex As Exception
            dropboxNotInstalledException()
        End Try

    End Sub

    Sub createStartupFiles()
        Console.WriteLine("Copying files to startup folder...")
        Dim startupPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup)
        Try
            File.Copy(My.Application.Info.DirectoryPath & "\TotallyDaemon.exe", startupPath & "\TotallyDaemon.exe", True)
            Console.WriteLine("Files copied successfully.")
        Catch ex As Exception
            fileNotFoundException(ex)
        End Try
    End Sub

    Sub runSetup()
        Try
            Process.Start(My.Application.Info.DirectoryPath & "\setup.exe")
        Catch ex As Exception
            fileNotFoundException(ex)
        End Try
    End Sub

    Sub finished()
        Console.WriteLine()
        Console.ForegroundColor = ConsoleColor.Black
        Console.BackgroundColor = ConsoleColor.White
        Console.WriteLine("Installation has completed successfully. Totally will now launch.")
        Threading.Thread.Sleep(2000)
        Console.Clear()
        Console.WriteLine("Thanks for using Totally!")
        Threading.Thread.Sleep(1000)
    End Sub

    Sub fileNotFoundException(ex As Exception)
        Console.BackgroundColor = ConsoleColor.Red
        Console.WriteLine("An error occurred. Extract the files from the zip archive again and run the installer without moving or renaming any files in the extracted directory.")
        Console.BackgroundColor = ConsoleColor.Black
        Console.WriteLine("Error details: " & ex.Message)
        Console.WriteLine("Press any key to close this installer and fix the error before trying again.")
        Console.ReadKey()
        End
    End Sub

    Sub dropboxNotInstalledException()
        Console.BackgroundColor = ConsoleColor.Red
        Console.WriteLine("An error occurred. Dropbox does not appear to be installed on this computer. Please install Dropbox and then run the installer again.")
        Console.BackgroundColor = ConsoleColor.Black
        Console.WriteLine("You can override this error if you are sure that Dropbox is installed. Would you like to? (Y/N)")
        Dim response As String = UCase(Console.ReadLine)
        Select Case response
            Case "Y"
                Console.WriteLine("Navigate to your Dropbox folder, usually located at C:\Users\%USERNAME%\Dropbox.")
                Console.WriteLine("Create a new folder called Totally and navigate inside this new folder.")
                Console.WriteLine("Create a new folder called New and navigate inside this new folder.")
                Console.WriteLine("In a separate instance of Windows Explorer, navigate to C:\Program Files\Totally or C:\Program Files (x86)\Totally.")
                Console.WriteLine("Copy the text file called file.txt from Program Files to C:\Users\%USERNAME%\Dropbox\Totally\New.")
                Console.WriteLine("When this is complete, press any key to return to installation.")
                Console.ReadKey()
            Case Else
                Console.WriteLine("Press any key to close this installer and fix the error before trying again.")
                Console.ReadKey()
                End
        End Select
    End Sub


End Module
