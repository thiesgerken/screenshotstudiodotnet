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

Namespace Extensibility
    Public Interface ITriggerManager
        Inherits IPlugin

        ''' <summary>
        ''' Adds a trigger.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Sub AddTrigger(ByVal triggerInfo As Plugin(Of ITriggerManager))

        ''' <summary>
        ''' Removes a trigger.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Sub RemoveTrigger(ByVal triggerInfo As Plugin(Of ITriggerManager))

        ''' <summary>
        ''' Occurs when a trigger was fired.
        ''' </summary>
        Event TriggerTriggered(ByVal sender As Object, ByVal e As TriggerTriggeredEventArgs)

        ''' <summary>
        ''' Starts the listening for fired triggers.
        ''' </summary>
        Sub StartListening()

        ''' <summary>
        ''' Stops the recognition of triggers.
        ''' </summary>
        Sub StopListening()
    End Interface
End Namespace
