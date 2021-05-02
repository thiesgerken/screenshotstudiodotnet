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

Namespace Selector
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SelectRegion
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                    If Not _lastCursor Is Nothing Then _lastCursor.Dispose()


                    If Not _freehandPath Is Nothing Then _freehandPath.Dispose()
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectRegion))
            Me.mnuContext = New System.Windows.Forms.ContextMenuStrip()
            Me.mnuNewSelection = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuCapture = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuSwitchShape = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuMove = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuResize = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuResize50 = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuResize75 = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuResize125 = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuResize150 = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuRotate = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuCancel = New System.Windows.Forms.ToolStripMenuItem()
            Me.CloseMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.picRegion = New System.Windows.Forms.PictureBox()
            Me.mnuSelect = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuContext.SuspendLayout()
            Me.SuspendLayout()
            '
            'mnuContext
            '
            Me.mnuContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSelect, Me.mnuNewSelection, Me.mnuCapture, Me.ToolStripSeparator2, Me.mnuSwitchShape, Me.mnuMove, Me.mnuResize, Me.mnuRotate, Me.ToolStripSeparator1, Me.mnuCancel, Me.CloseMenuToolStripMenuItem})
            Me.mnuContext.Name = "mnuContext"
            resources.ApplyResources(Me.mnuContext, "mnuContext")
            '
            'mnuNewSelection
            '
            Me.mnuNewSelection.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.NewSelection
            Me.mnuNewSelection.Name = "mnuNewSelection"
            resources.ApplyResources(Me.mnuNewSelection, "mnuNewSelection")
            '
            'mnuCapture
            '
            Me.mnuCapture.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.Capture
            Me.mnuCapture.Name = "mnuCapture"
            resources.ApplyResources(Me.mnuCapture, "mnuCapture")
            '
            'ToolStripSeparator2
            '
            Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
            resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
            '
            'mnuSwitchShape
            '
            Me.mnuSwitchShape.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.Shape
            Me.mnuSwitchShape.Name = "mnuSwitchShape"
            resources.ApplyResources(Me.mnuSwitchShape, "mnuSwitchShape")
            '
            'mnuMove
            '
            Me.mnuMove.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.Move
            Me.mnuMove.Name = "mnuMove"
            resources.ApplyResources(Me.mnuMove, "mnuMove")
            '
            'mnuResize
            '
            Me.mnuResize.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuResize50, Me.mnuResize75, Me.mnuResize125, Me.mnuResize150})
            Me.mnuResize.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.Resize
            Me.mnuResize.Name = "mnuResize"
            resources.ApplyResources(Me.mnuResize, "mnuResize")
            '
            'mnuResize50
            '
            Me.mnuResize50.Name = "mnuResize50"
            resources.ApplyResources(Me.mnuResize50, "mnuResize50")
            '
            'mnuResize75
            '
            Me.mnuResize75.Name = "mnuResize75"
            resources.ApplyResources(Me.mnuResize75, "mnuResize75")
            '
            'mnuResize125
            '
            Me.mnuResize125.Name = "mnuResize125"
            resources.ApplyResources(Me.mnuResize125, "mnuResize125")
            '
            'mnuResize150
            '
            Me.mnuResize150.Name = "mnuResize150"
            resources.ApplyResources(Me.mnuResize150, "mnuResize150")
            '
            'mnuRotate
            '
            Me.mnuRotate.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.Rotate
            Me.mnuRotate.Name = "mnuRotate"
            resources.ApplyResources(Me.mnuRotate, "mnuRotate")
            '
            'ToolStripSeparator1
            '
            Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
            resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
            '
            'mnuCancel
            '
            Me.mnuCancel.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.Cancel
            Me.mnuCancel.Name = "mnuCancel"
            resources.ApplyResources(Me.mnuCancel, "mnuCancel")
            '
            'CloseMenuToolStripMenuItem
            '
            Me.CloseMenuToolStripMenuItem.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.BreakpointHS
            Me.CloseMenuToolStripMenuItem.Name = "CloseMenuToolStripMenuItem"
            resources.ApplyResources(Me.CloseMenuToolStripMenuItem, "CloseMenuToolStripMenuItem")
            '
            'picRegion
            '
            Me.picRegion.ContextMenuStrip = Me.mnuContext
            resources.ApplyResources(Me.picRegion, "picRegion")
            Me.picRegion.Name = "picRegion"
            Me.picRegion.TabStop = False
            '
            'mnuSelect
            '
            Me.mnuSelect.Image = Global.ScreenshotStudioDotNet.ScreenshotTypes.My.Resources.Resources.NewSelection
            Me.mnuSelect.Name = "mnuSelect"
            resources.ApplyResources(Me.mnuSelect, "mnuSelect")
            '
            'SelectRegion
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.picRegion)
            Me.Name = "SelectRegion"
            Me.mnuContext.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents picRegion As System.Windows.Forms.PictureBox
        Friend WithEvents mnuContext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents mnuNewSelection As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuCapture As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuMove As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuResize As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuResize50 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuResize150 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuResize125 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuRotate As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuCancel As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuSwitchShape As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Friend WithEvents mnuResize75 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CloseMenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents mnuSelect As System.Windows.Forms.ToolStripMenuItem
    End Class
End Namespace
