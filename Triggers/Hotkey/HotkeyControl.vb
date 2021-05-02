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

Imports System.Windows.Forms

Namespace Hotkey
    ''' <summary>
    ''' A simple control that allows the user to select pretty much any valid hotkey combination
    ''' </summary>
    Public Class HotkeyControl
        Inherits TextBox

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the hotkey.
        ''' </summary>
        ''' <value>The hotkey.</value>
        Public Property Hotkey As Hotkey
            Get
                Return _hotkey
            End Get
            Set(ByVal value As Hotkey)
                _hotkey = value
                Redraw(True)
            End Set
        End Property

        ''' <summary>
        ''' Used to make sure that there is no right-click menu available
        ''' </summary>
        ''' <value></value>
        ''' <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> that represents the shortcut menu associated with the control.</returns>
        Public Overloads Overrides Property ContextMenu() As ContextMenu
            Get
                Return dummy
            End Get
            Set(ByVal value As ContextMenu)
                MyBase.ContextMenu = dummy
            End Set
        End Property

        ''' <summary>
        ''' Forces the control to be non-multiline
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if the control is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control; otherwise, false. The default is false.</returns>
        ''' <PermissionSet>
        ''' <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ''' <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ''' <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ''' <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ''' </PermissionSet>
        Public Overloads Overrides Property Multiline() As Boolean
            Get
                Return False
            End Get
            Set(ByVal value As Boolean)
                ' Ignore what the user wants; force Multiline to false
                MyBase.Multiline = False
            End Set
        End Property

#End Region

        ' ArrayLists used to enforce the use of proper modifiers.
        ' Shift+A isn't a valid hotkey, for instance, as it would screw up when the user is typing.

