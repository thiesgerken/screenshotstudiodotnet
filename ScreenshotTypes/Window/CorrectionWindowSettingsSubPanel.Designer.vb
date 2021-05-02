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
    Partial Class CorrectionWindowSettingsSubPanel
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CorrectionWindowSettingsSubPanel))
            Me.btnWindow = New System.Windows.Forms.Button
            Me.boxCorrection = New System.Windows.Forms.GroupBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.panelAdjust = New System.Windows.Forms.Panel
            Me.btnWindowStandard = New System.Windows.Forms.Button
            Me.btnWindowSubstract = New System.Windows.Forms.Button
            Me.panelLeft = New System.Windows.Forms.Panel
            Me.numLeft = New System.Windows.Forms.NumericUpDown
            Me.lblPxLeft = New System.Windows.Forms.Label
            Me.panelTop = New System.Windows.Forms.Panel
            Me.numTop = New System.Windows.Forms.NumericUpDown
            Me.lblPxTop = New System.Windows.Forms.Label
            Me.panelBottom = New System.Windows.Forms.Panel
            Me.numBottom = New System.Windows.Forms.NumericUpDown
            Me.lblPxBottom = New System.Windows.Forms.Label
            Me.panelRight = New System.Windows.Forms.Panel
            Me.numRight = New System.Windows.Forms.NumericUpDown
            Me.lblPxRight = New System.Windows.Forms.Label
            Me.boxCorrection.SuspendLayout()
            Me.panelAdjust.SuspendLayout()
            Me.panelLeft.SuspendLayout()
            CType(Me.numLeft, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panelTop.SuspendLayout()
            CType(Me.numTop, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panelBottom.SuspendLayout()
            CType(Me.numBottom, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panelRight.SuspendLayout()
            CType(Me.numRight, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnWindow
            '
            Me.btnWindow.BackColor = System.Drawing.Color.LightGreen
            resources.ApplyResources(Me.btnWindow, "btnWindow")
            Me.btnWindow.Name = "btnWindow"
            Me.btnWindow.UseVisualStyleBackColor = False
            '
            'boxCorrection
            '
            Me.boxCorrection.Controls.Add(Me.Label1)
            Me.boxCorrection.Controls.Add(Me.panelAdjust)
            resources.ApplyResources(Me.boxCorrection, "boxCorrection")
            Me.boxCorrection.Name = "boxCorrection"
            Me.boxCorrection.TabStop = False
            '
            'Label1
            '
            resources.ApplyResources(Me.Label1, "Label1")
            Me.Label1.Name = "Label1"
            '
            'panelAdjust
            '
            Me.panelAdjust.Controls.Add(Me.btnWindowStandard)
            Me.panelAdjust.Controls.Add(Me.btnWindow)
            Me.panelAdjust.Controls.Add(Me.btnWindowSubstract)
            Me.panelAdjust.Controls.Add(Me.panelLeft)
            Me.panelAdjust.Controls.Add(Me.panelTop)
            Me.panelAdjust.Controls.Add(Me.panelBottom)
            Me.panelAdjust.Controls.Add(Me.panelRight)
            resources.ApplyResources(Me.panelAdjust, "panelAdjust")
            Me.panelAdjust.Name = "panelAdjust"
            '
            'btnWindowStandard
            '
            Me.btnWindowStandard.BackColor = System.Drawing.SystemColors.ActiveCaption
            resources.ApplyResources(Me.btnWindowStandard, "btnWindowStandard")
            Me.btnWindowStandard.Name = "btnWindowStandard"
            Me.btnWindowStandard.UseVisualStyleBackColor = False
            '
            'btnWindowSubstract
            '
            Me.btnWindowSubstract.BackColor = System.Drawing.Color.DarkRed
            resources.ApplyResources(Me.btnWindowSubstract, "btnWindowSubstract")
            Me.btnWindowSubstract.Name = "btnWindowSubstract"
            Me.btnWindowSubstract.UseVisualStyleBackColor = False
            '
            'panelLeft
            '
            Me.panelLeft.Controls.Add(Me.numLeft)
            Me.panelLeft.Controls.Add(Me.lblPxLeft)
            resources.ApplyResources(Me.panelLeft, "panelLeft")
            Me.panelLeft.Name = "panelLeft"
            '
            'numLeft
            '
            resources.ApplyResources(Me.numLeft, "numLeft")
            Me.numLeft.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.numLeft.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
            Me.numLeft.Name = "numLeft"
            '
            'lblPxLeft
            '
            resources.ApplyResources(Me.lblPxLeft, "lblPxLeft")
            Me.lblPxLeft.Name = "lblPxLeft"
            '
            'panelTop
            '
            Me.panelTop.Controls.Add(Me.numTop)
            Me.panelTop.Controls.Add(Me.lblPxTop)
            resources.ApplyResources(Me.panelTop, "panelTop")
            Me.panelTop.Name = "panelTop"
            '
            'numTop
            '
            resources.ApplyResources(Me.numTop, "numTop")
            Me.numTop.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.numTop.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
            Me.numTop.Name = "numTop"
            '
            'lblPxTop
            '
            resources.ApplyResources(Me.lblPxTop, "lblPxTop")
            Me.lblPxTop.Name = "lblPxTop"
            '
            'panelBottom
            '
            Me.panelBottom.Controls.Add(Me.numBottom)
            Me.panelBottom.Controls.Add(Me.lblPxBottom)
            resources.ApplyResources(Me.panelBottom, "panelBottom")
            Me.panelBottom.Name = "panelBottom"
            '
            'numBottom
            '
            resources.ApplyResources(Me.numBottom, "numBottom")
            Me.numBottom.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.numBottom.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
            Me.numBottom.Name = "numBottom"
            '
            'lblPxBottom
            '
            resources.ApplyResources(Me.lblPxBottom, "lblPxBottom")
            Me.lblPxBottom.Name = "lblPxBottom"
            '
            'panelRight
            '
            Me.panelRight.Controls.Add(Me.numRight)
            Me.panelRight.Controls.Add(Me.lblPxRight)
            resources.ApplyResources(Me.panelRight, "panelRight")
            Me.panelRight.Name = "panelRight"
            '
            'numRight
            '
            resources.ApplyResources(Me.numRight, "numRight")
            Me.numRight.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.numRight.Minimum = New Decimal(New Integer() {10, 0, 0, -2147483648})
            Me.numRight.Name = "numRight"
            '
            'lblPxRight
            '
            resources.ApplyResources(Me.lblPxRight, "lblPxRight")
            Me.lblPxRight.Name = "lblPxRight"
            '
            'CorrectionWindowSettingsSubPanel
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.Controls.Add(Me.boxCorrection)
            resources.ApplyResources(Me, "$this")
            Me.Name = "CorrectionWindowSettingsSubPanel"
            Me.boxCorrection.ResumeLayout(False)
            Me.panelAdjust.ResumeLayout(False)
            Me.panelLeft.ResumeLayout(False)
            Me.panelLeft.PerformLayout()
            CType(Me.numLeft, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panelTop.ResumeLayout(False)
            Me.panelTop.PerformLayout()
            CType(Me.numTop, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panelBottom.ResumeLayout(False)
            Me.panelBottom.PerformLayout()
            CType(Me.numBottom, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panelRight.ResumeLayout(False)
            Me.panelRight.PerformLayout()
            CType(Me.numRight, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnWindow As System.Windows.Forms.Button
        Friend WithEvents boxCorrection As System.Windows.Forms.GroupBox
        Friend WithEvents numLeft As System.Windows.Forms.NumericUpDown
        Friend WithEvents numRight As System.Windows.Forms.NumericUpDown
        Friend WithEvents numBottom As System.Windows.Forms.NumericUpDown
        Friend WithEvents numTop As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblPxLeft As System.Windows.Forms.Label
        Friend WithEvents lblPxBottom As System.Windows.Forms.Label
        Friend WithEvents lblPxRight As System.Windows.Forms.Label
        Friend WithEvents lblPxTop As System.Windows.Forms.Label
        Friend WithEvents panelAdjust As System.Windows.Forms.Panel
        Friend WithEvents panelLeft As System.Windows.Forms.Panel
        Friend WithEvents panelTop As System.Windows.Forms.Panel
        Friend WithEvents panelBottom As System.Windows.Forms.Panel
        Friend WithEvents panelRight As System.Windows.Forms.Panel
        Friend WithEvents btnWindowStandard As System.Windows.Forms.Button
        Friend WithEvents btnWindowSubstract As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label

    End Class
End Namespace
