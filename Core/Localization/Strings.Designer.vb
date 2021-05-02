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

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class Strings
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("ScreenshotStudioDotNet.Core.Strings", GetType(Strings).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Date.
        '''</summary>
        Friend Shared ReadOnly Property _date() As String
            Get
                Return ResourceManager.GetString("_date", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Default.
        '''</summary>
        Friend Shared ReadOnly Property _default() As String
            Get
                Return ResourceManager.GetString("_default", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to User Selection.
        '''</summary>
        Friend Shared ReadOnly Property Ask() As String
            Get
                Return ResourceManager.GetString("Ask", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Bounds.
        '''</summary>
        Friend Shared ReadOnly Property Bounds() As String
            Get
                Return ResourceManager.GetString("Bounds", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cancel.
        '''</summary>
        Friend Shared ReadOnly Property cancel() As String
            Get
                Return ResourceManager.GetString("cancel", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to There is already another colorization with the same name..
        '''</summary>
        Friend Shared ReadOnly Property colorError() As String
            Get
                Return ResourceManager.GetString("colorError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Create.
        '''</summary>
        Friend Shared ReadOnly Property create() As String
            Get
                Return ResourceManager.GetString("create", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter a name for the new Colorization..
        '''</summary>
        Friend Shared ReadOnly Property createTask() As String
            Get
                Return ResourceManager.GetString("createTask", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Create New Colorization.
        '''</summary>
        Friend Shared ReadOnly Property createText() As String
            Get
                Return ResourceManager.GetString("createText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creation failed.
        '''</summary>
        Friend Shared ReadOnly Property creationFailed() As String
            Get
                Return ResourceManager.GetString("creationFailed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Delay.
        '''</summary>
        Friend Shared ReadOnly Property Delay() As String
            Get
                Return ResourceManager.GetString("Delay", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Are you sure that you want to delete all History entries?.
        '''</summary>
        Friend Shared ReadOnly Property DeleteAllQInstruction() As String
            Get
                Return ResourceManager.GetString("DeleteAllQInstruction", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to This action will delete the history irreversibly..
        '''</summary>
        Friend Shared ReadOnly Property DeleteAllQText() As String
            Get
                Return ResourceManager.GetString("DeleteAllQText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Are you sure that you want to delete this entry?.
        '''</summary>
        Friend Shared ReadOnly Property DeleteEntryQInstruction() As String
            Get
                Return ResourceManager.GetString("DeleteEntryQInstruction", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to The history entry will be lost after the deletion..
        '''</summary>
        Friend Shared ReadOnly Property DeleteEntryQText() As String
            Get
                Return ResourceManager.GetString("DeleteEntryQText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Are you sure that you want to clear the log file?.
        '''</summary>
        Friend Shared ReadOnly Property DeleteLogQInstruction() As String
            Get
                Return ResourceManager.GetString("DeleteLogQInstruction", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to The log could be important to track errors..
        '''</summary>
        Friend Shared ReadOnly Property DeleteLogQText() As String
            Get
                Return ResourceManager.GetString("DeleteLogQText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Do not ask this again.
        '''</summary>
        Friend Shared ReadOnly Property DontAskAgain() As String
            Get
                Return ResourceManager.GetString("DontAskAgain", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Ellipse.
        '''</summary>
        Friend Shared ReadOnly Property Ellipse() As String
            Get
                Return ResourceManager.GetString("Ellipse", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while loading the Screenshot.
        '''</summary>
        Friend Shared ReadOnly Property ErrorLoading() As String
            Get
                Return ResourceManager.GetString("ErrorLoading", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Filesize.
        '''</summary>
        Friend Shared ReadOnly Property FileSize() As String
            Get
                Return ResourceManager.GetString("FileSize", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while determining the filesize..
        '''</summary>
        Friend Shared ReadOnly Property FileSizeError() As String
            Get
                Return ResourceManager.GetString("FileSizeError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to found.
        '''</summary>
        Friend Shared ReadOnly Property Found() As String
            Get
                Return ResourceManager.GetString("Found", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Item.
        '''</summary>
        Friend Shared ReadOnly Property Item() As String
            Get
                Return ResourceManager.GetString("Item", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Items.
        '''</summary>
        Friend Shared ReadOnly Property Items() As String
            Get
                Return ResourceManager.GetString("Items", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Macro.
        '''</summary>
        Friend Shared ReadOnly Property Macro() As String
            Get
                Return ResourceManager.GetString("Macro", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Name.
        '''</summary>
        Friend Shared ReadOnly Property MacroName() As String
            Get
                Return ResourceManager.GetString("MacroName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Multiple Interval.
        '''</summary>
        Friend Shared ReadOnly Property MultipleInterval() As String
            Get
                Return ResourceManager.GetString("MultipleInterval", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to No Entries found.
        '''</summary>
        Friend Shared ReadOnly Property NoEntry() As String
            Get
                Return ResourceManager.GetString("NoEntry", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to None.
        '''</summary>
        Friend Shared ReadOnly Property None() As String
            Get
                Return ResourceManager.GetString("None", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Number.
        '''</summary>
        Friend Shared ReadOnly Property Number() As String
            Get
                Return ResourceManager.GetString("Number", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Output.
        '''</summary>
        Friend Shared ReadOnly Property Output() As String
            Get
                Return ResourceManager.GetString("Output", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Informations from Output-Plugin.
        '''</summary>
        Friend Shared ReadOnly Property OutputInformation() As String
            Get
                Return ResourceManager.GetString("OutputInformation", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Outputs used.
        '''</summary>
        Friend Shared ReadOnly Property OutputsUsed() As String
            Get
                Return ResourceManager.GetString("OutputsUsed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to (only Window) Process name.
        '''</summary>
        Friend Shared ReadOnly Property ProcessName() As String
            Get
                Return ResourceManager.GetString("ProcessName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Rectangle.
        '''</summary>
        Friend Shared ReadOnly Property Rectangle() As String
            Get
                Return ResourceManager.GetString("Rectangle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Relevance.
        '''</summary>
        Friend Shared ReadOnly Property Relevance() As String
            Get
                Return ResourceManager.GetString("Relevance", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Displays.
        '''</summary>
        Friend Shared ReadOnly Property Screens() As String
            Get
                Return ResourceManager.GetString("Screens", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Screenshot.
        '''</summary>
        Friend Shared ReadOnly Property Screenshot() As String
            Get
                Return ResourceManager.GetString("Screenshot", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Search.
        '''</summary>
        Friend Shared ReadOnly Property Search() As String
            Get
                Return ResourceManager.GetString("Search", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to An Error occured while searching..
        '''</summary>
        Friend Shared ReadOnly Property searchError() As String
            Get
                Return ResourceManager.GetString("searchError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Searching ....
        '''</summary>
        Friend Shared ReadOnly Property searching() As String
            Get
                Return ResourceManager.GetString("searching", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to (only Region) Shape.
        '''</summary>
        Friend Shared ReadOnly Property Shape() As String
            Get
                Return ResourceManager.GetString("Shape", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Show Quickstart.
        '''</summary>
        Friend Shared ReadOnly Property showQS() As String
            Get
                Return ResourceManager.GetString("showQS", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Time Taken.
        '''</summary>
        Friend Shared ReadOnly Property TimeTaken() As String
            Get
                Return ResourceManager.GetString("TimeTaken", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Total count of Screenshots.
        '''</summary>
        Friend Shared ReadOnly Property TotalNumber() As String
            Get
                Return ResourceManager.GetString("TotalNumber", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Screenshottype.
        '''</summary>
        Friend Shared ReadOnly Property Type() As String
            Get
                Return ResourceManager.GetString("Type", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to (only Website) Url.
        '''</summary>
        Friend Shared ReadOnly Property WebsiteUrl() As String
            Get
                Return ResourceManager.GetString("WebsiteUrl", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to (only Window) Window title.
        '''</summary>
        Friend Shared ReadOnly Property WindowTitle() As String
            Get
                Return ResourceManager.GetString("WindowTitle", resourceCulture)
            End Get
        End Property
    End Class
End Namespace