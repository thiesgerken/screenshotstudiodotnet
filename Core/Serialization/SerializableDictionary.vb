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

Imports System.Xml.Serialization
Imports System.Xml.Schema
Imports System.Xml

Namespace Serialization
    <XmlRoot("dictionary")> _
    Public Class SerializableDictionary(Of TKey, TValue)
        Inherits Dictionary(Of TKey, TValue)
        Implements IXmlSerializable

#Region "IXmlSerializable Members"

        ''' <summary>
        ''' This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        ''' </returns>
        Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
            Return Nothing
        End Function

        ''' <summary>
        ''' Generates an object from its XML representation.
        ''' </summary>
        ''' <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        Public Sub ReadXml(ByVal reader As XmlReader) Implements IXmlSerializable.ReadXml
            Dim keySerializer As New XmlSerializer(GetType(TKey))
            Dim valueSerializer As New XmlSerializer(GetType(TValue))

            Dim wasEmpty As Boolean = reader.IsEmptyElement

            reader.Read()

            If (wasEmpty) Then Return

            While Not reader.NodeType = XmlNodeType.EndElement
                reader.ReadStartElement("item")
                reader.ReadStartElement("key")

                Dim key As TKey = CType(keySerializer.Deserialize(reader), TKey)

                reader.ReadEndElement()
                reader.ReadStartElement("value")

                Dim value As TValue = CType(valueSerializer.Deserialize(reader), TValue)

                reader.ReadEndElement()


                Me.Add(key, value)

                reader.ReadEndElement()
                reader.MoveToContent()
            End While

            reader.ReadEndElement()

        End Sub

        ''' <summary>
        ''' Converts an object into its XML representation.
        ''' </summary>
        ''' <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        Public Sub WriteXml(ByVal writer As XmlWriter) Implements IXmlSerializable.WriteXml
            Dim keySerializer As XmlSerializer = New XmlSerializer(GetType(TKey))
            Dim valueSerializer As XmlSerializer = New XmlSerializer(GetType(TValue))

            For Each key As TKey In Me.Keys
                writer.WriteStartElement("item")
                writer.WriteStartElement("key")
                keySerializer.Serialize(writer, key)
                writer.WriteEndElement()
                writer.WriteStartElement("value")

                Dim value As TValue = Me(key)

                valueSerializer.Serialize(writer, value)
                writer.WriteEndElement()
                writer.WriteEndElement()
            Next
        End Sub

#End Region
    End Class
End Namespace
