Public Class clsCourse

    'ToDo Multiselect

    Friend folder As Object

    Public Shared Event SelectionChanged(ByRef course As clsCourse)
    Public Shared Property CircleDiameter As Integer
    Private Shared _isAnySelected As Boolean
    Private _waypoints As List(Of clsWaypoint)
    Private _selectedWP As Integer
    Public Property fileName As String
    Public Property Name As String
    Public Property id As Integer
    Public Property parent As Integer
    Public Property isUsed As Boolean = False
    Public Property listIndex As Integer
    Public Property changed As Boolean = False

    Private Structure Rechteck
        'ToDo Warum nicht Rect verwenden?
        Dim left As Integer
        Dim right As Integer
        Dim top As Integer
        Dim bottom As Integer
    End Structure

    Public ReadOnly Property WPCount As Integer
        Get
            Return _waypoints.Count
        End Get
    End Property

    Public ReadOnly Property SelectedWpIndex As Integer
        Get
            Return _selectedWP
        End Get
    End Property

    Public ReadOnly Property sFileName As String
        Get
            Return fileName
        End Get
    End Property

    Public Property isSelected As Boolean
    Public Property Hidden As Boolean


    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _waypoints = New List(Of clsWaypoint)
        Hidden = True
        AddHandler clsWaypoint.SelectionChanged, AddressOf Me.selectionChangedHandler
        AddHandler clsCourse.SelectionChanged, AddressOf Me.selectedCourseChanged
    End Sub
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Name">Course name</param>
    ''' <param name="id">Course ID</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Name As String, ByVal id As Integer)
        Me.New()
        Me.Name = Name
        Me.id = id
        Me.parent = parent
        Hidden = True
    End Sub

    ''' <summary>
    ''' Read Single Course File
    ''' </summary>
    Public Sub ReadCourseXML()
        Dim xmlDoc As New Xml.XmlDocument()
        'Dim xmlNode As Xml.XmlNode
        Dim xmlNodeReader As Xml.XmlNodeReader
        Dim waypoint As New clsWaypoint
        Dim stringA() As String
        If Not Me.isUsed Then Exit Sub
        xmlDoc.Load(Me.fileName)
        If xmlDoc Is Nothing Then Exit Sub
        'xmlNode = xmlDoc.DocumentElement.
        'If xmlNode Is Nothing Then Exit Sub
        xmlNodeReader = New Xml.XmlNodeReader(xmlDoc)
        Do While (xmlNodeReader.Read())
            Select Case xmlNodeReader.NodeType
                Case Xml.XmlNodeType.Element
                    If xmlNodeReader.LocalName = "course" Then
                        While xmlNodeReader.MoveToNextAttribute
                            Select Case xmlNodeReader.LocalName
                                Case "name"
                                    Me.Name = xmlNodeReader.Value
                                Case "id"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, Me.id)
                                Case "parent"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, Me.parent)
                            End Select
                        End While
                    ElseIf xmlNodeReader.LocalName.StartsWith("waypoint") Then
                        waypoint = New clsWaypoint
                        If Not Me Is Nothing Then
                            Me.addWaypoint(waypoint)
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
                                Case "generated"
                                    If xmlNodeReader.Value = "True" Then
                                        waypoint.generated = True
                                    Else
                                        waypoint.generated = False
                                    End If
                                Case "ridgemarker"
                                    Double.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, waypoint.ridgemarker)
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
    ''' Append waypoint to course
    ''' </summary>
    ''' <param name="waypoint"></param>
    ''' <remarks></remarks>
    Public Sub addWaypoint(ByVal waypoint As clsWaypoint)
        _waypoints.Add(waypoint)
    End Sub
    ''' <summary>
    ''' Insert waypoint before
    ''' </summary>
    ''' <param name="waypoint"></param>
    ''' <param name="before"></param>
    ''' <remarks></remarks>
    Public Sub insertWaypoint(ByVal waypoint As clsWaypoint, ByVal before As Integer)
        If before < 1 Then Exit Sub
        If _waypoints.Count > before Then
            _waypoints.Insert(before, waypoint)
        ElseIf _waypoints.Count = before Then
            _waypoints.Add(waypoint)
        End If
        Me.changed = True
    End Sub
    ''' <summary>
    ''' Fill waypoints between selected and previous waypoint
    ''' </summary>
    ''' <param name="range"></param>
    ''' <remarks></remarks>
    Public Sub fillBeforeSelected(ByVal range As Integer)
        If _selectedWP < 1 Then Exit Sub

        Dim dXtotal As Double
        Dim dYtotal As Double
        Dim lenTotal As Double
        Dim steps As Integer
        Dim wp As clsWaypoint

        dXtotal = _waypoints(_selectedWP).Pos_X - _waypoints(_selectedWP - 1).Pos_X
        dYtotal = _waypoints(_selectedWP).Pos_Y - _waypoints(_selectedWP - 1).Pos_Y
        lenTotal = Math.Sqrt((dXtotal * dXtotal) + (dYtotal * dYtotal))
        steps = lenTotal \ range

        For i As Integer = 1 To steps
            wp = _waypoints(_selectedWP - 1).Clone(dXtotal / steps * i, dYtotal / steps * i)
            _waypoints.Insert(_selectedWP - 1 + i, wp)
            wp = Nothing
        Next
        Me.changed = True
    End Sub
    ''' <summary>
    ''' Draw waypoints
    ''' </summary>
    ''' <param name="graphic"></param>
    ''' <param name="zoomLvl"></param>
    ''' <remarks></remarks>
    Public Sub draw(ByRef graphic As Graphics, ByVal zoomLvl As Integer)
        'ToDo Draw doublebuffered handle zooming and paning by grapics.transform matrix
        Dim waypoint As clsWaypoint
        Dim dr_points() As PointF
        Dim idx As Integer
        Dim pen As Pen
        Dim dr_point As PointF
        If _waypoints.Count = 0 Then Exit Sub
        ReDim dr_points(_waypoints.Count - 1)
        idx = 1
        For Each waypoint In _waypoints
            dr_points(idx - 1) = waypoint.PositionScreenDraw(zoomLvl)
            idx += 1
        Next
        'path
        If Me.isSelected = True Then
            'ToDo: Keep pens global not create this pens at every usage
            pen = New Pen(Brushes.Blue, 2)
        Else
            pen = New Pen(Brushes.DarkBlue)
        End If
        graphic.DrawLines(pen, dr_points)

        'waypoints
        idx = 1
        For Each waypoint In _waypoints
            If waypoint.isSelected = True Then
                pen = New Pen(Brushes.WhiteSmoke, 2)
            ElseIf idx = 1 Then
                pen = New Pen(Brushes.LightGreen, 2)
            ElseIf idx = _waypoints.Count Then
                pen = New Pen(Brushes.Red, 2)
            ElseIf waypoint.Wait = True Then
                pen = New Pen(Brushes.Blue, 2)
            ElseIf waypoint.Cross = True Then
                pen = New Pen(Brushes.Yellow, 2)
            ElseIf waypoint.Reverse = True Then
                pen = New Pen(Brushes.Pink, 2)
            ElseIf waypoint.Unload = True Then
                pen = New Pen(Brushes.Purple, 2)
            ElseIf waypoint.TurnStart = True Then
                pen = New Pen(Brushes.Orange, 2)
            ElseIf waypoint.TurnEnd = True Then
                pen = New Pen(Brushes.Salmon, 2)
            Else
                pen = New Pen(Brushes.DarkBlue)
            End If
            dr_point = waypoint.PositionScreenDraw(zoomLvl)
            graphic.DrawEllipse(pen, dr_point.X - 3, dr_point.Y - 3, 6, 6)
            idx += 1
        Next
        'guiding circle around selected node
        If Me.isSelected = True And _CircleDiameter > 0 And _selectedWP >= 0 Then
            Dim diameter As Single
            diameter = CircleDiameter * zoomLvl / 100
            dr_point = _waypoints(_selectedWP).PositionScreenDraw(zoomLvl)
            pen = New Pen(Brushes.LightGreen, 0.1)
            graphic.DrawEllipse(pen, dr_point.X - diameter / 2, dr_point.Y - diameter / 2, diameter, diameter)
            pen = New Pen(Brushes.OrangeRed, 0.1)
            graphic.DrawEllipse(pen, dr_point.X - diameter, dr_point.Y - diameter, diameter * 2, diameter * 2)
        End If
        'ToDo New guiding-circle with tangent to course
    End Sub
    ''' <summary>
    ''' Select waypoint at point
    ''' </summary>
    ''' <param name="point"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function selectWP(ByVal point As PointF, Optional NoEvent As Boolean = False) As Boolean
        Dim selected As Boolean
        If _isAnySelected = True Then
            RaiseEvent SelectionChanged(Nothing)
        End If
        For Each wp As clsWaypoint In _waypoints
            selected = wp.selectWP(point, 1)
            If selected = True Then
                If Me._isSelected = False And Not NoEvent Then
                    RaiseEvent SelectionChanged(Me)
                End If
                Exit For
            End If
        Next
        Return selected
    End Function
    ''' <summary>
    ''' Select waypoint by ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function selectWP(ByVal id As Integer, Optional NoEvent As Boolean = False) As Boolean
        If _isAnySelected = True Then
            RaiseEvent SelectionChanged(Nothing)
        End If
        If id >= 0 And id <= _waypoints.Count - 1 Then
            _waypoints(id).forceSelect()
            If Me._isSelected = False And Not NoEvent Then
                RaiseEvent SelectionChanged(Me)
            End If
        End If
        Return True
    End Function
    ''' <summary>
    ''' Set selected waypoint if selection changed
    ''' </summary>
    ''' <param name="wp"></param>
    ''' <remarks></remarks>
    Private Sub selectionChangedHandler(ByRef wp As clsWaypoint)
        If wp Is Nothing Then
            Me._isSelected = False
            Me._selectedWP = -1
        Else
            If Me._waypoints.Contains(wp) Then
                Me._selectedWP = _waypoints.IndexOf(wp)
            Else
                Me._selectedWP = -1
            End If
        End If
    End Sub
    ''' <summary>
    ''' Set isSelected attribute if selection changed
    ''' </summary>
    ''' <param name="crs"></param>
    ''' <remarks></remarks>
    Private Sub selectedCourseChanged(ByRef crs As clsCourse)
        If crs Is Nothing Then
            Me._isSelected = False
            If clsCourse._isAnySelected = True Then clsCourse._isAnySelected = False
            Exit Sub
        Else
            If clsCourse._isAnySelected = False Then clsCourse._isAnySelected = True
            If crs.Equals(Me) Then
                Me._isSelected = True
            Else
                Me._isSelected = False
            End If
        End If
    End Sub
    ''' <summary>
    ''' Delete selected waypoint
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub deleteSelectedWP()
        'ToDo Multiselect fähig machen
        Dim idx As Integer = Me._selectedWP
        If Me._selectedWP >= 0 Then
            Me._waypoints.RemoveAt(Me._selectedWP)
            If idx > _waypoints.Count - 1 Then
                _waypoints(_waypoints.Count - 1).forceSelect()
            End If
            _waypoints(Me._selectedWP).forceSelect()
            'por si acaso era el primero o el ultimo, ponemos el primer y ultimo wp cross a true (para que preguntar)
            _waypoints(0).Cross = True
            _waypoints(_waypoints.Count - 1).Cross = True
        End If
    End Sub
    ''' <summary>
    ''' Insert waypoint before
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub insertBeforeWP()
        Dim idx As Integer = Me._selectedWP
        If Me._selectedWP >= 0 Then
            Me._waypoints.Insert(_selectedWP, Me._waypoints(Me._selectedWP).Clone)
            clsWaypoint.forceUnselect()
            _waypoints(idx).forceSelect()
            If idx = 0 Then
                _waypoints(idx + 1).Cross = False
                _waypoints(idx).Cross = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Append waypoint
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub appendWP()
        'ToDo Make new Waypoint in direction of last course waypoint
        Me._waypoints.Add(Me._waypoints(Me._waypoints.Count - 1).Clone)
        clsWaypoint.forceUnselect()
        _waypoints(Me._waypoints.Count - 1).forceSelect()
        _waypoints(_selectedWP - 1).Cross = False
        _waypoints(_selectedWP).Cross = True
    End Sub
    ''' <summary>
    ''' Destructor
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Finalize()
        RemoveHandler clsWaypoint.SelectionChanged, AddressOf Me.selectionChangedHandler
        RemoveHandler clsCourse.SelectionChanged, AddressOf Me.selectedCourseChanged
        MyBase.Finalize()
    End Sub
    ''' <summary>
    ''' Create XML of course
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getXML(Optional noCourseDesc As Boolean = False) As XElement
        Dim el As New XElement("course")
        Dim idx As Integer = 1
        If noCourseDesc = False Then
            el.Add(New XAttribute("name", Me.Name))
            el.Add(New XAttribute("id", Me.id.ToString))
            el.Add(New XAttribute("parent", Me.parent.ToString))
        End If
        For Each wp As clsWaypoint In _waypoints
            el.Add(wp.getXML(idx))
            idx += 1
        Next
        Return el
    End Function
    ''' <summary>
    ''' Initial waypoints for new course
    ''' </summary>
    ''' <param name="Point"></param>
    ''' <remarks></remarks>
    Public Sub initWPforNewCourse(ByVal Point As PointF)
        Dim wp As New clsWaypoint
        clsWaypoint.forceUnselect()
        wp.setNewPos(Point)
        wp.Cross = True
        _waypoints.Add(wp)
        wp = New clsWaypoint
        wp.setNewPos(New PointF(Point.X + 10, Point.Y + 10))
        wp.Cross = True
        _waypoints.Add(wp)
        wp.forceSelect()
        wp = Nothing
    End Sub
    ''' <summary>
    ''' Force unselect course
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub forceUnselect()
        RaiseEvent SelectionChanged(Nothing)
    End Sub
    ''' <summary>
    ''' Calculate angle(direction) for all waypoints
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub calculateAngleAllWP()
        For i As Integer = 0 To _waypoints.Count - 1
            Me.calculateAngle(i)
        Next
    End Sub
    ''' <summary>
    ''' Calculate angle(direction) for waypoint with ID
    ''' </summary>
    ''' <param name="idx"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function calculateAngle(ByVal idx As Integer) As Double
        If _waypoints.Count < 2 Then Return 0

        Dim dir As Double

        If idx = 0 Then    'first WP
            dir = getDirection(_waypoints(idx).PositionScreen, _waypoints(idx + 1).PositionScreen)
        ElseIf idx = _waypoints.Count - 1 Then  'last WP
            dir = getDirection(_waypoints(idx - 1).PositionScreen, _waypoints(idx).PositionScreen)
        Else  'between other WPs
            Dim len_p As Double
            Dim len_n As Double
            Dim dX As Double
            Dim dY As Double
            Dim p1 As PointF
            Dim p2 As PointF

            dX = Math.Abs(_waypoints(idx).PositionScreen.X - _waypoints(idx + 1).PositionScreen.X)
            dY = Math.Abs(_waypoints(idx).PositionScreen.Y - _waypoints(idx + 1).PositionScreen.Y)
            len_p = Math.Sqrt((dX * dX) + (dY * dY))
            dX = Math.Abs(_waypoints(idx).PositionScreen.X - _waypoints(idx - 1).PositionScreen.X)
            dY = Math.Abs(_waypoints(idx).PositionScreen.Y - _waypoints(idx - 1).PositionScreen.Y)
            len_n = Math.Sqrt((dX * dX) + (dY * dY))

            If len_p < len_n Then
                p2 = _waypoints(idx + 1).PositionScreen
                dX = (_waypoints(idx).PositionScreen.X - _waypoints(idx - 1).PositionScreen.X) * (len_p / len_n)
                dY = (_waypoints(idx).PositionScreen.Y - _waypoints(idx - 1).PositionScreen.Y) * (len_p / len_n)
                p1 = New PointF(_waypoints(idx).PositionScreen.X - dX, _waypoints(idx).PositionScreen.Y - dY)
            Else
                p1 = _waypoints(idx - 1).PositionScreen
                dX = (_waypoints(idx + 1).PositionScreen.X - _waypoints(idx).PositionScreen.X) * (len_n / len_p)
                dY = (_waypoints(idx + 1).PositionScreen.Y - _waypoints(idx).PositionScreen.Y) * (len_n / len_p)
                p2 = New PointF(_waypoints(idx).PositionScreen.X + dX, _waypoints(idx).PositionScreen.Y + dY)
            End If

            dir = getDirection(p1, p2)
        End If
        _waypoints(idx).Angle = dir
        Return dir

    End Function
    ''' <summary>
    ''' Calculate angle(direction) for selected waypoint
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function calculateAngleSelWP() As Double
        If _selectedWP < 0 Then Return 0
        If _waypoints.Count < 2 Then Return 0


        Return Me.calculateAngle(_selectedWP)

    End Function
    ''' <summary>
    ''' Get direction between two points
    ''' </summary>
    ''' <param name="pt1"></param>
    ''' <param name="pt2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getDirection(ByVal pt1 As PointF, ByVal pt2 As PointF) As Double
        Dim dX As Double
        Dim dY As Double
        Dim v1 As Double
        Dim v2 As Double

        dX = Math.Abs(pt1.X - pt2.X)
        dY = Math.Abs(pt1.Y - pt2.Y)
        If dX = 0 And dY = 0 Then
            'same point, no direction...
            Return 0
        End If
        'dX = 0 => -180(180) / 0
        If dX = 0 Then
            If pt1.Y < pt2.Y Then
                Return 0
            Else
                Return 180
            End If
        End If
        'dY = 0 => -90 / 90
        If dY = 0 Then
            If pt1.X < pt2.X Then
                Return -90
            Else
                Return 90
            End If
        End If
        'dX <>0 and dY <> 0 ...
        v1 = (Math.Atan(dY / dX)) / (2 * Math.PI) * 360
        v2 = 90 - v1

        If pt1.X > pt2.X Then  '0 - -180
            If pt1.Y < pt2.Y Then '0 - -90
                Return -v2
            Else '-90 - 180
                Return -90 - v1
            End If
        Else '0 - +180
            If pt1.Y < pt2.Y Then '0 - +90
                Return v2
            Else '+90 - +180
                Return 90 + v1
            End If
        End If
    End Function


    ''' <summary>
    ''' Rechteck für den Kurs berechnen
    ''' Dieses kann zum bestimmen des Bereiches genutzt werden, der neu gezeichnet werden muss
    ''' </summary>
    ''' <returns>Rechteck als Rectangle</returns>
    'ToDo DrawingArea verwenden
    'ToDo Simplify if possible
    Public Function DrawingArea(Zoom As Integer) As Rectangle

        Dim oRechteck As Rechteck
        oRechteck.left = 0
        oRechteck.right = 0
        oRechteck.top = 0
        oRechteck.bottom = 0

        Dim ScreenPoint As New PointF
        For Each wp In _waypoints
            ScreenPoint = wp.PositionScreenDraw(Zoom)
            oRechteck.left = CInt(ScreenPoint.X)
            oRechteck.right = oRechteck.left
            oRechteck.top = CInt(ScreenPoint.Y)
            oRechteck.bottom = oRechteck.top
            Exit For
        Next

        For Each wp In _waypoints
            ScreenPoint = wp.PositionScreenDraw(Zoom)
            If CInt(ScreenPoint.X) < CInt(oRechteck.left) Then
                oRechteck.left = ScreenPoint.X
            End If
            If CInt(ScreenPoint.X) > CInt(oRechteck.right) Then
                oRechteck.right = ScreenPoint.X
            End If
            If CInt(ScreenPoint.Y) < CInt(oRechteck.top) Then
                oRechteck.top = ScreenPoint.Y
            End If
            If CInt(ScreenPoint.Y) > CInt(oRechteck.bottom) Then
                oRechteck.bottom = ScreenPoint.Y
            End If
        Next
        Dim margin As Integer = 10
        Return New Rectangle(oRechteck.left - margin, oRechteck.top - margin, oRechteck.right - oRechteck.left + margin * 2, oRechteck.bottom - oRechteck.top + margin * 2)

    End Function

    ''' <summary>
    ''' Calculates the area between the Previous the next and the Current Waypoint 
    ''' </summary>
    ''' <param name="RepaintRegion">The repaint Region where the new region is added to</param>
    ''' <param name="ChangedWaypointIndex">The Index of the current waypoint</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub CreateRepaintArea(ByRef RepaintRegion As Region, ByVal ChangedWaypointIndex As Integer, ByVal ZoomLevel As Integer)
        Dim MaxX As Single
        Dim MaxY As Single
        Dim MinX As Single
        Dim MinY As Single

        Dim PointPrev As PointF = _waypoints(ChangedWaypointIndex - 1).PositionScreenDraw(ZoomLevel)
        Dim PointWP As PointF = _waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel)
        Dim PointNext As PointF = _waypoints(ChangedWaypointIndex + 1).PositionScreenDraw(ZoomLevel)
        If Me.isSelected = True And _CircleDiameter > 0 And _selectedWP >= 0 Then
            RepaintRegion.Union(New RectangleF(_waypoints(_selectedWP).PositionScreenDraw(ZoomLevel).X - 1 - (CircleDiameter * ZoomLevel / 100), _waypoints(_selectedWP).PositionScreenDraw(ZoomLevel).Y - 1 - (CircleDiameter * ZoomLevel / 100), (CircleDiameter * 2 * ZoomLevel / 100) + 2, (CircleDiameter * 2 * ZoomLevel / 100) + 2))
        End If
        If 0 < ChangedWaypointIndex < _waypoints.Count - 1 And Me.isSelected = True Then

            MaxX = Math.Max(Math.Max(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).X, _waypoints(ChangedWaypointIndex - 1).PositionScreenDraw(ZoomLevel).X), Math.Max(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).X, _waypoints(ChangedWaypointIndex + 1).PositionScreenDraw(ZoomLevel).X))
            MaxY = Math.Max(Math.Max(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).Y, _waypoints(ChangedWaypointIndex - 1).PositionScreenDraw(ZoomLevel).Y), Math.Max(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).Y, _waypoints(ChangedWaypointIndex + 1).PositionScreenDraw(ZoomLevel).Y))
            MinX = Math.Min(Math.Min(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).X, _waypoints(ChangedWaypointIndex - 1).PositionScreenDraw(ZoomLevel).X), Math.Min(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).X, _waypoints(ChangedWaypointIndex + 1).PositionScreenDraw(ZoomLevel).X))
            MinY = Math.Min(Math.Min(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).Y, _waypoints(ChangedWaypointIndex - 1).PositionScreenDraw(ZoomLevel).Y), Math.Min(_waypoints(ChangedWaypointIndex).PositionScreenDraw(ZoomLevel).Y, _waypoints(ChangedWaypointIndex + 1).PositionScreenDraw(ZoomLevel).Y))
            RepaintRegion.Union(New RectangleF(MinX - 5, MinY - 5, (MaxX - MinX) + 10, (MaxY - MinY) + 10))

        End If
    End Sub
    Public Sub CreateRepaintWaypointArea(ByRef repaintRegion As Region, ByVal WaypointIndex As Integer, ByVal ZoomLevel As Integer)
        If 0 < WaypointIndex < _waypoints.Count - 1 Then
            Dim PointWp As PointF = _waypoints(WaypointIndex).PositionScreenDraw(ZoomLevel)
            repaintRegion.Union(New RectangleF(PointWp.X - 5, PointWp.Y - 5, 10, 10))
        End If
        If Me.isSelected = True And _CircleDiameter > 0 And _selectedWP >= 0 Then
            repaintRegion.Union(New RectangleF(_waypoints(_selectedWP).PositionScreenDraw(ZoomLevel).X - 1 - (CircleDiameter * ZoomLevel / 100), _waypoints(_selectedWP).PositionScreenDraw(ZoomLevel).Y - 1 - (CircleDiameter * ZoomLevel / 100), (CircleDiameter * 2 * ZoomLevel / 100) + 2, (CircleDiameter * 2 * ZoomLevel / 100) + 2S))
        End If

    End Sub
End Class
