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
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports ScreenshotStudioDotNet.Core.Settings
Imports System.Xml

Namespace Controls
    Public Class FormStateSaver
        Inherits Component

#Region "Fields"

        Private _form As Form

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the form to save.
        ''' </summary>
        ''' <value>The form to save.</value>
        Public Property Form() As Form
            Get
                Return _form
            End Get
            Set(ByVal value As Form)
                RemoveEventsFromForm()
                _form = value
                AddEventsToForm()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [save size].
        ''' </summary>
        ''' <value><c>true</c> if [save size]; otherwise, <c>false</c>.</value>
        <DefaultValue(False)> _
        Public Property SaveSize() As Boolean

        ''' <summary>
        ''' Gets or sets a value indicating whether [save location].
        ''' </summary>
        ''' <value><c>true</c> if [save location]; otherwise, <c>false</c>.</value>
        <DefaultValue(True)> _
        Public Property SaveLocation() As Boolean = True

        ''' <summary>
        ''' Gets or sets a value indicating whether [save window state].
        ''' </summary>
        ''' <value><c>true</c> if [save window state]; otherwise, <c>false</c>.</value>
        <DefaultValue(False)> _
        Public Property SaveWindowState() As Boolean

#End Region

#Region "Functions"

        ''' <summary>
        ''' Adds the events to form.
        ''' </summary>
        Private Sub AddEventsToForm()
            If Not DesignMode AndAlso _form IsNot Nothing Then
                AddHandler _form.Load, AddressOf Form_Load
                AddHandler _form.FormClosing, AddressOf Form_FormClosing
            End If
        End Sub

        ''' <summary>
        ''' Removes the events from form.
        ''' </summary>
        Private Sub RemoveEventsFromForm()
            If Not DesignMode AndAlso _form IsNot Nothing Then
                RemoveHandler _form.Load, AddressOf Form_Load
                RemoveHandler _form.FormClosing, AddressOf Form_FormClosing
            End If
        End Sub

        ''' <summary>
        ''' Gets the XML path.
        ''' </summary>
        ''' <returns></returns>
        Protected Function GetXmlPath() As String
            Return Path.Combine(StaticProperties.SettingsDirectory, _form.Name + ".FormState.xml")
        End Function

        ''' <summary>
        ''' Loads the state of the form.
        ''' </summary>
        Public Sub LoadFormState()
            If _form Is Nothing OrElse (Not _SaveLocation AndAlso Not _SaveSize AndAlso Not _SaveWindowState) Then
                Return
            End If

            Dim path As String = GetXmlPath()
            If File.Exists(path) Then
                Dim xml As New XmlDocument()
                xml.Load(path)
                If xml("Form") IsNot Nothing Then
                    Dim root As XmlNode = xml("Form")
                    If _SaveWindowState AndAlso root("WindowState") IsNot Nothing Then
                        _form.WindowState = _
                            DirectCast([Enum].Parse(GetType(FormWindowState), root("WindowState").InnerText),  _
                                FormWindowState)
                    End If
                    If _SaveLocation AndAlso root("Location") IsNot Nothing Then
                        If root("Location")("X") IsNot Nothing Then
                            _form.Left = Convert.ToInt32(root("Location")("X").InnerText)
                        End If
                        If root("Location")("Y") IsNot Nothing Then
                            _form.Top = Convert.ToInt32(root("Location")("Y").InnerText)
                        End If
                    End If
                    If _SaveSize AndAlso root("Size") IsNot Nothing Then
                        If root("Size")("Width") IsNot Nothing Then
                            _form.Width = Convert.ToInt32(root("Size")("Width").InnerText)
                        End If
                        If root("Size")("Height") IsNot Nothing Then
                            _form.Height = Convert.ToInt32(root("Size")("Height").InnerText)
                        End If
                    End If
                End If
            Else
                _form.Left = CInt(Screen.PrimaryScreen.WorkingArea.Width / 2 - Form.Width / 2)
                _form.Top = CInt(Screen.PrimaryScreen.WorkingArea.Height / 2 - Form.Height / 2)
            End If
        End Sub

        ''' <summary>
        ''' Saves the state of the form.
        ''' </summary>
        Public Sub SaveFormState()
            If _form Is Nothing OrElse (Not _SaveLocation AndAlso Not _SaveSize AndAlso Not _SaveWindowState) Then
                Return
            End If

            Dim xml As New XmlDocument()
            Dim root As XmlNode = xml.AppendChild(xml.CreateElement("Form"))
            Dim bounds As Rectangle = _form.Bounds
            If _form.WindowState <> FormWindowState.Normal Then
                bounds = _form.RestoreBounds
            End If
            If _SaveLocation Then
                Dim loc As XmlNode = root.AppendChild(xml.CreateElement("Location"))
                loc.AppendChild(xml.CreateElement("X")).InnerText = bounds.X.ToString()
                loc.AppendChild(xml.CreateElement("Y")).InnerText = bounds.Y.ToString()
            End If
            If _SaveSize Then
                Dim size As XmlNode = root.AppendChild(xml.CreateElement("Size"))
                size.AppendChild(xml.CreateElement("Width")).InnerText = bounds.Width.ToString()
                size.AppendChild(xml.CreateElement("Height")).InnerText = bounds.Height.ToString()
            End If
            If _SaveWindowState Then
                root.AppendChild(xml.CreateElement("WindowState")).InnerText = _form.WindowState.ToString()
            End If
            xml.Save(GetXmlPath())
        End Sub

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the Load event of the Form control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub Form_Load(ByVal sender As Object, ByVal e As EventArgs)
            LoadFormState()
        End Sub

        ''' <summary>
        ''' Handles the FormClosing event of the Form control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub Form_FormClosing(ByVal sender As Object, ByVal e As EventArgs)
            SaveFormState()
        End Sub

#End Region
    End Class
End Namespace
