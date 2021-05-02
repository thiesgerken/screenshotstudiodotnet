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
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.ScreenshotTypes.FreeHand
Imports ScreenshotStudioDotNet.ScreenshotTypes.Region
Imports ScreenshotStudioDotNet.ScreenshotTypes.Window
Imports ScreenshotStudioDotNet.Core.Settings
Imports System.Text
Imports System.Threading
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Selector
    Public Class SelectRegion

#Region "Fields"

#Region "General"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.ScreenshotTypes.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Required SettingDatabases"

        Private _regionSettings As New RegionSettings
        Private _windowSettings As New WindowSettings
        Private _freehandSettings As New FreeHandSettings

#End Region

#Region "Region specific"

        Private _regionMode As RegionChangeMode
        Private _regionShape As Shape
        Private _lastCursor As Cursor

        Private _regionStart As Point
        Private _regionEnd As Point

        Private _moveStart As Point
        Private _moveEnd As Point

        Private _resizeStart As Point
        Private _resizeEnd As Point

        Private _triangleInvert As Boolean

#End Region

#Region "Window specific"

        Private _selectedWindows As New List(Of IntPtr)
        Private _hoveredWindow As IntPtr
        Private _windows As New Dictionary(Of IntPtr, Rectangle)

#End Region

#Region "FreeHand specific"

        Private _freehandPath As New GraphicsPath
        Private _freehandMoveStart As Point
        Private _freehandMoveEnd As Point
        Private _freehandMode As FreeHandChangeMode

