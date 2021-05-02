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

Namespace Misc
    Public Class MouseUnderground

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MouseUnderground" /> class.
        ''' </summary>
        ''' <param name="type">The type.</param>
        ''' <param name="macroName">Name of the macro.</param>
        Public Sub New(ByVal type As MouseUndergroundTypes, ByVal macroName As String)
            _Type = type
            _MacroName = macroName
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MouseUnderground" /> class.
        ''' </summary>
        ''' <param name="type">The type.</param>
        Public Sub New(ByVal type As MouseUndergroundTypes)
            _Type = type
            _MacroName = ""
        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the type.
        ''' </summary>
        ''' <value>The type.</value>
        Public Property Type() As MouseUndergroundTypes

        ''' <summary>
        ''' Gets or sets the name of the macro.
        ''' </summary>
        ''' <value>The name of the macro.</value>
        Public Property MacroName() As String

#End Region
    End Class
End Namespace
