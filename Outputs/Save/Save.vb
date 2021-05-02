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
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Misc
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Settings
Imports System.Windows.Forms

Namespace Save
    Public Class Save
        Implements IOutput

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Outputs.General.Strings", Assembly.GetExecutingAssembly)

#Region "Overridden Members"

        ''' <summary>
        ''' Proceeds the specified screenshot.
        ''' </summary>
        ''' <param name="screenshot"></param>
        Public Function Proceed(ByVal screenshot As Screenshot) As String Implements IOutput.Proceed
            Dim filename As String = Path.Combine(SettingsDatabase.FilePath, FileNameMaskProcessor.ProcessMask(screenshot))

            Dim i As Integer = 1
            While File.Exists(filename)
                filename = Path.Combine(SettingsDatabase.FilePath, FileNameMaskProcessor.ProcessMask(screenshot, False) & " (" & i & ")." & FileNameMaskProcessor.GetFileSuffix(SettingsDatabase.FileFormat))
                i += 1
            End While

            screenshot.Screenshot.Save(filename, SettingsDatabase.FileFormat)

            Log.LogInformation("Screenshot saved with filename: " & filename)

            Return filename
        End Function

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description() As String Implements IPlugin.Description
            Get
                Return _langManager.GetString("saveDesc")
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName() As String Implements IPlugin.DisplayName
            Get
                Return _langManager.GetString("saveName")
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel() As PluginSettingsPanel Implements IPlugin.SettingsPanel
            Get
                'Has no settings
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name() As String Implements IPlugin.Name
            Get
                Return "Save"
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether this plugin is available.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is available; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsAvailable() As Boolean Implements IOutput.IsAvailable
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
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.IOutput.IsSupportedMultipleTimes
            Get
                Return True
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
#End Region

    End Class
End Namespace
