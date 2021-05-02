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

Namespace Settings
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ScreensSettingsPanel
        Inherits settingspanel

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
            Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
            Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScreensSettingsPanel))
            Me.listScreens = New System.Windows.Forms.ListView()
            Me.ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ScreenBounds = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.lblNone = New System.Windows.Forms.Label()
            Me.lblScreensInfo = New System.Windows.Forms.Label()
            Me.boxScreens = New System.Windows.Forms.GroupBox()
            Me.boxScreens.SuspendLayout()
            Me.SuspendLayout()
            '
            'listScreens
            '
            Me.listScreens.CheckBoxes = True
            Me.listScreens.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ID, Me.ScreenBounds})
            Me.listScreens.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
            resources.ApplyResources(Me.listScreens, "listScreens")
            Me.listScreens.Name = "listScreens"
            Me.listScreens.UseCompatibleStateImageBehavior = False
            Me.listScreens.View = System.Windows.Forms.View.Details
            '
            'ID
            '
            resources.ApplyResources(Me.ID, "ID")
            '
            'ScreenBounds
            '
            resources.ApplyResources(Me.ScreenBounds, "ScreenBounds")
            '
            'lblNone
            '
            resources.ApplyResources(Me.lblNone, "lblNone")
            Me.lblNone.Name = "lblNone"
            '
            'lblScreensInfo
            '
            resources.ApplyResources(Me.lblScreensInfo, "lblScreensInfo")
            Me.lblScreensInfo.Name = "lblScreensInfo"
            '
            'boxScreens
            '
            Me.boxScreens.Controls.Add(Me.listScreens)
            Me.boxScreens.Controls.Add(Me.lblNone)
            Me.boxScreens.Controls.Add(Me.lblScreensInfo)
            resources.ApplyResources(Me.boxScreens, "boxScreens")
            Me.boxScreens.Name = "boxScreens"
            Me.boxScreens.TabStop = False
            '
            'ScreensSettingsPanel
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.Controls.Add(Me.boxScreens)
            Me.Name = "ScreensSettingsPanel"
            Me.boxScreens.ResumeLayout(False)
            Me.boxScreens.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents listScreens As System.Windows.Forms.ListView
        Friend WithEvents ID As System.Windows.Forms.ColumnHeader
        Friend WithEvents ScreenBounds As System.Windows.Forms.ColumnHeader
        Friend WithEvents lblNone As System.Windows.Forms.Label
        Friend WithEvents lblScreensInfo As System.Windows.Forms.Label
        Friend WithEvents boxScreens As System.Windows.Forms.GroupBox

    End Class
End Namespace
