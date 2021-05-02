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

Imports System.Reflection
Imports System.IO

Namespace Extensibility
    Public Class PluginDatabase(Of T)
        Inherits List(Of Plugin(Of T))

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PluginDatabase(Of T)" /> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
            Refresh()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Gets the neutral plugin list.
        ''' </summary>
        ''' <returns></returns>
        Public Function ToNeutralPluginList() As List(Of Plugin(Of IPlugin))
            Dim l As New List(Of Plugin(Of IPlugin))

            For Each p In Me
                l.Add(p.ToNeutralPlugin)
            Next

            Return l
        End Function

        ''' <summary>
        ''' Refreshes this instance.
        ''' </summary>
        Public Overridable Sub Refresh()
            Dim pluginList As New List(Of Plugin(Of T))
            pluginList.AddRange(FindPlugins(My.Application.Info.DirectoryPath, GetType(T).Name))

            Me.Clear()
            For Each p In pluginList
                Me.Add(p)
            Next
        End Sub

        ''' <summary>
        ''' Finds the plugins.
        ''' </summary>
        ''' <param name="path">The path.</param>
        ''' <param name="usedInterface">The used interface.</param>
        ''' <returns></returns>
        Private Function FindPlugins(ByVal path As String, ByVal usedInterface As String) As List(Of Plugin(Of T))
            Dim plugins As New List(Of Plugin(Of T))
            Dim dlls() As String

            'Go through all DLLs in the directory, attempting to load them
            dlls = Directory.GetFileSystemEntries(path, "*.dll")
            For Each s As String In dlls
                Try
                    Dim dll As [Assembly]
                    dll = [Assembly].LoadFrom(s)
                    plugins.AddRange(ExamineAssembly(dll))
                Catch e As Exception
                    'Error loading DLL, we don't need to do anything special
                End Try
            Next

            Return plugins
        End Function

        ''' <summary>
        ''' Examines the assembly.
        ''' </summary>
        ''' <param name="dll">The DLL.</param>
        ''' <returns></returns>
        Private Function ExamineAssembly(ByVal dll As Assembly) As List(Of Plugin(Of T))
            Dim pluginsFound As New List(Of Plugin(Of T))

            'Loop through each type in the DLL
            For Each t As Type In dll.GetTypes
                'Only look at public types
                If t.IsPublic = True Then
                    'Ignore abstract classes
                    If Not ((t.Attributes And TypeAttributes.Abstract) = TypeAttributes.Abstract) Then

                        'See if this type implements our interface
                        Dim objInterface As Type = t.GetInterface(GetType(T).Name, True)

                        If Not (objInterface Is Nothing) Then
                            Dim p As IPlugin = CType((New Plugin(Of T)("", "", dll.Location, t.FullName, "", False).CreateInstance), IPlugin)

                            Dim plug As New Plugin(Of T)(p.Name, p.DisplayName, dll.Location, t.FullName, p.Description, p.IsSupportedMultipleTimes)
                            pluginsFound.Add(plug)
                        End If
                    End If
                End If
            Next

            Return pluginsFound
        End Function
#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the name of the plugin by.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <returns></returns>
        Default Public Overloads ReadOnly Property Item(ByVal name As String) As Plugin(Of T)
            Get
                For Each p In Me
                    If p.Name = name Or p.DisplayName = name Then Return p
                Next

                Throw New ArgumentException("The Plugin could not be found")
            End Get
        End Property

#End Region
    End Class
End Namespace
