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

Imports ScreenshotStudioDotNet.Core.Misc
Imports ScreenshotStudioDotNet.Core.Controls

Namespace Settings
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class QuickStartSettingsPanel
        Inherits SettingsPanel

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QuickStartSettingsPanel))
            Me.boxColors = New System.Windows.Forms.GroupBox()
            Me.lblNoColorSelected = New System.Windows.Forms.Label()
            Me.panelColorSelect = New System.Windows.Forms.Panel()
            Me.btnSetAsActive = New System.Windows.Forms.Button()
            Me.lblNormal = New System.Windows.Forms.Label()
            Me.lblMouseDown = New System.Windows.Forms.Label()
            Me.lblHovered = New System.Windows.Forms.Label()
            Me.btnDeleteCurrent = New System.Windows.Forms.Button()
            Me.cbtnNormal = New ScreenshotStudioDotNet.Core.Controls.ColorSelectionButton()
            Me.cbtnHover = New ScreenshotStudioDotNet.Core.Controls.ColorSelectionButton()
            Me.cbtnMouseDown = New ScreenshotStudioDotNet.Core.Controls.ColorSelectionButton()
            Me.listColorizations = New System.Windows.Forms.ListView()
            Me.Column = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.btnReset = New System.Windows.Forms.Button()
            Me.btnAddToDB = New System.Windows.Forms.Button()
            Me.boxScale = New System.Windows.Forms.GroupBox()
            Me.numScale = New System.Windows.Forms.NumericUpDown()
            Me.lblScaleInfo = New System.Windows.Forms.Label()
            Me.lblScale = New System.Windows.Forms.Label()
            Me.boxColors.SuspendLayout()
            Me.panelColorSelect.SuspendLayout()
            Me.boxScale.SuspendLayout()
            Me.SuspendLayout()
            '
            'boxColors
            '
            Me.boxColors.Controls.Add(Me.lblNoColorSelected)
            Me.boxColors.Controls.Add(Me.panelColorSelect)
            Me.boxColors.Controls.Add(Me.listColorizations)
            Me.boxColors.Controls.Add(Me.btnReset)
            Me.boxColors.Controls.Add(Me.btnAddToDB)
            resources.ApplyResources(Me.boxColors, "boxColors")
            Me.boxColors.Name = "boxColors"
            Me.boxColors.TabStop = False
            '
            'lblNoColorSelected
            '
            resources.ApplyResources(Me.lblNoColorSelected, "lblNoColorSelected")
            Me.lblNoColorSelected.Name = "lblNoColorSelected"
            '
            'panelColorSelect
            '
            Me.panelColorSelect.Controls.Add(Me.btnSetAsActive)
            Me.panelColorSelect.Controls.Add(Me.lblNormal)
            Me.panelColorSelect.Controls.Add(Me.lblMouseDown)
            Me.panelColorSelect.Controls.Add(Me.lblHovered)
            Me.panelColorSelect.Controls.Add(Me.btnDeleteCurrent)
            Me.panelColorSelect.Controls.Add(Me.cbtnNormal)
            Me.panelColorSelect.Controls.Add(Me.cbtnHover)
            Me.panelColorSelect.Controls.Add(Me.cbtnMouseDown)
            resources.ApplyResources(Me.panelColorSelect, "panelColorSelect")
            Me.panelColorSelect.Name = "panelColorSelect"
            '
            'btnSetAsActive
            '
            resources.ApplyResources(Me.btnSetAsActive, "btnSetAsActive")
            Me.btnSetAsActive.Name = "btnSetAsActive"
            Me.btnSetAsActive.UseVisualStyleBackColor = True
            '
            'lblNormal
            '
            resources.ApplyResources(Me.lblNormal, "lblNormal")
            Me.lblNormal.Name = "lblNormal"
            '
            'lblMouseDown
            '
            resources.ApplyResources(Me.lblMouseDown, "lblMouseDown")
            Me.lblMouseDown.Name = "lblMouseDown"
            '
            'lblHovered
            '
            resources.ApplyResources(Me.lblHovered, "lblHovered")
            Me.lblHovered.Name = "lblHovered"
            '
            'btnDeleteCurrent
            '
            resources.ApplyResources(Me.btnDeleteCurrent, "btnDeleteCurrent")
            Me.btnDeleteCurrent.Name = "btnDeleteCurrent"
            Me.btnDeleteCurrent.UseVisualStyleBackColor = True
            '
            'cbtnNormal
            '
            resources.ApplyResources(Me.cbtnNormal, "cbtnNormal")
            Me.cbtnNormal.Color = System.Drawing.Color.Black
            Me.cbtnNormal.FontSize = 15.0!
            Me.cbtnNormal.Name = "cbtnNormal"
            Me.cbtnNormal.UseVisualStyleBackColor = True
            '
            'cbtnHover
            '
            resources.ApplyResources(Me.cbtnHover, "cbtnHover")
            Me.cbtnHover.Color = System.Drawing.Color.Black
            Me.cbtnHover.FontSize = 15.0!
            Me.cbtnHover.Name = "cbtnHover"
            Me.cbtnHover.UseVisualStyleBackColor = True
            '
            'cbtnMouseDown
            '
            resources.ApplyResources(Me.cbtnMouseDown, "cbtnMouseDown")
            Me.cbtnMouseDown.Color = System.Drawing.Color.Black
            Me.cbtnMouseDown.FontSize = 15.0!
            Me.cbtnMouseDown.Name = "cbtnMouseDown"
            Me.cbtnMouseDown.UseVisualStyleBackColor = True
            '
            'listColorizations
            '
            Me.listColorizations.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Column})
            Me.listColorizations.FullRowSelect = True
            Me.listColorizations.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.listColorizations.Items.AddRange(New System.Windows.Forms.ListViewItem() {CType(resources.GetObject("listColorizations.Items"), System.Windows.Forms.ListViewItem)})
            resources.ApplyResources(Me.listColorizations, "listColorizations")
            Me.listColorizations.MultiSelect = False
            Me.listColorizations.Name = "listColorizations"
            Me.listColorizations.ShowGroups = False
            Me.listColorizations.UseCompatibleStateImageBehavior = False
            Me.listColorizations.View = System.Windows.Forms.View.Details
            '
            'Column
            '
            resources.ApplyResources(Me.Column, "Column")
            '
            'btnReset
            '
            resources.ApplyResources(Me.btnReset, "btnReset")
            Me.btnReset.Name = "btnReset"
            Me.btnReset.UseVisualStyleBackColor = True
            '
            'btnAddToDB
            '
            resources.ApplyResources(Me.btnAddToDB, "btnAddToDB")
            Me.btnAddToDB.Name = "btnAddToDB"
            Me.btnAddToDB.UseVisualStyleBackColor = True
            '
            'boxScale
            '
            Me.boxScale.Controls.Add(Me.numScale)
            Me.boxScale.Controls.Add(Me.lblScaleInfo)
            Me.boxScale.Controls.Add(Me.lblScale)
            resources.ApplyResources(Me.boxScale, "boxScale")
            Me.boxScale.Name = "boxScale"
            Me.boxScale.TabStop = False
            '
            'numScale
            '
            Me.numScale.DecimalPlaces = 1
            Me.numScale.Increment = New Decimal(New Integer() {2, 0, 0, 65536})
            resources.ApplyResources(Me.numScale, "numScale")
            Me.numScale.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.numScale.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
            Me.numScale.Name = "numScale"
            Me.numScale.Value = New Decimal(New Integer() {1, 0, 0, 65536})
            '
            'lblScaleInfo
            '
            resources.ApplyResources(Me.lblScaleInfo, "lblScaleInfo")
            Me.lblScaleInfo.Name = "lblScaleInfo"
            '
            'lblScale
            '
            resources.ApplyResources(Me.lblScale, "lblScale")
            Me.lblScale.Name = "lblScale"
            '
            'QuickStartSettingsPanel
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.Controls.Add(Me.boxScale)
            Me.Controls.Add(Me.boxColors)
            Me.Name = "QuickStartSettingsPanel"
            Me.boxColors.ResumeLayout(False)
            Me.panelColorSelect.ResumeLayout(False)
            Me.panelColorSelect.PerformLayout()
            Me.boxScale.ResumeLayout(False)
            Me.boxScale.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents boxColors As System.Windows.Forms.GroupBox
        Friend WithEvents lblHovered As System.Windows.Forms.Label
        Friend WithEvents lblMouseDown As System.Windows.Forms.Label
        Friend WithEvents lblNormal As System.Windows.Forms.Label
        Friend WithEvents cbtnMouseDown As ColorSelectionButton
        Friend WithEvents cbtnHover As ColorSelectionButton
        Friend WithEvents cbtnNormal As ColorSelectionButton
        Friend WithEvents boxScale As System.Windows.Forms.GroupBox
        Friend WithEvents lblScaleInfo As System.Windows.Forms.Label
        Friend WithEvents lblScale As System.Windows.Forms.Label
        Friend WithEvents numScale As System.Windows.Forms.NumericUpDown
        Friend WithEvents btnDeleteCurrent As System.Windows.Forms.Button
        Friend WithEvents btnAddToDB As System.Windows.Forms.Button
        Friend WithEvents btnReset As System.Windows.Forms.Button
        Friend WithEvents listColorizations As System.Windows.Forms.ListView
        Friend WithEvents Column As System.Windows.Forms.ColumnHeader
        Friend WithEvents panelColorSelect As System.Windows.Forms.Panel
        Friend WithEvents lblNoColorSelected As System.Windows.Forms.Label
        Friend WithEvents btnSetAsActive As System.Windows.Forms.Button

    End Class
End Namespace
