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

Namespace FreeHand
    Public Class FreeHandSettingsPanel
        Private _settings As New FreeHandSettings
        Private _changed As Boolean = False

        ''' <summary>
        ''' Gets a value indicating whether the user has changed the properties.
        ''' </summary>
        ''' <value><c>true</c> if the properties were changed; otherwise, <c>false</c>.</value>
        Public Overrides ReadOnly Property PropertiesChanged() As Boolean
            Get
                Return _changed
            End Get
        End Property

        ''' <summary>
        ''' Checks the changes.
        ''' </summary>
        ''' <returns></returns>
        Private Function CheckChanges() As Boolean
            If chkMagnifyingGlass.Checked <> _settings.ShowMagnifyingGlass Then
                Return True
            ElseIf chkPaintCrosshair.Checked <> _settings.PaintCrosshair Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Overrides Sub Save()
            _settings.ShowMagnifyingGlass = chkMagnifyingGlass.Checked
            _settings.PaintCrosshair = chkPaintCrosshair.Checked

            _changed = CheckChanges()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="RegionSettingsPanel" /> class.
        ''' </summary>
        Public Sub New()
            InitializeComponent()

            chkMagnifyingGlass.Checked = _settings.ShowMagnifyingGlass
            chkPaintCrosshair.Checked = _settings.PaintCrosshair
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the chkPaintCrosshair control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub chkPaintCrosshair_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPaintCrosshair.CheckedChanged
            _changed = CheckChanges()
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the chkMagnifyingGlass control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub chkMagnifyingGlass_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMagnifyingGlass.CheckedChanged
            _changed = CheckChanges()
        End Sub
    End Class
End Namespace
