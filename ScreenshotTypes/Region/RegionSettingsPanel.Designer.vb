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

Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Region
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class RegionSettingsPanel
        Inherits PluginSettingsPanel

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegionSettingsPanel))
            Me.chkMagnifyingGlass = New System.Windows.Forms.CheckBox()
            Me.groupSettings = New System.Windows.Forms.GroupBox()
            Me.chkPaintCrosshair = New System.Windows.Forms.CheckBox()
            Me.groupSettings.SuspendLayout()
            Me.SuspendLayout()
            '
            'chkMagnifyingGlass
            '
            resources.ApplyResources(Me.chkMagnifyingGlass, "chkMagnifyingGlass")
            Me.chkMagnifyingGlass.Name = "chkMagnifyingGlass"
            Me.chkMagnifyingGlass.UseVisualStyleBackColor = True
            '
            'groupSettings
            '
            Me.groupSettings.Controls.Add(Me.chkPaintCrosshair)
            Me.groupSettings.Controls.Add(Me.chkMagnifyingGlass)
            resources.ApplyResources(Me.groupSettings, "groupSettings")
            Me.groupSettings.Name = "groupSettings"
            Me.groupSettings.TabStop = False
            '
            'chkPaintCrosshair
            '
            resources.ApplyResources(Me.chkPaintCrosshair, "chkPaintCrosshair")
            Me.chkPaintCrosshair.Name = "chkPaintCrosshair"
            Me.chkPaintCrosshair.UseVisualStyleBackColor = True
            '
            'RegionSettingsPanel
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.Controls.Add(Me.groupSettings)
            resources.ApplyResources(Me, "$this")
            Me.Name = "RegionSettingsPanel"
            Me.groupSettings.ResumeLayout(False)
            Me.groupSettings.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents chkMagnifyingGlass As System.Windows.Forms.CheckBox
        Friend WithEvents groupSettings As System.Windows.Forms.GroupBox
        Friend WithEvents chkPaintCrosshair As System.Windows.Forms.CheckBox

    End Class
End Namespace
