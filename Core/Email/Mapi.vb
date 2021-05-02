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
Imports System.Threading
Imports System.IO
Imports ScreenshotStudioDotNet.Core.Logging

Namespace Email

#Region "Public MapiMailMessage Class"


    ''' <summary>
    ''' Represents an email message to be sent through MAPI.
    ''' </summary>
        Public Class MapiMailMessage

#Region "Private MapiFileDescriptor Class"

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
        Private Class MapiFileDescriptor
            Public reserved As Integer = 0
            Public flags As Integer = 0
            Public position As Integer = 0
            Public path As String = Nothing
            Public name As String = Nothing
            Public type As IntPtr = IntPtr.Zero
        End Class

#End Region

#Region "Enums"

        ''' <summary>
        ''' Specifies the valid RecipientTypes for a Recipient.
        ''' </summary>
        Public Enum RecipientType As Integer
            ''' <summary>
            ''' Recipient will be in the TO list.
            ''' </summary>
            [To] = 1

            ''' <summary>
            ''' Recipient will be in the CC list.
            ''' </summary>
            CC = 2

            ''' <summary>
            ''' Recipient will be in the BCC list.
            ''' </summary>
            BCC = 3
        End Enum

#End Region

#Region "Member Variables"

        Private _subject As String
        Private _body As String
        Private _recipientCollection As RecipientCollection
        Private _files As ArrayList
        Private _manualResetEvent As ManualResetEvent

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Creates a blank mail message.
        ''' </summary>
        Public Sub New()
            _files = New ArrayList()
            _recipientCollection = New RecipientCollection()
            _manualResetEvent = New ManualResetEvent(False)
        End Sub

        ''' <summary>
        ''' Creates a new mail message with the specified subject.
        ''' </summary>
        Public Sub New(ByVal subject As String)
            Me.New()
            _subject = subject
        End Sub

        ''' <summary>
        ''' Creates a new mail message with the specified subject and body.
        ''' </summary>
        Public Sub New(ByVal subject As String, ByVal body As String)
            Me.New()
            _subject = subject
            _body = body
        End Sub

#End Region

#Region "Public Properties"

        ''' <summary>
        ''' Gets or sets the subject of this mail message.
        ''' </summary>
        Public Property Subject() As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the body of this mail message.
        ''' </summary>
        Public Property Body() As String
            Get
                Return _body
            End Get
            Set(ByVal value As String)
                _body = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the recipient list for this mail message.
        ''' </summary>
        Public ReadOnly Property Recipients() As RecipientCollection
            Get
                Return _recipientCollection
            End Get
        End Property

        ''' <summary>
        ''' Gets the file list for this mail message.
        ''' </summary>
        Public ReadOnly Property Files() As ArrayList
            Get
                Return _files
            End Get
        End Property

