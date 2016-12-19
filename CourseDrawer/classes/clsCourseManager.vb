Public Class clsCourseManager
    Private Shared _Courses As List(Of clsCourse)
    Private _file As String
    Private Shared _instance As clsCourseManager

    Public Sub New(sCourseManagerXml As String)
        _file = sCourseManagerXml
        _Courses = New List(Of clsCourse)
        If sCourseManagerXml <> String.Empty Then
            CoursesReadXML(sCourseManagerXml)
        End If
    End Sub

    Public Shared Function getCourses() As List(Of clsCourse)
        Return _Courses
    End Function

    ''' <summary>
    ''' Get instance of singleton class
    ''' </summary>
    ''' <param name="forceNew">force create new instance</param>
    ''' <returns>Instance of courses collection</returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance(xmlFile As String, Optional ByVal forceNew As Boolean = False) As clsCourseManager
        If _instance Is Nothing Or forceNew = True Then
            _instance = New clsCourseManager(xmlFile)
        End If
        Return _instance
    End Function
    Public Sub CoursesReadXML(ByVal file As String)
        Dim Path As String
        Dim xmlDoc As New Xml.XmlDocument()
        Dim xmlNode As Xml.XmlNode
        Dim xmlNodeReader As Xml.XmlNodeReader
        Dim course As clsCourse
        If file = String.Empty Then Exit Sub
        Path = IO.Path.GetDirectoryName(file)
        xmlDoc.Load(file)
        If xmlDoc Is Nothing Then Exit Sub
        xmlNode = xmlDoc.DocumentElement.SelectSingleNode("saveSlot")
        If xmlNode Is Nothing Then Exit Sub
        xmlNodeReader = New Xml.XmlNodeReader(xmlNode)
        Do While (xmlNodeReader.Read())
            Select Case xmlNodeReader.NodeType
                Case Xml.XmlNodeType.Element
                    If xmlNodeReader.LocalName = "slot" Then
                        course = New clsCourse
                        While xmlNodeReader.MoveToNextAttribute
                            Select Case xmlNodeReader.LocalName
                                Case "fileName"
                                    course.fileName = Path + "\" + xmlNodeReader.Value
                                Case "name"
                                    course.Name = xmlNodeReader.Value
                                Case "id"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, course.id)
                                Case "parent"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, course.parent)
                                Case "isUsed"
                                    Boolean.TryParse(xmlNodeReader.Value, course.isUsed)
                            End Select
                        End While
                        If Not course Is Nothing Then
                            If course.isUsed Then
                                course.ReadCourseXML()
                                _Courses.Add(course)
                            End If
                        End If
                    End If
            End Select
        Loop
    End Sub
End Class
