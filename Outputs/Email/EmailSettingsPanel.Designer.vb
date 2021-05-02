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

Namespace Email
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class EmailSettingsPanel
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EmailSettingsPanel))
            Me.txtAddress = New System.Windows.Forms.TextBox
            Me.lblAddress = New System.Windows.Forms.Label
            Me.lblSubject = New System.Windows.Forms.Label
            Me.txtSubject = New System.Windows.Forms.TextBox
            Me.lblBody = New System.Windows.Forms.Label
            Me.txtBody = New System.Windows.Forms.TextBox
            Me.groupEmail = New System.Windows.Forms.GroupBox
            Me.groupEmail.SuspendLayout()
            Me.SuspendLayout()
            '
            'txtAddress
            '
            resources.ApplyResources(Me.txtAddress, "txtAddress")
            Me.txtAddress.Name = "txtAddress"
            '
            'lblAddress
            '
            resources.ApplyResources(Me.lblAddress, "lblAddress")
            Me.lblAddress.Name = "lblAddress"
            '
            'lblSubject
            '
            resources.ApplyResources(Me.lblSubject, "lblSubject")
            Me.lblSubject.Name = "lblSubject"
            '
            'txtSubject
            '
            resources.ApplyResources(Me.txtSubject, "txtSubject")
            Me.txtSubject.Name = "txtSubject"
            '
            'lblBody
            '
            resources.ApplyResources(Me.lblBody, "lblBody")
            Me.lblBody.Name = "lblBody"
            '
            'txtBody
            '
            resources.ApplyResources(Me.txtBody, "txtBody")
            Me.txtBody.Name = "txtBody"
            '
            'groupEmail
            '
            Me.groupEmail.Controls.Add(Me.txtBody)
            Me.groupEmail.Controls.Add(Me.lblBody)
            Me.groupEmail.Controls.Add(Me.txtAddress)
            Me.groupEmail.Controls.Add(Me.lblAddress)
            Me.groupEmail.Controls.Add(Me.lblSubject)
            Me.groupEmail.Controls.Add(Me.txtSubject)
            resources.ApplyResources(Me.groupEmail, "groupEmail")
            Me.groupEmail.Name = "groupEmail"
            Me.groupEmail.TabStop = False
            '
            'EmailSettingsPanel
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.Controls.Add(Me.groupEmail)
            resources.ApplyResources(Me, "$this")
            Me.Name = "EmailSettingsPanel"
            Me.groupEmail.ResumeLayout(False)
            Me.groupEmail.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents txtAddress As System.Windows.Forms.TextBox
        Friend WithEvents lblAddress As System.Windows.Forms.Label
        Friend WithEvents lblSubject As System.Windows.Forms.Label
        Friend WithEvents txtSubject As System.Windows.Forms.TextBox
        Friend WithEvents lblBody As System.Windows.Forms.Label
        Friend WithEvents txtBody As System.Windows.Forms.TextBox
        Friend WithEvents groupEmail As System.Windows.Forms.GroupBox

    End Class
End Namespace
