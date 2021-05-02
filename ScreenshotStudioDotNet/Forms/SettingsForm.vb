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
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Settings

Namespace Forms
    Public Class SettingsForm

#Region "Fields"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the SettingsForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub SettingsForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Me.Size = New Size(593, 395)
            panelRoot.Size = New Size(400, 300)

            TreeOptions.Nodes.Clear()

            'Add found SettingsPanels
            TreeOptions.Nodes.AddRange(AddSettingPanels)

            'Add Types           
            Dim typesNode As New TreeNode(_langManager.GetString("types"))
            typesNode.Name = "Types"
            typesNode.Nodes.AddRange(AddPlugins((New PluginDatabase(Of IScreenshotType)).ToNeutralPluginList, "types"))
            TreeOptions.Nodes.Add(typesNode)

            'Add Outputs
            Dim outputsNode As New TreeNode(_langManager.GetString("outputs"))
            outputsNode.Name = "Outputs"
            outputsNode.Nodes.AddRange(AddPlugins((New PluginDatabase(Of IOutput)).ToNeutralPluginList, "output"))
            TreeOptions.Nodes.Add(outputsNode)

            TreeOptions.ExpandAll()
        End Sub

        ''' <summary>
        ''' Handles the AfterSelect event of the TreeOptions control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs" /> instance containing the event data.</param>
        Private Sub TreeOptions_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeOptions.AfterSelect
            For Each c As Control In panelRoot.Controls
                c.Visible = c.Name = "panel" & e.Node.Name
                c.Location = New Point(0, 0)
            Next
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the SettingsForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub SettingsForm_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            e.Graphics.DrawLine(Pens.DarkGray, New Point(panelRoot.Left, panelRoot.Bottom), New Point(panelRoot.Right, panelRoot.Bottom))
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnApply control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
            Save()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnOK control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()

            Save()
        End Sub

        ''' <summary>
        ''' Handles the Tick event of the timChangeChecker control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub timChangeChecker_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timChangeChecker.Tick
            Dim _changes As Boolean = False

            For Each c As Control In panelRoot.Controls
                If TypeOf (c) Is SettingsPanel Then
                    Dim p As SettingsPanel = CType(c, SettingsPanel)
                    If p.PropertiesChanged Then _changes = True
                End If
            Next

            btnApply.Enabled = _changes
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Adds the setting panels.
        ''' </summary>
        ''' <returns></returns>
        Private Function AddSettingPanels() As TreeNode()
            Dim _nodes As New List(Of TreeNode)

            'We know that they are in Core.dll, but writing the path here would make it harder to 
            'change the filenames later 
            'The SettingsPanel Class is in the same Assembly 
            Dim dll As Assembly = Assembly.GetAssembly(GetType(SettingsPanel))

            'Loop through each type in the DLL
            For Each t As Type In dll.GetTypes
                'Only look at public types
                If t.IsPublic = True Then
                    'Ignore abstract classes
                    If Not ((t.Attributes And TypeAttributes.Abstract) = TypeAttributes.Abstract) Then

                        'See if this type inherits from SettingsPanel
                        Dim base As Type = t.BaseType

                        If Not (base Is Nothing) Then
                            If base = GetType(SettingsPanel) Then
                                'Implements the SettingsPanel, so we can add it here                           

                                'grab an instance 
                                Dim panelInstance As SettingsPanel = CType(dll.CreateInstance(t.FullName), SettingsPanel)

                                If Not panelInstance.IsEmpty Then
                                    Dim settingNode As New TreeNode
                                    settingNode.Name = panelInstance.Name
                                    settingNode.Text = panelInstance.DisplayName

                                    _nodes.Add(settingNode)

                                    panelInstance.Name = "panel" & panelInstance.Name
                                    panelInstance.Visible = False
                                    panelRoot.Controls.Add(panelInstance)
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            Return _nodes.ToArray
        End Function

        ''' <summary>
        ''' Adds the plugins.
        ''' </summary>
        ''' <param name="plugins">The plugins.</param>
        ''' <param name="type">The type.</param>
        ''' <param name="node">The node.</param>
        Private Function AddPlugins(ByVal plugins As List(Of Plugin(Of IPlugin)), ByVal type As String) As TreeNode()
            Dim _nodes As New List(Of TreeNode)

            For Each p In plugins
                Dim instance As IPlugin

                instance = p.CreateInstance

                Dim pnl = instance.SettingsPanel

                If Not pnl Is (Nothing) Then
                    Dim pluginNode As New TreeNode
                    pluginNode.Name = type & p.DisplayName
                    pluginNode.Text = p.DisplayName

                    pluginNode.Nodes.AddRange(IterateSubPanels(pnl))

                    _nodes.Add(pluginNode)

                    pnl.Name = "panel" & type & p.DisplayName
                    pnl.Visible = False
                    panelRoot.Controls.Add(pnl)
                End If
            Next

            Return _nodes.ToArray
        End Function

        ''' <summary>
        ''' Iterates the sub panels.
        ''' </summary>
        ''' <param name="panel">The panel.</param>
        ''' <returns></returns>
        Private Function IterateSubPanels(ByVal panel As PluginSettingsPanel) As TreeNode()
            Dim nodes As New List(Of TreeNode)

            For Each subPnl As PluginSettingsPanel In panel.SubPanels

                If Not subPnl.IsEmpty Then
                    Dim subNode As New TreeNode
                    subNode.Name = subPnl.Name
                    subNode.Text = subPnl.DisplayName

                    subPnl.Name = "panel" & subPnl.Name
                    subPnl.Visible = False
                    panelRoot.Controls.Add(subPnl)

                    subNode.Nodes.AddRange(IterateSubPanels(subPnl))

                    nodes.Add(subNode)
                End If

            Next

            Return nodes.ToArray
        End Function


   ''' <summary>
        ''' Saves all settings.
        ''' </summary>
        Private Sub Save()
            For Each c As Control In panelRoot.Controls
                If TypeOf (c) Is SettingsPanel Then
                    Dim p As SettingsPanel = CType(c, SettingsPanel)

                    'Only save if something has changed
                    '(panels that haven't been shown propably have not initialized their controls yet; 
                    ' saving them would reset their settings)
                    If p.PropertiesChanged Then p.Save()
                End If
            Next

            QuickStart.Refresh()
        End Sub

#End Region
    End Class
End Namespace
