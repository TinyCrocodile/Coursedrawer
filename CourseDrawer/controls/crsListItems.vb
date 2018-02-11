Imports System.ComponentModel
Imports CourseDrawer

Public Class crsListItems

    Private m_crsListItems As List(Of crsListItem)
    Private m_selectedCrsListItem As crsListItem
    Private m_ListIndexCnt As Integer = 0
    Private m_last_pos As New System.Drawing.Point(4, 4)


    Public Event SelectionChanged(ByRef crsItem As crsListItem, byClick As Boolean)


    Public ReadOnly Property crsListItems As List(Of crsListItem)
        Get
            Return m_crsListItems
        End Get
    End Property

    Public Sub clear()
        m_crsListItems = New List(Of crsListItem)
        FlowPanelCourses.Controls.Clear()
    End Sub


    ''' <summary>
    ''' Ausgewähltes Element zurück geben
    ''' </summary>
    ''' <returns>Auswahl als crsListItem</returns>
    Public ReadOnly Property SelectedCrsListItem As crsListItem
        Get
            Return m_selectedCrsListItem
        End Get
    End Property

    ''' <summary>
    ''' Anzahl der Elemente von Coursen in der Liste
    ''' </summary>
    ''' <returns>Anzahl als Integer</returns>
    Public ReadOnly Property crsCount As Integer
        Get
            Return m_crsListItems.Count
        End Get
    End Property



    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        m_crsListItems = New List(Of crsListItem)
    End Sub


    ''' <summary>
    ''' Neuen Kurs in die Liste aufnehmen und anzeigen
    ''' </summary>
    ''' <param name="CourseName"></param>
    ''' <param name="CourseVisible"></param>
    Public Sub createCourse(CourseName As String, CourseObject As Object, Optional CourseVisible As Boolean = False)
        Dim newCrsItem As New crsListItem()
        'Layoutlogik abschalten
        Me.SuspendLayout()
        'Eintrag bennenen und positionieren
        newCrsItem.CourseName = CourseName
        newCrsItem.CourseVisible = CourseVisible
        newCrsItem.AttachedObject = CourseObject
        'newCrsItem.Location = m_last_pos
        'm_last_pos.Y += newCrsItem.Height
        'Eintrag der Liste hinzufügen
        m_crsListItems.Add(newCrsItem)
        newCrsItem.ListIndex = m_crsListItems.IndexOf(newCrsItem)
        If TypeOf CourseObject Is clsCourse Then
            CourseObject.listindex = newCrsItem.ListIndex
        End If
        'Eventhandler eintragen
        AddHandler newCrsItem.ClickItem, AddressOf SelectCourseListItem
        AddHandler newCrsItem.VisibleStatusChanged, AddressOf CourseVisibiltyChanged
        'Eintrag dem Panel hinzufügen
        Me.FlowPanelCourses.Controls.Add(newCrsItem)
        'Layoutlogik wieder freigeben
        Me.ResumeLayout(False)
    End Sub

    Public Event CourseVisibilityChanged(Course As crsListItem, Visible As Boolean)
    Private Sub CourseVisibiltyChanged(ByRef crsItem As crsListItem, Visible As Boolean)
        RaiseEvent CourseVisibilityChanged(crsItem, Visible)
    End Sub

    Public Enum SortOrder
        Name = 0
        id = 1
        Filename = 2
    End Enum

    Public Sub SortList(SortOrder As SortOrder)
        Dim crsListSorted = m_crsListItems.OrderBy(Function(x) x.CourseName).ToList
        Dim ListIndex As Integer = 0
        For Each crs In crsListSorted
            Me.FlowPanelCourses.Controls.SetChildIndex(crs, ListIndex)
            crs.ListIndex = ListIndex
            ListIndex += 1
        Next
    End Sub

    ''' <summary>
    ''' Eine Kurs auswählen
    ''' </summary>
    ''' <param name="Course"></param>
    ''' <param name="e"></param>
    Private Sub SelectCourseListItem(Course As crsListItem, e As EventArgs)
        If TypeOf Course Is crsListItem Then
            Dim EventNeeded As Boolean = True
            'alten Kurs deseletieren
            Try
                m_selectedCrsListItem.selectItem(False)
                EventNeeded = Not m_selectedCrsListItem.Equals(Course)
            Catch ex As Exception
            Finally
                m_selectedCrsListItem = Course
            End Try
            'neuen Kurs selectieren
            Dim crsItem As crsListItem = Course
            crsItem.selectItem(True)
            'haken auch setzen
            If crsItem.CourseVisible = False Then crsItem.CheckItem(True)

            'event aufrufen, wenn objekt sich geändert hat
            e.GetType()
            Dim byClick = TypeOf e Is MouseEventArgs
            If EventNeeded Then RaiseEvent SelectionChanged(crsItem, byClick)

        End If
    End Sub

    Public Sub SelectItem(ListIndex As Integer)
        Try
            SelectCourseListItem(m_crsListItems(ListIndex), New EventArgs())
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Alle Einträge den Haken (zurück)setzen
    ''' </summary>
    ''' <param name="Visible"></param>
    Public Sub SelectionAll(Visible As Boolean)
        For Each crs In m_crsListItems
            crs.CheckItem(Visible)
        Next
    End Sub
End Class
