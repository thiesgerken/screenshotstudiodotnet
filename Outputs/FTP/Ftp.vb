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
Imports System.IO
Imports ScreenshotStudioDotnet.Core.Screenshots
Imports ScreenshotStudioDotnet.Core.Extensibility

Public Class Ftp
    Implements IOutput
    'Todo: FTP
    Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Outputs.General.Strings", Assembly.GetExecutingAssembly)

    ''' <summary>
    ''' Proceeds the specified screenshot.
    ''' </summary>
    ''' <param name="screenshot"></param>
    Public Function Proceed(ByVal screenshot As Screenshot) As String Implements IOutput.Proceed
        Return ""
    End Function

    ''' <summary>
    ''' Gets the description.
    ''' </summary>
    ''' <value>The description.</value>
    Public ReadOnly Property Description() As String Implements IPlugin.Description
        Get
            Return _langManager.GetString("ftpDesc")
        End Get
    End Property

    ''' <summary>
    ''' Gets the name.
    ''' </summary>
    ''' <value>The name.</value>
    Public ReadOnly Property DisplayName() As String Implements IPlugin.DisplayName
        Get
            Return _langManager.GetString("ftpName")
        End Get
    End Property

    ''' <summary>
    ''' Gets the settings panel.
    ''' </summary>
    ''' <value>The settings panel.</value>
    Public ReadOnly Property SettingsPanel() As PluginSettingsPanel Implements IPlugin.SettingsPanel
        Get
            Return New PluginSettingsPanel
        End Get
    End Property

    ''' <summary>
    ''' Gets the name.
    ''' </summary>
    ''' <value>The name.</value>
    Public ReadOnly Property Name() As String Implements Core.Extensibility.IPlugin.Name
        Get
            Return "Ftp"
        End Get
    End Property

    ''' <summary>
    ''' Gets the icon.
    ''' </summary>
    ''' <value>The icon.</value>
    Public ReadOnly Property IconPath() As String Implements Core.Extensibility.IPlugin.IconPath
        Get
            Return Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(Me.GetType).Location), "icons\ftp.ico")
        End Get
    End Property
End Class
