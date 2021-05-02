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

Imports ScreenshotStudioDotNet.Core.Extensibility
Imports System.Drawing

Namespace Watermark
    Public Class Watermark
        Implements IEffect

        'Todo: Watermark

        ''' <summary>
        ''' Gets a list of argument names that can be passed to the plugin (Name, Type)
        ''' </summary>
        ''' <value>The optional arguments.</value>
        Public ReadOnly Property OptionalArguments As System.Collections.Generic.Dictionary(Of String, String) Implements Core.Extensibility.IEffect.OptionalArguments
            Get
                Return New Dictionary(Of String, String)
            End Get
        End Property

        ''' <summary>
        ''' Proceeds the specified screenshot.
        ''' </summary>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <returns></returns>
        Public Function Proceed(ByVal screenshot As Core.Screenshots.Screenshot) As Core.Screenshots.Screenshot Implements Core.Extensibility.IEffect.Proceed
            Dim g As Graphics = Graphics.FromImage(screenshot.Screenshot)

            g.FillRectangle(Brushes.Black, New Rectangle(10, 10, 100, 100))

            g.Dispose()
            Return screenshot
        End Function

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description As String Implements Core.Extensibility.IPlugin.Description
            Get
                Return "Puts a text on the Screenshot" 'Todo: Loc
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName As String Implements Core.Extensibility.IPlugin.DisplayName
            Get
                Return "Watermark" 'Todo: Loc
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether this plugin is available.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is available; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsAvailable As Boolean Implements Core.Extensibility.IPlugin.IsAvailable
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name As String Implements Core.Extensibility.IPlugin.Name
            Get
                Return "Watermark"
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel As Core.Extensibility.PluginSettingsPanel Implements Core.Extensibility.IPlugin.SettingsPanel
            Get
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether its okay to add this plugin more than one time to a macro.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is supported multiple times; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.IEffect.IsSupportedMultipleTimes
            Get
                Return True
            End Get
        End Property
    End Class
End Namespace
