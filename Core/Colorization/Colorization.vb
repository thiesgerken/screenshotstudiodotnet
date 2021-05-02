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

Namespace Colorization
    Public Class Colorization

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public Property Name As String

        ''' <summary>
        ''' Gets or sets the hovered button color
        ''' </summary>
        ''' <value>The hovered.</value>
        Public Property Hovered As String

        ''' <summary>
        ''' Gets or sets the normal button color
        ''' </summary>
        ''' <value>The normal.</value>
        Public Property Normal As String

        ''' <summary>
        ''' Gets or sets mouse down button color
        ''' </summary>
        ''' <value>Down.</value>
        Public Property Down As String

#End Region

#Region "Constrcutors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Colorization" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Colorization" /> class.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="normalColor">Color of the normal.</param>
        ''' <param name="hoveredColor">Color of the hovered.</param>
        ''' <param name="downColor">Down color.</param>
        Public Sub New(ByVal name As String, ByVal normalColor As Color, ByVal hoveredColor As Color, ByVal downColor As Color)
            _Name = name

            Dim a As New ColorConverter
            _Normal = a.ConvertToString(normalColor)
            _Hovered = a.ConvertToString(hoveredColor)
            _Down = a.ConvertToString(downColor)
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Gets the color of the normal.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetNormalColor() As Color
            Dim a As New ColorConverter
            Return CType(a.ConvertFromString(_Normal), Color)
        End Function

        ''' <summary>
        ''' Gets the color of the hovered.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetHoveredColor() As Color
            Dim a As New ColorConverter
            Return CType(a.ConvertFromString(_Hovered), Color)
        End Function

        ''' <summary>
        ''' Gets down color.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetDownColor() As Color
            Dim a As New ColorConverter
            Return CType(a.ConvertFromString(_Down), Color)
        End Function

        ''' <summary>
        ''' Sets the color of the normal.
        ''' </summary>
        ''' <param name="value">The value.</param>
        Public Sub SetNormalColor(ByVal value As Color)
            Dim a As New ColorConverter
            _Normal = a.ConvertToString(value)
        End Sub

        ''' <summary>
        ''' Sets the color of the hovered.
        ''' </summary>
        ''' <param name="value">The value.</param>
        Public Sub SetHoveredColor(ByVal value As Color)
            Dim a As New ColorConverter
            _Hovered = a.ConvertToString(value)
        End Sub

        ''' <summary>
        ''' Sets down color.
        ''' </summary>
        ''' <param name="value">The value.</param>
        Public Sub SetDownColor(ByVal value As Color)
            Dim a As New ColorConverter
            _Down = a.ConvertToString(value)
        End Sub

        ''' <summary>
        ''' Returns a <see cref="System.String" /> that represents this instance.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="System.String" /> that represents this instance.
        ''' </returns>
        Public Overrides Function ToString() As String
            Return Name & Normal & Hovered & Down
        End Function

#End Region
    End Class
End Namespace
