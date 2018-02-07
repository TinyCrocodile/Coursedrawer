''' <summary>
''' Class for CP-Settings
''' </summary>
''' <remarks>Singleton class for Settings</remarks>
''' 

'ToDo: Settings implementation
Public Class clsSettings

    Public Property CPHposX As Double = 0.472291
    Public Property CPHposY As Double = 0.007407
    Public Property CPFautomaticScan As Boolean = True
    Public Property CPFonlyScanOwnedFields As Boolean = True
    Public Property CPFdebugScannedFields As Boolean = False
    Public Property CPFdebugCustomLoadedFields As Boolean = False
    Public Property CPFscanStep As Integer = 5
    Public Property CPWactive As Boolean = False
    Public Property CPWwagePerHour As Integer = 10
    Public Property CPIshowName As Boolean = True
    Public Property CPIshowCourse As Boolean = True
    Public Property CPIactive As Boolean = True
    Public Property CPMbatchWriteSize As Integer = 4096

    Private Shared _instance As clsSettings

    Private Sub New()

    End Sub
    ''' <summary>
    ''' Get instance of singleton class
    ''' </summary>
    ''' <param name="forceNew">force create new instance</param>
    ''' <returns>Instance of courses collection</returns>
    ''' <remarks></remarks>
    Public Shared Function getInstance(Optional ByVal forceNew As Boolean = False) As clsSettings
        If _instance Is Nothing Or forceNew = True Then
            _instance = New clsSettings
        End If
        Return _instance
    End Function
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadXML(ByVal file As String)
        Dim xmlDoc As New Xml.XmlDocument()
        Dim xmlNode As Xml.XmlNode
        Dim xmlNodeReader As Xml.XmlNodeReader
        If file = String.Empty Then Exit Sub
        xmlDoc.Load(file)
        If xmlDoc Is Nothing Then Exit Sub
        xmlNode = xmlDoc.DocumentElement


        If xmlNode Is Nothing Then Exit Sub
        xmlNodeReader = New Xml.XmlNodeReader(xmlNode)


        Do While (xmlNodeReader.Read())
            If xmlNodeReader.LocalName = "courseplayHud" Then
                While xmlNodeReader.MoveToNextAttribute
                    Select Case xmlNodeReader.LocalName
                        Case "posX"
                            Double.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, CPHposX)
                        Case "posY"
                            Double.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, CPHposY)
                    End Select
                End While

            ElseIf xmlNodeReader.LocalName = "courseplayFields" Then

                While xmlNodeReader.MoveToNextAttribute
                    Select Case xmlNodeReader.LocalName
                        Case "automaticScan"
                            If xmlNodeReader.Value = "true" Then
                                CPFautomaticScan = True
                            Else
                                CPFautomaticScan = False
                            End If
                        Case "onlyScanOwnedFields"
                            If xmlNodeReader.Value = "true" Then
                                CPFonlyScanOwnedFields = True
                            Else
                                CPFonlyScanOwnedFields = False
                            End If
                        Case "debugScannedFields"
                            If xmlNodeReader.Value = "true" Then
                                CPFdebugScannedFields = True
                            Else
                                CPFdebugScannedFields = False
                            End If
                        Case "debugCustomLoadedFields"
                            If xmlNodeReader.Value = "true" Then
                                CPFdebugCustomLoadedFields = True
                            Else
                                CPFdebugCustomLoadedFields = False
                            End If
                        Case "scanStep"
                            Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, CPFscanStep)
                    End Select
                End While
            ElseIf xmlNodeReader.LocalName = "courseplayWages" Then

                While xmlNodeReader.MoveToNextAttribute
                    Select Case xmlNodeReader.LocalName
                        Case "active"
                            If xmlNodeReader.Value = "true" Then
                                CPWactive = True
                            Else
                                CPWactive = False
                            End If
                        Case "wagePerHour"
                            Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, CPWwagePerHour)
                    End Select
                End While
            ElseIf xmlNodeReader.LocalName = "courseplayIngameMap" Then

                While xmlNodeReader.MoveToNextAttribute
                    Select Case xmlNodeReader.LocalName
                        Case "active"
                            If xmlNodeReader.Value = "true" Then
                                CPIactive = True
                            Else
                                CPIactive = False
                            End If
                        Case "showName"
                            If xmlNodeReader.Value = "true" Then
                                CPIshowName = True
                            Else
                                CPIshowName = False
                            End If
                        Case "showCourse"
                            If xmlNodeReader.Value = "true" Then
                                CPIshowCourse = True
                            Else
                                CPIshowCourse = False
                            End If
                    End Select
                End While
            ElseIf xmlNodeReader.LocalName = "courseManagement" Then

                While xmlNodeReader.MoveToNextAttribute
                    Select Case xmlNodeReader.LocalName
                        Case "batchWriteSize"
                            Integer.TryParse(xmlNodeReader.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, CPMbatchWriteSize)
                    End Select
                End While
            End If

            Loop

    End Sub


 


    ''' <summary>
    ''' Save folders to XML file
    ''' </summary>
    ''' <param name="root">byref xelement node</param>
    ''' <remarks></remarks>
    Public Sub SaveXML(ByRef root As XElement)

        Dim e1 As XElement
        e1 = New XElement("courseplayHud")


        e1.Add(New XAttribute("posX", CPHposX.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture)))
        e1.Add(New XAttribute("posY", CPHposY.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture)))
        root.Add(e1)

        e1 = New XElement("courseplayFields")
        e1.Add(New XAttribute("automaticScan", CPFautomaticScan))
        e1.Add(New XAttribute("debugCustomLoadedFields", CPFdebugCustomLoadedFields))
        e1.Add(New XAttribute("debugScannedFields", CPFdebugScannedFields))
        e1.Add(New XAttribute("onlyScanOwnedFields", CPFonlyScanOwnedFields))
        e1.Add(New XAttribute("scanStep", CPFscanStep))
        root.Add(e1)

        e1 = New XElement("courseplayWages")
        e1.Add(New XAttribute("active", CPWactive))
        e1.Add(New XAttribute("wagePerHour", CPWwagePerHour))
        root.Add(e1)

        e1 = New XElement("courseplayIngameMap")
        e1.Add(New XAttribute("active", CPIactive))
        e1.Add(New XAttribute("showCourse", CPIshowCourse))
        e1.Add(New XAttribute("showName", CPIshowName))
        root.Add(e1)

        e1 = New XElement("courseManagement")
        e1.Add(New XAttribute("batchWriteSize", CPMbatchWriteSize))
        root.Add(e1)



    End Sub
End Class