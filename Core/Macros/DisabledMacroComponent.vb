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
    Public Class DisabledMacroComponent
        Inherits MacroComponent

        ''' <summary>
        ''' Gets the type of the component.
        ''' </summary>
        ''' <value>The type of the component.</value>
        Protected Overrides ReadOnly Property ComponentType As ComponentTypes
            Get
                Return ComponentTypes.Disabled
            End Get
        End Property

        ''' <summary>
        ''' Gets the text.
        ''' </summary>
        ''' <value>The text.</value>
        Protected Overrides ReadOnly Property Text As String
            Get
                Return DisabledThing
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the disabled thing.
        ''' </summary>
        ''' <value>The disabled thing.</value>
        Public Property DisabledThing As String

        ''' <summary>
        ''' Gets a value indicating whether [delete able].
        ''' </summary>
        ''' <value><c>true</c> if [delete able]; otherwise, <c>false</c>.</value>
        Protected Overrides ReadOnly Property DeleteAble As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DisabledMacroComponent" /> class.
        ''' </summary>
        ''' <param name="form">The form.</param>
        Public Sub New(ByVal form As MacroGenerator, ByVal name As String)
            MyBase.New(form, name)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DisabledMacroComponent" /> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub
    End Class
End Namespace
