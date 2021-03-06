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
    Partial Class QuickStart
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QuickStart))
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.SuspendLayout()
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            '
            'QuickStart
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "QuickStart"
            Me.TopMost = True
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents FormStateSaver As Global.ScreenshotStudioDotNet.Core.Controls.FormStateSaver
    End Class
End Namespace
