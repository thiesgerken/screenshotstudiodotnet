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

Namespace Aero
    Public Class Margins

#Region "Fields"

        Private _left As Integer
        Private _right As Integer
        Private _top As Integer
        Private _bottom As Integer

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the bottom.
        ''' </summary>
        ''' <value>The bottom.</value>
        Public ReadOnly Property Bottom() As Integer
            Get
                Return _bottom
            End Get
        End Property

        ''' <summary>
        ''' Gets the top.
        ''' </summary>
        ''' <value>The top.</value>
        Public ReadOnly Property Top() As Integer
            Get
                Return _top
            End Get
        End Property

        ''' <summary>
        ''' Gets the right.
        ''' </summary>
        ''' <value>The right.</value>
        Public ReadOnly Property Right() As Integer
            Get
                Return _right
            End Get
        End Property

        ''' <summary>
        ''' Gets the left.
        ''' </summary>
        ''' <value>The left.</value>
        Public ReadOnly Property Left() As Integer
            Get
                Return _left
            End Get
        End Property

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Margins" /> class.
        ''' </summary>
        ''' <param name="left">The left.</param>
        ''' <param name="right">The right.</param>
        ''' <param name="top">The top.</param>
        ''' <param name="bottom">The bottom.</param>
        Public Sub New(ByVal left As Integer, ByVal right As Integer, ByVal top As Integer, ByVal bottom As Integer)
            _left = left
            _right = right
            _top = top
            _bottom = bottom
        End Sub

#End Region
    End Class
End Namespace
