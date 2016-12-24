Public Class crsListItem
    Public Property CourseVisible As Boolean
        Get
            Return CheckBox_Visible.Checked
        End Get
        Set(value As Boolean)
            If (value <> CheckBox_Visible.Checked) Then
                CheckBox_Visible.Checked = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property CourseName As String
        Get
            Return Label_Checkbox.Text
        End Get
        Set(value As String)
            If (value <> Label_Checkbox.Text) Then
                Label_Checkbox.Text = value
                Me.Width = Label_Checkbox.Left + Label_Checkbox.Width
                Me.Height = Label_Checkbox.Height
                Me.Invalidate()
            End If
        End Set
    End Property

    Private m_ListIndex As Integer
    Public Property ListIndex As Integer
        Get
            Return m_ListIndex
        End Get
        Set(value As Integer)
            If (value <> m_ListIndex) Then
                m_ListIndex = value
            End If
        End Set
    End Property

    Private m_attachedObject As Object
    Public Property AttachedObject As Object
        Get
            Return m_attachedObject
        End Get
        Set(value As Object)
            m_attachedObject = value
        End Set
    End Property

    Private m_selected As Boolean

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        Me.m_ListIndex = -1
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Public Sub New(listIndex As Integer)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        Me.m_ListIndex = listIndex
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Public Event VisibleStatusChanged(ByRef crsItem As crsListItem, ByVal Visible As Boolean)

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Visible.CheckedChanged
        RaiseEvent VisibleStatusChanged(Me, CheckBox_Visible.Checked)
    End Sub

    Public Event ClickCheckbox(Course As crsListItem, Visible As Boolean, e As EventArgs)
    Private Sub CheckBox_Visible_Click(sender As Object, e As EventArgs) Handles CheckBox_Visible.Click
        RaiseEvent ClickCheckbox(Me, Me.CourseVisible, e)
    End Sub

    Public Event ClickItem(Course As crsListItem, e As EventArgs)
    Private Sub Label_Checkbox_Click(sender As Object, e As EventArgs) Handles Label_Checkbox.Click
        RaiseEvent ClickItem(Me, e)
    End Sub


    ''' <summary>
    ''' Eintrag selektieren
    ''' </summary>
    ''' <param name="Selection">True: Auswählen, False: Abwählen</param>
    Public Sub selectItem(Selection As Boolean)
        m_selected = Selection
        If Selection Then
            Me.BackColor = Color.LightBlue
        Else
            Me.BackColor = Color.Transparent
        End If
    End Sub

    Public Sub CheckItem(Visible As Boolean)
        CheckBox_Visible.Checked = Visible
    End Sub
End Class
