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
Imports System.Drawing.Drawing2D
Imports ScreenshotStudioDotNet.Core.Misc
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Aero
Imports ScreenshotStudioDotNet.Misc
Imports ScreenshotStudioDotNet.Core.Settings
Imports Microsoft.WindowsAPICodePack.Taskbar
Imports System.Threading
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Forms
    Public Class QuickStart

#Region "Fields"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Strings", Assembly.GetExecutingAssembly)

        Public Property ButtonWidth As Integer = 120
        Public Property ButtonHeight As Integer = 60
        Public Property ButtonMargin As Integer = 9
        Private _macroHovered As String
        Private _mouseDown As Boolean

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            'Set Styles
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

            'Me.ShowTitleBar = False

            Me.Visible = False
            Me.Opacity = SettingsDatabase.Opacity / 100
            Me.TopMost = True
            Me.ActionIfDwmDisabled = DwmDisabledActions.NoAction
            Me.GlassMargins = New Margins(-1, -1, -1, -1)
        End Sub


        ''' <summary>
        ''' Handles the KeyPress event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Me.KeyPress
            If Asc(e.KeyChar) = Keys.Escape Then
                Me.Close()
            ElseIf Asc(e.KeyChar) = Keys.Enter Then
                If _macroHovered <> "" Then
                    MainForm.Creator.CreateScreenshotAsync(MainForm.MacroDatabase(_macroHovered))
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the MouseMove event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseMove
            ProcessMouseMove(e.Location)
        End Sub

        ''' <summary>
        ''' Handles the MouseUp event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseUp
            _mouseDown = False

            Dim underground = GetMouseUnderground(e.Location)

            If underground.Type = MouseUndergroundTypes.Macro Then
                MainForm.Creator.CreateScreenshotAsync(MainForm.MacroDatabase(underground.MacroName))
                Me.Close()
            End If

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Handles the MouseLeave event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseLeave
            _mouseDown = False

            If Not SettingsDatabase.KeyboardSelectionEnabled Then
                _macroHovered = ""
            End If

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Handles the MouseDown event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
            _mouseDown = True
            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Refreshes the jumplists.
        ''' </summary>
        Private Sub RefreshJumplists()
            If TaskbarManager.IsPlatformSupported Then
                Log.LogInformation("Refreshing Jump Lists")

                Dim path As String = Assembly.GetExecutingAssembly.Location

                Dim macroLinks As New List(Of JumpListLink)

                For Each mac In MainForm.MacroDatabase.GetMacrosWithTrigger("Quickstart")
                    Dim link As New JumpListLink(path, mac.Name)
                    link.Arguments = "-macro=" & mac.Name

                    macroLinks.Add(link)
                Next

                Try
                    Dim jump = JumpList.CreateJumpList()

                    'Try to add the custom category, and when it fails add it to the normal links
                    Dim cat As New JumpListCustomCategory(_langManager.GetString("macros"))

                    For Each m In macroLinks
                        cat.AddJumpListItems(m)
                    Next

                    jump.AddCustomCategories(cat)

                    Dim showLink As New JumpListLink(path, _langManager.GetString("show") & " Quick Start")
                    showLink.Arguments = "-showQS"

                    jump.AddUserTasks(showLink)

                    jump.Refresh()
                Catch ex As UnauthorizedAccessException
                    Log.LogError("Could not add a custom category to the Jumplist")

                    Dim jump = JumpList.CreateJumpList()

                    For Each m In macroLinks
                        jump.AddUserTasks(m)
                    Next

                    Dim showLink As New JumpListLink(path, _langManager.GetString("show") & " Quick Start")
                    showLink.Arguments = "-showQS"

                    jump.AddUserTasks(showLink)

                    jump.Refresh()
                End Try
            End If
        End Sub


        ''' <summary>
        ''' Handles the Paint event of the QuickStart control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub QuickStart_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            Me.Width = CInt((Me.Width - Me.ClientRectangle.Width) + SettingsDatabase.QuickStartScaleFactor * ((Me.ButtonWidth + Me.ButtonMargin) * MainForm.MacroPositions.GetLength(0) + Me.ButtonMargin))
            Me.Height = CInt((Me.Height - Me.ClientRectangle.Height) + SettingsDatabase.QuickStartScaleFactor * ((Me.ButtonHeight + Me.ButtonMargin) * MainForm.MacroPositions.GetLength(1) + Me.ButtonMargin))

            If Not MainForm.JumplistRefreshed Then
                Dim t As New Thread(AddressOf RefreshJumplists)
                t.Name = "JumpListRefreshThread"
                t.Start()
                MainForm.JumplistRefreshed = True
            End If

            Dim roundRadius As Integer = 20

            e.Graphics.CompositingQuality = CompositingQuality.HighQuality
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear

            For x As Integer = 0 To MainForm.MacroPositions.GetLength(0) - 1
                For y As Integer = 0 To MainForm.MacroPositions.GetLength(1) - 1
                    If MainForm.MacroPositions(x, y) <> "" Then
                        Dim buttonX As Integer = CInt(SettingsDatabase.QuickStartScaleFactor * (x * (_ButtonWidth + _ButtonMargin) + _ButtonMargin))
                        Dim buttonY As Integer = CInt(SettingsDatabase.QuickStartScaleFactor * (y * (_ButtonHeight + _ButtonMargin) + _ButtonMargin))

                        Dim rect As New RectangleF(buttonX, buttonY, CSng(SettingsDatabase.QuickStartScaleFactor * _ButtonWidth), CSng(SettingsDatabase.QuickStartScaleFactor * _ButtonHeight))
                        Dim path As GraphicsPath = GraphicHelpers.GetRoundedRectangle(rect, CSng(roundRadius / 2))

                        Dim buttonColor As Color
                        If MainForm.MacroPositions(x, y) = _macroHovered Then
                            If _mouseDown Then
                                buttonColor = SettingsDatabase.Colorization.GetDownColor
                                'MouseDown (Standard: Yellow)
                            Else
                                buttonColor = SettingsDatabase.Colorization.GetHoveredColor
                                'Hovered (Standard: Green)
                            End If
                        Else
                            buttonColor = SettingsDatabase.Colorization.GetNormalColor
                            'Not Hovered (Standard: Blue)
                        End If

                        e.Graphics.DrawButton(New Point(buttonX, buttonY), New Size(CInt(_ButtonWidth * SettingsDatabase.QuickStartScaleFactor), CInt(_ButtonHeight * SettingsDatabase.QuickStartScaleFactor)), buttonColor, MainForm.MacroPositions(x, y), "Segoe UI", CSng(SettingsDatabase.QuickStartScaleFactor * 16), 7)
                    End If
                Next
            Next
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Processes a command key.
        ''' </summary>
        ''' <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
        ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        ''' <returns>
        ''' true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
        ''' </returns>
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If SettingsDatabase.KeyboardSelectionEnabled Then
                Dim directionX = 0
                Dim directionY = 0

                Select Case keyData
                    Case Keys.Left
                        directionX = -1
                    Case Keys.Right
                        directionX = 1
                    Case Keys.Up
                        directionY = -1
                    Case Keys.Down
                        directionY = +1
                    Case Else
                        Return False
                End Select

                Dim currentPosition As Point

                If _macroHovered = "" Then _macroHovered = MainForm.MacroPositions(0, 0)

                For x As Integer = 0 To MainForm.MacroPositions.GetLength(0) - 1
                    For y As Integer = 0 To MainForm.MacroPositions.GetLength(1) - 1
                        If _macroHovered = MainForm.MacroPositions(x, y) Then
                            currentPosition = New Point(x, y)
                        End If
                    Next
                Next

                Dim newPosition As New Point(currentPosition.X + directionX, currentPosition.Y + directionY)
                If newPosition.X < 0 Then newPosition.X = currentPosition.X
                If newPosition.X >= MainForm.MacroPositions.GetLength(0) Then newPosition.X = currentPosition.X

                If newPosition.Y < 0 Then newPosition.Y = currentPosition.Y
                If newPosition.Y >= MainForm.MacroPositions.GetLength(1) Then newPosition.Y = currentPosition.Y

                _macroHovered = MainForm.MacroPositions(newPosition.X, newPosition.Y)
                Me.Refresh()
                Return True
            Else
                Return False
            End If
        End Function

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            ' if this is a click
            ' ...and it is on the client
            If m.Msg = &H84 And m.Result.ToInt32() = 1 Then
                'Check if the mouse is over the form area
                Dim x As Integer = (m.LParam.ToInt32 << 16) >> 16
                'lo order word
                Dim y As Integer = m.LParam.ToInt32 >> 16
                'hi order word

                Dim point As Point = Me.PointToClient(New Point(x, y))

                If GetMouseUnderground(point).Type = MouseUndergroundTypes.Form Then
                    ' ...and specifically in the glass area
                    ' lie and say they clicked on the title bar
                    m.Result = New IntPtr(2)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Processes the mouse move.
        ''' </summary>
        ''' <param name="mousePosition">The mouse position.</param>
        Private Sub ProcessMouseMove(ByVal mousePosition As Point)
            Dim mouseUnderground = GetMouseUnderground(mousePosition)

            If Not _macroHovered = "" Or Not SettingsDatabase.KeyboardSelectionEnabled Then
                _macroHovered = mouseUnderground.MacroName
            End If

            Select Case mouseUnderground.Type
                Case MouseUndergroundTypes.Form
                    Me.Cursor = Cursors.Arrow
                Case MouseUndergroundTypes.Macro
                    Me.Cursor = Cursors.Hand
            End Select

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Gets the mouse underground.
        ''' </summary>
        ''' <param name="mouseLocation">The mouse location.</param>
        ''' <returns></returns>
        Public Function GetMouseUnderground(ByVal mouseLocation As Point) As MouseUnderground
            For x As Integer = 0 To MainForm.MacroPositions.GetLength(0) - 1
                For y As Integer = 0 To MainForm.MacroPositions.GetLength(1) - 1
                    Dim buttonX As Integer = CInt(SettingsDatabase.QuickStartScaleFactor * (x * (_ButtonWidth + _ButtonMargin) + _ButtonMargin))
                    Dim buttonY As Integer = CInt(SettingsDatabase.QuickStartScaleFactor * (y * (_ButtonHeight + _ButtonMargin) + _ButtonMargin))

                    If New RectangleF(buttonX, buttonY, CSng(_ButtonWidth * SettingsDatabase.QuickStartScaleFactor), CSng(_ButtonHeight * SettingsDatabase.QuickStartScaleFactor)).Contains(mouseLocation) Then
                        If MainForm.MacroPositions(x, y) <> "" Then Return New MouseUnderground(MouseUndergroundTypes.Macro, MainForm.MacroPositions(x, y))
                    End If
                Next
            Next

            Return New MouseUnderground(MouseUndergroundTypes.Form)
        End Function

        ''' <summary>
        ''' Closes the thread safe.
        ''' </summary>
        Public Sub CloseThreadSafe()
            If Me.InvokeRequired Then
                Me.Invoke(New Action(AddressOf CloseThreadSafe))
            Else
                Me.Close()
            End If
        End Sub

#End Region
    End Class
End Namespace
