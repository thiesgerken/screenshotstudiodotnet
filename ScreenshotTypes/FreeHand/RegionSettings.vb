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

Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace FreeHand
    Public Class FreeHandSettings
        Inherits PluginSettingsBase

        Public Sub New()
            Me.PluginName = "FreeHand"
        End Sub

        ''' <summary>
        ''' Gets or sets a value indicating whether [show magnifying glass].
        ''' </summary>
        ''' <value><c>true</c> if [show magnifying glass]; otherwise, <c>false</c>.</value>
        Public Property ShowMagnifyingGlass() As Boolean
            Get
                Return CBool(GetSetting("ShowMagnifyingGlass", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("ShowMagnifyingGlass") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [paint crosshair].
        ''' </summary>
        ''' <value><c>true</c> if [paint crosshair]; otherwise, <c>false</c>.</value>
        Public Property PaintCrosshair() As Boolean
            Get
                Return CBool(GetSetting("PaintCrosshair", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("PaintCrosshair") = CStr(value)
            End Set
        End Property
    End Class
End Namespace
