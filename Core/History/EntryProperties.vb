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

Imports System.Drawing
Imports System.Resources
Imports System.Reflection
Imports System.IO
Imports System.ComponentModel

Namespace History
    Public Class EntryProperties
        Inherits GlobalizedObject

#Region "Fields"

        Private _screenshot As Bitmap
        Private _entry As HistoryEntry
        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EntryProperties" /> class.
        ''' </summary>
        ''' <param name="screenshot">The screenshot.</param>
        ''' <param name="entry">The entry.</param>
        Public Sub New(ByVal screenshot As Bitmap, ByVal entry As HistoryEntry)
            _screenshot = screenshot
            _entry = entry
        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the size.
        ''' </summary>
        ''' <value>The size.</value>
        <GlobalizedCategory("Screenshot", 2)> <PropertyOrder(4)> _
        Public ReadOnly Property Bounds() As Rectangle
            Get
                Return _entry.Bounds
            End Get
        End Property

        ''' <summary>
        ''' Gets the time taken.
        ''' </summary>
        ''' <value>The time taken.</value>
        <GlobalizedCategory("Screenshot", 2)> <PropertyOrder(5)> _
        Public ReadOnly Property TimeTaken() As Date
            Get
                Return _entry.DateTaken
            End Get
        End Property

        ''' <summary>
        ''' Gets the size of the file.
        ''' </summary>
        ''' <value>The size of the file.</value>
        <GlobalizedCategory("Screenshot", 2)> <PropertyOrder(3)> _
        Public ReadOnly Property FileSize() As String
            Get
                Dim fileName = ScreenshotHistory.EncodeFileName(_entry.DateTaken)
                Dim s As String

                Try
                    Dim fi As New FileInfo(fileName)
                    s = Math.Round(fi.Length / 1024, 0).ToString & " kB"
                Catch ex As Exception
                    s = _langManager.GetString("FileSizeError")
                End Try

                Return s
            End Get
        End Property

        ''' <summary>
        ''' Gets the output used.
        ''' </summary>
        ''' <value>The output used.</value>
        <GlobalizedCategory("OutputUsed", 1)> <PropertyOrder(3)>
        <TypeConverter(GetType(ViewableDictionaryConverter))>
        Public ReadOnly Property OutputsUsed() As ViewableDictionary(Of String, String)
            Get
                Dim l As New ViewableDictionary(Of String, String)

                If _entry.OutputsUsed.Count = 0 Then
                    l.Add(_langManager.GetString("None"), "")
                Else
                    For i As Integer = 0 To _entry.OutputsUsed.Count - 1
                        Dim info As String = _entry.AdditionalInformation(i)

                        If info = "" Then info = "[No Information given]"

                        l.Add(_entry.OutputsUsed(i).DisplayName, info)
                    Next
                End If
                Return l
            End Get
        End Property

        ''' <summary>
        ''' Gets the triggers.
        ''' </summary>
        ''' <value>The triggers.</value>
        <GlobalizedCategory("Triggers", 1)> <PropertyOrder(3)>
      <TypeConverter(GetType(ViewableDictionaryConverter))>
        Public ReadOnly Property Triggers() As ViewableDictionary(Of String, String)
            Get
                Dim l As New ViewableDictionary(Of String, String)

                If _entry.MacroUsed.Triggers.Count = 0 Then
                    l.Add(_langManager.GetString("None"), "")
                Else
                    For i As Integer = 0 To _entry.MacroUsed.Triggers.Count - 1
                        Dim value As String = "" 'Todo: Sum up additional arguments
                        If value = "" Then value = "[No Arguments given]"

                        l.Add(_entry.MacroUsed.Triggers(i).DisplayName, value)
                    Next
                End If
                Return l
            End Get
        End Property

        ''' <summary>
        ''' Gets the effects.
        ''' </summary>
        ''' <value>The effects.</value>
        <GlobalizedCategory("Effects", 1)> <PropertyOrder(3)>
        <TypeConverter(GetType(ViewableDictionaryConverter))>
        Public ReadOnly Property Effects() As ViewableDictionary(Of String, String)
            Get
                Dim l As New ViewableDictionary(Of String, String)

                If _entry.MacroUsed.Effects.Count = 0 Then
                    l.Add(_langManager.GetString("None"), "")
                Else
                    For i As Integer = 0 To _entry.MacroUsed.Effects.Count - 1
                        Dim value As String = "" 'Todo: Sum up additional arguments
                        If value = "" Then value = "[No Arguments given]"

                        l.Add(_entry.MacroUsed.Effects(i).DisplayName, value)
                    Next
                End If
                Return l
            End Get
        End Property

        ''' <summary>
        ''' Gets the delay.
        ''' </summary>
        ''' <value>The delay.</value>
        <GlobalizedCategory("Macro", 0)> <PropertyOrder(9)> _
        Public ReadOnly Property Delay() As String
            Get
                Return _entry.MacroUsed.Delay & " ms"
            End Get
        End Property

        ''' <summary>
        ''' Gets the type.
        ''' </summary>
        ''' <value>The type.</value>
        <GlobalizedCategory("Macro", 0)> <PropertyOrder(14)>
        <TypeConverter(GetType(ViewableDictionaryConverter))>
        Public ReadOnly Property Type() As ViewableDictionary(Of String, String)
            Get
                Dim l As New ViewableDictionary(Of String, String)
                l.DisplayTitle = _entry.MacroUsed.Type.DisplayName

                If _entry.MacroUsed.Type.Arguments.Count > 0 Then
                    For Each kvp In _entry.MacroUsed.Type.Arguments
                        l.Add(kvp.Key, kvp.Value.ToString)
                    Next
                End If

                Return l
            End Get
        End Property

        ''' <summary>
        ''' Gets the multiple interval.
        ''' </summary>
        ''' <value>The multiple interval.</value>
        <GlobalizedCategory("Macro", 0)> <PropertyOrder(10)> _
        Public ReadOnly Property MultipleInterval() As String
            Get
                Return _entry.MacroUsed.Multiple.Interval & " ms"
            End Get
        End Property

        ''' <summary>
        ''' Gets the total number.
        ''' </summary>
        ''' <value>The total number.</value>
        <GlobalizedCategory("Macro", 0)> <PropertyOrder(11)> _
        Public ReadOnly Property TotalNumber() As Integer
            Get
                Return _entry.MacroUsed.Multiple.Count
            End Get
        End Property

        ''' <summary>
        ''' Gets the number.
        ''' </summary>
        ''' <value>The number.</value>
        <GlobalizedCategory("Macro", 0)> <PropertyOrder(12)> _
        Public ReadOnly Property Number() As Integer
            Get
                Return _entry.Number
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the macro.
        ''' </summary>
        ''' <value>The name of the macro.</value>
        <GlobalizedCategory("Macro", 0)> <PropertyOrder(15)> _
        Public ReadOnly Property MacroName() As String
            Get
                Return _entry.MacroUsed.Name
            End Get
        End Property

#End Region
    End Class
End Namespace
