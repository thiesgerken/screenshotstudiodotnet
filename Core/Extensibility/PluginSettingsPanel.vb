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

Imports ScreenshotStudioDotNet.Core.Settings

Namespace Extensibility
#If DEBUG Then

    Public Class PluginSettingsPanel
#Else
     Public MustInherit Class PluginSettingsPanel
#End If
        Inherits SettingsPanel

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PluginSettingsPanel" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

#End Region

#Region "Properties"

    
        ''' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public Overrides ReadOnly Property DisplayName As String
            Get
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the sub panels.
        ''' </summary>
        ''' <value>The sub panels.</value>
        Public Property SubPanels As New List(Of PluginSettingsPanel)

#End Region

     End Class
End Namespace
