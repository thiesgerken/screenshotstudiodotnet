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
Imports ScreenshotStudioDotNet.Core.Misc

Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AboutForm
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.lblInfo = New System.Windows.Forms.Label()
            Me.lblInfo2 = New System.Windows.Forms.Label()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.lblText = New System.Windows.Forms.Label()
            Me.btnCheckUpdates = New System.Windows.Forms.Button()
            Me.btnSSSWeb = New System.Windows.Forms.Button()
            Me.btnHomepage = New System.Windows.Forms.Button()
            Me.Panel = New System.Windows.Forms.Panel()
            Me.btnShowLog = New System.Windows.Forms.Button()
            Me.Panel.SuspendLayout()
            Me.SuspendLayout()
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            '
            'lblInfo
            '
            resources.ApplyResources(Me.lblInfo, "lblInfo")
            Me.lblInfo.Name = "lblInfo"
            '
            'lblInfo2
            '
            resources.ApplyResources(Me.lblInfo2, "lblInfo2")
            Me.lblInfo2.Name = "lblInfo2"
            '
            'btnClose
            '
            resources.ApplyResources(Me.btnClose, "btnClose")
            Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnClose.Name = "btnClose"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'lblText
            '
            resources.ApplyResources(Me.lblText, "lblText")
            Me.lblText.Name = "lblText"
            '
            'btnCheckUpdates
            '
            resources.ApplyResources(Me.btnCheckUpdates, "btnCheckUpdates")
            Me.btnCheckUpdates.Name = "btnCheckUpdates"
            Me.btnCheckUpdates.UseVisualStyleBackColor = True
            '
            'btnSSSWeb
            '
            resources.ApplyResources(Me.btnSSSWeb, "btnSSSWeb")
            Me.btnSSSWeb.Name = "btnSSSWeb"
            Me.btnSSSWeb.UseVisualStyleBackColor = True
            '
            'btnHomepage
            '
            resources.ApplyResources(Me.btnHomepage, "btnHomepage")
            Me.btnHomepage.Name = "btnHomepage"
            Me.btnHomepage.UseVisualStyleBackColor = True
            '
            'Panel
            '
            resources.ApplyResources(Me.Panel, "Panel")
            Me.Panel.Controls.Add(Me.btnShowLog)
            Me.Panel.Controls.Add(Me.btnHomepage)
            Me.Panel.Controls.Add(Me.lblInfo)
            Me.Panel.Controls.Add(Me.btnSSSWeb)
            Me.Panel.Controls.Add(Me.lblInfo2)
            Me.Panel.Controls.Add(Me.btnCheckUpdates)
            Me.Panel.Controls.Add(Me.btnClose)
            Me.Panel.Controls.Add(Me.lblText)
            Me.Panel.Name = "Panel"
            '
            'btnShowLog
            '
            resources.ApplyResources(Me.btnShowLog, "btnShowLog")
            Me.btnShowLog.Name = "btnShowLog"
            Me.btnShowLog.UseVisualStyleBackColor = True
            '
            'AboutForm
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnClose
            Me.Controls.Add(Me.Panel)
            Me.DragWindowOnGlass = True
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AboutForm"
            Me.ShowInTaskbar = True
            Me.Panel.ResumeLayout(False)
            Me.Panel.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FormStateSaver As Core.Controls.FormStateSaver
        Friend WithEvents lblInfo As System.Windows.Forms.Label
        Friend WithEvents lblInfo2 As System.Windows.Forms.Label
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents lblText As System.Windows.Forms.Label
        Friend WithEvents btnCheckUpdates As System.Windows.Forms.Button
        Friend WithEvents Panel As System.Windows.Forms.Panel
        Friend WithEvents btnHomepage As System.Windows.Forms.Button
        Friend WithEvents btnSSSWeb As System.Windows.Forms.Button
        Friend WithEvents btnShowLog As System.Windows.Forms.Button

    End Class
End Namespace
