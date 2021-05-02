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
Imports System.Windows.Forms

Namespace Screenshots
    Public NotInheritable Class ScreenHelpers

#Region "Functions"

        ''' <summary>
        ''' Gets the fitting bitmap.
        ''' </summary>
        ''' <param name="screens">The screens.</param>
        ''' <returns></returns>
        Public Shared Function GetFittingRectangle(ByVal screens As List(Of String)) As Rectangle
            Dim allBounds As List(Of Rectangle) = GetBounds(screens)

            Dim left, top, right, bottom As Integer

            left = -1
            top = -1
            right = -1
            bottom = -1

            For Each rct In allBounds
                If left > rct.Left Or left = -1 Then left = rct.Left
                If top > rct.Top Or top = -1 Then top = rct.Top
                If right < rct.Right Or right = -1 Then right = rct.Right
                If bottom < rct.Bottom Or bottom = -1 Then bottom = rct.Bottom
            Next

            Return New Rectangle(left, top, right - left, bottom - top)
        End Function

        ''' <summary>
        ''' Gets the bounds.
        ''' </summary>
        ''' <param name="screenID">The screen.</param>
        ''' <returns></returns>
        Public Shared Function GetBounds(ByVal screenID As String) As Rectangle
            For Each scr In Screen.AllScreens
                If screenID = scr.DeviceName Then
                    Return scr.Bounds
                End If
            Next

            Throw New Exception("The Screen could not be found")
        End Function

        ''' <summary>
        ''' Gets the bounds.
        ''' </summary>
        ''' <param name="screens">The screens.</param>
        ''' <returns></returns>
        Public Shared Function GetBounds(ByVal screens As List(Of String)) As List(Of Rectangle)
            Dim allBounds As New List(Of Rectangle)

            For Each scr In Screen.AllScreens
                If screens.Contains(scr.DeviceName) Then
                    allBounds.Add(scr.Bounds)
                End If
            Next

            Return allBounds
        End Function

        ''' <summary>
        ''' Gets all screens.
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetAllScreens() As List(Of String)
            Dim allScreens As New List(Of String)

            For Each scr In Screen.AllScreens
                allScreens.Add(scr.DeviceName)
            Next

            Return allScreens
        End Function

#End Region
    End Class
End Namespace
