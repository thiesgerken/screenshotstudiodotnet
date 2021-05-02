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

Namespace Window
    Public Class WindowSettings
        Inherits PluginSettingsBase

        ''' <summary>
        ''' Initializes a new instance of the <see cref="WindowSettings" /> class.
        ''' </summary>
        Public Sub New()
            Me.PluginName = "Window"
        End Sub

        ''' <summary>
        ''' Gets or sets a value indicating whether to improve the window shots via a white form.
        ''' Default value is "True".
        ''' </summary>
        ''' <value><c>true</c> if [use white form]; otherwise, <c>false</c>.</value>
        Public Property WhiteFormEnabled() As Boolean
            Get
                Return CBool(GetSetting("WhiteFormEnabled", (New Version(My.Computer.Info.OSVersion).Major >= 6).ToString))
            End Get
            Set(ByVal value As Boolean)
                Settings("WhiteFormEnabled") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Determines if the window selected should be focused before creating the screenshot.
        ''' Default value is "True".
        ''' </summary>
        ''' <value><c>true</c> if focus; otherwise, <c>false</c>.</value>
        Public Property Focus() As Boolean
            Get
                Return CBool(GetSetting("Focus", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("Focus") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the correction left.
        ''' </summary>
        ''' <value>The correction left.</value>
        Public Property CorrectionLeft() As Integer
            Get
                Return CInt(GetSetting("CorrectionLeft", "0"))
            End Get
            Set(ByVal value As Integer)
                Settings("CorrectionLeft") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the correction right.
        ''' </summary>
        ''' <value>The correction right.</value>
        Public Property CorrectionRight() As Integer
            Get
                Return CInt(GetSetting("CorrectionRight", "0"))
            End Get
            Set(ByVal value As Integer)
                Settings("CorrectionRight") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the correction top.
        ''' </summary>
        ''' <value>The correction top.</value>
        Public Property CorrectionTop() As Integer
            Get
                Return CInt(GetSetting("CorrectionTop", "0"))
            End Get
            Set(ByVal value As Integer)
                Settings("CorrectionTop") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the correction bottom.
        ''' </summary>
        ''' <value>The correction bottom.</value>
        Public Property CorrectionBottom() As Integer
            Get
                Return CInt(GetSetting("CorrectionBottom", "0"))
            End Get
            Set(ByVal value As Integer)
                Settings("CorrectionBottom") = CStr(value)
            End Set
        End Property
    End Class
End Namespace
