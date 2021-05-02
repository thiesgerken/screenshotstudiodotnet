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

Namespace Controls
    Friend Class NativeMethods

#Region "Constants"

        Friend Shared ECM_FIRST As UInteger = &H1500
        Friend Shared EM_SETCUEBANNER As UInteger = CUInt(ECM_FIRST + 1)

#End Region

#Region "Function Imports"

        Friend Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr

#End Region

#Region "Private Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="NativeMethods" /> class.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region
    End Class
End Namespace
