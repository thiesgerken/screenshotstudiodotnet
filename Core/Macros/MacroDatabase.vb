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

Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Macros
    Public Class MacroDatabase
        Inherits List(Of Macro)

#Region "Properties"

        ''' <summary>
        ''' Gets the <see cref="ScreenshotStudioDotnet.Core.Macros.Macro" /> with the specified name.
        ''' </summary>
        ''' <value></value>
        Default Public Overloads ReadOnly Property Item(ByVal name As String) As Macro
            Get
                For Each m In Me
                    If m.Name = name Then Return m
                Next

                Throw New ArgumentException("Macro not found in the Database")
            End Get
        End Property

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MacroDatabase" /> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
            Load()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Deletes the macro.
        ''' </summary>
        ''' <param name="name">The name.</param>
        Public Shadows Sub Remove(ByVal name As String)
            Dim i As Integer = 0
            For Each m In Me
                If m.Name = name Then
                    Me.Remove(m)
                    Exit For
                End If
                i += 1
            Next
        End Sub

        ''' <summary>
        ''' Determines whether the specified macro name contains macro.
        ''' </summary>
        ''' <param name="macroName">Name of the macro.</param>
        ''' <returns>
        ''' <c>true</c> if the specified macro name contains macro; otherwise, <c>false</c>.
        ''' </returns>
        Public Overloads Function Contains(ByVal macroName As String) As Boolean
            For Each m In Me
                If m.Name = macroName Then
                    Return True
                End If
            Next

            Return False
        End Function

        ''' <summary>
        ''' Gets the macros with the specified trigger.
        ''' </summary>
        ''' <param name="triggerName">Name of the trigger.</param>
        ''' <returns></returns>
        Public Function GetMacrosWithTrigger(ByVal triggerName As String) As List(Of Macro)
            Dim macroList As New List(Of Macro)
            For Each m In Me
                For Each t In m.Triggers
                    If t.Name = triggerName Then
                        macroList.Add(m)
                    End If
                Next
            Next

            Return macroList
        End Function

        ''' <summary>
        ''' Adds the specified item.
        ''' Note: The Database is saved automatically after the macro has been added.
        ''' </summary>
        ''' <param name="item">The item.</param>
        Public Shadows Sub Add(ByVal item As Macro)
            MyBase.Add(item)
            Save()
        End Sub

        ''' <summary>
        ''' Removes the specified item.
        ''' Note: The Database is saved automatically after the macro has been removed.
        ''' </summary>
        ''' <param name="item">The item.</param>
        Public Shadows Sub Remove(ByVal item As Macro)
            MyBase.Remove(item)
            Save()
        End Sub

        ''' <summary>
        ''' Loads this instance.
        ''' </summary>
        Private Sub Load()
            'Create dump dictionary to get the type of it
            Dim _macros As New SerializableList(Of Macro)
            
            _macros = CType(Serializer.Deserialize("Macros.xml", _macros.GetType), SerializableList(Of Macro))

            If _macros Is Nothing Then
                _macros = New SerializableList(Of Macro)

                Dim types As New PluginDatabase(Of IScreenshotType)()
                Dim outputs As New PluginDatabase(Of IOutput)()
                Dim triggers As New PluginDatabase(Of ITriggerManager)()
                
                Dim quickstartTrigger As Plugin(Of ITriggerManager) = triggers("Quickstart")
          
                Dim fullscreenMacro As New Macro()
                Dim fullscreenType = types("FullScreen")
                fullscreenMacro.Name = fullscreenType.DisplayName
                fullscreenMacro.Type = fullscreenType
                fullscreenMacro.Triggers.Add(quickstartTrigger)
                fullscreenMacro.Multiple = New MultipleParameters(1, 1)
                _macros.Add(fullscreenMacro)

                Dim windowMacro As New Macro()
                Dim windowType = types("Window")
                windowMacro.Name = windowType.DisplayName
                windowMacro.Type = windowType
                windowMacro.Triggers.Add(quickstartTrigger)
                windowMacro.Multiple = New MultipleParameters(1, 1)
                _macros.Add(windowMacro)

                Dim freehandMacro As New Macro()
                Dim freehandType = types("FreeHand")
                freehandMacro.Name = freehandType.DisplayName
                freehandMacro.Type = freehandType
                freehandMacro.Triggers.Add(quickstartTrigger)
                freehandMacro.Multiple = New MultipleParameters(1, 1)
                _macros.Add(freehandMacro)

                Dim regionMacro As New Macro()
                Dim regionType = types("Region")
                regionMacro.Name = regionType.DisplayName
                regionMacro.Type = regionType
                regionMacro.Triggers.Add(quickstartTrigger)
                regionMacro.Multiple = New MultipleParameters(1, 1)
                _macros.Add(regionMacro)

                Dim websiteMacro As New Macro()
                Dim websiteType = types("Website")
                websiteMacro.Name = websiteType.DisplayName
                websiteMacro.Type = websiteType
                websiteMacro.Multiple = New MultipleParameters(1, 1)
                _macros.Add(websiteMacro)
            End If

            Me.Clear()

            For Each m In _macros
                MyBase.Add(m)
            Next
        End Sub

        ''' <summary>
        ''' Saves this instance.
        ''' </summary>
        Public Sub Save()
            Try
                Dim _macros As New SerializableList(Of Macro)
                For Each m In Me
                    _macros.Add(m)
                Next

                Serializer.Serialize("Macros.xml", _macros)
            Catch ex As Exception
                Log.LogError(ex)
            End Try
        End Sub

#End Region
    End Class
End Namespace
