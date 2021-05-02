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
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.ScreenshotTypes.Selector
Imports System.Windows.Forms

Namespace Region
    Public Class Region
        Implements IScreenshotType

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.ScreenshotTypes.Strings", Assembly.GetExecutingAssembly)

        ''' <summary>
        ''' Creates the screenshot.
        ''' </summary>
        ''' <param name="parameters">The parameters.</param>
        ''' <returns></returns>
        Public Function CreateScreenshot(ByVal parameters As Macro) As Screenshot Implements IScreenshotType.CreateScreenshot
            Dim reg As New SelectRegion

            reg.Macro = parameters
            reg.Fullscreen = ScreenshotCreator.TrimImage((New FullScreen.FullScreen).CreateScreenshot(parameters).Screenshot)
            reg.Mode = RegionTypes.Region

            reg.ShowDialog()
            Return reg.Screenshot
        End Function

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description() As String Implements IPlugin.Description
            Get
                Return _langManager.GetString("regionDescription")
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName() As String Implements IPlugin.DisplayName
            Get
                Return _langManager.GetString("regionName")
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel() As PluginSettingsPanel Implements IPlugin.SettingsPanel
            Get
                Return New RegionSettingsPanel
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name() As String Implements IPlugin.Name
            Get
                Return "Region"
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether this plugin is available.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is available; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsAvailable() As Boolean Implements IPlugin.IsAvailable
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether its okay to add this plugin more than one time to a macro.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is supported multiple times; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.IPlugin.IsSupportedMultipleTimes
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Returns a <see cref="System.String" /> of the arguments.
        ''' </summary>
        ''' <param name="arguments">The arguments.</param>
        ''' <returns>
        ''' A <see cref="System.String" /> that represents this instance.
        ''' </returns>
        Public Function ArgumentsToString(ByVal arguments As System.Collections.Generic.Dictionary(Of String, Object)) As String Implements Core.Extensibility.IArgumentPlugin.ArgumentsToString
            Return ""
        End Function

        ''' <summary>
        ''' Gets the argument designer.
        ''' </summary>
        ''' <value>The argument designer.</value>
        Public ReadOnly Property ArgumentDesigner As PluginArgumentDesigner Implements IArgumentPlugin.ArgumentDesigner
            Get
                Return Nothing
            End Get
        End Property
    End Class
End Namespace
