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
Imports System.Windows.Forms

Namespace Hotkey
    Public Class HotkeyManager
        Implements ITriggerManager

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description As String Implements Core.Extensibility.IPlugin.Description
            Get
                Return "Starts the screenshot when a key-combination is pressed, such as Control + Print" 'Todo: Loc
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName As String Implements Core.Extensibility.IPlugin.DisplayName
            Get
                Return "Hotkey"
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
                'I think almost everyone has a keyboard connected to his computer while using this application.
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property Name As String Implements Core.Extensibility.IPlugin.Name
            Get
                Return "Hotkey"
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

        Private _triggers As New List(Of Plugin(Of ITriggerManager))
        Private _form As Form
        Private WithEvents _manager As HotkeyManagerInternal

        ''' <summary>
        ''' Adds a trigger.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Public Sub AddTrigger(ByVal triggerInfo As Plugin(Of ITriggerManager)) Implements Core.Extensibility.ITriggerManager.AddTrigger
            _triggers.Add(triggerInfo)
        End Sub

        ''' <summary>
        ''' Removes a trigger.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Public Sub RemoveTrigger(ByVal triggerInfo As Plugin(Of ITriggerManager)) Implements Core.Extensibility.ITriggerManager.RemoveTrigger
            _triggers.Remove(triggerInfo)
        End Sub

        ''' <summary>
        ''' Starts the listening for fired triggers.
        ''' </summary>
        Public Sub StartListening() Implements Core.Extensibility.ITriggerManager.StartListening
            _form = New Form
            _form.Name = "HotkeyForm"
            
            _manager = New HotkeyManagerInternal(_form)

            For Each t In _triggers
                _manager.AddHotKey(CType(t.GetParameter("hotkey"), Hotkey))
            Next
        End Sub

        ''' <summary>
        ''' Stops the recognition of triggers.
        ''' </summary>
        Public Sub StopListening() Implements Core.Extensibility.ITriggerManager.StopListening
            _manager.RemoveAllHotKeys()
        End Sub

        ''' <summary>
        ''' Occurs when a trigger was fired.
        ''' </summary>
        Public Event TriggerTriggered(ByVal sender As Object, ByVal e As Core.Extensibility.TriggerTriggeredEventArgs) Implements Core.Extensibility.ITriggerManager.TriggerTriggered

        ''' <summary>
        ''' Handles the HotkeyPressed event of the _manager control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="ScreenshotStudioDotNet.Triggers.Hotkey.HotkeyPressedEventArgs" /> instance containing the event data.</param>
        Private Sub _manager_HotkeyPressed(ByVal sender As Object, ByVal e As HotkeyPressedEventArgs) Handles _manager.HotkeyPressed
            For Each t In _triggers
                Dim h = CType(t.GetParameter("hotkey"), Hotkey)

                If e.Hotkey.Modifier = h.Modifier And e.Hotkey.HotKey = h.HotKey Then
                    OnTriggerTriggered(t)
                    Exit For
                End If
            Next
        End Sub

        ''' <summary>
        ''' Called when [trigger triggered].
        ''' </summary>
        ''' <param name="trigger">The trigger.</param>
        Private Sub OnTriggerTriggered(ByVal trigger As Plugin(Of ITriggerManager))
            RaiseEvent TriggerTriggered(Me, New TriggerTriggeredEventArgs(trigger))
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether its okay to add this plugin more than one time to a macro.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is supported multiple times; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.ITriggerManager.IsSupportedMultipleTimes
            Get
                Return True
            End Get
        End Property
    End Class
End Namespace
