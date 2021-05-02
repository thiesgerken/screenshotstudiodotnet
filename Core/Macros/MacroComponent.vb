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

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports ScreenshotStudioDotNet.Core.Misc

Namespace Macros
    Public MustInherit Class MacroComponent
        Implements IDisposable
        
#Region "Properties"

        ''' <summary>
        ''' Gets or sets the type of the component.
        ''' </summary>
        ''' <value>The type of the component.</value>
        Protected MustOverride ReadOnly Property ComponentType As ComponentTypes

        ''' <summary>
        ''' Gets the text.
        ''' </summary>
        ''' <value>The text.</value>
        Protected MustOverride ReadOnly Property Text As String

        ''' <summary>
        ''' Gets or sets a value indicating whether [more than one occurence allowed].
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if [more than one occurence allowed]; otherwise, <c>false</c>.
        ''' </value>
        Public Property MoreThanOneOccurenceAllowed As Boolean = True

        ''' <summary>
        ''' Gets or sets a value indicating whether [drag drop enabled].
        ''' </summary>
        ''' <value><c>true</c> if [drag drop enabled]; otherwise, <c>false</c>.</value>
        Public Property InUse As Boolean = False

        ''' <summary>
        ''' Gets or sets the size.
        ''' </summary>
        ''' <value>The size.</value>
        Public Property Size As New Size(1, 1)

        ''' <summary>
        ''' Gets or sets the location.
        ''' </summary>
        ''' <value>The location.</value>
        Public Property Location As New Point(1, 1)

        ''' <summary>
        ''' Gets or sets the width.
        ''' </summary>
        ''' <value>The width.</value>
        Public Property Width As Integer
            Get
                Return Me.Size.Width
            End Get
            Set(ByVal value As Integer)
                _Size.Width = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the height.
        ''' </summary>
        ''' <value>The height.</value>
        Public Property Height As Integer
            Get
                Return Me.Size.Height
            End Get
            Set(ByVal value As Integer)
                _Size.Height = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the left.
        ''' </summary>
        ''' <value>The left.</value>
        Public Property Left As Integer
            Get
                Return _Location.X
            End Get
            Set(ByVal value As Integer)
                _Location.X = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the top.
        ''' </summary>
        ''' <value>The top.</value>
        Public Property Top As Integer
            Get
                Return _Location.Y
            End Get
            Set(ByVal value As Integer)
                _Location.Y = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the right.
        ''' </summary>
        ''' <value>The right.</value>
        Public ReadOnly Property Right As Integer
            Get
                Return _Location.X + _Size.Width
            End Get
        End Property

        ''' <summary>
        ''' Gets the bottom.
        ''' </summary>
        ''' <value>The bottom.</value>
        Public ReadOnly Property Bottom As Integer
            Get
                Return _Location.Y + _Size.Height
            End Get
        End Property

        ''' <summary>
        ''' Gets the client rectangle.
        ''' </summary>
        ''' <value>The client rectangle.</value>
        Public ReadOnly Property ClientRectangle As Rectangle
            Get
                Return New Rectangle(Me.Location, Me.Size)
            End Get
        End Property

        ''' <summary>
        ''' Gets the parent form.
        ''' </summary>
        ''' <value>The parent form.</value>
        Public Property ParentForm As MacroGenerator
            Get
                Return _parentForm
            End Get
            Set(ByVal value As MacroGenerator)
                _parentForm = value
                Init(True)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name.
        ''' </summary>
        ''' <value>The name.</value>
        Public Property Name As String

        ''' <summary>
        ''' Gets or sets a value indicating whether this <see cref="MacroComponent" /> is visible.
        ''' </summary>
        ''' <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        Public Property Visible As Boolean = True

        ''' <summary>
        ''' Gets or sets a value indicating whether this <see cref="MacroComponent" /> is inited.
        ''' </summary>
        ''' <value><c>true</c> if inited; otherwise, <c>false</c>.</value>
        Protected Property Inited As Boolean = False

        ''' <summary>
        ''' Gets a value indicating whether [delete able].
        ''' </summary>
        ''' <value><c>true</c> if [delete able]; otherwise, <c>false</c>.</value>
        Protected Overridable ReadOnly Property DeleteAble As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the context menu.
        ''' </summary>
        ''' <value>The context menu.</value>
        Protected Property ContextMenu As New ContextMenuStrip

#End Region

#Region "Fields"

        Private WithEvents _parentForm As MacroGenerator

#End Region

#Region "Event Handlers"

        ''' <summary>
        ''' Handles the MouseDown event of the MacroComponent control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub MacroComponent_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _parentForm.MouseMove
            If e.Button = Windows.Forms.MouseButtons.Left And Not InUse And Me.ClientRectangle.Contains(New Point(e.X, e.Y)) And Not _parentForm.Dragging And Me.Visible Then
                _parentForm.Dragging = True
                _parentForm.DoDragDrop(Me.Name, If(MoreThanOneOccurenceAllowed, DragDropEffects.Copy, DragDropEffects.Move))
            End If
        End Sub

        ''' <summary>
        ''' Handles the MouseUp event of the _parentForm control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        Private Sub parentForm_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _parentForm.MouseUp
            If e.Button = MouseButtons.Right And Not _parentForm.Dragging And Me.Visible And Me.IsOnPoint(e.Location) And Me.InUse Then
                ContextMenu.Show(ParentForm, e.Location)
            End If

            _parentForm.Dragging = False
        End Sub

        ''' <summary>
        ''' Handles the Paint event of the MacroComponent control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        Private Sub MacroComponent_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles _parentForm.Paint
            If Me.Visible Then
                Dim alpha As Integer = 255
                Dim backgroundColor As Color = Color.White.ModifyAlpha(alpha)

                Select Case Me.ComponentType
                    Case ComponentTypes.Disabled
                        backgroundColor = Color.Gray.ModifyAlpha(alpha)
                    Case ComponentTypes.Delay
                        backgroundColor = Color.BlueViolet.ModifyAlpha(alpha)
                    Case ComponentTypes.Output
                        backgroundColor = Color.Blue.ModifyAlpha(alpha)
                    Case ComponentTypes.Effect
                        backgroundColor = Color.Red.ModifyAlpha(alpha)
                    Case ComponentTypes.Multiple
                        backgroundColor = Color.Orange.ModifyAlpha(alpha)
                    Case ComponentTypes.Trigger
                        backgroundColor = Color.DarkTurquoise.ModifyAlpha(alpha)
                    Case ComponentTypes.Type
                        backgroundColor = Color.Green.ModifyAlpha(alpha)
                End Select

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality

                Extensions.DrawButton(e.Graphics, Me.Location, Me.Size, backgroundColor, Me.Text, "Segoe UI", If(InUse, 12, 10), If(InUse, 8, 5))
            End If
        End Sub


        ''' <summary>
        ''' Handles the Click event of the mnuDelete control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
            ParentForm.DeleteComponentFromMacro(Me)
        End Sub

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MacroComponent" /> class.
        ''' </summary>
        ''' <param name="form">The form.</param>
        Public Sub New(ByVal form As MacroGenerator, ByVal name As String)
            _parentForm = form
            _Name = name
            Init()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MacroComponent" /> class.
        ''' </summary>
        Public Sub New()
            Init()
        End Sub

