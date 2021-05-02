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
Imports System.Text.RegularExpressions
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Settings

Namespace Misc
    Public NotInheritable Class FileNameMaskProcessor

#Region "Properties"

#Region "Replacements"

        ''' <summary>
        ''' Gets the date replacement.
        ''' </summary>
        ''' <value>The date replacement.</value>
        Public Shared ReadOnly Property DateReplacement() As String
            Get
                Return "[date]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the time replacement.
        ''' </summary>
        ''' <value>The time replacement.</value>
        Public Shared ReadOnly Property TimeReplacement() As String
            Get
                Return "[time]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the macro name replacement.
        ''' </summary>
        ''' <value>The macro name replacement.</value>
        Public Shared ReadOnly Property MacroNameReplacement() As String
            Get
                Return "[macro]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the screenshot type replacement.
        ''' </summary>
        ''' <value>The screenshot type replacement.</value>
        Public Shared ReadOnly Property ScreenshotTypeReplacement() As String
            Get
                Return "[type]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the total number replacement.
        ''' </summary>
        ''' <value>The total number replacement.</value>
        Public Shared ReadOnly Property TotalNumberReplacement() As String
            Get
                Return "[total]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the number replacement.
        ''' </summary>
        ''' <value>The number replacement.</value>
        Public Shared ReadOnly Property NumberReplacement() As String
            Get
                Return "[number]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the shape replacement.
        ''' </summary>
        ''' <value>The shape replacement.</value>
        Public Shared ReadOnly Property ShapeReplacement() As String
            Get
                Return "[shape]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the window title replacement.
        ''' </summary>
        ''' <value>The window title replacement.</value>
        Public Shared ReadOnly Property WindowTitleReplacement() As String
            Get
                Return "[windowtitle]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the window process replacement.
        ''' </summary>
        ''' <value>The window process replacement.</value>
        Public Shared ReadOnly Property WindowProcessReplacement() As String
            Get
                Return "[process]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the size replacement.
        ''' </summary>
        ''' <value>The bounds replacement.</value>
        Public Shared ReadOnly Property SizeReplacement() As String
            Get
                Return "[size]"
            End Get
        End Property

        ''' <summary>
        ''' Gets the location replacement.
        ''' </summary>
        ''' <value>The bounds replacement.</value>
        Public Shared ReadOnly Property LocationReplacement() As String
            Get
                Return "[location]"
            End Get
        End Property

#End Region

#Region "Conditions"

        ''' <summary>
        ''' Gets the type condition.
        ''' </summary>
        ''' <value>The type condition.</value>
        Public Shared ReadOnly Property TypeCondition() As String
            Get
                Return "type"
            End Get
        End Property

        ''' <summary>
        ''' Gets the number condition.
        ''' </summary>
        ''' <value>The number condition.</value>
        Public Shared ReadOnly Property NumberCondition() As String
            Get
                Return "number"
            End Get
        End Property

        ''' <summary>
        ''' Gets the total condition.
        ''' </summary>
        ''' <value>The total condition.</value>
        Public Shared ReadOnly Property TotalCondition() As String
            Get
                Return "total"
            End Get
        End Property

#End Region

#End Region

