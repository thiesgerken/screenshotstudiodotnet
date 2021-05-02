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

Namespace Controls

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class InputDialog
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
            Me.panelContent = New System.Windows.Forms.Panel()
            Me.txtInput = New ScreenshotStudioDotNet.Core.Controls.WatermarkTextBox()
            Me.btnCancel = New Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink()
            Me.lblLongInstruction = New System.Windows.Forms.Label()
            Me.btnOK = New Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.panelContent.SuspendLayout()
            Me.SuspendLayout()
            '
            'panelContent
            '
            Me.panelContent.Controls.Add(Me.txtInput)
            Me.panelContent.Controls.Add(Me.btnCancel)
            Me.panelContent.Controls.Add(Me.lblLongInstruction)
            Me.panelContent.Controls.Add(Me.btnOK)
            Me.panelContent.Location = New System.Drawing.Point(12, 12)
            Me.panelContent.Name = "panelContent"
            Me.panelContent.Size = New System.Drawing.Size(358, 205)
            Me.panelContent.TabIndex = 0
            '
            'txtInput
            '
            Me.txtInput.Location = New System.Drawing.Point(17, 41)
            Me.txtInput.Name = "txtInput"
            Me.txtInput.Size = New System.Drawing.Size(322, 25)
            Me.txtInput.TabIndex = 0
            Me.txtInput.WatermarkText = Nothing
            '
            'btnCancel
            '
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnCancel.Location = New System.Drawing.Point(17, 88)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.NoteText = ""
            Me.btnCancel.Size = New System.Drawing.Size(322, 54)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'lblLongInstruction
            '
            Me.lblLongInstruction.Location = New System.Drawing.Point(14, 6)
            Me.lblLongInstruction.Name = "lblLongInstruction"
            Me.lblLongInstruction.Size = New System.Drawing.Size(325, 32)
            Me.lblLongInstruction.TabIndex = 3
            Me.lblLongInstruction.Text = "blablabla"
            Me.lblLongInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnOK
            '
            Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnOK.Location = New System.Drawing.Point(17, 143)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.NoteText = ""
            Me.btnOK.Size = New System.Drawing.Size(322, 54)
            Me.btnOK.TabIndex = 0
            Me.btnOK.Text = "Save"
            Me.btnOK.UseVisualStyleBackColor = True
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            '
            'InputDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.Control
            Me.ClientSize = New System.Drawing.Size(375, 218)
            Me.Controls.Add(Me.panelContent)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Location = New System.Drawing.Point(0, 0)
            Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "InputDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "ScreenshotStudio.Net 6"
            Me.panelContent.ResumeLayout(False)
            Me.panelContent.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents panelContent As System.Windows.Forms.Panel
        Friend WithEvents btnOK As Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink
        Friend WithEvents lblLongInstruction As System.Windows.Forms.Label
        Friend WithEvents btnCancel As Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink
        Friend WithEvents txtInput As ScreenshotStudioDotNet.Core.Controls.WatermarkTextBox
        Friend WithEvents FormStateSaver As ScreenshotStudioDotNet.Core.Controls.FormStateSaver

    End Class
End Namespace
