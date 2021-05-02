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
    Public Class ViewableDictionaryPropertyDescriptor(Of TKey, TValue)
        Inherits PropertyDescriptor

#Region "Fields"

        Private _dict As Dictionary(Of TKey, TValue) = Nothing
        Private _index As Integer = -1

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ViewableListPropertyDescriptor(Of T)" /> class.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <param name="idx">The idx.</param>
        Public Sub New(ByVal dict As Dictionary(Of TKey, TValue), ByVal idx As Integer)
            MyBase.New("#" & idx.ToString(), Nothing)
            Me._dict = dict
            Me._index = idx
        End Sub

#End Region

#Region "Members Overridden from PropertyDescriptor"

        ''' <summary>
        ''' Gets the collection of attributes for this member.
        ''' </summary>
        ''' <value></value>
        ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> that provides the attributes for this member, or an empty collection if there are no attributes in the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray" />.</returns>
        Public Overloads Overrides ReadOnly Property Attributes() As AttributeCollection
            Get
                Return New AttributeCollection(Nothing)
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, returns whether resetting an object changes its value.
        ''' </summary>
        ''' <param name="component">The component to test for reset capability.</param>
        ''' <returns>
        ''' true if resetting the component changes its value; otherwise, false.
        ''' </returns>
        Public Overloads Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' When overridden in a derived class, gets the type of the component this property is bound to.
        ''' </summary>
        ''' <value></value>
        ''' <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
        Public Overloads Overrides ReadOnly Property ComponentType() As Type
            Get
                Return Me._dict.GetType()
            End Get
        End Property

        ''' <summary>
        ''' Gets the name that can be displayed in a window, such as a Properties window.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The name to display for the member.</returns>
        Public Overloads Overrides ReadOnly Property DisplayName() As String
            Get
                Dim i As Integer = 0
                For Each kvp In _dict
                    If i = _index Then
                        Return kvp.Key.ToString
                    End If

                    i += 1
                Next
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the member, as specified in the <see cref="T:System.ComponentModel.DescriptionAttribute" />.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The description of the member. If there is no <see cref="T:System.ComponentModel.DescriptionAttribute" />, the property value is set to the default, which is an empty string ("").</returns>
        Public Overloads Overrides ReadOnly Property Description() As String
            Get
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, gets the current value of the property on a component.
        ''' </summary>
        ''' <param name="component">The component with the property for which to retrieve the value.</param>
        ''' <returns>
        ''' The value of a property for a given component.
        ''' </returns>
        Public Overloads Overrides Function GetValue(ByVal component As Object) As Object
            Dim i As Integer = 0
            For Each kvp In _dict
                If i = _index Then
                    Return kvp.Value.ToString
                End If

                i += 1
            Next
            Return ""
        End Function

        ''' <summary>
        ''' When overridden in a derived class, gets a value indicating whether this property is read-only.
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if the property is read-only; otherwise, false.</returns>
        Public Overloads Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the member.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The name of the member.</returns>
        Public Overloads Overrides ReadOnly Property Name() As String
            Get
                Return "#" & _index.ToString()
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, gets the type of the property.
        ''' </summary>
        ''' <value></value>
        ''' <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
        Public Overloads Overrides ReadOnly Property PropertyType() As Type
            Get
                Return GetType(KeyValuePair(Of TKey, TValue))
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, resets the value for this property of the component to the default value.
        ''' </summary>
        ''' <param name="component">The component with the property value that is to be reset to the default value.</param>
        Public Overloads Overrides Sub ResetValue(ByVal component As Object)
        End Sub

        ''' <summary>
        ''' When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
        ''' </summary>
        ''' <param name="component">The component with the property to be examined for persistence.</param>
        ''' <returns>
        ''' true if the property should be persisted; otherwise, false.
        ''' </returns>
        Public Overloads Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' When overridden in a derived class, sets the value of the component to a different value.
        ''' </summary>
        ''' <param name="component">The component with the property value that is to be set.</param>
        ''' <param name="value">The new value.</param>
        Public Overloads Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
        End Sub

#End Region
    End Class
End Namespace
