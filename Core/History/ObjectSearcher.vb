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

Imports System.Threading
Imports System.Reflection

Namespace History
    ''' <summary>
    ''' Provides functions to search in a pool of possible results.
    ''' </summary>
    ''' <typeparam name="TKey">The type of the unique identifier of the items.</typeparam>
    ''' <typeparam name="TValue">The type of the objects to search in.</typeparam>
    Public Class ObjectSearcher(Of TKey, TValue)

#Region "Fields"

        Private _resultItems As New Dictionary(Of TKey, Integer)
        Private _searchThread As Thread

#End Region

#Region "Properties"

        ''' <summary>
        ''' dictionary with the dates and a relevance rating 
        ''' Only Items with a relevance >0 are included.
        ''' </summary>
        ''' <value>The items.</value>
        Public ReadOnly Property Results() As Dictionary(Of TKey, Integer)
            Get
                Return _resultItems
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the possible results.
        ''' </summary>
        ''' <value>The possible results.</value>
        Public Property PossibleResults() As Dictionary(Of TKey, TValue)

        ''' <summary>
        ''' Gets or sets the unnecessary properties.
        ''' </summary>
        ''' <value>The unnecessary properties.</value>
        Public Property UnnecessaryProperties() As List(Of String)

        ''' <summary>
        ''' Gets or sets the unnecessary values.
        ''' </summary>
        ''' <value>The unnecessary values.</value>
        Public Property UnnecessaryValues() As List(Of String)

#End Region