#End Region

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the macro.
        ''' </summary>
        ''' <value>The macro.</value>
        Public Property Macro() As Macro

        ''' <summary>
        ''' Gets or sets the screenshot.
        ''' </summary>
        ''' <value>The screenshot.</value>
        Public Property Screenshot() As Screenshot

        ''' <summary>
        ''' Gets or sets the fullscreen.
        ''' </summary>
        ''' <value>The fullscreen.</value>
        Public Property Fullscreen() As Bitmap

        ''' <summary>
        ''' Gets or sets the mode.
        ''' </summary>
        ''' <value>The mode.</value>
        Public Property Mode() As RegionTypes

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the Region control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub Region_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Me.Visible = False
            Me.TopMost = True
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Bounds = ScreenHelpers.GetFittingRectangle(SettingsDatabase.Screens)
            Me.ShowIcon = False
            Me.ShowInTaskbar = True

            picRegion.Image = _Fullscreen

            Dim _macroHandle As IntPtr = IntPtr.Zero
            Dim _macroApplicationName As String = ""
            Dim _macroTitle As String = ""
            Dim _macroShape As Shape
            Dim _macroRect As Rectangle

            'Retrieve the optional parameters
            _macroShape = CType(Macro.Type.GetParameter("Shape"), Shape)
            _macroRect = CType(Macro.Type.GetParameter("Rectangle"), Rectangle)
            _macroTitle = CType(Macro.Type.GetParameter("WindowTitle"), String)
            _macroHandle = CType(Macro.Type.GetParameter("WindowHandle"), IntPtr)
            _macroApplicationName = CType(Macro.Type.GetParameter("ApplicationName"), String)
              

            'Test optional parameters
            If Not _macroShape.Equals(Nothing) And _Mode = RegionTypes.Region Then
                _regionShape = _macroShape
            Else
                _regionShape = Shape.Rectangle
            End If

            If Not _macroRect.Equals(Nothing) And _Mode = RegionTypes.Region Then
                _regionStart = _macroRect.Location
                _regionEnd = New Point(_macroRect.Right, _macroRect.Bottom)
            End If

            If Not _macroHandle = Nothing And _Mode = RegionTypes.Window Then
                If GetWindowRectangle(_macroHandle).Width > 0 Then
                    _selectedWindows.Add(_macroHandle)
                End If
            End If

            If _Mode = RegionTypes.Window Then
                NativeMethods.EnumWindows(New NativeMethods.EnumWindowsProc(AddressOf EnumWindow), 0)
            End If

            If Not _macroTitle = "" And _Mode = RegionTypes.Window Then
                For Each win In _windows
                    If GetWindowText(win.Key) = _macroTitle Then
                        _selectedWindows.Add(win.Key)

                        Exit For
                    End If
                Next
            End If

            If Not _macroApplicationName = "" And _Mode = RegionTypes.Window Then
                Dim prc() = Process.GetProcessesByName(_macroApplicationName)

                For Each p As Process In prc
                    If _windows.ContainsKey(p.MainWindowHandle) Then
                        _selectedWindows.Add(p.MainWindowHandle)

                        Exit For
                    End If

                Next
            End If

            Me.Visible = True
            Me.Focus()
        End Sub

        ''' <summary>
        ''' Handles the MouseDoubleClick event of the picRegion control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub picRegion_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picRegion.MouseDoubleClick
            If e.Button = System.Windows.Forms.MouseButtons.Left Then CaptureRegion()
        End Sub


        ''' <summary>
        ''' Handles the MouseDown event of the picRegion control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub picRegion_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picRegion.MouseDown
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Select Case _Mode
                    Case RegionTypes.Region
                        If _regionMode = RegionChangeMode.Create Then
                            _regionStart = e.Location
                            _regionEnd = e.Location
                        ElseIf _regionMode = RegionChangeMode.Move Then
                            _moveStart = e.Location
                            _moveEnd = e.Location
                        ElseIf _regionMode.ToString.StartsWith("Resize") Then
                            _resizeStart = e.Location
                            _resizeEnd = e.Location
                        End If
                    Case RegionTypes.Freehand
                        If _freehandMode = FreeHandChangeMode.Move Then
                            _freehandMoveStart = e.Location
                            _freehandMoveEnd = e.Location
                        Else
                            _freehandPath = New GraphicsPath
                        End If
                End Select

                Me.Refresh()
            End If
        End Sub

        ''' <summary>
        ''' Processes a command key.
        ''' </summary>
        ''' <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
        ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        ''' <returns>
        ''' true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
        ''' </returns>
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            Return ProcessKey(keyData)
        End Function

        ''' <summary>
        ''' Handles the MouseMove event of the picRegion control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub picRegion_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picRegion.MouseMove
            Select Case _Mode
                Case RegionTypes.Region
                    'Change mode if mouse is not pressed
                    If e.Button = System.Windows.Forms.MouseButtons.None Then
                        _regionMode = GetRegionMode(e.Location)
                        _lastCursor = Nothing
                    End If

                    'Set the cursor depending on the mode
                    If _regionMode = RegionChangeMode.Create Then
                        picRegion.Cursor = Cursors.Cross
                    ElseIf _regionMode = RegionChangeMode.Move Then
                        picRegion.Cursor = Cursors.SizeAll
                    ElseIf _regionMode = RegionChangeMode.ResizeW Or _regionMode = RegionChangeMode.ResizeE Then
                        picRegion.Cursor = Cursors.SizeWE
                    ElseIf _regionMode = RegionChangeMode.ResizeN Or _regionMode = RegionChangeMode.ResizeS Then
                        picRegion.Cursor = Cursors.SizeNS
                    ElseIf _regionMode = RegionChangeMode.ResizeNW Or _regionMode = RegionChangeMode.ResizeSE Then
                        picRegion.Cursor = Cursors.SizeNWSE
                    ElseIf _regionMode = RegionChangeMode.ResizeNE Or _regionMode = RegionChangeMode.ResizeSW Then
                        picRegion.Cursor = Cursors.SizeNESW
                    End If

                    'Be sure that the right resize cursor is used
                    Dim cursorMode As RegionChangeMode = RegionChangeMode.Create
                    If _regionMode.ToString.StartsWith("Resize") Then
                        If GetRegionMode(e.Location).ToString.StartsWith("Resize") And _regionMode.ToString.Length = GetRegionMode(e.Location).ToString.Length Then
                            cursorMode = GetRegionMode(e.Location)
                        End If
                    End If

                    If cursorMode = RegionChangeMode.ResizeNW Or cursorMode = RegionChangeMode.ResizeSE Then
                        picRegion.Cursor = Cursors.SizeNWSE
                        _lastCursor = picRegion.Cursor
                    ElseIf cursorMode = RegionChangeMode.ResizeNE Or cursorMode = RegionChangeMode.ResizeSW Then
                        picRegion.Cursor = Cursors.SizeNESW
                        _lastCursor = picRegion.Cursor
                    Else
                        If Not _lastCursor Is Nothing Then
                            picRegion.Cursor = _lastCursor
                        End If
                    End If

                    'set the end points
                    If e.Button = System.Windows.Forms.MouseButtons.Left Then
                        If _regionMode = RegionChangeMode.Create Then
                            _regionEnd = e.Location
                        ElseIf _regionMode = RegionChangeMode.Move Then
                            _moveEnd = e.Location
                        ElseIf _regionMode.ToString.StartsWith("Resize") Then
                            _resizeEnd = e.Location
                        End If
                    End If
                Case RegionTypes.Freehand
                    If e.Button = System.Windows.Forms.MouseButtons.None Then
                        If _freehandPath.GetBounds.Contains(e.Location) Then
                            _freehandMode = FreeHandChangeMode.Move
                            picRegion.Cursor = Cursors.SizeAll
                        Else
                            _freehandMode = FreeHandChangeMode.Create
                            picRegion.Cursor = Cursors.Cross
                        End If
                    End If

                    If e.Button = System.Windows.Forms.MouseButtons.Left Then
                        If _freehandMode = FreeHandChangeMode.Create Then
                            Dim lastPoint As PointF

                            If _freehandPath.PointCount > 0 Then
                                lastPoint = _freehandPath.GetLastPoint
                            Else
                                lastPoint = e.Location
                            End If

                            _freehandPath.AddLine(lastPoint, e.Location)
                        Else
                            _freehandMoveEnd = e.Location
                        End If
                    Else

                    End If
                Case RegionTypes.Window
                    Me.Cursor = Cursors.Hand

                    'Find out what window is hovered by the mouse
                    For Each win In _windows
                        If win.Value.Contains(e.Location) Then
                            'Enum Windows returns Windows in Z-Order
                            _hoveredWindow = win.Key
                            Exit For
                        End If
                    Next

            End Select

            'refresh the window
            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Handles the MouseUp event of the picRegion control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub picRegion_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picRegion.MouseUp
            Select Case _Mode
                Case RegionTypes.Region
                    Dim band = GetRubberBandRectangle()

                    _regionStart = New Point(band.Left, band.Top)
                    _regionEnd = New Point(band.Right, band.Bottom)

                    'reset the points
                    _moveStart = New Point(0, 0)
                    _moveEnd = New Point(0, 0)

                    _resizeStart = New Point(0, 0)
                    _resizeEnd = New Point(0, 0)
                Case RegionTypes.Freehand
                    _freehandPath = GetPath()

                    _freehandMoveStart = New Point(0, 0)
                    _freehandMoveEnd = New Point(0, 0)

                Case RegionTypes.Window
                    If e.Button = System.Windows.Forms.MouseButtons.Left Then
                        If Not My.Computer.Keyboard.CtrlKeyDown Then
                            _selectedWindows.Clear()
                        End If

                        _selectedWindows.Add(_hoveredWindow)
                        Me.Refresh()

                        If Not My.Computer.Keyboard.CtrlKeyDown Then
                            CaptureRegion()
                        End If

                    End If
            End Select
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the picRegion control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub picRegion_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles picRegion.Paint
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear

            If _regionStart = New Point(1, 1) And _regionEnd = New Point(2, 2) Then Exit Sub

            Dim alpha As Integer = 145

            Dim lightBrush As SolidBrush = New SolidBrush(Color.FromArgb(alpha, Color.LightCyan))
            Dim darkBrush As SolidBrush = New SolidBrush(Color.FromArgb(alpha + 10, Color.DarkGreen))
            Dim darkPen As New Pen(Color.FromArgb(alpha + 10, Color.DarkCyan), 1)

            'x and y lines for better orientation
            If (Mode = RegionTypes.Region And _regionSettings.ShowMagnifyingGlass) Or (Mode = RegionTypes.Freehand And _freehandSettings.ShowMagnifyingGlass) Then
                If (Mode = RegionTypes.Region And _regionMode = RegionChangeMode.Create) Or (Mode = RegionTypes.Freehand And _freehandMode = FreeHandChangeMode.Create) Then
                    e.Graphics.DrawLine(darkPen, Cursor.Position.X, 0, Cursor.Position.X, picRegion.Bottom)
                    e.Graphics.DrawLine(darkPen, 0, Cursor.Position.Y, picRegion.Right, Cursor.Position.Y)
                End If

                'magnifying glass
                If (_regionMode = RegionChangeMode.Create And Mode = RegionTypes.Region And _regionSettings.ShowMagnifyingGlass) Or (_freehandMode = FreeHandChangeMode.Create And Mode = RegionTypes.Freehand And _freehandSettings.ShowMagnifyingGlass) Then
                    e.Graphics.DrawImage(_Fullscreen, New Rectangle(Cursor.Position.X + 3, Cursor.Position.Y + 3, 50, 50), New Rectangle(Cursor.Position.X - 10, Cursor.Position.Y - 10, 20, 20), GraphicsUnit.Pixel)
                    e.Graphics.DrawRectangle(darkPen, New Rectangle(Cursor.Position.X + 3, Cursor.Position.Y + 3, 50, 50))
                    e.Graphics.DrawLine(darkPen, Cursor.Position.X + 28, Cursor.Position.Y + 4, Cursor.Position.X + 28, Cursor.Position.Y + 52)
                    e.Graphics.DrawLine(darkPen, Cursor.Position.X + 4, Cursor.Position.Y + 28, Cursor.Position.X + 52, Cursor.Position.Y + 28)
                End If
            End If

            If _Mode = RegionTypes.Region Then
                Dim bandRectangle As Rectangle = GetRubberBandRectangle()

                'Draw + Fill Region
                If _regionShape = Shape.Rectangle Then
                    e.Graphics.FillRectangle(lightBrush, bandRectangle)
                    e.Graphics.DrawRectangle(darkPen, bandRectangle)
                ElseIf _regionShape = Shape.Ellipse Then
                    e.Graphics.FillEllipse(lightBrush, bandRectangle)
                    e.Graphics.DrawEllipse(darkPen, bandRectangle)
                End If

                'paint the x- and y-axis
                PaintAxes(e, bandRectangle, darkPen, darkBrush, IntPtr.Zero)
            ElseIf _Mode = RegionTypes.Window Then
                For Each win In _selectedWindows
                    Dim bandRectangle = _windows(win)

                    Dim hoverPen As New Pen(Color.FromArgb(alpha + 20, 255, 140, 0))
                    Dim hoverBrush As New SolidBrush(Color.FromArgb(alpha + 20, 255, 165, 0))
                    Dim hoverBrushLight As SolidBrush = New SolidBrush(Color.FromArgb(alpha - 50, 255, 170, 0))

                    'Draw + Fill Region
                    e.Graphics.FillRectangle(hoverBrushLight, bandRectangle)
                    e.Graphics.DrawRectangle(hoverPen, bandRectangle)

                    'paint the x- and y-axis
                    PaintAxes(e, bandRectangle, hoverPen, hoverBrush, win)
                Next

                If Not _selectedWindows.Contains(_hoveredWindow) Then
                    Dim bandRectangle = _windows(_hoveredWindow)

                    'Draw + Fill Region
                    e.Graphics.FillRectangle(lightBrush, bandRectangle)
                    e.Graphics.DrawRectangle(darkPen, bandRectangle)

                    'paint the x- and y-axis
                    PaintAxes(e, bandRectangle, darkPen, darkBrush, _hoveredWindow)
                End If

            ElseIf _Mode = RegionTypes.Freehand Then
                'draw the path
                Dim path = GetPath()

                e.Graphics.FillPath(lightBrush, path)
                e.Graphics.DrawPath(darkPen, path)

                'draw the axes
                If _freehandPath.PointCount > 0 Then
                    PaintAxes(e, GetRubberBandRectangle, darkPen, darkBrush, IntPtr.Zero)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the Opening event of the mnuContext control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
        Private Sub mnuContext_Opening(ByVal sender As Object, ByVal e As CancelEventArgs) Handles mnuContext.Opening
            ManipulateMenuItems()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuCapture control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuCapture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuCapture.Click
            CaptureRegion()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuCancel.Click
            _Screenshot = Nothing
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuRotate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuRotate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuRotate.Click
            Rotate()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuSwitchShape control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuSwitchShape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuSwitchShape.Click
            SwitchShape()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuResize150 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuResize150_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuResize150.Click
            ResizeRegion(1.5)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuResize50 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuResize50_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuResize50.Click
            ResizeRegion(0.5)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuResize200 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuResize125_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuResize125.Click
            ResizeRegion(1.25)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuResize75 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuResize75_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuResize75.Click
            ResizeRegion(0.75)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuResize control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuResize_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuResize.Click
            ShowResizeInput()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuMove control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuMove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMove.Click
            ShowMoveInput()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuNewSelection control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuNewSelection_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuNewSelection.Click
            ShowSelectInput()
        End Sub

        Friend Declare Function SetForegroundWindow Lib "user32" Alias "SetForegroundWindow" (ByVal hWnd As Long) As Boolean

        ''' <summary>
        ''' Handles the Shown event of the SelectRegion control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub SelectRegion_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
            Me.Focus()
            Me.BringToFront()
            Me.Show()
            SetForegroundWindow(Me.Handle.ToInt64)
        End Sub

#End Region

#Region "Window Functions"

        ''' <summary>
        ''' Enums the windows.
        ''' </summary>
        ''' <param name="hwnd">The HWND.</param>
        ''' <param name="lparam">The lparam.</param>
        ''' <returns></returns>
        Private Function EnumWindow(ByVal hwnd As Integer, ByVal lparam As Integer) As Boolean

            Dim rect = GetWindowRectangle(New IntPtr(hwnd))
            If rect.Width > 0 And NativeMethods.IsWindowVisible(hwnd) And Not Me.Handle = New IntPtr(hwnd) Then
                If Not _windows.ContainsKey(New IntPtr(hwnd)) Then _windows.Add(New IntPtr(hwnd), rect)
            End If

            Return True
        End Function

        ''' <summary>
        ''' Enums the child proc.
        ''' </summary>
        ''' <param name="hWnd">The h WND.</param>
        ''' <param name="lParam">The l param.</param>
        ''' <returns></returns>
        Function EnumChilds(ByVal hWnd As Long, ByVal lParam As Long) As Boolean
            Dim rect = GetWindowRectangle(New IntPtr(hWnd))
            If rect.Width > 0 And NativeMethods.IsWindowVisible(hWnd) And Not Me.Handle = New IntPtr(hWnd) Then
                If Not _windows.ContainsKey(New IntPtr(hWnd)) Then _windows.Add(New IntPtr(hWnd), rect)
            End If

            Return True
        End Function

        ''' <summary>
        ''' Gets the window text.
        ''' </summary>
        ''' <param name="window">The window.</param>
        ''' <returns></returns>
        Public Function GetWindowText(ByVal window As IntPtr) As String
            Dim bufferLength As Long, buffer As String

            'get length 
            bufferLength = NativeMethods.GetWindowTextLength(window.ToInt64) + 1
            buffer = Space(CInt(bufferLength))

            'read text
            bufferLength = NativeMethods.GetWindowText(window.ToInt64, buffer, bufferLength)

            Return buffer.Substring(0, buffer.Length - 1)
        End Function

        ''' <summary>
        ''' Gets the window process.
        ''' </summary>
        ''' <param name="window">The window.</param>
        ''' <returns></returns>
        Public Function GetWindowProcess(ByVal window As IntPtr) As String
            Dim processID As Integer
            NativeMethods.GetWindowThreadProcessId(window.ToInt64, processID)

            Return Process.GetProcessById(processID).ProcessName
        End Function

        ''' <summary>
        ''' Gets the window rectangle.
        ''' </summary>
        ''' <param name="window">The window.</param>
        ''' <returns></returns>
        Public Shared Function GetWindowRectangle(ByVal window As IntPtr) As Rectangle
            Dim rect As NativeMethods.RECT

            NativeMethods.GetWindowRect(window, rect)

            Return New Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top)
        End Function

