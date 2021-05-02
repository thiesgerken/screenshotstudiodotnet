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

Namespace Screenshots
    Public Class ScreenshotTakenEventArgs
        Inherits EventArgs

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the screenshots total.
        ''' </summary>
        ''' <value>The screenshots total.</value>
        Public Property ScreenshotsTotal() As Integer

        ''' <summary>
        ''' Gets or sets the screenshots taken.
        ''' </summary>
        ''' <value>The screenshots taken.</value>
        Public Property ScreenshotsTaken() As Integer

        ''' <summary>
        ''' Gets or sets the screenshot.
        ''' </summary>
        ''' <value>The screenshot.</value>
        Public Property Screenshot() As Screenshot

        ''' <summary>
        ''' Gets or sets the output info.
        ''' </summary>
        ''' <value>The output info.</value>
        Public Property OutputInfo() As List(Of String)

        ''' <summary>
        ''' Gets or sets the output used.
        ''' </summary>
        ''' <value>The output used.</value>
        Public Property OutputsUsed() As List(Of Plugin(Of IOutput))

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ScreenshotTakenEventArgs" /> class.
        ''' </summary>
        ''' <param name="screenshotsTaken">The screenshots taken.</param>
        ''' <param name="screenshotsTotal">The screenshots total.</param>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <param name="outputUsed">The output used.</param>
        ''' <param name="outputInfo">The output info.</param>
        Public Sub New(ByVal screenshotsTaken As Integer, ByVal screenshotsTotal As Integer, ByVal screenshot As Screenshot, ByVal outputsUsed As List(Of Plugin(Of IOutput)), ByVal outputInfo As List(Of String))
            _ScreenshotsTaken = screenshotsTaken
            _ScreenshotsTotal = screenshotsTotal
            _Screenshot = screenshot
            _OutputsUsed = outputsUsed
            _OutputInfo = outputInfo
        End Sub

#End Region
    End Class
End Namespace
