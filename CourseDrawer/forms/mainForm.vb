Imports System.ComponentModel
Imports CourseDrawer

Public Enum MapSize As Integer
    'ad new sizes if needed dont forget to update the Combobox
    X1 = 2048
    X4 = 4096
    X8 = 8192
    X16 = 16384
End Enum

Public Class mainForm
    Dim ZoomStep As Integer = 50
    Dim zoomLvl As Integer = 100
    Dim myMousePos As Point
    Dim myLocation As PointF
    Dim selectedWP As clsWaypoint
    Dim selectedCrs As clsCourse
    Dim WithEvents selectedCrsItem As crsListItem
    Dim myZoomCursor As Cursor
    Dim myGrabCursor As Cursor
    Dim myGrabbingCursor As Cursor
    Dim doListChange As Boolean = False
    Dim firstDraw As Boolean = False
    Dim debugRect As Rectangle
    Dim repaintRegion As Region

    ''' <summary>
    ''' Loads bitmap as map 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLoadBGImage.Click

        Dim filename As String
        Dim mapdds As System.Drawing.Bitmap
        OpenFileDialog1.FileName = IO.Path.GetFileName(My.Settings.MapPath)
        OpenFileDialog1.InitialDirectory = IO.Path.GetDirectoryName(My.Settings.MapPath)
        OpenFileDialog1.Filter = "DDS picture|*.dds"
        OpenFileDialog1.Filter += "|BMP picture|*.bmp"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            My.Settings.MapPath = filename
        Else
            Exit Sub
        End If
        mapdds = DevIL.DevIL.LoadBitmap(filename)

        PictureBox1.Image = mapdds
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Me.panel1.AutoScrollPosition = New Point(0, 0)
        initBackgroundImage()

    End Sub

    Private Sub initBackgroundImage()

        Dim locSize As Drawing.Size

        Try
            If PictureBox1.Image Is Nothing Then
                MapSizeSelector.Enabled = True
                locSize = New Size(System.Enum.Parse(GetType(MapSize), MapSizeSelector.Text), System.Enum.Parse(GetType(MapSize), MapSizeSelector.Text))
            Else
                locSize = PictureBox1.Image.Size
                MapSizeSelector.Text = System.Enum.Parse(GetType(MapSize), locSize.Width.ToString).ToString
                MapSizeSelector.Enabled = False
            End If
            zoomLvl = 100
            Me.StatusZoomLevel.Text = zoomLvl & "%"
            PictureBox1.Size = locSize
            clsWaypoint.mapSize = locSize
            Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))

        Catch ex As Exception
            Debug.Print(ex.Message & " " & ex.TargetSite.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Do things                                                                             
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PictureBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Dim ev As System.Windows.Forms.MouseEventArgs
        Dim locSize As Size

        ev = e
        Dim origPoint As New PointF(ev.X * 100 / zoomLvl, ev.Y * 100 / zoomLvl)

        If butNewCourse.Checked = True Then
            clsCourses.getInstance.addCourse(origPoint)

            'clsCourses.getInstance.selectwp.lastwp

            'ToDo: Neuen Kurs erstellen Fertig machen und dabei neue Checkboxlist nehmen
            'ToDo: New Feature to alingn Waypoints.(Maybe a combo of deleting Waypoints and inserting new ones would do the job)
            'ToDo: new Feature to reduce Waypoint-Count on long courses.(Maybe a combo of deleting Waypoints and inserting new ones would do the job) finde out if cp has a limit of max. waypoint distance.
            'ToDo: Enable zooming and paning on mousewheel / middle mouse
            'ToDo: Paint in MouseMove-Event not in Timer Tick (combined with backbufferd drawing)
            'ToDo: Only use mouse difference to move the Waypoint. Do not move the Waypoint center to the Mouseposition
            'ToDo: When creating new Courses make Turn Radius between lines
            'ToDo: Change Cursor on Course hover
            'ToDo: Handle Selection if multiple Waypoints are on the same point
            'ToDo: Make Enum of Drawing operations see https://www.codeproject.com/Tips/1223125/Resize-and-rotate-shapes-in-GDIplus
            'ToDo: Snap CourseSegments Horizontal and Vertical
            'ToDo: Snap CourseSegment to Circle
            'ToDo: Choose witch course to select, when multiple Waypoints overlap
            'ToDo: Remove all leftovers of CheckedListBox1(assuming this is old Listbox) in code 
            'ToDo: Implement Contextmenu for Courseactions
            'ToDo: New feature for joining and splitting Courses
            'ToDo: Use WinGup for providing Updates to the Application

            Me.butSave.Enabled = True
            Me.butAppendNode.Enabled = True
            Me.butDelCourse.Enabled = True
            Me.butDeleteNode.Enabled = True
            Me.butInsertNode.Enabled = True
            Me.butLoadBGImage.Enabled = True
            Me.butNewCourse.Checked = False
            Me.butSave.Enabled = True
            Me.butLoadCourse.Enabled = True
            Me.butSelect.Enabled = True
            Me.butZoom.Enabled = True
            Me.butMove.Enabled = True

            ToolTip1.RemoveAll()

        ElseIf butZoom.Checked = True Then

            locSize = clsWaypoint.mapSize

            'Picture size (zoom)
            If ev.Button = Windows.Forms.MouseButtons.Left Then
                If zoomLvl < 100 Then
                    ZoomStep = 10
                Else
                    ZoomStep = 50
                End If
                If zoomLvl < 4000 Then 'The max zoomlevel
                    zoomLvl += ZoomStep
                End If
            ElseIf ev.Button = Windows.Forms.MouseButtons.Right Then
                If zoomLvl <= 100 Then
                    ZoomStep = 10
                Else
                    ZoomStep = 50
                End If
                If zoomLvl > ZoomStep Then
                    zoomLvl -= ZoomStep
                End If
            ElseIf ev.Button = Windows.Forms.MouseButtons.Middle Then
                zoomLvl = 50
            End If
            locSize.Width = locSize.Width * zoomLvl / 100
            locSize.Height = locSize.Height * zoomLvl / 100
            PictureBox1.Size = locSize

            'Picture location (center on click spot)
            'ToDo Picture location on mouse position not centering
            Dim posOffset As New Point((origPoint.X * zoomLvl / 100) - (panel1.Size.Width / 2), (origPoint.Y * zoomLvl / 100) - (panel1.Size.Height / 2))
            panel1.AutoScrollPosition = posOffset

            Me.StatusZoomLevel.Text = zoomLvl & "%"
        ElseIf butSelect.Checked = True Then
            If selectedCrs IsNot Nothing And selectedWP IsNot Nothing Then
                selectedCrs.CreateRepaintWaypointArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
                PictureBox1.Invalidate(repaintRegion)
            End If
            clsCourses.getInstance.selectWP(ev.Location, zoomLvl)
        End If
        'Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown

        Dim PrevSelectedCourse As clsCourse

        If butMove.Checked = True Then
            myMousePos = Cursor.Position
            Me.Cursor = myGrabbingCursor
            TimerDragPicture.Enabled = True
        End If
        If butSelect.Checked = True Then

            PrevSelectedCourse = selectedCrs
            myMousePos = (Cursor.Position)

            If selectedCrs IsNot Nothing And selectedWP IsNot Nothing Then
                selectedCrs.CreateRepaintWaypointArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
            End If

            clsCourses.getInstance.selectWP(e.Location, zoomLvl, False)
            myLocation = e.Location
            TimerDragPicture.Interval = SystemInformation.DoubleClickTime
            TimerDragPicture.Enabled = True

            If selectedCrs IsNot Nothing And selectedWP IsNot Nothing Then
                If selectedCrs IsNot PrevSelectedCourse Then
                    repaintRegion.Union(New Region(selectedCrs.DrawingArea(zoomLvl)))
                    If PrevSelectedCourse IsNot Nothing Then repaintRegion.Union(New Region(PrevSelectedCourse.DrawingArea(zoomLvl)))
                End If
                selectedCrs.CreateRepaintWaypointArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
            End If
            PictureBox1.Invalidate(repaintRegion)
        End If

    End Sub


    Private Sub TimerDragPicture_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerDragPicture.Tick
        Dim newMousePos As Point = Cursor.Position

        If butMove.Checked = True Then

            Dim newOffset As New Point(-Me.panel1.AutoScrollPosition.X - (newMousePos.X - myMousePos.X), -Me.panel1.AutoScrollPosition.Y - (newMousePos.Y - myMousePos.Y))
            Me.panel1.AutoScrollPosition = newOffset

        End If
        If butSelect.Checked = True And Not Me.selectedWP Is Nothing Then
            Dim invRange As Single
            Dim newOffset As New Point(myLocation.X + (newMousePos.X - myMousePos.X), myLocation.Y + (newMousePos.Y - myMousePos.Y))
            Dim origPoint As New PointF(newOffset.X * 100 / zoomLvl, newOffset.Y * 100 / zoomLvl)

            TimerDragPicture.Interval = 100
            Try
                Me.selectedCrs.changed = True
                Me.selectedCrs.CreateRepaintArea(repaintRegion, Me.selectedCrs.SelectedWpIndex, zoomLvl)
                Me.selectedWP.setNewPos(origPoint)
            Catch ex As Exception
            End Try

            myLocation = newOffset
            If clsCourse.CircleDiameter > 0 Then invRange = (clsCourse.CircleDiameter * 2 * zoomLvl / 100) + 20

            If Me.firstDraw = True And Me.selectedCrs Is Nothing Then
                Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
                Me.firstDraw = False
            Else
                Me.selectedCrs.CreateRepaintArea(repaintRegion, Me.selectedCrs.SelectedWpIndex, zoomLvl)
                Me.PictureBox1.Invalidate(repaintRegion)
                'Me.PictureBox1.Invalidate(New System.Drawing.Rectangle(newOffset.X - invRange / 2, newOffset.Y - invRange / 2, invRange, invRange))
            End If
        End If
        myMousePos = newMousePos
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp

        If TimerDragPicture.Enabled = True Then TimerDragPicture.Enabled = False
        If butMove.Checked Then Me.Cursor = myGrabCursor
        Me.firstDraw = True
        Try
            'ToDo: Use minimal invaldidate region not shure wich repaintarea it would be
            'Me.PictureBox1.Invalidate(selectedCrs.DrawingArea)
            Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub butMove_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butMove.CheckedChanged
        If butMove.Checked = True Then
            butZoom.Checked = False
            butSelect.Checked = False
        End If
        If butZoom.Checked = False And butSelect.Checked = False And butMove.Checked = False Then
            butMove.Checked = True
        End If
    End Sub

    Private Sub butZoom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butZoom.CheckedChanged
        If butZoom.Checked = True Then
            butMove.Checked = False
            butSelect.Checked = False
        End If
        If butZoom.Checked = False And butSelect.Checked = False And butMove.Checked = False Then
            butMove.Checked = True
        End If
    End Sub

    Private Sub butSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSelect.CheckedChanged
        If butSelect.Checked = True Then
            butMove.Checked = False
            butZoom.Checked = False
        End If
        If butZoom.Checked = False And butSelect.Checked = False And butMove.Checked = False Then
            butMove.Checked = True
        End If
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint

        If clsCourses.getInstance.Count < 1 Then Exit Sub
        clsCourses.getInstance.draw(e.Graphics, zoomLvl)
        'e.Graphics.DrawRectangle(New Pen(Color.Red, 3), debugRect)
        repaintRegion.MakeEmpty()
    End Sub

    Private Sub butLoadCourse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butLoadCourse.Click
        Dim filename As String
        ToolStripStatusLabel1.Text = "Open File"
        OpenFileDialog1.FileName = IO.Path.GetFileName(My.Settings.SavePath)

        OpenFileDialog1.AutoUpgradeEnabled = False
        OpenFileDialog1.InitialDirectory = IO.Path.GetDirectoryName(My.Settings.SavePath)
        OpenFileDialog1.Filter = "XML files|courseManager.xml"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            My.Settings.SavePath = filename
        Else
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Me.ToolStripProgressBar1.Value = 10
        ToolStripStatusLabel1.Text = "Loading..."
        clsCourseManager.getInstance(xmlFile:=filename, forceNew:=True)
        Me.ToolStripProgressBar1.Value = 33
        ToolStripStatusLabel1.Text = "Loading...Folders"
        clsFolders.getInstance(True).ReadXML(filename)
        Me.fillFolderList()
        Me.ToolStripProgressBar1.Value = 66
        ToolStripStatusLabel1.Text = "Loading...Courses"
        clsCourses.getInstance(Courses:=clsCourseManager.getCourses, forceNew:=True)
        clsCourses.getInstance.WaypointDistanceSetting = CDbl(Me.ToolStripComboBoxWPDistance.Text)
        clsCourses.getInstance.AlignmentWPCount = CInt(Me.TSWPCount.Text)
        Me.fillCourseList()

        'ToDo: Implement Settings and load settings xml in its own button Proc 
        '      Copy this code to the new Settings Open button
        '      Finish clsSettings Class
        'ToolStripStatusLabel1.Text = "Loading...Settings"
        'clsSettings.getInstance(True).ReadXML(filename)
        'Me.Cursor = Cursors.Default
        Me.PictureBox1.Invalidate()
        Me.ToolStripProgressBar1.Value = 100
        ToolStripStatusLabel1.Text = ""
    End Sub

    Private Sub fillCourseList()

        Dim crs As clsCourse
        Me.CrsList.clear()
        For Each crs In clsCourses.getInstance().CourseListItems
            Me.CrsList.createCourse(crs.Name, crs, False)
        Next

        'generate Tooltip
        Dim strInfo As String
        Dim buffFolder As clsFolder

        For Each crsListItem In CrsList.crsListItems
            crs = crsListItem.AttachedObject
            strInfo = "Id: " & crs.id
            strInfo &= Environment.NewLine & "Name: " & crs.Name

            buffFolder = clsFolders.getFolder(crs.parent)
            If buffFolder Is Nothing Then
                strInfo &= Environment.NewLine & "Folder: Root"
            Else
                strInfo &= Environment.NewLine & "Folder: " & buffFolder.Name
                buffFolder = Nothing
            End If

            strInfo &= Environment.NewLine & "Filename: " & Path.GetFileName(crs.sFileName)
            ToolTip1.SetToolTip(crsListItem.Label_Checkbox, strInfo)
        Next


        'eventhandler registrieren
        AddHandler Me.CrsList.CourseVisibilityChanged, AddressOf CourseVisibilityChanged
        AddHandler Me.CrsList.SelectionChanged, AddressOf CourseSelectionChanged

        Me.CrsList.SortList(crsListItems.SortOrder.Name)

    End Sub

    Private Sub CourseSelectionChanged(ByRef crsItem As crsListItem, byClick As Boolean)
        Dim crs As clsCourse

        Try
            crs = crsItem.AttachedObject
            If byClick Then crs.selectWP(0, False)
            selectedCrs = crs
            If crs IsNot Nothing Then
                Me.WPIDMcbx.Text = crs.SelectedWpIndex + 1
                Me.WPNumInfoLbl.Text = crs.WPCount
            Else
                Me.WPIDMcbx.Text = 0
                Me.WPNumInfoLbl.Text = 0
            End If
            Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CourseVisibilityChanged(Course As crsListItem, Visible As Boolean)
        Dim crsObject As clsCourse
        If TypeOf (Course.AttachedObject) Is clsCourse Then
            crsObject = Course.AttachedObject
            Course.AttachedObject.Hidden = Not Visible

            'Grafik neu zeichnen (Repaint Graphics)
            If Visible Then 'wenn sichtbar, nur den Bereich für den neuen Kurs zeichnen, sonst komplett (repaint region if visible and all if not visible)
                Me.PictureBox1.Invalidate(crsObject.DrawingArea(zoomLvl))
                debugRect = crsObject.DrawingArea(zoomLvl)
            Else
                Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
            End If
        End If
    End Sub

    Private Sub fillFolderList()
        Dim crsList As Dictionary(Of String, Boolean)
        crsList = clsFolders.getInstance.FolderList
        For Each pair As KeyValuePair(Of String, Boolean) In crsList
            ComboBox1.Items.Add(pair.Key)
        Next

        'ToDo Ordnerauswahl programmieren
    End Sub

    Private Sub mainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TBWP_X.ValidatingType = GetType(Double)
        TBWP_Y.ValidatingType = GetType(Double)
        TBWP_Angle.ValidatingType = GetType(Double)
        TBWP_Speed.ValidatingType = GetType(Double)
        WPIDMcbx.ValidatingType = GetType(Integer)
        'ToDo Auf eine anzahl an Stellen runden? 2 zum Beispiel? Wie viele Stellen schreibt CP?
        AddHandler clsWaypoint.SelectionChanged, AddressOf Me.selectionChangedHandler
        AddHandler clsCourse.SelectionChanged, AddressOf Me.selectedCourseChanged

        repaintRegion = New Region
        repaintRegion.MakeEmpty()

        Dim ms As New System.IO.MemoryStream(My.Resources.magnify)
        myZoomCursor = New Cursor(ms)
        ms = New System.IO.MemoryStream(My.Resources.grab)
        myGrabCursor = New Cursor(ms)
        ms = New System.IO.MemoryStream(My.Resources.grabbing)
        myGrabbingCursor = New Cursor(ms)
        ms.Dispose()

        Dim Version As System.Version
        Dim strVersion As String
        Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        strVersion = Version.Major & "." & Version.Minor & "." & Version.Revision
        Me.Text = "CourseDrawer " & Version.ToString()
        initBackgroundImage()

    End Sub

    Private Sub MTB_Double_TypeValidationCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TypeValidationEventArgs) Handles TBWP_X.TypeValidationCompleted, TBWP_Y.TypeValidationCompleted, TBWP_Speed.TypeValidationCompleted, TBWP_Angle.TypeValidationCompleted, WPIDMcbx.TypeValidationCompleted, WPIDMcbx.TypeValidationCompleted
        If Not e.IsValidInput Then
            ToolTip1.ToolTipTitle = "Invalid number"
            ToolTip1.Show("Data entered is not valid number...", sender, 5000)
            e.Cancel = True
        End If
    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseEnter
        If Me.butNewCourse.Checked = True Then
            Me.Cursor = Cursors.Arrow
        ElseIf Me.butMove.Checked = True Then
            Me.Cursor = myGrabCursor
        ElseIf Me.butZoom.Checked = True Then
            Me.Cursor = myZoomCursor
        Else
            Me.Cursor = Cursors.Arrow
        End If
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub selectionChangedHandler(ByRef wp As clsWaypoint)
        Dim IDText As String = "0/0"

        If wp Is Nothing Then
            Me.selectedWP = Nothing
            Me.butDeleteNode.Enabled = False
            Me.butInsertNode.Enabled = False
            Me.butAppendNode.Enabled = False
            Me.butDelCourse.Enabled = False
            Me.WPNextBtn.Enabled = False
            Me.WPPrevBtn.Enabled = False

            Me.butCalcAngleSel.Enabled = False
            Me.butRecalcAngleCrs.Enabled = False
            Me.sButFillNodes.Enabled = False

            Me.TBWP_X.Text = "0"
            Me.TBWP_Y.Text = "0"
            Me.TBWP_Angle.Text = "0"
            Me.TBWP_Speed.Text = "0"
            Me.ChWP_Rev.Checked = False
            Me.ChWP_Cross.Checked = False
            Me.ChWP_Wait.Checked = False
            Me.ChWP_Unload.Checked = False
            Me.ChWP_TurnStart.Checked = False
            Me.ChWP_TurnEnd.Checked = False
            Me.TBWP_X.Enabled = False
            Me.TBWP_Y.Enabled = False
            Me.TBWP_Angle.Enabled = False
            Me.TBWP_Speed.Enabled = False
            Me.WPIDMcbx.Enabled = False
            Me.ChWP_Rev.Enabled = False
            Me.ChWP_Cross.Enabled = False
            Me.ChWP_Wait.Enabled = False
            Me.ChWP_Unload.Enabled = False
            Me.ChWP_TurnStart.Enabled = False
            Me.ChWP_TurnEnd.Enabled = False

            Me.WPIDMcbx.Text = 0
            Me.WPNumInfoLbl.Text = 0

        Else
            Me.TBWP_X.Enabled = True
            Me.TBWP_Y.Enabled = True
            Me.TBWP_Angle.Enabled = True
            Me.TBWP_Speed.Enabled = True
            Me.WPIDMcbx.Enabled = True
            Me.ChWP_Rev.Enabled = True
            Me.ChWP_Unload.Enabled = True
            Me.ChWP_Cross.Enabled = True
            Me.ChWP_Wait.Enabled = True
            Me.ChWP_TurnStart.Enabled = True
            Me.ChWP_TurnEnd.Enabled = True
            Me.butCalcAngleSel.Enabled = True
            Me.butRecalcAngleCrs.Enabled = True
            Me.sButFillNodes.Enabled = True
            Me.WPNextBtn.Enabled = True
            Me.WPPrevBtn.Enabled = True

            Me.selectedWP = wp
            Me.TBWP_X.Text = Me.selectedWP.Pos_X.ToString
            Me.TBWP_Y.Text = Me.selectedWP.Pos_Y.ToString
            Me.TBWP_Angle.Text = Me.selectedWP.Angle.ToString
            Me.TBWP_Speed.Text = Me.selectedWP.Speed.ToString
            Me.ChWP_Rev.Checked = Me.selectedWP.Reverse
            Me.ChWP_Wait.Checked = Me.selectedWP.Wait
            Me.ChWP_Unload.Checked = Me.selectedWP.Unload
            Me.ChWP_Cross.Checked = Me.selectedWP.Cross
            Me.ChWP_TurnStart.Checked = Me.selectedWP.TurnStart
            Me.ChWP_TurnEnd.Checked = Me.selectedWP.TurnEnd

            Me.butDeleteNode.Enabled = True
            Me.butInsertNode.Enabled = True
            Me.butAppendNode.Enabled = True
            Me.butDelCourse.Enabled = True
            If Not selectedCrs Is Nothing Then
                Me.WPIDMcbx.Text = (selectedCrs.SelectedWpIndex + 1)
                Me.WPNumInfoLbl.Text = selectedCrs.WPCount
            Else
                Me.WPIDMcbx.Text = 0
                Me.WPNumInfoLbl.Text = 0
            End If

        End If
        'alway reset color for input boxes
        Me.TBWP_X.BackColor = Color.White
        Me.TBWP_Y.BackColor = Color.White
        Me.TBWP_Angle.BackColor = Color.White
        Me.TBWP_Speed.BackColor = Color.White

    End Sub

    Private Sub TBWP_X_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_X.Leave
        If selectedCrs IsNot Nothing Then
            selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
            If Me.selectedWP Is Nothing Then Exit Sub
            Double.TryParse(TBWP_X.Text, System.Globalization.NumberStyles.Any, TBWP_X.FormatProvider, Me.selectedWP.Pos_X)
            selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
            PictureBox1.Invalidate(repaintRegion)
        End If
    End Sub

    Private Sub TBWP_X_KeyDown(sender As Object, e As KeyEventArgs) Handles TBWP_X.KeyDown

        If e.KeyCode = Keys.Enter Then
            If selectedCrs IsNot Nothing Then
                selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
                If (setX(TBWP_X.Text, Me.selectedWP)) Then
                    TBWP_X.BackColor = Color.LightGreen
                    selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
                    PictureBox1.Invalidate(repaintRegion)
                End If
            End If
        Else
            TBWP_X.BackColor = Color.Orange
        End If
    End Sub

    Private Sub TBWP_Y_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_Y.Leave
        If selectedCrs IsNot Nothing Then
            selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
            If Me.selectedWP Is Nothing Then Exit Sub
            Double.TryParse(TBWP_Y.Text, System.Globalization.NumberStyles.Any, TBWP_Y.FormatProvider, Me.selectedWP.Pos_Y)
            selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
            PictureBox1.Invalidate(repaintRegion)
        End If
    End Sub

    Private Sub TBWP_Y_KeyDown(sender As Object, e As KeyEventArgs) Handles TBWP_Y.KeyDown
        If e.KeyCode = Keys.Enter Then
            If selectedCrs IsNot Nothing Then
                selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
                If (setY(TBWP_Y.Text, Me.selectedWP)) Then
                    TBWP_Y.BackColor = Color.LightGreen
                    selectedCrs.CreateRepaintArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
                    PictureBox1.Invalidate(repaintRegion)
                End If
            End If
        Else
            TBWP_Y.BackColor = Color.Orange
        End If
    End Sub

    Private Sub TBWP_Angle_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_Angle.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        'Double.TryParse(TBWP_Angle.Text, System.Globalization.NumberStyles.Any, TBWP_Angle.FormatProvider, Me.selectedWP.Angle)
        If (setAngle(TBWP_Angle.Text, Me.selectedWP)) Then
            TBWP_Angle.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub TBWP_Angle_KeyDown(sender As Object, e As KeyEventArgs) Handles TBWP_Angle.KeyDown
        If e.KeyCode = Keys.Enter Then
            If (setAngle(TBWP_Angle.Text, Me.selectedWP)) Then
                TBWP_Angle.BackColor = Color.LightGreen
            End If
        Else
            TBWP_Angle.BackColor = Color.Orange
        End If
    End Sub

    Private Sub TBWP_Speed_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_Speed.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        'Double.TryParse(TBWP_Speed.Text, System.Globalization.NumberStyles.Any, TBWP_Speed.FormatProvider, Me.selectedWP.Speed)
        If (setSpeed(TBWP_Speed.Text, Me.selectedWP)) Then
            TBWP_Speed.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub TBWP_Speed_KeyDown(sender As Object, e As KeyEventArgs) Handles TBWP_Speed.KeyDown
        If e.KeyCode = Keys.Enter Then
            If (setSpeed(TBWP_Speed.Text, Me.selectedWP)) Then
                TBWP_Speed.BackColor = Color.LightGreen
            End If
        Else
            TBWP_Speed.BackColor = Color.Orange
        End If
    End Sub

    Private Function setSpeed(ByRef Speed As String, ByRef WP As clsWaypoint) As Boolean
        If WP Is Nothing Then Return 0
        Dim nSpeed As Double
        If (Double.TryParse(Speed, nSpeed)) Then
            WP.Speed = nSpeed
            Return 1
        Else
            Speed = WP.Speed
            Return 0
        End If
    End Function

    Private Function setAngle(ByRef Angle As String, ByRef WP As clsWaypoint) As Boolean
        If WP Is Nothing Then Return 0
        Dim nAngle As Double
        If (Double.TryParse(Angle, nAngle)) Then
            WP.Angle = nAngle
            Return 1
        Else
            nAngle = WP.Angle
            Return 0
        End If
    End Function


    Private Function setX(ByRef X As String, ByRef WP As clsWaypoint) As Boolean
        If WP Is Nothing Then Return 0

        Dim nX As Double
        If (Double.TryParse(X, nX)) Then
            WP.Pos_X = nX
            Return 1
        Else
            X = WP.Pos_X
            Return 0
        End If

    End Function

    Private Function setY(ByRef Y As String, ByRef WP As clsWaypoint) As Boolean
        If WP Is Nothing Then Return 0
        Dim nY As Double
        If (Double.TryParse(Y, nY)) Then
            WP.Pos_Y = nY
            Return 1
        Else
            Y = WP.Pos_Y
            Return 0
        End If
    End Function

    Private Sub ChWP_Rev_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_Rev.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.Reverse = ChWP_Rev.Checked
    End Sub
    Private Sub ChWP_Unload_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_Unload.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.Unload = ChWP_Unload.Checked
    End Sub
    Private Sub ChWP_TurnStart_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_TurnStart.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.TurnStart = ChWP_TurnStart.Checked
    End Sub
    Private Sub ChWP_TurnEnd_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_TurnEnd.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.TurnEnd = ChWP_TurnEnd.Checked
    End Sub

    Private Sub ChWP_Wait_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_Wait.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.Wait = ChWP_Wait.Checked
    End Sub

    Private Sub ChWP_Cross_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_Cross.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.Cross = ChWP_Cross.Checked
    End Sub

    Private Sub butDeleteNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDeleteNode.Click
        clsCourses.getInstance.deleteSelectedWP()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
        'ToDo Multiselect programmieren
    End Sub

    Private Sub butInsertNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butInsertNode.Click
        clsCourses.getInstance.insertBeforeWP()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub butAppendNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAppendNode.Click
        clsCourses.getInstance.appendWP()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub butSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSelectAll.Click
        Dim CheckIt As Boolean
        If butSelectAll.Tag = 1 Then
            CheckIt = True
            butSelectAll.Text = "Unselect all"
            butSelectAll.Tag = 0
        Else
            CheckIt = False
            butSelectAll.Text = "Select all"
            butSelectAll.Tag = 1
        End If

        Me.CrsList.SelectionAll(CheckIt)

    End Sub

    Private Sub butSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSave.Click

        Dim changedCourses As New List(Of clsCourse)
        'Dim checkbox As Object
        For Each course As clsCourse In clsCourses.getChangedCourses
            If course.Equals(selectedCrs) Then Continue For
            saveCourse(course)
            Me.ToolStripStatusLabel1.Text = course.Name & " saved to " & course.fileName
        Next

        If Not selectedCrs Is Nothing Then
            saveCourse(selectedCrs)
            Me.ToolStripStatusLabel1.Text = selectedCrs.Name & " saved to " & selectedCrs.fileName
        End If

        'clsSettings.getInstance.SaveXML(root)
        'clsCourses.getInstance.SaveXML(root)
        'clsFolders.getInstance.SaveXML(root)
        'lSaveDialog = Nothing

    End Sub

    Private Sub saveCourse(course As clsCourse)
        Dim doc As XDocument
        Dim filename As String
        doc = New XDocument(New XDeclaration("1.0", "utf-8", "no"))
        doc.Document.Add(course.getXML(True))
        filename = course.fileName

        If System.IO.File.Exists(filename) = True Then
            Dim backupName As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filename), System.IO.Path.GetFileNameWithoutExtension(filename) & "_backup" & System.IO.Path.GetExtension(filename))
            If System.IO.File.Exists(backupName) Then System.IO.File.Delete(backupName)
            System.IO.File.Move(filename, backupName)
        End If
        doc.Save(filename)
    End Sub

    Private Sub selectedCourseChanged(ByRef crs As clsCourse)
        selectedCrs = crs
        If crs Is Nothing Then
            butDelCourse.Enabled = False
            TBCrs_Name.Enabled = False
            TBCrs_ID.Text = "0"
            TBCrs_Name.Text = ""
            WPNumInfoLbl.Text = 0
            CrsList.SelectItem(-1)

        Else
            butDelCourse.Enabled = True
            TBCrs_Name.Enabled = True
            TBCrs_ID.Text = crs.id.ToString
            TBCrs_Name.Text = crs.Name

            CrsList.SelectItem(crs.listIndex)
            WPIDMcbx.Text = (crs.SelectedWpIndex + 1)
            WPNumInfoLbl.Text = crs.WPCount

            ComboBox1.SelectedIndex = clsFolders.parentListIndex(crs.parent, clsFolders.getFolders)
        End If

    End Sub

    Private Sub TBCrs_Name_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBCrs_Name.Leave
        If selectedCrs Is Nothing Then Exit Sub
        selectedCrs.Name = TBCrs_Name.Text
        CrsList.SelectedCrsListItem.CourseName = selectedCrs.Name
    End Sub

    Private Sub butNewCourse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butNewCourse.Click

        Me.butSave.Enabled = False
        Me.butAppendNode.Enabled = False
        Me.butDelCourse.Enabled = False
        Me.butDeleteNode.Enabled = False
        Me.butInsertNode.Enabled = False
        Me.butLoadBGImage.Enabled = False
        Me.butMove.Enabled = False
        Me.butSave.Enabled = False
        Me.butLoadCourse.Enabled = False
        Me.butSelect.Enabled = False
        Me.butZoom.Enabled = False

        ToolTip1.ToolTipTitle = "Click in area"
        ToolTip1.Show("Click on place you want create new course...", PictureBox1, 5000)
        'ToDo Neuen Kurs erstellen 

    End Sub

    Private Sub butDelCourse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDelCourse.Click
        clsCourses.getInstance.deleteSelectedCrs()
        Me.fillCourseList()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub butCalcAngleSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCalcAngleSel.Click
        Dim ang As Double
        ang = clsCourses.getInstance.calculateAngleSelWP()
        Me.TBWP_Angle.Text = FormatNumber(ang.ToString, 2)
    End Sub

    Private Sub butRecalcAngleCrs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRecalcAngleCrs.Click
        clsCourses.getInstance.calculateAngleAllWP()
    End Sub

    Private Sub sButFillNodes_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sButFillNodes.ButtonClick
        clsCourses.getInstance.fillBeforeSelected(CInt(ToolStripComboBoxWPDistance.Text))
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub Distance5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Distance5ToolStripMenuItem.Click
        clsCourses.getInstance.fillBeforeSelected(5)
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub Distance10ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Distance10ToolStripMenuItem.Click
        clsCourses.getInstance.fillBeforeSelected(10)
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub Distance20ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Distance20ToolStripMenuItem.Click
        clsCourses.getInstance.fillBeforeSelected(20)
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub GuidingCircleTbx_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuidingCircleTbx.Leave
        Dim i As Integer
        Dim res As Boolean
        res = Integer.TryParse(GuidingCircleTbx.Text, i)
        If res = False Then i = 0
        GuidingCircleTbx.Text = i.ToString
        clsCourse.CircleDiameter = i
        If selectedCrs IsNot Nothing And selectedWP IsNot Nothing Then
            If selectedCrs.SelectedWpIndex >= 0 Then
                selectedCrs.CreateRepaintWaypointArea(repaintRegion, selectedCrs.SelectedWpIndex, zoomLvl)
                PictureBox1.Invalidate(repaintRegion)
            End If
        End If
    End Sub

    'ToDo Ordner-Strukur aktivieren

    Private Sub butZoom_Click(sender As Object, e As EventArgs) Handles butZoom.Click
        butZoom.Checked = Not butZoom.Checked
    End Sub

    Private Sub MapSizeSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MapSizeSelector.SelectedIndexChanged
        initBackgroundImage()
    End Sub


    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles butDebugInvalidating.Click
        Dim gr As Graphics
        gr = PictureBox1.CreateGraphics
        gr.FillRectangle(Brushes.Aquamarine, New RectangleF(0, 0, PictureBox1.Width, PictureBox1.Height))
        gr.Dispose()
    End Sub

    Private Sub WPPrevBtn_Click(sender As Object, e As EventArgs) Handles WPPrevBtn.Click
        'OnClick select Previous Waypoint of the course
        Dim wpindex As Integer

        If Me.selectedWP Is Nothing Then Exit Sub

        If selectedCrs IsNot Nothing Then
            selectedCrs.CreateRepaintWaypointArea(repaintRegion, Me.selectedCrs.SelectedWpIndex, zoomLvl)
            With selectedCrs

                If .SelectedWpIndex < 1 Then
                    wpindex = .WPCount - 1
                Else
                    wpindex = .SelectedWpIndex - 1
                End If
                .selectWP(wpindex)
                selectedCrs.CreateRepaintWaypointArea(repaintRegion, Me.selectedCrs.SelectedWpIndex, zoomLvl)
                PictureBox1.Invalidate(repaintRegion)

            End With
        End If
    End Sub

    Private Sub WPNextBtn_Click(sender As Object, e As EventArgs) Handles WPNextBtn.Click
        'OnClick select next Waypoint of the course
        Dim wpindex As Integer
        If Me.selectedWP Is Nothing Then Exit Sub

        If selectedCrs IsNot Nothing Then
            selectedCrs.CreateRepaintWaypointArea(repaintRegion, Me.selectedCrs.SelectedWpIndex, zoomLvl)

            With selectedCrs
                If .SelectedWpIndex >= .WPCount - 1 Then
                    wpindex = 0
                Else
                    wpindex = .SelectedWpIndex + 1
                End If
                .selectWP(wpindex)
                selectedCrs.CreateRepaintWaypointArea(repaintRegion, Me.selectedCrs.SelectedWpIndex, zoomLvl)

                PictureBox1.Invalidate(repaintRegion)
            End With
        End If
    End Sub

    Private Sub WPIDMcbx_Leave(sender As Object, e As EventArgs) Handles WPIDMcbx.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        repaintRegion.Union(New RectangleF(Me.selectedWP.PositionScreenDraw(Me.zoomLvl).X - 5, Me.selectedWP.PositionScreenDraw(Me.zoomLvl).Y - 5, 10, 10))
        If Me.selectedCrs IsNot Nothing And Me.WPIDMcbx.Text > 0 Then
            Me.selectedCrs.selectWP(Me.WPIDMcbx.Text - 1)
        End If
        repaintRegion.Union(New RectangleF(Me.selectedWP.PositionScreenDraw(Me.zoomLvl).X - 5, Me.selectedWP.PositionScreenDraw(Me.zoomLvl).Y - 5, 10, 10))
        PictureBox1.Invalidate(repaintRegion)
    End Sub

    Private Sub WPIDMcbx_KeyDown(sender As Object, e As KeyEventArgs) Handles WPIDMcbx.KeyDown
        If Me.selectedWP Is Nothing Then Exit Sub
        repaintRegion.Union(New RectangleF(Me.selectedWP.PositionScreenDraw(Me.zoomLvl).X - 5, Me.selectedWP.PositionScreenDraw(Me.zoomLvl).Y - 5, 10, 10))
        Dim InputNumber As Integer
        Integer.TryParse(Me.WPIDMcbx.Text, InputNumber)
        If e.KeyCode = Keys.Enter And Me.selectedCrs IsNot Nothing And InputNumber > 0 Then
            Me.selectedCrs.selectWP(Me.WPIDMcbx.Text - 1)
        End If
        repaintRegion.Union(New RectangleF(Me.selectedWP.PositionScreenDraw(Me.zoomLvl).X - 5, Me.selectedWP.PositionScreenDraw(Me.zoomLvl).Y - 5, 10, 10))
        PictureBox1.Invalidate(repaintRegion)
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        Me.DebugPos.Text = "x: " & e.X & " y: " & e.Y
    End Sub

    Private Sub ToolStripComboBoxWPDistance_TextChanged(sender As Object, e As EventArgs) Handles ToolStripComboBoxWPDistance.TextChanged

        Dim DistanceValue As Single
        Dim ParsingResult As Boolean
        ParsingResult = Single.TryParse(ToolStripComboBoxWPDistance.Text, DistanceValue)
        If ParsingResult = False Then
            DistanceValue = 10
            ToolStripComboBoxWPDistance.Text = DistanceValue.ToString
        End If
        clsCourses.getInstance.WaypointDistanceSetting = DistanceValue

    End Sub

    Private Sub TSWPCount_TextChanged(sender As Object, e As EventArgs) Handles TSWPCount.TextChanged

        Dim WpCountValue As Integer
        Dim ParsingResult As Boolean
        ParsingResult = Integer.TryParse(TSWPCount.Text, WpCountValue)
        If ParsingResult = False Then
            WpCountValue = 2
            ToolStripComboBoxWPDistance.Text = WpCountValue.ToString
        End If
        clsCourses.getInstance.AlignmentWPCount = WpCountValue

    End Sub

    Private Sub AlignHorizontalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlignHorizontalToolStripMenuItem.Click
        clsCourses.getInstance.AlignWpSegmentHorizontal()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub AlignVerticalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlignVerticalToolStripMenuItem.Click
        clsCourses.getInstance.AlignWpSegmentVertical()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub
End Class
