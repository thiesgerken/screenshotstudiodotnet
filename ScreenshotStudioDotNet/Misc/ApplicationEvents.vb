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

Imports ScreenshotStudioDotNet.Misc
Imports ScreenshotStudioDotNet.Core.Logging
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        ''' <summary>
        ''' Handles the Shutdown event of the MyApplication control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shutdown
            Core.Logging.Log.LogInformation("Application Exited")
        End Sub

        ''' <summary>
        ''' Handles the Startup event of the MyApplication control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="Microsoft.VisualBasic.ApplicationServices.StartupEventArgs" /> instance containing the event data.</param>
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As StartupEventArgs) Handles Me.Startup
            Core.Logging.Log.LogInformation("Application Started")
        End Sub

        ''' <summary>
        ''' Handles the StartupNextInstance event of the MyApplication control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs" /> instance containing the event data.</param>
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            CommandLineParser.ParseCommandLine(e.CommandLine.ToArray, True)
        End Sub

        ''' <summary>
        ''' Handles the UnhandledException event of the MyApplication control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs" /> instance containing the event data.</param>
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Core.Logging.Log.LogError(e.Exception)
            Core.Logging.Log.LogInformation("Application Exit because of Unhandled exception")
            e.ExitApplication = True
        End Sub
    End Class
End Namespace

