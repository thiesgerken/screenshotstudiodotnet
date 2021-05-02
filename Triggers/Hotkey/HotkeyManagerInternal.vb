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
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Hotkey
    Public Class HotkeyManagerInternal
        Implements IMessageFilter, IDisposable

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the hotkeys.
        '''</summary>
        ''' <value>The hotkeys.</value>
        Public Property Hotkeys() As New Dictionary(Of Hotkey, Integer)

        ''' <summary>
        ''' Gets or sets the owner form.
        ''' </summary>
        ''' <value>The owner form.</value>
        Public Property OwnerForm As Form

#End Region

#Region "Functions"

        ''' <summary>
        ''' Adds the hot key.
        ''' </summary>
        ''' <param name="keyCode">The key code.</param>
        ''' <param name="modifiers">The modifiers.</param>
        ''' <param name="Action">The action.</param>
        Public Sub AddHotKey(ByVal keyCode As Keys, ByVal modifiers As ModifierKeys, ByVal register As Boolean)
            For Each h In Hotkeys
                If h.Key.HotKey = keyCode And h.Key.Modifier = modifiers Then
                    Log.LogWarning("Tried to Add a hotkey with an invalid id")
                    Exit Sub
                End If
            Next

            Dim ID As Integer = NativeMethods.GlobalAddAtom(keyCode.ToString & modifiers.ToString)
            Hotkeys.Add(New Hotkey(keyCode, modifiers), ID)

            If register Then NativeMethods.RegisterHotKey(OwnerForm.Handle, ID, modifiers, keyCode)
        End Sub

        ''' <summary>
        ''' Adds the hotkey.
        ''' </summary>
        ''' <param name="hotkey">The hotkey.</param>
        Public Sub AddHotkey(ByVal hotkey As Hotkey)
            AddHotKey(hotkey.HotKey, hotkey.Modifier, True)
        End Sub

        ''' <summary>
        ''' Removes the hot key.
        ''' </summary>
        ''' <param name="modifiers">The modifiers.</param>
        ''' <param name="keycode">The keycode.</param>
        Public Sub RemoveHotKey(ByVal modifiers As ModifierKeys, ByVal keycode As Keys)
            Try
                Dim ID As Integer = _Hotkeys(New Hotkey(keycode, modifiers))
                _Hotkeys.Remove(New Hotkey(keycode, modifiers))
                Hotkeys.Remove(New Hotkey(keycode, modifiers))
                NativeMethods.UnregisterHotKey(OwnerForm.Handle, ID)
                NativeMethods.GlobalDeleteAtom(ID)
            Catch ex As Exception
                Log.LogWarning("Error removing Hotkey")
                Log.LogError(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Removes all hot keys.
        ''' </summary>
        Public Sub RemoveAllHotKeys()
            'copy the list
            Dim copiedList As New List(Of Hotkey)
            For Each h In Hotkeys
                copiedList.Add(h.Key)
            Next

            For Each h In copiedList
                RemoveHotKey(h.Modifier, h.HotKey)
            Next
        End Sub

        ''' <summary>
        ''' Filters out a message before it is dispatched.
        ''' </summary>
        ''' <param name="m">The message to be dispatched. You cannot modify this message.</param>
        ''' <returns>
        ''' true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.
        ''' </returns>
        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            If m.Msg = NativeMethods.WM_HOTKEY Then
                Try
                    For Each h In Hotkeys
                        If h.Value = m.WParam.ToInt32 Then
                            OnHotkeyPressed(h.Key)
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                End Try
            End If
            Return False
        End Function

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="HotkeyDatabase" /> class.
        ''' </summary>
        ''' <param name="form">The form.</param>
        Public Sub New(ByVal ownerForm As Form)
            'Set the owner Form
            _OwnerForm = ownerForm
            Application.AddMessageFilter(Me)
        End Sub

#End Region

#Region "Events"

        Public Event HotkeyPressed(ByVal sender As Object, ByVal e As HotkeyPressedEventArgs)

        ''' <summary>
        ''' Called when [hotkey pressed].
        ''' </summary>
        ''' <param name="macroName">Name of the macro.</param>
        Private Sub OnHotkeyPressed(ByVal hotkey As Hotkey)
            RaiseEvent HotkeyPressed(Me, New HotkeyPressedEventArgs(hotkey))
        End Sub

#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources
        ''' </summary>
        ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    For Each h In Hotkeys
                        Try
                            NativeMethods.UnregisterHotKey(OwnerForm.Handle, h.Value)
                            NativeMethods.GlobalDeleteAtom(h.Value)
                        Catch ex As Exception
                        End Try
                    Next

                    Try
                        Application.RemoveMessageFilter(Me)
                    Catch ex As Exception
                    End Try
                End If
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
