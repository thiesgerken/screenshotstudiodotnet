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

Imports System.Threading
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.History
Imports ScreenshotStudioDotNet.Core.Logging
Imports ScreenshotStudioDotNet.Core.Extensibility
Imports ScreenshotStudioDotNet.Core.Macros
Imports ScreenshotStudioDotNet.Core.Serialization

Namespace Screenshots
    Public Class ScreenshotCreator

#Region "Fields"

        Private _workerThread As Thread

#End Region

#Region "Events"

        Public Event ScreenshotTaken(ByVal sender As Object, ByVal e As ScreenshotTakenEventArgs)

        ''' <summary>
        ''' Called when [screenshot taken].
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The e.</param>
        Private Sub OnScreenshotTaken(ByVal sender As Object, ByVal e As ScreenshotTakenEventArgs)
            RaiseEvent ScreenshotTaken(sender, e)
        End Sub

        Public Event HideRequested(ByVal sender As Object, ByVal e As EventArgs)

        ''' <summary>
        ''' Called when [hide requested].
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub OnHideRequested(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent HideRequested(sender, e)
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Creates a screenshot.
        ''' </summary>
        ''' <param name="parameters">The parameters.</param>
        Private Overloads Sub CreateScreenshot(ByVal parameters As Macro)
            Try
                Log.LogInformation("Beginning the Creation of a Screenshot, Macro Name: " & parameters.Name)
                OnHideRequested(Me, New EventArgs)

                'wait, if a delay is given
                Thread.Sleep(parameters.Delay)

                'for each screenshot to create
                For i As Integer = 1 To parameters.Multiple.Count
                    Dim t As New Thread(New ParameterizedThreadStart(AddressOf CreateScreenshotInternal))
                    t.Name = "Screenshot Creation of number " & i

                    'Set the right apartement State (important for "clipboard" plugin)
                    t.SetApartmentState(ApartmentState.STA)

                    t.Start(New KeyValuePair(Of Macro, Integer)(parameters, i))
                Next
            Catch ex As ThreadAbortException
                'just close
                Log.LogWarning("Creation aborted (Thread Aborted)")
            End Try
        End Sub

        ''' <summary>
        ''' Creates the screenshot internal.
        ''' </summary>
        ''' <param name="info">The info.</param>
        Private Sub CreateScreenshotInternal(ByVal info As Object)
            CreateScreenshotInternal(CType(info, KeyValuePair(Of Macro, Integer)))
        End Sub

        ''' <summary>
        ''' Creates the screenshot internal.
        ''' </summary>
        ''' <param name="info">The info, a keyvaluepair to pass 2 parameters in 1 argument</param>
        Private Sub CreateScreenshotInternal(ByVal info As KeyValuePair(Of Macro, Integer))
            Try
                Log.LogInformation("Screenshot " & info.Value & " of " & info.Key.Multiple.Count)

                'wait, if this isnt the first screenshot
                If Not info.Value = 1 Then Thread.Sleep(info.Key.Multiple.Interval)

                'create the screenshot
                Log.LogInformation("Creating ... ScreenshotType: " & info.Key.Type.DisplayName)

                Dim creator As IScreenshotType = CType((info.Key.Type).CreateInstance, IScreenshotType)
                Dim screenshot As Screenshot = creator.CreateScreenshot(info.Key)

                If Not screenshot Is Nothing Then
                    Log.LogInformation("Creation successful")

                    'Effects
                    If info.Key.Effects.Count > 0 Then
                        Log.LogInformation("Applying effects")

                        For Each p In info.Key.Effects
                            Log.LogInformation("Applying effect: " & p.Name)

                            screenshot = CType(p.CreateInstance, IEffect).Proceed(screenshot)
                        Next
                    Else
                        Log.LogInformation("No Effects defined in Macro")
                    End If

                    'Play Sound
                    My.Computer.Audio.Play(My.Resources.camera, AudioPlayMode.Background)

                    screenshot.Screenshot = TrimImage(screenshot.Screenshot)
                    screenshot.Number = info.Value

                    'use the associated plugin to output the screen, or, if no plugin was given, ask the user what to do
                    Dim outputsUsed As New List(Of Plugin(Of IOutput))
                    Dim infomationFromOutput As New List(Of String)

                    If info.Key.Outputs.Count = 0 Then
                        Log.LogInformation("No Output defined, asking")

                        Dim p As New OutputPicker
                        p.ShowDialog()
                        If p.SelectedOutputName <> "" Then
                            Log.LogInformation("Outputting ... Plugin (from user ask): " & p.SelectedOutputName)

                            'get an instance of the output plugin
                            Dim outputDatabase = New PluginDatabase(Of IOutput)()
                            Dim outputPlugin = outputDatabase(p.SelectedOutputName)
                            Dim output As IOutput = CType(outputPlugin.CreateInstance, IOutput)

                            outputsUsed.Add(outputPlugin)
                            infomationFromOutput.Add(output.Proceed(screenshot))
                        Else
                            Log.LogWarning("User canceled the output")
                        End If
                    Else
                        For Each p As Plugin(Of IOutput) In info.Key.Outputs
                            Log.LogInformation("Outputting ... Plugin (from macro settings): " & p.DisplayName)

                            'get an instance of the output plugin
                            Dim output As IOutput = CType(p.CreateInstance, IOutput)

                            'start the output
                            outputsUsed.Add(p)
                            infomationFromOutput.Add(output.Proceed(screenshot))
                        Next
                    End If


                    'Add to History
                    Log.LogInformation("Adding Screenshot to History")
                    ScreenshotHistory.AddEntry(screenshot, info.Key, outputsUsed, infomationFromOutput)

                    OnScreenshotTaken(Me, New ScreenshotTakenEventArgs(info.Value, info.Key.Multiple.Count, screenshot, outputsUsed, infomationFromOutput))
                Else
                    Log.LogWarning("Creation unsuccessful (Screenshot equals nothing)")
                End If

                Log.LogInformation("Creation of Screenshot " & info.Value & " of " & info.Key.Multiple.Count & " complete")
            Catch ex As ThreadAbortException
                'just close
                Log.LogWarning("Creation of screenshot number " & info.Value & " aborted (Thread Aborted)")
            End Try
        End Sub

        ''' <summary>
        ''' Trims the image.
        ''' </summary>
        ''' <param name="bmp">The BMP.</param>
        ''' <returns></returns>
        Public Shared Function TrimImage(ByVal bmp As Bitmap) As Bitmap
            Dim reached As Boolean = False

            reached = False
            Dim topBorder As Integer = -1
            For y As Integer = 0 To bmp.Height - 1
                For x As Integer = 0 To bmp.Width - 1 Step 200
                    If Not reached Then topBorder = y

                    If Not bmp.GetPixel(x, y).A = 0 Then
                        reached = True
                    End If
                Next
                If reached Then Exit For
            Next

            reached = False
            Dim bottomBorder As Integer = -1
            For y As Integer = bmp.Height - 1 To 0 Step -1
                For x As Integer = 0 To bmp.Width - 1 Step 200
                    If Not reached Then bottomBorder = y

                    If Not bmp.GetPixel(x, y).A = 0 Then
                        reached = True
                    End If
                Next
                If reached Then Exit For
            Next

            reached = False
            Dim leftBorder As Integer = 0
            For x As Integer = 0 To bmp.Width - 1
                For y As Integer = 0 To bmp.Height - 1 Step 200
                    If Not reached Then leftBorder = x

                    If Not bmp.GetPixel(x, y).A = 0 Then
                        reached = True
                    End If
                Next
                If reached Then Exit For
            Next

            reached = False
            Dim rightBorder As Integer = 0
            For x As Integer = bmp.Width - 1 To 0 Step -1
                For y As Integer = 0 To bmp.Height - 1 Step 200
                    If Not reached Then rightBorder = x

                    If Not bmp.GetPixel(x, y).A = 0 Then
                        reached = True
                    End If
                Next
                If reached Then Exit For
            Next

            Dim newImage As New Bitmap(rightBorder - leftBorder + 1, bottomBorder - topBorder + 1)

            Using g As Graphics = Graphics.FromImage(newImage)
                g.DrawImage(bmp, New RectangleF(0, 0, newImage.Width, newImage.Height), New RectangleF(leftBorder, topBorder, newImage.Width, newImage.Height), GraphicsUnit.Pixel)
            End Using

            Return newImage
        End Function

        ''' <summary>
        ''' Creates the screenshot.
        ''' Just a helper function, because the <see cref="ParameterizedThreadStart"/> Delegate does not accept the type <see cref="Macro" /> as parameter.
        ''' </summary>
        ''' <param name="parameters">The parameters.</param>
        Private Overloads Sub CreateScreenshot(ByVal parameters As Object)
            CreateScreenshot(CType(parameters, Macro))
        End Sub

        ''' <summary>
        ''' Creates the screenshot async.
        ''' </summary>
        ''' <param name="parameters">The parameters.</param>
        Public Overloads Sub CreateScreenshotAsync(ByVal parameters As Macro)
            _workerThread = New Thread(AddressOf CreateScreenshot)
            _workerThread.Name = "ScreenshotCreator"
            _workerThread.Start(parameters)
        End Sub

        ''' <summary>
        ''' Creates the screenshot async.
        ''' </summary>
        ''' <param name="type">The type.</param>
        Public Overloads Sub CreateScreenshotAsync(ByVal type As Plugin(Of IScreenshotType))
            CreateScreenshotAsync(New Macro(type))
        End Sub

        ''' <summary>
        ''' Creates the screenshot.
        ''' </summary>
        ''' <param name="type">The type.</param>
        Private Overloads Sub CreateScreenshot(ByVal type As Plugin(Of IScreenshotType))
            CreateScreenshot(New Macro(type))
        End Sub

        ''' <summary>
        ''' Aborts the screenshot creation process if there are screenshots being taken now.
        ''' </summary>
        Public Sub Abort()
            If Not _workerThread Is Nothing Then
                If _workerThread.IsAlive Then _workerThread.Abort()
            End If
        End Sub

#End Region
    End Class
End Namespace
