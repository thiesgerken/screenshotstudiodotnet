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

Namespace Window
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class WindowSettingsPanel
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WindowSettingsPanel))
            Me.chkWhiteForm = New System.Windows.Forms.CheckBox
            Me.groupWhiteForm = New System.Windows.Forms.GroupBox
            Me.lblWhiteFormInfo = New System.Windows.Forms.Label
            Me.groupFocus = New System.Windows.Forms.GroupBox
            Me.chkFocus = New System.Windows.Forms.CheckBox
            Me.groupWhiteForm.SuspendLayout()
            Me.groupFocus.SuspendLayout()
            Me.SuspendLayout()
            '
            'chkWhiteForm
            '
            resources.ApplyResources(Me.chkWhiteForm, "chkWhiteForm")
            Me.chkWhiteForm.Name = "chkWhiteForm"
            Me.chkWhiteForm.UseVisualStyleBackColor = True
            '
            'groupWhiteForm
            '
            Me.groupWhiteForm.Controls.Add(Me.lblWhiteFormInfo)
            Me.groupWhiteForm.Controls.Add(Me.chkWhiteForm)
            resources.ApplyResources(Me.groupWhiteForm, "groupWhiteForm")
            Me.groupWhiteForm.Name = "groupWhiteForm"
            Me.groupWhiteForm.TabStop = False
            '
            'lblWhiteFormInfo
            '
            resources.ApplyResources(Me.lblWhiteFormInfo, "lblWhiteFormInfo")
            Me.lblWhiteFormInfo.Name = "lblWhiteFormInfo"
            '
            'groupFocus
            '
            Me.groupFocus.Controls.Add(Me.chkFocus)
            resources.ApplyResources(Me.groupFocus, "groupFocus")
            Me.groupFocus.Name = "groupFocus"
            Me.groupFocus.TabStop = False
            '
            'chkFocus
            '
            resources.ApplyResources(Me.chkFocus, "chkFocus")
            Me.chkFocus.Name = "chkFocus"
            Me.chkFocus.UseVisualStyleBackColor = True
            '
            'WindowSettingsPanel
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.Controls.Add(Me.groupFocus)
            Me.Controls.Add(Me.groupWhiteForm)
            resources.ApplyResources(Me, "$this")
            Me.Name = "WindowSettingsPanel"
            Me.groupWhiteForm.ResumeLayout(False)
            Me.groupWhiteForm.PerformLayout()
            Me.groupFocus.ResumeLayout(False)
            Me.groupFocus.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents chkWhiteForm As System.Windows.Forms.CheckBox
        Friend WithEvents groupWhiteForm As System.Windows.Forms.GroupBox
        Friend WithEvents lblWhiteFormInfo As System.Windows.Forms.Label
        Friend WithEvents groupFocus As System.Windows.Forms.GroupBox
        Friend WithEvents chkFocus As System.Windows.Forms.CheckBox

    End Class
End Namespace
