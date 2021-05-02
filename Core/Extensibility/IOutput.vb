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

Imports ScreenshotStudioDotNet.Core.Screenshots

Namespace Extensibility
    Public Interface IOutput
        Inherits IArgumentPlugin
        ''' <summary>
        ''' Proceeds the specified screenshot.
        ''' </summary>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <returns>additional information about the output, e.g. the filename where it was saved</returns>
        Function Proceed(ByVal screenshot As Screenshot) As String
   End Interface
End Namespace
