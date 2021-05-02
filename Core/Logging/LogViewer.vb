' Copyright (C) 2007-2011 Thies Gerken

' This file is part of ScreenshotStudio.Net.

' ScreenshotStudio.Net is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.

' ScreenshotStudio.Net is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.

' You should have received a copy of the GNU General Public License
' along with ScreenshotStudio.Net. If not, see <http://www.gnu.org/licenses/>.

Imports System.Resources
Imports System.Reflection
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports ScreenshotStudioDotNet.Core.Email
Imports ScreenshotStudioDotNet.Core.Settings

Namespace Logging
    Public Class LogViewer

#Region "Fields"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the LogViewer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub LogViewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            UpdateList()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnClose control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Resize event of the LogViewer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub LogViewer_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            listLogEntries.Height = Me.ClientRectangle.Height - 12 - btnClose.Height
            btnClose.Location = New Point(Me.ClientRectangle.Width - btnClose.Width - 6, Me.ClientRectangle.Height - btnClose.Height - 6)
            btnExport.Location = New Point(btnClose.Left - btnExport.Width - 6, btnClose.Top)
            btnSupport.Location = New Point(btnExport.Left - btnSupport.Width - 6, btnClose.Top)
            btnDelete.Location = New Point(btnSupport.Left - btnDelete.Width - 6, btnClose.Top)

            Dim msgWidth = listLogEntries.Width - colDate.Width - colType.Width - 25
            If msgWidth > colDate.Width Then colMessage.Width = msgWidth
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnSupport control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnSupport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSupport.Click
            Dim message As New MapiMailMessage("ScreenshotStudio.Net Log File", "[Please add some useful information, you can write on either German or English.]")
            message.Files.Add(Path.Combine(StaticProperties.SettingsDirectory, "Log.xml"))
            message.Recipients.Add(New Recipient("support@thiesgerken.de", "ScreenshotStudio.Net Support"))
            message.ShowDialog()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnExport control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
            Dim o As New SaveFileDialog
            o.AddExtension = True
            o.DefaultExt = "xml"
            o.FileName = "Log.xml"
            o.Filter = ".xml|XML Files"
            o.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            o.Title = "Save Log File"

            If o.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim fileName = o.FileName
                If File.Exists(fileName) Then File.Delete(fileName)
                File.Copy(Path.Combine(StaticProperties.SettingsDirectory, "Log.xml"), fileName)
            End If
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnDelete control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
            Dim result As TaskDialogResult
            If SettingsDatabase.AskForLogClean Then
                Dim tdlg As New TaskDialog()
                tdlg.Caption = "ScreenshotStudio.Net"
                tdlg.InstructionText = _langManager.GetString("DeleteLogQInstruction")
                tdlg.Text = _langManager.GetString("DeleteLogQText")
                tdlg.Icon = TaskDialogStandardIcon.Warning
                tdlg.StandardButtons = TaskDialogStandardButtons.Yes Or TaskDialogStandardButtons.No
                tdlg.FooterCheckBoxText = _langManager.GetString("DontAskAgain")
                tdlg.FooterCheckBoxChecked = False

                result = tdlg.Show()

                SettingsDatabase.AskForLogClean = Not CBool(tdlg.FooterCheckBoxChecked)
            Else
                result = TaskDialogResult.Yes
            End If

            If result = TaskDialogResult.Yes Then
                File.Delete(Path.Combine(StaticProperties.SettingsDirectory, "Log.xml"))
                Log.Load()
                Log.LogInformation("Log Deleted by User")
                UpdateList()
            End If
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Updates the list.
        ''' </summary>
        Private Sub UpdateList()
            listLogEntries.Items.Clear()

            Dim entries = Log.LogEntries

            For Each l In entries
                listLogEntries.Items.Add(New ListViewItem(New String() {l.TimeCreated.ToString, l.Type, l.LogMessage}))
            Next
        End Sub

#End Region
    End Class
End Namespace
