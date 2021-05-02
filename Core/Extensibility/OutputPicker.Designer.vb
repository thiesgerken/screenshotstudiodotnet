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

Namespace Extensibility
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class OutputPicker
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
            Me.btnCancel = New Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.SuspendLayout()
            '
            'btnCancel
            '
            Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.btnCancel.Location = New System.Drawing.Point(12, 256)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.NoteText = "Cancels the Output"
            Me.btnCancel.Size = New System.Drawing.Size(180, 60)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            Me.FormStateSaver.SaveLocation = True
            '
            'OutputPicker
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(519, 328)
            Me.Controls.Add(Me.btnCancel)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.Name = "OutputPicker"
            Me.Opacity = 0.92R
            Me.Text = "OutputPicker"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FormStateSaver As Core.Controls.FormStateSaver
        Friend WithEvents btnCancel As Microsoft.WindowsAPICodePack.Controls.WindowsForms.CommandLink
    End Class
End Namespace
