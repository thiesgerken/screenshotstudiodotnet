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

Imports System.Drawing
Imports System.Windows.Forms
Imports ScreenshotStudioDotNet.Core.Settings

Namespace Selector
    Public Class Input
        Private _oldBounds As Rectangle
        Private _maxBounds As Rectangle
        Private _updateFunction As UpdateRegionPropertiesDelegate
        Private _creating As Boolean

        Public Delegate Sub UpdateRegionPropertiesDelegate(ByVal startPoint As Point, ByVal endPoint As Point)

        ''' <summary>
        ''' Shows the dialog.
        ''' </summary>
        ''' <param name="title">The title.</param>
        ''' <param name="icon">The icon.</param>
        ''' <param name="adjustSize">if set to <c>true</c> [adjust size].</param>
        ''' <param name="adjustLocation">if set to <c>true</c> [adjust location].</param>
        ''' <param name="updateFunction">The update function.</param>
        ''' <param name="currentBounds">The current bounds.</param>
        ''' <param name="maxBounds">The max bounds.</param>
        ''' <param name="showRadioBoxes">if set to <c>true</c> [show radio boxes].</param>
        ''' <returns></returns>
        Public Overloads Function ShowDialog(ByVal title As String, ByVal icon As Icon, ByVal adjustSize As Boolean, ByVal adjustLocation As Boolean, ByVal updateFunction As UpdateRegionPropertiesDelegate, ByVal currentBounds As Rectangle, ByVal maxBounds As Rectangle, ByVal showRadioBoxes As Boolean) As DialogResult
            Me.Text = title
            Me.Icon = icon

            _updateFunction = updateFunction

            boxLocation.Visible = adjustLocation
            boxSize.Visible = adjustSize

            'Adjust the form's height when only 1 property should be changed
            If adjustLocation Xor adjustSize Then
                Me.Height -= 106
                'Height of one Box + Space
                pnlRadios.Top = boxSize.Top
                pnlButtons.Top = pnlRadios.Bottom + 6
                boxSize.Location = boxLocation.Location
            End If

            If Not showRadioBoxes Then
                'Hide the Radios
                pnlRadios.Visible = False
                pnlButtons.Top -= pnlRadios.Height + 6
                Me.Height -= pnlRadios.Height + 6
            End If

            If currentBounds.Location = New Point(1, 1) And currentBounds.Size = New Size(1, 1) Then
                _oldBounds = New Rectangle(0, 0, 0, 0)
                _creating = True
            Else
                _oldBounds = currentBounds
            End If

            _maxBounds = maxBounds

            'Read the input mode from the settings, except when the radios aren't visible.
            If SettingsDatabase.InputMode = "Absolute" Or Not pnlRadios.Visible Then
                optAbsolute.Checked = True
            Else
                optRelative.Checked = True
            End If

            UpdateProperties(True)

            Return ShowDialog()
        End Function

        ''' <summary>
        ''' Updates the properties.
        ''' </summary>
        Private Overloads Sub UpdateProperties()
            UpdateProperties(False)
        End Sub

        Private _updating As Boolean

        ''' <summary>
        ''' Updates the properties.
        ''' </summary>
        ''' <param name="firstRun">if set to <c>true</c> [first run].</param>
        Private Overloads Sub UpdateProperties(ByVal firstRun As Boolean)
            If _updating Then Exit Sub

            _updating = True

            If firstRun Then
                If optAbsolute.Checked Then
                    'Absolute Values
                    numX.Minimum = 1
                    numX.Maximum = _maxBounds.Right - 1
                    numX.Value = GetValueAboveZero(_oldBounds.Left)

                    numY.Minimum = 1
                    numY.Maximum = _maxBounds.Bottom - 1
                    numY.Value = GetValueAboveZero(_oldBounds.Top)

                    numWidth.Minimum = 1
                    numWidth.Maximum = _maxBounds.Width
                    numWidth.Value = GetValueAboveZero(_oldBounds.Width)

                    numHeight.Minimum = 1
                    numHeight.Maximum = _maxBounds.Height
                    numHeight.Value = GetValueAboveZero(_oldBounds.Height)
                Else
                    'Relative Values 

                    numX.Minimum = -(_oldBounds.Left - _maxBounds.Left)
                    numX.Maximum = _maxBounds.Right - _oldBounds.Left
                    numX.Value = 0

                    numY.Minimum = -(_oldBounds.Top - _maxBounds.Top)
                    numY.Maximum = _maxBounds.Bottom - _oldBounds.Top
                    numY.Value = 0

                    numWidth.Minimum = 1 - _oldBounds.Width
                    numWidth.Maximum = _maxBounds.Width - _oldBounds.Width
                    numWidth.Value = 0

                    numHeight.Minimum = 1 - _oldBounds.Height
                    numHeight.Maximum = _maxBounds.Height - _oldBounds.Height
                    numHeight.Value = 0
                End If
            Else
                If optAbsolute.Checked Then
                    'Relative -> Absolute
                    Dim relX, relY, relWidth, relHeight As Integer

                    relX = CInt(numX.Value)
                    relY = CInt(numY.Value)
                    relWidth = CInt(numWidth.Value)
                    relHeight = CInt(numHeight.Value)

                    numX.Minimum = 1
                    numX.Maximum = _maxBounds.Right - 1
                    numX.Value = GetValueAboveZero(_oldBounds.Left + relX)

                    numY.Minimum = 1
                    numY.Maximum = _maxBounds.Bottom - 1
                    numY.Value = GetValueAboveZero(_oldBounds.Top + relY)

                    numWidth.Minimum = 1
                    numWidth.Maximum = _maxBounds.Width
                    numWidth.Value = GetValueAboveZero(_oldBounds.Width + relWidth)

                    numHeight.Minimum = 1
                    numHeight.Maximum = _maxBounds.Height
                    numHeight.Value = GetValueAboveZero(_oldBounds.Height + relHeight)
                Else
                    'Absolute -> Relative
                    Dim absX, absY, absWidth, absHeight As Integer

                    absX = CInt(numX.Value)
                    absY = CInt(numY.Value)
                    absWidth = CInt(numWidth.Value)
                    absHeight = CInt(numHeight.Value)


                    numX.Minimum = -(_oldBounds.Left - _maxBounds.Left)
                    numX.Maximum = _maxBounds.Right - _oldBounds.Left
                    numX.Value = absX - _oldBounds.Left

                    numY.Minimum = -(_oldBounds.Top - _maxBounds.Top)
                    numY.Maximum = _maxBounds.Bottom - _oldBounds.Top
                    numY.Value = absY - _oldBounds.Top

                    numWidth.Minimum = 1 - _oldBounds.Width
                    numWidth.Maximum = _maxBounds.Width - _oldBounds.Width
                    numWidth.Value = absWidth - _oldBounds.Width

                    numHeight.Minimum = 1 - _oldBounds.Height
                    numHeight.Maximum = _maxBounds.Height - _oldBounds.Height
                    numHeight.Value = absHeight - _oldBounds.Height
                End If
            End If

            _updating = False
        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the optAbsolute control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub optAbsolute_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optAbsolute.CheckedChanged
            UpdateProperties()
        End Sub


        ''' <summary>
        ''' Sends the update.
        ''' </summary>
        Private Sub SendUpdate()
            If _updating Then Exit Sub

            Dim startPoint As Point
            Dim endPoint As Point

            If optAbsolute.Checked Then
                startPoint = New Point(CInt(numX.Value), CInt(numY.Value))
                endPoint = New Point(CInt(numX.Value + numWidth.Value), CInt(numY.Value + numHeight.Value))
            Else
                startPoint = New Point(CInt(_oldBounds.X + numX.Value), CInt(_oldBounds.Y + numY.Value))
                endPoint = New Point(CInt(_oldBounds.Right + numX.Value + numWidth.Value), CInt(_oldBounds.Bottom + numY.Value + numHeight.Value))
            End If

            _updateFunction.Invoke(startPoint, endPoint)
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numX control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numX_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numX.ValueChanged
            SendUpdate()
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numY control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numY_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numY.ValueChanged
            SendUpdate()
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numWidth control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numWidth_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numWidth.ValueChanged
            SendUpdate()
        End Sub

        ''' <summary>
        ''' Handles the ValueChanged event of the numHeight control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub numHeight_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numHeight.ValueChanged
            SendUpdate()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnCancel control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            'Update with the old bounds
            Dim startPoint As Point = _oldBounds.Location
            Dim endPoint As Point = New Point(_oldBounds.Right, _oldBounds.Bottom)

            _updateFunction.Invoke(startPoint, endPoint)

            'Close
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        ''' <summary>
        ''' Handles the Click event of the btnOK control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
            'Just close, no updates
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        ''' <summary>
        ''' Gets the value above zero.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        Public Function GetValueAboveZero(ByVal value As Integer) As Integer
            If value <= 0 Then Return 1
            Return value
        End Function
    End Class
End Namespace
