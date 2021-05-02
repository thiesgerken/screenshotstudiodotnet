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

Imports ScreenshotStudioDotNet.Core.Aero

Namespace Macros
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MacroGenerator
        Inherits AeroForm
    
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MacroGenerator))
            Me.panelBtns = New System.Windows.Forms.Panel()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.lblBadThings = New System.Windows.Forms.Label()
            Me.btnCreate = New System.Windows.Forms.Button()
            Me.txtName = New ScreenshotStudioDotNet.Core.Controls.WatermarkTextBox()
            Me.timCreationEnabledChecker = New System.Windows.Forms.Timer()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.panelBtns.SuspendLayout()
            Me.SuspendLayout()
            '
            'panelBtns
            '
            Me.panelBtns.Controls.Add(Me.btnCancel)
            Me.panelBtns.Controls.Add(Me.lblBadThings)
            Me.panelBtns.Controls.Add(Me.btnCreate)
            Me.panelBtns.Controls.Add(Me.txtName)
            Me.panelBtns.Location = New System.Drawing.Point(153, 355)
            Me.panelBtns.Name = "panelBtns"
            Me.panelBtns.Size = New System.Drawing.Size(811, 57)
            Me.panelBtns.TabIndex = 22
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(553, 12)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(123, 32)
            Me.btnCancel.TabIndex = 0
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'lblBadThings
            '
            Me.lblBadThings.Location = New System.Drawing.Point(206, 1)
            Me.lblBadThings.Name = "lblBadThings"
            Me.lblBadThings.Size = New System.Drawing.Size(341, 54)
            Me.lblBadThings.TabIndex = 2
            Me.lblBadThings.Text = "Bad Things go here"
            Me.lblBadThings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnCreate
            '
            Me.btnCreate.Enabled = False
            Me.btnCreate.Location = New System.Drawing.Point(682, 12)
            Me.btnCreate.Name = "btnCreate"
            Me.btnCreate.Size = New System.Drawing.Size(123, 32)
            Me.btnCreate.TabIndex = 1
            Me.btnCreate.Text = "Create Macro"
            Me.btnCreate.UseVisualStyleBackColor = True
            '
            'txtName
            '
            Me.txtName.Location = New System.Drawing.Point(15, 16)
            Me.txtName.Name = "txtName"
            Me.txtName.Size = New System.Drawing.Size(185, 25)
            Me.txtName.TabIndex = 2
            Me.txtName.WatermarkText = "Name"
            '
            'timCreationEnabledChecker
            '
            Me.timCreationEnabledChecker.Enabled = True
            Me.timCreationEnabledChecker.Interval = 500
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            '
            'MacroGenerator
            '
            Me.AllowDrop = True
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.ClientSize = New System.Drawing.Size(976, 424)
            Me.Controls.Add(Me.panelBtns)
            Me.DoubleBuffered = True
            Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(0, 0)
            Me.MaximizeBox = False
            Me.Name = "MacroGenerator"
            Me.Text = "Macro Designer - ScreenshotStudio.Net"
            Me.panelBtns.ResumeLayout(False)
            Me.panelBtns.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents panelBtns As System.Windows.Forms.Panel
        Friend WithEvents btnCreate As System.Windows.Forms.Button
        Friend WithEvents txtName As ScreenshotStudioDotNet.Core.Controls.WatermarkTextBox
        Friend WithEvents timCreationEnabledChecker As System.Windows.Forms.Timer
        Friend WithEvents lblBadThings As System.Windows.Forms.Label
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents FormStateSaver As ScreenshotStudioDotNet.Core.Controls.FormStateSaver
    End Class
End Namespace
