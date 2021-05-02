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

Imports System.Runtime.InteropServices

Namespace Selector
    ''' <summary>
    ''' P/Invokes for ScreenshotStudioDotNet.ScreenshotTypes
    ''' </summary>
    Friend Class NativeMethods
        ''' <summary>
        ''' Initializes a new instance of the <see cref="NativeMethods" /> class.
        ''' </summary>
        Private Sub New()
        End Sub

        Friend Declare Function EnumWindows Lib "user32.dll" (ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Long) As Boolean

        Friend Delegate Function EnumWindowsProc(ByVal hWnd As Integer, ByVal lParam As Integer) As Boolean

        Friend Declare Function GetWindowRect Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Boolean

        <StructLayout(LayoutKind.Sequential)> _
        Friend Structure RECT
            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer
        End Structure

        Friend Declare Function IsWindowVisible Lib "user32" (ByVal hWnd As Long) As Boolean
        Friend Declare Function WindowFromPoint Lib "user32" (ByVal x As Long, ByVal y As Long) As Long
        Friend Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hwnd As Long) As Integer
        Friend Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lp As String, ByVal cch As Long) As Long
        Friend Declare Function SetForegroundWindow Lib "user32" Alias "SetForegroundWindow" (ByVal hWnd As Long) As Boolean
        Friend Declare Function EnumChildWindows Lib "user32" (ByVal hWndParent As Long, ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Long) As Long
        Friend Declare Function EnumThreadWindows Lib "user32" (ByVal dwThreadId As Long, ByVal lpfn As EnumWindowsProc, ByVal lParam As Long) As Long
        Friend Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Long, ByVal lpdwProcessId As Long) As Long
    End Class
End Namespace
