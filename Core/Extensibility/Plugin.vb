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
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Extensibility
    Public Class Plugin(Of T)

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Plugin" /> struct.
        ''' </summary>
        ''' <param name="Name">The name.</param>
        ''' <param name="Dll">The DLL.</param>
        Public Sub New(ByVal name As String, ByVal displayName As String, ByVal dll As String, ByVal className As String, ByVal description As String, ByVal multiTimesAllowed As Boolean)
            Me.DisplayName = displayName
            Me.Dll = dll
            Me.ClassName = className
            Me.Description = description
            Me.Name = name
            Me.MultiTimesAllowed = multiTimesAllowed
        End Sub
        ''' <summary>
        ''' Initializes a new instance of the <see cref="Plugin" /> struct.
        ''' </summary>
        ''' <param name="Name">The name.</param>
        ''' <param name="Dll">The DLL.</param>
        Public Sub New(ByVal name As String, ByVal displayName As String, ByVal dll As String, ByVal className As String, ByVal description As String, ByVal arguments As SerializableDictionary(Of String, Object), ByVal multiTimesAllowed As Boolean)
            Me.DisplayName = displayName
            Me.Dll = dll
            Me.ClassName = className
            Me.Description = description
            Me.Name = name
            Me.Arguments = arguments
            Me.MultiTimesAllowed = multiTimesAllowed
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Plugin" /> class.
        ''' </summary>
        Public Sub New()
            Me.DisplayName = ""
            Me.Dll = ""
            Me.ClassName = ""
            Me.Description = ""
            Me.Name = ""
        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public Property DisplayName() As String

        ''' <summary>
        ''' Gets or sets the DLL.
        ''' </summary>
        ''' <value>The DLL.</value>
        Public Property Dll() As String

        ''' <summary>
        ''' Gets or sets the name of the class.
        ''' </summary>
        ''' <value>The name of the class.</value>
        Public Property ClassName() As String

        ''' <summary>
        ''' Gets or sets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public Property Description() As String

        ''' <summary>
        ''' Gets or sets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public Property Name() As String


        ''' <summary>
        ''' Gets or sets the arguments that are passed to the plugin (Name, Value)
        ''' </summary>
        ''' <value>The arguments.</value>
        Public Property Arguments As New SerializableDictionary(Of String, Object)

        ''' <summary>
        ''' Gets or sets a value indicating whether [multi times allowed].
        ''' </summary>
        ''' <value><c>true</c> if [multi times allowed]; otherwise, <c>false</c>.</value>
        Public Property MultiTimesAllowed As Boolean = True
#End Region

#Region "Functions"

        ''' <summary>
        ''' Creates the instance.
        ''' </summary>
        ''' <param name="plugin">The plugin.</param>
        ''' <returns></returns>
        Public Shared Function CreateInstance(ByVal plugin As Plugin(Of T)) As T
            Dim objDLL As Assembly
            Try
                'Load dll
                objDLL = Assembly.LoadFrom(plugin.Dll)

                'Create and return class instance
                Return CType(objDLL.CreateInstance(plugin.ClassName), T)
            Catch e As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Creates the instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function CreateInstance() As T
            Dim objDLL As Assembly
            Try
                'Load dll
                objDLL = Assembly.LoadFrom(Me.Dll)

                'Create and return class instance
                Return CType(objDLL.CreateInstance(Me.ClassName), T)
            Catch e As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Gets the parameter.
        ''' </summary>
        ''' <param name="paramName">Name of the param.</param>
        ''' <returns></returns>
        Public Function GetParameter(ByVal paramName As String) As Object
            If Me.Arguments.ContainsKey(paramName) Then
                Return Me.Arguments(paramName)
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Converts this instance to a Plugin(Of IPlugin)
        ''' </summary>
        ''' <returns></returns>
        Public Function ToNeutralPlugin() As Plugin(Of IPlugin)
            Return New Plugin(Of IPlugin)(Me.Name, Me.DisplayName, Me.Dll, Me.ClassName, Me.Description, Me.Arguments, Me.MultiTimesAllowed)
        End Function

        ''' <summary>
        ''' Toes the neutral plugin list.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <returns></returns>
        Public Function ToNeutralPluginList(ByVal list As List(Of Plugin(Of T))) As List(Of Plugin(Of IPlugin))
            Dim newList As New List(Of Plugin(Of IPlugin))

            For Each t In list
                newList.Add(t.ToNeutralPlugin)
            Next

            Return newList
        End Function

        ''' <summary>
        ''' Converts this instance to a Plugin(Of IArgumentPlugin)
        ''' </summary>
        ''' <returns></returns>
        Public Function ToArgumentPlugin() As Plugin(Of IArgumentPlugin)
            Return New Plugin(Of IArgumentPlugin)(Me.Name, Me.DisplayName, Me.Dll, Me.ClassName, Me.Description, Me.Arguments, Me.MultiTimesAllowed)
        End Function

        ''' <summary>
        ''' Converts this instance to a Plugin(Of IOutput)
        ''' </summary>
        ''' <returns></returns>
        Public Function ToOutputPlugin() As Plugin(Of IOutput)
            Return New Plugin(Of IOutput)(Me.Name, Me.DisplayName, Me.Dll, Me.ClassName, Me.Description, Me.Arguments, Me.MultiTimesAllowed)
        End Function

        ''' <summary>
        ''' Converts this instance to a Plugin(Of IScreenshotType)
        ''' </summary>
        ''' <returns></returns>
        Public Function ToTypePlugin() As Plugin(Of IScreenshotType)
            Return New Plugin(Of IScreenshotType)(Me.Name, Me.DisplayName, Me.Dll, Me.ClassName, Me.Description, Me.Arguments, Me.MultiTimesAllowed)
        End Function

        ''' <summary>
        ''' Converts this instance to a Plugin(Of IEffect)
        ''' </summary>
        ''' <returns></returns>
        Public Function ToEffectPlugin() As Plugin(Of IEffect)
            Return New Plugin(Of IEffect)(Me.Name, Me.DisplayName, Me.Dll, Me.ClassName, Me.Description, Me.Arguments, Me.MultiTimesAllowed)
        End Function

        ''' <summary>
        ''' Converts this instance to a Plugin(Of ITriggerManager)
        ''' </summary>
        ''' <returns></returns>
        Public Function ToTriggerPlugin() As Plugin(Of ITriggerManager)
            Return New Plugin(Of ITriggerManager)(Me.Name, Me.DisplayName, Me.Dll, Me.ClassName, Me.Description, Me.Arguments, Me.MultiTimesAllowed)
        End Function


#End Region

    End Class
End Namespace
