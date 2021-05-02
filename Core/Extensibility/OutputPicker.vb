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
Imports Microsoft.WindowsAPICodePack.Controls.WindowsForms

Namespace Extensibility
    Public Class OutputPicker

#Region "Fields"

        Private _outputs As New PluginDatabase(Of IOutput)
        Private _selectedOutputName As String

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the name of the selected output.
        ''' </summary>
        ''' <value>The name of the selected output.</value>
        Public ReadOnly Property SelectedOutputName() As String
            Get
                Return _selectedOutputName
            End Get
        End Property

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the OutputPicker control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub OutputPicker_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            _selectedOutputName = ""

            Dim i As Integer = 0

            Dim linkWidth As Integer = 350
            Dim linkHeight As Integer = 60

            btnCancel.Width = linkWidth
            btnCancel.Height = linkHeight

            For Each p In _outputs
                If Not p.Name = "None" Or Not p.CreateInstance.IsAvailable Then
                    Dim c As New CommandLink
                    c.Name = p.Name
                    c.Text = p.DisplayName
                    c.NoteText = p.Description

                    c.Width = linkWidth
                    c.Height = linkHeight

                    c.Location = New Point(12, 12 + (linkHeight + 3) * i)

                    AddHandler c.Click, AddressOf OutputLink_Click

                    Me.Controls.Add(c)

                    i += 1
                End If
            Next

            btnCancel.Location = New Point(12, 12 + (linkHeight + 3) * i + 10)

            Me.Height = Me.Height - Me.ClientRectangle.Height + 24 + (linkHeight + 3) * (i + 1) + 10
            Me.Width = Me.Width - Me.ClientRectangle.Width + 24 + linkWidth
        End Sub

        ''' <summary>
        ''' Handles the Click event of the OutputLink control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub OutputLink_Click(ByVal sender As Object, ByVal e As EventArgs)
            _selectedOutputName = CType(sender, CommandLink).Name
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the OutputPicker control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub OutputPicker_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            e.Graphics.DrawLine(Pens.LightGray, 12, btnCancel.Top - 5, btnCancel.Right, btnCancel.Top - 5)
        End Sub

#End Region
    End Class
End Namespace
