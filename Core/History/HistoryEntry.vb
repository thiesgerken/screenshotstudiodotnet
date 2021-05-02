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
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace History
    Public Class HistoryEntry

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the additional information.
        ''' </summary>
        ''' <value>The additional information.</value>
        Public Property AdditionalInformation() As List(Of String)

        ''' <summary>
        ''' Gets or sets the output used.
        ''' </summary>
        ''' <value>The output used.</value>
        Public Property OutputsUsed() As List(Of Plugin(Of IOutput))

        ''' <summary>
        ''' Gets or sets the macro used.
        ''' </summary>
        ''' <value>The macro used.</value>
        Public Property MacroUsed() As Macro

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
        ''' Gets or sets the number.
        ''' </summary>
        ''' <value>The number.</value>
        Public Property Number() As Integer

        ''' <summary>
        ''' Gets or sets the total number.
        ''' </summary>
        ''' <value>The total number.</value>
        Public Property TotalNumber() As Integer

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="HistoryEntry" /> class.
        ''' </summary>
        Public Sub New()
            _MacroUsed = New Macro
            _OutputsUsed = New List(Of Plugin(Of IOutput))
            _AdditionalInformation = New List(Of String)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="HistoryEntry" /> class.
        ''' </summary>
        ''' <param name="macroUsed">The macro used.</param>
        ''' <param name="outputUsed">The output used.</param>
        ''' <param name="additionalInformation">The additional information.</param>
        ''' <param name="shape">The shape.</param>
        ''' <param name="windowTitle">The window title.</param>
        ''' <param name="processName">Name of the process.</param>
        ''' <param name="bounds">The bounds.</param>
        ''' <param name="dateTaken">The date taken.</param>
        ''' <param name="number">The number.</param>
        ''' <param name="totalNumber">The total number.</param>
        ''' <param name="websiteUrl">The website URL.</param>
        Public Sub New(ByVal macroUsed As Macro, ByVal outputUsed As List(Of Plugin(Of IOutput)), ByVal additionalInformation As List(Of String), ByVal bounds As Rectangle, ByVal dateTaken As Date, ByVal number As Integer, ByVal totalNumber As Integer)
            _MacroUsed = macroUsed
            _OutputsUsed = outputUsed
            _AdditionalInformation = additionalInformation
            _Bounds = bounds
            _DateTaken = dateTaken
            _Number = number
            _TotalNumber = totalNumber
        End Sub

#End Region
    End Class
End Namespace