#End Region

#Region "General Functions"

        ''' <summary>
        ''' Rotates this instance.
        ''' </summary>
        Private Sub Rotate()
            'swap the width and height
            Dim rectOriginal = GetRubberBandRectangle()
            Dim rectNew As New Rectangle(CInt(rectOriginal.X + rectOriginal.Width / 2 - rectOriginal.Height / 2), CInt(rectOriginal.Y + rectOriginal.Height / 2 - rectOriginal.Width / 2), rectOriginal.Height, rectOriginal.Width)

            _regionStart = rectNew.Location
            _regionEnd = New Point(rectNew.Right, rectNew.Bottom)

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Processes the key.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <returns></returns>
        Private Function ProcessKey(ByVal key As Keys) As Boolean
            Select Case key
                Case Keys.Escape
                    Me.Close()
                Case Keys.Enter
                    If GetRubberBandRectangle.Height > 0 And GetRubberBandRectangle.Width > 0 Then
                        CaptureRegion()
                    ElseIf _Mode = RegionTypes.Region Then
                        ShowSelectInput()
                    End If
                Case Keys.F9
                    If _Mode = RegionTypes.Region Then SwitchShape()
                Case Keys.F10
                    If _Mode = RegionTypes.Region Then ShowMoveInput()
                Case Keys.F11
                    If _Mode = RegionTypes.Region Then ShowResizeInput()
                Case Keys.F12
                    If _Mode = RegionTypes.Region Then Rotate()
                Case Keys.D1
                    If _Mode = RegionTypes.Region Then ResizeRegion(0.5)
                Case Keys.D2
                    If _Mode = RegionTypes.Region Then ResizeRegion(0.75)
                Case Keys.D3
                    If _Mode = RegionTypes.Region Then ResizeRegion(1.25)
                Case Keys.D4
                    If _Mode = RegionTypes.Region Then ResizeRegion(1.5)
                Case Else
                    Return False
            End Select

            Return True
        End Function

        ''' <summary>
        ''' Updates the function.
        ''' </summary>
        ''' <param name="startPoint">The start point.</param>
        ''' <param name="endPoint">The end point.</param>
        Private Sub UpdateFunction(ByVal startPoint As Point, ByVal endPoint As Point)
            _regionStart = startPoint
            _regionEnd = endPoint

            Dim r = GetRubberBandRectangle()

            _regionStart = r.Location
            _regionEnd = New Point(r.Right, r.Bottom)

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Shows the select input.
        ''' </summary>
        Private Sub ShowSelectInput()
            Dim i As New Input
            i.ShowDialog(_langManager.GetString("newSelection"), Icon.FromHandle(My.Resources.NewSelection.GetHicon), True, True, New Input.UpdateRegionPropertiesDelegate(AddressOf UpdateFunction), New Rectangle(1, 1, 1, 1), ScreenHelpers.GetFittingRectangle(SettingsDatabase.Screens), False)
        End Sub

        ''' <summary>
        ''' Shows the move input.
        ''' </summary>
        Private Sub ShowMoveInput()
            Dim i As New Input
            i.ShowDialog(_langManager.GetString("moveSelection"), Icon.FromHandle(My.Resources.Move.GetHicon), False, True, New Input.UpdateRegionPropertiesDelegate(AddressOf UpdateFunction), GetRubberBandRectangle, ScreenHelpers.GetFittingRectangle(SettingsDatabase.Screens), True)
        End Sub

        ''' <summary>
        ''' Shows the resize input.
        ''' </summary>
        Private Sub ShowResizeInput()
            Dim i As New Input
            i.ShowDialog(_langManager.GetString("resizeSelection"), Icon.FromHandle(My.Resources.Resize.GetHicon), True, False, New Input.UpdateRegionPropertiesDelegate(AddressOf UpdateFunction), GetRubberBandRectangle, ScreenHelpers.GetFittingRectangle(SettingsDatabase.Screens), True)
        End Sub

        ''' <summary>
        ''' Switches the shape.
        ''' </summary>
        Private Sub SwitchShape()
            If _regionShape = Shape.Ellipse Then
                _regionShape = Shape.Rectangle
            Else
                _regionShape = Shape.Ellipse
            End If

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Manipulates the menu items.
        ''' </summary>
        Private Sub ManipulateMenuItems()
            'hide all
            mnuMove.Visible = False
            mnuResize.Visible = False
            mnuRotate.Visible = False
            mnuCapture.Visible = False
            mnuCancel.Visible = False
            mnuNewSelection.Visible = False
            mnuSwitchShape.Visible = (_Mode = RegionTypes.Region)
            ToolStripSeparator1.Visible = False
            ToolStripSeparator2.Visible = False
            mnuNewSelection.ShortcutKeyDisplayString = "Enter"
            mnuSelect.Visible = False

            'show the right items
            Dim hovered As Boolean = GetRubberBandRectangle.Contains(Cursor.Position)

            mnuCancel.Visible = True

            If _Mode = RegionTypes.Window Then
                If Not _selectedWindows.Contains(_hoveredWindow) Then mnuSelect.Visible = True
            End If

            If hovered Then
                mnuCapture.Visible = True

                If _Mode = RegionTypes.Region Then
                    mnuMove.Visible = True
                    mnuResize.Visible = True
                    mnuRotate.Visible = True
                    ToolStripSeparator1.Visible = True
                End If
            Else
                If _Mode = RegionTypes.Region Then
                    mnuNewSelection.Visible = True

                    If GetRubberBandRectangle.Height > 0 And GetRubberBandRectangle.Width > 0 Then
                        mnuNewSelection.ShortcutKeyDisplayString = ""
                    End If

                    ToolStripSeparator1.Visible = True
                    ToolStripSeparator2.Visible = True
                End If
            End If

            If Mode = RegionTypes.Window Then
                mnuCapture.ShortcutKeyDisplayString = _langManager.GetString("enterAndClick")
            Else
                mnuCapture.ShortcutKeyDisplayString = _langManager.GetString("enterAndDoubleClick")
            End If
        End Sub

        ''' <summary>
        ''' Gets the path.
        ''' </summary>
        ''' <returns></returns>
        Private Function GetPath() As GraphicsPath
            Dim tempP As New GraphicsPath

            If _freehandPath.PointCount > 0 Then
                Dim _freehandMoveX = _freehandMoveEnd.X - _freehandMoveStart.X
                Dim _freehandMoveY = _freehandMoveEnd.Y - _freehandMoveStart.Y

                For Each p In _freehandPath.PathPoints
                    Dim lastPoint As PointF

                    Dim pNew = New Point(CInt(p.X + _freehandMoveX), CInt(p.Y + _freehandMoveY))

                    If tempP.PointCount > 0 Then
                        lastPoint = tempP.GetLastPoint
                    Else
                        lastPoint = pNew
                    End If

                    tempP.AddLine(lastPoint, pNew)
                Next
            End If

            Return tempP
        End Function

        ''' <summary>
        ''' Paints the axes.
        ''' </summary>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        ''' <param name="bandRectangle">The band rectangle.</param>
        ''' <param name="darkPen">The dark pen.</param>
        ''' <param name="darkBrush">The dark brush.</param>
        Private Sub PaintAxes(ByVal e As PaintEventArgs, ByVal bandRectangle As RectangleF, ByVal darkPen As Pen, ByVal darkBrush As SolidBrush, ByVal windowHandle As IntPtr)
            If bandRectangle.Width > 14 Then
                'X Axis
                e.Graphics.DrawLine(darkPen, bandRectangle.Left + 1, bandRectangle.Top - 10, bandRectangle.Right - 1, bandRectangle.Top - 10)
                e.Graphics.DrawLine(darkPen, bandRectangle.Left, bandRectangle.Top - 14, bandRectangle.Left, bandRectangle.Top - 6)
                e.Graphics.DrawLine(darkPen, bandRectangle.Right, bandRectangle.Top - 14, bandRectangle.Right, bandRectangle.Top - 6)

                'Width 
                Dim fontSizeWidth As Integer = 18
                Dim sizeWidth As SizeF

                Dim strWidth As String = bandRectangle.Width.ToString & " px"

                While e.Graphics.MeasureString(strWidth, New Font("Georgia", fontSizeWidth, FontStyle.Regular, GraphicsUnit.Pixel)).Width * 1.2 > bandRectangle.Width And fontSizeWidth >= 2
                    fontSizeWidth -= 1
                End While

                sizeWidth = e.Graphics.MeasureString(strWidth, New Font("Georgia", fontSizeWidth, FontStyle.Regular, GraphicsUnit.Pixel))

                e.Graphics.DrawString(strWidth, New Font("Georgia", fontSizeWidth, FontStyle.Regular, GraphicsUnit.Pixel), darkBrush, bandRectangle.Left + CSng(bandRectangle.Width / 2 - sizeWidth.Width / 2), bandRectangle.Top - 15 - sizeWidth.Height)
            End If
            If bandRectangle.Height > 14 Then
                'Y Axis
                e.Graphics.DrawLine(darkPen, bandRectangle.Left - 10, bandRectangle.Top + 1, bandRectangle.Left - 10, bandRectangle.Bottom - 1)
                e.Graphics.DrawLine(darkPen, bandRectangle.Left - 14, bandRectangle.Top, bandRectangle.Left - 6, bandRectangle.Top)
                e.Graphics.DrawLine(darkPen, bandRectangle.Left - 14, bandRectangle.Bottom, bandRectangle.Left - 6, bandRectangle.Bottom)

                'Height 
                Dim fontSizeHeight As Integer = 18
                Dim sizeHeight As SizeF

                Dim strHeight As String = bandRectangle.Height.ToString & " px"

                While e.Graphics.MeasureString(strHeight, New Font("Georgia", fontSizeHeight, FontStyle.Regular, GraphicsUnit.Pixel)).Height * 1.2 > bandRectangle.Height And fontSizeHeight >= 2
                    fontSizeHeight -= 1
                End While

                sizeHeight = e.Graphics.MeasureString(strHeight, New Font("Georgia", fontSizeHeight, FontStyle.Regular, GraphicsUnit.Pixel))

                e.Graphics.DrawString(strHeight, New Font("Georgia", fontSizeHeight, FontStyle.Regular, GraphicsUnit.Pixel), darkBrush, CInt(bandRectangle.Left - 15 - sizeHeight.Width), CInt(bandRectangle.Top + bandRectangle.Height / 2 - sizeHeight.Height / 2))
            End If

            If bandRectangle.Height > 14 And bandRectangle.Width > 14 Then
                'Print the Window Title, the Size, or a help string
                Dim s As String = ""

                'Show the right-click-hint (default) or display the size of the region
                If SettingsDatabase.ShowRightClickInfo Then
                    s = "(right-click for menu)"
                Else
                    If _Mode = RegionTypes.Window Then
                        s = GetWindowText(windowHandle)
                    Else
                        s = bandRectangle.Width & " x " & bandRectangle.Height
                    End If
                End If

                Dim fontsize As Integer = 16

                While (e.Graphics.MeasureString(s, New Font("Georgia", fontsize)).Width > bandRectangle.Width Or e.Graphics.MeasureString(s, New Font("Georgia", fontsize)).Height > bandRectangle.Height) And fontsize >= 2
                    fontsize -= 1
                End While

                Dim size = e.Graphics.MeasureString(s, New Font("Georgia", fontsize))

                e.Graphics.DrawString(s, New Font("Georgia", fontsize), darkBrush, CSng(bandRectangle.Left + bandRectangle.Width / 2 - size.Width / 2), CSng(bandRectangle.Top + bandRectangle.Height / 2 - size.Height / 2))
            End If
        End Sub

        ''' <summary>
        ''' Captures the region.
        ''' </summary>
        Private Sub CaptureRegion()
            Me.Visible = False

            'Wait some time to ensure that form is hidden
            Wait(SettingsDatabase.BreakLength)

            Dim bounds = GetRubberBandRectangle()

            If bounds.Width > 0 And bounds.Height > 0 Then
                'check if new screenshot is to create
                If SettingsDatabase.RefreshScreenshotBeforeCapturing Then _Fullscreen = (New FullScreen.FullScreen).CreateScreenshot(Macro).Screenshot

                'declare a temp bitmap to paint with the texture brush
                Dim tempBitmap As New Bitmap(_Fullscreen.Width, _Fullscreen.Height)
                Dim tempGraphics As Graphics = Graphics.FromImage(tempBitmap)

                'Fill the (transparent) background of the screenshot with a user-defined color if desired
                If SettingsDatabase.FillScreenshotBackgroundEnabled Then
                    tempGraphics.Clear(SettingsDatabase.FillScreenshotBackgroundColor)
                End If

                Dim processes As New StringBuilder
                Dim texts As New StringBuilder

                Select Case _Mode
                    Case RegionTypes.Freehand
                        tempGraphics.FillPath(New TextureBrush(_Fullscreen), GetPath)
                    Case RegionTypes.Window
                        For Each win In _selectedWindows
                            Dim winRect As Rectangle = _windows(win)

                            'show a white form, to make the aero windows titles look better
                            If _windowSettings.WhiteFormEnabled Or _windowSettings.Focus Then
                                'show the whiteform if necessary
                                Dim w As New WhiteForm
                                If _windowSettings.WhiteFormEnabled Then
                                    w.Show()
                                    w.Focus()

                                    w.Location = winRect.Location
                                    w.Size = winRect.Size

                                    w.Left -= _windowSettings.CorrectionLeft
                                    w.Top -= _windowSettings.CorrectionTop
                                    w.Width += _windowSettings.CorrectionLeft + _windowSettings.CorrectionRight
                                    w.Height += _windowSettings.CorrectionTop + _windowSettings.CorrectionBottom
                                End If

                                'focus the selected window
                                NativeMethods.SetForegroundWindow(win.ToInt64)

                                'Wait some time to ensure that the whiteform is painted and the window focused
                                Wait(SettingsDatabase.BreakLength)

                                'generate a new screenshot
                                _Fullscreen = (New FullScreen.FullScreen).CreateScreenshot(Macro).Screenshot

                                'close the whiteform if its visible
                                If _windowSettings.WhiteFormEnabled Then
                                    w.Close()

                                    'Check if the whiteform is the only thing on the shot
                                    '(Check 20 Random Pixels if they are white)

                                    Dim onlyWhitePixels As Boolean = True

                                    For i As Integer = 0 To 20
                                        Dim random As New Random(CInt(Now.Ticks / 1000000000))
                                        Dim location As New Point(random.Next(bounds.Left, bounds.Right), random.Next(bounds.Top, bounds.Bottom))

                                        Dim color = _Fullscreen.GetPixel(location.X, location.Y)

                                        If Not (color.R = 255 And color.G = 255 And color.B = 255) Then
                                            onlyWhitePixels = False
                                            Exit For
                                        End If

                                        If onlyWhitePixels Then
                                            Log.LogInformation("Window Shot with nothing but white pixels")

                                            'oooooops, new one!
                                            Thread.Sleep(20)
                                            _Fullscreen = (New FullScreen.FullScreen).CreateScreenshot(Macro).Screenshot
                                        End If

                                    Next
                                End If
                            End If
                            If processes.Length > 0 Then
                                processes.Append("; ")
                            End If
                            processes.Append(GetWindowProcess(win))

                            If texts.Length > 0 Then
                                texts.Append("; ")
                            End If

                            texts.Append(GetWindowText(win))


                            tempGraphics.FillRectangle(New TextureBrush(_Fullscreen), winRect)
                        Next
                    Case RegionTypes.Region
                        If _regionShape = Shape.Rectangle Then
                            tempGraphics.FillRectangle(New TextureBrush(_Fullscreen), bounds)
                        Else
                            tempGraphics.FillEllipse(New TextureBrush(_Fullscreen), bounds)
                        End If
                End Select

                tempGraphics.Dispose()

                'now copy the temp bitmap to the real bitmap
                Dim realBitmap As New Bitmap(bounds.Width, bounds.Height)
                Dim realGraphics As Graphics = Graphics.FromImage(realBitmap)

                realGraphics.DrawImage(tempBitmap, New Rectangle(0, 0, bounds.Width, bounds.Height), bounds, GraphicsUnit.Pixel)

                realGraphics.Dispose()

                _Screenshot = New Screenshot(realBitmap, bounds, Now, _Macro.Multiple.Count, _Mode.ToString, _Macro.Name, _Macro)
                _Screenshot.Shape = _regionShape

                If _Mode = RegionTypes.Window Then
                    _Screenshot.ProcessName = processes.ToString
                    _Screenshot.WindowTitle = texts.ToString
                Else
                    _Screenshot.ProcessName = ""
                    _Screenshot.WindowTitle = ""
                End If
            End If

            Me.Close()
        End Sub

        ''' <summary>
        ''' Implements a break
        ''' </summary>
        ''' <param name="miliSeconds">The length of the break in miliseconds</param>
        ''' <remarks></remarks>
        Public Shared Sub Wait(ByVal miliSeconds As Integer)
            Dim stpWatch As New Stopwatch
            stpWatch.Start()

            While stpWatch.ElapsedMilliseconds < miliSeconds
                Application.DoEvents()
            End While
            stpWatch.Stop()
        End Sub

        ''' <summary>
        ''' Gets the mode.
        ''' </summary>
        ''' <param name="mouseLocation">The mouse location.</param>
        ''' <returns></returns>
        Private Function GetRegionMode(ByVal mouseLocation As Point) As RegionChangeMode
            Dim bandRect = GetRubberBandRectangle()

            Dim borderSize As Integer = 16

            'The Rectangles
            Dim moveRect As New Rectangle(CInt(bandRect.Left + borderSize / 2), CInt(bandRect.Top + borderSize / 2), bandRect.Width - borderSize, bandRect.Height - borderSize)

            Dim sizeWestRect As New Rectangle(CInt(bandRect.Left - borderSize / 2), CInt(bandRect.Top - borderSize / 2), borderSize, bandRect.Height + borderSize)
            Dim sizeEastRect As New Rectangle(CInt(bandRect.Right - borderSize / 2), CInt(bandRect.Top - borderSize / 2), borderSize, bandRect.Height + borderSize)

            Dim sizeNorthRect As New Rectangle(CInt(bandRect.Left - borderSize / 2), CInt(bandRect.Top - borderSize / 2), bandRect.Width + borderSize, borderSize)
            Dim sizeSouthRect As New Rectangle(CInt(bandRect.Left - borderSize / 2), CInt(bandRect.Bottom - borderSize / 2), bandRect.Width + borderSize, borderSize)

            Dim sizeNorthWest As Rectangle = Rectangle.Intersect(sizeNorthRect, sizeWestRect)
            Dim sizeNorthEast As Rectangle = Rectangle.Intersect(sizeNorthRect, sizeEastRect)
            Dim sizeSouthWest As Rectangle = Rectangle.Intersect(sizeSouthRect, sizeWestRect)
            Dim sizeSouthEast As Rectangle = Rectangle.Intersect(sizeSouthRect, sizeEastRect)

            'Check what is hovered by the mouse
            If moveRect.Contains(mouseLocation) Then
                Return RegionChangeMode.Move
            ElseIf sizeNorthWest.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeNW
            ElseIf sizeSouthEast.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeSE
            ElseIf sizeNorthEast.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeNE
            ElseIf sizeSouthWest.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeSW
            ElseIf sizeWestRect.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeW
            ElseIf sizeEastRect.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeE
            ElseIf sizeNorthRect.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeN
            ElseIf sizeSouthRect.Contains(mouseLocation) Then
                Return RegionChangeMode.ResizeS
            Else
                Return RegionChangeMode.Create
            End If
        End Function

        ''' <summary>
        ''' Gets the rubber band rectangle.
        ''' </summary>
        ''' <returns></returns>
        Private Function GetRubberBandRectangle() As Rectangle
            Dim shapeBounds As New Rectangle

            Select Case _Mode
                Case RegionTypes.Region
                    Dim tempStart As Point = _regionStart
                    Dim tempEnd As Point = _regionEnd

                    'Add the Resize, if any
                    Dim resizeX As Integer = _resizeEnd.X - _resizeStart.X
                    Dim resizeY As Integer = _resizeEnd.Y - _resizeStart.Y

                    Dim direction As String = _regionMode.ToString
                    If direction.StartsWith("Resize") Then
                        direction = direction.Replace("Resize", "")

                        If direction.Contains("W") Then
                            tempStart.X += resizeX
                        End If
                        If direction.Contains("E") Then
                            tempEnd.X += resizeX
                        End If
                        If direction.Contains("N") Then
                            tempStart.Y += resizeY
                        End If
                        If direction.Contains("S") Then
                            tempEnd.Y += resizeY
                        End If
                    End If


                    If tempEnd.X > tempStart.X Then
                        shapeBounds.X = tempStart.X
                        shapeBounds.Width = tempEnd.X - tempStart.X
                    Else
                        shapeBounds.X = tempEnd.X
                        shapeBounds.Width = tempStart.X - tempEnd.X
                    End If

                    If tempEnd.Y > tempStart.Y Then
                        shapeBounds.Y = tempStart.Y
                        shapeBounds.Height = tempEnd.Y - tempStart.Y
                    Else
                        shapeBounds.Y = tempEnd.Y
                        shapeBounds.Height = tempStart.Y - tempEnd.Y
                    End If

                    'Add the Movement, if any
                    Dim moveX As Integer = _moveEnd.X - _moveStart.X
                    Dim moveY As Integer = _moveEnd.Y - _moveStart.Y

                    shapeBounds.X += moveX
                    shapeBounds.Y += moveY
                Case RegionTypes.Window
                    Dim left, top, right, bottom As Integer

                    left = -1
                    top = -1
                    right = -1
                    bottom = -1

                    For Each win In _selectedWindows
                        Dim windowRect As Rectangle = _windows(win)

                        windowRect.X -= _windowSettings.CorrectionLeft
                        windowRect.Y -= _windowSettings.CorrectionTop
                        windowRect.Width += _windowSettings.CorrectionLeft + _windowSettings.CorrectionRight
                        windowRect.Height += _windowSettings.CorrectionTop + _windowSettings.CorrectionBottom

                        If left > windowRect.Left Or left = -1 Then left = windowRect.Left
                        If top > windowRect.Top Or top = -1 Then top = windowRect.Top
                        If right < windowRect.Right Or right = -1 Then right = windowRect.Right
                        If bottom < windowRect.Bottom Or bottom = -1 Then bottom = windowRect.Bottom
                    Next

                    shapeBounds = New Rectangle(left, top, right - left, bottom - top)
                Case RegionTypes.Freehand
                    Dim b = GetPath.GetBounds
                    shapeBounds = New Rectangle(CInt(b.Left), CInt(b.Top), CInt(b.Width), CInt(b.Height))
            End Select

            Return shapeBounds
        End Function

        ''' <summary>
        ''' Resizes the region.
        ''' </summary>
        ''' <param name="percentage">The percentage.</param>
        Private Sub ResizeRegion(ByVal percentage As Single)
            Dim rectOrig = GetRubberBandRectangle()
            Dim rectNew As New Rectangle(CInt(rectOrig.Left + (rectOrig.Width - percentage * rectOrig.Width) / 2), CInt(rectOrig.Top + (rectOrig.Height - percentage * rectOrig.Height) / 2), CInt(percentage * rectOrig.Width), CInt(percentage * rectOrig.Height))

            _regionStart = rectNew.Location
            _regionEnd = New Point(rectNew.Right, rectNew.Bottom)

            Me.Refresh()
        End Sub

#End Region

        ''' <summary>
        ''' Handles the Click event of the mnuSelect control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuSelect.Click
            _selectedWindows.Add(_hoveredWindow)
            ManipulateMenuItems()
        End Sub
    End Class
End Namespace
