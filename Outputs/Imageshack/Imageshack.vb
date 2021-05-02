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
Imports System.Net
Imports System.IO
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Misc
Imports ScreenshotStudioDotNet.Core.Screenshots
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports System.Threading
Imports ScreenshotStudioDotNet.Core.Settings
Imports Microsoft.WindowsAPICodePack.Taskbar
Imports System.Text
Imports ScreenshotStudioDotNet.Core.Serialization
Imports System.Drawing.Imaging

Namespace Imageshack
    Public Class Imageshack
        Implements IOutput

#Region "Fields"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Outputs.General.Strings", Assembly.GetExecutingAssembly)

        Private _tempFileName As String
        Private _workerThread As Thread

        Private _bytesUploaded As Long
        Private _fileSize As Long

        Private _speed As Double
        Private _remainingTime As Double

        Private _arguments As New SerializableDictionary(Of String, Object)

#End Region

#Region "IOutput Members"

        ''' <summary>
        ''' Proceeds the specified screenshot.
        ''' </summary>
        ''' <param name="screenshot"></param>
        Public Function Proceed(ByVal screenshot As Screenshot) As String Implements IOutput.Proceed
            For Each p In screenshot.Macro.Outputs
                If p.Name = Me.Name1 Then
                    _arguments = p.Arguments
                End If
            Next

            Dim format As ImageFormat = SettingsDatabase.FileFormat

            If _arguments.ContainsKey("Format") Then
                format = FileNameMaskProcessor.StringToFileFormat(_arguments("Format").ToString)
            End If

            Log.LogInformation ( "Using Format: " & format.ToString )

            'Create temp file name and if it exists already
            _tempFileName = Path.Combine(StaticProperties.TempDirectory, FileNameMaskProcessor.ProcessMask(screenshot, False)) & FileNameMaskProcessor.GetFileSuffix(format)

            Dim i As Integer = 1
            While File.Exists(_tempFileName)
                _tempFileName = Path.Combine(StaticProperties.TempDirectory, FileNameMaskProcessor.ProcessMask(screenshot, False)) & " (" & i & ")." & FileNameMaskProcessor.GetFileSuffix(format)
                i += 1
            End While

            'save under temp file name
            screenshot.Screenshot.Save(_tempFileName, format)

            Log.LogInformation("Temp file Screenshot saved with filename: " & _tempFileName)

            If TaskbarManager.IsPlatformSupported Then TaskbarManager.Instance.RefreshInstance()

            Me.ShowDialog()

            Log.LogInformation("Plugin finished")

            Return Link
        End Function

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public ReadOnly Property Description() As String Implements IPlugin.Description
            Get
                Return _langManager.GetString("imageshackDesc")
            End Get
        End Property

        ''' <summary>
        ''' Gets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public ReadOnly Property DisplayName() As String Implements IPlugin.DisplayName
            Get
                Return _langManager.GetString("imageshackName")
            End Get
        End Property

        ''' <summary>
        ''' Gets the settings panel.
        ''' </summary>
        ''' <value>The settings panel.</value>
        Public ReadOnly Property SettingsPanel() As PluginSettingsPanel Implements IPlugin.SettingsPanel
            Get
                Return Nothing
            End Get
        End Property

#End Region

