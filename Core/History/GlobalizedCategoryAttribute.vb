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

Imports System.ComponentModel
Imports System.Resources
Imports System.Reflection

Namespace History
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Class GlobalizedCategoryAttribute
        Inherits CategoryAttribute

#Region "Localization"

        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the category order.
        ''' </summary>
        ''' <value>The category order.</value>
        Public Property CategoryOrder() As Integer

#End Region

#Region "Functions"

        ''' <summary>
        ''' Looks up the localized name of the specified category.
        ''' </summary>
        ''' <param name="value">The identifer for the category to look up.</param>
        ''' <returns>
        ''' The localized name of the category, or null if a localized name does not exist.
        ''' </returns>
        Protected Overrides Function GetLocalizedString(ByVal value As String) As String
            'Get the string from the resources.
            Dim sort As String = ""
            For i As Integer = 0 To CategoryOrder
                sort &= Chr(31) & Chr(32)
            Next

            Try
                Return sort & _langManager.GetString(value)
            Catch ex As Exception
                Return sort & value
            End Try
        End Function

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GlobalizedCategoryAttribute" /> class.
        ''' </summary>
        ''' <param name="category">The category.</param>
        ''' <param name="categoryOrder">The category order. (higher order = appears first)</param>
        Public Sub New(ByVal category As String, ByVal categoryOrder As Integer)
            MyBase.New(category)
            _CategoryOrder = categoryOrder
        End Sub

#End Region
    End Class
End Namespace
