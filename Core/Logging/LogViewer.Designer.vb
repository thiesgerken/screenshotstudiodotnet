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

Namespace Logging
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class LogViewer
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogViewer))
            Me.listLogEntries = New System.Windows.Forms.ListView()
            Me.colDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colMessage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.btnClose = New System.Windows.Forms.Button()
            Me.btnExport = New System.Windows.Forms.Button()
            Me.btnSupport = New System.Windows.Forms.Button()
            Me.btnDelete = New System.Windows.Forms.Button()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.SuspendLayout()
            '
            'listLogEntries
            '
            Me.listLogEntries.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colDate, Me.colType, Me.colMessage})
            resources.ApplyResources(Me.listLogEntries, "listLogEntries")
            Me.listLogEntries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
            Me.listLogEntries.Name = "listLogEntries"
            Me.listLogEntries.UseCompatibleStateImageBehavior = False
            Me.listLogEntries.View = System.Windows.Forms.View.Details
            '
            'colDate
            '
            resources.ApplyResources(Me.colDate, "colDate")
            '
            'colType
            '
            resources.ApplyResources(Me.colType, "colType")
            '
            'colMessage
            '
            resources.ApplyResources(Me.colMessage, "colMessage")
            '
            'btnClose
            '
            resources.ApplyResources(Me.btnClose, "btnClose")
            Me.btnClose.Name = "btnClose"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'btnExport
            '
            resources.ApplyResources(Me.btnExport, "btnExport")
            Me.btnExport.Name = "btnExport"
            Me.btnExport.UseVisualStyleBackColor = True
            '
            'btnSupport
            '
            resources.ApplyResources(Me.btnSupport, "btnSupport")
            Me.btnSupport.Name = "btnSupport"
            Me.btnSupport.UseVisualStyleBackColor = True
            '
            'btnDelete
            '
            resources.ApplyResources(Me.btnDelete, "btnDelete")
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.UseVisualStyleBackColor = True
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            Me.FormStateSaver.SaveSize = True
            '
            'LogViewer
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.btnDelete)
            Me.Controls.Add(Me.btnSupport)
            Me.Controls.Add(Me.btnExport)
            Me.Controls.Add(Me.btnClose)
            Me.Controls.Add(Me.listLogEntries)
            Me.MaximizeBox = False
            Me.Name = "LogViewer"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents listLogEntries As System.Windows.Forms.ListView
        Friend WithEvents colDate As System.Windows.Forms.ColumnHeader
        Friend WithEvents colType As System.Windows.Forms.ColumnHeader
        Friend WithEvents colMessage As System.Windows.Forms.ColumnHeader
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents FormStateSaver As Core.Controls.FormStateSaver
        Friend WithEvents btnSupport As System.Windows.Forms.Button
        Friend WithEvents btnExport As System.Windows.Forms.Button
        Friend WithEvents btnDelete As System.Windows.Forms.Button
    End Class
End Namespace
