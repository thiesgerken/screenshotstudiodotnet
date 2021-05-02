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

Imports System.Resources
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Logging
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports ScreenshotStudioDotNet.Core.Settings

Namespace History
    Public Class HistoryViewer

#Region "Fields"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)
        Private _loaded As Boolean = False
        Private WithEvents _searcher As ObjectSearcher(Of Date, HistoryEntry)

#End Region


#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the HistoryViewer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub HistoryViewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(ControlStyles.UserPaint, True)

            Me.Opacity = 0
            txtSearch.WatermarkText = _langManager.GetString("Search")

            Dim unnecessaryValues As New List(Of String)
            unnecessaryValues.Add("")
            unnecessaryValues.Add("True")
            unnecessaryValues.Add("False")
            unnecessaryValues.Add("0")
            unnecessaryValues.Add("1")
            unnecessaryValues.Add("(Icon)")

            Dim unnecessaryProperties As New List(Of String)
            unnecessaryProperties.Add("Icon")
            unnecessaryProperties.Add("UtcNow")
            unnecessaryProperties.Add("Now")
            unnecessaryProperties.Add("Length")
            unnecessaryProperties.Add("Chars")
            unnecessaryProperties.Add("Icon")
            unnecessaryProperties.Add("IconPath")

            _searcher = New ObjectSearcher(Of Date, HistoryEntry)(ScreenshotHistory.HistoryEntries, unnecessaryValues, unnecessaryProperties)
            _searcher.ApplyFilter(SettingsDatabase.LastHistoryFilter)
            txtSearch.Text = SettingsDatabase.LastHistoryFilter

            ProcessResize()
        End Sub

        ''' <summary>
        ''' Handles the Resize event of the HistoryViewer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub HistoryViewer_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
            ProcessResize()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnClose control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnDeleteAll control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteAll.Click
            Dim result As TaskDialogResult
            If SettingsDatabase.AskForHistoryClean Then
                Dim tdlg As New TaskDialog()
                tdlg.Caption = "ScreenshotStudio.Net"
                tdlg.InstructionText = _langManager.GetString("DeleteAllQInstruction")
                tdlg.Text = _langManager.GetString("DeleteAllQText")
                tdlg.Icon = TaskDialogStandardIcon.Warning
                tdlg.StandardButtons = TaskDialogStandardButtons.Yes Or TaskDialogStandardButtons.No
                tdlg.FooterCheckBoxText = _langManager.GetString("DontAskAgain")
                tdlg.FooterCheckBoxChecked = False

                result = tdlg.Show()

                SettingsDatabase.AskForHistoryClean = Not CBool(tdlg.FooterCheckBoxChecked)
            Else
                result = TaskDialogResult.Yes
            End If

            If result = TaskDialogResult.Yes Then
                ScreenshotHistory.ClearHistory()
                Log.LogInformation("Cleared History")
                _searcher.ApplyFilter(txtSearch.Text)
            End If
        End Sub

        ''' <summary>
        ''' Handles the Shown event of the HistoryViewer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub HistoryViewer_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
            Me.Opacity = 1
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnDeleteEntry control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnDeleteEntry_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteEntry.Click
            DeleteCurrentEntry()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnOutputEntry control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnOutputEntry_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOutputEntry.Click
            OutputCurrentEntry()
        End Sub

        ''' <summary>
        ''' Handles the TextChanged event of the txtSearch control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.TextChanged
            _searcher.ApplyFilter(txtSearch.Text.ToUpperInvariant)
        End Sub

        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the listScreenshots control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub listScreenshots_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listScreenshots.SelectedIndexChanged
            UpdateView()
        End Sub

        ''' <summary>
        ''' Handles the Tick event of the timNoEntryChecker control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub timNoEntryChecker_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timNoEntryChecker.Tick
            If listScreenshots.SelectedIndices.Count = 0 Then
                ToggleView(Views.NoEntry)
            End If

            timNoEntryChecker.Enabled = False
        End Sub

        ''' <summary>
        ''' Handles the SearchCompleted event of the _searcher control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub _searcher_SearchCompleted(ByVal sender As Object, ByVal e As EventArgs) Handles _searcher.SearchCompleted
            DisplayResults()
        End Sub

        ''' <summary>
        ''' Handles the SearchError event of the _searcher control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="ScreenshotStudioDotnet.Core.History.SearchErrorEventArgs" /> instance containing the event data.</param>
        Private Sub _searcher_SearchError(ByVal sender As Object, ByVal e As SearchErrorEventArgs) Handles _searcher.SearchError
            DisplayErrorMessage(e.Exception)
        End Sub

        ''' <summary>
        ''' Handles the SearchStarted event of the _searcher control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub _searcher_SearchStarted(ByVal sender As Object, ByVal e As EventArgs) Handles _searcher.SearchStarted
            DisplaySearchMessage()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Updates the view.
        ''' </summary>
        Private Sub UpdateView()
            If Not _loaded Then Exit Sub

            listScreenshots.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
            If listScreenshots.Columns.Count > 1 Then
                listScreenshots.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize)
            End If

            If listScreenshots.SelectedItems.Count = 0 Then
                timNoEntryChecker.Enabled = True
            ElseIf Date.TryParse(listScreenshots.Items(listScreenshots.SelectedIndices(0)).Text, New Date) Then
                ToggleView(Views.Entry)

                Dim item = ScreenshotHistory.HistoryEntries(New Date(CLng(listScreenshots.Items(listScreenshots.SelectedIndices(0)).Name)))

                Dim screenshot As Bitmap
                Try
                    screenshot = ScreenshotHistory.GetScreenshot(item).Screenshot

                    btnOutputEntry.Enabled = True
                Catch ex As Exception
                    '"dummy" bitmap to get the size of the string
                    screenshot = New Bitmap(1, 1)
                    Dim g As Graphics = Graphics.FromImage(screenshot)
                    Dim size As SizeF = g.MeasureString(_langManager.GetString("ErrorLoading"), New Font("Segoe UI", 28))
                    g.Dispose()

                    'now with the real size
                    screenshot = New Bitmap(CInt(size.Width), CInt(size.Height))
                    g = Graphics.FromImage(screenshot)
                    g.DrawString(_langManager.GetString("ErrorLoading"), New Font("Segoe UI", 28), Brushes.DarkBlue, CInt(screenshot.Width / 2 - size.Width / 2), CInt(screenshot.Height / 2 - size.Height / 2))
                    g.Dispose()

                    btnOutputEntry.Enabled = False
                End Try

                'Scale the image
                Dim picBmp As New Bitmap(picScreenshot.Width, picScreenshot.Height)
                Dim picG As Graphics = Graphics.FromImage(picBmp)
                Dim height As Integer
                Dim width As Integer

                If screenshot.Width / screenshot.Height > picBmp.Width / picBmp.Height Then
                    height = CInt(screenshot.Height / screenshot.Width * picBmp.Width)
                    width = picBmp.Width
                Else
                    width = CInt(screenshot.Width / screenshot.Height * picBmp.Height)
                    height = picBmp.Height
                End If

                picG.DrawImage(screenshot, New RectangleF(CSng(picBmp.Width / 2 - width / 2), CSng(picBmp.Height / 2 - height / 2), width, height))

                'clean up
                picG.Dispose()

                picScreenshot.Image = picBmp
                pgridScreenshotProperties.SelectedObject = New EntryProperties(screenshot, item)
            Else
                ToggleView(Views.NoEntry)
            End If
        End Sub

        ''' <summary>
        ''' Processes the resize.
        ''' </summary>
        Private Sub ProcessResize()
            Dim totalHeight As Integer = Me.ClientRectangle.Height - 9 - Panel1.Height
            listScreenshots.Height = totalHeight - 6 - txtSearch.Height - lblItemsMatching.Height - 3 - 3 - 6

            txtSearch.Location = New Point(9, 12)
            txtSearch.Width = listScreenshots.Width

            lblItemsMatching.Left = listScreenshots.Left
            lblItemsMatching.Top = listScreenshots.Bottom + 3

            boxPreview.Left = listScreenshots.Right + 6
            boxPreview.Height = CInt((totalHeight - 6 - panelActions.Height) * 4 / 10)
            boxPreview.Width = Me.ClientRectangle.Width - listScreenshots.Width - 23
            boxPreview.Top = 6

            panelActions.Left = listScreenshots.Right + 6
            panelActions.Top = boxPreview.Bottom
            panelActions.Width = Me.ClientRectangle.Width - listScreenshots.Width - 23

            pgridScreenshotProperties.Left = listScreenshots.Right + 6
            pgridScreenshotProperties.Height = CInt((totalHeight - 6 - panelActions.Height) * 6 / 10)
            pgridScreenshotProperties.Top = panelActions.Bottom
            pgridScreenshotProperties.Width = Me.ClientRectangle.Width - listScreenshots.Width - 23

            panelNoEntry.Width = Me.ClientRectangle.Width - listScreenshots.Width - 23
            panelNoEntry.Height = totalHeight
            panelNoEntry.Left = listScreenshots.Right + 6
            panelNoEntry.Top = 6

            UpdateView()
        End Sub

        ''' <summary>
        ''' Deletes the current entry.
        ''' </summary>
        Private Sub DeleteCurrentEntry()
            Dim result As TaskDialogResult
            If SettingsDatabase.AskForHistoryDeleteItem Then
                Dim tdlg As New TaskDialog()
                tdlg.Caption = "ScreenshotStudio.Net"
                tdlg.InstructionText = _langManager.GetString("DeleteEntryQInstruction")
                tdlg.Text = _langManager.GetString("DeleteEntryQText")
                tdlg.Icon = TaskDialogStandardIcon.Information
                tdlg.StandardButtons = TaskDialogStandardButtons.Yes Or TaskDialogStandardButtons.No
                tdlg.FooterCheckBoxText = _langManager.GetString("DontAskAgain")
                tdlg.FooterCheckBoxChecked = False

                result = tdlg.Show()

                SettingsDatabase.AskForHistoryDeleteItem = Not CBool(tdlg.FooterCheckBoxChecked)
            Else
                result = TaskDialogResult.Yes
            End If

            If result = TaskDialogResult.Yes Then
                Dim item = ScreenshotHistory.HistoryEntries(New Date(CLng(listScreenshots.Items(listScreenshots.SelectedIndices(0)).Name)))
                ScreenshotHistory.DeleteItem(item)
                Log.LogInformation("Deleted History Item: " & item.DateTaken.Ticks)
                _searcher.ApplyFilter(txtSearch.Text)
            End If
        End Sub

        ''' <summary>
        ''' Outputs the current entry.
        ''' </summary>
        Private Sub OutputCurrentEntry()
            Dim item = ScreenshotHistory.HistoryEntries(New Date(CLng(listScreenshots.Items(listScreenshots.SelectedIndices(0)).Name)))
            Dim screenshot = ScreenshotHistory.GetScreenshot(item)

            Log.LogInformation("Outputting Screenshot " & item.DateTaken.Ticks & " (from history) again")

            Dim o As New OutputPicker
            o.ShowDialog()

            If o.SelectedOutputName <> "" Then
                Log.LogInformation("Using output: " & o.SelectedOutputName)
                'get an instance of the output plugin
                Dim plugDatabase As New PluginDatabase(Of IOutput)
                Dim plug = plugDatabase(o.SelectedOutputName)
                Dim output As IOutput = CType(plug.CreateInstance, IOutput)
                Dim result = output.Proceed(screenshot)

                item.OutputsUsed.Add(plug)
                item.AdditionalInformation.Add(result)

                UpdateView()
            Else
                Log.LogWarning("User canceled the output")
            End If
        End Sub

        ''' <summary>
        ''' Toggles the view.
        ''' </summary>
        ''' <param name="newView">The new view.</param>
        Private Sub ToggleView(ByVal newView As Views)
            panelActions.Visible = newView = Views.Entry
            boxPreview.Visible = newView = Views.Entry
            pgridScreenshotProperties.Visible = newView = Views.Entry
            panelNoEntry.Visible = newView = Views.NoEntry
        End Sub

        ''' <summary>
        ''' Displays the error message.
        ''' </summary>
        Private Sub DisplayErrorMessage(ByVal ex As Exception)
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Exception)(AddressOf DisplayErrorMessage))
            Else
                Log.LogError(ex)

                listScreenshots.Columns.Clear()
                listScreenshots.Columns.Add("Info")

                listScreenshots.Items.Clear()
                listScreenshots.Items.Add(_langManager.GetString("searchError"))
                lblItemsMatching.Text = ""
            End If
        End Sub

        ''' <summary>
        ''' Displays the search message.
        ''' </summary>
        Private Sub DisplaySearchMessage()
            If Me.InvokeRequired Then
                Me.Invoke(New Action(AddressOf DisplaySearchMessage))
            Else
                listScreenshots.Columns.Clear()
                listScreenshots.Columns.Add("Info")

                listScreenshots.Items.Clear()
                listScreenshots.Items.Add(_langManager.GetString("searching"))
                listScreenshots.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

                lblItemsMatching.Text = ""
            End If
        End Sub

        ''' <summary>
        ''' Displays the results.
        ''' </summary>
        Private Sub DisplayResults()
            If Me.InvokeRequired Then
                Me.Invoke(New Action(AddressOf DisplayResults))
            Else
                SettingsDatabase.LastHistoryFilter = txtSearch.Text

                'Update the List
                Dim items As New List(Of ListViewItem)

                For Each entry In _searcher.Results
                    Dim itm As New ListViewItem(New String() {entry.Key.ToString, entry.Value & "%"})
                    itm.Name = CStr(entry.Key.Ticks)

                    items.Add(itm)
                Next

                'Update the count:
                lblItemsMatching.Text = _searcher.Results.Count & " " & _langManager.GetString("Item" & If(_searcher.Results.Count <> 1, "s", "")) & " " & _langManager.GetString("Found")

                listScreenshots.Columns.Clear()
                listScreenshots.Columns.Add(_langManager.GetString("_date"))
                listScreenshots.Columns.Add(_langManager.GetString("Relevance"))

                'Hide the second column if that was no real search
                If txtSearch.Text = "" Or items.Count = 0 Then
                    listScreenshots.Columns.RemoveAt(1)

                    listScreenshots.Sorting = SortOrder.Descending
                    listScreenshots.Sort()
                End If

                If items.Count = 0 Then
                    listScreenshots.Columns(0).Text = "Info"
                    items.Add(New ListViewItem(_langManager.GetString("NoEntry")))
                End If

                listScreenshots.Items.Clear()
                listScreenshots.Items.AddRange(items.ToArray)

                _loaded = True
                UpdateView()
            End If
        End Sub

        ''' <summary>
        ''' Processes a command key.
        ''' </summary>
        ''' <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
        ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        ''' <returns>
        ''' true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
        ''' </returns>
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If listScreenshots.Focused And listScreenshots.SelectedItems.Count <> 0 Then
                If Date.TryParse(listScreenshots.Items(listScreenshots.SelectedIndices(0)).Text, New Date) Then
                    If keyData = Keys.Delete Then
                        DeleteCurrentEntry()
                        Return True
                    ElseIf keyData = Keys.Enter Then
                        OutputCurrentEntry()
                        Return True
                    End If
                End If
            End If

            Return False
        End Function

#End Region
    End Class
End Namespace
