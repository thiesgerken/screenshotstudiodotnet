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

Imports System.Web
Imports System.Xml.Linq

Namespace Imageshack
    Public NotInheritable Class BitlyApi
        Private Shared login As String = "screenshotstudiodotnet"
        Private Shared apiKey As String = "R_f6ad15ed5852021a1b328fcd1ea41891"

        ''' <summary>
        ''' Shortens the URL.
        ''' </summary>
        ''' <param name="longUrl">The long URL.</param>
        ''' <returns></returns>
        Public Shared Function ShortenUrl(ByVal longUrl As String) As BitlyResults
            Dim url = String.Format("http://api.bit.ly/shorten?format=xml&version=2.0.1&longUrl={0}&login={1}&apiKey={2}", HttpUtility.UrlEncode(longUrl), login, apiKey)
            Dim resultXml = XDocument.Load(url)
            Dim x = (From result In resultXml.Descendants("nodeKeyVal") Select New BitlyResults(result.Element("userHash").Value, result.Element("shortUrl").Value))
            Return x.[Single]()
        End Function
    End Class
End Namespace
