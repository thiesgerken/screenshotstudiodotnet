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
Imports System.Windows.Forms
Imports ScreenshotStudioDotNet.Core.Misc

Namespace Controls
    Public Class ColorSelectionButton

#Region "Fields"

        Private _color As Color

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the color.
        ''' </summary>
        ''' <value>The color.</value>
        Public Property Color As Color
            Get
                Return _color
            End Get
            Set(ByVal value As Color)
                _color = value

                Me.Refresh()
            End Set
        End Property

        Public Property FontSize As Single = 16.0F

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns>The text associated with this control.</returns>
        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
            End Set
        End Property

#End Region

#Region "Events"

        Public Event ColorChanged(ByVal sender As Object, ByVal e As EventArgs)

        ''' <summary>
        ''' Called when [color changed].
        ''' </summary>
        Public Sub OnColorChanged()
            RaiseEvent ColorChanged(Me, New EventArgs)
        End Sub

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ColorSelectionButton" /> class.
        ''' </summary>
        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Color = Color.Black
            Me.Refresh()
        End Sub

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Click event of the ColorSelectionButton control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub ColorSelectionButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Click
            ColorSelector.Color = _color
            Dim result = ColorSelector.ShowDialog()

            If result = System.Windows.Forms.DialogResult.OK Then
                If Me.Color <> ColorSelector.Color Then
                    Me.Color = ColorSelector.Color
                    OnColorChanged()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the ControlAdded event of the ColorSelectionButton control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.ControlEventArgs" /> instance containing the event data.</param>
        Private Sub ColorSelectionButton_ControlAdded(ByVal sender As Object, ByVal e As ControlEventArgs) Handles Me.ControlAdded
            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the ColorSelectionButton control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub ColorSelectionButton_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            Dim hexCode As String = "#"
            hexCode &= String.Format("{0:X2}", _color.R)
            hexCode &= String.Format("{0:X2}", _color.G)
            hexCode &= String.Format("{0:X2}", _color.B)

            Me.Text = ""
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            Extensions.DrawButton(e.Graphics, New Point(0, 0), Me.Size, _color, hexCode, "Segoe UI", FontSize, 4)
        End Sub

        ''' <summary>
        ''' Handles the Resize event of the ColorSelectionButton control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub ColorSelectionButton_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            Me.Refresh()
        End Sub

#End Region
    End Class
End Namespace
