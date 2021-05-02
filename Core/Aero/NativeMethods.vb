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

Imports System.Runtime.InteropServices

Namespace Aero
    Friend Class NativeMethods

#Region "Private Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="NativeMethods" /> class.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region "Function Imports"

        <DllImport("gdi32.dll")> _
        Friend Shared Function BitBlt(ByVal hdc As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As UInt32) As Boolean
        End Function

        <DllImport("gdi32.dll", ExactSpelling:=True)> _
        Friend Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll", SetLastError:=True, ExactSpelling:=True)> _
        Friend Shared Function SaveDC(ByVal hdc As IntPtr) As Integer
        End Function

        <DllImport("gdi32.dll", SetLastError:=True, ExactSpelling:=True)> _
        Friend Shared Function DeleteDC(ByVal hdc As IntPtr) As Boolean
        End Function

        <DllImport("gdi32.dll", SetLastError:=True, ExactSpelling:=True)> _
        Friend Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll", SetLastError:=True, ExactSpelling:=True)> _
        Friend Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
        End Function

        <DllImport("gdi32.dll", SetLastError:=True, ExactSpelling:=True)> _
        Friend Shared Function CreateDIBSection(ByVal hdc As IntPtr, ByRef pbmi As BITMAPINFO, ByVal iUsage As UInt32, ByVal ppvBits As Integer, ByVal hSection As IntPtr, ByVal dwOffset As UInt32) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True, ExactSpelling:=True)> _
        Friend Shared Function ReleaseDC(ByVal hdc As IntPtr, ByVal state As Integer) As Integer
        End Function

        <DllImport("dwmapi.dll", PreserveSig:=False)> _
        Friend Shared Sub DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByVal pMargins As MARGINS)
        End Sub

        <DllImport("dwmapi.dll")> _
        Friend Shared Sub DwmIsCompositionEnabled(ByRef enabledptr As Boolean)
        End Sub

        <DllImport("user32.dll", ExactSpelling:=True, SetLastError:=True)>
 _
        Friend Shared Function GetDC(ByVal hdc As IntPtr) As IntPtr
        End Function

        <DllImport("UxTheme.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)>
 _
        Friend Shared Function DrawThemeTextEx(ByVal hTheme As IntPtr, ByVal hdc As IntPtr, ByVal iPartId As Integer, ByVal iStateId As Integer, ByVal text As String, ByVal iCharCount As Integer, ByVal dwFlags As Integer, ByRef pRect As RECT, ByRef pOptions As DTTOPTS) As Integer
        End Function

        '''<summary>
        '''Set The Window's Theme Attributes
        '''</summary>
        '''<param name="hWnd">The Handle to the Window</param>
        '''<param name="wtype">What Type of Attributes</param>
        '''<param name="attributes">The Attributes to Add/Remove</param>
        '''<param name="size">The Size of the Attributes Struct</param>
        '''<returns>If The Call Was Successful or Not</returns>
        <DllImport("UxTheme.dll")>
 _
        Friend Shared Function SetWindowThemeAttribute(ByVal hWnd As IntPtr, ByVal wtype As WindowThemeAttributeType, ByRef attributes As WTA_OPTIONS, ByVal size As UInteger) As Integer
        End Function

#End Region

#Region "Structures"

        Friend Structure BITMAPINFO
            Public bmiHeader As BITMAPINFOHEADER
            Public bmiColors As RGBQUAD
        End Structure

        Friend Structure RGBQUAD
            Public rgbBlue As Byte
            Public rgbGreen As Byte
            Public rgbRed As Byte
            Public rgbReserved As Byte
        End Structure

        Friend Structure BITMAPINFOHEADER
            Public biSize As Integer
            Public biWidth As Integer
            Public biHeight As Integer
            Public biPlanes As Short
            Public biBitCount As Short
            Public biCompression As Integer
            Public biSizeImage As Integer
            Public biXPelsPerMeter As Integer
            Public biYPelsPerMeter As Integer
            Public biClrUsed As Integer
            Public biClrImportant As Integer
        End Structure

        Friend Structure RECT
            Public left As Integer
            Public top As Integer
            Public right As Integer
            Public bottom As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Friend Class MARGINS
            Public cxLeftWidth As Integer, cxRightWidth As Integer, cyTopHeight As Integer, cyBottomHeight As Integer

            ''' <summary>
            ''' Initializes a new instance of the <see cref="MARGINS" /> class.
            ''' </summary>
            ''' <param name="left">The left.</param>
            ''' <param name="top">The top.</param>
            ''' <param name="right">The right.</param>
            ''' <param name="bottom">The bottom.</param>
            Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
                cxLeftWidth = left
                cyTopHeight = top
                cxRightWidth = right
                cyBottomHeight = bottom
            End Sub
        End Class


        'Text related
        Friend Const DTT_COMPOSITED As Integer = CInt((1 << 13))
        Friend Const DTT_GLOWSIZE As Integer = CInt((1 << 11))

        Friend Const DT_SINGLELINE As Integer = &H20
        Friend Const DT_CENTER As Integer = &H1
        Friend Const DT_VCENTER As Integer = &H4
        Friend Const DT_NOPREFIX As Integer = &H800

        Friend Structure DTTOPTS
            Public dwSize As UInteger
            Public dwFlags As UInteger
            Public crText As UInteger
            Public crBorder As UInteger
            Public crShadow As UInteger
            Public iTextShadowType As Integer
            Public ptShadowOffset As POINTAPI
            Public iBorderSize As Integer
            Public iFontPropId As Integer
            Public iColorPropId As Integer
            Public iStateId As Integer
            Public fApplyOverlay As Integer
            Public iGlowSize As Integer
            Public pfnDrawTextCallback As IntPtr
            Public lParam As Integer
        End Structure

        Friend Structure POINTAPI
            Public x As Integer
            Public y As Integer
        End Structure

        ''' <summary>
        ''' The Options of What Attributes to Add/Remove
        ''' </summary>
        <StructLayout(LayoutKind.Sequential)> _
        Friend Structure WTA_OPTIONS
            Public Flags As UInteger
            Public Mask As UInteger
        End Structure

        '''<summary>
        '''What Type of Attributes? (Only One is Currently Defined)
        '''</summary>
        Friend Enum WindowThemeAttributeType
            WTA_NONCLIENT = 1
        End Enum

#End Region

#Region "Constants"

        ''' <summary>
        ''' Do Not Draw The Caption (Text)
        ''' </summary>
        Friend Shared WTNCA_NODRAWCAPTION As UInteger = &H1

        ''' <summary>
        ''' Do Not Draw the Icon
        ''' </summary>
        Friend Shared WTNCA_NODRAWICON As UInteger = &H2

        Friend Shared BI_RGB As Integer = 0
        Friend Shared SRCCOPY As Integer = &HCC0020
        Friend Shared DIB_RGB_COLORS As Integer = 0

#End Region
    End Class
End Namespace
