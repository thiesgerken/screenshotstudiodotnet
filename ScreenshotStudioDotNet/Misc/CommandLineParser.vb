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

Imports System.Collections.Specialized
Imports System.Text.RegularExpressions
Imports ScreenshotStudioDotNet.Forms
Imports ScreenshotStudioDotNet.Core.Macros

Namespace Misc
    ''' <summary>
    ''' Arguments class
    ''' </summary>
    Public Class CommandLineParser
        Inherits StringDictionary

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CommandLineParser" /> class.
        ''' </summary>
        ''' <param name="Args">
        ''' Valid parameter forms:
        ''' {-,/,--}param{ ,=,:}((",')value(",'))
        ''' Examples: 
        ''' -param1 value1 --param2 /param3:"Test-:-work" 
        ''' /param4=happy -param5 '--=nice=--'
        ''' </param>
        Public Sub New(ByVal Args As String())
            MyBase.New()

            Dim splitter As New Regex("^-{1,2}|^/|=|:", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            Dim remover As New Regex("^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

            Dim parameter As String = Nothing
            Dim parts As String()

            For Each Txt As String In Args
                ' Look for new parameters (-,/ or --) and a
                ' possible enclosed value (=,:)

                parts = splitter.Split(Txt, 3)

                Select Case parts.Length
                    ' Found a value (for the last parameter 
                    ' found (space separator))

                    Case 1
                        If parameter IsNot Nothing Then
                            If Not Me.ContainsKey(parameter) Then
                                parts(0) = remover.Replace(parts(0), "$1")

                                Me.Add(parameter, parts(0))
                            End If
                            parameter = Nothing
                        End If
                        ' else Error: no parameter waiting for a value (skipped)
                        Exit Select
                        ' Found just a parameter
                    Case 2
                        ' The last parameter is still waiting. 
                        ' With no value, set it to true.

                        If parameter IsNot Nothing Then
                            If Not Me.ContainsKey(parameter) Then
                                Me.Add(parameter, "true")
                            End If
                        End If
                        parameter = parts(1)
                        Exit Select

                        ' Parameter with enclosed value

                    Case 3
                        ' The last parameter is still waiting. 
                        ' With no value, set it to true.

                        If parameter IsNot Nothing Then
                            If Not Me.ContainsKey(parameter) Then
                                Me.Add(parameter, "true")
                            End If
                        End If

                        parameter = parts(1)

                        ' Remove possible enclosing characters (",')

                        If Not Me.ContainsKey(parameter) Then
                            parts(2) = remover.Replace(parts(2), "$1")
                            Me.Add(parameter, parts(2))
                        End If

                        parameter = Nothing
                        Exit Select
                End Select
            Next
            ' In case a parameter is still waiting
            If parameter IsNot Nothing Then
                If Not Me.ContainsKey(parameter) Then
                    Me.Add(parameter, "true")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CommandLineParser" /> class, with the command line arguments of the app as arguments
        ''' </summary>
        Public Sub New()
            Me.New(Environment.CommandLine.Split(" "c))
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Parses the command line.
        ''' </summary>
        ''' <param name="arguments">The arguments.</param>
        Public Shared Sub ParseCommandLine(ByVal arguments As String)
            If arguments.Contains(" "c) Then
                ParseCommandLine(arguments.Split(" "c))
            End If
        End Sub

        ''' <summary>
        ''' Parses the command line.
        ''' </summary>
        ''' <param name="arguments">The arguments.</param>
        Public Shared Sub ParseCommandLine(ByVal arguments As String(), ByVal isSecondInstance As Boolean)
            ParseCommandLine(New CommandLineParser(arguments), isSecondInstance)
        End Sub

        ''' <summary>
        ''' Parses the command line.
        ''' </summary>
        Public Shared Sub ParseCommandLine(ByVal isSecondInstance As Boolean)
            ParseCommandLine(New CommandLineParser, isSecondInstance)
        End Sub

        ''' <summary>
        ''' Parses the command line.
        ''' </summary>
        ''' <param name="arguments">The arguments.</param>
        Public Shared Sub ParseCommandLine(ByVal arguments As String())
            ParseCommandLine(New CommandLineParser(arguments), False)
        End Sub

        ''' <summary>
        ''' Parses the command line.
        ''' </summary>
        Public Shared Sub ParseCommandLine()
            ParseCommandLine(New CommandLineParser, False)
        End Sub

        ''' <summary>
        ''' Parses the command line.
        ''' </summary>
        ''' <param name="parser">The parser.</param>
        Private Shared Sub ParseCommandLine(ByVal parser As CommandLineParser, ByVal isSecondInstance As Boolean)
            If parser.ContainsKey("macro") Then
                Dim mdb As New MacroDatabase
                If mdb.Contains(parser("macro")) Then
                    MainForm.NotifyIconVisible = isSecondInstance
                    MainForm.Creator.CreateScreenshotAsync(mdb(parser("macro")))
                End If
            ElseIf parser.ContainsKey("showQS") Or isSecondInstance Then
                MainForm.NotifyIconVisible = isSecondInstance
                QuickStart.Show()
            End If
        End Sub

#End Region
    End Class
End Namespace