#Region "Upload-Related functions"

        Private Delegate Sub PresentLinkDelegate(ByVal link As String)

        ''' <summary>
        ''' Presents the link.
        ''' </summary>
        ''' <param name="link">The link.</param>
        Public Sub PresentLink(ByVal link As String)
            If Me.InvokeRequired Then
                Me.Invoke(New PresentLinkDelegate(AddressOf PresentLink), New Object() {link})
            Else
                Log.LogInformation("Screenshot uploaded, presenting the Link: " & link)

                Me.Visible = False

                _Link = link

                If _arguments.ContainsKey("Action") Then
                    Log.LogInformation("Link Destination : " & _arguments("Action").ToString)

                    Select Case _arguments("Action").ToString
                        Case "Clipboard"
                            My.Computer.Clipboard.SetText(link)
                        Case "Save"
                            Try
                                Dim filename As String = _arguments("Filename").ToString

                                My.Computer.FileSystem.WriteAllText(filename, If(IO.File.Exists(filename), vbCrLf, "") & link, _arguments.ContainsKey("Append"))

                                Log.LogInformation("Saved the link to file: " & filename)
                            Catch ex As Exception
                                Log.LogError("Error saving the Link")
                                Log.LogError(ex)
                            End Try
                    End Select
                Else
                    Log.LogInformation("Link Destination : None")
                End If

              
                'Close
                Me.Close()
                Me.Dispose()
            End If
        End Sub

        Public Delegate Sub CloseThreadSafeDelegate()

        ''' <summary>
        ''' Closes the thread safe.
        ''' </summary>
        Public Sub CloseThreadSafe()
            If Me.InvokeRequired Then
                Me.Invoke(New CloseThreadSafeDelegate(AddressOf CloseThreadSafe))
            Else
                Me.Close()
            End If
        End Sub

        Private Delegate Sub UpdateProgressDelegate(ByVal progress As Double, ByVal bytesUploaded As Long, ByVal bytesTotal As Long)

        ''' <summary>
        ''' Updates the progress.
        ''' </summary>
        ''' <param name="progress">The progress.</param>
        Private Sub UpdateProgress(ByVal progress As Double, ByVal bytesUploaded As Long, ByVal bytesTotal As Long)
            'Thread safe
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateProgressDelegate(AddressOf UpdateProgress), New Object() {progress, bytesUploaded, bytesTotal})
            Else
                _bytesUploaded = bytesUploaded
                _fileSize = bytesTotal

                If progress = -1 Then
                    'Update the progress bar
                    ProgressBar1.Style = ProgressBarStyle.Marquee
                    ProgressBar1.Value = 0

                    'Update the taskbar progressbar, if supported
                    If TaskbarManager.IsPlatformSupported Then
                        Try
                            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate, Me.Handle)
                        Catch ex As Exception
                        End Try
                    End If

                    lblStatus.Text = _langManager.GetString("gettingLink")
                Else
                    'Update the progress bar
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Value = CInt(progress * 100)

                    'Update the taskbar progressbar, if supported
                    If TaskbarManager.IsPlatformSupported Then
                        Try
                            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal, Me.Handle)
                            TaskbarManager.Instance.SetProgressValue(CInt(progress * 100), 10000, Me.Handle)
                        Catch ex As Exception
                        End Try
                    End If

                    lblStatus.Text = _langManager.GetString("pleaseWait") & vbCrLf & FileDownloadFunctions.GetReadableSize(_bytesUploaded) & " " & _langManager.GetString("waitOf") & " " & FileDownloadFunctions.GetReadableSize(_fileSize)

                    If _speed <> 0 Or True Then
                        lblStatus.Text &= " @ " & FileDownloadFunctions.GetReadableSpeed(_speed)
                    End If

                    lblStatus.Text &= " (" & CInt(_bytesUploaded / _fileSize * 100) & "%"

                    If _remainingTime <> 0 Then
                        lblStatus.Text &= ", " & FileDownloadFunctions.GetReadableTime(_remainingTime) & " " & _langManager.GetString("waitLeft")
                    End If

                    lblStatus.Text &= ")"

                End If
            End If
        End Sub

        ''' <summary>
        ''' Uploads the file to imageshack.
        ''' </summary>
        Public Sub UploadFileToImageShack()
            Try
                'Save the setting
                Dim oldValue As Boolean = ServicePointManager.Expect100Continue

                ServicePointManager.Expect100Continue = False

                'new cookiecontainer
                Dim cookie As New CookieContainer()

                'build the query string
                Dim queryStringArguments As New Dictionary(Of String, String)
                queryStringArguments.Add("MAX_FILE_SIZE", "3145728")
                queryStringArguments.Add("refer", "")
                queryStringArguments.Add("brand", "")
                queryStringArguments.Add("optimage", "1")
                queryStringArguments.Add("rembar", "1")
                queryStringArguments.Add("submit", "host it!")
                queryStringArguments.Add("optsize", "resample")

                'select the content type
                Dim contentType As String = ""
                Select Case Path.GetExtension(_tempFileName).ToUpperInvariant
                    Case ".jpg".ToUpperInvariant
                        contentType = "image/jpeg"
                    Case ".jpeg".ToUpperInvariant
                        contentType = "image/jpeg"
                    Case ".gif".ToUpperInvariant
                        contentType = "image/gif"
                    Case ".png".ToUpperInvariant
                        contentType = "image/png"
                    Case ".bmp".ToUpperInvariant
                        contentType = "image/bmp"
                    Case ".tif".ToUpperInvariant
                        contentType = "image/tiff"
                    Case ".tiff".ToUpperInvariant
                        contentType = "image/tiff"
                    Case Else
                        contentType = "image/unknown"
                End Select

                'upload
                Log.LogInformation("Starting the upload")
                Dim resp As String = UploadFileEx("http://www.imageshack.us/index.php", "fileupload", contentType, queryStringArguments, cookie)

                'reset the changed setting
                ServicePointManager.Expect100Continue = oldValue

                'retreive the link
                Dim regexDirect As String = "Direct\ <a\ style='font-size:\ 15pt;'\ href='.*'>link</a>\ to\ image"
                Dim regexLink As String = "http[a-zA-Z0-9.:/]*"

                'finished, show the link
                Dim link As String = Regex.Match(resp, regexDirect).Value
                link = Regex.Match(link, regexLink).Value

                If _arguments.ContainsKey("Bitly") Then
                    'use bit.ly as short url service
                    Log.LogInformation("Using Bit.ly")

                    link = BitlyApi.ShortenUrl(link).ShortUrl
                End If

                PresentLink(link)
            Catch ex As ThreadAbortException
                'Catch
            Catch ex As Exception
                Log.LogError(ex)
                ShowErrorHelp()
            End Try
        End Sub

        ''' <summary>
        ''' Uploads the file ex.
        ''' </summary>
        ''' <param name="url">The URL.</param>
        ''' <param name="fileFormName">Name of the file form.</param>
        ''' <param name="contentType">Type of the content.</param>
        ''' <param name="queryStringArguments">The query string arguments.</param>
        ''' <param name="cookies">The cookies.</param>
        ''' <returns></returns>
        Private Function UploadFileEx(ByVal url As String, ByVal fileFormName As String, ByVal contentType As String, ByVal queryStringArguments As Dictionary(Of String, String), ByVal cookies As CookieContainer) As String
            'Validate the arguments
            If fileFormName = "" Then fileFormName = "file"
            If contentType = "" Then contentType = "application/octet-stream"

            'Build the Query String
            Dim postData As String = "?"
            If queryStringArguments IsNot Nothing Then
                For Each kvp As KeyValuePair(Of String, String) In queryStringArguments
                    postData &= kvp.Key & "=" & kvp.Value & "&"
                Next
            End If

            'Build the URI and the Request
            Dim URI As New Uri(url + postData)
            Dim boundary As String = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture)
            Dim wReq As HttpWebRequest = DirectCast(WebRequest.Create(URI), HttpWebRequest)
            wReq.CookieContainer = cookies
            wReq.ContentType = "multipart/form-data; boundary=" + boundary
            wReq.Method = "POST"

            Dim postHeader As String = String.Format(CultureInfo.InvariantCulture, "--" & boundary & "{0}" & "Content-Disposition: form-data; name=""" & fileFormName & """; filename=""" & Path.GetFileName(_tempFileName) & """{0}" & "Content-Type: " & contentType & "{0}{0}", vbNewLine)
            Dim postHeaderBytes As Byte() = Encoding.UTF8.GetBytes(postHeader)
            Dim boundaryBytes As Byte() = Encoding.ASCII.GetBytes(vbNewLine & "--" + boundary + vbNewLine)

            'open the screenshot
            Dim fileStream As New FileStream(_tempFileName, FileMode.Open, FileAccess.Read)

            'Update the Progress
            UpdateProgress(0, 0, fileStream.Length)

            wReq.ContentLength = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length
            Dim requestStream As Stream = wReq.GetRequestStream()
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length)

            'Submit the screenshot
            Dim buffer As Byte() = New Byte(CInt(Math.Min(2048, CInt(fileStream.Length))) - 1) {}
            Dim bytesRead As Integer = 0
            Dim position = 0
            Do
                position += bytesRead

                'Update the Progress
                UpdateProgress(Math.Round(position / fileStream.Length * 100, 4), position, fileStream.Length)

                bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                If bytesRead = 0 Then Exit Do
                requestStream.Write(buffer, 0, bytesRead)
            Loop

            'set in intermediate status 
            UpdateProgress(-1, position, fileStream.Length)

            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length)

            'close the filestream
            fileStream.Close()
            fileStream.Dispose()

            Dim Rta As WebResponse = wReq.GetResponse()
            Dim s As Stream = Rta.GetResponseStream()
            Dim sr As New StreamReader(s)

            Return sr.ReadToEnd
        End Function

        ''' <summary>
        ''' Shows the error help.
        ''' </summary>
        Private Sub ShowErrorHelp()
            Me.CloseThreadSafe()
        End Sub

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the Imageshack control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub Imageshack_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            'Hide?
            If _arguments.ContainsKey("Hide") Then
                Log.LogInformation("Hiding ImageShackUploadForm")

                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.Size = New Drawing.Size(1, 1)
                Me.Opacity = 0
            End If

            'Start the uploading process
            _workerThread = New Thread(AddressOf UploadFileToImageShack)
            _workerThread.Name = "Image Upload"
            _workerThread.Start()
        End Sub

        ''' <summary>
        ''' Handles the Tick event of the SpeedTimer control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub SpeedTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles SpeedTimer.Tick
            Static _laststatus As Long = 0

            If _bytesUploaded = 0 Then Exit Sub

            _speed = (_bytesUploaded - _laststatus) / SpeedTimer.Interval * 1000
            _laststatus = _bytesUploaded

            If Not _speed = 0 Then _remainingTime = (_fileSize - _bytesUploaded) / _speed
        End Sub

