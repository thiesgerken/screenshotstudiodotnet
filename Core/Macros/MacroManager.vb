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
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Macros
    Public Class MacroManager
#Region "Fields"

        Private macroDatabase As New MacroDatabase

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Click event of the btnNew control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Dim macroGen As New MacroGenerator
            macroGen.ShowDialog()

            If macroGen.Macro IsNot Nothing Then
                macroDatabase.Add(macroGen.Macro)
                macroDatabase.Save()
            End If

            UpdateMacroList()
        End Sub

        ''' <summary>
        ''' Handles the Load event of the MacroManager control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub MacroManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            UpdateMacroList()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnClose control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub
        
        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the listMacros control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub listMacros_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listMacros.SelectedIndexChanged
            btnEdit.Enabled = listMacros.SelectedItems.Count > 0
            btnDelete.Enabled = listMacros.SelectedItems.Count > 0
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Updates the macro list.
        ''' </summary>
        Private Sub UpdateMacroList()
            listMacros.Items.Clear()

            For Each m In macroDatabase
                Dim triggers As String = List(New Plugin(Of ITriggerManager)().ToNeutralPluginList(m.Triggers.ToList))
                Dim outputs As String = List(New Plugin(Of IOutput)().ToNeutralPluginList(m.Outputs.ToList))
                Dim effects As String = List(New Plugin(Of IEffect)().ToNeutralPluginList(m.Effects.ToList))
                Dim delay As String = If(m.Delay <> 0, m.Delay & " seconds", "")
                Dim multiple As String = If(m.Multiple.Count <> 1, m.Multiple.Count & ", every " & m.Multiple.Interval & " milisecond" & If(m.Multiple.Interval <> 1, "s", ""), "")

                listMacros.Items.Add(New ListViewItem(New String() {m.Name, triggers, m.Type.DisplayName, effects, outputs, delay, multiple}))
            Next

            listMacros.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        End Sub

        ''' <summary>
        ''' Lists the specified plug list.
        ''' </summary>
        ''' <param name="plugList">The plug list.</param>
        ''' <returns></returns>
        Private Function List(ByVal plugList As List(Of Plugin(Of IPlugin))) As String
            Dim output As String = ""

            For Each p In plugList
                If Not output.Contains(p.DisplayName) Then output &= p.DisplayName & ", "
            Next

            If output.Length > 2 Then output = output.Substring(0, output.Length - 2)

            Return output
        End Function

#End Region

    End Class
End Namespace
