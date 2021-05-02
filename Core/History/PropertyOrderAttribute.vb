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

Namespace History
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class PropertyOrderAttribute
        Inherits Attribute

        ''' <summary>
        ''' Gets or sets the order.
        ''' </summary>
        ''' <value>The order.</value>
        Public Property Order() As Integer

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PropertyOrderAttribute" /> class.
        ''' </summary>
        ''' <param name="order">The order.</param>
        Public Sub New(ByVal order As Integer)
            _order = order
        End Sub
    End Class
End Namespace
