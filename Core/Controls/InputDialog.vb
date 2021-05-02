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
Imports ScreenshotStudioDotNet.Core.Aero

Namespace Controls
    Public Class InputDialog

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the InputDialog control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub InputDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Me.GlassMargins = New Margins(3, 3, 200, 3)
            Me.ShowTitleBar = False
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the InputDialog control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub InputDialog_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            Dim f As New Font("Segoe UI", 10)
            Dim size = e.Graphics.MeasureString(Me.Instruction, f)

            If Me.GlassMargins.Top <> CInt(size.Height * 2) Then
                Me.GlassMargins = New Margins(0, 0, CInt(size.Height * 2), 0)
                Me.Height = Me.GlassMargins.Top + panelContent.Height + Me.Height - Me.ClientRectangle.Height
                Me.Width = panelContent.Width + Me.Width - Me.ClientRectangle.Width
                panelContent.Location = New Point(0, Me.GlassMargins.Top)
            End If

            Me.DrawTextOnGlass(Me.Instruction, f, New Rectangle(0, 0, CInt(size.Width), CInt(size.Height)), 10)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnOK control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End Sub

        ''' <summary>
        ''' Handles the KeyPress event of the txtInput control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs" /> instance containing the event data.</param>
        Private Sub txtInput_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtInput.KeyPress
            If e.KeyChar = Chr(Keys.Enter) Then Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the instruction.
        ''' </summary>
        ''' <value>The instruction.</value>
        Public Property Instruction As String

        ''' <summary>
        ''' Gets or sets the long instruction.
        ''' </summary>
        ''' <value>The long instruction.</value>
        Public Property LongInstruction As String
            Get
                Return lblLongInstruction.Text
            End Get
            Set(ByVal value As String)
                lblLongInstruction.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the action.
        ''' </summary>
        ''' <value>The name of the action.</value>
        Public Property ActionName As String
            Get
                Return btnOK.Text
            End Get
            Set(ByVal value As String)
                btnOK.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the cancel action.
        ''' </summary>
        ''' <value>The name of the cancel action.</value>
        Public Property CancelActionName As String
            Get
                Return btnCancel.Text
            End Get
            Set(ByVal value As String)
                btnCancel.Text = value
            End Set
        End Property

        ''' <summary>
        ''' The text to display in the watermark textbox.
        ''' </summary>
        ''' <value>The name of the input parameter.</value>
        Public Property InputParameterName As String
            Get
                Return txtInput.WatermarkText
            End Get
            Set(ByVal value As String)
                txtInput.WatermarkText = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the input.
        ''' </summary>
        ''' <value>The input.</value>
        Public Property Input As String
            Get
                Return txtInput.Text
            End Get
            Set(ByVal value As String)
                txtInput.Text = value
            End Set
        End Property

#End Region
    End Class
End Namespace
