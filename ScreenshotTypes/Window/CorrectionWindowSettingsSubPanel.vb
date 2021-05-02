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

Namespace Window
    Public Class CorrectionWindowSettingsSubPanel
        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.ScreenshotTypes.Strings", Assembly.GetExecutingAssembly)
        Private _settings As New WindowSettings

        '' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public Overrides ReadOnly Property DisplayName() As String
            Get
                Return _langManager.GetString("correction")
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether [properties changed].
        ''' </summary>
        ''' <value><c>true</c> if [properties changed]; otherwise, <c>false</c>.</value>
        Public Overrides ReadOnly Property PropertiesChanged() As Boolean
            Get
                If _settings.CorrectionLeft <> CInt(numLeft.Value) Then Return True
                If _settings.CorrectionTop <> CInt(numTop.Value) Then Return True
                If _settings.CorrectionRight <> CInt(numRight.Value) Then Return True
                If _settings.CorrectionBottom <> CInt(numBottom.Value) Then Return True

                Return False
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CorrectionWindowSettingsSubPanel" /> class.
        ''' </summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            RefreshButtonSize()

            'Load Settings
            numLeft.Value = _settings.CorrectionLeft
            numTop.Value = _settings.CorrectionTop
            numRight.Value = _settings.CorrectionRight
            numBottom.Value = _settings.CorrectionBottom
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Overrides Sub Save()
            _settings.CorrectionLeft = CInt(numLeft.Value)
            _settings.CorrectionTop = CInt(numTop.Value)
            _settings.CorrectionRight = CInt(numRight.Value)
            _settings.CorrectionBottom = CInt(numBottom.Value)
        End Sub

        ''' <summary>
        ''' Refreshes the size of the button.
        ''' </summary>
        Private Sub RefreshButtonSize()
            btnWindow.Width = CInt(100 + numLeft.Value + numRight.Value)
            btnWindow.Height = CInt(100 + numTop.Value + numBottom.Value)
            btnWindow.Left = CInt(panelAdjust.Width / 2 - 50 - numLeft.Value)
            btnWindow.Top = CInt(panelAdjust.Height / 2 - 50 - numTop.Value)

            btnWindowStandard.Width = 100
            btnWindowStandard.Height = 100
            btnWindowStandard.Left = CInt(panelAdjust.Width / 2 - 50)
            btnWindowStandard.Top = CInt(panelAdjust.Height / 2 - 50)

            If numLeft.Value < 0 Then
                btnWindowStandard.Width += CInt(numLeft.Value)
                btnWindowStandard.Left -= CInt(numLeft.Value)
            End If

            If numRight.Value < 0 Then
                btnWindowStandard.Width += CInt(numRight.Value)
            End If

            If numTop.Value < 0 Then
                btnWindowStandard.Height += CInt(numTop.Value)
                btnWindowStandard.Top -= CInt(numTop.Value)
            End If

            If numBottom.Value < 0 Then
                btnWindowStandard.Height += CInt(numBottom.Value)
            End If

            btnWindowSubstract.Width = 100
            btnWindowSubstract.Height = 100
            btnWindowSubstract.Left = CInt(panelAdjust.Width / 2 - 50)
            btnWindowSubstract.Top = CInt(panelAdjust.Height / 2 - 50)

            panelTop.Left = CInt(panelAdjust.Width / 2 - panelTop.Width / 2)
            panelTop.Top = CInt(panelAdjust.Height / 2 - 50 - panelTop.Height - 15)

            panelBottom.Left = CInt(panelAdjust.Width / 2 - panelBottom.Width / 2)
            panelBottom.Top = CInt(panelAdjust.Height / 2 + 50 + 15)

            panelLeft.Left = CInt(panelAdjust.Width / 2 - 50 - panelLeft.Width - 15)
            panelLeft.Top = CInt(panelAdjust.Height / 2 - panelLeft.Height / 2)

            panelRight.Left = CInt(panelAdjust.Width / 2 + 50 + 15)
            panelRight.Top = CInt(panelAdjust.Height / 2 - panelRight.Height / 2)
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numTop control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numTop_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numTop.ValueChanged
            RefreshButtonSize()
        End Sub


        ''' <summary>
        ''' Handles the ValueChanged event of the numRight control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numRight_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numRight.ValueChanged
            RefreshButtonSize()
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numBottom control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numBottom_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numBottom.ValueChanged
            RefreshButtonSize()
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numLeft control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numLeft_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numLeft.ValueChanged
            RefreshButtonSize()
        End Sub
    End Class
End Namespace
