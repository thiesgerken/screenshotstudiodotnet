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

Namespace Imageshack
    Public Class BitlyResults

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the user hash.
        ''' </summary>
        ''' <value>The user hash.</value>
        Public Property UserHash() As String

        ''' <summary>
        ''' Gets or sets the short URL.
        ''' </summary>
        ''' <value>The short URL.</value>
        Public Property ShortUrl() As String

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="BitlyResults" /> class.
        ''' </summary>
        ''' <param name="userHash">The user hash.</param>
        ''' <param name="shortUrl">The short URL.</param>
        Public Sub New(ByVal userHash As String, ByVal shortUrl As String)
            _UserHash = userHash
            _ShortUrl = shortUrl
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="BitlyResults" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

#End Region
    End Class
End Namespace
