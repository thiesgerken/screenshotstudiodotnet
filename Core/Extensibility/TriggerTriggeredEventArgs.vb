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
    Public Class TriggerTriggeredEventArgs
        Inherits EventArgs

        ''' <summary>
        ''' Gets or sets the trigger info.
        ''' </summary>
        ''' <value>The trigger info.</value>
        Public Property TriggerInfo As Plugin(Of ITriggerManager)

        ''' <summary>
        ''' Initializes a new instance of the <see cref="TriggerTriggeredEventArgs" /> class.
        ''' </summary>
        ''' <param name="triggerInfo">The trigger info.</param>
        Public Sub New(ByVal triggerInfo As Plugin(Of ITriggerManager))
            Me.TriggerInfo = triggerInfo
        End Sub

    End Class
End Namespace
