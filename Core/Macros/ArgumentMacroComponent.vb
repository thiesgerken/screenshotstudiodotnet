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

Imports System.Windows.Forms
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Macros
    Public MustInherit Class ArgumentMacroComponent
        Inherits MacroComponent

#Region "Private Fields"

        Private _plugin As New Plugin(Of IArgumentPlugin)

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the text that will be painted on the control.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The text associated with this control.</returns>
        Protected Overrides ReadOnly Property Text As String
            Get
                Dim s As String = Plugin.DisplayName

                If InUse Then
                    s &= vbCrLf
                    s &= Plugin.CreateInstance.ArgumentsToString(Plugin.Arguments)
                End If

                Return s
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the plugin.
        ''' </summary>
        ''' <value>The plugin.</value>
        Public Property Plugin As Plugin(Of IArgumentPlugin)
            Get
                Return _plugin
            End Get
            Set(ByVal value As Plugin(Of IArgumentPlugin))
                _plugin = value
                Init(True)
            End Set
        End Property

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DisabledMacroComponent" /> class.
        ''' </summary>
        ''' <param name="form">The form.</param>
        Public Sub New(ByVal form As MacroGenerator, ByVal name As String)
            MyBase.New(form, name)
            Init()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="OutputMacroComponent" /> class.
        ''' </summary>
        Public Sub New()
            Init()
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Inits this instance.
        ''' </summary>
        Public Overrides Sub Init(ByVal force As Boolean)
            If Not Me.Inited Or force Then
                MyBase.Init(force)

                If Plugin IsNot Nothing AndAlso Plugin.CreateInstance.ArgumentDesigner IsNot Nothing Then

                    Dim mnuEdit As New ToolStripMenuItem("Edit")
                    mnuEdit.Name = "mnuEdit"
                    mnuEdit.Font = New Font("Segoe UI", mnuEdit.Font.Size, FontStyle.Bold)
                    mnuEdit.Image = My.Resources.settings_16

                    ContextMenu.Items.Add(mnuEdit)
                    RemoveHandler mnuEdit.Click, AddressOf mnuEdit_Click
                    AddHandler mnuEdit.Click, AddressOf mnuEdit_Click

                    If Not ParentForm Is Nothing Then
                        RemoveHandler mnuEdit.Click, AddressOf mnuEdit_Click
                        AddHandler mnuEdit.Click, AddressOf mnuEdit_Click

                        RemoveHandler ParentForm.MouseUp, AddressOf parentForm_MouseUp
                        AddHandler ParentForm.MouseUp, AddressOf parentForm_MouseUp
                    End If
                End If

                Inited = True
            End If
        End Sub

        ''' <summary>
        ''' Inits this instance.
        ''' </summary>
        Public Overrides Sub Init()
            Init(False)
        End Sub

        ''' <summary>
        ''' Shows the editor.
        ''' </summary>
        Private Sub ShowEditor()
            Dim f = Plugin.CreateInstance.ArgumentDesigner

            If f IsNot Nothing Then
                f.Result = Plugin.Arguments
                f.ShowDialog()
                Plugin.Arguments = f.Result
                ParentForm.Refresh()
            End If
        End Sub
#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Click event of the mnuEdit control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
            ShowEditor()
        End Sub
        
        ''' <summary>
        ''' Handles the MouseUp event of the parentForm.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub parentForm_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            If e.Button = MouseButtons.Left And Not ParentForm.Dragging And Me.Visible And Me.IsOnPoint(e.Location) And Me.InUse Then
                ShowEditor()
            End If
        End Sub
#End Region
    End Class
End Namespace
