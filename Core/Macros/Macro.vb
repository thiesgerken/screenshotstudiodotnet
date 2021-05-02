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

Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Macros
    Public Class Macro

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the type of the ScreenShot.
        ''' </summary>
        ''' <value>The type.</value>
        Public Property Type() As Plugin(Of IScreenshotType)

        ''' <summary>
        ''' Gets or sets the outputs.
        ''' </summary>
        ''' <value>The output.</value>
        Public Property Outputs() As New SerializableList(Of Plugin(Of IOutput))

        ''' <summary>
        ''' Gets or sets the effects.
        ''' </summary>
        ''' <value>The effects.</value>
        Public Property Effects() As New SerializableList(Of Plugin(Of IEffect))

        ''' <summary>
        ''' Gets or sets the triggers.
        ''' </summary>
        ''' <value>The triggers.</value>
        Public Property Triggers() As New SerializableList(Of Plugin(Of ITriggerManager))

        ''' <summary>
        ''' Gets or sets the delay of the macro.
        ''' </summary>
        ''' <value>The delay, in miliseconds.</value>
        Public Property Delay() As Integer

        ''' <summary>
        ''' Gets or sets the multiple infos for this macro.
        ''' </summary>
        ''' <value>The multipleparameters.</value>
        Public Property Multiple() As MultipleParameters

        ''' <summary>
        ''' Gets or sets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public Property Name() As String

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Macro" /> class.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="type">The type.</param>
        ''' <param name="outputs">The outputs.</param>
        ''' <param name="triggers">The triggers.</param>
        ''' <param name="effects">The effects.</param>
        ''' <param name="delay">The delay.</param>
        ''' <param name="multi">The multi.</param>
        Public Sub New(ByVal name As String, ByVal type As Plugin(Of IScreenshotType), ByVal outputs As SerializableList(Of Plugin(Of IOutput)), ByVal triggers As SerializableList(Of Plugin(Of ITriggerManager)), ByVal effects As SerializableList(Of Plugin(Of IEffect)), ByVal delay As Integer, ByVal multi As MultipleParameters)
            Me.Name = name
            Me.Type = type
            Me.Outputs = outputs
            Me.Effects = effects
            Me.Triggers = triggers
            Me.Delay = delay
            Me.Multiple = multi
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Macro" /> class.
        ''' </summary>
        ''' <param name="type">The type.</param>
        Public Sub New(ByVal type As Plugin(Of IScreenshotType))
            Me.Name = type.DisplayName
            Me.Type = type
            Me.Triggers = New SerializableList(Of Plugin(Of ITriggerManager))
            Me.Outputs = New SerializableList(Of Plugin(Of IOutput))
            Me.Effects = New SerializableList(Of Plugin(Of IEffect))
            Me.Delay = 0
            Me.Multiple = New MultipleParameters
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Macro" /> class.
        ''' </summary>
        Public Sub New()
            Me.Multiple = New MultipleParameters
            Me.Triggers = New SerializableList(Of Plugin(Of ITriggerManager))
            Me.Outputs = New SerializableList(Of Plugin(Of IOutput))
            Me.Effects = New SerializableList(Of Plugin(Of IEffect))
            Me.Delay = 0
        End Sub
#End Region
    End Class
End Namespace
