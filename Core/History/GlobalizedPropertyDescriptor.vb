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
    ''' <summary>
    ''' GlobalizedPropertyDescriptor enhances the base class bay obtaining the display name for a property
    ''' from the resource.
    ''' </summary>
    Public Class GlobalizedPropertyDescriptor
        Inherits PropertyDescriptor

#Region "Fields"

        Private _basePropertyDescriptor As PropertyDescriptor
        Private _langManager As New ResourceManager("ScreenshotStudioDotNet.Core.Strings", Assembly.GetExecutingAssembly)

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GlobalizedPropertyDescriptor" /> class.
        ''' </summary>
        ''' <param name="basePropertyDescriptor">The base property descriptor.</param>
        Public Sub New(ByVal basePropertyDescriptor As PropertyDescriptor)
            MyBase.New(basePropertyDescriptor)
            _basePropertyDescriptor = basePropertyDescriptor
        End Sub

#End Region

#Region "Function Overloads"

        ''' <summary>
        ''' When overridden in a derived class, returns whether resetting an object changes its value.
        ''' </summary>
        ''' <param name="component">The component to test for reset capability.</param>
        ''' <returns>
        ''' true if resetting the component changes its value; otherwise, false.
        ''' </returns>
        Public Overloads Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return _basePropertyDescriptor.CanResetValue(component)
        End Function

        ''' <summary>
        ''' When overridden in a derived class, gets the current value of the property on a component.
        ''' </summary>
        ''' <param name="component">The component with the property for which to retrieve the value.</param>
        ''' <returns>
        ''' The value of a property for a given component.
        ''' </returns>
        Public Overloads Overrides Function GetValue(ByVal component As Object) As Object
            Return _basePropertyDescriptor.GetValue(component)
        End Function

        ''' <summary>
        ''' When overridden in a derived class, resets the value for this property of the component to the default value.
        ''' </summary>
        ''' <param name="component">The component with the property value that is to be reset to the default value.</param>
        Public Overloads Overrides Sub ResetValue(ByVal component As Object)
            _basePropertyDescriptor.ResetValue(component)
        End Sub

        ''' <summary>
        ''' When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
        ''' </summary>
        ''' <param name="component">The component with the property to be examined for persistence.</param>
        ''' <returns>
        ''' true if the property should be persisted; otherwise, false.
        ''' </returns>
        Public Overloads Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return _basePropertyDescriptor.ShouldSerializeValue(component)
        End Function

        ''' <summary>
        ''' When overridden in a derived class, sets the value of the component to a different value.
        ''' </summary>
        ''' <param name="component">The component with the property value that is to be set.</param>
        ''' <param name="value">The new value.</param>
        Public Overloads Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            _basePropertyDescriptor.SetValue(component, value)
        End Sub

#End Region

#Region "Property Overloads"

        ''' <summary>
        ''' When overridden in a derived class, gets a value indicating whether this property is read-only.
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if the property is read-only; otherwise, false.</returns>
        Public Overloads Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return _basePropertyDescriptor.IsReadOnly
            End Get
        End Property

        ''' <summary>
        ''' Gets the collection of attributes for this member.
        ''' </summary>
        ''' <value></value>
        ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> that provides the attributes for this member, or an empty collection if there are no attributes in the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray" />.</returns>
        Public Overloads Overrides ReadOnly Property Attributes() As AttributeCollection
            Get
                Return _basePropertyDescriptor.Attributes
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, gets the type of the component this property is bound to.
        ''' </summary>
        ''' <value></value>
        ''' <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
        Public Overloads Overrides ReadOnly Property ComponentType() As Type
            Get
                Return _basePropertyDescriptor.ComponentType
            End Get
        End Property

        ''' <summary>
        ''' Gets the name that can be displayed in a window, such as a Properties window.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The name to display for the member.</returns>
        Public Overloads Overrides ReadOnly Property DisplayName() As String
            Get
                'sort
                Dim sortOrder As Integer = 0

                For Each attr In _basePropertyDescriptor.Attributes
                    If TypeOf (attr) Is PropertyOrderAttribute Then
                        sortOrder = CType(attr, PropertyOrderAttribute).Order
                    End If
                Next

                Dim sort As String = ""
                For i As Integer = 0 To sortOrder
                    sort &= Chr(31) & Chr(32)
                Next

                ' Get the string from the resources. 
                ' If this fails, then use default display name (usually the property name) 
                Dim s As String = _langManager.GetString(_basePropertyDescriptor.DisplayName)

                Return sort & If((s IsNot Nothing), s, _basePropertyDescriptor.DisplayName)
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the member, as specified in the <see cref="T:System.ComponentModel.DescriptionAttribute" />.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The description of the member. If there is no <see cref="T:System.ComponentModel.DescriptionAttribute" />, the property value is set to the default, which is an empty string ("").</returns>
        Public Overloads Overrides ReadOnly Property Description() As String
            Get
                ' Get the string from the resources. 
                ' If this fails, then use default display name (usually the property name) 
                Dim s As String = _langManager.GetString(_basePropertyDescriptor.DisplayName & " Description")

                Return If((s IsNot Nothing), s, _basePropertyDescriptor.DisplayName & " Description")
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the member.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The name of the member.</returns>
        Public Overloads Overrides ReadOnly Property Name() As String
            Get
                Return _basePropertyDescriptor.Name
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, gets the type of the property.
        ''' </summary>
        ''' <value></value>
        ''' <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
        Public Overloads Overrides ReadOnly Property PropertyType() As Type
            Get
                Return _basePropertyDescriptor.PropertyType
            End Get
        End Property

#End Region
    End Class
End Namespace
