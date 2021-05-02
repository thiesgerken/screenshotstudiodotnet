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

Imports ScreenshotStudioDotnet.Core.Misc

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SettingsForm
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsForm))
            Me.TreeOptions = New System.Windows.Forms.TreeView()
            Me.panelButtons = New System.Windows.Forms.Panel()
            Me.btnApply = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.panelRoot = New System.Windows.Forms.Panel()
            Me.panelTypes = New System.Windows.Forms.Panel()
            Me.lblTypesInfo = New System.Windows.Forms.Label()
            Me.panelOutputs = New System.Windows.Forms.Panel()
            Me.lblPluginInfo = New System.Windows.Forms.Label()
            Me.timChangeChecker = New System.Windows.Forms.Timer()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.panelButtons.SuspendLayout()
            Me.panelRoot.SuspendLayout()
            Me.panelTypes.SuspendLayout()
            Me.panelOutputs.SuspendLayout()
            Me.SuspendLayout()
            '
            'TreeOptions
            '
            resources.ApplyResources(Me.TreeOptions, "TreeOptions")
            Me.TreeOptions.FullRowSelect = True
            Me.TreeOptions.Name = "TreeOptions"
            Me.TreeOptions.ShowLines = False
            Me.TreeOptions.ShowPlusMinus = False
            Me.TreeOptions.ShowRootLines = False
            '
            'panelButtons
            '
            resources.ApplyResources(Me.panelButtons, "panelButtons")
            Me.panelButtons.Controls.Add(Me.btnApply)
            Me.panelButtons.Controls.Add(Me.btnCancel)
            Me.panelButtons.Controls.Add(Me.btnOK)
            Me.panelButtons.Name = "panelButtons"
            '
            'btnApply
            '
            resources.ApplyResources(Me.btnApply, "btnApply")
            Me.btnApply.Name = "btnApply"
            Me.btnApply.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            resources.ApplyResources(Me.btnCancel, "btnCancel")
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'btnOK
            '
            resources.ApplyResources(Me.btnOK, "btnOK")
            Me.btnOK.BackColor = System.Drawing.Color.Transparent
            Me.btnOK.Name = "btnOK"
            Me.btnOK.UseVisualStyleBackColor = False
            '
            'panelRoot
            '
            resources.ApplyResources(Me.panelRoot, "panelRoot")
            Me.panelRoot.Controls.Add(Me.panelTypes)
            Me.panelRoot.Controls.Add(Me.panelOutputs)
            Me.panelRoot.Name = "panelRoot"
            '
            'panelTypes
            '
            resources.ApplyResources(Me.panelTypes, "panelTypes")
            Me.panelTypes.Controls.Add(Me.lblTypesInfo)
            Me.panelTypes.Name = "panelTypes"
            '
            'lblTypesInfo
            '
            resources.ApplyResources(Me.lblTypesInfo, "lblTypesInfo")
            Me.lblTypesInfo.Name = "lblTypesInfo"
            '
            'panelOutputs
            '
            resources.ApplyResources(Me.panelOutputs, "panelOutputs")
            Me.panelOutputs.Controls.Add(Me.lblPluginInfo)
            Me.panelOutputs.Name = "panelOutputs"
            '
            'lblPluginInfo
            '
            resources.ApplyResources(Me.lblPluginInfo, "lblPluginInfo")
            Me.lblPluginInfo.Name = "lblPluginInfo"
            '
            'timChangeChecker
            '
            Me.timChangeChecker.Enabled = True
            Me.timChangeChecker.Interval = 200
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            '
            'SettingsForm
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.panelRoot)
            Me.Controls.Add(Me.TreeOptions)
            Me.Controls.Add(Me.panelButtons)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "SettingsForm"
            Me.panelButtons.ResumeLayout(False)
            Me.panelRoot.ResumeLayout(False)
            Me.panelTypes.ResumeLayout(False)
            Me.panelTypes.PerformLayout()
            Me.panelOutputs.ResumeLayout(False)
            Me.panelOutputs.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TreeOptions As System.Windows.Forms.TreeView
        Friend WithEvents panelButtons As System.Windows.Forms.Panel
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents FormStateSaver As Core.Controls.FormStateSaver
        Friend WithEvents panelRoot As System.Windows.Forms.Panel
        Friend WithEvents btnApply As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents panelOutputs As System.Windows.Forms.Panel
        Friend WithEvents lblPluginInfo As System.Windows.Forms.Label
        Friend WithEvents panelTypes As System.Windows.Forms.Panel
        Friend WithEvents lblTypesInfo As System.Windows.Forms.Label
        Friend WithEvents timChangeChecker As System.Windows.Forms.Timer
    End Class
End Namespace
