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

Imports System.Windows.Forms

Namespace Hotkey
    Public Class Hotkey

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the hot key.
        ''' </summary>
        ''' <value>The hot key.</value>
        Public Property HotKey() As Keys

        ''' <summary>
        ''' Gets or sets the modifier.
        ''' </summary>
        ''' <value>The modifier.</value>
        Public Property Modifier() As ModifierKeys

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Hotkey" /> class.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <param name="modifier">The modifier.</param>
        ''' <param name="id">The id.</param>
        Public Sub New(ByVal key As Keys, ByVal modifier As ModifierKeys)
            _HotKey = key
            _Modifier = modifier
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Hotkey" /> class.
        ''' </summary>
        ''' <param name="s">The s.</param>
        Public Sub New(ByVal s As String)
            Try
                Dim splits() As String = s.Split(New String() {" + "}, StringSplitOptions.RemoveEmptyEntries)

                Me.Modifier = CType([Enum].Parse(GetType(ModifierKeys), (splits(0))), ModifierKeys)
                Me.HotKey = CType([Enum].Parse(GetType(Keys), splits(1)), Keys)
            Catch ex As Exception
                Throw New ArgumentException("Error converting the string to a hotkey", ex)
            End Try
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Hotkey" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Returns a <see cref="System.String" /> that represents this instance.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="System.String" /> that represents this instance.
        ''' </returns>
        Public Overrides Function ToString() As String
            Return Modifier.ToString & " + " & HotKey.ToString
        End Function

#End Region
    End Class
End Namespace
