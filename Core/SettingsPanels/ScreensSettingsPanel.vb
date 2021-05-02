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
Imports System.Windows.Forms

Namespace Settings
    Public Class ScreensSettingsPanel

#Region "Fields"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ScreensSettingsPanel" /> class.
        ''' </summary>
        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            listScreens.Items.Clear()

            For Each scr In Screen.AllScreens
                Dim boundsText As String = "X: " & scr.Bounds.X & " Y:" & scr.Bounds.Y & " " & _langManager.GetString("width") & scr.Bounds.Width & " " & _langManager.GetString("height") & scr.Bounds.Height

                Dim l As New ListViewItem(New String() {scr.DeviceName, boundsText})
                l.Name = scr.DeviceName
                l.Checked = SettingsDatabase.Screens.Contains(scr.DeviceName)
                listScreens.Items.Add(l)
            Next

            listScreens.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Gets the checked screens.
        ''' </summary>
        ''' <returns></returns>
        Private Function GetCheckedScreens() As List(Of String)
            Dim l As New List(Of String)

            For Each lvi As ListViewItem In listScreens.CheckedItems
                l.Add(lvi.Name)
            Next

            Return l
        End Function

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Overrides Sub Save()
            SettingsDatabase.Screens = GetCheckedScreens()
            SettingsDatabase.Save()
        End Sub

#End Region

#Region "Overridden Properties"

        ''' <summary>
        ''' Gets a value indicating whether [properties changed].
        ''' </summary>
        ''' <value><c>true</c> if [properties changed]; otherwise, <c>false</c>.</value>
        Public Overrides ReadOnly Property PropertiesChanged As Boolean
            Get
                Dim newList As List(Of String) = GetCheckedScreens()

                For Each s In newList
                    If Not SettingsDatabase.Screens.Contains(s) Then Return True
                Next

                For Each s In SettingsDatabase.Screens
                    If Not newList.Contains(s) Then Return True
                Next

                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public Overrides ReadOnly Property DisplayName As String
            Get
                Return _langManager.GetString("Screens")
            End Get
        End Property

#End Region
    End Class
End Namespace
