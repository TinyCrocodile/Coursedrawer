''' <summary>
''' Class for collection of folders
''' </summary>
''' <remarks>Singleton class for wrapping folders</remarks>
Public Class clsFolders
    Private Shared _folders As List(Of clsFolder)
    Private _seledtedFrs As Integer
    Dim Hidden As Boolean

    Public ReadOnly Property Count As Integer
        Get
            Return _folders.Count
        End Get
    End Property

    Private Shared _instance As clsFolders
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadXML(ByVal file As String)
        Dim xmlDoc As New Xml.XmlDocument()
        Dim xmlNode As Xml.XmlNode
        Dim xmlNodeReader As Xml.XmlNodeReader
        Dim folder As New clsFolder
        If file = String.Empty Then Exit Sub
        xmlDoc.Load(file)
        If xmlDoc Is Nothing Then Exit Sub
        xmlNode = xmlDoc.DocumentElement.SelectSingleNode("folders")
        If xmlNode Is Nothing Then Exit Sub
        xmlNodeReader = New Xml.XmlNodeReader(xmlNode)
        Do While (xmlNodeReader.Read())
            Select Case xmlNodeReader.NodeType
                Case Xml.XmlNodeType.Element
                    If xmlNodeReader.LocalName = "folder" Then
                        folder = New clsFolder
                        _folders.Add(folder)
                        While xmlNodeReader.MoveToNextAttribute
                            Select Case xmlNodeReader.LocalName
                                Case "name"
                                    folder.Name = xmlNodeReader.Value
                                Case "id"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, folder.id)
                                Case "parent"
                                    Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, folder.parent)
                            End Select
                        End While
                    End If
            End Select
        Loop
    End Sub
    Public Sub New()
        _folders = New List(Of clsFolder)
    End Sub
    ''' <summary>
    ''' Get instance of singleton class
    ''' </summary>
    ''' <param name="forceNew">force create new instance</param>
    ''' <returns>Instance of courses collection</returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance(Optional ByVal forceNew As Boolean = False) As clsFolders
        If _instance Is Nothing Or forceNew = True Then
            _instance = New clsFolders
        End If
        Return _instance
    End Function
    ''' <summary>
    ''' Get list of course name and hidden attribute
    ''' </summary>
    ''' <value></value>
    ''' <returns>Dictionary of course names and hidden attribute</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FolderList As Dictionary(Of String, Boolean)
        Get
            Dim dir As New Dictionary(Of String, Boolean)
            Dim index As Integer = 1
            For Each frs As clsFolder In _folders
                dir.Add(frs.parent.ToString & " : " & frs.Name, Not Hidden)
                frs.listIndex = index
                index += 1
            Next
            Return dir
        End Get
    End Property

    Public Function getXML() As XElement
        Dim e1 As New XElement("folder")
        Dim idx As Integer = 1
        'e1.Add(New XAttribute("name", Me.Name))
        'e1.Add(New XAttribute("id", Me.id.ToString))
        'e1.Add(New XAttribute("parent", Me.parent.ToString))
        'For Each folder As clsFolder In _folders
        ' e1.Add(folder.getXML())
        ' Next
        Return e1
    End Function


    ''' <summary>
    ''' Save folders to XML file
    ''' </summary>
    ''' <param name="root">byref xelement node</param>
    ''' <remarks></remarks>
    Public Sub SaveXML(ByRef root As XElement)

        Dim folders As XElement

        folders = New XElement("folders")
        For Each folder In _folders
            'folders.Add(folder.getXML)
        Next
        If _folders.Count > 0 Then
            root.Add(folders)
        End If

    End Sub
    Public Shared Function parentListIndex(Parent As Integer, Folders As List(Of clsFolder)) As Integer
        If Folders Is Nothing Then Return 0
        For Each folder In Folders
            If folder.id = Parent Then
                Return folder.listIndex
            End If
        Next
        Return 0
    End Function

    Public Shared Function getFolders() As List(Of clsFolder)
        Return _folders
    End Function
End Class