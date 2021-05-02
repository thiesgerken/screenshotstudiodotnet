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

Namespace Email
    Public Class EmailSettings
        Inherits PluginSettingsBase

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmailSettings" /> class.
        ''' </summary>
        Public Sub New()
            Me.PluginName = "Email"
        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the subject.
        ''' </summary>
        ''' <value>The subject.</value>
        Public Property Subject() As String
            Get
                Return GetSetting("Subject", "Screenshot")
            End Get
            Set(ByVal value As String)
                Settings("Subject") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the body.
        ''' </summary>
        ''' <value>The body.</value>
        Public Property Body() As String
            Get
                Return GetSetting("Body", "Screenshot Taken with ScreenshotStudio.Net 6")
            End Get
            Set(ByVal value As String)
                Settings("Body") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the address.
        ''' </summary>
        ''' <value>The address.</value>
        Public Property Address() As String
            Get
                Return GetSetting("Address", "user@server.com")
            End Get
            Set(ByVal value As String)
                Settings("Address") = value
            End Set
        End Property

#End Region
    End Class
End Namespace
