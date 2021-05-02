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
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Website
    Public Class Website
        Implements IScreenshotType

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.ScreenshotTypes.Strings", Assembly.GetExecutingAssembly)

        ''' <summary>
        ''' Creates the screenshot.
        ''' </summary>
        ''' <param name="parameters">The parameters.</param>
        ''' <returns></returns>
        Public Function CreateScreenshot(ByVal parameters As Macro) As Screenshot Implements IScreenshotType.CreateScreenshot
            'Retrieve the url

            Dim url As String = CStr(parameters.Type.GetParameter("Website URL"))

            If url Is Nothing Or url = "" Then
                'Todo: Show Picker
                url = "http://www.thiesgerken.de"
            End If

            Dim wb As New WebBrowser()

            Try
                wb.ScrollBarsEnabled = False
            Catch ex As Exception
                'COMException occurs
            End Try
            wb.ScriptErrorsSuppressed = True
            wb.Navigate(url)

            'Wait while the page is loading
            'Todo: Show Progress Window
            While wb.ReadyState <> WebBrowserReadyState.Complete
                Application.DoEvents()
            End While

            wb.Width = wb.Document.Body.ScrollRectangle.Width
            wb.Height = wb.Document.Body.ScrollRectangle.Height

            'Save the page
            Dim bitmap As New Bitmap(wb.Width, wb.Height)
            wb.DrawToBitmap(bitmap, New Rectangle(0, 0, wb.Width, wb.Height))
            'Clean up
            wb.Dispose()

            Log.LogInformation("Website " & url & " captured")
            Dim s As New Screenshot(bitmap, New Rectangle(New Point(0, 0), bitmap.Size), Now, parameters.Multiple.Count, Me.DisplayName, parameters.Name, parameters)
            s.WebsiteUrl = url
            Return s
        End Function

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description() As String Implements IPlugin.Description
            Get
                Return _langManager.GetString("websiteDescription")
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName() As String Implements IPlugin.DisplayName
            Get
                Return _langManager.GetString("websiteName")
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel() As PluginSettingsPanel Implements IPlugin.SettingsPanel
            Get
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name() As String Implements IPlugin.Name
            Get
                Return "Website"
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
                'Todo: Check internet access
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