#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Displays the mail message dialog asynchronously.
        ''' </summary>
        Public Sub ShowDialog()
            ' Create the mail message in an STA thread
            Dim t As New Thread(New ThreadStart(AddressOf _ShowMail))
            t.IsBackground = True
            t.SetApartmentState(ApartmentState.STA)
            t.Start()

            ' only return when the new thread has built it's interop representation
            _manualResetEvent.WaitOne()
            _manualResetEvent.Reset()
        End Sub

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Sends the mail message.
        ''' </summary>
        Private Sub _ShowMail(ByVal ignore As Object)
            Dim message As New NativeMethods.MapiMessage()

            Using interopRecipients As RecipientCollection.InteropRecipientCollection = _recipientCollection.GetInteropRepresentation()

                message.Subject = _subject
                message.NoteText = _body

                message.Recipients = interopRecipients.Handle
                message.RecipientCount = _recipientCollection.Count

                ' Check if we need to add attachments
                If _files.Count > 0 Then
                    ' Add attachments
                    message.Files = _AllocAttachments(message.FileCount)
                End If

                ' Signal the creating thread (make the remaining code async)
                _manualResetEvent.[Set]()

                Const MAPI_DIALOG As Integer = &H8
                'const int MAPI_LOGON_UI = 0x1;
                Const SUCCESS_SUCCESS As Integer = 0
                Dim [error] As Integer = NativeMethods.MAPISendMail(IntPtr.Zero, IntPtr.Zero, message, MAPI_DIALOG, 0)

                If _files.Count > 0 Then
                    ' Deallocate the files
                    _DeallocFiles(message)
                End If

                ' Check for error
                If [error] <> SUCCESS_SUCCESS Then
                    _LogErrorMapi([error])
                End If
            End Using
        End Sub

        ''' <summary>
        ''' Deallocates the files in a message.
        ''' </summary>
        ''' <param name="message">The message to deallocate the files from.</param>
        Private Sub _DeallocFiles(ByVal message As NativeMethods.MapiMessage)
            If message.Files <> IntPtr.Zero Then
                Dim fileDescType As Type = GetType(MapiFileDescriptor)
                Dim fsize As Integer = Marshal.SizeOf(fileDescType)

                ' Get the ptr to the files
                Dim runptr As Integer = CInt(message.Files)
                ' Release each file
                For i As Integer = 0 To message.FileCount - 1
                    Marshal.DestroyStructure(CType(runptr, IntPtr), fileDescType)
                    runptr += fsize
                Next
                ' Release the file
                Marshal.FreeHGlobal(message.Files)
            End If
        End Sub

        ''' <summary>
        ''' Allocates the file attachments
        ''' </summary>
        ''' <param name="fileCount"></param>
        ''' <returns></returns>
        Private Function _AllocAttachments(ByRef fileCount As Integer) As IntPtr
            fileCount = 0
            If _files Is Nothing Then
                Return IntPtr.Zero
            End If
            If (_files.Count <= 0) OrElse (_files.Count > 100) Then
                Return IntPtr.Zero
            End If

            Dim atype As Type = GetType(MapiFileDescriptor)
            Dim asize As Integer = Marshal.SizeOf(atype)
            Dim ptra As IntPtr = Marshal.AllocHGlobal(_files.Count * asize)

            Dim mfd As New MapiFileDescriptor()
            mfd.position = -1
            Dim runptr As Integer = CInt(ptra)
            For i As Integer = 0 To _files.Count - 1
                Dim path__1 As String = TryCast(_files(i), String)
                mfd.name = Path.GetFileName(path__1)
                mfd.path = path__1
                Marshal.StructureToPtr(mfd, CType(runptr, IntPtr), False)
                runptr += asize
            Next

            fileCount = _files.Count
            Return ptra
        End Function

        ''' <summary>
        ''' Sends the mail message.
        ''' </summary>
        Private Sub _ShowMail()
            _ShowMail(Nothing)
        End Sub


        ''' <summary>
        ''' Logs any Mapi errors.
        ''' </summary>
        Private Sub _LogErrorMapi(ByVal errorCode As Integer)
            Const MAPI_USER_ABORT As Integer = 1
            Const MAPI_E_FAILURE As Integer = 2
            Const MAPI_E_LOGIN_FAILURE As Integer = 3
            Const MAPI_E_DISK_FULL As Integer = 4
            Const MAPI_E_INSUFFICIENT_MEMORY As Integer = 5
            Const MAPI_E_BLK_TOO_SMALL As Integer = 6
            Const MAPI_E_TOO_MANY_SESSIONS As Integer = 8
            Const MAPI_E_TOO_MANY_FILES As Integer = 9
            Const MAPI_E_TOO_MANY_RECIPIENTS As Integer = 10
            Const MAPI_E_ATTACHMENT_NOT_FOUND As Integer = 11
            Const MAPI_E_ATTACHMENT_OPEN_FAILURE As Integer = 12
            Const MAPI_E_ATTACHMENT_WRITE_FAILURE As Integer = 13
            Const MAPI_E_UNKNOWN_RECIPIENT As Integer = 14
            Const MAPI_E_BAD_RECIPTYPE As Integer = 15
            Const MAPI_E_NO_MESSAGES As Integer = 16
            Const MAPI_E_INVALID_MESSAGE As Integer = 17
            Const MAPI_E_TEXT_TOO_LARGE As Integer = 18
            Const MAPI_E_INVALID_SESSION As Integer = 19
            Const MAPI_E_TYPE_NOT_SUPPORTED As Integer = 20
            Const MAPI_E_AMBIGUOUS_RECIPIENT As Integer = 21
            Const MAPI_E_MESSAGE_IN_USE As Integer = 22
            Const MAPI_E_NETWORK_FAILURE As Integer = 23
            Const MAPI_E_INVALID_EDITFIELDS As Integer = 24
            Const MAPI_E_INVALID_RECIPS As Integer = 25
            Const MAPI_E_NOT_SUPPORTED As Integer = 26
            Const MAPI_E_NO_LIBRARY As Integer = 999
            Const MAPI_E_INVALID_PARAMETER As Integer = 998

            Dim [error] As String = String.Empty
            Select Case errorCode
                Case MAPI_USER_ABORT
                    [error] = "User Aborted."
                    Exit Select
                Case MAPI_E_FAILURE
                    [error] = "MAPI Failure."
                    Exit Select
                Case MAPI_E_LOGIN_FAILURE
                    [error] = "Login Failure."
                    Exit Select
                Case MAPI_E_DISK_FULL
                    [error] = "MAPI Disk full."
                    Exit Select
                Case MAPI_E_INSUFFICIENT_MEMORY
                    [error] = "MAPI Insufficient memory."
                    Exit Select
                Case MAPI_E_BLK_TOO_SMALL
                    [error] = "MAPI Block too small."
                    Exit Select
                Case MAPI_E_TOO_MANY_SESSIONS
                    [error] = "MAPI Too many sessions."
                    Exit Select
                Case MAPI_E_TOO_MANY_FILES
                    [error] = "MAPI too many files."
                    Exit Select
                Case MAPI_E_TOO_MANY_RECIPIENTS
                    [error] = "MAPI too many recipients."
                    Exit Select
                Case MAPI_E_ATTACHMENT_NOT_FOUND
                    [error] = "MAPI Attachment not found."
                    Exit Select
                Case MAPI_E_ATTACHMENT_OPEN_FAILURE
                    [error] = "MAPI Attachment open failure."
                    Exit Select
                Case MAPI_E_ATTACHMENT_WRITE_FAILURE
                    [error] = "MAPI Attachment Write Failure."
                    Exit Select
                Case MAPI_E_UNKNOWN_RECIPIENT
                    [error] = "MAPI Unknown recipient."
                    Exit Select
                Case MAPI_E_BAD_RECIPTYPE
                    [error] = "MAPI Bad recipient type."
                    Exit Select
                Case MAPI_E_NO_MESSAGES
                    [error] = "MAPI No messages."
                    Exit Select
                Case MAPI_E_INVALID_MESSAGE
                    [error] = "MAPI Invalid message."
                    Exit Select
                Case MAPI_E_TEXT_TOO_LARGE
                    [error] = "MAPI Text too large."
                    Exit Select
                Case MAPI_E_INVALID_SESSION
                    [error] = "MAPI Invalid session."
                    Exit Select
                Case MAPI_E_TYPE_NOT_SUPPORTED
                    [error] = "MAPI Type not supported."
                    Exit Select
                Case MAPI_E_AMBIGUOUS_RECIPIENT
                    [error] = "MAPI Ambiguous recipient."
                    Exit Select
                Case MAPI_E_MESSAGE_IN_USE
                    [error] = "MAPI Message in use."
                    Exit Select
                Case MAPI_E_NETWORK_FAILURE
                    [error] = "MAPI Network failure."
                    Exit Select
                Case MAPI_E_INVALID_EDITFIELDS
                    [error] = "MAPI Invalid edit fields."
                    Exit Select
                Case MAPI_E_INVALID_RECIPS
                    [error] = "MAPI Invalid Recipients."
                    Exit Select
                Case MAPI_E_NOT_SUPPORTED
                    [error] = "MAPI Not supported."
                    Exit Select
                Case MAPI_E_NO_LIBRARY
                    [error] = "MAPI No Library."
                    Exit Select
                Case MAPI_E_INVALID_PARAMETER
                    [error] = "MAPI Invalid parameter."
                    Exit Select
            End Select

            If [error] <> "User Aborted." Then
                Throw New Exception("Error sending MAPI Email. Error: " & [error] & " (code = " & errorCode & ").")
            Else
                'Only Log
                Log.LogError("Error sending MAPI Email. Error: " & [error] & " (code = " & errorCode & ").")
            End If
        End Sub

#End Region
    End Class

#End Region

#Region "Public Recipient Class"

    ''' <summary>
    ''' Represents a Recipient for a MapiMailMessage.
    ''' </summary>
    Public Class Recipient

