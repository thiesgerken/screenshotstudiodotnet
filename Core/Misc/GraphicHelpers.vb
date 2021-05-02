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
Imports System.Drawing.Drawing2D

Namespace Misc
    Public NotInheritable Class GraphicHelpers

#Region "Functions"


        ''' <summary>
        ''' Contructs a graphics path containing a rounded rectangle.
        ''' </summary>
        ''' <param name="baseRect">The rectangle to fit the rounded rectangle into.</param>
        ''' <param name="radius">The radius of the corner arcs.</param>
        ''' <returns>A graphics path contaning the rounded rectangle.</returns>
        ''' <remarks></remarks>
        Public Shared Function GetRoundedRectangle(ByVal baseRect As RectangleF, ByVal radius As Single) As GraphicsPath
            If radius <= 0.0F Then
                Dim mPath As New GraphicsPath
                mPath.AddRectangle(baseRect)
                mPath.CloseFigure()
                Return mPath
            End If
            If radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2 Then
                Return GetCapsule(baseRect)
            End If
            Dim diameter As Single = radius * 2.0F
            Dim sizeF As SizeF = New SizeF(diameter, diameter)
            Dim arc As RectangleF = New RectangleF(baseRect.Location, sizeF)
            Dim path As GraphicsPath = New GraphicsPath
            path.AddArc(arc, 180, 90)
            arc.X = baseRect.Right - diameter
            path.AddArc(arc, 270, 90)
            arc.Y = baseRect.Bottom - diameter
            path.AddArc(arc, 0, 90)
            arc.X = baseRect.Left
            path.AddArc(arc, 90, 90)
            path.CloseFigure()
            Return path
        End Function

        ''' <summary>
        ''' Gets the capsule.
        ''' </summary>
        ''' <param name="baseRect">The base rect.</param>
        ''' <returns></returns>
        Private Shared Function GetCapsule(ByVal baseRect As RectangleF) As GraphicsPath
            Dim diameter As Single
            Dim arc As RectangleF
            Dim path As New GraphicsPath
            Try
                If baseRect.Width > baseRect.Height Then
                    diameter = baseRect.Height
                    Dim sizeF As SizeF = New SizeF(diameter, diameter)
                    arc = New RectangleF(baseRect.Location, sizeF)
                    path.AddArc(arc, 90, 180)
                    arc.X = baseRect.Right - diameter
                    path.AddArc(arc, 270, 180)
                Else
                    If baseRect.Width < baseRect.Height Then
                        diameter = baseRect.Width
                        Dim sizeF As SizeF = New SizeF(diameter, diameter)
                        arc = New RectangleF(baseRect.Location, sizeF)
                        path.AddArc(arc, 180, 180)
                        arc.Y = baseRect.Bottom - diameter
                        path.AddArc(arc, 0, 180)
                    Else
                        path.AddEllipse(baseRect)
                    End If
                End If
            Catch ex As Exception
                path.AddEllipse(baseRect)
            Finally
                path.CloseFigure()
            End Try
            Return path
        End Function

#End Region
    End Class
End Namespace
