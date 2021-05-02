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

Namespace History
    Public Class ViewableDictionaryConverter
        Inherits ExpandableObjectConverter

        ''' <summary>
        ''' Converts the given value object to the specified type, using the specified context and culture information.
        ''' </summary>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        ''' <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
        ''' <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        ''' <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
        ''' <returns>
        ''' An <see cref="T:System.Object" /> that represents the converted value.
        ''' </returns>
        ''' <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
        ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If destinationType = GetType(String) Then
                If value.GetType() = GetType(ViewableDictionary(Of String, String)) Then
                    Return CType(value, ViewableDictionary(Of String, String)).DisplayTitle
                Else
                    Return "[List, Expand to see the contents]"
                End If
            End If

            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

    End Class
End Namespace
