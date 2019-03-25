Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class frmSortJobDisplay
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    'Private SQL As New SQLConn
    Private writeerrorLog As New writeError

    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    ' Public LConn As New SqlConnection("Server=192.168.1.211,1433;Database=Toraydb;User ID=sa;Password=tecknose4260")

    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Public LDA As SqlDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SqlCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SqlParameter)
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    Private Sub frmSortJobDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ScreenReportCreate()

    End Sub


    Private Sub ScreenReportCreate()

        LExecQuery("Select POYMCNUM ,poyprodname,POYmergenum , poyprodweight, POYDOFFNUM FROM " _
                 & "POYTRACK2 Where POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL GROUP BY POYMCNUM ,poyprodname ,POYmergenum , poyprodweight , POYDOFFNUM Order by poymcnum,poydoffnum ")

        If LRecordCount > 0 Then
            DGVTmp.DataSource = LDS.Tables(0)
            DGVTmp.Rows(0).Selected = True

            Dim rwcount = LRecordCount 'DGVTmp.Rows.Count()


            DGVDisplays.Rows.Add(rwcount)


            'Define temp variables
            Dim tmpACount As Integer
            Dim tmpABCount As Integer
            Dim tmpShortCount As Integer
            Dim tmpShortABCount As Integer
            Dim tmpmissing As Integer
            Dim tmpCartCount As Integer
            Dim tmpStartTime As String
            Dim tmpEndTime As String
            Dim tmpMcNum As String
            Dim tmpProdName As String
            Dim tmpDOFFNum As String
            Dim tmpTFNum As String


            Try


                For i = 1 To rwcount

                    ' DGVDisplays.Rows(i - 1).Cells("poystate").Value = DGVTmp.Rows(i - 1).Cells("POYS").Value.ToString()   'GET STATE

                    DGVDisplays.Rows(i - 1).Cells("poymcnum").Value = DGVTmp.Rows(i - 1).Cells("POYMCNUM").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodname").Value = DGVTmp.Rows(i - 1).Cells("POYPRODNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poymergenum").Value = DGVTmp.Rows(i - 1).Cells("POYMERGENUM").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodweight").Value = DGVTmp.Rows(i - 1).Cells("POYPRODWEIGHT").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value = DGVTmp.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString()


                    'Set variables needed
                    tmpMcNum = DGVDisplays.Rows(i - 1).Cells("poymcnum").Value
                    tmpProdName = DGVDisplays.Rows(i - 1).Cells("poyprodname").Value
                    tmpDOFFNum = DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value
                    tmpTFNum = DGVDisplays.Rows(i - 1).Cells("poymergenum").Value



                    'GET
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL Order by poymcnum ")

                        DGVTmp2.DataSource = LDS.Tables(0)
                    DGVTmp2.Rows(rwcount).Selected = True



                    'GET ALL MISSING DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL AND poymissdrum > 0 ")

                    If LRecordCount > 0 Then tmpmissing = LRecordCount

                    'If Not IsDBNull(DGVTmp2.Rows(x - 1).Cells("POYMISSDRUM").Value) Then
                    '    If DGVTmp2.Rows(x - 1).Cells("POYMISSDRUM").Value > 0 Then tmpmissing += tmpmissing
                    'End If


                    'GET ALL SHORT DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL AND POYSHORTDRUM > 0 and (POYDEFDRUM < 1 Or POYDEFDRUM is Null) ")

                    If LRecordCount > 0 Then tmpShortCount = LRecordCount

                    ' If DGVTmp2.Rows(x - 1).Cells("POYSHORTDRUM").Value > 0 And DGVTmp2.Rows(x - 1).Cells("POYDEFDRUM").Value = 0 Then tmpShortCount += tmpShortCount



                    'GET ALL SHORTAB DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                    If LRecordCount > 0 Then tmpShortABCount = LRecordCount
                    'If DGVTmp2.Rows(x - 1).Cells("POYSHORTDRUM").Value > 0 And DGVTmp2.Rows(x - 1).Cells("POYDEFDRUM").Value > 0 Then tmpShortABCount += tmpShortABCount


                    'GET ALL Defect DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL AND  POYDEFDRUM > 1 And (POYSHORTDRUM < 1 Or POYSHORTDRUM is Null) ")

                    If LRecordCount > 0 Then tmpABCount = LRecordCount
                    ' If DGVTmp2.Rows(x - 1).Cells("POYDEFDRUM").Value > 0 And DGVTmp2.Rows(x - 1).Cells("POYSHORTDRUM").Value = 0 Then tmpABCount += tmpABCount

                    'GET CART COUNT
                    LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL Group by poycartname ")
                    If LRecordCount > 0 Then tmpCartCount = LRecordCount / 2




                    tmpACount = (DGVTmp2.Rows.Count - 1) - (tmpShortCount + tmpShortABCount + tmpABCount)

                    tmpStartTime = DGVTmp2.Rows(i - 1).Cells("POYSORTSTART").Value   '.ToString("yy-MM-dd hh:mm")
                    tmpEndTime = DGVTmp2.Rows(i - 1).Cells("POYSORTENDTM").Value   '.ToString("yy-MM-dd hh:mm")



                    DGVDisplays.Rows(i - 1).Cells("poyGradeA").Value = tmpACount
                    DGVDisplays.Rows(i - 1).Cells("poyGradeAB").Value = tmpABCount
                    DGVDisplays.Rows(i - 1).Cells("gradeShort").Value = tmpShortCount
                    DGVDisplays.Rows(i - 1).Cells("gradeShortAB").Value = tmpShortABCount
                    DGVDisplays.Rows(i - 1).Cells("missing").Value = tmpmissing
                    DGVDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpCartCount
                    DGVDisplays.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                    DGVDisplays.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime

                    'Set State colour
                    DGVDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Orange






                    'reset variables for next scan
                    tmpACount = 0
                    tmpABCount = 0
                    tmpShortCount = 0
                    tmpShortABCount = 0
                    tmpmissing = 0
                    tmpCartCount = 0
                    tmpStartTime = ""
                    tmpEndTime = ""
                Next


            Catch ex As Exception

            End Try

        Else
            MsgBox("No Data Found")
        End If
    End Sub


    '--------------------------------------------- START SQL DATBASE ROUTINES  -----------------------------------------------------------
    Public Sub LExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""


        If LConn.State = ConnectionState.Open Then LConn.Close()
        Try

            'OPEN SQL DATABSE CONNECTION
            LConn.Open()

            'CREATE SQL COMMAND
            LCmd = New SqlCommand(Query, LConn)

            'LOAD PARAMETER INTO SQL COMMAND
            LParams.ForEach(Sub(p) LCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            LParams.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            LDS = New DataSet
            LDT = New DataTable
            LDA = New SqlDataAdapter(LCmd)

            LRecordCount = LDA.Fill(LDS)

        Catch ex As Exception

            LException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(LException)
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub
    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub

    ' ERROR CHECKING
    Public Function HasException(Optional Report As Boolean = False) As Boolean
        If String.IsNullOrEmpty(LException) Then Return False
        If Report = True Then MsgBox(LException, MsgBoxStyle.Critical, "Exception:")
        Return True
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.Show()
        Close()
    End Sub
    '--------------------------------------------- END SQL DATBASE ROUTINES  -----------------------------------------------------------

End Class