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

Namespace Email
    Public Class EmailSettingsPanel

#Region "Fields"

        Private _settings As New EmailSettings
        Private _changed As Boolean = False

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets a value indicating whether the user has changed the properties.
        ''' </summary>
        ''' <value><c>true</c> if the properties were changed; otherwise, <c>false</c>.</value>
        Public Overrides ReadOnly Property PropertiesChanged() As Boolean
            Get
                Return _changed
            End Get
        End Property

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmailSettingsPanel" /> class.
        ''' </summary>
        Public Sub New()
            InitializeComponent()

            txtAddress.Text = _settings.Address
            txtBody.Text = _settings.Body
            txtSubject.Text = _settings.Subject
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Checks the changes.
        ''' </summary>
        ''' <returns></returns>
        Private Function CheckChanges() As Boolean
            If txtAddress.Text <> _settings.Address Then
                Return True
            ElseIf txtBody.Text <> _settings.Body Then
                Return True
            ElseIf txtSubject.Text <> _settings.Subject Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Overrides Sub Save()
            _settings.Body = txtBody.Text
            _settings.Subject = txtSubject.Text
            _settings.Address = txtAddress.Text

            _changed = CheckChanges()
        End Sub

#End Region

#Region "EventHandlers"

        ''' <summary>
        ''' Handles the Leave event of the txtAddress control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub txtAddress_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtAddress.Leave
            Dim regex As String = "^.+?@.+?\..+?$"

            If Not (txtAddress.Text = "" Or System.Text.RegularExpressions.Regex.IsMatch(txtAddress.Text, regex)) Then
                txtAddress.Text = _settings.Address
            End If
        End Sub

        ''' <summary>
        ''' Handles the TextChanged event of the txtAddress control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub txtAddress_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAddress.TextChanged
            _changed = CheckChanges()
        End Sub

        ''' <summary>
        ''' Handles the TextChanged event of the txtSubject control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub txtSubject_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSubject.TextChanged
            _changed = CheckChanges()
        End Sub

        ''' <summary>
        ''' Handles the TextChanged event of the txtBody control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub txtBody_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBody.TextChanged
            _changed = CheckChanges()
        End Sub

#End Region
    End Class
End Namespace