#Region "Fields"

        Private needNonShiftModifier As ArrayList = Nothing
        Private needNonAltGrModifier As ArrayList = Nothing
        Private _hotkey As New Hotkey
        Private dummy As New ContextMenu()

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Creates a new HotkeyControl
        ''' </summary>
        Public Sub New()
            Me.ContextMenu = dummy
            ' Disable right-clicking
            Me.Text = "None"

            ' Handle events that occurs when keys are pressed

            ' Fill the ArrayLists that contain all invalid hotkey combinations
            needNonShiftModifier = New ArrayList()
            needNonAltGrModifier = New ArrayList()
            PopulateModifierLists()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Populates the ArrayLists specifying disallowed hotkeys
        ''' such as Shift+A, Ctrl+Alt+4 (would produce a dollar sign) etc
        ''' </summary>
        Private Sub PopulateModifierLists()
            For k As Keys = Keys.D0 To Keys.Z
                needNonShiftModifier.Add(CInt(k))
            Next
            For k As Keys = Keys.NumPad0 To Keys.NumPad9
                needNonShiftModifier.Add(CInt(k))
            Next
            For k As Keys = Keys.Oem1 To Keys.OemBackslash
                needNonShiftModifier.Add(CInt(k))
            Next
            For k As Keys = Keys.Space To Keys.Home
                needNonShiftModifier.Add(CInt(k))
            Next
            ' Shift + 0 - 9, A - Z

            ' Shift + Numpad keys

            ' Shift + Misc (,;<./ etc)

            ' Shift + Space, PgUp, PgDn, End, Home

            ' Misc keys that we can't loop through
            needNonShiftModifier.Add(CInt(Keys.Insert))
            needNonShiftModifier.Add(CInt(Keys.Help))
            needNonShiftModifier.Add(CInt(Keys.Multiply))
            needNonShiftModifier.Add(CInt(Keys.Add))
            needNonShiftModifier.Add(CInt(Keys.Subtract))
            needNonShiftModifier.Add(CInt(Keys.Divide))
            needNonShiftModifier.Add(CInt(Keys.[Decimal]))
            needNonShiftModifier.Add(CInt(Keys.[Return]))
            needNonShiftModifier.Add(CInt(Keys.Escape))
            needNonShiftModifier.Add(CInt(Keys.NumLock))
            needNonShiftModifier.Add(CInt(Keys.Scroll))
            needNonShiftModifier.Add(CInt(Keys.Pause))
            For k As Keys = Keys.D0 To Keys.D9
                needNonAltGrModifier.Add(CInt(k))
            Next

            ' Ctrl+Alt + 0 - 9
        End Sub

        ''' <summary>
        ''' Resets this hotkey control to None
        ''' </summary>
        Public Overloads Sub Clear()
            Me.Hotkey = New Hotkey(Keys.None, Triggers.Hotkey.ModifierKeys.None)
        End Sub

        ''' <summary>
        ''' Handles some misc keys, such as Ctrl+Delete and Shift+Insert
        ''' </summary>
        ''' <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
        ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process.</param>
        ''' <returns>
        ''' true if the shortcut key was processed by the control; otherwise, false.
        ''' </returns>
        Protected Overloads Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If keyData = Keys.Delete OrElse keyData = (Keys.Control Or Keys.Delete) Then
                ResetHotkey()
                Return True
            End If

            If keyData = (Keys.Shift Or Keys.Insert) Then
                Return True
                ' Paste
            End If
            ' Don't allow
            ' Allow the rest
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function

        ''' <summary>
        ''' Clears the current hotkey and resets the TextBox
        ''' </summary>
        Public Sub ResetHotkey()
            Me.Hotkey = New Hotkey(Keys.None, Triggers.Hotkey.ModifierKeys.None)

            Redraw()
        End Sub

        ''' <summary>
        ''' Helper function
        ''' </summary>
        Private Sub Redraw()
            Redraw(False)
        End Sub

        ''' <summary>
        ''' Redraws the TextBox when necessary.
        ''' </summary>
        ''' <param name="bCalledProgramatically">Specifies whether this function was called by the Hotkey/HotkeyModifiers properties or by the user.</param>
        Private Sub Redraw(ByVal bCalledProgramatically As Boolean)
            ' No hotkey set
            If Me.Hotkey.HotKey = Keys.None Then
                Me.Text = "None"
                Return
            End If

            ' LWin/RWin doesn't work as hotkeys (neither do they work as modifier keys in .NET 2.0)
            If Me.Hotkey.HotKey = Keys.LWin OrElse Me.Hotkey.HotKey = Keys.RWin Then
                Me.Text = "None"
                Return
            End If

            ' Only validate input if it comes from the user
            If bCalledProgramatically = False Then
                ' No modifier or shift only, AND a hotkey that needs another modifier
                If _
                    (Me.Hotkey.Modifier = Keys.Shift OrElse Me.Hotkey.Modifier = Keys.None) AndAlso _
                    Me.needNonShiftModifier.Contains(CInt(Me.Hotkey.HotKey)) Then
                    If Me.Hotkey.Modifier = Keys.None Then
                        ' Set Ctrl+Alt as the modifier unless Ctrl+Alt+<key> won't work...
                        If needNonAltGrModifier.Contains(CInt(Me.Hotkey.HotKey)) = False Then
                            Me.Hotkey.Modifier = Triggers.Hotkey.ModifierKeys.Alt Or Triggers.Hotkey.ModifierKeys.Control
                        Else
                            Me.Hotkey.Modifier = Triggers.Hotkey.ModifierKeys.Alt Or Triggers.Hotkey.ModifierKeys.Shift
                            ' ... in that case, use Shift+Alt instead.
                        End If
                    Else
                        ' User pressed Shift and an invalid key (e.g. a letter or a number),
                        ' that needs another set of modifier keys
                        Me.Hotkey.HotKey = Keys.None
                        Me.Text = Me.Hotkey.Modifier.ToString() + " + Invalid key"
                        Return
                    End If
                End If
                ' Check all Ctrl+Alt keys
                If (Me.Hotkey.Modifier = (Triggers.Hotkey.ModifierKeys.Alt Or Triggers.Hotkey.ModifierKeys.Control)) AndAlso Me.needNonAltGrModifier.Contains(CInt(Me.Hotkey.HotKey)) _
                    Then
                    ' Ctrl+Alt+4 etc won't work; reset hotkey and tell the user
                    Me.Hotkey.HotKey = Keys.None
                    Me.Text = Me.Hotkey.Modifier.ToString() + " + Invalid key"
                    Return
                End If
            End If

            If Me.Hotkey.Modifier = Triggers.Hotkey.ModifierKeys.None Then
                If Me.Hotkey.HotKey = Keys.None Then
                    Me.Text = "None"
                    Return
                Else
                    ' We get here if we've got a hotkey that is valid without a modifier,
                    ' like F1-F12, Media-keys etc.
                    Me.Text = Me.Hotkey.HotKey.ToString()
                    Return
                End If
            End If

            ' I have no idea why this is needed, but it is. Without this code, pressing only Ctrl
            ' will show up as "Control + ControlKey", etc.
            If Me.Hotkey.HotKey = Keys.Menu OrElse Me.Hotkey.HotKey = Keys.ShiftKey OrElse Me.Hotkey.HotKey = Keys.ControlKey Then
                Me.Hotkey.HotKey = Keys.None
                ' Alt
            End If

            Me.Text = Me.Hotkey.ToString()
        End Sub

        ''' <summary>
        ''' Keyses to modifier.
        ''' </summary>
        ''' <param name="keys">The keys.</param>
        ''' <returns></returns>
        Private Function KeysToModifier(ByVal keys As Keys) As ModifierKeys
            Dim output As ModifierKeys = 0

            If (keys And System.Windows.Forms.Keys.Control) = System.Windows.Forms.Keys.Control Then
                output = output Or Triggers.Hotkey.ModifierKeys.Control
            End If

            If (keys And System.Windows.Forms.Keys.Alt) = System.Windows.Forms.Keys.Alt Then
                output = output Or Triggers.Hotkey.ModifierKeys.Alt
            End If

            If (keys And System.Windows.Forms.Keys.Shift) = System.Windows.Forms.Keys.Shift Then
                output = output Or Triggers.Hotkey.ModifierKeys.Shift
            End If

            If ((keys And System.Windows.Forms.Keys.LWin) = System.Windows.Forms.Keys.LWin) Or ((keys And System.Windows.Forms.Keys.RWin) = System.Windows.Forms.Keys.RWin) Then
                output = output Or Triggers.Hotkey.ModifierKeys.Windows
            End If

            Return output
        End Function

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Fires when a key is pushed down. Here, we'll want to update the text in the box
        ''' to notify the user what combination is currently pressed.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        Private Sub HotkeyControl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            ' Clear the current hotkey
            If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete Then
                ResetHotkey()
                Return
            Else
                Me.Hotkey.Modifier = KeysToModifier(e.Modifiers)
                Me.Hotkey.HotKey = e.KeyCode
                Redraw()
            End If
        End Sub

        ''' <summary>
        ''' Fires when all keys are released. If the current hotkey isn't valid, reset it.
        ''' Otherwise, do nothing and keep the text and hotkey as it was.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        Private Sub HotkeyControl_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
            If Me.Hotkey.HotKey = Keys.None AndAlso ModifierKeys = Keys.None Then
                ResetHotkey()
                Return
            End If
        End Sub

        ''' <summary>
        ''' Prevents the letter/whatever entered to show up in the TextBox
        ''' Without this, a "A" key press would appear as "aControl, Alt + A"
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs" /> instance containing the event data.</param>
        Private Sub HotkeyControl_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Me.KeyPress
            e.Handled = True
        End Sub

#End Region
    End Class
End Namespace
