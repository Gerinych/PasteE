Imports SharpShell.SharpContextMenu
Imports SharpShell.Attributes
Imports System.Windows.Forms
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Principal

<ComVisibleAttribute(True)> _
<COMServerAssociation(SharpShell.Attributes.AssociationType.Class, "Directory\Background")> _
<COMServerAssociation(SharpShell.Attributes.AssociationType.Class, "Library\Background")> _
Public Class PasteE
    Inherits SharpContextMenu

    Protected Overrides Function CanShowMenu() As Boolean
        'Check if clipboard has some data
        If Clipboard.ContainsText Or Clipboard.ContainsImage Then Return True Else Return False
    End Function

    Protected Overrides Function CreateMenu() As Windows.Forms.ContextMenuStrip
        Dim menu As New ContextMenuStrip
        Dim data = Nothing

        'If clipboard contains text (duh)
        If Clipboard.ContainsText Then
            Dim itemPaste As New ToolStripMenuItem
            itemPaste.Text = "Paste text to file..."

            'Store text in data variable
            data = Clipboard.GetText

            AddHandler itemPaste.Click, Sub() Me.subPaste(data, ".txt")

            menu.Items.Add(itemPaste)
            menu.Items.Add("-")
        ElseIf Clipboard.ContainsImage Then
            Dim itemPaste As New ToolStripMenuItem
            itemPaste.Text = "Paste image into file"

            'Store image in data variable
            data = Clipboard.GetImage

            Dim tBmp, tJpg, tPng As New ToolStripMenuItem
            tBmp.Text = "Save as BMP..."
            tJpg.Text = "Save as JPG..."
            tPng.Text = "Save as PNG..."

            AddHandler tBmp.Click, Sub() Me.subPaste(data, ".bmp")
            AddHandler tJpg.Click, Sub() Me.subPaste(data, ".jpg")
            AddHandler tPng.Click, Sub() Me.subPaste(data, ".png")

            itemPaste.DropDownItems.Add(tBmp)
            itemPaste.DropDownItems.Add(tJpg)
            itemPaste.DropDownItems.Add(tPng)

            menu.Items.Add(itemPaste)
            menu.Items.Add("-")
        End If

        Return menu
    End Function

    Private Sub subPaste(ByVal data As Object, ByVal ext As String)

        Do
            Dim filename = InputBox("Please enter a file name:")
            Dim stat As Boolean = True

            If filename = Nothing Then
                Exit Sub
            End If

            For Each c In Path.GetInvalidFileNameChars()
                If filename.Contains(c) Then
                    stat = False
                End If
            Next

            If stat = False Then
                MsgBox("File name contains invalid characters.")
            Else
                Dim path As String = FolderPath & "\" & filename & ext

                If File.Exists(path) Then
                    MsgBox(filename & ext & " already exists in this directory. Overwrite it?")
                    If MsgBoxResult.Yes Then
                        WriteToFile(path, data, ext)
                        Exit Sub
                    End If
                Else
                    WriteToFile(path, data, ext)
                    Exit Sub
                End If
            End If
        Loop
    End Sub

    Private Sub WriteToFile(ByVal path As String, ByVal data As Object, ByVal type As String)
        'Dim isAdmin As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)

        'Right now the code doesn't support writing files to folders where you need to be an admin. I don't know
        'how to implement that, and it's giving me such a fucking headache.

        'Parse file type passed by the sub
        Select Case type
            Case ".txt"
                Try
                    File.WriteAllText(path, data)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case ".bmp"
                Try
                    File.Delete(path)
                    data.Save(path, Drawing.Imaging.ImageFormat.Bmp)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case ".jpg"
                Try
                    File.Delete(path)
                    data.Save(path, Drawing.Imaging.ImageFormat.Jpeg)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case ".png"
                Try
                    File.Delete(path)
                    data.Save(path, Drawing.Imaging.ImageFormat.Png)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
        End Select
    End Sub
End Class


