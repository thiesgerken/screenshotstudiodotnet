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
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Colorization
    Public Class ColorizationDatabase
        Inherits List(Of Colorization)

#Region "Properties"

        ''' <summary>
        ''' Gets the <see cref="ScreenshotStudioDotNet.Core.Colorization.Colorization" /> with the specified name.
        ''' </summary>
        ''' <value></value>
        Default Public Overloads ReadOnly Property Item(ByVal name As String) As Colorization
            Get
                For Each c In Me
                    If c.Name = name Then Return c
                Next

                Throw New ArgumentException("Colorization not found")
            End Get
        End Property

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ColorizationDatabase" /> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
            Load()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Deletes the colorization.
        ''' </summary>
        ''' <param name="name">The name.</param>
        Public Overloads Sub Remove(ByVal name As String)
            For Each c In Me
                If c.Name = name Then
                    Me.Remove(c)
                    Return
                End If
            Next

            Throw New Exception("Colorization not found")
        End Sub

        ''' <summary>
        ''' Determines whether [contains] [the specified name].
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <returns>
        ''' <c>true</c> if [contains] [the specified name]; otherwise, <c>false</c>.
        ''' </returns>
        Public Overloads Function Contains(ByVal name As String) As Boolean
            For Each c In Me
                If c.Name = name Then Return True
            Next

            Return False
        End Function

        ''' <summary>
        ''' Loads this instance.
        ''' </summary>
        Public Sub Load()
            'Create dump dictionary to get the type of it
            Dim _colorizations As New SerializableList(Of Colorization)
            _colorizations = CType(Serializer.Deserialize("Colorizations.xml", _colorizations.GetType), SerializableList(Of Colorization))

            Dim loadNewDB As Boolean = False
            If _colorizations Is Nothing Then
                loadNewDB = True
            Else
                If _colorizations.Count = 0 Then loadNewDB = True
            End If

            If loadNewDB Then
                _Colorizations = New SerializableList(Of Colorization)

                'Add default Colorizations
                _colorizations.Add(New Colorization("Black", Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 64, 64, 64), Color.FromArgb(255, 93, 93, 93)))
                _colorizations.Add(New Colorization("Cold", Color.FromArgb(255, 0, 0, 255), Color.FromArgb(255, 9, 162, 54), Color.FromArgb(255, 98, 3, 184)))
                _colorizations.Add(New Colorization("Hot", Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 196, 196, 0), Color.FromArgb(255, 255, 95, 17)))
                _colorizations.Add(New Colorization("Blue", Color.FromArgb(255, 0, 0, 255), Color.FromArgb(255, 28, 133, 151), Color.FromArgb(255, 12, 174, 203)))
                _colorizations.Add(New Colorization("Elegant", Color.FromArgb(255, 0, 0, 113), Color.FromArgb(255, 0, 0, 170), Color.FromArgb(255, 10, 10, 210)))
                _colorizations.Add(New Colorization("Windows XP", Color.FromArgb(255, 0, 185, 0), Color.FromArgb(255, 0, 0, 255), Color.FromArgb(255, 225, 113, 0)))
                _colorizations.Add(New Colorization("Traffic Lights", Color.FromArgb(255, 0, 198, 0), Color.FromArgb(255, 232, 232, 0), Color.FromArgb(255, 255, 0, 0)))
            End If

            Me.Clear()
            For Each c In _colorizations
                Me.Add(c)
            Next
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Sub Save()
            Try
                Dim colorizations As New SerializableList(Of Colorization)

                For Each c In Me
                    colorizations.Add(c)
                Next

                Serializer.Serialize("Colorizations.xml", colorizations)
            Catch ex As Exception
                Log.LogError(ex)
            End Try
        End Sub

#End Region
    End Class
End Namespace
