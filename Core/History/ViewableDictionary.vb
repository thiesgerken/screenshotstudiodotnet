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
    Public Class ViewableDictionary(Of TKey, TValue)
        Inherits Dictionary(Of TKey, TValue)
        Implements ICustomTypeDescriptor

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ViewableList(Of T)" /> class.
        ''' </summary>
        ''' <param name="baseList">The base list.</param>
        Public Sub New(ByVal baseDict As Dictionary(Of TKey, TValue))
            For Each i In baseDict
                Me.Add(i.Key, i.Value)
            Next
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ViewableList(Of T)" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

#End Region

#Region "ICustomTypeDescriptor Members"

        ''' <summary>
        ''' Returns a collection of custom attributes for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.
        ''' </returns>
        Public Function GetAttributes() As System.ComponentModel.AttributeCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetAttributes
            Return TypeDescriptor.GetAttributes(Me, True)
        End Function

        ''' <summary>
        ''' Returns the class name of this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' The class name of the object, or null if the class does not have a name.
        ''' </returns>
        Public Function GetClassName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetClassName
            Return TypeDescriptor.GetClassName(Me, True)
        End Function

        ''' <summary>
        ''' Returns the name of this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' The name of the object, or null if the object does not have a name.
        ''' </returns>
        Public Function GetComponentName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetComponentName
            Return TypeDescriptor.GetComponentName(Me, True)
        End Function

        ''' <summary>
        ''' Returns a type converter for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.
        ''' </returns>
        Public Function GetConverter() As System.ComponentModel.TypeConverter Implements System.ComponentModel.ICustomTypeDescriptor.GetConverter
            Return New ViewableDictionaryConverter
        End Function

        ''' <summary>
        ''' Returns the default event for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.
        ''' </returns>
        Public Function GetDefaultEvent() As System.ComponentModel.EventDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent
            Return TypeDescriptor.GetDefaultEvent(Me, True)
        End Function

        ''' <summary>
        ''' Returns the default property for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.
        ''' </returns>
        Public Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty
            Return TypeDescriptor.GetDefaultProperty(Me, True)
        End Function

        ''' <summary>
        ''' Returns an editor of the specified type for this instance of a component.
        ''' </summary>
        ''' <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
        ''' <returns>
        ''' An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.
        ''' </returns>
        Public Function GetEditor(ByVal editorBaseType As System.Type) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetEditor
            Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
        End Function

        ''' <summary>
        ''' Returns the events for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.
        ''' </returns>
        Public Function GetEvents() As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, True)
        End Function

        ''' <summary>
        ''' Returns the events for this instance of a component using the specified attribute array as a filter.
        ''' </summary>
        ''' <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.
        ''' </returns>
        Public Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, attributes, True)
        End Function

        ''' <summary>
        ''' Returns the properties for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.
        ''' </returns>
        Public Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
            'Create a new collection object PropertyDescriptorCollection
            Dim pds = New PropertyDescriptorCollection(Nothing)

            'Iterate the list of employees

            For i As Integer = 0 To Me.Count - 1
                Dim pd As New ViewableDictionaryPropertyDescriptor(Of TKey, TValue)(Me, i)
                pds.Add(pd)
            Next

            Return pds
        End Function

        ''' <summary>
        ''' Returns the properties for this instance of a component using the attribute array as a filter.
        ''' </summary>
        ''' <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.
        ''' </returns>
        Public Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
            Return GetProperties()
        End Function

        ''' <summary>
        ''' Returns an object that contains the property described by the specified property descriptor.
        ''' </summary>
        ''' <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
        ''' <returns>
        ''' An <see cref="T:System.Object" /> that represents the owner of the specified property.
        ''' </returns>
        Public Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner
            Return Me
        End Function

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the display title.
        ''' </summary>
        ''' <value>The display title.</value>
        Public Property DisplayTitle As String = "[List, Expand to see the contents]"

#End Region
    End Class
End Namespace
