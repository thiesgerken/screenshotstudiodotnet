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

Namespace Imageshack
    Public Class ImageshackArgumentDesigner

        Private _loading As Boolean = True

        ''' <summary>
        ''' Handles the Load event of the ArgumentDesigner control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub ArgumentDesigner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            chkShort.Checked = Me.Result.ContainsKey("Bitly")
            chkHide.Checked = Me.Result.ContainsKey("Hide")
            chkAppend.Checked = Me.Result.ContainsKey("Append")

            optCustom.Checked = Me.Result.ContainsKey("Format")
            If optCustom.Checked Then
                cboxFileFormat.SelectedItem = Me.Result("Format")
            Else
                optFormatAsGlobal.Checked = True
                cboxFileFormat.SelectedIndex = 0
            End If

            If Me.Result.ContainsKey("Action") Then
                cboxLinkAction.SelectedItem = Me.Result("Action")
            Else
                cboxLinkAction.SelectedIndex = 0 'No Action 
            End If

            If Me.Result.ContainsKey("Filename") Then
                txtFilename.Text = CStr(Me.Result("Filename"))
            End If

            _loading = False
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the chkShort control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub chkShort_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShort.CheckedChanged
            CheckPropertyChanges()
        End Sub

        ''' <summary>
        ''' Checks the property changes.
        ''' </summary>
        Private Sub CheckPropertyChanges()
            If _loading Then Exit Sub

            Dim changed As Boolean = False

            changed = changed Or Not chkShort.Checked = Me.Result.ContainsKey("Bitly")
            changed = changed Or Not chkHide.Checked = Me.Result.ContainsKey("Hide")
            changed = changed Or Not chkAppend.Checked = Me.Result.ContainsKey("Append")
            changed = changed Or Not optCustom.Checked = Me.Result.ContainsKey("Format")

            If Me.Result.ContainsKey("Format") Then
                changed = changed Or Not cboxFileFormat.SelectedItem.ToString = Me.Result("Format").ToString
            End If

            If Me.Result.ContainsKey("Action") Then
                changed = changed Or Not cboxLinkAction.SelectedItem.ToString = Me.Result("Action").ToString
            Else
                changed = changed Or Not cboxLinkAction.SelectedIndex = 0
            End If

            If Me.Result.ContainsKey("Filename") Then
                changed = changed Or Not txtFilename.Text = CStr(Me.Result("Filename"))
            Else
                changed = changed Or Not txtFilename.Text = ""
            End If

            Me.PropertiesChanged = changed
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Overrides Sub Save()
            Me.Result.Clear()

            If chkShort.Checked Then Me.Result.Add("Bitly", "")
            If chkHide.Checked Then Me.Result.Add("Hide", "")
            If chkAppend.Checked Then Me.Result.Add("Append", "")
            If optCustom.Checked Then Me.Result.Add("Format", cboxFileFormat.SelectedItem.ToString)
       
            If Not cboxLinkAction.SelectedIndex = 0 Then
                Me.Result.Add("Action", cboxLinkAction.SelectedItem.ToString)
            End If

            If Not txtFilename.Text = "" Then
                Me.Result.Add("Filename", txtFilename.Text)
            End If
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the optFormatAsGlobal control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub optFormatAsGlobal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormatAsGlobal.CheckedChanged
            cboxFileFormat.Enabled = optCustom.Checked
            CheckPropertyChanges()
        End Sub

        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the cboxLinkAction control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub cboxLinkAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxLinkAction.SelectedIndexChanged
            btnSelectFile.Enabled = cboxLinkAction.SelectedIndex = 1  '1 = Save
            CheckPropertyChanges()
        End Sub

        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the cboxFileFormat control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub cboxFileFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxFileFormat.SelectedIndexChanged
            CheckPropertyChanges()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnSelectFile control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnSelectFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFile.Click
            Try
                If IO.Directory.Exists(IO.Path.GetDirectoryName(txtFilename.Text)) Then
                    FilenamePicker.InitialDirectory = IO.Path.GetDirectoryName(txtFilename.Text)
                    FilenamePicker.FileName = IO.Path.GetFileName(txtFilename.Text)
                End If
            Catch ex As ArgumentException
                FilenamePicker.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                FilenamePicker.FileName = ""
            End Try

            Dim result = FilenamePicker.ShowDialog
            If result = Windows.Forms.DialogResult.OK Then
                txtFilename.Text = FilenamePicker.FileName

                CheckPropertyChanges()
            End If
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the chkAppend control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub chkAppend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAppend.CheckedChanged
            CheckPropertyChanges()
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the chkHide control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub chkHide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHide.CheckedChanged
            CheckPropertyChanges()
        End Sub
    End Class
End Namespace
