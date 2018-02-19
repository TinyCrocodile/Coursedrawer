<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                myZoomCursor.Dispose()
                myGrabbingCursor.Dispose()
                myGrabCursor.Dispose()
                repaintRegion.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.butLoadCourse = New System.Windows.Forms.ToolStripButton()
        Me.butLoadBGImage = New System.Windows.Forms.ToolStripButton()
        Me.butSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.butMove = New System.Windows.Forms.ToolStripButton()
        Me.butZoom = New System.Windows.Forms.ToolStripButton()
        Me.butSelect = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.butAppendNode = New System.Windows.Forms.ToolStripButton()
        Me.butInsertNode = New System.Windows.Forms.ToolStripButton()
        Me.butDeleteNode = New System.Windows.Forms.ToolStripButton()
        Me.sButFillNodes = New System.Windows.Forms.ToolStripSplitButton()
        Me.Distance5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Distance10ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Distance20ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.butNewCourse = New System.Windows.Forms.ToolStripButton()
        Me.butDelCourse = New System.Windows.Forms.ToolStripButton()
        Me.butRecalcAngleCrs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabelMapSize = New System.Windows.Forms.ToolStripLabel()
        Me.MapSizeSelector = New System.Windows.Forms.ToolStripComboBox()
        Me.butDebugInvalidating = New System.Windows.Forms.ToolStripButton()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TimerDragPicture = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.butSelectAll = New System.Windows.Forms.Button()
        Me.WPNextBtn = New System.Windows.Forms.Button()
        Me.WPPrevBtn = New System.Windows.Forms.Button()
        Me.WPIDMcbx = New System.Windows.Forms.MaskedTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.WPSpeedLbl = New System.Windows.Forms.Label()
        Me.butCalcAngleSel = New System.Windows.Forms.Button()
        Me.ChWP_Cross = New System.Windows.Forms.CheckBox()
        Me.ChWP_Wait = New System.Windows.Forms.CheckBox()
        Me.ChWP_Rev = New System.Windows.Forms.CheckBox()
        Me.WPCbxSeparator = New System.Windows.Forms.Label()
        Me.WPTextSeparator = New System.Windows.Forms.Label()
        Me.ChWP_TurnStart = New System.Windows.Forms.CheckBox()
        Me.ChWP_Unload = New System.Windows.Forms.CheckBox()
        Me.ChWP_TurnEnd = New System.Windows.Forms.CheckBox()
        Me.WPAngleLbl = New System.Windows.Forms.Label()
        Me.WPPosYLbl = New System.Windows.Forms.Label()
        Me.TBWP_Speed = New System.Windows.Forms.MaskedTextBox()
        Me.TBWP_Angle = New System.Windows.Forms.MaskedTextBox()
        Me.TBWP_Y = New System.Windows.Forms.MaskedTextBox()
        Me.WPNumInfoLbl = New System.Windows.Forms.Label()
        Me.WPNumberTextLbl = New System.Windows.Forms.Label()
        Me.WPIDTextLbl = New System.Windows.Forms.Label()
        Me.WPPosXLbl = New System.Windows.Forms.Label()
        Me.TBWP_X = New System.Windows.Forms.MaskedTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TBCrs_Name = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TBCrs_ID = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CrsList = New CourseDrawer.crsListItems()
        Me.ClsCourseBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClsCourseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.ClsCourseBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClsCourseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.butLoadCourse, Me.butLoadBGImage, Me.butSave, Me.ToolStripSeparator1, Me.butMove, Me.butZoom, Me.butSelect, Me.ToolStripSeparator2, Me.butAppendNode, Me.butInsertNode, Me.butDeleteNode, Me.sButFillNodes, Me.ToolStripSeparator3, Me.ToolStripLabel1, Me.ToolStripTextBox1, Me.ToolStripSeparator4, Me.butNewCourse, Me.butDelCourse, Me.butRecalcAngleCrs, Me.ToolStripSeparator5, Me.ToolStripLabelMapSize, Me.MapSizeSelector, Me.butDebugInvalidating})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1299, 39)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'butLoadCourse
        '
        Me.butLoadCourse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butLoadCourse.Image = Global.CourseDrawer.My.Resources.Resources.butOpen
        Me.butLoadCourse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butLoadCourse.Name = "butLoadCourse"
        Me.butLoadCourse.Size = New System.Drawing.Size(36, 36)
        Me.butLoadCourse.Text = "Savegame"
        Me.butLoadCourse.ToolTipText = "Load XML"
        '
        'butLoadBGImage
        '
        Me.butLoadBGImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butLoadBGImage.Image = Global.CourseDrawer.My.Resources.Resources.butMap
        Me.butLoadBGImage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butLoadBGImage.Name = "butLoadBGImage"
        Me.butLoadBGImage.Size = New System.Drawing.Size(36, 36)
        Me.butLoadBGImage.Text = "Map"
        Me.butLoadBGImage.ToolTipText = "Load bitmap Map"
        '
        'butSave
        '
        Me.butSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butSave.Image = Global.CourseDrawer.My.Resources.Resources.butSave
        Me.butSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butSave.Name = "butSave"
        Me.butSave.Size = New System.Drawing.Size(36, 36)
        Me.butSave.Text = "Save"
        Me.butSave.ToolTipText = "Save XML"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'butMove
        '
        Me.butMove.Checked = True
        Me.butMove.CheckOnClick = True
        Me.butMove.CheckState = System.Windows.Forms.CheckState.Checked
        Me.butMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butMove.Image = Global.CourseDrawer.My.Resources.Resources.butMove
        Me.butMove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butMove.Name = "butMove"
        Me.butMove.Size = New System.Drawing.Size(36, 36)
        Me.butMove.Text = "Move"
        Me.butMove.ToolTipText = "Move map"
        '
        'butZoom
        '
        Me.butZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butZoom.Image = Global.CourseDrawer.My.Resources.Resources.butZoom
        Me.butZoom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butZoom.Name = "butZoom"
        Me.butZoom.Size = New System.Drawing.Size(36, 36)
        Me.butZoom.Text = "Zoom"
        Me.butZoom.ToolTipText = "Zoom map"
        '
        'butSelect
        '
        Me.butSelect.CheckOnClick = True
        Me.butSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butSelect.Image = Global.CourseDrawer.My.Resources.Resources.butSelect
        Me.butSelect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butSelect.Name = "butSelect"
        Me.butSelect.Size = New System.Drawing.Size(36, 36)
        Me.butSelect.Text = "Select"
        Me.butSelect.ToolTipText = "Select Waypoint"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'butAppendNode
        '
        Me.butAppendNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butAppendNode.Enabled = False
        Me.butAppendNode.Image = Global.CourseDrawer.My.Resources.Resources.butAddNode
        Me.butAppendNode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butAppendNode.Name = "butAppendNode"
        Me.butAppendNode.Size = New System.Drawing.Size(36, 36)
        Me.butAppendNode.Text = "Add"
        Me.butAppendNode.ToolTipText = "Append waypoint"
        '
        'butInsertNode
        '
        Me.butInsertNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butInsertNode.Enabled = False
        Me.butInsertNode.Image = Global.CourseDrawer.My.Resources.Resources.butInsertNode
        Me.butInsertNode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butInsertNode.Name = "butInsertNode"
        Me.butInsertNode.Size = New System.Drawing.Size(36, 36)
        Me.butInsertNode.Text = "Insert"
        Me.butInsertNode.ToolTipText = "Insert waypoint"
        '
        'butDeleteNode
        '
        Me.butDeleteNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butDeleteNode.Enabled = False
        Me.butDeleteNode.Image = Global.CourseDrawer.My.Resources.Resources.butDeleteNode
        Me.butDeleteNode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butDeleteNode.Name = "butDeleteNode"
        Me.butDeleteNode.Size = New System.Drawing.Size(36, 36)
        Me.butDeleteNode.Text = "Delete"
        Me.butDeleteNode.ToolTipText = "Delete waypoint"
        '
        'sButFillNodes
        '
        Me.sButFillNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.sButFillNodes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Distance5ToolStripMenuItem, Me.Distance10ToolStripMenuItem, Me.Distance20ToolStripMenuItem})
        Me.sButFillNodes.Enabled = False
        Me.sButFillNodes.Image = Global.CourseDrawer.My.Resources.Resources.butFillNodes
        Me.sButFillNodes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.sButFillNodes.Name = "sButFillNodes"
        Me.sButFillNodes.Size = New System.Drawing.Size(48, 36)
        Me.sButFillNodes.Text = "Fill Nodes"
        Me.sButFillNodes.ToolTipText = "Fill Nodes between actual and previous waypoint"
        '
        'Distance5ToolStripMenuItem
        '
        Me.Distance5ToolStripMenuItem.Name = "Distance5ToolStripMenuItem"
        Me.Distance5ToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.Distance5ToolStripMenuItem.Text = "Distance 5"
        '
        'Distance10ToolStripMenuItem
        '
        Me.Distance10ToolStripMenuItem.Name = "Distance10ToolStripMenuItem"
        Me.Distance10ToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.Distance10ToolStripMenuItem.Text = "Distance 10"
        '
        'Distance20ToolStripMenuItem
        '
        Me.Distance20ToolStripMenuItem.Name = "Distance20ToolStripMenuItem"
        Me.Distance20ToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.Distance20ToolStripMenuItem.Text = "Distance 20"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(37, 36)
        Me.ToolStripLabel1.Text = "Circle"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(50, 39)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 39)
        '
        'butNewCourse
        '
        Me.butNewCourse.CheckOnClick = True
        Me.butNewCourse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butNewCourse.Enabled = False
        Me.butNewCourse.Image = Global.CourseDrawer.My.Resources.Resources.butNewCrs
        Me.butNewCourse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butNewCourse.Name = "butNewCourse"
        Me.butNewCourse.Size = New System.Drawing.Size(36, 36)
        Me.butNewCourse.Text = "NewCourse"
        '
        'butDelCourse
        '
        Me.butDelCourse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butDelCourse.Enabled = False
        Me.butDelCourse.Image = Global.CourseDrawer.My.Resources.Resources.butDelCrs
        Me.butDelCourse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butDelCourse.Name = "butDelCourse"
        Me.butDelCourse.Size = New System.Drawing.Size(36, 36)
        Me.butDelCourse.Text = "DelCourse"
        '
        'butRecalcAngleCrs
        '
        Me.butRecalcAngleCrs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butRecalcAngleCrs.Enabled = False
        Me.butRecalcAngleCrs.Image = Global.CourseDrawer.My.Resources.Resources.butRecalcDir
        Me.butRecalcAngleCrs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butRecalcAngleCrs.Name = "butRecalcAngleCrs"
        Me.butRecalcAngleCrs.Size = New System.Drawing.Size(36, 36)
        Me.butRecalcAngleCrs.Text = "Recalc directions"
        Me.butRecalcAngleCrs.ToolTipText = "Recalc selected course directions"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripLabelMapSize
        '
        Me.ToolStripLabelMapSize.Name = "ToolStripLabelMapSize"
        Me.ToolStripLabelMapSize.Size = New System.Drawing.Size(53, 36)
        Me.ToolStripLabelMapSize.Text = "Map size"
        '
        'MapSizeSelector
        '
        Me.MapSizeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MapSizeSelector.Items.AddRange(New Object() {"Normal", "Quadruple", "Octuple"})
        Me.MapSizeSelector.Name = "MapSizeSelector"
        Me.MapSizeSelector.Size = New System.Drawing.Size(121, 39)
        Me.MapSizeSelector.ToolTipText = "Select map size when no map image is loaded."
        '
        'butDebugInvalidating
        '
        Me.butDebugInvalidating.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.butDebugInvalidating.Image = CType(resources.GetObject("butDebugInvalidating.Image"), System.Drawing.Image)
        Me.butDebugInvalidating.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.butDebugInvalidating.Name = "butDebugInvalidating"
        Me.butDebugInvalidating.Size = New System.Drawing.Size(36, 36)
        Me.butDebugInvalidating.Text = "butDebugInvalidating"
        Me.butDebugInvalidating.ToolTipText = "Picturebox mit Farbe füllen um invalidate rect zu sehen"
        '
        'panel1
        '
        Me.panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panel1.AutoScroll = True
        Me.panel1.Controls.Add(Me.PictureBox1)
        Me.panel1.Location = New System.Drawing.Point(0, 42)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(1060, 542)
        Me.panel1.TabIndex = 2
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(2048, 2048)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'TimerDragPicture
        '
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        '
        'butSelectAll
        '
        Me.butSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butSelectAll.Location = New System.Drawing.Point(1140, 289)
        Me.butSelectAll.Name = "butSelectAll"
        Me.butSelectAll.Size = New System.Drawing.Size(72, 23)
        Me.butSelectAll.TabIndex = 6
        Me.butSelectAll.Tag = "1"
        Me.butSelectAll.Text = "Select All"
        Me.ToolTip1.SetToolTip(Me.butSelectAll, "Select or unselect all Objects")
        Me.butSelectAll.UseVisualStyleBackColor = True
        '
        'WPNextBtn
        '
        Me.WPNextBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.WPNextBtn.Enabled = False
        Me.WPNextBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WPNextBtn.Image = Global.CourseDrawer.My.Resources.Resources.butNextWaypoint_
        Me.WPNextBtn.Location = New System.Drawing.Point(182, 3)
        Me.WPNextBtn.Name = "WPNextBtn"
        Me.WPNextBtn.Size = New System.Drawing.Size(36, 36)
        Me.WPNextBtn.TabIndex = 6
        Me.WPNextBtn.TabStop = False
        Me.ToolTip1.SetToolTip(Me.WPNextBtn, "Select next Waypoint")
        Me.WPNextBtn.UseVisualStyleBackColor = False
        '
        'WPPrevBtn
        '
        Me.WPPrevBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.WPPrevBtn.Enabled = False
        Me.WPPrevBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WPPrevBtn.Image = Global.CourseDrawer.My.Resources.Resources.butPrevWaypoint
        Me.WPPrevBtn.Location = New System.Drawing.Point(3, 3)
        Me.WPPrevBtn.Name = "WPPrevBtn"
        Me.WPPrevBtn.Size = New System.Drawing.Size(36, 36)
        Me.WPPrevBtn.TabIndex = 6
        Me.WPPrevBtn.TabStop = False
        Me.ToolTip1.SetToolTip(Me.WPPrevBtn, "Select previous Waypoint")
        Me.WPPrevBtn.UseVisualStyleBackColor = False
        '
        'WPIDMcbx
        '
        Me.WPIDMcbx.BeepOnError = True
        Me.WPIDMcbx.Culture = New System.Globalization.CultureInfo("")
        Me.WPIDMcbx.Enabled = False
        Me.WPIDMcbx.Location = New System.Drawing.Point(45, 19)
        Me.WPIDMcbx.Name = "WPIDMcbx"
        Me.WPIDMcbx.Size = New System.Drawing.Size(57, 20)
        Me.WPIDMcbx.TabIndex = 1
        Me.WPIDMcbx.Text = "0"
        Me.WPIDMcbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.WPIDMcbx.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.ToolTip1.SetToolTip(Me.WPIDMcbx, "Enter a Waypoint ID to Select")
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.WPNextBtn)
        Me.Panel2.Controls.Add(Me.WPPrevBtn)
        Me.Panel2.Controls.Add(Me.WPSpeedLbl)
        Me.Panel2.Controls.Add(Me.butCalcAngleSel)
        Me.Panel2.Controls.Add(Me.ChWP_Cross)
        Me.Panel2.Controls.Add(Me.ChWP_Wait)
        Me.Panel2.Controls.Add(Me.ChWP_Rev)
        Me.Panel2.Controls.Add(Me.WPCbxSeparator)
        Me.Panel2.Controls.Add(Me.WPTextSeparator)
        Me.Panel2.Controls.Add(Me.ChWP_TurnStart)
        Me.Panel2.Controls.Add(Me.ChWP_Unload)
        Me.Panel2.Controls.Add(Me.ChWP_TurnEnd)
        Me.Panel2.Controls.Add(Me.WPAngleLbl)
        Me.Panel2.Controls.Add(Me.WPPosYLbl)
        Me.Panel2.Controls.Add(Me.TBWP_Speed)
        Me.Panel2.Controls.Add(Me.TBWP_Angle)
        Me.Panel2.Controls.Add(Me.TBWP_Y)
        Me.Panel2.Controls.Add(Me.WPNumInfoLbl)
        Me.Panel2.Controls.Add(Me.WPNumberTextLbl)
        Me.Panel2.Controls.Add(Me.WPIDTextLbl)
        Me.Panel2.Controls.Add(Me.WPPosXLbl)
        Me.Panel2.Controls.Add(Me.WPIDMcbx)
        Me.Panel2.Controls.Add(Me.TBWP_X)
        Me.Panel2.Location = New System.Drawing.Point(1066, 403)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(221, 177)
        Me.Panel2.TabIndex = 4
        '
        'WPSpeedLbl
        '
        Me.WPSpeedLbl.Location = New System.Drawing.Point(111, 74)
        Me.WPSpeedLbl.Name = "WPSpeedLbl"
        Me.WPSpeedLbl.Size = New System.Drawing.Size(38, 13)
        Me.WPSpeedLbl.TabIndex = 5
        Me.WPSpeedLbl.Text = "Speed"
        '
        'butCalcAngleSel
        '
        Me.butCalcAngleSel.Location = New System.Drawing.Point(71, 142)
        Me.butCalcAngleSel.Name = "butCalcAngleSel"
        Me.butCalcAngleSel.Size = New System.Drawing.Size(75, 23)
        Me.butCalcAngleSel.TabIndex = 12
        Me.butCalcAngleSel.Text = "Calc angle"
        Me.butCalcAngleSel.UseVisualStyleBackColor = True
        '
        'ChWP_Cross
        '
        Me.ChWP_Cross.Enabled = False
        Me.ChWP_Cross.Location = New System.Drawing.Point(76, 119)
        Me.ChWP_Cross.Name = "ChWP_Cross"
        Me.ChWP_Cross.Size = New System.Drawing.Size(70, 17)
        Me.ChWP_Cross.TabIndex = 9
        Me.ChWP_Cross.Text = "Crossing"
        Me.ChWP_Cross.UseVisualStyleBackColor = True
        '
        'ChWP_Wait
        '
        Me.ChWP_Wait.Enabled = False
        Me.ChWP_Wait.Location = New System.Drawing.Point(76, 96)
        Me.ChWP_Wait.Name = "ChWP_Wait"
        Me.ChWP_Wait.Size = New System.Drawing.Size(70, 17)
        Me.ChWP_Wait.TabIndex = 8
        Me.ChWP_Wait.Text = "Wait"
        Me.ChWP_Wait.UseVisualStyleBackColor = True
        '
        'ChWP_Rev
        '
        Me.ChWP_Rev.Enabled = False
        Me.ChWP_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChWP_Rev.Location = New System.Drawing.Point(3, 96)
        Me.ChWP_Rev.Name = "ChWP_Rev"
        Me.ChWP_Rev.Size = New System.Drawing.Size(70, 17)
        Me.ChWP_Rev.TabIndex = 6
        Me.ChWP_Rev.Text = "Reverse"
        Me.ChWP_Rev.UseVisualStyleBackColor = True
        '
        'WPCbxSeparator
        '
        Me.WPCbxSeparator.Location = New System.Drawing.Point(106, 21)
        Me.WPCbxSeparator.Name = "WPCbxSeparator"
        Me.WPCbxSeparator.Size = New System.Drawing.Size(13, 16)
        Me.WPCbxSeparator.TabIndex = 2
        Me.WPCbxSeparator.Text = "/"
        Me.WPCbxSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'WPTextSeparator
        '
        Me.WPTextSeparator.Location = New System.Drawing.Point(106, 3)
        Me.WPTextSeparator.Name = "WPTextSeparator"
        Me.WPTextSeparator.Size = New System.Drawing.Size(13, 16)
        Me.WPTextSeparator.TabIndex = 2
        Me.WPTextSeparator.Text = "/"
        Me.WPTextSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChWP_TurnStart
        '
        Me.ChWP_TurnStart.Enabled = False
        Me.ChWP_TurnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChWP_TurnStart.Location = New System.Drawing.Point(148, 119)
        Me.ChWP_TurnStart.Name = "ChWP_TurnStart"
        Me.ChWP_TurnStart.Size = New System.Drawing.Size(70, 17)
        Me.ChWP_TurnStart.TabIndex = 11
        Me.ChWP_TurnStart.Text = "TurnStart"
        Me.ChWP_TurnStart.UseVisualStyleBackColor = True
        '
        'ChWP_Unload
        '
        Me.ChWP_Unload.Enabled = False
        Me.ChWP_Unload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChWP_Unload.Location = New System.Drawing.Point(3, 119)
        Me.ChWP_Unload.Name = "ChWP_Unload"
        Me.ChWP_Unload.Size = New System.Drawing.Size(70, 17)
        Me.ChWP_Unload.TabIndex = 7
        Me.ChWP_Unload.Text = "Unload"
        Me.ChWP_Unload.UseVisualStyleBackColor = True
        '
        'ChWP_TurnEnd
        '
        Me.ChWP_TurnEnd.Enabled = False
        Me.ChWP_TurnEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChWP_TurnEnd.Location = New System.Drawing.Point(148, 96)
        Me.ChWP_TurnEnd.Name = "ChWP_TurnEnd"
        Me.ChWP_TurnEnd.Size = New System.Drawing.Size(70, 17)
        Me.ChWP_TurnEnd.TabIndex = 10
        Me.ChWP_TurnEnd.Text = "TurnEnd"
        Me.ChWP_TurnEnd.UseVisualStyleBackColor = True
        '
        'WPAngleLbl
        '
        Me.WPAngleLbl.Location = New System.Drawing.Point(3, 74)
        Me.WPAngleLbl.Name = "WPAngleLbl"
        Me.WPAngleLbl.Size = New System.Drawing.Size(38, 13)
        Me.WPAngleLbl.TabIndex = 2
        Me.WPAngleLbl.Text = "Angle"
        '
        'WPPosYLbl
        '
        Me.WPPosYLbl.Location = New System.Drawing.Point(111, 49)
        Me.WPPosYLbl.Name = "WPPosYLbl"
        Me.WPPosYLbl.Size = New System.Drawing.Size(38, 13)
        Me.WPPosYLbl.TabIndex = 2
        Me.WPPosYLbl.Text = "Pos Y"
        '
        'TBWP_Speed
        '
        Me.TBWP_Speed.BeepOnError = True
        Me.TBWP_Speed.Culture = New System.Globalization.CultureInfo("")
        Me.TBWP_Speed.Enabled = False
        Me.TBWP_Speed.Location = New System.Drawing.Point(150, 71)
        Me.TBWP_Speed.Name = "TBWP_Speed"
        Me.TBWP_Speed.Size = New System.Drawing.Size(68, 20)
        Me.TBWP_Speed.TabIndex = 5
        Me.TBWP_Speed.Text = "0"
        '
        'TBWP_Angle
        '
        Me.TBWP_Angle.BeepOnError = True
        Me.TBWP_Angle.Culture = New System.Globalization.CultureInfo("")
        Me.TBWP_Angle.Enabled = False
        Me.TBWP_Angle.Location = New System.Drawing.Point(43, 69)
        Me.TBWP_Angle.Name = "TBWP_Angle"
        Me.TBWP_Angle.Size = New System.Drawing.Size(68, 20)
        Me.TBWP_Angle.TabIndex = 4
        Me.TBWP_Angle.Text = "0"
        '
        'TBWP_Y
        '
        Me.TBWP_Y.BeepOnError = True
        Me.TBWP_Y.Culture = New System.Globalization.CultureInfo("")
        Me.TBWP_Y.Enabled = False
        Me.TBWP_Y.Location = New System.Drawing.Point(150, 45)
        Me.TBWP_Y.Name = "TBWP_Y"
        Me.TBWP_Y.Size = New System.Drawing.Size(68, 20)
        Me.TBWP_Y.TabIndex = 3
        Me.TBWP_Y.Text = "0"
        '
        'WPNumInfoLbl
        '
        Me.WPNumInfoLbl.Location = New System.Drawing.Point(119, 19)
        Me.WPNumInfoLbl.Name = "WPNumInfoLbl"
        Me.WPNumInfoLbl.Size = New System.Drawing.Size(57, 20)
        Me.WPNumInfoLbl.TabIndex = 2
        Me.WPNumInfoLbl.Text = "0"
        Me.WPNumInfoLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WPNumberTextLbl
        '
        Me.WPNumberTextLbl.Location = New System.Drawing.Point(119, 3)
        Me.WPNumberTextLbl.Name = "WPNumberTextLbl"
        Me.WPNumberTextLbl.Size = New System.Drawing.Size(57, 16)
        Me.WPNumberTextLbl.TabIndex = 2
        Me.WPNumberTextLbl.Text = "Waypoints"
        Me.WPNumberTextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WPIDTextLbl
        '
        Me.WPIDTextLbl.Location = New System.Drawing.Point(45, 3)
        Me.WPIDTextLbl.Name = "WPIDTextLbl"
        Me.WPIDTextLbl.Size = New System.Drawing.Size(57, 16)
        Me.WPIDTextLbl.TabIndex = 2
        Me.WPIDTextLbl.Text = "ID"
        Me.WPIDTextLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'WPPosXLbl
        '
        Me.WPPosXLbl.Location = New System.Drawing.Point(3, 49)
        Me.WPPosXLbl.Name = "WPPosXLbl"
        Me.WPPosXLbl.Size = New System.Drawing.Size(38, 13)
        Me.WPPosXLbl.TabIndex = 2
        Me.WPPosXLbl.Text = "Pos X"
        '
        'TBWP_X
        '
        Me.TBWP_X.BeepOnError = True
        Me.TBWP_X.Culture = New System.Globalization.CultureInfo("")
        Me.TBWP_X.Enabled = False
        Me.TBWP_X.Location = New System.Drawing.Point(43, 45)
        Me.TBWP_X.Name = "TBWP_X"
        Me.TBWP_X.Size = New System.Drawing.Size(68, 20)
        Me.TBWP_X.TabIndex = 2
        Me.TBWP_X.Text = "0"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 583)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1299, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(75, 16)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(121, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.ComboBox1)
        Me.Panel3.Controls.Add(Me.TBCrs_Name)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.TBCrs_ID)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Location = New System.Drawing.Point(1067, 318)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(220, 79)
        Me.Panel3.TabIndex = 7
        '
        'ComboBox1
        '
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"*Root*"})
        Me.ComboBox1.Location = New System.Drawing.Point(40, 53)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(173, 21)
        Me.ComboBox1.TabIndex = 6
        '
        'TBCrs_Name
        '
        Me.TBCrs_Name.Location = New System.Drawing.Point(40, 29)
        Me.TBCrs_Name.Name = "TBCrs_Name"
        Me.TBCrs_Name.Size = New System.Drawing.Size(173, 20)
        Me.TBCrs_Name.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Folder"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(2, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Name"
        '
        'TBCrs_ID
        '
        Me.TBCrs_ID.Enabled = False
        Me.TBCrs_ID.Location = New System.Drawing.Point(40, 5)
        Me.TBCrs_ID.Name = "TBCrs_ID"
        Me.TBCrs_ID.Size = New System.Drawing.Size(173, 20)
        Me.TBCrs_ID.TabIndex = 1
        Me.TBCrs_ID.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(21, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "ID:"
        '
        'CrsList
        '
        Me.CrsList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrsList.Location = New System.Drawing.Point(1067, 44)
        Me.CrsList.Name = "CrsList"
        Me.CrsList.Size = New System.Drawing.Size(220, 239)
        Me.CrsList.TabIndex = 10
        '
        'ClsCourseBindingSource1
        '
        Me.ClsCourseBindingSource1.DataSource = GetType(CourseDrawer.clsCourse)
        '
        'ClsCourseBindingSource
        '
        Me.ClsCourseBindingSource.DataSource = GetType(CourseDrawer.clsCourse)
        '
        'mainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1299, 605)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.butSelectAll)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.CrsList)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "mainForm"
        Me.Text = "CourseDrawer"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.ClsCourseBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClsCourseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents butLoadCourse As System.Windows.Forms.ToolStripButton
    Friend WithEvents butLoadBGImage As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TimerDragPicture As System.Windows.Forms.Timer
    Friend WithEvents butZoom As System.Windows.Forms.ToolStripButton
    Friend WithEvents butMove As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents butAppendNode As System.Windows.Forms.ToolStripButton
    Friend WithEvents butInsertNode As System.Windows.Forms.ToolStripButton
    Friend WithEvents butDeleteNode As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents butSelect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TBWP_X As System.Windows.Forms.MaskedTextBox
    Friend WithEvents WPPosYLbl As System.Windows.Forms.Label
    Friend WithEvents TBWP_Y As System.Windows.Forms.MaskedTextBox
    Friend WithEvents WPPosXLbl As System.Windows.Forms.Label
    Friend WithEvents ChWP_Cross As System.Windows.Forms.CheckBox
    Friend WithEvents ChWP_Wait As System.Windows.Forms.CheckBox
    Friend WithEvents ChWP_Rev As System.Windows.Forms.CheckBox
    Friend WithEvents ChWP_TurnStart As System.Windows.Forms.CheckBox
    Friend WithEvents ChWP_TurnEnd As System.Windows.Forms.CheckBox
    Friend WithEvents WPAngleLbl As System.Windows.Forms.Label
    Friend WithEvents TBWP_Speed As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TBWP_Angle As System.Windows.Forms.MaskedTextBox
    Friend WithEvents butSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents butSelectAll As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents butNewCourse As System.Windows.Forms.ToolStripButton
    Friend WithEvents butDelCourse As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TBCrs_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TBCrs_Name As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents butCalcAngleSel As System.Windows.Forms.Button
    Friend WithEvents sButFillNodes As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents Distance5ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Distance10ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butRecalcAngleCrs As System.Windows.Forms.ToolStripButton
    Friend WithEvents Distance20ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents WPSpeedLbl As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub
    Friend WithEvents ClsCourseBindingSource As BindingSource
    Friend WithEvents ClsCourseBindingSource1 As BindingSource
    Friend WithEvents CrsList As crsListItems
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents MapSizeSelector As ToolStripComboBox
    Friend WithEvents ToolStripLabelMapSize As ToolStripLabel
    Friend WithEvents butDebugInvalidating As ToolStripButton
    Friend WithEvents ChWP_Unload As CheckBox
    Friend WithEvents WPNumInfoLbl As Label
    Friend WithEvents WPPrevBtn As Button
    Friend WithEvents WPNextBtn As Button
    Friend WithEvents WPNumberTextLbl As Label
    Friend WithEvents WPIDTextLbl As Label
    Friend WithEvents WPCbxSeparator As Label
    Friend WithEvents WPTextSeparator As Label
    Friend WithEvents WPIDMcbx As MaskedTextBox
End Class
