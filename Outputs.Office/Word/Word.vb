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
Imports System.Drawing.Imaging
Imports System.IO
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports Microsoft.Office.Interop.Word
Imports System.Windows.Forms

Namespace Word
    Public Class Word
        Implements IOutput

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Outputs.Office.Strings", Assembly.GetExecutingAssembly)

#Region "Overridden Members"

        ''' <summary>
        ''' Proceeds the specified screenshot.
        ''' </summary>
        ''' <param name="screenshot"></param>
        Public Function Proceed(ByVal screenshot As Screenshot) As String Implements IOutput.Proceed
            'Todo: Setting: New Instance or Select Instance
            Dim app As New Microsoft.Office.Interop.Word.Application()
            app.Visible = True
            app.Activate()
            Dim doc = app.Documents.Add()

            'Get a temp filename and save the picture
            Dim filename = My.Computer.FileSystem.GetTempFileName()
            screenshot.Screenshot.Save(filename, ImageFormat.Jpeg)
            'Todo: Format by setting

            Dim inlineShapes As InlineShapes = doc.InlineShapes
            inlineShapes.AddPicture(filename)
            Return ""
        End Function

        ''' <summary> 
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description() As String Implements IPlugin.Description
            Get
                Return _langManager.GetString("wordDescription")
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName() As String Implements IPlugin.DisplayName
            Get
                Return "Word"
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel() As PluginSettingsPanel Implements IPlugin.SettingsPanel
            Get
                'has no settings
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name() As String Implements IPlugin.Name
            Get
                Return "Word"
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
                'Todo: Determine if Word 2007 is installed
                Return True
            End Get
        End Property

#End Region

        ''' <summary>
        ''' Gets a value indicating whether its okay to add this plugin more than one time to a macro.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is supported multiple times; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.IOutput.IsSupportedMultipleTimes
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
