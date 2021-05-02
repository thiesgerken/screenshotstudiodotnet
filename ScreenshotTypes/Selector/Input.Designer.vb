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

Namespace Selector
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Input
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Input))
            Me.numX = New System.Windows.Forms.NumericUpDown()
            Me.numY = New System.Windows.Forms.NumericUpDown()
            Me.numWidth = New System.Windows.Forms.NumericUpDown()
            Me.numHeight = New System.Windows.Forms.NumericUpDown()
            Me.optAbsolute = New System.Windows.Forms.RadioButton()
            Me.optRelative = New System.Windows.Forms.RadioButton()
            Me.boxLocation = New System.Windows.Forms.GroupBox()
            Me.lblY = New System.Windows.Forms.Label()
            Me.lblPx2 = New System.Windows.Forms.Label()
            Me.lblPx3 = New System.Windows.Forms.Label()
            Me.lblX = New System.Windows.Forms.Label()
            Me.boxSize = New System.Windows.Forms.GroupBox()
            Me.lblPx4 = New System.Windows.Forms.Label()
            Me.lblPx1 = New System.Windows.Forms.Label()
            Me.lblHeight = New System.Windows.Forms.Label()
            Me.lblWidth = New System.Windows.Forms.Label()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.pnlButtons = New System.Windows.Forms.Panel()
            Me.pnlRadios = New System.Windows.Forms.Panel()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.boxLocation.SuspendLayout()
            Me.boxSize.SuspendLayout()
            Me.pnlButtons.SuspendLayout()
            Me.pnlRadios.SuspendLayout()
            Me.SuspendLayout()
            '
            'numX
            '
            Me.numX.Increment = New Decimal(New Integer() {5, 0, 0, 0})
            resources.ApplyResources(Me.numX, "numX")
            Me.numX.Name = "numX"
            '
            'numY
            '
            Me.numY.Increment = New Decimal(New Integer() {5, 0, 0, 0})
            resources.ApplyResources(Me.numY, "numY")
            Me.numY.Name = "numY"
            '
            'numWidth
            '
            Me.numWidth.Increment = New Decimal(New Integer() {5, 0, 0, 0})
            resources.ApplyResources(Me.numWidth, "numWidth")
            Me.numWidth.Name = "numWidth"
            '
            'numHeight
            '
            Me.numHeight.Increment = New Decimal(New Integer() {5, 0, 0, 0})
            resources.ApplyResources(Me.numHeight, "numHeight")
            Me.numHeight.Name = "numHeight"
            '
            'optAbsolute
            '
            resources.ApplyResources(Me.optAbsolute, "optAbsolute")
            Me.optAbsolute.Name = "optAbsolute"
            Me.optAbsolute.TabStop = True
            Me.optAbsolute.UseVisualStyleBackColor = True
            '
            'optRelative
            '
            resources.ApplyResources(Me.optRelative, "optRelative")
            Me.optRelative.Name = "optRelative"
            Me.optRelative.TabStop = True
            Me.optRelative.UseVisualStyleBackColor = True
            '
            'boxLocation
            '
            Me.boxLocation.Controls.Add(Me.lblY)
            Me.boxLocation.Controls.Add(Me.lblPx2)
            Me.boxLocation.Controls.Add(Me.lblPx3)
            Me.boxLocation.Controls.Add(Me.lblX)
            Me.boxLocation.Controls.Add(Me.numX)
            Me.boxLocation.Controls.Add(Me.numY)
            resources.ApplyResources(Me.boxLocation, "boxLocation")
            Me.boxLocation.Name = "boxLocation"
            Me.boxLocation.TabStop = False
            '
            'lblY
            '
            resources.ApplyResources(Me.lblY, "lblY")
            Me.lblY.Name = "lblY"
            '
            'lblPx2
            '
            resources.ApplyResources(Me.lblPx2, "lblPx2")
            Me.lblPx2.Name = "lblPx2"
            '
            'lblPx3
            '
            resources.ApplyResources(Me.lblPx3, "lblPx3")
            Me.lblPx3.Name = "lblPx3"
            '
            'lblX
            '
            resources.ApplyResources(Me.lblX, "lblX")
            Me.lblX.Name = "lblX"
            '
            'boxSize
            '
            Me.boxSize.Controls.Add(Me.lblPx4)
            Me.boxSize.Controls.Add(Me.lblPx1)
            Me.boxSize.Controls.Add(Me.lblHeight)
            Me.boxSize.Controls.Add(Me.lblWidth)
            Me.boxSize.Controls.Add(Me.numWidth)
            Me.boxSize.Controls.Add(Me.numHeight)
            resources.ApplyResources(Me.boxSize, "boxSize")
            Me.boxSize.Name = "boxSize"
            Me.boxSize.TabStop = False
            '
            'lblPx4
            '
            resources.ApplyResources(Me.lblPx4, "lblPx4")
            Me.lblPx4.Name = "lblPx4"
            '
            'lblPx1
            '
            resources.ApplyResources(Me.lblPx1, "lblPx1")
            Me.lblPx1.Name = "lblPx1"
            '
            'lblHeight
            '
            resources.ApplyResources(Me.lblHeight, "lblHeight")
            Me.lblHeight.Name = "lblHeight"
            '
            'lblWidth
            '
            resources.ApplyResources(Me.lblWidth, "lblWidth")
            Me.lblWidth.Name = "lblWidth"
            '
            'btnOK
            '
            resources.ApplyResources(Me.btnOK, "btnOK")
            Me.btnOK.Name = "btnOK"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            resources.ApplyResources(Me.btnCancel, "btnCancel")
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'pnlButtons
            '
            Me.pnlButtons.Controls.Add(Me.btnCancel)
            Me.pnlButtons.Controls.Add(Me.btnOK)
            resources.ApplyResources(Me.pnlButtons, "pnlButtons")
            Me.pnlButtons.Name = "pnlButtons"
            '
            'pnlRadios
            '
            Me.pnlRadios.Controls.Add(Me.optAbsolute)
            Me.pnlRadios.Controls.Add(Me.optRelative)
            resources.ApplyResources(Me.pnlRadios, "pnlRadios")
            Me.pnlRadios.Name = "pnlRadios"
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            Me.FormStateSaver.SaveLocation = True
            '
            'Input
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.pnlRadios)
            Me.Controls.Add(Me.pnlButtons)
            Me.Controls.Add(Me.boxSize)
            Me.Controls.Add(Me.boxLocation)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "Input"
            Me.ShowInTaskbar = False
            Me.TopMost = True
            Me.boxLocation.ResumeLayout(False)
            Me.boxLocation.PerformLayout()
            Me.boxSize.ResumeLayout(False)
            Me.boxSize.PerformLayout()
            Me.pnlButtons.ResumeLayout(False)
            Me.pnlRadios.ResumeLayout(False)
            Me.pnlRadios.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents numX As System.Windows.Forms.NumericUpDown
        Friend WithEvents numY As System.Windows.Forms.NumericUpDown
        Friend WithEvents numWidth As System.Windows.Forms.NumericUpDown
        Friend WithEvents numHeight As System.Windows.Forms.NumericUpDown
        Friend WithEvents optAbsolute As System.Windows.Forms.RadioButton
        Friend WithEvents optRelative As System.Windows.Forms.RadioButton
        Friend WithEvents boxLocation As System.Windows.Forms.GroupBox
        Friend WithEvents lblX As System.Windows.Forms.Label
        Friend WithEvents boxSize As System.Windows.Forms.GroupBox
        Friend WithEvents lblY As System.Windows.Forms.Label
        Friend WithEvents lblHeight As System.Windows.Forms.Label
        Friend WithEvents lblWidth As System.Windows.Forms.Label
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents lblPx2 As System.Windows.Forms.Label
        Friend WithEvents lblPx3 As System.Windows.Forms.Label
        Friend WithEvents lblPx4 As System.Windows.Forms.Label
        Friend WithEvents lblPx1 As System.Windows.Forms.Label
        Friend WithEvents pnlButtons As System.Windows.Forms.Panel
        Friend WithEvents pnlRadios As System.Windows.Forms.Panel
        Friend WithEvents FormStateSaver As Global.ScreenshotStudioDotNet.Core.Controls.FormStateSaver
    End Class
End Namespace
