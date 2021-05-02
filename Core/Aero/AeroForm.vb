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

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles
Imports ScreenshotStudioDotNet.Core.Logging

Namespace Aero
    ''' <summary>
    ''' Provides a aero enabled form on that can be painted without having any ugly effects.
    ''' </summary>
        Public Class AeroForm
        Inherits Form

#Region "Fields"

        Private _margins As Margins
        Private _actionIfDisabled As DwmDisabledActions
        Private _dragWindowOnGlass As Boolean

#End Region

#Region "Properties"



        ''' <summary>
        ''' Gets a value indicating whether [DWM enabled].
        ''' </summary>
        ''' <value><c>true</c> if [DWM enabled]; otherwise, <c>false</c>.</value>
        Public Shared ReadOnly Property DwmEnabled() As Boolean
            Get
                'OS Check
                If Environment.OSVersion.Version.Major < 6 Then Return False

                'Aero Check
                Dim b As Boolean
                NativeMethods.DwmIsCompositionEnabled(b)

                Return b
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the action if DWM disabled.
        ''' </summary>
        ''' <value>The action if DWM disabled.</value>
        Public Property ActionIfDwmDisabled() As DwmDisabledActions
            Get
                Return _actionIfDisabled
            End Get
            Set(ByVal value As DwmDisabledActions)
                _actionIfDisabled = value
                Me.Refresh()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the margins.
        ''' </summary>
        ''' <value>The margins.</value>
        Public Property GlassMargins() As Margins
            Get
                Return _margins
            End Get
            Set(ByVal value As Margins)
                _margins = value

                'Refresh Glass
                SetDwmArea()
            End Set
        End Property

        ''' <summary>
        ''' Sets a value indicating whether [show title bar].
        ''' </summary>
        ''' <value><c>true</c> if [show title bar]; otherwise, <c>false</c>.</value>
        Public WriteOnly Property ShowTitleBar As Boolean
            Set(ByVal value As Boolean)
                Dim ops As New NativeMethods.WTA_OPTIONS()

                If Not value Then
                    'We Want To Hide the Caption and the Icon
                    ops.Flags = NativeMethods.WTNCA_NODRAWCAPTION Or NativeMethods.WTNCA_NODRAWICON
                    'If we set the Mask to the same value as the Flags, the Flags are Added. If Not They are Removed
                    ops.Mask = NativeMethods.WTNCA_NODRAWCAPTION Or NativeMethods.WTNCA_NODRAWICON
                End If

                'Set It, The Marshal.Sizeof() stuff is to get the right size of the custom struct, and in UINT/DWORD Form
                NativeMethods.SetWindowThemeAttribute(Me.Handle, NativeMethods.WindowThemeAttributeType.WTA_NONCLIENT, ops, CUInt(Marshal.SizeOf(GetType(NativeMethods.WTA_OPTIONS))))
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [drag window on glass].
        ''' </summary>
        ''' <value><c>true</c> if [drag window on glass]; otherwise, <c>false</c>.</value>
        Public Property DragWindowOnGlass As Boolean
            Get
                Return _dragWindowOnGlass
            End Get
            Set(ByVal value As Boolean)
                _dragWindowOnGlass = value
            End Set
        End Property

#End Region

#Region "Functions"

        ''' <summary>
        ''' Draws text on glass.
        ''' </summary>
        ''' <param name="text">The text.</param>
        ''' <param name="font">The font.</param>
        ''' <param name="ctlrct">The CTLRCT.</param>
        ''' <param name="iglowSize">Size of the iglow.</param>
        Public Sub DrawTextOnGlass(ByVal text As String, ByVal font As Font, ByVal ctlrct As Rectangle, ByVal iglowSize As Integer)
            Dim hwnd As IntPtr = Me.Handle

            If DwmEnabled Then
                Dim rc As New NativeMethods.RECT()
                Dim rc2 As New NativeMethods.RECT()

                rc.left = ctlrct.Left
                rc.right = ctlrct.Right + 2 * iglowSize
                'make it larger to contain the glow effect
                rc.top = ctlrct.Top
                rc.bottom = ctlrct.Bottom + 2 * iglowSize

                'Just the same rect with rc,but (0,0) at the lefttop
                rc2.left = 0
                rc2.top = 0
                rc2.right = rc.right - rc.left
                rc2.bottom = rc.bottom - rc.top

                Dim destdc As IntPtr = NativeMethods.GetDC(hwnd)
                'hwnd must be the handle of form,not control
                Dim Memdc As IntPtr = NativeMethods.CreateCompatibleDC(destdc)
                'Set up a memory DC where we'll draw the text.
                Dim bitmap As IntPtr
                Dim bitmapOld As IntPtr = IntPtr.Zero
                Dim logfnotOld As IntPtr

                Dim uFormat As Integer = NativeMethods.DT_SINGLELINE Or NativeMethods.DT_CENTER Or NativeMethods.DT_VCENTER Or NativeMethods.DT_NOPREFIX
                'text format

                Dim dib As New NativeMethods.BITMAPINFO()
                dib.bmiHeader.biHeight = -(rc.bottom - rc.top)
                'negative because DrawThemeTextEx() uses a top-down DIB
                dib.bmiHeader.biWidth = rc.right - rc.left
                dib.bmiHeader.biPlanes = 1
                dib.bmiHeader.biSize = Marshal.SizeOf(GetType(NativeMethods.BITMAPINFOHEADER))
                dib.bmiHeader.biBitCount = 32
                dib.bmiHeader.biCompression = NativeMethods.BI_RGB
                If Not NativeMethods.SaveDC(Memdc) = 0 Then
                    bitmap = NativeMethods.CreateDIBSection(Memdc, dib, CUInt(NativeMethods.DIB_RGB_COLORS), 0, IntPtr.Zero, 0)
                    'Create a 32-bit bmp for use in offscreen drawing when glass is on
                    If Not bitmap = IntPtr.Zero Then
                        bitmapOld = NativeMethods.SelectObject(Memdc, bitmap)
                        Dim hFont As IntPtr = font.ToHfont()
                        logfnotOld = NativeMethods.SelectObject(Memdc, hFont)
                        Try
                            Dim renderer As New VisualStyleRenderer(VisualStyleElement.Window.Caption.Active)

                            Dim dttOpts As New NativeMethods.DTTOPTS()
                            dttOpts.dwSize = CUInt(Marshal.SizeOf(dttOpts.GetType))
                            dttOpts.dwFlags = NativeMethods.DTT_COMPOSITED Or NativeMethods.DTT_GLOWSIZE
                            dttOpts.iGlowSize = iglowSize

                            NativeMethods.DrawThemeTextEx(renderer.Handle, Memdc, 0, 0, text, -1, uFormat, rc2, dttOpts)

                            NativeMethods.BitBlt(destdc, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, Memdc, 0, 0, CUInt(NativeMethods.SRCCOPY))
                        Catch e As Exception
                            Log.LogError(e)
                        End Try

                        'Remember to clean up
                        NativeMethods.SelectObject(Memdc, bitmapOld)
                        NativeMethods.SelectObject(Memdc, logfnotOld)
                        NativeMethods.DeleteObject(bitmap)
                        NativeMethods.DeleteObject(hFont)

                        NativeMethods.ReleaseDC(Memdc, -1)
                        NativeMethods.DeleteDC(Memdc)
                    End If
                End If
            End If
        End Sub


        ''' <summary>
        ''' Sets the DWM area.
        ''' </summary>
        Private Sub SetDwmArea()
            'Only if aero enabled
            If DwmEnabled Then
                NativeMethods.DwmExtendFrameIntoClientArea(Me.Handle, New NativeMethods.MARGINS(_margins.Left, _margins.Top, _margins.Right, _margins.Bottom))
            End If

            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Paints the black region.
        ''' </summary>
        ''' <param name="region">The region.</param>
        ''' <param name="graphics">The graphics.</param>
        Private Sub PaintBlackRegion(ByVal region As Rectangle, ByVal graphics As Graphics)
            If DwmEnabled Then
                Dim rc As New NativeMethods.RECT()
                rc.left = region.Left
                rc.right = region.Right
                rc.top = region.Top
                rc.bottom = region.Bottom

                Dim destdc As IntPtr = graphics.GetHdc()
                Dim Memdc As IntPtr = NativeMethods.CreateCompatibleDC(destdc)
                Dim bitmap As IntPtr
                Dim bitmapOld As IntPtr = IntPtr.Zero

                Dim dib As New NativeMethods.BITMAPINFO()
                dib.bmiHeader.biHeight = -(rc.bottom - rc.top)
                dib.bmiHeader.biWidth = rc.right - rc.left
                dib.bmiHeader.biPlanes = 1
                dib.bmiHeader.biSize = Marshal.SizeOf(GetType(NativeMethods.BITMAPINFOHEADER))
                dib.bmiHeader.biBitCount = 32
                dib.bmiHeader.biCompression = NativeMethods.BI_RGB
                If Not (NativeMethods.SaveDC(Memdc) = 0) Then
                    bitmap = NativeMethods.CreateDIBSection(Memdc, dib, CUInt(NativeMethods.DIB_RGB_COLORS), 0, IntPtr.Zero, 0)

                    If Not (bitmap = IntPtr.Zero) Then
                        bitmapOld = NativeMethods.SelectObject(Memdc, bitmap)

                        NativeMethods.BitBlt(destdc, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, Memdc, 0, 0, CUInt(NativeMethods.SRCCOPY))
                    End If

                    'Remember to clean up
                    NativeMethods.SelectObject(Memdc, bitmapOld)
                    NativeMethods.DeleteObject(bitmap)
                    NativeMethods.ReleaseDC(Memdc, -1)
                    NativeMethods.DeleteDC(Memdc)
                End If

                graphics.ReleaseHdc()
            Else
                If _actionIfDisabled = DwmDisabledActions.FillWithOtherColor Then
                    'Paint with other color
                    graphics.FillRectangle(New SolidBrush(Color.FromArgb(255, 185, 209, 234)), region)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DwmForm" /> class.
        ''' </summary>
        Public Sub New()
            _margins = New Margins(0, 0, 0, 0)
            _actionIfDisabled = DwmDisabledActions.FillWithOtherColor

            'Initialize the Glass Area
            SetDwmArea()
        End Sub

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Called before the Paint-Event of the Form is raised.
        ''' </summary>
        ''' <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            If _margins.Bottom = -1 And _margins.Left = -1 And _margins.Top = -1 And _margins.Right = -1 Then
                PaintBlackRegion(Me.ClientRectangle, e.Graphics)
            Else
                PaintBlackRegion(New Rectangle(0, 0, _margins.Left, Me.ClientRectangle.Height), e.Graphics)
                PaintBlackRegion(New Rectangle(Me.ClientRectangle.Width - _margins.Right, 0, _margins.Right, Me.ClientRectangle.Height), e.Graphics)
                PaintBlackRegion(New Rectangle(_margins.Left, 0, Me.ClientRectangle.Width - _margins.Left - _margins.Right, _margins.Top), e.Graphics)
                PaintBlackRegion(New Rectangle(_margins.Left, Me.ClientRectangle.Height - _margins.Bottom, Me.ClientRectangle.Width - _margins.Left - _margins.Right, _margins.Bottom), e.Graphics)
            End If

            MyBase.OnPaint(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            ' if this is a click, it is on the client and DragWindowOnGlass-Property is set to true
            If m.Msg = &H84 And m.Result.ToInt32() = 1 And Me.DragWindowOnGlass Then
                'Check if the mouse is over the form area

                Dim x As Integer = (m.LParam.ToInt32 << 16) >> 16 'lo order word
                Dim y As Integer = m.LParam.ToInt32 >> 16 'hi order word
                
                Dim mousePoint As Point = Me.PointToClient(New Point(x, y))
                Dim nonGlassSurface As New Rectangle(Me.GlassMargins.Left, Me.GlassMargins.Top, Me.Width - Me.GlassMargins.Right - Me.GlassMargins.Left, Me.Height - Me.GlassMargins.Top - Me.GlassMargins.Bottom)

                If Not nonGlassSurface.Contains(mousePoint) Then
                    ' lie and say they clicked on the title bar
                    m.Result = New IntPtr(2)
                End If
            End If
        End Sub

#End Region
    End Class
End Namespace
