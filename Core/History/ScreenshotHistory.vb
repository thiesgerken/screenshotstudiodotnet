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
Imports System.Drawing.Imaging
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Serialization
Imports ScreenshotStudioDotNet.Core.Settings

Namespace History
    Public Class ScreenshotHistory

#Region "Fields"

        Private Shared _entries As SerializableDictionary(Of Date, HistoryEntry)

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the log entries.
        ''' </summary>
        ''' <value>The log entries.</value>
        Public Shared ReadOnly Property HistoryEntries() As Dictionary(Of Date, HistoryEntry)
            Get
                'Singleton
                If _entries Is Nothing Then Load()

                Return _entries
            End Get
        End Property

#End Region

#Region "Functions"

        ''' <summary>
        ''' Loads this instance.
        ''' </summary>
        Private Shared Sub Load()
            'dummy List to get the type
            Dim a As New SerializableDictionary(Of Date, HistoryEntry)

            'Load from serialized XML file
            _entries = CType(Serializer.Deserialize("History", a.GetType), SerializableDictionary(Of Date, HistoryEntry))

            If _entries Is Nothing Then _entries = New SerializableDictionary(Of Date, HistoryEntry)


            'Check for Age of each entry 
            If SettingsDatabase.HistoryAutoCleanAge <> 0 Then
                For Each entry In _entries
                    If (Now - entry.Key).TotalDays >= SettingsDatabase.HistoryAutoCleanAge Then
                        _entries.Remove(entry.Key)

                        'Write Deletion to Log
                        Log.LogInformation("History Item '" & entry.Key.Ticks & "' exceeded the maximum age -> deleted it")
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
                    'Check for item count in history

                    If _entries.Count > SettingsDatabase.HistoryMaxItemCount Then
                        Dim itemsToDelete As Integer = _entries.Count - SettingsDatabase.HistoryMaxItemCount
                        Dim _entriesNew As New SerializableDictionary(Of Date, HistoryEntry)

                        Dim i As Integer = 0
                        For Each e In _entries
                            If i >= itemsToDelete Then
                                _entriesNew.Add(e.Key, e.Value)
                            End If

                            i += 1
                        Next

                        _entries = _entriesNew

                        Log.LogInformation("Deleted " & itemsToDelete & " History entries because of max count")
                    End If
                End If

                Serializer.Serialize("History", _entries)
            Catch ex As Exception
                Log.LogError(ex)
                Log.LogError("Error Saving History")
            End Try
        End Sub

        ''' <summary>
        ''' Adds the entry.
        ''' </summary>
        ''' <param name="screenshotToAdd">The screenshot to add.</param>
        Public Shared Sub AddEntry(ByVal screenshotToAdd As Screenshot, ByVal macroUsed As Macro, ByVal outputUsed As List(Of Plugin(Of IOutput)), ByVal additionalInformation As List(Of String))
            'Singleton
            If _entries Is Nothing Then Load()

            Dim timeTaken As Date = screenshotToAdd.DateTaken
            Dim filename = EncodeFileName(timeTaken)

            Try
                'Check whether the history dir exists or not (and create if necessary)
                If Not Directory.Exists(Path.GetDirectoryName(filename)) Then
                    Directory.CreateDirectory(Path.GetDirectoryName(filename))
                End If

                If File.Exists(filename) Then
                    File.Delete(filename)
                End If

                screenshotToAdd.Screenshot.Save(filename, ImageFormat.Jpeg)

                'Add to History
                _entries.Add(timeTaken, New HistoryEntry(macroUsed, outputUsed, additionalInformation, screenshotToAdd.Bounds, timeTaken, screenshotToAdd.Number, screenshotToAdd.TotalNumber))

                Save()

                Log.LogInformation("Added a History entry: " & filename)
            Catch ex As Exception
                Log.LogError(ex)
                Log.LogError("Error adding Screenshot to History")
            End Try
        End Sub

        ''' <summary>
        ''' Encodes the name of the file.
        ''' </summary>
        ''' <param name="time">The time.</param>
        ''' <returns></returns>
        Public Shared Function EncodeFileName(ByVal time As Date) As String
            Return Path.Combine(StaticProperties.SettingsDirectory, "History\" & time.Ticks)
        End Function

        ''' <summary>
        ''' Gets the screenshot.
        ''' </summary>
        ''' <param name="timeTaken">The time taken.</param>
        ''' <returns></returns>
        Public Shared Function GetScreenshot(ByVal timeTaken As Date) As Screenshot
            If HistoryEntries.ContainsKey(timeTaken) Then
                Dim item = HistoryEntries(timeTaken)
                Dim screenshot = New Bitmap(EncodeFileName(timeTaken))

                Dim result As Screenshot = New Screenshot(screenshot, item.Bounds, timeTaken, item.MacroUsed.Multiple.Count, item.MacroUsed.Type.DisplayName, item.MacroUsed.Name, item.MacroUsed)
                result.Number = item.Number

                Return result
            Else
                Throw New Exception("The requested screenshot could not be found")
            End If
        End Function

        ''' <summary>
        ''' Gets the screenshot.
        ''' </summary>
        ''' <param name="entry">The entry.</param>
        ''' <returns></returns>
        Public Shared Function GetScreenshot(ByVal entry As HistoryEntry) As Screenshot
            Return GetScreenshot(entry.DateTaken)
        End Function

        ''' <summary>
        ''' Clears the history.
        ''' </summary>
        Public Shared Sub ClearHistory()
            Log.LogInformation("Clearing History")

            Dim copiedList As New Dictionary(Of Date, HistoryEntry)

            For Each itm In HistoryEntries
                copiedList.Add(itm.Key, itm.Value)
            Next

            For Each itm In copiedList
                DeleteItem(itm.Key)
            Next

            Save()
        End Sub

        ''' <summary>
        ''' Deletes the item.
        ''' </summary>
        ''' <param name="timeTaken">The time taken.</param>
        Public Shared Sub DeleteItem(ByVal timeTaken As Date)
            If HistoryEntries.ContainsKey(timeTaken) Then
                Log.LogInformation("Deleting History Entry: " & timeTaken.ToString)

                HistoryEntries.Remove(timeTaken)

                Dim fileName = EncodeFileName(timeTaken)
                If File.Exists(fileName) Then
                    Try
                        File.Delete(fileName)
                    Catch ex As Exception
                        Log.LogError(ex)
                        Log.LogError("Couldn't Delete History Entry Screenshot: " & fileName)
                    End Try
                End If

                Save()
            Else
                Throw New Exception("The History Item could not be found")
            End If
        End Sub

        ''' <summary>
        ''' Deletes the item.
        ''' </summary>
        ''' <param name="entry">The entry.</param>
        Public Shared Sub DeleteItem(ByVal entry As HistoryEntry)
            DeleteItem(entry.DateTaken)
        End Sub

#End Region
    End Class
End Namespace
