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

Namespace Screenshots
    Public Class MultipleParameters

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the count of screenshots that should be taken.
        ''' </summary>
        ''' <value>The count.</value>
        Public Property Count() As Integer

        ''' <summary>
        ''' Gets or sets the interval.
        ''' </summary>
        ''' <value>The interval, in miliseconds.</value>
        Public Property Interval() As Integer

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MultipleParameters" /> struct.
        ''' </summary>
        ''' <param name="count">The count.</param>
        ''' <param name="interval">The interval.</param>
        Public Sub New(ByVal count As Integer, ByVal interval As Integer)
            _Count = count
            _Interval = interval
        End Sub

        Public Sub New()
            _Count = 1
            _Interval = 1
        End Sub

#End Region
    End Class
End Namespace
