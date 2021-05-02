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

Namespace Imageshack
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ImageshackArgumentDesigner
        Inherits PluginArgumentDesigner

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
            Me.chkShort = New System.Windows.Forms.CheckBox()
            Me.boxShort = New System.Windows.Forms.GroupBox()
            Me.lblShortInfo = New System.Windows.Forms.Label()
            Me.chkHide = New System.Windows.Forms.CheckBox()
            Me.cboxFileFormat = New System.Windows.Forms.ComboBox()
            Me.optFormatAsGlobal = New System.Windows.Forms.RadioButton()
            Me.optCustom = New System.Windows.Forms.RadioButton()
            Me.cboxLinkAction = New System.Windows.Forms.ComboBox()
            Me.lblAction = New System.Windows.Forms.Label()
            Me.boxFormat = New System.Windows.Forms.GroupBox()
            Me.boxLink = New System.Windows.Forms.GroupBox()
            Me.btnSelectFile = New System.Windows.Forms.Button()
            Me.chkAppend = New System.Windows.Forms.CheckBox()
            Me.txtFilename = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.boxMisc = New System.Windows.Forms.GroupBox()
            Me.FilenamePicker = New System.Windows.Forms.SaveFileDialog()
            Me.boxShort.SuspendLayout()
            Me.boxFormat.SuspendLayout()
            Me.boxLink.SuspendLayout()
            Me.boxMisc.SuspendLayout()
            Me.SuspendLayout()
            '
            'chkShort
            '
            Me.chkShort.AutoSize = True
            Me.chkShort.Location = New System.Drawing.Point(19, 26)
            Me.chkShort.Name = "chkShort"
            Me.chkShort.Size = New System.Drawing.Size(114, 17)
            Me.chkShort.TabIndex = 2
            Me.chkShort.Text = "Generate Short-Url"
            Me.chkShort.UseVisualStyleBackColor = True
            '
            'boxShort
            '
            Me.boxShort.Controls.Add(Me.lblShortInfo)
            Me.boxShort.Controls.Add(Me.chkShort)
            Me.boxShort.Location = New System.Drawing.Point(12, 12)
            Me.boxShort.Name = "boxShort"
            Me.boxShort.Size = New System.Drawing.Size(431, 100)
            Me.boxShort.TabIndex = 3
            Me.boxShort.TabStop = False
            Me.boxShort.Text = "Bit.ly"
            '
            'lblShortInfo
            '
            Me.lblShortInfo.Location = New System.Drawing.Point(16, 50)
            Me.lblShortInfo.Name = "lblShortInfo"
            Me.lblShortInfo.Size = New System.Drawing.Size(399, 42)
            Me.lblShortInfo.TabIndex = 3
            Me.lblShortInfo.Text = "Use Bit.ly to generate a Url that is shorter than the imageshack-url. (example: h" & _
                "ttp://bit.ly/QtQET)"
            '
            'chkHide
            '
            Me.chkHide.AutoSize = True
            Me.chkHide.Location = New System.Drawing.Point(18, 24)
            Me.chkHide.Name = "chkHide"
            Me.chkHide.Size = New System.Drawing.Size(118, 17)
            Me.chkHide.TabIndex = 4
            Me.chkHide.Text = "Hide Upload-Dialog"
            Me.chkHide.UseVisualStyleBackColor = True
            '
            'cboxFileFormat
            '
            Me.cboxFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboxFileFormat.FormattingEnabled = True
            Me.cboxFileFormat.Items.AddRange(New Object() {"Portable Network Graphics (.png)", "JPEG (.jpg)", "Bitmap (.bmp)", "Graphics Interchange Format (.gif)"})
            Me.cboxFileFormat.Location = New System.Drawing.Point(191, 49)
            Me.cboxFileFormat.Name = "cboxFileFormat"
            Me.cboxFileFormat.Size = New System.Drawing.Size(224, 25)
            Me.cboxFileFormat.TabIndex = 5
            '
            'optFormatAsGlobal
            '
            Me.optFormatAsGlobal.AutoSize = True
            Me.optFormatAsGlobal.Location = New System.Drawing.Point(18, 23)
            Me.optFormatAsGlobal.Name = "optFormatAsGlobal"
            Me.optFormatAsGlobal.Size = New System.Drawing.Size(113, 17)
            Me.optFormatAsGlobal.TabIndex = 6
            Me.optFormatAsGlobal.TabStop = True
            Me.optFormatAsGlobal.Text = "Use Global Setting"
            Me.optFormatAsGlobal.UseVisualStyleBackColor = True
            '
            'optCustom
            '
            Me.optCustom.AutoSize = True
            Me.optCustom.Location = New System.Drawing.Point(18, 50)
            Me.optCustom.Name = "optCustom"
            Me.optCustom.Size = New System.Drawing.Size(63, 17)
            Me.optCustom.TabIndex = 7
            Me.optCustom.TabStop = True
            Me.optCustom.Text = "Custom:"
            Me.optCustom.UseVisualStyleBackColor = True
            '
            'cboxLinkAction
            '
            Me.cboxLinkAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboxLinkAction.FormattingEnabled = True
            Me.cboxLinkAction.Items.AddRange(New Object() {"None", "Save", "Clipboard"})
            Me.cboxLinkAction.Location = New System.Drawing.Point(190, 20)
            Me.cboxLinkAction.Name = "cboxLinkAction"
            Me.cboxLinkAction.Size = New System.Drawing.Size(225, 25)
            Me.cboxLinkAction.TabIndex = 8
            '
            'lblAction
            '
            Me.lblAction.AutoSize = True
            Me.lblAction.Location = New System.Drawing.Point(16, 23)
            Me.lblAction.Name = "lblAction"
            Me.lblAction.Size = New System.Drawing.Size(116, 17)
            Me.lblAction.TabIndex = 9
            Me.lblAction.Text = "Action for the Link:"
            '
            'boxFormat
            '
            Me.boxFormat.Controls.Add(Me.cboxFileFormat)
            Me.boxFormat.Controls.Add(Me.optFormatAsGlobal)
            Me.boxFormat.Controls.Add(Me.optCustom)
            Me.boxFormat.Location = New System.Drawing.Point(12, 118)
            Me.boxFormat.Name = "boxFormat"
            Me.boxFormat.Size = New System.Drawing.Size(431, 88)
            Me.boxFormat.TabIndex = 10
            Me.boxFormat.TabStop = False
            Me.boxFormat.Text = "File-Format"
            '
            'boxLink
            '
            Me.boxLink.Controls.Add(Me.btnSelectFile)
            Me.boxLink.Controls.Add(Me.chkAppend)
            Me.boxLink.Controls.Add(Me.txtFilename)
            Me.boxLink.Controls.Add(Me.Label1)
            Me.boxLink.Controls.Add(Me.lblAction)
            Me.boxLink.Controls.Add(Me.cboxLinkAction)
            Me.boxLink.Location = New System.Drawing.Point(12, 212)
            Me.boxLink.Name = "boxLink"
            Me.boxLink.Size = New System.Drawing.Size(431, 103)
            Me.boxLink.TabIndex = 11
            Me.boxLink.TabStop = False
            Me.boxLink.Text = "Link Destination"
            '
            'btnSelectFile
            '
            Me.btnSelectFile.Location = New System.Drawing.Point(378, 50)
            Me.btnSelectFile.Name = "btnSelectFile"
            Me.btnSelectFile.Size = New System.Drawing.Size(37, 25)
            Me.btnSelectFile.TabIndex = 13
            Me.btnSelectFile.Text = "..."
            Me.btnSelectFile.UseVisualStyleBackColor = True
            '
            'chkAppend
            '
            Me.chkAppend.AutoSize = True
            Me.chkAppend.Location = New System.Drawing.Point(18, 76)
            Me.chkAppend.Name = "chkAppend"
            Me.chkAppend.Size = New System.Drawing.Size(141, 21)
            Me.chkAppend.TabIndex = 12
            Me.chkAppend.Text = "Append if file exists"
            Me.chkAppend.UseVisualStyleBackColor = True
            '
            'txtFilename
            '
            Me.txtFilename.Location = New System.Drawing.Point(190, 50)
            Me.txtFilename.Name = "txtFilename"
            Me.txtFilename.ReadOnly = True
            Me.txtFilename.Size = New System.Drawing.Size(182, 25)
            Me.txtFilename.TabIndex = 11
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(15, 51)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(62, 17)
            Me.Label1.TabIndex = 10
            Me.Label1.Text = "Filename:"
            '
            'boxMisc
            '
            Me.boxMisc.Controls.Add(Me.chkHide)
            Me.boxMisc.Location = New System.Drawing.Point(12, 321)
            Me.boxMisc.Name = "boxMisc"
            Me.boxMisc.Size = New System.Drawing.Size(431, 57)
            Me.boxMisc.TabIndex = 12
            Me.boxMisc.TabStop = False
            Me.boxMisc.Text = "Misc"
            '
            'FilenamePicker
            '
            Me.FilenamePicker.Filter = "Text Files|*.txt|All Files|*.*"
            Me.FilenamePicker.SupportMultiDottedExtensions = True
            Me.FilenamePicker.Title = "Save Link"
            '
            'ImageshackArgumentDesigner
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.ClientSize = New System.Drawing.Size(456, 425)
            Me.Controls.Add(Me.boxMisc)
            Me.Controls.Add(Me.boxLink)
            Me.Controls.Add(Me.boxFormat)
            Me.Controls.Add(Me.boxShort)
            Me.Name = "ImageshackArgumentDesigner"
            Me.Text = "Imageshack Settings"
            Me.Controls.SetChildIndex(Me.boxShort, 0)
            Me.Controls.SetChildIndex(Me.boxFormat, 0)
            Me.Controls.SetChildIndex(Me.boxLink, 0)
            Me.Controls.SetChildIndex(Me.boxMisc, 0)
            Me.boxShort.ResumeLayout(False)
            Me.boxShort.PerformLayout()
            Me.boxFormat.ResumeLayout(False)
            Me.boxFormat.PerformLayout()
            Me.boxLink.ResumeLayout(False)
            Me.boxLink.PerformLayout()
            Me.boxMisc.ResumeLayout(False)
            Me.boxMisc.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents chkShort As System.Windows.Forms.CheckBox
        Friend WithEvents boxShort As System.Windows.Forms.GroupBox
        Friend WithEvents lblShortInfo As System.Windows.Forms.Label
        Friend WithEvents chkHide As System.Windows.Forms.CheckBox
        Friend WithEvents cboxFileFormat As System.Windows.Forms.ComboBox
        Friend WithEvents optFormatAsGlobal As System.Windows.Forms.RadioButton
        Friend WithEvents optCustom As System.Windows.Forms.RadioButton
        Friend WithEvents cboxLinkAction As System.Windows.Forms.ComboBox
        Friend WithEvents lblAction As System.Windows.Forms.Label
        Friend WithEvents boxFormat As System.Windows.Forms.GroupBox
        Friend WithEvents boxLink As System.Windows.Forms.GroupBox
        Friend WithEvents boxMisc As System.Windows.Forms.GroupBox
        Friend WithEvents btnSelectFile As System.Windows.Forms.Button
        Friend WithEvents chkAppend As System.Windows.Forms.CheckBox
        Friend WithEvents txtFilename As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents FilenamePicker As System.Windows.Forms.SaveFileDialog
    End Class
End Namespace
