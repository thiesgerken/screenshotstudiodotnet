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

Namespace Macros
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MacroManager
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MacroManager))
            Me.btnNew = New System.Windows.Forms.Button()
            Me.listMacros = New System.Windows.Forms.ListView()
            Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colTriggers = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colEffects = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colOutputs = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colDelay = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colMultiple = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.btnDelete = New System.Windows.Forms.Button()
            Me.btnEdit = New System.Windows.Forms.Button()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.Line1 = New ScreenshotStudioDotNet.Core.Controls.Line()
            Me.SuspendLayout()
            '
            'btnNew
            '
            Me.btnNew.Location = New System.Drawing.Point(12, 338)
            Me.btnNew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.btnNew.Name = "btnNew"
            Me.btnNew.Size = New System.Drawing.Size(110, 30)
            Me.btnNew.TabIndex = 0
            Me.btnNew.Text = "New Macro"
            Me.btnNew.UseVisualStyleBackColor = True
            '
            'listMacros
            '
            Me.listMacros.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colTriggers, Me.colType, Me.colEffects, Me.colOutputs, Me.colDelay, Me.colMultiple})
            Me.listMacros.FullRowSelect = True
            Me.listMacros.Location = New System.Drawing.Point(12, 13)
            Me.listMacros.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.listMacros.Name = "listMacros"
            Me.listMacros.Size = New System.Drawing.Size(637, 317)
            Me.listMacros.TabIndex = 1
            Me.listMacros.UseCompatibleStateImageBehavior = False
            Me.listMacros.View = System.Windows.Forms.View.Details
            '
            'colName
            '
            Me.colName.Text = "Name"
            '
            'colTriggers
            '
            Me.colTriggers.DisplayIndex = 2
            Me.colTriggers.Text = "Triggers"
            '
            'colType
            '
            Me.colType.DisplayIndex = 1
            Me.colType.Text = "Type"
            '
            'colEffects
            '
            Me.colEffects.Text = "Effects"
            '
            'colOutputs
            '
            Me.colOutputs.Text = "Outputs"
            '
            'colDelay
            '
            Me.colDelay.Text = "Delay"
            '
            'colMultiple
            '
            Me.colMultiple.Text = "Multiple"
            '
            'btnDelete
            '
            Me.btnDelete.Enabled = False
            Me.btnDelete.Location = New System.Drawing.Point(251, 338)
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.Size = New System.Drawing.Size(110, 30)
            Me.btnDelete.TabIndex = 2
            Me.btnDelete.Text = "Delete Macro"
            Me.btnDelete.UseVisualStyleBackColor = True
            '
            'btnEdit
            '
            Me.btnEdit.Enabled = False
            Me.btnEdit.Location = New System.Drawing.Point(135, 338)
            Me.btnEdit.Name = "btnEdit"
            Me.btnEdit.Size = New System.Drawing.Size(110, 30)
            Me.btnEdit.TabIndex = 3
            Me.btnEdit.Text = "Edit Macro "
            Me.btnEdit.UseVisualStyleBackColor = True
            '
            'btnClose
            '
            Me.btnClose.Location = New System.Drawing.Point(539, 339)
            Me.btnClose.Name = "btnClose"
            Me.btnClose.Size = New System.Drawing.Size(110, 30)
            Me.btnClose.TabIndex = 4
            Me.btnClose.Text = "Close"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'Line1
            '
            Me.Line1.Direction = ScreenshotStudioDotNet.Core.Controls.LineDirection.Vertical
            Me.Line1.ForeColor = System.Drawing.Color.LightGray
            Me.Line1.LineColor = System.Drawing.Color.LightGray
            Me.Line1.Location = New System.Drawing.Point(123, 339)
            Me.Line1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.Line1.Name = "Line1"
            Me.Line1.Size = New System.Drawing.Size(10, 28)
            Me.Line1.TabIndex = 5
            Me.Line1.Thickness = 1
            '
            'MacroManager
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(661, 379)
            Me.Controls.Add(Me.btnClose)
            Me.Controls.Add(Me.btnEdit)
            Me.Controls.Add(Me.btnDelete)
            Me.Controls.Add(Me.listMacros)
            Me.Controls.Add(Me.btnNew)
            Me.Controls.Add(Me.Line1)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.MaximizeBox = False
            Me.Name = "MacroManager"
            Me.Text = "MacroManager"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents btnNew As System.Windows.Forms.Button
        Friend WithEvents listMacros As System.Windows.Forms.ListView
        Friend WithEvents colName As System.Windows.Forms.ColumnHeader
        Friend WithEvents colType As System.Windows.Forms.ColumnHeader
        Friend WithEvents btnDelete As System.Windows.Forms.Button
        Friend WithEvents btnEdit As System.Windows.Forms.Button
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents Line1 As ScreenshotStudioDotNet.Core.Controls.Line
        Friend WithEvents colTriggers As System.Windows.Forms.ColumnHeader
        Friend WithEvents colEffects As System.Windows.Forms.ColumnHeader
        Friend WithEvents colOutputs As System.Windows.Forms.ColumnHeader
        Friend WithEvents colDelay As System.Windows.Forms.ColumnHeader
        Friend WithEvents colMultiple As System.Windows.Forms.ColumnHeader
    End Class
End Namespace