#Region "Functions"

        ''' <summary>
        ''' Processes the mask.
        ''' </summary>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <param name="appendFormatSuffix">if set to <c>true</c> [append format suffix].</param>
        ''' <returns></returns>
        Public Overloads Shared Function ProcessMask(ByVal screenshot As Screenshot, ByVal appendFormatSuffix As Boolean) As String
            Dim s As String = SettingsDatabase.FileMask
            s = Replace(s, screenshot)

            Dim oldS = String.Copy(s)

            Dim regexConditionStart As String = "\(\(\w+?:[<>]{0,1}\w+?\)\)"
            Dim regexConditionEnd As String = "\(\(/\)\)"
            For Each condition As Match In Regex.Matches(oldS, regexConditionStart & ".*?" & regexConditionEnd)
                Dim conditionType As String = Regex.Match(condition.Value, "\(\(\w+?:").Value
                Dim conditionValue As String = condition.Value.Substring(conditionType.Length, condition.Value.IndexOf("))") - conditionType.Length)
                conditionType = conditionType.Substring(2, conditionType.Length - 3)

                Dim conditionTrue As Boolean
                Select Case conditionType
                    Case "type"
                        Try
                            Dim t As New PluginDatabase(Of IScreenshotType)
                            Dim plugin = t(screenshot.ScreenshotType)
                            conditionTrue = (conditionValue = screenshot.ScreenshotType) Or (plugin.Name = conditionValue)
                        Catch ex As Exception
                            conditionTrue = False
                        End Try
                    Case "number"
                        If conditionValue.StartsWith(">") Then
                            conditionTrue = (screenshot.Number.ToString > conditionValue.Substring(1))
                        ElseIf conditionValue.StartsWith("<") Then
                            conditionTrue = (screenshot.Number.ToString < conditionValue.Substring(1))
                        Else
                            conditionTrue = (conditionValue = screenshot.Number.ToString)
                        End If
                    Case "total"
                        If conditionValue.StartsWith(">") Then
                            conditionTrue = (screenshot.TotalNumber.ToString > conditionValue.Substring(1))
                        ElseIf conditionValue.StartsWith("<") Then
                            conditionTrue = (screenshot.TotalNumber.ToString < conditionValue.Substring(1))
                        Else
                            conditionTrue = (conditionValue = screenshot.TotalNumber.ToString)
                        End If
                    Case Else
                        conditionTrue = False
                End Select

                Dim replacement As String
                If conditionTrue Then
                    replacement = Regex.Replace(Regex.Replace(condition.Value, regexConditionStart, ""), regexConditionEnd, "")
                Else
                    replacement = ""
                End If

                s = s.Replace(condition.Value, replacement)
            Next

            'remove unsupported chars
            Dim unsupportedChars() As String = New String() {"/", "\", ":", "*", "?", """", "<", ">", "|"}
            For Each c In unsupportedChars
                s = s.Replace(c, "")
            Next

            If appendFormatSuffix Then s &= "." & GetFileSuffix(SettingsDatabase.FileFormat)

            Return s
        End Function

        ''' <summary>
        ''' Replaces the specified input.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <returns></returns>
        Private Shared Function Replace(ByVal input As String, ByVal screenshot As Screenshot) As String
            Dim s = input

            s = s.Replace(NumberReplacement, screenshot.Number.ToString)
            s = s.Replace(TotalNumberReplacement, screenshot.TotalNumber.ToString)
            s = s.Replace(TimeReplacement, screenshot.DateTaken.ToLongTimeString.Replace(":", "_"))
            s = s.Replace(DateReplacement, screenshot.DateTaken.ToShortDateString.Replace(".", "_"))
            s = s.Replace(MacroNameReplacement, screenshot.MacroName)
            s = s.Replace(ScreenshotTypeReplacement, screenshot.ScreenshotType)
            s = s.Replace(SizeReplacement, screenshot.Bounds.Width & "x" & screenshot.Bounds.Height)
            s = s.Replace(LocationReplacement, screenshot.Bounds.X & ";" & screenshot.Bounds.Y)
            s = s.Replace(WindowProcessReplacement, screenshot.ProcessName)
            s = s.Replace(WindowTitleReplacement, screenshot.WindowTitle)
            s = s.Replace(ShapeReplacement, screenshot.Shape.ToString)

            Return s
        End Function

        ''' <summary>
        ''' Processes the mask.
        ''' </summary>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <returns></returns>
        Public Overloads Shared Function ProcessMask(ByVal screenshot As Screenshot) As String
            Return ProcessMask(screenshot, True)
        End Function

        ''' <summary>
        ''' Gets the file suffix.
        ''' </summary>
        ''' <param name="imgFormat">The img format.</param>
        ''' <returns></returns>
        Public Shared Function GetFileSuffix(ByVal imgFormat As ImageFormat) As String
            Select Case imgFormat.ToString
                Case ImageFormat.Bmp.ToString
                    Return "bmp"
                Case ImageFormat.Emf.ToString
                    Return "emf"
                Case ImageFormat.Gif.ToString
                    Return "gif"
                Case ImageFormat.Jpeg.ToString
                    Return "jpg"
                Case ImageFormat.Png.ToString
                    Return "png"
                Case ImageFormat.Tiff.ToString
                    Return "tiff"
                Case ImageFormat.Wmf.ToString
                    Return "wmf"
                Case Else
                    Return "jpg"
            End Select
        End Function

        ''' <summary>
        ''' Strings to file format.
        ''' </summary>
        ''' <param name="s">The s.</param>
        ''' <returns></returns>
        Public Shared Function StringToFileFormat(ByVal s As String) As ImageFormat
            s = s.ToUpperInvariant

            If s.Contains("BMP") Then Return ImageFormat.Bmp
            If s.Contains("PNG") Then Return ImageFormat.Png
            If s.Contains("GIF") Then Return ImageFormat.Gif
            If s.Contains("JPG") Then Return ImageFormat.Jpeg

            Return ImageFormat.Bmp
        End Function

#End Region
    End Class
End Namespace