#End Region

#Region "Functions"

        ''' <summary>
        ''' Puts the in toolbox.
        ''' </summary>
        ''' <param name="buttonSize">Size of the button.</param>
        ''' <param name="number">The number.</param>
        ''' <param name="offset">The offset.</param>
        ''' <param name="space">The space.</param>
        Public Function PutInToolbox(ByVal buttonSize As Size, ByVal number As Integer, ByVal offset As Point, ByVal space As Integer) As Integer
            Me.Width = buttonSize.Width
            Me.Height = buttonSize.Height
            Me.Left = offset.X
            Me.Top = offset.Y + number * (Me.Height + space)
            Me.InUse = False
           Return number + Math.Abs(CInt(Me.Visible)) * 1
        End Function

        ''' <summary>
        ''' Sets the location.
        ''' </summary>
        ''' <param name="control">The control.</param>
        ''' <param name="column">The column.</param>
        ''' <param name="columnCount">The column count.</param>
        ''' <param name="row">The row.</param>
        ''' <param name="rowCount">The row count.</param>
        Public Sub Arrange(ByVal layoutarea As Rectangle, ByVal buttonSize As Size, ByVal column As Integer, ByVal columnCount As Integer, ByVal row As Integer, ByVal rowCount As Integer)
            Me.Visible = True
            Me.InUse = True

            Dim yMargin = (layoutarea.Height - rowCount * buttonSize.Height) / (rowCount + 1)

            Dim xMargin As Integer = 0
            If columnCount > 1 Then
                xMargin = CInt((layoutarea.Width - columnCount * buttonSize.Width) / (columnCount - 1))
            End If

            Dim xPos As Integer = column * (buttonSize.Width + xMargin)
            Dim yPos As Integer = CInt((buttonSize.Height + yMargin) * row + yMargin)

            Me.Location = New Point(layoutarea.Left + xPos, layoutarea.Top + yPos)
            Me.Size = New Size(buttonSize.Width, buttonSize.Height)
        End Sub

        ''' <summary>
        ''' Determines whether [is on point] [the specified p].
        ''' </summary>
        ''' <param name="p">The p.</param>
        ''' <returns>
        ''' <c>true</c> if [is on point] [the specified p]; otherwise, <c>false</c>.
        ''' </returns>
        Public Function IsOnPoint(ByVal p As Point) As Boolean
            If Me.ClientRectangle.Contains(p) And Me.Visible Then Return True
            Return False
        End Function

        ''' <summary>
        ''' Determines whether [is on point] [the specified p].
        ''' </summary>
        ''' <param name="p">The p.</param>
        ''' <param name="formerResult">if set to <c>true</c> [former result].</param>
        ''' <returns>
        ''' <c>true</c> if [is on point] [the specified p]; otherwise, <c>false</c>.
        ''' </returns>
        Public Function IsOnPoint(ByVal p As Point, ByVal formerResult As Boolean) As Boolean
            Return formerResult Or Me.IsOnPoint(p)
        End Function

        ''' <summary>
        ''' Inits this instance.
        ''' </summary>
        Public Overridable Sub Init(ByVal force As Boolean)
            If (Not Inited) Or force Then
                ContextMenu.Items.Clear()

                If DeleteAble Then
                    Dim mnuDelete As New ToolStripMenuItem("Delete")
                    mnuDelete.Name = "mnuDelete"
                    mnuDelete.Image = My.Resources.delete_12
                    ContextMenu.Items.Add(mnuDelete)

                    RemoveHandler mnuDelete.Click, AddressOf mnuDelete_Click
                    AddHandler mnuDelete.Click, AddressOf mnuDelete_Click
                End If

                Inited = True
            End If
        End Sub

        ''' <summary>
        ''' Inits this instance.
        ''' </summary>
        Public Overridable Sub Init()
            Init(False)
        End Sub

#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources
        ''' </summary>
        ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Me.ParentForm = Nothing
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>
        ''' Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.
        ''' </summary>
        Protected Overrides Sub Finalize()
            'Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(False)
            MyBase.Finalize()
        End Sub

        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
