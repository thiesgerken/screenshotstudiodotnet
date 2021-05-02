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

Imports System.Drawing.Imaging
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Colorization
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Settings
    Public Class SettingsDatabase

#Region "Settings Logic"

        Private Shared _settings As SerializableDictionary(Of String, String)
        'Name:Value

        ''' <summary>
        ''' Gets or sets the settings.
        ''' </summary>
        ''' <value>The setting.</value>
        Private Shared Property Settings(ByVal name As String) As String
            Get
                'Check if the settings are loaded (Singleton)
                If _settings Is Nothing Then Load()

                'Check if this setting is in the database
                If _settings.ContainsKey(name) Then
                    Return _settings(name)
                Else
                    Return ""
                End If
            End Get
            Set(ByVal value As String)
                'Check if the settings are loaded
                If _settings Is Nothing Then Load()

                'Write this setting
                _settings(name) = value

                If AutoSaveSettings Then Save()
            End Set
        End Property

        ''' <summary>
        ''' Gets the setting if it exists in the database and is not "".
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="defaultValue">The default value.</param>
        ''' <returns></returns>
        Public Shared Function GetSetting(ByVal name As String, ByVal defaultValue As String) As String
            If _settings Is Nothing Then Load()

            If _settings.ContainsKey(name) Then
                If Not _settings(name) = "" Then Return _settings(name)
            End If

            Return defaultValue
        End Function

        ''' <summary>
        ''' Empties the cache.
        ''' </summary>
        Public Shared Sub EmptyCache()
            _colorizationCache = Nothing
        End Sub

        ''' <summary>
        ''' Loads this instance.
        ''' </summary>
        Private Shared Sub Load()
            'Create dump dictionary to get the type of it
            Dim d As New SerializableDictionary(Of String, String)
            _settings = CType(Serializer.Deserialize("Settings", d.GetType, True), SerializableDictionary(Of String, String))

            If _settings Is Nothing Then _settings = New SerializableDictionary(Of String, String)
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Shared Sub Save()
            Try
                Serializer.Serialize("Settings", _settings)
            Catch ex As Exception
                Log.LogError(ex)
            End Try
        End Sub

#End Region

#Region "The Settings"

        ''' <summary>
        ''' Gets or sets a value indicating whether settings should be automatically saved when they have been changed.
        ''' Default value is "True".
        ''' </summary>
        ''' <value><c>true</c> if [auto save settings]; otherwise, <c>false</c>.</value>
        Public Shared Property AutoSaveSettings() As Boolean
            Get
                Return CBool(GetSetting("AutoSaveSettings", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("AutoSaveSettings") = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether this instance is first start.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is first start; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property IsFirstStart() As Boolean
            Get
                Return CBool(GetSetting("IsFirstStart", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("IsFirstStart") = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [keyboard selection enabled].
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [keyboard selection enabled]; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property KeyboardSelectionEnabled() As Boolean
            Get
                Return CBool(GetSetting("KeyboardSelectionEnabled", "False"))
            End Get
            Set(ByVal value As Boolean)
                Settings("KeyboardSelectionEnabled") = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the quickstart scale factor.
        ''' </summary>
        ''' <value>The quickstart scale factor.</value>
        Public Shared Property QuickStartScaleFactor() As Double
            Get
                Return CDbl(GetSetting("QuickStartScaleFactor", "1"))
            End Get
            Set(ByVal value As Double)
                Settings("QuickStartScaleFactor") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the standard file format used to save screenshots.
        ''' Default Format is PNG.
        ''' </summary>
        ''' <value>The file format.</value>
        Public Shared Property FileFormat() As ImageFormat
            Get
                Select Case Settings("FileFormat")
                    Case "png"
                        Return ImageFormat.Png
                    Case "jpg"
                        Return ImageFormat.Jpeg
                    Case "gif"
                        Return ImageFormat.Gif
                    Case "bmp"
                        Return ImageFormat.Bmp
                    Case "tiff"
                        Return ImageFormat.Tiff
                    Case Else
                        Return ImageFormat.Png
                End Select
            End Get
            Set(ByVal value As ImageFormat)
                Dim setting As String

                Select Case value.ToString
                    Case ImageFormat.Gif.ToString
                        setting = "gif"
                    Case ImageFormat.Tiff.ToString
                        setting = "tiff"
                    Case ImageFormat.Jpeg.ToString
                        setting = "jpg"
                    Case ImageFormat.Png.ToString
                        setting = "png"
                    Case ImageFormat.Bmp.ToString
                        setting = "bmp"
                    Case Else
                        setting = "png"
                End Select

                Settings("FileFormat") = setting
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the file path.
        ''' Default Path is the user's desktop.
        ''' </summary>
        ''' <value>The file path.</value>
        Public Shared Property FilePath() As String
            Get
                Return GetSetting("FilePath", My.Computer.FileSystem.SpecialDirectories.Desktop)
            End Get
            Set(ByVal value As String)
                Settings("FilePath") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the file mask.
        ''' Default Mask is "[type]_[date]_[time]".
        ''' </summary>
        ''' <value>The file mask.</value>
        Public Shared Property FileMask() As String
            Get
                Return GetSetting("FileMask", "[macro]@[date]_[time]((total:>1))[number]_of_[total]((/))((type:Region))_[shape]((/))((type:Window))_[process]_[windowtitle]((/))")
            End Get
            Set(ByVal value As String)
                Settings("FileMask") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the input mode while moving/resizing the region area.
        ''' Default value is "Absolute".
        ''' </summary>
        ''' <value>The input mode, either "Absolute" or "Relative".</value>
        Public Shared Property InputMode() As String
            Get
                Return GetSetting("InputMode", "Absolute")
            End Get
            Set(ByVal value As String)
                Settings("InputMode") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the last history filter.
        ''' </summary>
        ''' <value>The last history filter.</value>
        Public Shared Property LastHistoryFilter() As String
            Get
                Return GetSetting("LastHistoryFilter", "")
            End Get
            Set(ByVal value As String)
                Settings("LastHistoryFilter") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [show right click info].
        ''' Default value is "True".
        ''' </summary>
        ''' <value><c>true</c> if [show right click info]; otherwise, <c>false</c>.</value>
        Public Shared Property ShowRightClickInfo() As Boolean
            Get
                Return CBool(GetSetting("ShowRightClickInfo", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("ShowRightClickInfo") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [refresh screenshot before capturing].
        ''' Default value is "True".
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [refresh screenshot before capturing]; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property RefreshScreenshotBeforeCapturing() As Boolean
            Get
                Return CBool(GetSetting("RefreshScreenshotBeforeCapturing", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("RefreshScreenshotBeforeCapturing") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [fill screenshot background enabled].
        ''' Default value is "False".
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [fill screenshot background enabled]; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property FillScreenshotBackgroundEnabled() As Boolean
            Get
                Return CBool(GetSetting("FillScreenshotBackgroundEnabled", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("FillScreenshotBackgroundEnabled") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [show quick start on startup].
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [show quick start on startup]; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property ShowQuickStartOnStartup() As Boolean
            Get
                Return CBool(GetSetting("ShowQuickStartOnStartup", "False"))
            End Get
            Set(ByVal value As Boolean)
                Settings("ShowQuickStartOnStartup") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [ask for history clean].
        ''' </summary>
        ''' <value><c>true</c> if [ask for history clean]; otherwise, <c>false</c>.</value>
        Public Shared Property AskForHistoryClean() As Boolean
            Get
                Return CBool(GetSetting("AskForHistoryClean", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("AskForHistoryClean") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [ask for log clean].
        ''' </summary>
        ''' <value><c>true</c> if [ask for log clean]; otherwise, <c>false</c>.</value>
        Public Shared Property AskForLogClean() As Boolean
            Get
                Return CBool(GetSetting("AskForLogClean", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("AskForLogClean") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [ask for history delete item].
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [ask for history delete item]; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property AskForHistoryDeleteItem() As Boolean
            Get
                Return CBool(GetSetting("AskForHistoryDeleteItem", "True"))
            End Get
            Set(ByVal value As Boolean)
                Settings("AskForHistoryDeleteItem") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [show cursor on screenshot].
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [show cursor on screenshot]; otherwise, <c>false</c>.
        ''' </value>
        Public Shared Property ShowCursorOnScreenshot() As Boolean
            Get
                Return CBool(GetSetting("ShowCursorOnScreenshot", "False"))
            End Get
            Set(ByVal value As Boolean)
                Settings("ShowCursorOnScreenshot") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the color of the fill screenshot background.
        ''' Default value is "White".
        ''' </summary>
        ''' <value>The color of the fill screenshot background.</value>
        Public Shared Property FillScreenshotBackgroundColor() As Color
            Get
                Return Color.FromName((GetSetting("FillScreenshotBackgroundColor", "White")))
            End Get
            Set(ByVal value As Color)
                Settings("FillScreenshotBackgroundColor") = value.ToKnownColor.ToString()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the length of the break.
        ''' Default value is "250".
        ''' </summary>
        ''' <value>The length of the break.</value>
        Public Shared Property BreakLength() As Integer
            Get
                Return CInt(GetSetting("BreakLength", "250"))
            End Get
            Set(ByVal value As Integer)
                Settings("BreakLength") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the history auto clean age.
        ''' </summary>
        ''' <value>The history auto clean age in days. 0 means never auto clean because of the screenshot age</value>
        Public Shared Property HistoryAutoCleanAge() As Integer
            Get
                Return CInt(GetSetting("HistoryAutoCleanAge", "100"))
            End Get
            Set(ByVal value As Integer)
                Settings("HistoryAutoCleanAge") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the log auto clean age.
        ''' </summary>
        ''' <value>The log auto clean age in days. 0 means never auto clean because of the log entry age</value>
        Public Shared Property LogAutoCleanAge() As Integer
            Get
                Return CInt(GetSetting("LogAutoCleanAge", "100"))
            End Get
            Set(ByVal value As Integer)
                Settings("LogAutoCleanAge") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the log max item count.
        ''' </summary>
        ''' <value>the maximum log entry count. 0 means no maximum.</value>
        Public Shared Property LogMaxItemCount() As Integer
            Get
                Return CInt(GetSetting("LogMaxItemCount", "400"))
            End Get
            Set(ByVal value As Integer)
                Settings("LogMaxItemCount") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the history max item count.
        ''' </summary>
        ''' <value>the maximum history entry count. 0 means no maximum.</value>
        Public Shared Property HistoryMaxItemCount() As Integer
            Get
                Return CInt(GetSetting("HistoryMaxItemCount", "400"))
            End Get
            Set(ByVal value As Integer)
                Settings("HistoryMaxItemCount") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the opacity.
        ''' </summary>
        ''' <value>The opacity.</value>
        Public Shared Property Opacity() As Integer
            Get
                Return CInt(GetSetting("Opacity", "82"))
            End Get
            Set(ByVal value As Integer)
                Settings("Opacity") = CStr(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the screens, that should be included in the screenshot.
        ''' </summary>
        ''' <value>a list of the IDs of the screens. Leave empty or set to nothing to use all screens.</value>
        Public Shared Property Screens() As List(Of String)
            Get
                Dim _screens As New List(Of String)

                Dim s = GetSetting("Screens", "")

                If Not s = "" Then
                    For Each scr In s.Split("|"c)
                        _screens.Add(scr)
                    Next
                End If

                If _screens.Count > 0 Then
                    Return _screens
                Else
                    Return ScreenHelpers.GetAllScreens
                End If
            End Get
            Set(ByVal value As List(Of String))
                Dim s As String = ""

                Dim isFirst As Boolean = True
                For Each scr In value
                    If isFirst Then
                        isFirst = False
                    Else
                        s &= "|"
                    End If

                    s &= scr
                Next

                Settings("Screens") = s
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the colorization.
        ''' </summary>
        ''' <value>The colorization.</value>
        Public Shared Property Colorization As Colorization.Colorization
            Get
                Try
                    If _colorizationCache Is Nothing Then
                        _colorizationCache = (New ColorizationDatabase)(GetSetting("Colorization", "Elegant"))
                    End If
                Catch ex As Exception
                End Try

                If _colorizationCache Is Nothing Then
                    _colorizationCache = (New ColorizationDatabase)("Elegant")
                End If

                Return _colorizationCache
            End Get
            Set(ByVal value As Colorization.Colorization)
                _colorizationCache = value
                Settings("Colorization") = value.Name
            End Set
        End Property

#Region "Fields"

        Private Shared _colorizationCache As Colorization.Colorization

#End Region

#End Region
    End Class
End Namespace
