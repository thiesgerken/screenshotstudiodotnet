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

Imports ScreenshotStudioDotNet.Core.Aero
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Extensibility
    Public Class PluginArgumentDesigner

        ''' <summary>
        ''' Gets or sets the result.
        ''' </summary>
        ''' <value>The result.</value>
        Public Property Result As SerializableDictionary(Of String, Object)

        ''' <summary>
        ''' Sets a value indicating whether [properties changed].
        ''' </summary>
        ''' <value><c>true</c> if [properties changed]; otherwise, <c>false</c>.</value>
        Protected WriteOnly Property PropertiesChanged As Boolean
            Set(ByVal value As Boolean)
                btnSave.Enabled = value
            End Set
        End Property

        ''' <summary>
        ''' Handles the Click event of the btnSave control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Save()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Overridable Sub Save()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

        ''' <summary>
        ''' Handles the Load event of the PluginArgumentDesigner control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub PluginArgumentDesigner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            PropertiesChanged = False

            'Me.GlassMargins = New Margins(10, 10, 10, 10)
        End Sub
    End Class
End Namespace
