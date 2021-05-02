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

Namespace Macros
    Public Class DelayMacroComponent
        Inherits MacroComponent

        ''' <summary>
        ''' Gets the text that will be painted on the control.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The text associated with this control.</returns>
        Protected Overrides ReadOnly Property Text As String
            Get
                Dim s As String = "Delay" 'Todo: Loc

                If InUse Then
                    s &= vbCrLf & Delay.ToString & " second" & If(Delay = 1, "", "s")  'Todo: Loc
                End If

                Return s
            End Get
        End Property

        ''' <summary>
        ''' Gets the type of the component.
        ''' </summary>
        ''' <value>The type of the component.</value>
        Protected Overrides ReadOnly Property ComponentType As ComponentTypes
            Get
                Return ComponentTypes.Delay
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the delay.
        ''' </summary>
        ''' <value>The delay.</value>
        Public Property Delay As Integer = 0

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DelayMacroComponent" /> class.
        ''' </summary>
        ''' <param name="form">The form.</param>
        ''' <param name="name">The name.</param>
        Public Sub New(ByVal form As MacroGenerator, ByVal name As String)
            MyBase.New(form, name)
            Me.MoreThanOneOccurenceAllowed = False
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DelayMacroComponent" /> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

    End Class
End Namespace