#Region "Public Properties"

        ''' <summary>
        ''' The email address of this recipient.
        ''' </summary>
        Public Address As String = Nothing

        ''' <summary>
        ''' The display name of this recipient.
        ''' </summary>
        Public DisplayName As String = Nothing

        ''' <summary>
        ''' How the recipient will receive this message (To, CC, BCC).
        ''' </summary>
        Public RecipientType As MapiMailMessage.RecipientType = MapiMailMessage.RecipientType.[To]

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Creates a new recipient with the specified address.
        ''' </summary>
        Public Sub New(ByVal address__1 As String)
            Address = address__1
        End Sub

        ''' <summary>
        ''' Creates a new recipient with the specified address and display name.
        ''' </summary>
        Public Sub New(ByVal address__1 As String, ByVal displayName__2 As String)
            Address = address__1
            DisplayName = displayName__2
        End Sub

        ''' <summary>
        ''' Creates a new recipient with the specified address and recipient type.
        ''' </summary>
        Public Sub New(ByVal address__1 As String, ByVal recipientType__2 As MapiMailMessage.RecipientType)
            Address = address__1
            RecipientType = recipientType__2
        End Sub

        ''' <summary>
        ''' Creates a new recipient with the specified address, display name and recipient type.
        ''' </summary>
        Public Sub New(ByVal address__1 As String, ByVal displayName__2 As String, ByVal recipientType__3 As MapiMailMessage.RecipientType)
            Address = address__1
            DisplayName = displayName__2
            RecipientType = recipientType__3
        End Sub

#End Region

#Region "Internal Methods"

        ''' <summary>
        ''' Returns an interop representation of a recepient.
        ''' </summary>
        ''' <returns></returns>
        Friend Function GetInteropRepresentation() As NativeMethods.MapiRecipDesc
            Dim interop As New NativeMethods.MapiRecipDesc()

            If DisplayName Is Nothing Then
                interop.Name = Address
            Else
                interop.Name = DisplayName
                interop.Address = Address
            End If

            interop.RecipientClass = CInt(RecipientType)

            Return interop
        End Function

#End Region
    End Class

#End Region

#Region "Public RecipientCollection Class"

    ''' <summary>
    ''' Represents a colleciton of recipients for a mail message.
    ''' </summary>
    Public Class RecipientCollection
        Inherits CollectionBase

        ''' <summary>
        ''' Adds the specified recipient to this collection.
        ''' </summary>
        Public Sub Add(ByVal value As Recipient)
            List.Add(value)
        End Sub

        ''' <summary>
        ''' Adds a new recipient with the specified address to this collection.
        ''' </summary>
        Public Sub Add(ByVal address As String)
            Me.Add(New Recipient(address))
        End Sub

        ''' <summary>
        ''' Adds a new recipient with the specified address and display name to this collection.
        ''' </summary>
        Public Sub Add(ByVal address As String, ByVal displayName As String)
            Me.Add(New Recipient(address, displayName))
        End Sub

        ''' <summary>
        ''' Adds a new recipient with the specified address and recipient type to this collection.
        ''' </summary>
        Public Sub Add(ByVal address As String, ByVal recipientType As MapiMailMessage.RecipientType)
            Me.Add(New Recipient(address, recipientType))
        End Sub

        ''' <summary>
        ''' Adds a new recipient with the specified address, display name and recipient type to this collection.
        ''' </summary>
        Public Sub Add(ByVal address As String, ByVal displayName As String, ByVal recipientType As MapiMailMessage.RecipientType)
            Me.Add(New Recipient(address, displayName, recipientType))
        End Sub

        ''' <summary>
        ''' Returns the recipient stored in this collection at the specified index.
        ''' </summary>
        Default Public ReadOnly Property Item(ByVal index As Integer) As Recipient
            Get
                Return DirectCast(List(index), Recipient)
            End Get
        End Property

        ''' <summary>
        ''' Gets the interop representation.
        ''' </summary>
        ''' <returns></returns>
        Friend Function GetInteropRepresentation() As InteropRecipientCollection
            Return New InteropRecipientCollection(Me)
        End Function

        ''' <summary>
        ''' Struct which contains an interop representation of a colleciton of recipients.
        ''' </summary>
        Friend Class InteropRecipientCollection
            Implements IDisposable

#Region "Member Variables"

            Private _handle As IntPtr
            Private _count As Integer

#End Region

#Region "Constructors"

            ''' <summary>
            ''' Default constructor for creating InteropRecipientCollection.
            ''' </summary>
            ''' <param name="outer"></param>
            Public Sub New(ByVal outer As RecipientCollection)
                _count = outer.Count

                If _count = 0 Then
                    _handle = IntPtr.Zero
                    Return
                End If

                ' allocate enough memory to hold all recipients
                Dim size As Integer = Marshal.SizeOf(GetType(NativeMethods.MapiRecipDesc))
                _handle = Marshal.AllocHGlobal(_count * size)

                ' place all interop recipients into the memory just allocated
                Dim ptr As Integer = CInt(_handle)
                For Each native As Recipient In outer
                    Dim interop As NativeMethods.MapiRecipDesc = native.GetInteropRepresentation()

                    ' stick it in the memory block
                    Marshal.StructureToPtr(interop, CType(ptr, IntPtr), False)
                    ptr += size
                Next
            End Sub

#End Region

#Region "Public Properties"

            ''' <summary>
            ''' Gets the handle.
            ''' </summary>
            ''' <value>The handle.</value>
            Public ReadOnly Property Handle() As IntPtr
                Get
                    Return _handle
                End Get
            End Property

#End Region

#Region "Public Methods"

            ''' <summary>
            ''' Disposes of resources.
            ''' </summary>
            Public Sub Dispose() Implements IDisposable.Dispose
                If _handle <> IntPtr.Zero Then
                    Dim type As Type = GetType(NativeMethods.MapiRecipDesc)
                    Dim size As Integer = Marshal.SizeOf(type)

                    ' destroy all the structures in the memory area
                    Dim ptr As Integer = CInt(_handle)
                    For i As Integer = 0 To _count - 1
                        Marshal.DestroyStructure(CType(ptr, IntPtr), type)
                        ptr += size
                    Next

                    ' free the memory
                    Marshal.FreeHGlobal(_handle)

                    _handle = IntPtr.Zero
                    _count = 0
                End If
            End Sub

#End Region
        End Class
    End Class

#End Region
End Namespace
