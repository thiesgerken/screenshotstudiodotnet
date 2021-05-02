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

Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Quickstart
    ''' <summary>
    ''' The Trigger to show the macro in the Quickstart.
    ''' No Add/Remove Functions needed because sss.net checks which 
    ''' Macros contain this trigger and displays them.
    ''' </summary>
    Public Class QuickstartTriggerManager
        Implements ITriggerManager
        
        ''' <summary>
        ''' Adds a trigger.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Public Sub AddTrigger(ByVal triggerInfo As Plugin(Of ITriggerManager)) Implements ITriggerManager.AddTrigger
        End Sub

        ''' <summary>
        ''' Removes a trigger.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Public Sub RemoveTrigger(ByVal triggerInfo As Plugin(Of ITriggerManager)) Implements ITriggerManager.RemoveTrigger
        End Sub

        ''' <summary>
        ''' Occurs when a trigger was fired.
        ''' </summary>
        Public Event TriggerTriggered(ByVal sender As Object, ByVal e As TriggerTriggeredEventArgs) Implements ITriggerManager.TriggerTriggered

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description As String Implements Core.Extensibility.ITriggerManager.Description
            Get
                Return "Shows the Macro in the Quickstart" 'Todo: Loc
            End Get
        End Property

        ''' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public ReadOnly Property DisplayName As String Implements Core.Extensibility.ITriggerManager.DisplayName
            Get
                Return "Quickstart"
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name As String Implements Core.Extensibility.ITriggerManager.Name
            Get
                Return "Quickstart"
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether this plugin is available.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is available; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsAvailable As Boolean Implements Core.Extensibility.IPlugin.IsAvailable
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel As Core.Extensibility.PluginSettingsPanel Implements Core.Extensibility.IPlugin.SettingsPanel
            Get
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Starts the listening for fired triggers.
        ''' </summary>
        Public Sub StartListening() Implements Core.Extensibility.ITriggerManager.StartListening
        End Sub

        ''' <summary>
        ''' Stops the recognition of triggers.
        ''' </summary>
        Public Sub StopListening() Implements Core.Extensibility.ITriggerManager.StopListening
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether its okay to add this plugin more than one time to a macro.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is supported multiple times; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.ITriggerManager.IsSupportedMultipleTimes
            Get
                Return False
            End Get
        End Property
    End Class
End Namespace
