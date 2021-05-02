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
Imports System.Windows.Forms

Namespace Hotkey
    ''' <summary>
    ''' Represents win32 Api shared methods, structures, and constants.
    ''' </summary>
    Friend NotInheritable Class NativeMethods

#Region "Private Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="NativeMethods" /> class.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region "Function Imports"

        ''' <summary>
        ''' Globals the add atom.
        ''' </summary>
        ''' <param name="IDString">The ID string.</param>
        ''' <returns></returns>
        <DllImport("kernel32", SetLastError:=True)>
 _
        Friend Shared Function GlobalAddAtom(ByVal IDString As String) As Integer
        End Function

        ''' <summary>
        ''' Globals the delete atom.
        ''' </summary>
        ''' <param name="Atom">The atom.</param>
        ''' <returns></returns>
        <DllImport("kernel32", SetLastError:=True)>
 _
        Friend Shared Function GlobalDeleteAtom(ByVal Atom As Integer) As Integer
        End Function

        ''' <summary>
        ''' Registers the hot key.
        ''' </summary>
        ''' <param name="hwnd">The HWND.</param>
        ''' <param name="id">The id.</param>
        ''' <param name="modifiers">The modifiers.</param>
        ''' <param name="key">The key.</param>
        ''' <returns></returns>
        <DllImport("user32", SetLastError:=True)>
 _
        Friend Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal modifiers As Integer, ByVal key As Keys) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>
        ''' Unregisters the hot key.
        ''' </summary>
        ''' <param name="hwnd">The HWND.</param>
        ''' <param name="id">The id.</param>
        ''' <returns></returns>
        <DllImport("user32", SetLastError:=True)>
 _
        Friend Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

#Region "Constants"

        Friend Shared WM_HOTKEY As Integer = 786

#End Region
    End Class
End Namespace
