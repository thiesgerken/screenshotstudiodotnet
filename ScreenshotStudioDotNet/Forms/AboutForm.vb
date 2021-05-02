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
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Aero

Namespace Forms
    Public Class AboutForm

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the AboutForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub AboutForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Me.GlassMargins = New Margins(Panel.Left - 2, Me.ClientRectangle.Width - Panel.Right - 2, Panel.Top - 2, Me.ClientRectangle.Height - Panel.Bottom - 2)

            lblInfo2.Text = lblInfo2.Text.Replace("[version]", My.Application.Info.Version.ToString(3))
            lblInfo2.Text = lblInfo2.Text.Replace("[build]", RetrieveLinkerTimestamp(Assembly.GetExecutingAssembly().Location).ToString)
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the AboutForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub AboutForm_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            'e.Graphics.DrawLine(Pens.DarkGray, 225, 15, 225, Me.ClientRectangle.Height - 15)
            e.Graphics.DrawImage(My.Resources.cameraImage, New Rectangle(0, 45, 225, 231))
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnClose control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCheckUpdates control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCheckUpdates_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheckUpdates.Click
            MainForm.Updater.BeginUpdating()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnHomepage control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnHomepage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHomepage.Click
            Process.Start("http://www.thiesgerken.de")
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnSSSWeb control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnSSSWeb_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSSSWeb.Click
            Process.Start("http://www.screenshotstudio.net")
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnShowLog control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnShowLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowLog.Click
            Dim v As New LogViewer
            Me.Hide()
            v.ShowDialog()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Retrieves the linker timestamp.
        ''' </summary>
        ''' <param name="filePath">The file path.</param>
        ''' <returns></returns>
        Function RetrieveLinkerTimestamp(ByVal filePath As String) As DateTime
            Const PeHeaderOffset As Integer = 60
            Const LinkerTimestampOffset As Integer = 8

            Dim b(2047) As Byte
            Dim s As Stream = Nothing
            'set to Nothing to get rid of the null-reference-warning
            Try
                s = New FileStream(filePath, FileMode.Open, FileAccess.Read)
                s.Read(b, 0, 2048)
            Finally
                If Not s Is Nothing Then s.Close()
            End Try

            Dim i As Integer = BitConverter.ToInt32(b, PeHeaderOffset)

            Dim SecondsSince1970 As Integer = BitConverter.ToInt32(b, i + LinkerTimestampOffset)
            Dim dt As New DateTime(1970, 1, 1, 0, 0, 0)
            dt = dt.AddSeconds(SecondsSince1970)
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours)
            Return dt
        End Function

#End Region
    End Class
End Namespace
