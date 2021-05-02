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

Imports System.IO
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Settings
Imports System.Xml.Serialization

Namespace Serialization
    Public Class Serializer

#Region "Functions"

        ''' <summary>
        ''' Serializes the specified name.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="objectToSave">The object to save.</param>
        Public Shared Sub Serialize(ByVal name As String, ByVal objectToSave As Object)
            Dim fileName As String = Path.Combine(StaticProperties.SettingsDirectory, name)

            'Check for file extension and eventually append one
            If Not Path.HasExtension(name) Then fileName &= ".xml"

            'Save the object to the specified file
            Using fs As New FileStream(fileName, FileMode.Create)
                Dim x As New XmlSerializer(objectToSave.GetType)

                x.Serialize(fs, objectToSave)
            End Using
        End Sub

        ''' <summary>
        ''' Deserializes the specified name.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="type">The type.</param>
        ''' <returns></returns>
        Public Shared Function Deserialize(ByVal name As String, ByVal type As Type) As Object
            Return Deserialize(name, type, False)
        End Function

        ''' <summary>
        ''' Deserializes the specified name.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="type">The type.</param>
        ''' <returns></returns>
        Public Shared Function Deserialize(ByVal name As String, ByVal type As Type, ByVal suppressLogging As Boolean) As Object
            Dim fileName As String = Path.Combine(StaticProperties.SettingsDirectory, name)

            'Check for file extension and eventually append one
            If Not Path.HasExtension(name) Then fileName &= ".xml"

            Try

                'Check if the file exist ...
                If File.Exists(fileName) Then
                    'open this file and return the deserialized contents
                    Using s As New FileStream(fileName, FileMode.Open)
                        Dim x As New XmlSerializer(type)

                        Dim result = x.Deserialize(s)
                        Return result
                    End Using
                Else
                    ' ... and if it does not, return nothing 

                    If Not suppressLogging Then
                        Log.LogError("Deserialization for " & fileName & " failed. File doesn't exist.")
                    End If

                    Return Nothing
                End If
            Catch ex As Exception
                If Not suppressLogging Then
                    Log.LogError("Deserialization for " & fileName & " failed.")
                    Log.LogError(ex)
                End If

                Return Nothing
            End Try
        End Function

#End Region
    End Class
End Namespace
