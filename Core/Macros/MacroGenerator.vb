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
Imports System.Windows.Forms
Imports ScreenshotStudioDotNet.Core.Misc
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Aero
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports System.Threading
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Macros
    Public Class MacroGenerator
#Region "Properties"

        ''' <summary>
        ''' Gets or sets a value indicating whether this <see cref="MacroGenerator" /> is dragging.
        ''' </summary>
        ''' <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
        Public Property Dragging As Boolean

        ''' <summary>
        ''' Gets or sets the macro.
        ''' </summary>
        ''' <value>The macro.</value>
        Public Property Macro As Macro

#End Region

#Region "Fields"

        Private _buttonWidth As Integer = 140
        Private _buttonHeight As Integer = 22
        Private _buttonHeightExpanded As Integer = 70
        Private _layoutArea As Rectangle

        Private _triggerDatabase As New PluginDatabase(Of ITriggerManager)
        Private _typeDatabase As New PluginDatabase(Of IScreenshotType)
        Private _outputDatabase As New PluginDatabase(Of IOutput)
        Private _effectDatabase As New PluginDatabase(Of IEffect)
        Private _macroDatabase As New MacroDatabase()

        Private _typeDisabledComponent As New DisabledMacroComponent(Me, "Disabled")
        Private _typeInUse As TypeMacroComponent
        Private _delayComponent As New DelayMacroComponent(Me, "Delay")
        Private _multipleComponent As New MultipleMacroComponent(Me, "Multiple")

        Private _delayInUse As DelayMacroComponent
        Private _multipleInUse As MultipleMacroComponent

        Private _outputsInUse As New List(Of OutputMacroComponent)
        Private _triggersInUse As New List(Of TriggerMacroComponent)
        Private _effectsInUse As New List(Of EffectMacroComponent)

        Private _typesAvailable As New List(Of TypeMacroComponent)
        Private _outputsAvailable As New List(Of OutputMacroComponent)
        Private _triggersAvailable As New List(Of TriggerMacroComponent)
        Private _effectsAvailable As New List(Of EffectMacroComponent)

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the DragLeave event of the MacroGenerator control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub MacroGenerator_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DragLeave
            Dragging = False
        End Sub

        ''' <summary>
        ''' Handles the Load event of the MacroGenerator control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub MacroGenerator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.GlassMargins = New Margins(panelBtns.Left, Me.ClientRectangle.Width - panelBtns.Right, panelBtns.Top, Me.ClientRectangle.Height - panelBtns.Bottom)

            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

            Reset()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCreate control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
            CreateMacroAndClose()
        End Sub

        ''' <summary>
        ''' Handles the DragDrop event of the MacroGenerator control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.DragEventArgs" /> instance containing the event data.</param>
        Private Sub MacroGenerator_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
            Dim controlName As String = e.Data.GetData(DataFormats.Text, True).ToString

            Dim control As MacroComponent = Nothing

            If controlName = _delayComponent.Name Then control = _delayComponent
            If controlName = _multipleComponent.Name Then control = _multipleComponent

            For Each t In _triggersAvailable
                If t.Name = controlName Then control = t
            Next

            For Each t In _outputsAvailable
                If t.Name = controlName Then control = t
            Next

            For Each t In _effectsAvailable
                If t.Name = controlName Then control = t
            Next

            For Each t In _typesAvailable
                If t.Name = controlName Then control = t
            Next

            If control IsNot Nothing Then
                Dim controlCopy As MacroComponent = CType(Extensions.Clone(control), MacroComponent)
                controlCopy.InUse = True
                controlCopy.Height = _buttonHeightExpanded
                controlCopy.ParentForm = Me
                controlCopy.Visible = False

                Select Case controlCopy.GetType
                    Case GetType(DelayMacroComponent)
                        _delayInUse = CType(control, DelayMacroComponent)
                    Case GetType(MultipleMacroComponent)
                        _multipleInUse = CType(control, MultipleMacroComponent)
                    Case GetType(TypeMacroComponent)
                        If _typeInUse IsNot Nothing Then _typeInUse.Visible = False

                        _typeInUse = CType(controlCopy, TypeMacroComponent)
                    Case GetType(EffectMacroComponent)
                        _effectsInUse.Add(CType(controlCopy, EffectMacroComponent))
                    Case GetType(OutputMacroComponent)
                        _outputsInUse.Add(CType(controlCopy, OutputMacroComponent))
                    Case GetType(TriggerMacroComponent)
                        _triggersInUse.Add(CType(controlCopy, TriggerMacroComponent))
                End Select

                RefreshToolBox()
            End If

            Dim waitThread As New Thread(AddressOf SetDragState)
            waitThread.Start()
        End Sub

        ''' <summary>
        ''' Handles the DragEnter event of the MacroGenerator control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.DragEventArgs" /> instance containing the event data.</param>
        Private Sub MacroGenerator_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
            Dragging = True

            Dim controlName As String = e.Data.GetData(DataFormats.Text, True).ToString

            Dim similiarControlCount As Integer = 0

            For Each t In _triggersAvailable
                If t.Name = controlName Then similiarControlCount = _triggersInUse.Count
            Next

            For Each t In _outputsAvailable
                If t.Name = controlName Then similiarControlCount = _outputsInUse.Count
            Next

            For Each t In _effectsAvailable
                If t.Name = controlName Then similiarControlCount = _effectsInUse.Count
            Next

            If similiarControlCount >= 4 Or e.X <= _buttonWidth + 10 Then
                e.Effect = DragDropEffects.None
            Else
                e.Effect = e.AllowedEffect
            End If
        End Sub

        ''' <summary>
        ''' Handles the Tick event of the timCreationEnabledChecker control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub timCreationEnabledChecker_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timCreationEnabledChecker.Tick
            Dim badThings As New List(Of String)

            If txtName.Text = "" Then
                badThings.Add("The name of the macro must not be blank.")
            Else
                If _macroDatabase.Contains(txtName.Text) Then badThings.Add("A macro with this name is already existing in the database.")
            End If

            Dim typeExisting As Boolean = _typeInUse IsNot Nothing

            If Not typeExisting Then badThings.Add("The macro has no type.")

            lblBadThings.Text = ""
            If badThings.Count = 0 Then
                btnCreate.Enabled = True
            Else
                btnCreate.Enabled = False

                Dim isFirst As Boolean = True
                For Each bt In badThings
                    lblBadThings.Text &= If(isFirst, "", vbCrLf) & bt
                    isFirst = False
                Next
            End If

        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the KeyPress event of the txtName control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs" /> instance containing the event data.</param>
        Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
            If Asc(e.KeyChar) = Keys.Enter And btnCreate.Enabled Then CreateMacroAndClose()
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Sets the state of the drag.
        ''' </summary>
        Public Sub SetDragState()
            Thread.Sleep(750)

            Dragging = False
        End Sub

        ''' <summary>
        ''' Creates the macro and close.
        ''' </summary>
        Private Sub CreateMacroAndClose()
            Dim m As New Macro()
            m.Type = _typeInUse.Plugin
            m.Name = txtName.Text

            m.Outputs = New SerializableList(Of Plugin(Of IOutput))
            For Each mc In _outputsInUse
                m.Outputs.Add(mc.Plugin.ToOutputPlugin)
            Next

            m.Triggers = New SerializableList(Of Plugin(Of ITriggerManager))
            For Each mc In _triggersInUse
                m.Triggers.Add(mc.Plugin.ToTriggerPlugin)
            Next

            m.Effects = New SerializableList(Of Plugin(Of IEffect))
            For Each mc In _effectsInUse
                m.Effects.Add(mc.Plugin.ToEffectPlugin)
            Next

            If _delayInUse IsNot Nothing Then
                m.Delay = _delayInUse.Delay
            End If

            If _multipleInUse IsNot Nothing Then
                m.Multiple = _multipleInUse.MultipleInfos
            End If

            Me.Macro = m

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        ''' <summary>
        ''' Refreshes the tool box.
        ''' </summary>
        Private Sub RefreshToolBox()
            For Each t In _typesAvailable
                If _typeInUse IsNot Nothing Then
                    t.Visible = Not t.Name = _typeInUse.Name
                Else
                    t.Visible = True
                End If
            Next

            Dim triggersInUseNames As New List(Of String)

            For Each t In _triggersInUse
                If Not triggersInUseNames.Contains(t.Name) Then
                    triggersInUseNames.Add(t.Name)
                End If
            Next

            For Each t In _triggersAvailable
                t.Visible = Not triggersInUseNames.Contains(t.Name) Or t.MoreThanOneOccurenceAllowed
            Next

            Dim effectsInUseNames As New List(Of String)

            For Each t In _effectsInUse
                If Not effectsInUseNames.Contains(t.Name) Then
                    effectsInUseNames.Add(t.Name)
                End If
            Next

            For Each t In _effectsAvailable
                t.Visible = (Not effectsInUseNames.Contains(t.Name)) Or t.MoreThanOneOccurenceAllowed
            Next

            Dim outputsInUseNames As New List(Of String)

            For Each t In _outputsInUse
                If Not outputsInUseNames.Contains(t.Name) Then
                    outputsInUseNames.Add(t.Name)
                End If
            Next

            For Each t In _outputsAvailable
                t.Visible = (Not outputsInUseNames.Contains(t.Name)) Or t.MoreThanOneOccurenceAllowed
            Next

            ArrangeComponents()
        End Sub

        ''' <summary>
        ''' Deletes the component from macro.
        ''' </summary>
        ''' <param name="component">The component.</param>
        Public Sub DeleteComponentFromMacro(ByVal component As MacroComponent)
            If component IsNot Nothing Then
                Select Case component.GetType
                    Case GetType(TriggerMacroComponent)
                        If _triggersInUse.Contains(CType(component, TriggerMacroComponent)) Then
                            component.Dispose()
                            _triggersInUse.Remove(CType(component, TriggerMacroComponent))
                            RefreshToolBox()
                        End If
                    Case GetType(OutputMacroComponent)
                        If _outputsInUse.Contains(CType(component, OutputMacroComponent)) Then
                            component.Dispose()
                            _outputsInUse.Remove(CType(component, OutputMacroComponent))
                            RefreshToolBox()
                        End If
                    Case GetType(EffectMacroComponent)
                        If _effectsInUse.Contains(CType(component, EffectMacroComponent)) Then
                            component.Dispose()
                            _effectsInUse.Remove(CType(component, EffectMacroComponent))
                            RefreshToolBox()
                        End If
                    Case GetType(TypeMacroComponent)
                        If _typeInUse IsNot Nothing Then
                            _typeInUse.Dispose()
                            _typeInUse = Nothing
                            RefreshToolBox()
                        End If
                    Case GetType(MultipleMacroComponent)
                        If _multipleInUse IsNot Nothing Then
                            _multipleInUse = Nothing
                            RefreshToolBox()
                        End If
                    Case GetType(DelayMacroComponent)
                        If _delayInUse IsNot Nothing Then
                            _delayInUse = Nothing
                            RefreshToolBox()
                        End If
                End Select
            End If
        End Sub

        ''' <summary>
        ''' Refreshes the tool box elements.
        ''' </summary>
        Private Sub Reset()
            'The disabled Component
            _typeDisabledComponent.DisabledThing = "Type"
            _typeDisabledComponent.InUse = True
            _typeDisabledComponent.MoreThanOneOccurenceAllowed = False

            'Delay
            _delayComponent.Delay = 0
            _delayInUse = Nothing

            'Multiple
            _multipleComponent.MultipleInfos = New MultipleParameters(1, 1)
            _multipleInUse = Nothing

            'Refresh the lists
            _triggersAvailable.Clear()
            _triggersInUse.Clear()
            For Each t In _triggerDatabase
                Dim triggerControl As New TriggerMacroComponent(Me, t.Name)
                triggerControl.Plugin = t
                triggerControl.MoreThanOneOccurenceAllowed = t.MultiTimesAllowed

                _triggersAvailable.Add(triggerControl)
            Next

            _effectsAvailable.Clear()
            _effectsInUse.Clear()
            For Each t In _effectDatabase
                Dim effectControl As New EffectMacroComponent(Me, t.Name)
                effectControl.Plugin = t
                effectControl.MoreThanOneOccurenceAllowed = t.MultiTimesAllowed

                _effectsAvailable.Add(effectControl)
            Next

            _typesAvailable.Clear()
            _typeInUse = Nothing
            For Each t In _typeDatabase
                Dim typeControls As New TypeMacroComponent(Me, t.Name)
                typeControls.Plugin = t
                typeControls.MoreThanOneOccurenceAllowed = False

                _typesAvailable.Add(typeControls)
            Next

            _outputsAvailable.Clear()
            _outputsInUse.Clear()
            For Each t In _outputDatabase
                Dim outputControl As New OutputMacroComponent(Me, t.Name)
                outputControl.Plugin = t.ToArgumentPlugin
                outputControl.MoreThanOneOccurenceAllowed = t.MultiTimesAllowed

                _outputsAvailable.Add(outputControl)
            Next

            ArrangeComponents()
        End Sub

        ''' <summary>
        ''' Arranges the components.
        ''' </summary>
        Private Sub ArrangeComponents()
            'Toolbox
            Dim i As Integer = 0

            If _delayInUse Is Nothing Then
                i = _delayComponent.PutInToolbox(New Size(_buttonWidth, _buttonHeight), i, New Point(3, 3), 3)
            End If

            If _multipleInUse Is Nothing Then
                i = _multipleComponent.PutInToolbox(New Size(_buttonWidth, _buttonHeight), i, New Point(3, 3), 3)
            End If

            For Each t In _triggersAvailable
                i = t.PutInToolbox(New Size(_buttonWidth, _buttonHeight), i, New Point(3, 3), 3)
            Next

            For Each t In _typesAvailable
                i = t.PutInToolbox(New Size(_buttonWidth, _buttonHeight), i, New Point(3, 3), 3)
            Next
            For Each t In _effectsAvailable
                i = t.PutInToolbox(New Size(_buttonWidth, _buttonHeight), i, New Point(3, 3), 3)
            Next
            For Each t In _outputsAvailable
                i = t.PutInToolbox(New Size(_buttonWidth, _buttonHeight), i, New Point(3, 3), 3)
            Next

            Dim showTriggers As Boolean = _triggersInUse.Count > 0
            Dim showEffects As Boolean = _effectsInUse.Count > 0
            Dim showOutputs As Boolean = _outputsInUse.Count > 0
            Dim showDelay As Boolean = _delayInUse IsNot Nothing

            'How many columns?
            '(Always at least one, either the type is displayed or a filler control)
            'CInt(True) = -1 ; CInt(False) = 0
            Dim columnCount As Integer = Math.Abs(-1 + CInt(showTriggers) + CInt(showEffects) + CInt(showOutputs) + CInt(showDelay))
            Dim currentColumn As Integer = 0

            Dim marginWanted As Integer = 15
            Dim layoutWidth As Integer = columnCount * (_buttonWidth + marginWanted) - marginWanted

            Dim areaStart As New Point(_buttonWidth + 10, 3)
            Dim areaSize As New Size(Me.ClientRectangle.Width - areaStart.X, Me.ClientRectangle.Height - areaStart.Y - (Me.ClientRectangle.Height - panelBtns.Top))
            _layoutArea = New Rectangle(New Point(CInt(areaStart.X + areaSize.Width / 2 - layoutWidth / 2), areaStart.Y), New Size(layoutWidth, areaSize.Height))

            'Triggers
            If showTriggers Then
                For ii As Integer = 0 To _triggersInUse.Count - 1
                    _triggersInUse(ii).Arrange(_layoutArea, New Size(_buttonWidth, _buttonHeightExpanded), currentColumn, columnCount, ii, _triggersInUse.Count)
                Next

                currentColumn += 1
            End If

            'Delay 
            If showDelay Then
                _delayComponent.Arrange(_layoutArea, New Size(_buttonWidth, _buttonHeightExpanded), currentColumn, columnCount, 0, 1)
                currentColumn += 1
            End If

            'Type
            Dim typeControl As MacroComponent = Nothing

            If _typeInUse Is Nothing Then
                typeControl = _typeDisabledComponent
                _typeDisabledComponent.Visible = True
            Else
                typeControl = _typeInUse
                _typeDisabledComponent.Visible = False
            End If

            typeControl.Arrange(_layoutArea, New Size(_buttonWidth, _buttonHeightExpanded), currentColumn, columnCount, 0, 1)
            currentColumn += 1

            'Effects
            If showEffects Then
                For ii As Integer = 0 To _effectsInUse.Count - 1
                    _effectsInUse(ii).Arrange(_layoutArea, New Size(_buttonWidth, _buttonHeightExpanded), currentColumn, columnCount, ii, _effectsInUse.Count)
                Next

                currentColumn += 1
            End If

            'Outputs
            If showOutputs Then
                For ii As Integer = 0 To _outputsInUse.Count - 1
                    _outputsInUse(ii).Arrange(_layoutArea, New Size(_buttonWidth, _buttonHeightExpanded), currentColumn, columnCount, ii, _outputsInUse.Count)
                Next

                currentColumn += 1
            End If

            'Multiple
            If _multipleInUse IsNot Nothing Then
                'Bottom + Center
                _multipleInUse.Arrange(_layoutArea, New Size(_buttonWidth, _buttonHeightExpanded), currentColumn, columnCount, 1, _outputsInUse.Count)
                _multipleInUse.Size = New Size(_buttonWidth, _buttonHeightExpanded)
                _multipleInUse.Location = New Point(typeControl.Left, CInt(_layoutArea.Top + _layoutArea.Height - _buttonHeightExpanded * 1.2))
            End If

            Me.Refresh()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            ' if this is a click
            ' ...and it is on the client
            If m.Msg = &H84 And m.Result.ToInt32() = 1 Then
                'Check if the mouse is over the form area
                Dim x As Integer = (m.LParam.ToInt32 << 16) >> 16
                'lo order word
                Dim y As Integer = m.LParam.ToInt32 >> 16
                'hi order word

                Dim point As Point = Me.PointToClient(New Point(x, y))

                'Check if this is on a component
                Dim onControl As Boolean = panelBtns.ClientRectangle.Contains(point)

                onControl = _delayComponent.IsOnPoint(point, onControl)
                onControl = If(_delayInUse Is Nothing, onControl, _delayInUse.IsOnPoint(point, onControl))

                onControl = _multipleComponent.IsOnPoint(point, onControl)
                onControl = If(_multipleInUse Is Nothing, onControl, _multipleInUse.IsOnPoint(point, onControl))

                onControl = _typeDisabledComponent.IsOnPoint(point, onControl)
                onControl = If(_typeInUse Is Nothing, onControl, _typeInUse.IsOnPoint(point, onControl))

                For Each t In _triggersAvailable
                    onControl = t.IsOnPoint(point, onControl)
                Next

                For Each t In _triggersInUse
                    onControl = t.IsOnPoint(point, onControl)
                Next

                For Each t In _typesAvailable
                    onControl = t.IsOnPoint(point, onControl)
                Next

                For Each t In _outputsAvailable
                    onControl = t.IsOnPoint(point, onControl)
                Next

                For Each t In _outputsInUse
                    onControl = t.IsOnPoint(point, onControl)
                Next

                For Each t In _effectsAvailable
                    onControl = t.IsOnPoint(point, onControl)
                Next

                For Each t In _effectsInUse
                    onControl = t.IsOnPoint(point, onControl)
                Next

                If Not onControl Then
                    ' ...and specifically in the glass area
                    ' lie and say they clicked on the title bar
                    m.Result = New IntPtr(2)
                End If
            End If
        End Sub

#End Region
    End Class
End Namespace
