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

Imports ScreenshotStudioDotNet.Core.Macros

Namespace Hotkey
    ''' <summary>
    ''' Provides data for HotkeyManager events.
    ''' </summary>
    Public Class HotkeyPressedEventArgs
        Inherits EventArgs

#Region "Constructor"

        ''' <param name="hk">The HotkeyManager.Hotkey that contains the hot key information.</param>
        ''' <param name="hwnd">The window handle of the message.</param>
        Sub New(ByVal hotkey As Hotkey)
            Me.Hotkey = hotkey
        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the macro.
        ''' </summary>
        ''' <value>The macro.</value>
        Public Property Hotkey() As Hotkey

#End Region
    End Class
End Namespace
