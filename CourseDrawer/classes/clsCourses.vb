''' <summary>
''' Class for collection of courses
''' </summary>
''' <remarks>Singleton class for wrapping courses</remarks>
Public Class clsCourses
    Private Shared _courses As List(Of clsCourse)
    Private _selectedWP As clsWaypoint
    Private _selectedCrs As Integer
    Private _WaypointDistanceSetting As Single
    Private _AlignmentWPCount As Integer

    Public ReadOnly Property Count As Integer
        Get
            Return _courses.Count
        End Get
    End Property

    Public ReadOnly Property CourseListItems As List(Of clsCourse)
        Get
            Return _courses
        End Get
    End Property

    Public Property WaypointDistanceSetting As Single
        Get
            Return _WaypointDistanceSetting
        End Get
        Set(value As Single)
            _WaypointDistanceSetting = value
        End Set
    End Property

    Public Property AlignmentWPCount As Integer
        Get
            Return _AlignmentWPCount
        End Get
        Set(value As Integer)
            _AlignmentWPCount = value
        End Set
    End Property

    Private Shared _instance As clsCourses
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
        _courses = New List(Of clsCourse)
        AddHandler clsWaypoint.SelectionChanged, AddressOf Me.selectedChangeHandler
    End Sub
    Private Sub New(Courses As List(Of clsCourse))
        _courses = Courses
        AddHandler clsWaypoint.SelectionChanged, AddressOf Me.selectedChangeHandler
    End Sub
    ''' <summary>
    ''' Get instance of singleton class
    ''' </summary>
    ''' <param name="forceNew">force create new instance</param>
    ''' <returns>Instance of courses collection</returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance(Optional ByVal forceNew As Boolean = False) As clsCourses
        If _instance Is Nothing Or forceNew = True Then
            _instance = New clsCourses
        End If
        Return _instance
    End Function

    Public Shared Function getChangedCourses() As List(Of clsCourse)
        Dim changedCourse As New List(Of clsCourse)
        For Each course As clsCourse In _courses
            If course.changed Then
                changedCourse.Add(course)
            End If
        Next
        Return changedCourse
    End Function

    ''' <summary>
    ''' Get instance of singleton class
    ''' </summary>
    ''' <param name="forceNew">force create new instance</param>
    ''' <returns>Instance of courses collection</returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance(Courses As List(Of clsCourse), Optional ByVal forceNew As Boolean = False) As clsCourses
        If _instance Is Nothing Or forceNew = True Then
            _instance = New clsCourses(Courses)
        End If
        Return _instance
    End Function
    ''' <summary>
    ''' Get list of course name and hidden attribute
    ''' </summary>
    ''' <value></value>
    ''' <returns>Dictionary of course names and hidden attribute</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CourseList As Dictionary(Of String, Boolean)
        Get
            Dim dir As New Dictionary(Of String, Boolean)
            Dim index As Integer = 0
            _courses.Sort(Function(a, b) a.Name.CompareTo(b.Name))

            For Each crs As clsCourse In _courses
                If crs.isUsed = True Then
                    dir.Add(Right("000" & crs.id.ToString, 3) & " : " & crs.Name, Not crs.Hidden And crs.isUsed)
                    crs.listIndex = index
                    index += 1
                End If
            Next
            Return dir
        End Get
    End Property
    ''' <summary>
    ''' Set course hidden or visible
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <param name="hide">true = hidden, false = visible</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ItemHide(ByVal id As Integer, ByVal hide As Boolean) As Boolean
        If id < 0 Or id >= _courses.Count Then Return False
        _courses(id).Hidden = hide
        Return True
    End Function


    Public Sub ReadCourses()
        Dim course As clsCourse
        For Each course In _courses
            ReadCourseXML(course)
        Next
        _courses.Sort(AddressOf SortCourses)
        Me.RecalcCoursesID()
    End Sub

    ''' <summary>
    ''' Read Single Course File
    ''' </summary>
    ''' <param name="Course"></param>
    Private Sub ReadCourseXML(Course As clsCourse)
        Dim xmlDoc As New Xml.XmlDocument()
        Dim xmlNode As Xml.XmlNode
        Dim xmlNodeReader As Xml.XmlNodeReader
        Dim waypoint As New clsWaypoint
        Dim stringA() As String

        xmlDoc.Load(Course.fileName)
        If xmlDoc Is Nothing Then Exit Sub
        xmlNode = xmlDoc.DocumentElement.SelectSingleNode("course")
        xmlNodeReader = New Xml.XmlNodeReader(xmlNode)
        Do While (xmlNodeReader.Read())
            Select Case xmlNodeReader.NodeType
                Case Xml.XmlNodeType.Element
                    If xmlNodeReader.LocalName = "course" Then
                        Course = New clsCourse
                        _courses.Add(Course)
                        While xmlNodeReader.MoveToNextAttribute
                            Select Case xmlNodeReader.LocalName
                                Case "name"
                                    Course.Name = xmlNodeReader.Value
                                Case "id"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, Course.id)
                                Case "parent"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, Course.parent)
                            End Select
                        End While
                    ElseIf xmlNodeReader.LocalName.StartsWith("waypoint") Then
                        waypoint = New clsWaypoint
                        If Not Course Is Nothing Then
                            Course.addWaypoint(waypoint)
                        End If
                        While xmlNodeReader.MoveToNextAttribute
                            Select Case xmlNodeReader.LocalName
                                Case "angle"
                                    Double.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, waypoint.Angle)
                                Case "speed"
                                    Double.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, waypoint.Speed)
                                Case "turnend"
                                    If xmlNodeReader.Value = "1" Then
                                        waypoint.TurnEnd = True
                                    Else
                                        waypoint.TurnEnd = False
                                    End If
                                Case "pos"
                                    stringA = xmlNodeReader.Value.Split(" "c)
                                    Double.TryParse(stringA(0), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, waypoint.Pos_X)
                                    Double.TryParse(stringA(1), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, waypoint.Pos_Y)
                                Case "crossing"
                                    If xmlNodeReader.Value = "1" Then
                                        waypoint.Cross = True
                                    Else
                                        waypoint.Cross = False
                                    End If
                                Case "turnstart"
                                    If xmlNodeReader.Value = "1" Then
                                        waypoint.TurnStart = True
                                    Else
                                        waypoint.TurnStart = False
                                    End If
                                Case "wait"
                                    If xmlNodeReader.Value = "1" Then
                                        waypoint.Wait = True
                                    Else
                                        waypoint.Wait = False
                                    End If
                                Case "rev"
                                    If xmlNodeReader.Value = "1" Then
                                        waypoint.Reverse = True
                                    Else
                                        waypoint.Reverse = False
                                    End If
                                Case "unload"
                                    If xmlNodeReader.Value = "1" Then
                                        waypoint.Unload = True
                                    Else
                                        waypoint.Unload = False
                                    End If
                                Case "isconnectingtrack"
                                    If xmlNodeReader.Value = "true" Then
                                        waypoint.ConnectingTrack = True
                                    Else
                                        waypoint.ConnectingTrack = False
                                    End If
                                Case "generated"
                                    If xmlNodeReader.Value = "True" Then
                                        waypoint.generated = True
                                    Else
                                        waypoint.generated = False
                                    End If
                                Case "ridgemarker"
                                    Double.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, waypoint.ridgemarker)
                                Case "lane"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, waypoint.lane)
                                Case "dir"
                                    waypoint.dir = xmlNodeReader.Value
                                Case "turn"
                                    If xmlNodeReader.Value <> "false" Then
                                        waypoint.turn = xmlNodeReader.Value
                                    End If
                            End Select
                        End While
                    End If
            End Select
        Loop

    End Sub


    ''' <summary>
    ''' Draw courses
    ''' </summary>
    ''' <param name="graphics"></param>
    ''' <param name="zoomLvl"></param>
    ''' <remarks></remarks>
    Public Sub draw(ByRef graphics As Graphics, ByVal zoomLvl As Integer)
        Dim course As clsCourse
        For Each course In _courses
            If course.Hidden = False And course.isUsed = True Then
                course.draw(graphics, zoomLvl)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Select course at point
    ''' </summary>
    ''' <param name="point"></param>
    ''' <remarks></remarks>
    Public Sub selectWP(ByVal point As PointF, ByVal ZoomLevel As Integer, Optional noEvent As Boolean = False)
        Dim selected As Boolean
        Me._selectedCrs = -1
        For Each crs As clsCourse In _courses
            If crs.Hidden = False And crs.isUsed = True Then
                selected = crs.selectWP(point, ZoomLevel, noEvent)
                If selected = True Then
                    Me._selectedCrs = _courses.IndexOf(crs)
                    Exit For
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Select course by ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Public Sub selectWP(ByVal id As Integer)
        If id > 0 And id <= _courses.Count Then
            _courses(id - 1).selectWP(0)
            Me._selectedCrs = _courses.IndexOf(_courses(id - 1))
        End If
    End Sub

    ''' <summary>
    ''' Sort courses by ID
    ''' </summary>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SortCourses(ByVal X As clsCourse, ByVal Y As clsCourse) As Integer
        Return X.id.CompareTo(Y.id)
    End Function
    ''' <summary>
    ''' Set selected waypoint
    ''' </summary>
    ''' <param name="wp"></param>
    ''' <remarks></remarks>
    Private Sub selectedChangeHandler(ByRef wp As clsWaypoint)
        Me._selectedWP = wp
    End Sub
    ''' <summary>
    ''' Delete selected waypoint
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub deleteSelectedWP()
        If Me._selectedCrs >= 0 Then
            _courses(Me._selectedCrs).deleteSelectedWP()
        End If
    End Sub
    ''' <summary>
    ''' Delete selected course
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub deleteSelectedCrs()
        If Me._selectedCrs >= 0 Then
            _courses.RemoveAt(_selectedCrs)
            Me.RecalcCoursesID()
            clsWaypoint.forceUnselect()
            clsCourse.forceUnselect()
        End If
    End Sub
    ''' <summary>
    ''' Move course UP in list
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Public Sub moveCourseUp(ByVal id As Integer)
        If id > 1 Then
            Dim selCrs As clsCourse
            selCrs = _courses(id - 1)
            _courses.Remove(selCrs)
            _courses.Insert(id - 2, selCrs)
            Me.RecalcCoursesID()
        End If
    End Sub
    ''' <summary>
    ''' Move course DOWN in list
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Public Sub moveCourseDown(ByVal id As Integer)
        If id < _courses.Count Then
            Dim selCrs As clsCourse
            selCrs = _courses(id - 1)
            _courses.Remove(selCrs)
            _courses.Insert(id, selCrs)
            Me.RecalcCoursesID()
        End If
    End Sub
    ''' <summary>
    ''' Recalculate IDs of courses in list based on their position in list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RecalcCoursesID()
        'Commented to avoid courses Id recalculation
        'For idx = 1 To _courses.Count
        '    _courses(idx - 1).id = idx
        'Next
    End Sub
    ''' <summary>
    ''' Insert waypoint before selected WP in selected course
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub insertBeforeWP()
        If Me._selectedCrs >= 0 Then
            _courses(Me._selectedCrs).insertBeforeWP()
        End If
    End Sub
    ''' <summary>
    ''' Append waypoint to selected course
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub appendWP()
        If Me._selectedCrs >= 0 Then
            _courses(Me._selectedCrs).insertAfterWP()
        End If
    End Sub
    ''' <summary>
    ''' Destructor
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Finalize()
        RemoveHandler clsWaypoint.SelectionChanged, AddressOf Me.selectedChangeHandler
        MyBase.Finalize()
    End Sub
    ''' <summary>
    ''' Save courses to XML file
    ''' </summary>
    ''' <param name="root">byref xelement node</param>
    ''' <remarks></remarks>
    Public Sub SaveXML(ByRef root As XElement)

        Dim courses As XElement

        courses = New XElement("courses")
        For Each crs In _courses
            courses.Add(crs.getXML)
        Next
        root.Add(courses)



    End Sub
    ''' <summary>
    ''' Create new course
    ''' </summary>
    ''' <param name="point">Starting point</param>
    ''' <remarks></remarks>
    Public Sub addCourse(ByVal point As PointF)
        'Attention, new course ID must be lastcourse+1 instead count + 1
        Dim lastcourse As Integer
        If _courses.Count = 0 Then lastcourse = 0 Else lastcourse = _courses(_courses.Count - 1).id
        Dim crs As New clsCourse("course " & lastcourse + 1, lastcourse + 1)
        crs.isUsed = 1
        crs.initWPforNewCourse(point)
        _courses.Add(crs)
        Me._selectedCrs = _courses.Count - 1
    End Sub

    ''' <summary>
    ''' Calculate angles(directions) for selected waypoint
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function calculateAngleSelWP() As Double
        If Me._selectedCrs < 0 Then Return 0
        Return _courses(_selectedCrs).calculateAngleSelWP
    End Function
    ''' <summary>
    ''' Calculate angles(directions) for all waypoints in selected course
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub calculateAngleAllWP()
        If Me._selectedCrs < 0 Then Exit Sub
        _courses(_selectedCrs).calculateAngleAllWP()
    End Sub
    ''' <summary>
    ''' Fill waypoints between selected and previous WP in course
    ''' </summary>
    ''' <param name="range"></param>
    ''' <remarks></remarks>
    Public Sub fillBeforeSelected(ByVal range As Integer)
        If Me._selectedCrs < 0 Then Exit Sub
        _courses(_selectedCrs).fillBeforeSelected(range)
    End Sub

    Public Sub AlignWpSegmentHorizontal()
        If Me._selectedCrs < 0 Then Exit Sub
        _courses(_selectedCrs).AlignWpSegmentHorizontal(_AlignmentWPCount)
    End Sub

    Public Sub AlignWpSegmentVertical()
        If Me._selectedCrs < 0 Then Exit Sub
        _courses(_selectedCrs).AlignWpSegmentVertical(_AlignmentWPCount)
    End Sub

End Class