#Region "Functions"

        ''' <summary>
        ''' Applies the filter.
        ''' </summary>
        ''' <param name="filter">The filter.</param>
        Private Sub ApplyFilterInternal(ByVal filter As String)
            Try
                OnSearchStarted()

                'Update the items, filtered if filter <> ""
                _resultItems = New Dictionary(Of TKey, Integer)

                filter = filter.ToUpperInvariant

                If filter = "" Then
                    'All Items
                    For Each e In _PossibleResults
                        _resultItems.Add(e.Key, 100)
                    Next
                Else
                    'apply a filter

                    For Each e In _PossibleResults
                        Dim rating As Integer = 0

                        'go through each property of the entry and compare it with the filter string
                        Dim t As Type = GetType(HistoryEntry)

                        'parts of the filter and how often it was found
                        Dim filterDict As New Dictionary(Of String, Integer)

                        For Each s In filter.Split(" "c)
                            If Not filterDict.ContainsKey(s) Then
                                filterDict.Add(s, 0)
                            End If
                        Next

                        For Each f In t.GetProperties()
                            rating += GetRelevanceOfProperty(f, filterDict, e.Value)
                        Next

                        'Check whether some filter parts were not found
                        For Each f In filterDict
                            If f.Value = 0 Then
                                rating -= 10 * f.Key.Length
                            End If
                        Next

                        'if the item is not totally irrelevant add it to the dictionary
                        If rating > 0 Then
                            If rating > 100 Then rating = 100
                            _resultItems.Add(e.Key, rating)
                        End If

                    Next

                    'sort the items (descending relevance rating)

                    'create a list with the key/valuepairs and sort it with a selfmade comparer
                    Dim sortList As New List(Of KeyValuePair(Of TKey, Integer))

                    For Each i In _resultItems
                        sortList.Add(i)
                    Next

                    sortList.Sort(New RelevanceComparer(Of TKey))

                    _resultItems = New Dictionary(Of TKey, Integer)

                    For Each i In sortList
                        'Change the relevance from an absolute to a relative value
                        _resultItems.Add(i.Key, i.Value)
                    Next
                End If

                OnSearchCompleted()
            Catch ex As ThreadAbortException
            Catch ex As Exception
                OnSearchError(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Applies the filter.
        ''' </summary>
        ''' <param name="filter">The filter.</param>
        Private Sub ApplyFilter(ByVal filter As Object)
            ApplyFilterInternal(CStr(filter))
        End Sub

        ''' <summary>
        ''' Applies the filter.
        ''' </summary>
        ''' <param name="filter">The filter.</param>
        Public Sub ApplyFilter(ByVal filter As String)
            If Not _searchThread Is Nothing Then
                If _searchThread.IsAlive Then _searchThread.Abort()
            End If

            _searchThread = New Thread(New ParameterizedThreadStart(AddressOf ApplyFilter))
            _searchThread.Start(filter)
        End Sub


        ''' <summary>
        ''' Gets the relevance of property.
        ''' </summary>
        ''' <param name="propertyToCheck">The property to check.</param>
        ''' <param name="filter">The filter.</param>
        ''' <param name="instance">The instance.</param>
        ''' <returns></returns>
        Private Function GetRelevanceOfProperty(ByVal propertyToCheck As PropertyInfo, ByRef filter As Dictionary(Of String, Integer), ByVal instance As Object) As Integer
            Dim relevance As Integer = 0

            Try
                If propertyToCheck.GetGetMethod.GetParameters.Length >= 0 Then
                    Dim value = propertyToCheck.GetValue(instance, New Object() {})
                    If value Is Nothing Then Return 0

                    'Check whether the value is good for our purpose
                    If Not value.ToString.StartsWith("{") And Not _UnnecessaryValues.Contains(value.ToString) And Not value.ToString = value.GetType.ToString Then
                        'Compare the filter with the value
                        Dim comp = value.ToString.ToUpperInvariant

                        'copy the dict to search in the real one
                        Dim searchList As New List(Of KeyValuePair(Of String, Integer))
                        For Each f In filter
                            searchList.Add(f)
                        Next

                        For Each f In searchList
                            If f.Value < 2 Then
                                '100 points more to get sure that the rating is 100% when the whole statement was found 
                                If comp = f.Key Then
                                    relevance += 100
                                    filter(f.Key) = 2
                                End If

                                If comp.Contains(f.Key) Then
                                    relevance += 2 * f.Key.Length
                                    filter(f.Key) = 1
                                End If
                            End If
                        Next
                    End If

                    'Check if the parent type is the same as the child (See the Date-Type for this)
                    'this is a protection against an endless loop, but not the best, because it doesnt check 
                    'whether a child on the next level contains the same type.
                    If Not value.GetType.ToString = instance.GetType.ToString Then
                        For Each p In value.GetType.GetProperties
                            If Not _UnnecessaryProperties.Contains(p.Name) Then
                                relevance += GetRelevanceOfProperty(p, filter, value)
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
            End Try

            Return relevance
        End Function

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ObjectSearcher(Of TKey)" /> class.
        ''' </summary>
        ''' <param name="possibleResults">The possible results.</param>
        ''' <param name="unnecessaryValues">The unnecessary values.</param>
        ''' <param name="unnecessaryProperties">The unnecessary properties.</param>
        Public Sub New(ByVal possibleResults As Dictionary(Of TKey, TValue), ByVal unnecessaryValues As List(Of String), ByVal unnecessaryProperties As List(Of String))
            _PossibleResults = possibleResults
            _UnnecessaryValues = unnecessaryValues
            _UnnecessaryProperties = unnecessaryProperties

            ApplyFilter("")
        End Sub

#End Region

#Region "Events"

        ''' <summary>
        ''' Occurs when the Search was completed successfully.
        ''' </summary>
        Public Event SearchCompleted(ByVal sender As Object, ByVal e As EventArgs)

        ''' <summary>
        ''' Called when [search completed].
        ''' </summary>
        Private Sub OnSearchCompleted()
            RaiseEvent SearchCompleted(Me, New EventArgs)
        End Sub

        Public Event SearchStarted(ByVal sender As Object, ByVal e As EventArgs)

        ''' <summary>
        ''' Called when [search started].
        ''' </summary>
        Private Sub OnSearchStarted()
            RaiseEvent SearchStarted(Me, New EventArgs)
        End Sub

        Public Event SearchError(ByVal sender As Object, ByVal e As SearchErrorEventArgs)

        ''' <summary>
        ''' Called when [search error].
        ''' </summary>
        ''' <param name="ex">The ex.</param>
        Private Sub OnSearchError(ByVal ex As Exception)
            RaiseEvent SearchError(Me, New SearchErrorEventArgs(ex))
        End Sub

#End Region
    End Class
End Namespace
