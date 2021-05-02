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

Namespace History
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HistoryViewer
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistoryViewer))
            Me.picScreenshot = New System.Windows.Forms.PictureBox()
            Me.pgridScreenshotProperties = New System.Windows.Forms.PropertyGrid()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.btnDeleteAll = New System.Windows.Forms.Button()
            Me.btnClose = New System.Windows.Forms.Button()
            Me.boxPreview = New System.Windows.Forms.GroupBox()
            Me.panelActions = New System.Windows.Forms.Panel()
            Me.lblActions = New System.Windows.Forms.Label()
            Me.btnOutputEntry = New System.Windows.Forms.Button()
            Me.btnDeleteEntry = New System.Windows.Forms.Button()
            Me.panelNoEntry = New System.Windows.Forms.Panel()
            Me.lblNoEntry = New System.Windows.Forms.Label()
            Me.lblItemsMatching = New System.Windows.Forms.Label()
            Me.listScreenshots = New System.Windows.Forms.ListView()
            Me.colDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.colRelevance = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.timNoEntryChecker = New System.Windows.Forms.Timer()
            Me.txtSearch = New ScreenshotStudioDotNet.Core.Controls.WatermarkTextBox()
            Me.FormStateSaver = New ScreenshotStudioDotNet.Core.Controls.FormStateSaver()
            Me.Panel1.SuspendLayout()
            Me.boxPreview.SuspendLayout()
            Me.panelActions.SuspendLayout()
            Me.panelNoEntry.SuspendLayout()
            Me.SuspendLayout()
            '
            'picScreenshot
            '
            resources.ApplyResources(Me.picScreenshot, "picScreenshot")
            Me.picScreenshot.Name = "picScreenshot"
            Me.picScreenshot.TabStop = False
            '
            'pgridScreenshotProperties
            '
            resources.ApplyResources(Me.pgridScreenshotProperties, "pgridScreenshotProperties")
            Me.pgridScreenshotProperties.Name = "pgridScreenshotProperties"
            Me.pgridScreenshotProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
            Me.pgridScreenshotProperties.ToolbarVisible = False
            '
            'Panel1
            '
            resources.ApplyResources(Me.Panel1, "Panel1")
            Me.Panel1.Controls.Add(Me.btnDeleteAll)
            Me.Panel1.Controls.Add(Me.btnClose)
            Me.Panel1.Name = "Panel1"
            '
            'btnDeleteAll
            '
            resources.ApplyResources(Me.btnDeleteAll, "btnDeleteAll")
            Me.btnDeleteAll.Name = "btnDeleteAll"
            Me.btnDeleteAll.UseVisualStyleBackColor = True
            '
            'btnClose
            '
            resources.ApplyResources(Me.btnClose, "btnClose")
            Me.btnClose.Name = "btnClose"
            Me.btnClose.UseVisualStyleBackColor = True
            '
            'boxPreview
            '
            resources.ApplyResources(Me.boxPreview, "boxPreview")
            Me.boxPreview.Controls.Add(Me.picScreenshot)
            Me.boxPreview.Name = "boxPreview"
            Me.boxPreview.TabStop = False
            '
            'panelActions
            '
            resources.ApplyResources(Me.panelActions, "panelActions")
            Me.panelActions.Controls.Add(Me.lblActions)
            Me.panelActions.Controls.Add(Me.btnOutputEntry)
            Me.panelActions.Controls.Add(Me.btnDeleteEntry)
            Me.panelActions.Name = "panelActions"
            '
            'lblActions
            '
            resources.ApplyResources(Me.lblActions, "lblActions")
            Me.lblActions.Name = "lblActions"
            '
            'btnOutputEntry
            '
            resources.ApplyResources(Me.btnOutputEntry, "btnOutputEntry")
            Me.btnOutputEntry.Name = "btnOutputEntry"
            Me.btnOutputEntry.UseVisualStyleBackColor = True
            '
            'btnDeleteEntry
            '
            resources.ApplyResources(Me.btnDeleteEntry, "btnDeleteEntry")
            Me.btnDeleteEntry.Name = "btnDeleteEntry"
            Me.btnDeleteEntry.UseVisualStyleBackColor = True
            '
            'panelNoEntry
            '
            resources.ApplyResources(Me.panelNoEntry, "panelNoEntry")
            Me.panelNoEntry.Controls.Add(Me.lblNoEntry)
            Me.panelNoEntry.Name = "panelNoEntry"
            '
            'lblNoEntry
            '
            resources.ApplyResources(Me.lblNoEntry, "lblNoEntry")
            Me.lblNoEntry.Name = "lblNoEntry"
            '
            'lblItemsMatching
            '
            resources.ApplyResources(Me.lblItemsMatching, "lblItemsMatching")
            Me.lblItemsMatching.Name = "lblItemsMatching"
            '
            'listScreenshots
            '
            resources.ApplyResources(Me.listScreenshots, "listScreenshots")
            Me.listScreenshots.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colDate, Me.colRelevance})
            Me.listScreenshots.FullRowSelect = True
            Me.listScreenshots.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
            Me.listScreenshots.MultiSelect = False
            Me.listScreenshots.Name = "listScreenshots"
            Me.listScreenshots.UseCompatibleStateImageBehavior = False
            Me.listScreenshots.View = System.Windows.Forms.View.Details
            '
            'colDate
            '
            resources.ApplyResources(Me.colDate, "colDate")
            '
            'colRelevance
            '
            resources.ApplyResources(Me.colRelevance, "colRelevance")
            '
            'timNoEntryChecker
            '
            '
            'txtSearch
            '
            resources.ApplyResources(Me.txtSearch, "txtSearch")
            Me.txtSearch.Name = "txtSearch"
            Me.txtSearch.WatermarkText = "Search"
            '
            'FormStateSaver
            '
            Me.FormStateSaver.Form = Me
            Me.FormStateSaver.SaveSize = True
            '
            'HistoryViewer
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.listScreenshots)
            Me.Controls.Add(Me.lblItemsMatching)
            Me.Controls.Add(Me.panelNoEntry)
            Me.Controls.Add(Me.panelActions)
            Me.Controls.Add(Me.txtSearch)
            Me.Controls.Add(Me.boxPreview)
            Me.Controls.Add(Me.pgridScreenshotProperties)
            Me.Controls.Add(Me.Panel1)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "HistoryViewer"
            Me.Panel1.ResumeLayout(False)
            Me.boxPreview.ResumeLayout(False)
            Me.panelActions.ResumeLayout(False)
            Me.panelActions.PerformLayout()
            Me.panelNoEntry.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents picScreenshot As System.Windows.Forms.PictureBox
        Friend WithEvents pgridScreenshotProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents FormStateSaver As Core.Controls.FormStateSaver
        Friend WithEvents btnClose As System.Windows.Forms.Button
        Friend WithEvents btnDeleteAll As System.Windows.Forms.Button
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents boxPreview As System.Windows.Forms.GroupBox
        Friend WithEvents txtSearch As ScreenshotStudioDotNet.Core.Controls.WatermarkTextBox
        Friend WithEvents panelActions As System.Windows.Forms.Panel
        Friend WithEvents btnDeleteEntry As System.Windows.Forms.Button
        Friend WithEvents btnOutputEntry As System.Windows.Forms.Button
        Friend WithEvents lblActions As System.Windows.Forms.Label
        Friend WithEvents panelNoEntry As System.Windows.Forms.Panel
        Friend WithEvents lblNoEntry As System.Windows.Forms.Label
        Friend WithEvents lblItemsMatching As System.Windows.Forms.Label
        Friend WithEvents listScreenshots As System.Windows.Forms.ListView
        Friend WithEvents colDate As System.Windows.Forms.ColumnHeader
        Friend WithEvents colRelevance As System.Windows.Forms.ColumnHeader
        Friend WithEvents timNoEntryChecker As System.Windows.Forms.Timer

    End Class
End Namespace
