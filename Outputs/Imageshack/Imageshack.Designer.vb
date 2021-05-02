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

Namespace Imageshack
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
        Partial Class Imageshack
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Imageshack))
            Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
            Me.SpeedTimer = New System.Windows.Forms.Timer()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.pnlLabel = New System.Windows.Forms.TableLayoutPanel()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.pnlLabel.SuspendLayout()
            Me.SuspendLayout()
            '
            'ProgressBar1
            '
            resources.ApplyResources(Me.ProgressBar1, "ProgressBar1")
            Me.ProgressBar1.MarqueeAnimationSpeed = 55
            Me.ProgressBar1.Maximum = 10000
            Me.ProgressBar1.Name = "ProgressBar1"
            '
            'SpeedTimer
            '
            Me.SpeedTimer.Enabled = True
            Me.SpeedTimer.Interval = 1000
            '
            'lblStatus
            '
            resources.ApplyResources(Me.lblStatus, "lblStatus")
            Me.lblStatus.Name = "lblStatus"
            '
            'pnlLabel
            '
            resources.ApplyResources(Me.pnlLabel, "pnlLabel")
            Me.pnlLabel.Controls.Add(Me.lblStatus, 0, 0)
            Me.pnlLabel.Name = "pnlLabel"
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            '
            'Imageshack
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.pnlLabel)
            Me.Controls.Add(Me.ProgressBar1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "Imageshack"
            Me.pnlLabel.ResumeLayout(False)
            Me.pnlLabel.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
        Friend WithEvents SpeedTimer As System.Windows.Forms.Timer
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents pnlLabel As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents FormStateSaver As Global.ScreenshotStudioDotNet.Core.Controls.FormStateSaver
    End Class
End Namespace
