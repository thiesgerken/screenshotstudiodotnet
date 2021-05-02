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

Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports System.Reflection
Imports ScreenshotStudioDotNet.Core.Macros
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace Misc
    Public Module Extensions


        ''' <summary>
        ''' Modifies the alpha.
        ''' </summary>
        ''' <param name="color">The color.</param>
        ''' <param name="alpha">The alpha.</param>
        ''' <returns></returns>
        <Extension()>
        Public Function ModifyAlpha(ByVal color As Color, ByVal alpha As Integer) As Color
            Return color.FromArgb(color.A, color.R, color.G, color.B)
        End Function

        ''' <summary>
        ''' Draws the button.
        ''' </summary>
        ''' <param name="width">The width.</param>
        ''' <param name="height">The height.</param>
        ''' <param name="color">The color.</param>
        ''' <returns></returns>
        <Extension()>
        Public Sub DrawButton(ByVal graphics As Graphics, ByVal location As Point, ByVal size As Size, ByVal fillColor As Color, ByVal text As String, ByVal fontName As String, ByVal fontSize As Single, ByVal roundRadius As Integer)
            'Declare the Bitmap and Graphics
            Dim bitmap As New Bitmap(size.Width, size.Height)
            Dim g As Graphics = graphics.FromImage(bitmap)

            'Set the Quality to high (Aliasing is bad)
            g.CompositingQuality = CompositingQuality.HighQuality
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBilinear

            Dim rect As New Rectangle(0, 0, size.Width, size.Height)

            g.FillRectangle(New SolidBrush(fillColor), rect)

            'Background Gradient
            Dim bgColor1 As Color = Color.FromArgb(255, CInt(fillColor.R * 7 / 9), CInt(fillColor.G * 7 / 9), CInt(fillColor.B * 7 / 9))
            Dim bgColor2 As Color = Color.FromArgb(0, 255, 255, 255)

            Dim backgroundBrush As New LinearGradientBrush(rect, bgColor1, bgColor2, LinearGradientMode.Vertical)
            g.FillRectangle(backgroundBrush, rect)

            'Elliptic Shadow
            Dim circle As New GraphicsPath
            circle.AddEllipse(New RectangleF(CSng(-1 / 2 * size.Width), CSng(-1 / 2 * size.Height), size.Width * 2, size.Height * 2))

            Dim shadowBrush As New PathGradientBrush(circle)
            shadowBrush.CenterPoint = New Point(CInt(1 / 2 * size.Width), CInt(1 / 2 * size.Height))
            shadowBrush.CenterColor = Color.Transparent
            shadowBrush.SurroundColors = New Color() {Color.FromArgb(147, 0, 0, 0)}
            g.FillRectangle(shadowBrush, rect)

            'Draw the text
            Dim textSize = g.MeasureString(text, New Font(fontName, fontSize))
            Dim format As New StringFormat()
            ' format.Alignment = StringAlignment.Near

            Dim shortendedName = text
            While textSize.Width > size.Width And shortendedName.Length > 4
                shortendedName = shortendedName.Substring(0, shortendedName.Length - 4) & "..."
                textSize = g.MeasureString(shortendedName, New Font(fontName, fontSize), 99999, format)
            End While

            Dim posX As Integer = CInt(size.Width / 2 - textSize.Width / 2)
            Dim posY As Integer = CInt(size.Height / 2 - textSize.Height / 2)

            Dim foreColor As Color = Color.FromArgb(255, 255 - 35, 255 - 35, 255 - 35)

            g.DrawString(shortendedName, New Font(fontName, fontSize), New SolidBrush(foreColor), posX, posY, format)

            'Add Reflection
            Dim reflectionBrush As New LinearGradientBrush(rect, Color.FromArgb(30, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), LinearGradientMode.Vertical)
            g.FillRectangle(reflectionBrush, New Rectangle(0, 0, size.Width, CInt(size.Height / 2)))

            'Clean Up
            g.Dispose()

            'We want rounded borders, so we need another bitmap to paint the 
            '1. bitmap in
            Dim realBitmap As New Bitmap(size.Width, size.Height)
            g = graphics.FromImage(realBitmap)

            'the Quality thing
            g.CompositingQuality = CompositingQuality.HighQuality
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBilinear

            'Grab a rounded rectangle and fill it with the button
            Dim buttonPath As GraphicsPath = GraphicHelpers.GetRoundedRectangle(New Rectangle(0, 0, size.Width, size.Height), roundRadius)
            g.FillPath(New TextureBrush(bitmap), buttonPath)

            'Clean up, of course
            g.Dispose()

            'return the bitmap 
            graphics.DrawImage(realBitmap, location)
        End Sub

        '''' <summary>
        '''' Draws the S.
        '''' </summary>
        '''' <param name="graphicsToPaintOn">The graphics to paint on.</param>
        '''' <param name="location">The location.</param>
        '''' <param name="size">The size.</param>
        '<Extension()>
        'Sub DrawS(ByVal graphicsToPaintOn As Graphics, ByVal brace As Brace)
        'Draw Curly Brace as connector between the components
        'Dim bmp As New Bitmap(brace.Region.Size.Width, brace.Region.Size.Height)
        'Dim g As Graphics = Graphics.FromImage(bmp)
        'g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        'g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

        'Dim lastPoint As Point

        'For x As Double = -brace.Region.Size.Height / 2 To brace.Region.Size.Height / 2
        ''We need a polynomical function of the form f(x)=a*x^3+b*x2+cx+d
        ''The curve to paint has the following requirements:   
        ''(f = the function, fx is the x. derivate of the function, dx = width of the curve (height of the bitmap), dy = height of the curve (width of the bitmap))
        ''f(0)     = 0     -> a*0 + b*0 + c*0 +d = 0 -> d = 0
        ''f2(0)    = 0     -> 3*a*0 + c = 0          -> c = 0
        ''f(-dx/2) = -dy/2 -> a*(-dx/2)^3 = -dy/2    -> a = 4dy/dx^3
        ''--> f(x)=(4dy/dx^3)x^3
        'Dim fx As Double = 4 * brace.Region.Size.Width / brace.Region.Size.Height ^ 3 * x ^ 3

        'Dim point As New Point(CInt(fx - 1 + brace.Region.Size.Width / 2), CInt(x - 1 + brace.Region.Size.Height / 2))

        'If Not lastPoint.Equals(Nothing) Then
        'g.DrawLine(New Pen(New SolidBrush(brace.Color), 2), lastPoint, point)
        'End If

        'lastPoint = point
        'Next

        'graphicsToPaintOn.DrawImage(bmp, brace.Region.Location)
        'End Sub


        ''' <summary>
        ''' Inserts a item at a specified position in a dictionary.
        ''' </summary>
        ''' <typeparam name="TKey">The type of the key.</typeparam>
        ''' <typeparam name="TValue">The type of the value.</typeparam>
        ''' <param name="dic">The dictionary to write in</param>
        ''' <param name="item">The item.</param>
        ''' <param name="index">The index.</param>
        <Extension()>
        Public Sub InsertAt(Of TKey, TValue)(ByVal dic As Dictionary(Of TKey, TValue), ByVal item As KeyValuePair(Of TKey, TValue), ByVal index As Integer)
            Dim newDic As New Dictionary(Of TKey, TValue)

            Dim added As Boolean = False

            Dim currentIndex As Integer = 0

            For Each i In dic
                If currentIndex = index Then
                    newDic.Add(item.Key, item.Value)
                    added = True
                End If

                newDic.Add(i.Key, i.Value)
                currentIndex += 1
            Next

            If Not added Then
                newDic.Add(item.Key, item.Value)
            End If

            dic.Clear()

            For Each i In newDic
                dic.Add(i.Key, i.Value)
            Next
        End Sub

        ''' <summary>
        ''' Clones a control.
        ''' </summary>
        ''' <param name="c">The control to clone</param>
        ''' <returns>a copy of the control</returns>
        <Extension()>
        Public Function Clone(ByVal c As MacroComponent) As MacroComponent
            Dim excludeProps As New List(Of String)
            excludeProps.Add("WindowTarget")

            Dim type As Type = c.GetType()
            Dim properties() As PropertyInfo = type.GetProperties()
            Dim retObject As MacroComponent = CType(type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, Nothing, c, Nothing), MacroComponent)

            For Each prop In properties
                If prop.CanWrite And Not excludeProps.Contains(prop.Name) Then
                    prop.SetValue(retObject, prop.GetValue(c, Nothing), Nothing)
                End If
            Next

            retObject.Init()
            Return retObject
        End Function

        ''' <summary>
        ''' Determines whether [contains] [the specified list].
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <param name="name">The name.</param>
        ''' <returns>
        ''' <c>true</c> if [contains] [the specified list]; otherwise, <c>false</c>.
        ''' </returns>
        <Extension()>
        Public Function Contains(ByVal list As List(Of MacroComponent), ByVal name As String) As Boolean
            For Each i In list
                If i.Name = name Then
                    Return True
                End If
            Next

            Return False
        End Function

        ''' <summary>
        ''' Gets the name of the item with.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <param name="name">The name.</param>
        ''' <returns></returns>
        <Extension()>
        Public Function GetItemWithName(ByVal list As List(Of MacroComponent), ByVal name As String) As MacroComponent
            For Each i In list
                If i.Name = name Then
                    Return i
                End If
            Next

            Throw New ArgumentException("An item with this name does not exist.")
        End Function

        ''' <summary>
        ''' Copies the specified list.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <returns></returns>
        <Extension()>
        Public Function Copy(ByVal list As List(Of MacroComponent)) As List(Of MacroComponent)
            Dim listCopy As New List(Of MacroComponent)

            For Each l In list
                listCopy.Add(l)
            Next

            Return listCopy
        End Function
    End Module
End Namespace
