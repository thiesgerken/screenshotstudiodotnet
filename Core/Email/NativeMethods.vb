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

Imports System.Runtime.InteropServices

Namespace Email
    ''' <summary>
    ''' Internal class for calling MAPI APIs
    ''' </summary>
    Friend Class NativeMethods

#Region "Private Constructor"

        ''' <summary>
        ''' Private constructor.
        ''' </summary>
        Private Sub New()
            ' Intenationally blank
        End Sub

#End Region

#Region "Constants"

        Public Shared MAPI_LOGON_UI As Integer = &H1

#End Region

#Region "Function Imports"

        <DllImport("MAPI32.DLL")> _
        Public Shared Function MAPISendMail(ByVal session As IntPtr, ByVal hwnd As IntPtr, ByVal message As MapiMessage, ByVal flg As Integer, ByVal rsv As Integer) As Integer
        End Function

        <DllImport("MAPI32.DLL", CharSet:=CharSet.Ansi)> _
        Public Shared Function MAPILogon(ByVal hwnd As IntPtr, ByVal prf As String, ByVal pw As String, ByVal flg As Integer, ByVal rsv As Integer, ByRef sess As IntPtr) As Integer
        End Function

#End Region

#Region "Structures"

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
        Public Class MapiMessage
            Public Reserved As Integer = 0
            Public Subject As String = Nothing
            Public NoteText As String = Nothing
            Public MessageType As String = Nothing
            Public DateReceived As String = Nothing
            Public ConversationID As String = Nothing
            Public Flags As Integer = 0
            Public Originator As IntPtr = IntPtr.Zero
            Public RecipientCount As Integer = 0
            Public Recipients As IntPtr = IntPtr.Zero
            Public FileCount As Integer = 0
            Public Files As IntPtr = IntPtr.Zero
        End Class

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
        Public Class MapiRecipDesc
            Public Reserved As Integer = 0
            Public RecipientClass As Integer = 0
            Public Name As String = Nothing
            Public Address As String = Nothing
            Public eIDSize As Integer = 0
            Public EntryID As IntPtr = IntPtr.Zero
        End Class

#End Region
    End Class
End Namespace
