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

Imports System.Resources
Imports System.Reflection

Namespace Imageshack
    Public NotInheritable Class FileDownloadFunctions
        Private Shared _langManager As New ResourceManager("ScreenshotStudioDotNet.Outputs.General.Strings", Assembly.GetExecutingAssembly)

        ''' <summary>
        ''' Gets the readable time.
        ''' </summary>
        ''' <param name="seconds">The seconds.</param>
        ''' <returns></returns>
        Public Shared Function GetReadableTime(ByVal seconds As Double) As String
            seconds = Math.Round(seconds, 0)

            If seconds > 60 Then
                Dim mins As Integer = 1

                While seconds - mins * 60 > 60
                    mins += 1
                End While

                Return mins & " " & CStr(IIf(mins = 1, _langManager.GetString("minute"), _langManager.GetString("minutes"))) & " " & _langManager.GetString("waitAnd") & " " & seconds - mins * 60 & " " & CStr(IIf(seconds - mins * 60 = 1, _langManager.GetString("second"), _langManager.GetString("seconds")))
            Else
                Return seconds & " " & CStr(IIf(seconds = 1, _langManager.GetString("second"), _langManager.GetString("seconds")))
            End If
        End Function

        ''' <summary>
        ''' Gets the size of the readable.
        ''' </summary>
        ''' <param name="size">The size.</param>
        ''' <returns></returns>
        Public Shared Function GetReadableSize(ByVal size As Long) As String
            Dim steps As Integer = 0
            While size / (1024 ^ steps) > 1024
                steps += 1
            End While

            Dim s As String = Math.Round(CDbl(size / (1024 ^ steps)), CInt(IIf(steps > 1, 2, 0))).ToString(IIf(steps > 1, "F", "G").ToString) & " "

            Select Case steps
                Case 0
                    s &= "Bytes"
                Case 1
                    s &= "Kilobytes"
                Case 2
                    s &= "Megabytes"
                Case 3
                    s &= "Gigabytes"
                Case 1
                    s &= "Terabytes"
                Case Else
                    s &= "many"
            End Select

            Return s
        End Function


        ''' <summary>
        ''' Gets the readable speed.
        ''' </summary>
        ''' <param name="speed">The speed.</param>
        ''' <returns></returns>
        Public Shared Function GetReadableSpeed(ByVal speed As Double) As String
            Dim steps As Integer = 0
            While speed / (1024 ^ steps) > 1024
                steps += 1
            End While

            Dim s As String = Math.Round(CDbl(speed / (1024 ^ steps)), CInt(IIf(steps > 1, 2, 0))) & " "

            Select Case steps
                Case 0
                    s &= "b/s"
                Case 1
                    s &= "kb/s"
                Case 2
                    s &= "mb/s"
                Case 3
                    s &= "gb/s"
                Case 1
                    s &= "tb/s"
                Case Else
                    s &= "viele/s"
            End Select

            Return s
        End Function
    End Class
End Namespace
