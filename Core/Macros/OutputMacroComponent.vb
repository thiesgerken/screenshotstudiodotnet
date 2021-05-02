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
Imports ScreenshotStudioDotNet.Core.Extensibility

Namespace Macros
    Public Class OutputMacroComponent
        Inherits ArgumentMacroComponent

        ''' <summary>
        ''' Gets the type of the component.
        ''' </summary>
        ''' <value>The type of the component.</value>
        Protected Overrides ReadOnly Property ComponentType As ComponentTypes
            Get
                Return ComponentTypes.Output
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DisabledMacroComponent" /> class.
        ''' </summary>
        ''' <param name="form">The form.</param>
        Public Sub New(ByVal form As MacroGenerator, ByVal name As String)
            MyBase.New(form, name)
            Init()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="OutputMacroComponent" /> class.
        ''' </summary>
        Public Sub New()
            Init()
        End Sub
    End Class
End Namespace
