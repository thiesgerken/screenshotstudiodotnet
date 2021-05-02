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
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports ScreenshotStudioDotNet.Core.Controls
Imports ScreenshotStudioDotNet.Core.Colorization
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Settings
    Public Class QuickStartSettingsPanel

#Region "Fields"

        Private _colorizations As New ColorizationDatabase
        Private _formerColorizations As ColorizationDatabase
        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)
        Private _activeColorization As String

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="QuickStartSettingsPanel" /> class.
        ''' </summary>
        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            numScale.Value = CDec(SettingsDatabase.QuickStartScaleFactor)
            _activeColorization = SettingsDatabase.Colorization.Name

            lblNoColorSelected.Location = panelColorSelect.Location
            lblNoColorSelected.Width = panelColorSelect.Width
            lblNoColorSelected.Height = listColorizations.Height

            _formerColorizations = New ColorizationDatabase

            RefreshItems()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Refreshes the items.
        ''' </summary>
        Private Sub RefreshItems()
            listColorizations.Items.Clear()
            For Each color In _colorizations
                Dim i As New ListViewItem(color.Name)
                i.Name = color.Name

                Dim isActive As Boolean = _activeColorization = color.Name

                i.Font = New Font("Segoe UI", 10, If(isActive, FontStyle.Bold, FontStyle.Regular))

                listColorizations.Items.Add(i)

                If isActive Then i.Selected = True
            Next

            CheckForChanges()
        End Sub

        ''' <summary>
        ''' Checks for changes.
        ''' </summary>
        Private Sub CheckForChanges()
            For Each i As ListViewItem In listColorizations.Items
                Dim oldListContains As Boolean = _formerColorizations.Contains(i.Name)
                Dim newListContains = _colorizations.Contains(i.Name)

                If Not oldListContains And newListContains Then
                    i.Text = i.Name & " (+)"
                Else
                    If _formerColorizations(i.Name).ToString <> _colorizations(i.Name).ToString Then
                        i.Text = i.Name & " (*)"
                    Else
                        i.Text = i.Name
                    End If
                End If
            Next
        End Sub

        ''' <summary>
        ''' Processes a command key.
        ''' </summary>
        ''' <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
        ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        ''' <returns>
        ''' true if the character was processed by the control; otherwise, false.
        ''' </returns>
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If listColorizations.Focused And listColorizations.SelectedItems.Count <> 0 Then
                If keyData = Keys.Delete Then
                    DeleteCurrentEntry()
                    Return True
                ElseIf keyData = Keys.Enter Then
                    SetCurrentEntryAsActive()
                    Return True
                End If
            End If

            Return False
        End Function

        ''' <summary>
        ''' Saves this instance.
        ''' </summary> 
        Public Overrides Sub Save()
            _colorizations.Save()
            _colorizations.Load()

            _formerColorizations = New ColorizationDatabase

            SettingsDatabase.QuickStartScaleFactor = numScale.Value
            SettingsDatabase.Colorization = If(_colorizations.Contains(_activeColorization), _colorizations(_activeColorization), _colorizations.ToArray()(0))
            SettingsDatabase.Save()
            SettingsDatabase.EmptyCache()

            _activeColorization = SettingsDatabase.Colorization.Name

            RefreshItems()
        End Sub

        ''' <summary>
        ''' Sets the current entry as active.
        ''' </summary>
        Public Sub SetCurrentEntryAsActive()
            If listColorizations.SelectedItems.Count > 0 Then
                _activeColorization = listColorizations.SelectedItems(0).Name

                RefreshItems()
            End If
        End Sub

        ''' <summary>
        ''' Deletes the current entry.
        ''' </summary>
        Public Sub DeleteCurrentEntry()
            _colorizations.Remove(listColorizations.SelectedItems(0).Name)

            If _activeColorization = listColorizations.SelectedItems(0).Name Then _activeColorization = _colorizations(0).Name

            RefreshItems()
        End Sub

#End Region

#Region "Overridden Properties"

        ''' <summary>
        ''' Gets a value indicating whether [properties changed].
        ''' </summary>
        ''' <value><c>true</c> if [properties changed]; otherwise, <c>false</c>.</value>
        Public Overrides ReadOnly Property PropertiesChanged() As Boolean
            Get
                If SettingsDatabase.QuickStartScaleFactor <> numScale.Value Or SettingsDatabase.Colorization.Name <> _activeColorization Then Return True

                For Each i As Colorization.Colorization In _colorizations
                    Dim oldListContains As Boolean = _formerColorizations.Contains(i.Name)

                    If Not oldListContains Then
                        Return True
                    Else
                        If _formerColorizations(i.Name).ToString <> _colorizations(i.Name).ToString Then
                            Return True
                        End If
                    End If
                Next

                For Each i As Colorization.Colorization In _formerColorizations
                    Dim newListContains = _colorizations.Contains(i.Name)

                    If Not newListContains Then
                        Return True
                    Else
                        If _formerColorizations(i.Name).ToString <> _colorizations(i.Name).ToString Then
                            Return True
                        End If
                    End If
                Next

                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets the display name.
        ''' </summary>
        ''' <value>The display name.</value>
        Public Overrides ReadOnly Property DisplayName As String
            Get
                Return "QuickStart"
            End Get
        End Property

#End Region

#Region "Functions"

        ''' <summary>
        ''' Handles the DoubleClick event of the listColorizations control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub listColorizations_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles listColorizations.DoubleClick
            SetCurrentEntryAsActive()
        End Sub

        ''' <summary>
        ''' Handles the SelectedIndexChanged event of the listColorizations control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub listColorizations_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listColorizations.SelectedIndexChanged
            panelColorSelect.Visible = listColorizations.SelectedItems.Count > 0
            lblNoColorSelected.Visible = listColorizations.SelectedItems.Count = 0

            If listColorizations.SelectedItems.Count > 0 Then
                Dim colorization = _colorizations(listColorizations.SelectedItems(0).Name)

                cbtnHover.Color = colorization.GetHoveredColor
                cbtnNormal.Color = colorization.GetNormalColor
                cbtnMouseDown.Color = colorization.GetDownColor

                btnDeleteCurrent.Width = 94
                btnSetAsActive.Visible = True
                If _activeColorization = listColorizations.SelectedItems(0).Name Then
                    btnDeleteCurrent.Width += 100
                    btnSetAsActive.Visible = False
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the ColorChanged event of the cbtnHover control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub cbtnHover_ColorChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbtnHover.ColorChanged
            _colorizations(listColorizations.SelectedItems(0).Name).SetHoveredColor(cbtnHover.Color)
            CheckForChanges()
        End Sub

        ''' <summary>
        ''' Handles the ColorChanged event of the cbtnMouseDown control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub cbtnMouseDown_ColorChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbtnMouseDown.ColorChanged
            _colorizations(listColorizations.SelectedItems(0).Name).SetDownColor(cbtnMouseDown.Color)
            CheckForChanges()
        End Sub

        ''' <summary>
        ''' Handles the ColorChanged event of the cbtnNormal control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub cbtnNormal_ColorChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbtnNormal.ColorChanged
            _colorizations(listColorizations.SelectedItems(0).Name).SetNormalColor(cbtnNormal.Color)
            CheckForChanges()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnReset control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
            _colorizations.Clear()
            _colorizations.Save()
            _colorizations.Load()

            RefreshItems()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnDeleteCurrent control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnDeleteCurrent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteCurrent.Click
            DeleteCurrentEntry()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnAddToDB control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnAddToDB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddToDB.Click
            Dim inputDlg As New InputDialog
            inputDlg.Instruction = _langManager.GetString("createText")
            inputDlg.LongInstruction = _langManager.GetString("createTask")
            inputDlg.ActionName = _langManager.GetString("create")
            inputDlg.CancelActionName = _langManager.GetString("cancel")
            inputDlg.InputParameterName = "Name"
            inputDlg.Input = ""

            If inputDlg.ShowDialog() = DialogResult.OK Then
                If _colorizations.Contains(inputDlg.Input) Then
                    Dim errorDlg As New TaskDialog

                    errorDlg.Caption = "ScreenshotStudio.Net"
                    errorDlg.InstructionText = _langManager.GetString("creationFailed")
                    errorDlg.Text = _langManager.GetString("colorError")
                    errorDlg.StandardButtons = TaskDialogStandardButtons.Ok
                    errorDlg.Icon = TaskDialogStandardIcon.Error

                    errorDlg.Show()
                Else
                    _colorizations.Add(New Colorization.Colorization(inputDlg.Input, Color.Black, Color.Black, Color.Black))

                    'Refresh
                    RefreshItems()

                    listColorizations.SelectedItems.Clear()
                    listColorizations.Items(inputDlg.Input).Selected = True
                End If
            End If
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnSetAsActive control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnSetAsActive_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetAsActive.Click
            SetCurrentEntryAsActive()
        End Sub

#End Region
    End Class
End Namespace
