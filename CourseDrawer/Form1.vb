Public Class Form1
    Const zoomStep As Integer = 50
    Dim zoomLvl As Integer = 50
    Dim myMousePos As Point
    Dim myLocation As PointF
    Dim selectedWP As clsWaypoint
    Dim selectedCrs As clsCourse
    Dim myZoomCursor As Cursor
    Dim doListChange As Boolean = False
    Dim firstDraw As Boolean = False

    ''' <summary>
    ''' Loads bitmap as background
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub butLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butLoadBGImage.Click
        Dim filename As String
        Dim mapdds As System.Drawing.Bitmap
        OpenFileDialog1.FileName = IO.Path.GetFileName(My.Settings("MapPath").ToString)
        OpenFileDialog1.InitialDirectory = IO.Path.GetDirectoryName(My.Settings("MapPath").ToString)
        OpenFileDialog1.Filter = "DDS picture|*.dds"
        OpenFileDialog1.Filter = "BMP picture|*.bmp"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            My.Settings("MapPath") = filename
        Else
            Exit Sub
        End If
        mapdds = DevIL.DevIL.LoadBitmap(filename)
        zoomLvl = 50
        PictureBox1.Image = mapdds
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Me.panel1.AutoScrollPosition = New Point(0, 0)
        clsWaypoint.mapSize = Me.PictureBox1.Image.Size
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

            Dim crsList As Dictionary(Of String, Boolean)
            crsList = clsCourses.getInstance.CourseList
            Me.CheckedListBox1.Items.Clear()
            For Each pair As KeyValuePair(Of String, Boolean) In crsList
                Me.CheckedListBox1.Items.Add(pair.Key, pair.Value)
            Next
            Me.doListChange = True
            Me.CheckedListBox1.SelectedIndex = Me.CheckedListBox1.Items.Count - 1

            Me.butSave.Enabled = True
            Me.butAppendNode.Enabled = True
            Me.butDelCourse.Enabled = True
            Me.butDeleteNode.Enabled = True
            Me.butInsertNode.Enabled = True
            Me.butLoadBGImage.Enabled = True
            Me.butNewCourse.Checked = False
            Me.butSave.Enabled = True
            Me.butSaveGame.Enabled = True
            Me.butSelect.Enabled = True
            Me.butZoom.Enabled = True
            Me.butMove.Enabled = True

            ToolTip1.RemoveAll()

        ElseIf butZoom.Checked = True Then
            If PictureBox1.Image Is Nothing Then
                locSize = New Size(4096, 4096)
            Else
                locSize = PictureBox1.Image.Size
            End If

            'Picture size (zoom)
            If ev.Button = Windows.Forms.MouseButtons.Left Then
                If zoomLvl < 4000 Then
                    zoomLvl += zoomStep
                Else
                    'MsgBox("max level reached")
                End If
            ElseIf ev.Button = Windows.Forms.MouseButtons.Right Then
                If zoomLvl > zoomStep Then
                    zoomLvl -= zoomStep
                Else
                    'MsgBox("min level reached")
                End If
            ElseIf ev.Button = Windows.Forms.MouseButtons.Middle Then
                zoomLvl = 50
            End If
            locSize.Width = locSize.Width * zoomLvl / 100
            locSize.Height = locSize.Height * zoomLvl / 100
            PictureBox1.Size = locSize

            'Picture location (center on click spot)
            Dim posOffset As New Point((origPoint.X * zoomLvl / 100) - (panel1.Size.Width / 2), (origPoint.Y * zoomLvl / 100) - (panel1.Size.Height / 2))
            panel1.AutoScrollPosition = posOffset
        ElseIf butSelect.Checked = True Then
            clsCourses.getInstance.selectWP(origPoint)
        End If
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If butMove.Checked = True Then
            myMousePos = Cursor.Position
            TimerDragPicture.Enabled = True
        End If
        If butSelect.Checked = True Then
            Dim origPoint As New PointF(e.Location.X * 100 / zoomLvl, e.Location.Y * 100 / zoomLvl)
            myMousePos = Cursor.Position
            clsCourses.getInstance.selectWP(origPoint)
            myLocation = e.Location
            TimerDragPicture.Enabled = True
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
            Me.selectedWP.setNewPos(origPoint)
            myLocation = newOffset
            If clsCourse.CircleDiameter > 0 Then invRange = (clsCourse.CircleDiameter * 2 * zoomLvl / 100) + 20
            If invRange < 100 Then invRange = 100
            If Me.firstDraw = True Then
                Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
                Me.firstDraw = False
            Else
                Me.PictureBox1.Invalidate(New System.Drawing.Rectangle(newOffset.X - invRange / 2, newOffset.Y - invRange / 2, invRange, invRange))
            End If
        End If
        myMousePos = newMousePos
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        If TimerDragPicture.Enabled = True Then TimerDragPicture.Enabled = False
        Me.firstDraw = True
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

    End Sub

    Private Sub butSaveGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSaveGame.Click
        Dim filename As String

        OpenFileDialog1.FileName = IO.Path.GetFileName(My.Settings("SavePath").ToString)
        OpenFileDialog1.InitialDirectory = IO.Path.GetDirectoryName(My.Settings("SavePath").ToString)
        OpenFileDialog1.Filter = "XML files|*.xml"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            My.Settings("SavePath") = filename
        Else
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        ToolStripStatusLabel1.Text = "Loading..."
        clsCourseManager.getInstance(xmlFile:=filename, forceNew:=True)
        clsCourses.getInstance(Courses:=clsCourseManager.getCourses, forceNew:=True)
        Me.fillCourseList()
        clsFolders.getInstance(True).ReadXML(filename)
        Me.fillFolderList()
        clsSettings.getInstance(True).ReadXML(filename)
        Me.Cursor = Cursors.Default
        ToolStripStatusLabel1.Text = ""
    End Sub

    Private Sub fillCourseList()
        Dim crsList As Dictionary(Of String, Boolean)
        crsList = clsCourses.getInstance.CourseList
        Me.CheckedListBox1.Items.Clear()
        For Each pair As KeyValuePair(Of String, Boolean) In crsList
            Me.CheckedListBox1.Items.Add(pair.Key, pair.Value)
        Next
    End Sub
    Private Sub fillFolderList()
        Dim crsList As Dictionary(Of String, Boolean)
        crsList = clsFolders.getInstance.FolderList
        For Each pair As KeyValuePair(Of String, Boolean) In crsList
            ComboBox1.Items.Add(pair.Key)
        Next
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TBWP_X.ValidatingType = GetType(Double)
        TBWP_Y.ValidatingType = GetType(Double)
        TBWP_Angle.ValidatingType = GetType(Double)
        TBWP_Speed.ValidatingType = GetType(Double)
        AddHandler clsWaypoint.SelectionChanged, AddressOf Me.selectionChangedHandler
        AddHandler clsCourse.SelectionChanged, AddressOf Me.selectedCourseChanged

        Dim ms As New System.IO.MemoryStream(My.Resources.magnify)
        myZoomCursor = New Cursor(ms)
        Me.Text = "CourseDrawer " & Me.ProductVersion
        ms.Dispose()

    End Sub


    Private Sub MTB_Double_TypeValidationCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TypeValidationEventArgs) Handles TBWP_X.TypeValidationCompleted, TBWP_Y.TypeValidationCompleted, TBWP_Speed.TypeValidationCompleted, TBWP_Angle.TypeValidationCompleted
        If Not e.IsValidInput Then
            ToolTip1.ToolTipTitle = "Invalid number"
            ToolTip1.Show("Data entered is not valid number...", sender, 5000)
            e.Cancel = True
        End If
    End Sub


    Private Sub CheckedListBox1_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Dim hide As Boolean
        If e.NewValue = CheckState.Checked Then
            hide = False
        Else
            hide = True
        End If
        If clsCourses.getInstance.ItemHide(e.Index, hide) = False Then e.NewValue = e.CurrentValue
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseEnter
        If Me.butNewCourse.Checked = True Then
            Me.Cursor = Cursors.Arrow
        ElseIf Me.butMove.Checked = True Then
            Me.Cursor = Cursors.Hand
        ElseIf Me.butZoom.Checked = True Then
            'Cursor = Cursors.SizeAll
            Me.Cursor = myZoomCursor
        Else
            Me.Cursor = Cursors.Arrow
        End If
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub selectionChangedHandler(ByRef wp As clsWaypoint)
        If wp Is Nothing Then
            Me.selectedWP = Nothing
            Me.butDeleteNode.Enabled = False
            Me.butInsertNode.Enabled = False
            Me.butAppendNode.Enabled = False
            Me.butDelCourse.Enabled = False

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
            Me.ChWP_TurnStart.Checked = False
            Me.ChWP_TurnEnd.Checked = False
            Me.TBWP_X.Enabled = False
            Me.TBWP_Y.Enabled = False
            Me.TBWP_Angle.Enabled = False
            Me.TBWP_Speed.Enabled = False
            Me.ChWP_Rev.Enabled = False
            Me.ChWP_Cross.Enabled = False
            Me.ChWP_Wait.Enabled = False
            Me.ChWP_TurnStart.Enabled = False
            Me.ChWP_TurnEnd.Enabled = False

        Else
            Me.TBWP_X.Enabled = True
            Me.TBWP_Y.Enabled = True
            Me.TBWP_Angle.Enabled = True
            Me.TBWP_Speed.Enabled = True
            Me.ChWP_Rev.Enabled = True
            Me.ChWP_Cross.Enabled = True
            Me.ChWP_Wait.Enabled = True
            Me.ChWP_TurnStart.Enabled = True
            Me.ChWP_TurnEnd.Enabled = True
            Me.butCalcAngleSel.Enabled = True
            Me.butRecalcAngleCrs.Enabled = True
            Me.sButFillNodes.Enabled = True

            Me.selectedWP = wp
            Me.TBWP_X.Text = Me.selectedWP.Pos_X.ToString
            Me.TBWP_Y.Text = Me.selectedWP.Pos_Y.ToString
            Me.TBWP_Angle.Text = Me.selectedWP.Angle.ToString
            Me.TBWP_Speed.Text = Me.selectedWP.Speed.ToString
            Me.ChWP_Rev.Checked = Me.selectedWP.Reverse
            Me.ChWP_Cross.Checked = Me.selectedWP.Cross
            Me.ChWP_Wait.Checked = Me.selectedWP.Wait
            Me.ChWP_TurnStart.Checked = Me.selectedWP.TurnStart
            Me.ChWP_TurnEnd.Checked = Me.selectedWP.TurnEnd

            Me.butDeleteNode.Enabled = True
            Me.butInsertNode.Enabled = True
            Me.butAppendNode.Enabled = True
            Me.butDelCourse.Enabled = True
        End If
    End Sub

    Private Sub TBWP_X_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_X.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        Double.TryParse(TBWP_X.Text, System.Globalization.NumberStyles.Any, TBWP_X.FormatProvider, Me.selectedWP.Pos_X)
    End Sub

    Private Sub TBWP_Y_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_Y.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        Double.TryParse(TBWP_Y.Text, System.Globalization.NumberStyles.Any, TBWP_Y.FormatProvider, Me.selectedWP.Pos_Y)
    End Sub

    Private Sub TBWP_Angle_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_Angle.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        Double.TryParse(TBWP_Angle.Text, System.Globalization.NumberStyles.Any, TBWP_Angle.FormatProvider, Me.selectedWP.Angle)
    End Sub

    Private Sub TBWP_Speed_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBWP_Speed.Leave
        If Me.selectedWP Is Nothing Then Exit Sub
        Double.TryParse(TBWP_Speed.Text, System.Globalization.NumberStyles.Any, TBWP_Speed.FormatProvider, Me.selectedWP.Speed)
    End Sub

    Private Sub ChWP_Rev_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChWP_Rev.CheckStateChanged
        If Me.selectedWP Is Nothing Then Exit Sub
        Me.selectedWP.Reverse = ChWP_Rev.Checked
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
        If Me.CheckedListBox1.CheckedItems.Count = Me.CheckedListBox1.Items.Count Then
            CheckIt = False
        Else
            CheckIt = True
        End If

        For i As Integer = 0 To Me.CheckedListBox1.Items.Count - 1
            Me.CheckedListBox1.SetItemChecked(i, CheckIt)
        Next
    End Sub

    Private Sub butSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSave.Click
        Dim lSaveDialog As New Windows.Forms.SaveFileDialog
        Dim filename As String
        Dim doc As XDocument


        'lSaveDialog.FileName = IO.Path.GetFileName(My.Settings("SavePath").ToString)
        'lSaveDialog.InitialDirectory = IO.Path.GetDirectoryName(My.Settings("SavePath").ToString)
        'lSaveDialog.Filter = "XML files|*.xml"
        'If lSaveDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
        ' filename = lSaveDialog.FileName
        ' Else
        ' Exit Sub
        ' End If




        doc = New XDocument(New XDeclaration("1.0", "utf-8", "no"))
        doc.Document.Add(selectedCrs.getXML(True))
        filename = selectedCrs.fileName
        'clsSettings.getInstance.SaveXML(root)
        'clsCourses.getInstance.SaveXML(root)
        'clsFolders.getInstance.SaveXML(root)




        If System.IO.File.Exists(filename) = True Then
            Dim backupName As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filename), System.IO.Path.GetFileNameWithoutExtension(filename) & "_backup" & System.IO.Path.GetExtension(filename))
            If System.IO.File.Exists(backupName) Then System.IO.File.Delete(backupName)
            System.IO.File.Move(filename, backupName)
        End If
        doc.Save(filename)



        'lSaveDialog = Nothing
    End Sub
    Private Sub selectedCourseChanged(ByRef crs As clsCourse)
        If crs Is Nothing Then
            butDelCourse.Enabled = False
            TBCrs_Name.Enabled = False
            TBCrs_ID.Text = "0"
            TBCrs_Name.Text = ""
            CheckedListBox1.SelectedItems.Clear()
        Else
            butDelCourse.Enabled = True
            TBCrs_Name.Enabled = True
            TBCrs_ID.Text = crs.id.ToString
            TBCrs_Name.Text = crs.Name
            If CheckedListBox1.Items.Count > crs.listIndex Then
                If CheckedListBox1.SelectedIndex <> crs.listIndex Then
                    CheckedListBox1.SelectedIndex = crs.listIndex
                End If
            End If
            ComboBox1.SelectedIndex = clsFolders.parentListIndex(crs.parent, clsFolders.getFolders)
        End If
        selectedCrs = crs

        'selectedCrs.selectWP(selectedCrs.WPCount)
        'selsectedwp = 
    End Sub

    Private Sub TBCrs_Name_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBCrs_Name.Leave
        If selectedCrs Is Nothing Then Exit Sub
        selectedCrs.Name = TBCrs_Name.Text
        Me.CheckedListBox1.Items(selectedCrs.id - 1) = selectedCrs.id & " : " & selectedCrs.Name
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
        Me.butSaveGame.Enabled = False
        Me.butSelect.Enabled = False
        Me.butZoom.Enabled = False

        ToolTip1.ToolTipTitle = "Click in area"
        ToolTip1.Show("Click on place you want create new course...", PictureBox1, 5000)

    End Sub

    Private Sub butDelCourse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butDelCourse.Click
        clsCourses.getInstance.deleteSelectedCrs()
        Me.fillCourseList()
        Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
    End Sub

    Private Sub butCalcAngleSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCalcAngleSel.Click
        Dim ang As Double
        ang = clsCourses.getInstance.calculateAngleSelWP()
        Me.TBWP_Angle.Text = ang.ToString
    End Sub

    Private Sub butRecalcAngleCrs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRecalcAngleCrs.Click
        clsCourses.getInstance.calculateAngleAllWP()
    End Sub

    Private Sub sButFillNodes_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sButFillNodes.ButtonClick
        clsCourses.getInstance.fillBeforeSelected(10)
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

    Private Sub ToolStripTextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.Leave
        Dim i As Integer
        Dim res As Boolean
        res = Integer.TryParse(ToolStripTextBox1.Text, i)
        If res = False Then i = 0
        ToolStripTextBox1.Text = i.ToString
        clsCourse.CircleDiameter = i
    End Sub

    Private Sub butCrsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCrsUp.Click
        If Me.CheckedListBox1.SelectedIndex > 0 Then
            Dim selID As Integer = Me.CheckedListBox1.SelectedIndex
            clsCourses.getInstance.moveCourseUp(selID + 1)
            Me.fillCourseList()
            Me.CheckedListBox1.SelectedIndex = selID - 1
        End If
    End Sub

    Private Sub butCrsDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCrsDown.Click
        If Me.CheckedListBox1.SelectedIndex >= 0 And Me.CheckedListBox1.SelectedIndex < Me.CheckedListBox1.Items.Count - 1 Then
            Dim selID As Integer = Me.CheckedListBox1.SelectedIndex
            clsCourses.getInstance.moveCourseDown(selID + 1)
            Me.fillCourseList()
            Me.CheckedListBox1.SelectedIndex = selID + 1
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        If Me.doListChange = True Then
            If Me.CheckedListBox1.SelectedItems.Count > 0 Then
                clsCourses.getInstance.selectWP(Me.CheckedListBox1.SelectedIndex + 1)
                Me.PictureBox1.Invalidate(New Drawing.Rectangle(-Me.panel1.AutoScrollPosition.X, -Me.panel1.AutoScrollPosition.Y, Me.panel1.Width, Me.panel1.Height))
            End If
        End If
        Me.doListChange = False
    End Sub

    Private Sub CheckedListBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CheckedListBox1.MouseClick
        Me.doListChange = True
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs)

    End Sub

    Private Sub TBWP_Speed_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles TBWP_Speed.MaskInputRejected

    End Sub

    Private Sub TBCrs_Name_TextChanged(sender As Object, e As EventArgs) Handles TBCrs_Name.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub butZoom_Click(sender As Object, e As EventArgs) Handles butZoom.Click
        butZoom.Checked = Not butZoom.Checked
    End Sub
End Class