#End Region

        ''' <summary>
        ''' Gets the name1.
        ''' </summary>
        ''' <value>The name1.</value>
        Public ReadOnly Property Name1() As String Implements IPlugin.Name
            Get
                Return "Imageshack.us"
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the link.
        ''' </summary>
        ''' <value>The link.</value>
        Public Property Link() As String

        ''' <summary>
        ''' Gets a value indicating whether this plugin is available.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is available; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsAvailable() As Boolean Implements IOutput.IsAvailable
            Get
                'Todo: Check Internet Access
                Return True
            End Get
        End Property

        ''' <summary> 
        ''' Gets a value indicating whether its okay to add this plugin more than one time to a macro.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is supported multiple times; otherwise, <c>false</c>.
        ''' </value>
        Public ReadOnly Property IsSupportedMultipleTimes As Boolean Implements Core.Extensibility.IOutput.IsSupportedMultipleTimes
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Returns a <see cref="System.String" /> of the arguments.
        ''' </summary>
        ''' <param name="arguments">The arguments.</param>
        ''' <returns>
        ''' A <see cref="System.String" /> that represents this instance.
        ''' </returns>
        Public Function ArgumentsToString(ByVal arguments As System.Collections.Generic.Dictionary(Of String, Object)) As String Implements Core.Extensibility.IArgumentPlugin.ArgumentsToString
            Dim s As String = ""

            If arguments.ContainsKey("Bitly") Then
                s &= "Short-URL, "
            End If

            If arguments.ContainsKey("Hide") Then
                s &= "Hide, "
            End If

            If arguments.ContainsKey("Append") Then
                s &= "Append, "
            End If

            If arguments.ContainsKey("Action") Then
                s &= arguments("Action").ToString & ", "
            End If

            If arguments.ContainsKey("Format") Then
                s &= arguments("Format").ToString & ", "
            End If

            If s.Length > 2 Then s = s.Substring(0, s.Length - 2)

            Return s
        End Function

        ''' <summary>
        ''' Gets the argument designer.
        ''' </summary>
        ''' <value>The argument designer.</value>
        Public ReadOnly Property ArgumentDesigner As PluginArgumentDesigner Implements IArgumentPlugin.ArgumentDesigner
            Get
                Return New ImageshackArgumentDesigner
            End Get
        End Property
    End Class
End Namespace
