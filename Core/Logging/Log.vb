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

Imports ScreenshotStudioDotNet.Core.Serialization
Imports ScreenshotStudioDotNet.Core.Settings

Namespace Logging
    Public Class Log
       
#Region "Fields"

        Private Shared _logEntries As SerializableList(Of LogEntry)

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the log entries.
        ''' </summary>
        ''' <value>The log entries.</value>
        Public Shared ReadOnly Property LogEntries() As List(Of LogEntry)
            Get
                Return _logEntries
            End Get
        End Property

#End Region

#Region "Functions"

        ''' <summary>
        ''' Loads this instance.
        ''' </summary>
        Public Shared Sub Load()
            'dummy List to get the type
            Dim a As New SerializableList(Of LogEntry)

            'Load from serialized XML file
            _logEntries = CType(Serializer.Deserialize("Log", a.GetType, True), SerializableList(Of LogEntry))

            If _logEntries Is Nothing Then _logEntries = New SerializableList(Of LogEntry)

            'Check for Age of each entry 
            If SettingsDatabase.LogAutoCleanAge <> 0 Then
                Dim _logEntriesCopy As New SerializableList(Of LogEntry)


                'Grab a copy of the log entries because we are not able to modify a list we are iterating trough
                For Each entry In _logEntries
                    _logEntriesCopy.Add(entry)
                Next

                'go through the copy
                For Each entry In _logEntriesCopy
                    If (Now - entry.TimeCreated).TotalDays >= SettingsDatabase.LogAutoCleanAge Then
                        _logEntries.Remove(entry)
                    End If
                Next

                Save()
            End If
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Private Shared Sub Save()
            Try
                If SettingsDatabase.HistoryMaxItemCount <> 0 Then
                    'Check for item count in log

                    If _logEntries.Count > SettingsDatabase.LogMaxItemCount Then
                        Dim itemsToDelete As Integer = _logEntries.Count - SettingsDatabase.LogMaxItemCount
                        Dim _entriesNew As New SerializableList(Of LogEntry)

                        Dim i As Integer = 0
                        For Each e In _logEntries
                            If i >= itemsToDelete Then
                                _entriesNew.Add(e)
                            End If

                            i += 1
                        Next

                        _logEntries = _entriesNew
                    End If
                End If

                Serializer.Serialize("Log", _logEntries)
            Catch ex As Exception
            End Try
        End Sub

        ''' <summary>
        ''' Logs the information.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        Public Shared Sub LogInformation(ByVal logMessage As String)
            Log(logMessage, "Information", True)
        End Sub

        ''' <summary>
        ''' Logs the information.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        Public Shared Sub LogInformation(ByVal logMessage As String, ByVal PrintToDebug As Boolean)
            Log(logMessage, "Information", PrintToDebug)
        End Sub

        ''' <summary>
        ''' Logs the warning.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        Public Shared Sub LogWarning(ByVal logMessage As String, ByVal PrintToDebug As Boolean)
            Log(logMessage, "Warning", PrintToDebug)
        End Sub

        ''' <summary>
        ''' Logs the warning.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        Public Shared Sub LogWarning(ByVal logMessage As String)
            Log(logMessage, "Warning", True)
        End Sub

        ''' <summary>
        ''' Logs the error.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        Public Shared Sub LogError(ByVal logMessage As String, ByVal PrintToDebug As Boolean)
            Log(logMessage, "Error", PrintToDebug)
        End Sub

        ''' <summary>
        ''' Logs the error.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        Public Shared Sub LogError(ByVal logMessage As String)
            Log(logMessage, "Error", True)
        End Sub

        ''' <summary>
        ''' Logs the error.
        ''' </summary>
        ''' <param name="e">The e.</param>
        Public Shared Sub LogError(ByVal e As Exception, ByVal PrintToDebug As Boolean)
            LogError(e.Message & vbCrLf & e.GetType().ToString & vbCrLf & e.StackTrace, PrintToDebug)
        End Sub

        ''' <summary>
        ''' Logs the error.
        ''' </summary>
        ''' <param name="e">The e.</param>
        Public Shared Sub LogError(ByVal e As Exception)
            LogError(e.Message & vbCrLf & e.GetType().ToString & vbCrLf & e.StackTrace, True)
        End Sub

        ''' <summary>
        ''' Logs the specified log message.
        ''' </summary>
        ''' <param name="logMessage">The log message.</param>
        ''' <param name="type">The type.</param>
        Private Shared Sub Log(ByVal logMessage As String, ByVal type As String, ByVal PrintToDebug As Boolean)
            'Singleton
            If _logEntries Is Nothing Then Load()

            Dim entry As LogEntry = New LogEntry(type, Now, Process.GetCurrentProcess.ProcessName, Process.GetCurrentProcess.Id, My.Computer.Name, logMessage)

            If PrintToDebug Then
                'Sloooow
                Debug.Print(entry.Type & ": " & entry.LogMessage)
            End If

            _logEntries.Add(entry)

            Save()
        End Sub

#End Region
    End Class
End Namespace
