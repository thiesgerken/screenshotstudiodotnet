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
    ''' <summary>
    ''' GlobalizedObject implements ICustomTypeDescriptor to enable 
    ''' required functionality to describe a type (class).<br></br>
    ''' The main task of this class is to instantiate our own property descriptor 
    ''' of type GlobalizedPropertyDescriptor.  
    ''' </summary>
    Public Class GlobalizedObject
        Implements ICustomTypeDescriptor

#Region "Fields"

        Private _globalizedProps As PropertyDescriptorCollection

#End Region

#Region "ICustomTypeDescriptor Members"

        ''' <summary>
        ''' Returns the class name of this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' The class name of the object, or null if the class does not have a name.
        ''' </returns>
        Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
            Return TypeDescriptor.GetClassName(Me, True)
        End Function

        ''' <summary>
        ''' Returns a collection of custom attributes for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.
        ''' </returns>
        Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
            Return TypeDescriptor.GetAttributes(Me, True)
        End Function

        ''' <summary>
        ''' Returns the name of this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' The name of the object, or null if the object does not have a name.
        ''' </returns>
        Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
            Return TypeDescriptor.GetComponentName(Me, True)
        End Function

        ''' <summary>
        ''' Returns a type converter for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.
        ''' </returns>
        Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
            Return TypeDescriptor.GetConverter(Me, True)
        End Function

        ''' <summary>
        ''' Returns the default event for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.
        ''' </returns>
        Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
            Return TypeDescriptor.GetDefaultEvent(Me, True)
        End Function

        ''' <summary>
        ''' Returns the default property for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.
        ''' </returns>
        Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
            Return TypeDescriptor.GetDefaultProperty(Me, True)
        End Function

        ''' <summary>
        ''' Returns an editor of the specified type for this instance of a component.
        ''' </summary>
        ''' <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
        ''' <returns>
        ''' An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.
        ''' </returns>
        Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
            Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
        End Function

        ''' <summary>
        ''' Returns the events for this instance of a component using the specified attribute array as a filter.
        ''' </summary>
        ''' <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.
        ''' </returns>
        Public Function GetEvents(ByVal attributes As Attribute()) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, attributes, True)
        End Function

        ''' <summary>
        ''' Returns the events for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.
        ''' </returns>
        Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, True)
        End Function

        ''' <summary>
        ''' Called to get the properties of a type.
        ''' </summary>
        ''' <param name="attributes"></param>
        ''' <returns></returns>
        Public Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            If _globalizedProps Is Nothing Then
                ' Get the collection of properties
                Dim baseProps As PropertyDescriptorCollection = TypeDescriptor.GetProperties(Me, attributes, True)

                _globalizedProps = New PropertyDescriptorCollection(Nothing)

                ' For each property use a property descriptor of our own that is able to be globalized
                For Each oProp As PropertyDescriptor In baseProps
                    _globalizedProps.Add(New GlobalizedPropertyDescriptor(oProp))
                Next
            End If
            Return _globalizedProps
        End Function

        ''' <summary>
        ''' Returns the properties for this instance of a component.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.
        ''' </returns>
        Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            ' Only do once
            If _globalizedProps Is Nothing Then
                ' Get the collection of properties
                Dim baseProps As PropertyDescriptorCollection = TypeDescriptor.GetProperties(Me, True)
                _globalizedProps = New PropertyDescriptorCollection(Nothing)

                ' For each property use a property descriptor of our own that is able to be globalized
                For Each oProp As PropertyDescriptor In baseProps
                    _globalizedProps.Add(New GlobalizedPropertyDescriptor(oProp))
                Next
            End If
            Return _globalizedProps
        End Function

        ''' <summary>
        ''' Returns an object that contains the property described by the specified property descriptor.
        ''' </summary>
        ''' <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
        ''' <returns>
        ''' An <see cref="T:System.Object" /> that represents the owner of the specified property.
        ''' </returns>
        Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
            Return Me
        End Function

#End Region
    End Class
End Namespace
