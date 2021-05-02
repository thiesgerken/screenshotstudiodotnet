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

Namespace Settings
#If DEBUG Then

    Public Class SettingsPanel
        ''' <summary>
        ''' Saves the settings of this plugin
        ''' </summary>
        Public Overridable Sub Save()
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether the user has changed the properties.
        ''' The Save() function will only be called if this is <c>true</c>!
        ''' (See Comment in SettingsForm.Save for the reason of this behaviour)
        ''' </summary>
        ''' <value><c>true</c> if the properties were changed; otherwise, <c>false</c>.</value>
        Public Overridable ReadOnly Property PropertiesChanged() As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public Overridable ReadOnly Property DisplayName As String
            Get
                Return ""
            End Get
        End Property

#Else
        Public MustInherit Class SettingsPanel
        ''' <summary>
        ''' Saves the settings of this plugin
        ''' </summary>
        Public MustOverride Sub Save()

        ''' <summary>
        ''' Gets a value indicating whether the user has changed the properties.
        ''' The Save() function will only be called if this is <c>true</c>!
        ''' (See Comment in SettingsForm.Save for the reason of this behaviour)
        ''' </summary>
        ''' <value><c>true</c> if the properties were changed; otherwise, <c>false</c>.</value>
        Public MustOverride ReadOnly Property PropertiesChanged() As Boolean

        ''' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public MustOverride ReadOnly Property DisplayName As String

#End If

        ''' <summary>
        ''' Gets a value indicating whether this instance is empty.
        ''' </summary>
        ''' <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        Public ReadOnly Property IsEmpty() As Boolean
            Get
                For Each c In Me.Controls
                    Return False
                Next

                Return True
            End Get
        End Property
    End Class
End Namespace
