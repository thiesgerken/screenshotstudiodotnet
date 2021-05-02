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

Namespace Settings
    Public NotInheritable Class StaticProperties

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="StaticProperties" /> class.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region "Fields"

        Private Shared _settingsDirectory As String

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the settings directory.
        ''' </summary>
        ''' <value>The settings directory.</value>
        Public Shared ReadOnly Property SettingsDirectory() As String
            Get
                If _settingsDirectory = "" Then
                    Dim SettingsPath As String = Path.GetDirectoryName(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)

                    Try
                        'Clean Settings Directory (not necessary, but it looks better without an empty directory ^.^)
                        Dim emptyDirectoryPath As String = Path.Combine(SettingsPath, My.Application.Info.Version.ToString(4))
                        If Directory.Exists(emptyDirectoryPath) Then Directory.Delete(emptyDirectoryPath)
                    Catch ex As Exception
                        Log.LogWarning("empty dir couldn't be deleted")
                    End Try

                    _settingsDirectory = SettingsPath
                End If

                Return _settingsDirectory
            End Get
        End Property

        ''' <summary>
        ''' Gets the temp directory.
        ''' </summary>
        ''' <value>The temp directory.</value>
        Public Shared ReadOnly Property TempDirectory() As String
            Get
                Dim _tempFileDir = Path.Combine(SettingsDirectory, "Temp\")

                'Check if the temp directory exists
                If Not Directory.Exists(_tempFileDir) Then Directory.CreateDirectory(_tempFileDir)

                'Delete items in the temp folder that are older than 3 days
                Dim liveTime As Integer = 3

                For Each f As String In Directory.GetFiles(_tempFileDir)
                    Dim fi As New FileInfo(f)
                    If (Now - fi.CreationTime).TotalDays > 3 Then
                        File.Delete(f)
                    End If
                Next

                Return _tempFileDir
            End Get
        End Property

        ''' <summary>
        ''' Gets the update directory.
        ''' </summary>
        ''' <value>The update directory.</value>
        Public Shared ReadOnly Property UpdateDirectory() As String
            Get
                Dim _updatesDir = Path.Combine(SettingsDirectory, "Updates\")

                'Check if the update directory exists, and if it doesn't create it
                If Not Directory.Exists(_updatesDir) Then Directory.CreateDirectory(_updatesDir)

                Return _updatesDir
            End Get
        End Property

#End Region
    End Class
End Namespace
