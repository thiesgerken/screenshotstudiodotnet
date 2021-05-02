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

Namespace Logging
    Public Class LogEntry

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the log message.
        ''' </summary>
        ''' <value>The log message.</value>
        Public Property LogMessage() As String

        ''' <summary>
        ''' Gets or sets the name of the computer.
        ''' </summary>
        ''' <value>The name of the computer.</value>
        Public Property ComputerName() As String

        ''' <summary>
        ''' Gets or sets the process id.
        ''' </summary>
        ''' <value>The process id.</value>
        Public Property ProcessId() As Integer

        ''' <summary>
        ''' Gets or sets the name of the process.
        ''' </summary>
        ''' <value>The name of the process.</value>
        Public Property ProcessName() As String

        ''' <summary>
        ''' Gets or sets the time created.
        ''' </summary>
        ''' <value>The time created.</value>
        Public Property TimeCreated() As Date

        ''' <summary>
        ''' Gets or sets the type.
        ''' </summary>
        ''' <value>The type.</value>
        Public Property Type() As String

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="LogEntry" /> class.
        ''' </summary>
        ''' <param name="type">The type.</param>
        ''' <param name="timeCreated">The time created.</param>
        ''' <param name="processName">Name of the process.</param>
        ''' <param name="processId">The process id.</param>
        ''' <param name="computerName">Name of the computer.</param>
        ''' <param name="logMessage">The log message.</param>
        Public Sub New(ByVal type As String, ByVal timeCreated As Date, ByVal processName As String, ByVal processId As Integer, ByVal computerName As String, ByVal logMessage As String)
            _Type = type
            _TimeCreated = timeCreated
            _ProcessName = processName
            _ProcessId = processId
            _ComputerName = computerName
            _LogMessage = logMessage
        End Sub

        Public Sub New()
            _Type = ""
            _TimeCreated = Now
            _ProcessName = ""
            _ProcessId = 0
            _ComputerName = ""
            _LogMessage = ""
        End Sub

#End Region
    End Class
End Namespace
