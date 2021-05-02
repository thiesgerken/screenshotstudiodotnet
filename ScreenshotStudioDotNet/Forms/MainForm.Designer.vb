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

Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MainForm
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
            Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.TrayMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.mnuShow = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuMacros = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuNewMacro = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuNewSeparator = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuManageMacros = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuShowHistory = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuClose = New System.Windows.Forms.ToolStripMenuItem()
            Me.TrayMenu.SuspendLayout()
            Me.SuspendLayout()
            '
            'NotifyIcon
            '
            Me.NotifyIcon.ContextMenuStrip = Me.TrayMenu
            Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
            Me.NotifyIcon.Text = "ScreenshotStudio.Net"
            Me.NotifyIcon.Visible = True
            '
            'TrayMenu
            '
            Me.TrayMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShow, Me.ToolStripSeparator2, Me.mnuMacros, Me.mnuManageMacros, Me.mnuShowHistory, Me.mnuSettings, Me.mnuAbout, Me.ToolStripSeparator1, Me.mnuClose})
            Me.TrayMenu.Name = "TrayMenu"
            Me.TrayMenu.Size = New System.Drawing.Size(167, 192)
            '
            'mnuShow
            '
            Me.mnuShow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.mnuShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.mnuShow.Name = "mnuShow"
            Me.mnuShow.Size = New System.Drawing.Size(166, 22)
            Me.mnuShow.Text = "Show Quickstart"
            '
            'ToolStripSeparator2
            '
            Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
            Me.ToolStripSeparator2.Size = New System.Drawing.Size(163, 6)
            '
            'mnuMacros
            '
            Me.mnuMacros.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewMacro, Me.mnuNewSeparator})
            Me.mnuMacros.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.mnuMacros.Name = "mnuMacros"
            Me.mnuMacros.Size = New System.Drawing.Size(166, 22)
            Me.mnuMacros.Text = "Execute Macro"
            '
            'mnuNewMacro
            '
            Me.mnuNewMacro.Image = CType(resources.GetObject("mnuNewMacro.Image"), System.Drawing.Image)
            Me.mnuNewMacro.Name = "mnuNewMacro"
            Me.mnuNewMacro.Size = New System.Drawing.Size(147, 22)
            Me.mnuNewMacro.Text = "New Macro ..."
            '
            'mnuNewSeparator
            '
            Me.mnuNewSeparator.Name = "mnuNewSeparator"
            Me.mnuNewSeparator.Size = New System.Drawing.Size(144, 6)
            '
            'mnuManageMacros
            '
            Me.mnuManageMacros.Name = "mnuManageMacros"
            Me.mnuManageMacros.Size = New System.Drawing.Size(166, 22)
            Me.mnuManageMacros.Text = "Manage Macros"
            '
            'mnuShowHistory
            '
            Me.mnuShowHistory.Name = "mnuShowHistory"
            Me.mnuShowHistory.Size = New System.Drawing.Size(166, 22)
            Me.mnuShowHistory.Text = "Show History"
            '
            'mnuSettings
            '
            Me.mnuSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.mnuSettings.Name = "mnuSettings"
            Me.mnuSettings.Size = New System.Drawing.Size(166, 22)
            Me.mnuSettings.Text = "Settings"
            '
            'mnuAbout
            '
            Me.mnuAbout.Name = "mnuAbout"
            Me.mnuAbout.Size = New System.Drawing.Size(166, 22)
            Me.mnuAbout.Text = "About"
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            Me.ToolStripSeparator1.Size = New System.Drawing.Size(163, 6)
            '
            'mnuClose
            '
            Me.mnuClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.mnuClose.Name = "mnuClose"
            Me.mnuClose.Size = New System.Drawing.Size(166, 22)
            Me.mnuClose.Text = "Exit"
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(179, 69)
            Me.ControlBox = False
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MainForm"
            Me.Text = "MainForm"
            Me.TrayMenu.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
        Friend WithEvents TrayMenu As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents mnuShow As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents mnuMacros As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuShowHistory As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuSettings As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents mnuClose As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuManageMacros As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuNewMacro As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuNewSeparator As System.Windows.Forms.ToolStripSeparator
    End Class
End Namespace
