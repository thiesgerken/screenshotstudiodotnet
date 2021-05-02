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

Imports System.Resources
Imports System.Reflection
Imports ScreenshotStudioDotNet.Misc
Imports ScreenshotStudioDotNet.Core.History
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Settings
Imports Updater.Library
Imports ScreenshotStudioDotNet.Core.Logging

Namespace Forms
    Public Class MainForm

#Region "Fields"

        Private WithEvents _updater As New UpdateAgent("http://updates.thiesgerken.de", "screenshotstudiodotnet6", My.Application.Info.Version, StaticProperties.UpdateDirectory)
        Private WithEvents _creator As New ScreenshotCreator
        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Strings", Assembly.GetExecutingAssembly)

        Private _triggers As New List(Of ITriggerManager)

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the macro positions.
        ''' </summary>
        ''' <value>The macro positions.</value>
        Public Property MacroPositions() As String(,)

        ''' <summary>
        ''' Gets or sets the updater.
        ''' </summary>
        ''' <value>The updater.</value>
        Public Property Updater() As UpdateAgent
            Get
                Return _updater
            End Get
            Set(ByVal value As UpdateAgent)
                _updater = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the creator.
        ''' </summary>
        ''' <value>The creator.</value>
        Public Property Creator() As ScreenshotCreator
            Get
                Return _creator
            End Get
            Set(ByVal value As ScreenshotCreator)
                _creator = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the type database.
        ''' </summary>
        ''' <value>The type database.</value>
        Public Property TypeDatabase As New PluginDatabase(Of IScreenshotType)

        ''' <summary>
        ''' Gets or sets the output database.
        ''' </summary>
        ''' <value>The output database.</value>
        Public Property OutputDatabase As New PluginDatabase(Of IOutput)

        ''' <summary>
        ''' Gets or sets the trigger manager database.
        ''' </summary>
        ''' <value>The trigger manager database.</value>
        Public Property TriggerManagerDatabase As New PluginDatabase(Of ITriggerManager)

        ''' <summary>
        ''' Gets or sets the effects database.
        ''' </summary>
        ''' <value>The effects database.</value>
        Public Property EffectsDatabase As New PluginDatabase(Of IEffect)

        ''' <summary>
        ''' Gets or sets a value indicating whether [notify icon visible].
        ''' </summary>
        ''' <value><c>true</c> if [notify icon visible]; otherwise, <c>false</c>.</value>
        Public Property NotifyIconVisible() As Boolean
            Get
                Return NotifyIcon.Visible
            End Get
            Set(ByVal value As Boolean)
                NotifyIcon.Visible = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the macro database.
        ''' </summary>
        ''' <value>The macro database.</value>
        Public Property MacroDatabase As New MacroDatabase
        
        ''' <summary>
        ''' Gets or sets a value indicating whether [jumplist refreshed].
        ''' </summary>
        ''' <value><c>true</c> if [jumplist refreshed]; otherwise, <c>false</c>.</value>
        Public Property JumplistRefreshed As Boolean

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MainForm" /> class.
        ''' </summary>
        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Width = 1
            Me.Height = 1
        End Sub

#End Region


#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Click event of the mnuAbout control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuAbout.Click
            AboutForm.ShowDialog()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuSettings control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuSettings.Click
            Dim s As New SettingsForm
            s.ShowDialog()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuShow control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuShow.Click
            QuickStart.Show()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the ShowHistoryToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub ShowHistoryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuShowHistory.Click
            Dim h As New HistoryViewer
            Me.Hide()
            h.ShowDialog()
        End Sub

        ''' <summary>
        ''' Handles the ApplicationEndRequested event of the _updater control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub updater_ApplicationEndRequested(ByVal sender As Object, ByVal e As EventArgs) Handles _updater.ApplicationEndRequested
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuClose control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuClose.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the HideRequested event of the _creator control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub creator_HideRequested(ByVal sender As Object, ByVal e As EventArgs) Handles _creator.HideRequested
            QuickStart.CloseThreadSafe()
        End Sub

        ''' <summary>
        ''' Handles the MouseClick event of the NotifyIcon control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub NotifyIcon_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles NotifyIcon.MouseClick
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                If QuickStart.Visible Then
                    QuickStart.Close()
                Else
                    QuickStart.Show()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the Load event of the MainForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            UpdateMacros()

            'Initialize Colorizations
            Dim col = SettingsDatabase.Colorization.Hovered

            'Check for first start
            If SettingsDatabase.IsFirstStart Then
                AboutForm.ShowDialog()
                SettingsDatabase.IsFirstStart = False
            End If

            CommandLineParser.ParseCommandLine(False)

            If SettingsDatabase.ShowQuickStartOnStartup Then
                QuickStart.Show()
            End If

        End Sub

        ''' <summary>
        ''' Handles the TriggerTriggered event of the triggerManager control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="ScreenshotStudioDotNet.Core.Extensibility.TriggerTriggeredEventArgs" /> instance containing the event data.</param>
        Private Sub triggerManager_TriggerTriggered(ByVal sender As Object, ByVal e As TriggerTriggeredEventArgs)
            Debug.Print("trig")
        End Sub


        ''' <summary>
        ''' Handles the ScreenshotTaken event of the _creator control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="ScreenshotStudioDotnet.Core.Screenshots.ScreenshotTakenEventArgs" /> instance containing the event data.</param>
        Private Sub creator_ScreenshotTaken(ByVal sender As Object, ByVal e As ScreenshotTakenEventArgs) Handles _creator.ScreenshotTaken
            If e.ScreenshotsTaken = e.ScreenshotsTotal Then
                'if last screenshot display balloon

                If e.ScreenshotsTaken = 1 Then
                    Dim s As String

                    If e.OutputsUsed.Count = 0 Then
                        s = _langManager.GetString("ScreenshotTakenNoOutput")
                    Else
                        s = _langManager.GetString("Output") & ": " & vbCrLf

                        For i As Integer = 0 To e.OutputsUsed.Count - 1

                            If e.OutputInfo(i) <> "" Then
                                s &= e.OutputInfo(i) & vbCrLf
                            Else
                                s &= e.OutputsUsed(i).DisplayName
                            End If
                        Next
                    End If
                    NotifyIcon.ShowBalloonTip(3000, _langManager.GetString("ScreenshotTaken"), s, ToolTipIcon.Info)
                Else
                    NotifyIcon.ShowBalloonTip(3000, _langManager.GetString("MacroTaken"), _langManager.GetString("MacroTakenText"), ToolTipIcon.Info)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuMacro control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuMacro_Click(ByVal sender As Object, ByVal e As EventArgs)
            _creator.CreateScreenshotAsync(_MacroDatabase(CType(sender, ToolStripMenuItem).Name))
        End Sub

        ''' <summary>
        ''' Handles the Shown event of the MainForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub MainForm_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
            Me.Visible = False
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuManageMacros control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuManageMacros_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageMacros.Click
            Dim m As New MacroManager
            m.ShowDialog()

            UpdateMacros()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the mnuNewMacro control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuNewMacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewMacro.Click
            Dim m As New MacroGenerator
            m.ShowDialog()

            If m.Macro IsNot Nothing Then
                MacroDatabase.Add(m.Macro)
                MacroDatabase.Save()

                UpdateMacros()
            End If
        End Sub
#End Region

#Region "Functions"

        ''' <summary>
        ''' Updates the macros.
        ''' </summary>
        Private Sub UpdateMacros()
            _MacroDatabase = New MacroDatabase

            'Update Lists 
            mnuMacros.DropDownItems.Clear()
            For Each m As Macro In _MacroDatabase
                Dim mnu As New ToolStripMenuItem
                mnu.Text = m.Name
                mnu.Name = m.Name

                AddHandler mnu.Click, AddressOf mnuMacro_Click

                mnuMacros.DropDownItems.Add(mnu)
            Next

            mnuMacros.DropDownItems.Add(mnuNewSeparator)
            mnuMacros.DropDownItems.Add(mnuNewMacro)

            'Refresh the form size
            Me.MacroPositions = CalculateMacroPositions()

            'Load Triggers

            'Sort the Triggers
            Dim triggerList As New Dictionary(Of String, List(Of Plugin(Of ITriggerManager)))
            For Each m In _MacroDatabase
                For Each t In m.Triggers
                    If Not triggerList.ContainsKey(t.Name) Then
                        triggerList.Add(t.Name, New List(Of Plugin(Of ITriggerManager)))
                    End If

                    triggerList(t.Name).Add(t)
                Next
            Next

            For Each t In _triggers
                t.StopListening()
            Next

            _triggers.Clear()

            For Each tm In _TriggerManagerDatabase
                Dim trig = CType(tm.CreateInstance(), ITriggerManager)

                If triggerList.ContainsKey(tm.Name) Then
                    For Each t In triggerList(tm.Name)
                        trig.AddTrigger(t)
                    Next

                    _triggers.Add(trig)
                    
                    RemoveHandler trig.TriggerTriggered, AddressOf triggerManager_TriggerTriggered
                    AddHandler trig.TriggerTriggered, AddressOf triggerManager_TriggerTriggered

                    Log.LogInformation("(Re)Starting Listening for Trigger " & tm.Name)

                    trig.StartListening()
                End If
            Next
        End Sub

        ''' <summary>
        ''' Calculates the macro positions.
        ''' </summary>
        ''' <returns></returns>
        Private Function CalculateMacroPositions() As String(,)
            Dim macrosToShow As List(Of Macro) = _macroDatabase.GetMacrosWithTrigger("Quickstart")

            'Determine the length of the edges
            Dim edgeLength As Integer = macrosToShow.Count
            While Math.Sqrt(edgeLength) Mod 1 <> 0
                edgeLength += 1
            End While

            Dim edgeLengthX = CInt(Math.Sqrt(edgeLength))
            Dim edgeLengthY = CInt(Math.Sqrt(edgeLength))

            Do Until edgeLengthX * edgeLengthY < macrosToShow.Count
                edgeLengthY -= 1
            Loop

            edgeLengthY += 1

            Dim positions(edgeLengthX - 1, edgeLengthY - 1) As String

            For y As Integer = 0 To edgeLengthY - 1
                For x As Integer = 0 To edgeLengthX - 1
                    If edgeLengthX * y + x <= macrosToShow.Count - 1 Then
                        Dim m As Macro = macrosToShow(edgeLengthX * y + x)

                        positions(x, y) = m.Name
                    End If
                Next
            Next

            Return positions
        End Function

#End Region

      
    End Class
End Namespace
