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

Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Extensibility
    Public MustInherit Class PluginSettingsBase

#Region "Fields"

        Private _settings As SerializableDictionary(Of String, String)

#End Region

        'Name:Value

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the settings.
        ''' </summary>
        ''' <value>The setting.</value>
        Public Property Settings(ByVal name As String) As String
            Get
                'Check if the settings are loaded (Singleton)
                If _settings Is Nothing Then Load()

                'Check if this setting is in the database
                If _settings.ContainsKey(name) Then
                    Return _settings(name)
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                'Check if the settings are loaded
                If _settings Is Nothing Then Load()

                'Write this setting
                _settings(name) = value

                Save()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the plugin.
        ''' </summary>
        ''' <value>The name of the plugin.</value>
        Public Property PluginName() As String

#End Region

#Region "Functions"

        ''' <summary>
        ''' Gets the setting if it exists in the database and is not "".
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="defaultValue">The default value.</param>
        ''' <returns></returns>
        Public Function GetSetting(ByVal name As String, ByVal defaultValue As String) As String
            If _settings Is Nothing Then Load()

            If _settings.ContainsKey(name) Then
                If Not _settings(name) = "" Then Return _settings(name)
            End If

            Return defaultValue
        End Function

        ''' <summary>
        ''' Loads this instance.
        ''' </summary>
        Private Sub Load()
            'Create dump dictionary to get the type of it
            Dim d As New SerializableDictionary(Of String, String)
            _settings = CType(Serializer.Deserialize("Plugin." & _PluginName & ".xml", d.GetType), SerializableDictionary(Of String, String))

            If _settings Is Nothing Then _settings = New SerializableDictionary(Of String, String)
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Sub Save()
            Try
                Serializer.Serialize("Plugin." & _PluginName & ".xml", _settings)
            Catch ex As Exception
                Log.LogError(ex)
            End Try
        End Sub

#End Region
    End Class
End Namespace
