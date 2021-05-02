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

Imports System.Drawing

Namespace Controls
    Public Class Line
        ''' <summary>
        ''' Gets or sets the thickness.
        ''' </summary>
        ''' <value>The thickness.</value>
        Public Property Thickness As Integer = 1

        ''' <summary>
        ''' Gets or sets the color of the line.
        ''' </summary>
        ''' <value>The color of the line.</value>
        Public Property LineColor As Color
            Get
                Return Me.ForeColor
            End Get
            Set(ByVal value As Color)
                Me.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the direction.
        ''' </summary>
        ''' <value>The direction.</value>
        Public Property Direction As LineDirection = LineDirection.Horizontal

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Line" /> class.
        ''' </summary>
        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.ForeColor = Drawing.Color.LightGray
            Me.Width = Me.Thickness
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the Line control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub Line_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias


            Dim startPoint, endPoint As Point

            If Me.Direction = LineDirection.Horizontal Then
                startPoint = New Point(0, CInt(Me.Height / 2))
                endPoint = New Point(Me.Width, CInt(Me.Height / 2))
            Else
                startPoint = New Point(CInt(Me.Width / 2), 0)
                endPoint = New Point(CInt(Me.Width / 2), Me.Height)
            End If

            e.Graphics.DrawLine(New Pen(New SolidBrush(Me.ForeColor), Me.Thickness), startPoint, endPoint)
        End Sub
    End Class
End Namespace
