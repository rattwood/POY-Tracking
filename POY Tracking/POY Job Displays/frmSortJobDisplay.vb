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

        LExecQuery("Select POYMCNUM ,poymcname,poyprodname,POYmergenum , poyprodweight, POYDOFFNUM FROM " _
                 & "POYTRACK2 Where POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) GROUP BY POYMCNUM,poymcname,poyprodname ,POYmergenum , poyprodweight , POYDOFFNUM Order by poymcnum,poydoffnum ")

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

                    DGVDisplays.Rows(i - 1).Cells("poymccode").Value = DGVTmp.Rows(i - 1).Cells("POYMCNUM").Value.ToString()
                    ' DGVDisplays.Rows(i - 1).Cells("poymcname").Value = DGVTmp.Rows(i - 1).Cells("POYMCNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodname").Value = DGVTmp.Rows(i - 1).Cells("POYPRODNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poymergenum").Value = DGVTmp.Rows(i - 1).Cells("POYMERGENUM").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodweight").Value = DGVTmp.Rows(i - 1).Cells("POYPRODWEIGHT").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value = DGVTmp.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString()


                    'Set variables needed
                    tmpMcNum = DGVDisplays.Rows(i - 1).Cells("poymccode").Value
                    tmpProdName = DGVDisplays.Rows(i - 1).Cells("poyprodname").Value
                    tmpDOFFNum = DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value
                    tmpTFNum = DGVDisplays.Rows(i - 1).Cells("poymergenum").Value



                    'GET
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL)  ")

                    DGVTmp2.DataSource = LDS.Tables(0)
                    DGVTmp2.Rows(rwcount).Selected = True



                    'GET ALL MISSING DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND poymissdrum > 0 ")

                    If LRecordCount > 0 Then tmpmissing = LRecordCount



                    'GET ALL "SHORT" DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                    If LRecordCount > 0 Then tmpShortCount = LRecordCount




                    'GET ALL "SHORTAB" DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                    If LRecordCount > 0 Then tmpShortABCount = LRecordCount


                    'GET ALL "Defect" DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND  POYDEFDRUM > 0 And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) ")

                    If LRecordCount > 0 Then tmpABCount = LRecordCount

                    'GET "CART" COUNT
                    LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) Group by poycartname ")
                    If LRecordCount > 0 Then tmpCartCount = LRecordCount

                    'GET "A" COUNT
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")

                    If LRecordCount > 0 Then tmpACount = LRecordCount



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

                tmrUpdateTimer.Interval = My.Settings.scrRefresh * 1000

            Catch ex As Exception

            End Try

        Else
            MsgBox("No Data Found")
        End If
    End Sub

    Private Sub screenReportUpdate()
        LExecQuery("Select POYMCNUM ,poyprodname,POYmergenum , poyprodweight, POYDOFFNUM FROM " _
                & "POYTRACK2 Where POYDRUMSTATE = 1 And POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL GROUP BY POYMCNUM ,poyprodname ,POYmergenum , poyprodweight , POYDOFFNUM Order by poymcnum,poydoffnum ")

        ' DGVTmp.DataSource = Nothing


        If LRecordCount > 0 Then
            DGVTmp.DataSource = LDS.Tables(0)
            DGVTmp.Rows(0).Selected = True

            Dim rwcount = LRecordCount 'DGVTmp.Rows.Count(1

            DGVDisplays.AllowUserToDeleteRows = True
            DGVDisplays.SelectAll()

            lblMessage.Visible = True
            For i As Integer = DGVDisplays.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                DGVDisplays.Rows.RemoveAt(DGVDisplays.SelectedRows(i).Index)

            Next

            DGVDisplays.Refresh()

            DGVDisplays.DataSource = Nothing
            ' DGVDisplays.Rows.Clear()
            DGVDisplays.Rows.Add(rwcount)
            DGVDisplays.AllowUserToDeleteRows = False

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
            Dim tmpMCName As String
            Dim tmpProdName As String
            Dim tmpDOFFNum As String
            Dim tmpTFNum As String


            Try


                For i = 1 To rwcount

                    ' DGVDisplays.Rows(i - 1).Cells("poystate").Value = DGVTmp.Rows(i - 1).Cells("POYS").Value.ToString()   'GET STATE



                    DGVDisplays.Rows(i - 1).Cells("poymccode").Value = DGVTmp.Rows(i - 1).Cells("POYMCNUM").Value.ToString()
                    '   DGVDisplays.Rows(i - 1).Cells("poymcname").Value = DGVTmp.Rows(i - 1).Cells("POYMCNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodname").Value = DGVTmp.Rows(i - 1).Cells("POYPRODNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poymergenum").Value = DGVTmp.Rows(i - 1).Cells("POYMERGENUM").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodweight").Value = DGVTmp.Rows(i - 1).Cells("POYPRODWEIGHT").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value = DGVTmp.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString()


                    'Set variables needed
                    tmpMcNum = DGVDisplays.Rows(i - 1).Cells("poymccode").Value
                    tmpProdName = DGVDisplays.Rows(i - 1).Cells("poyprodname").Value
                    tmpDOFFNum = DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value
                    tmpTFNum = DGVDisplays.Rows(i - 1).Cells("poymergenum").Value



                    'GET
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) ")

                    DGVTmp2.DataSource = LDS.Tables(0)
                    DGVTmp2.Rows(rwcount).Selected = True



                    'GET ALL MISSING DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND poymissdrum > 0 ")

                    If LRecordCount > 0 Then tmpmissing = LRecordCount



                    'GET ALL SHORT DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                    If LRecordCount > 0 Then tmpShortCount = LRecordCount





                    'GET ALL SHORTAB DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                    If LRecordCount > 0 Then tmpShortABCount = LRecordCount



                    'GET ALL Defect DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND  POYDEFDRUM > 0 And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) ")

                    If LRecordCount > 0 Then tmpABCount = LRecordCount


                    'GET CART COUNT
                    LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) Group by poycartname ")
                    If LRecordCount > 0 Then tmpCartCount = LRecordCount

                    'GET "A" COUNT
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null And  POYSORTRELEASE Is NULL) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")

                    If LRecordCount > 0 Then tmpACount = LRecordCount




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
                lblMessage.Visible = False

            Catch ex As Exception
                lblMessage.Visible = False
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



    Private Sub tmrUpdateTimer_Tick(sender As Object, e As EventArgs) Handles tmrUpdateTimer.Tick
        'used to flash the log in lamp

        If Not (frmSettings.IsHandleCreated) Then  'if you go to setting this will stop the timer

            screenReportUpdate()
        End If




    End Sub
End Class