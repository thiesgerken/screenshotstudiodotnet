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

Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Macros

Namespace Screenshots
    Public Class Screenshot

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the shape.
        ''' </summary>
        ''' <value>The shape.</value>
        Public Property Shape() As Shape

        ''' <summary>
        ''' Gets or sets the window title.
        ''' </summary>
        ''' <value>The window title.</value>
        Public Property WindowTitle() As String

        ''' <summary>
        ''' Gets or sets the name of the process.
        ''' </summary>
        ''' <value>The name of the process.</value>
        Public Property ProcessName() As String

        ''' <summary>
        ''' Gets or sets the bounds.
        ''' </summary>
        ''' <value>The bounds.</value>
        Public Property Bounds() As Rectangle

        ''' <summary>
        ''' Gets or sets the date taken.
        ''' </summary>
        ''' <value>The date taken.</value>
        Public Property DateTaken() As Date

        ''' <summary>
        ''' Gets or sets the name of the macro.
        ''' </summary>
        ''' <value>The name of the macro.</value>
        Public Property MacroName() As String

        ''' <summary>
        ''' Gets or sets the macro.
        ''' </summary>
        ''' <value>The macro.</value>
        Public Property Macro As Macro

        ''' <summary>
        ''' Gets or sets the type of the screenshot.
        ''' </summary>
        ''' <value>The type of the screenshot.</value>
        Public Property ScreenshotType() As String

        ''' <summary>
        ''' Gets or sets the total number.
        ''' </summary>
        ''' <value>The total number.</value>
        Public Property TotalNumber() As Integer

        ''' <summary>
        ''' Gets or sets the number.
        ''' </summary>
        ''' <value>The number.</value>
        Public Property Number() As Integer

        ''' <summary>
        ''' Gets or sets the screenshot.
        ''' </summary>
        ''' <value>The screenshot.</value>
        Public Property Screenshot() As Bitmap

        ''' <summary>
        ''' Gets or sets the website URL.
        ''' </summary>
        ''' <value>The website URL.</value>
        Public Property WebsiteUrl() As String

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Screenshot" /> class.
        ''' </summary>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <param name="bounds">The bounds.</param>
        ''' <param name="dateTaken">The date taken.</param>
        ''' <param name="totalNumber">The total number.</param>
        ''' <param name="screenshotType">Type of the screenshot.</param>
        ''' <param name="macroName">Name of the macro.</param>
        Public Sub New(ByVal screenshot As Bitmap, ByVal bounds As Rectangle, ByVal dateTaken As Date, ByVal totalNumber As Integer, ByVal screenshotType As String, ByVal macroName As String, ByVal macro As Macro)
            _Bounds = bounds
            _DateTaken = dateTaken
            _Number = Number
            _TotalNumber = totalNumber
            _ScreenshotType = screenshotType
            _MacroName = macroName
            _Macro = macro
            _Screenshot = screenshot
        End Sub

#End Region
    End Class
End Namespace
