﻿Imports System.Data.SqlClient
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

    Dim rwcount As Integer
    Public DisplayDoffIndex As Integer
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    Private Sub frmSortJobDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ScreenReportCreate()

    End Sub


    Public Sub ScreenReportCreate()
        Dim tmptimenow As Date = Date.Now


        '******************************  ORIGINAL SCRIPT DO NOT DELETE ********************************************************************************
        'LExecQuery("Select POYMCNUM ,poymcname,poyprodname,POYmergenum , poyprodweight, POYDOFFNUM, poydrumstate, POYSORTENDTM FROM " _
        '  & "POYTRACK2 Where POYDRUMSTATE BETWEEN 1 and 14 And (POYSORTENDTM > DateAdd(Day, -7, '" & tmptimenow & "') AND POYSORTENDTM < '" & tmptimenow & "')  GROUP BY POYMCNUM,poymcname,poyprodname ,POYmergenum , poyprodweight , POYDOFFNUM, poydrumstate,POYSORTENDTM Order by POYSORTENDTM ")
        ''**************************************************************************************************************************************************




        LExecQuery("Select poymcnum ,poymcname,poyprodname,POYmergenum , poyprodweight, POYDOFFNUM, poydrumstate,poybcodejob,CAST(POYSORTENDTM As Date) As SortEnd  FROM POYTRACK2 " _
            & "Where POYDRUMSTATE BETWEEN 1 And 14 And " _
            & "(POYSORTENDTM >= DateAdd(Day, -7  ,'" & tmptimenow & "') And POYSORTENDTM < '" & tmptimenow & "') " _
            & "GROUP BY POYMCNUM, poymcname, poyprodname, POYBCODEJOB, poymergenum, poyprodweight, poydoffnum, poydrumstate, CAST(poySortEndTM As Date) " _
            & "Order by poymcnum,poydoffnum,SortEnd DESC ")


        '******************************  ORIGINAL SCRIPT DO NOT DELETE ********************************************************************************
        'LExecQuery("Select POYMCNUM , poymcname, poyprodname, poymergenum, poyprodweight, poydoffnum, poydrumstate, poySortEndTM FROM " _
        '  & "POYTRACK2 Where POYDRUMSTATE BETWEEN 1 And 14  And" _
        '  & "(POYSORTENDTM > DateAdd(Day, -7, '" & tmptimenow & "') AND POYSORTENDTM < '" & tmptimenow & "')" _
        '  & "GROUP BY POYMCNUM,poymcname,poyprodname ,POYmergenum , poyprodweight , POYDOFFNUM, poydrumstate, poysortendtm " _
        '  & "Order by poymcnum,poydoffnum,poysortendtm ")
        '**************************************************************************************************************************************************



        If LRecordCount > 0 Then
            DGVTmp.DataSource = LDS.Tables(0)
            DGVTmp.Rows(0).Selected = True

            rwcount = LRecordCount

            DGVDisplays.AllowUserToDeleteRows = True
            DGVDisplays.SelectAll()

            For i As Integer = DGVDisplays.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                DGVDisplays.Rows.RemoveAt(DGVDisplays.SelectedRows(i).Index)

            Next

            DGVDisplays.Refresh()

            DGVDisplays.DataSource = Nothing
            ' DGVDisplays.Rows.Clear()
            DGVDisplays.Rows.Add(rwcount)
            DGVDisplays.AllowUserToDeleteRows = False
            DGVDisplays.Controls(0).Enabled = True
            DGVDisplays.Controls(1).Enabled = True


            'Define temp variables
            Dim tmpACount As Integer
            Dim tmpAHold As Integer
            Dim tmpABCount As Integer
            Dim tmpShortCount As Integer
            Dim tmpShortABCount As Integer
            Dim tmpmissing As Integer
            Dim tmpCartCountSort As Integer
            Dim tmpcartcountPack As Integer
            Dim tmpCartCountHold As Integer
            Dim tmpStartTime As String
            Dim tmpEndTime As String
            Dim tmpMcNum As String
            Dim tmpProdName As String
            Dim tmpDOFFNum As String
            Dim tmpTFNum As String


            Try


                For i = 1 To rwcount


                    DGVDisplays.Rows(i - 1).Cells("poymccode").Value = DGVTmp.Rows(i - 1).Cells("POYMCNUM").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poymcnum").Value = DGVTmp.Rows(i - 1).Cells("POYMCNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodname").Value = DGVTmp.Rows(i - 1).Cells("POYPRODNAME").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poymergenum").Value = DGVTmp.Rows(i - 1).Cells("POYMERGENUM").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poyprodweight").Value = DGVTmp.Rows(i - 1).Cells("POYPRODWEIGHT").Value.ToString()
                    DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value = DGVTmp.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString()


                    'Set variables needed
                    tmpMcNum = DGVDisplays.Rows(i - 1).Cells("poymccode").Value
                    tmpProdName = DGVDisplays.Rows(i - 1).Cells("poyprodname").Value
                    tmpDOFFNum = DGVDisplays.Rows(i - 1).Cells("poydoffnum").Value
                    tmpTFNum = DGVDisplays.Rows(i - 1).Cells("poymergenum").Value



                    'GET MAIN JOB INFO
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE BETWEEN 1 and 14 And (POYSORTENDTM Is Not Null )  ")


                    DGVTmp2.DataSource = LDS.Tables(0)
                    DGVTmp2.Rows(0).Selected = True


                    'Set State colour
                    Dim tmpDrumState = DGVTmp.Rows(i - 1).Cells("POYDRUMSTATE").Value


                    tmpStartTime = DGVTmp2.Rows(0).Cells("POYSORTSTART").Value  '.ToString("yy-MM-dd hh:mm")
                    tmpEndTime = DGVTmp2.Rows(0).Cells("POYSORTENDTM").Value  '.ToString("yy-MM-dd hh:mm")

                    Select Case tmpDrumState
                        Case 1
                            'GET "A" COUNT
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null and POYSORTRELEASE is Null ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")

                            If LRecordCount > 0 Then tmpACount = LRecordCount

                            'GET ALL "SHORT" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                            If LRecordCount > 0 Then tmpShortCount = LRecordCount

                            'GET ALL MISSING DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null ) AND poymissdrum > 0 ")

                            If LRecordCount > 0 Then tmpmissing = LRecordCount

                            'GET ALL "SHORT" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                            If LRecordCount > 0 Then tmpShortCount = LRecordCount

                            'GET ALL "SHORTAB" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND    POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null ) AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                            If LRecordCount > 0 Then tmpShortABCount = LRecordCount


                            'GET ALL "Defect" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND    POYDRUMSTATE = 1 And (POYSORTENDTM Is Not Null ) AND  POYDEFDRUM > 0 And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) ")

                            If LRecordCount > 0 Then tmpABCount = LRecordCount

                            'GET "CARTSORT" COUNT
                            LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND POYDRUMSTATE  = 1 And (POYSORTENDTM Is Not Null and POYSORTRELEASE is NULL ) Group by poycartname ")
                            If LRecordCount > 0 Then tmpCartCountSort = LRecordCount


                            DGVDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Orange
                            DGVDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpCartCountSort
                            DGVDisplays.Rows(i - 1).Cells("poyGradeA").Value = tmpACount
                            DGVDisplays.Rows(i - 1).Cells("poyGradeAB").Value = tmpABCount
                            DGVDisplays.Rows(i - 1).Cells("gradeShort").Value = tmpShortCount
                            DGVDisplays.Rows(i - 1).Cells("gradeShortAB").Value = tmpShortABCount
                            DGVDisplays.Rows(i - 1).Cells("missing").Value = tmpmissing
                            DGVDisplays.Rows(i - 1).Cells("poyGradeA").Value = tmpACount
                            DGVDisplays.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                            DGVDisplays.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime
                        Case 2


                        Case 3
                            'GET "A" COUNT
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null and POYSORTRELEASE > 0 ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")

                            If LRecordCount > 0 Then tmpACount = LRecordCount

                            'GET ALL "SHORT" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                            If LRecordCount > 0 Then tmpShortCount = LRecordCount

                            'GET ALL MISSING DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null ) AND poymissdrum > 0 ")

                            If LRecordCount > 0 Then tmpmissing = LRecordCount

                            'GET ALL "SHORT" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                            If LRecordCount > 0 Then tmpShortCount = LRecordCount

                            'GET ALL "SHORTAB" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND    POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null ) AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                            If LRecordCount > 0 Then tmpShortABCount = LRecordCount


                            'GET ALL "Defect" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND    POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null ) AND  POYDEFDRUM > 0 And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) ")

                            If LRecordCount > 0 Then tmpABCount = LRecordCount

                            'GET "CARTPAK" COUNT
                            LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null and POYSORTRELEASE is NOT NULL ) Group by poycartname ")
                            If LRecordCount > 0 Then tmpcartcountPack = LRecordCount


                            DGVDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Green
                            DGVDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpcartcountPack
                            DGVDisplays.Rows(i - 1).Cells("poyGradeA").Value = tmpACount
                            DGVDisplays.Rows(i - 1).Cells("poyGradeAB").Value = tmpABCount
                            DGVDisplays.Rows(i - 1).Cells("gradeShort").Value = tmpShortCount
                            DGVDisplays.Rows(i - 1).Cells("gradeShortAB").Value = tmpShortABCount
                            DGVDisplays.Rows(i - 1).Cells("missing").Value = tmpmissing
                            DGVDisplays.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                            DGVDisplays.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime

                        Case 4
                            'GET "CARTHOLD" COUNT and if on hold find drum count
                            LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null) Group by poycartname ")
                            If LRecordCount > 0 Then tmpCartCountHold = LRecordCount

                            'COUNT NUMBER OF "A" DRUMS ON HOLD IN PRODUCT GROUP
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null and POYSORTRELEASE > 0 ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")
                            If LRecordCount > 0 Then tmpAHold = LRecordCount   'sets A count on hold

                            'GET ALL "SHORT" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                            If LRecordCount > 0 Then tmpShortCount = LRecordCount

                            'GET ALL MISSING DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null ) AND poymissdrum > 0 ")

                            If LRecordCount > 0 Then tmpmissing = LRecordCount

                            'GET ALL "SHORT" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                            If LRecordCount > 0 Then tmpShortCount = LRecordCount

                            'GET ALL "SHORTAB" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND    POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null ) AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                            If LRecordCount > 0 Then tmpShortABCount = LRecordCount


                            'GET ALL "Defect" DRUMS IN THIS DOFF
                            LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " AND    POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null ) AND  POYDEFDRUM > 0 And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) ")

                            If LRecordCount > 0 Then tmpABCount = LRecordCount



                            DGVDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Red
                            DGVDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpCartCountHold
                            DGVDisplays.Rows(i - 1).Cells("poyGradeA").Value = tmpAHold
                            DGVDisplays.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                            DGVDisplays.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime

                    End Select


                    'reset variables for next scan
                    tmpACount = 0
                    tmpAHold = 0
                    tmpABCount = 0
                    tmpShortCount = 0
                    tmpShortABCount = 0
                    tmpmissing = 0
                    tmpCartCountSort = 0
                    tmpcartcountPack = 0
                    tmpCartCountHold = 0
                    tmpStartTime = ""
                    tmpEndTime = ""
                    tmpMcNum = ""
                    tmpProdName = ""
                    tmpDOFFNum = ""
                    tmpTFNum = ""


                Next
                DGVDisplays.ClearSelection()



                tmrUpdateTimer.Interval = My.Settings.scrRefresh * 1000
                tmrUpdateTimer.Enabled = True
            Catch ex As Exception
                Me.Cursor = System.Windows.Forms.Cursors.Default
                MsgBox(ex.ToString)
            End Try

        Else

            MsgBox("No Data Found")
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default


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
        Me.Cursor = System.Windows.Forms.Cursors.Default
        tmrUpdateTimer.Enabled = False
        frmJobEntry.Show()
        Close()
    End Sub



    Private Sub tmrUpdateTimer_Tick(sender As Object, e As EventArgs) Handles tmrUpdateTimer.Tick


        If Not (frmSettings.IsHandleCreated) Then  'if you go to setting this will stop the timer
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            ScreenReportCreate()

        End If
    End Sub

    REM Keeps track of selection status
    Private selectionChanged As Boolean

    REM Fires Second
    Private Sub DGVDisplays_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDisplays.CellContentClick
        If Not selectionChanged Then
            DGVDisplays.ClearSelection()
            selectionChanged = True
        Else
            selectionChanged = False
        End If
    End Sub

    REM Fires first
    Private Sub DGVDisplays_SelectionChanged(sender As Object, e As EventArgs)
        selectionChanged = True
    End Sub

    Private Sub DGVDisplays_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDisplays.CellDoubleClick

        DisplayDoffIndex = DGVDisplays.CurrentCell.RowIndex

        frmJobDetail.Show()

    End Sub

    Private Sub btnRelease_Click(sender As Object, e As EventArgs) Handles btnRelease.Click
        Me.Hide()
        frmHoldRelMethod.Show()
    End Sub


End Class