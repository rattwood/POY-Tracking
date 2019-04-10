Imports System.Data.SqlClient
Imports System.ComponentModel



Public Class frmPackDisplay

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




    Private Sub frmPackJobDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ScreenReportCreate()

    End Sub


    Public Sub ScreenReportCreate()


        '******************************  ORIGINAL SCRIPT DO NOT DELETE ********************************************************************************
        LExecQuery("Select POYMCNUM ,poymcname,poyprodname,POYmergenum , poyprodweight, POYDOFFNUM, poydrumstate FROM " _
              & "POYTRACK2 Where POYDRUMSTATE BETWEEN 3 and 5 And (POYSORTENDTM Is Not Null )  GROUP BY POYMCNUM,poymcname,poyprodname ,POYmergenum , poyprodweight , POYDOFFNUM, poydrumstate Order by poymcnum,poydoffnum ")
        '**************************************************************************************************************************************************




        If LRecordCount > 0 Then
                DGVTmp.DataSource = LDS.Tables(0)
                DGVTmp.Rows(0).Selected = True

                rwcount = LRecordCount

            DGVPackDisplays.AllowUserToDeleteRows = True
            DGVPackDisplays.SelectAll()

            For i As Integer = DGVPackDisplays.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                DGVPackDisplays.Rows.RemoveAt(DGVPackDisplays.SelectedRows(i).Index)

            Next

            DGVPackDisplays.Refresh()

            DGVPackDisplays.DataSource = Nothing
            ' DGVPackDisplays.Rows.Clear()
            DGVPackDisplays.Rows.Add(rwcount)
            DGVPackDisplays.AllowUserToDeleteRows = False




            'Define temp variables
            Dim tmpACount As Integer
            Dim tmpAHold As Integer
            Dim tmpCartCountHold As Integer
            Dim tmpcartcountPack As Integer
            Dim tmpStartTime As String
            Dim tmpEndTime As String
            Dim tmpMcNum As String
            Dim tmpProdName As String
            Dim tmpDOFFNum As String
            Dim tmpTFNum As String
            Dim tmpCartHoldTM As String

            Try
                    lblMessage.Visible = True

                    For i = 1 To rwcount


                    DGVPackDisplays.Rows(i - 1).Cells("poymcnum").Value = DGVTmp.Rows(i - 1).Cells("POYMCNAME").Value.ToString()
                    DGVPackDisplays.Rows(i - 1).Cells("poyprodname").Value = DGVTmp.Rows(i - 1).Cells("POYPRODNAME").Value.ToString()
                    DGVPackDisplays.Rows(i - 1).Cells("poymergenum").Value = DGVTmp.Rows(i - 1).Cells("POYMERGENUM").Value.ToString()
                    DGVPackDisplays.Rows(i - 1).Cells("poyprodweight").Value = DGVTmp.Rows(i - 1).Cells("POYPRODWEIGHT").Value.ToString()
                    DGVPackDisplays.Rows(i - 1).Cells("poydoffnum").Value = DGVTmp.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString()


                    'Set variables needed
                    tmpMcNum = DGVTmp.Rows(i - 1).Cells("POYMCNUM").Value.ToString()
                    tmpProdName = DGVPackDisplays.Rows(i - 1).Cells("poyprodname").Value
                    tmpDOFFNum = DGVPackDisplays.Rows(i - 1).Cells("poydoffnum").Value
                    tmpTFNum = DGVPackDisplays.Rows(i - 1).Cells("poymergenum").Value



                    'GET MAIN JOB INFO
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & " AND   POYDRUMSTATE BETWEEN 3 and 5 And (POYSORTENDTM Is Not Null )  ")

                    DGVTmp2.DataSource = LDS.Tables(0)
                    DGVTmp2.Rows(rwcount).Selected = True





                    'GET "CARTHOLD" COUNT and if on hold find drum count
                    LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null and POYHoldStartTm is not null ) Group by poycartname ")
                    If LRecordCount > 0 Then
                        tmpCartCountHold = LRecordCount

                        'GET "A" COUNT on Hold
                        LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                           & " AND   POYDRUMSTATE = 4 And (POYSORTENDTM Is Not Null and POYSORTRELEASE is not null ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")

                        If LRecordCount > 0 Then tmpAHold = LRecordCount

                    End If




                    'GET "CARTPAK" COUNT
                    LExecQuery("Select poycartname FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null and POYSORTRELEASE is NOT NULL ) Group by poycartname ")
                    If LRecordCount > 0 Then tmpcartcountPack = LRecordCount



                    'GET "A" COUNT
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMcNum & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & " AND   POYDRUMSTATE = 3 And (POYSORTENDTM Is Not Null and POYSORTRELEASE is not null ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")

                    If LRecordCount > 0 Then tmpACount = LRecordCount



                    tmpCartHoldTM = DGVTmp2.Rows(i - 1).Cells("POYHOLDSTARTTM").Value.ToString '.ToString("yy-MM-dd hh:mm")
                    'tmpEndTime = DGVTmp2.Rows(i - 1).Cells("POYSORTENDTM").Value   '.ToString("yy-MM-dd hh:mm")



                    ' DGVPackDisplays.Rows(i - 1).Cells("drumCount").Value = tmpACount
                    DGVPackDisplays.Rows(i - 1).Cells("PALLET48").Value = Int(tmpACount / 48)
                    DGVPackDisplays.Rows(i - 1).Cells("PALLET72").Value = Int(tmpACount / 72)
                    DGVPackDisplays.Rows(i - 1).Cells("PALLET120").Value = Int(tmpACount / 120)
                    'DGVPackDisplays.Rows(i - 1).Cells("missing").Value = tmpmissing
                    ' DGVPackDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpCartCountSort
                    ' DGVPackDisplays.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                    ' DGVPackDisplays.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime

                    'Set State colour
                    Dim tmpDrumState = DGVTmp.Rows(i - 1).Cells("POYDRUMSTATE").Value

                        Select Case tmpDrumState
                            Case 1

                        Case 2


                            Case 3
                            DGVPackDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Green
                            DGVPackDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpcartcountPack
                            DGVPackDisplays.Rows(i - 1).Cells("drumCount").Value = tmpACount

                        Case 4
                            DGVPackDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Red
                            DGVPackDisplays.Rows(i - 1).Cells("poycartcount").Value = tmpCartCountHold
                            DGVPackDisplays.Rows(i - 1).Cells("holdstarttm").Value = tmpCartHoldTM
                            DGVPackDisplays.Rows(i - 1).Cells("drumCount").Value = tmpAHold
                        Case 5


                    End Select



                        'Set State colour
                        'DGVDisplays.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Orange


                        'reset variables for next scan
                        tmpcartcountPack = 0
                    tmpCartCountHold = 0
                    tmpACount = 0
                    'tmpABCount = 0
                    'tmpShortCount = 0
                    'tmpShortABCount = 0
                    'tmpmissing = 0

                    tmpStartTime = ""
                        tmpEndTime = ""
                    Next
                DGVPackDisplays.ClearSelection()
                lblMessage.Visible = False


                TimerRefresh.Interval = My.Settings.scrRefresh * 1000
                TimerRefresh.Enabled = True
            Catch ex As Exception

                End Try

            Else
                lblMessage.Visible = False
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
        TimerRefresh.Enabled = False
        frmJobEntry.Show()
            Close()
        End Sub



    Private Sub TimerRefresh_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        'used to flash the log in lamp

        If Not (frmSettings.IsHandleCreated) Then  'if you go to setting this will stop the timer
            ScreenReportCreate()

        End If
    End Sub

    'REM Keeps track of selection status
    Private selectionChanged As Boolean

    'REM Fires Second
    Private Sub DGVPackDisplays_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPackDisplays.CellContentClick
        If Not selectionChanged Then
            DGVPackDisplays.ClearSelection()
            selectionChanged = True
        Else
            selectionChanged = False
        End If
    End Sub

    'REM Fires first
    Private Sub DGVPackDisplays_SelectionChanged(sender As Object, e As EventArgs)
        selectionChanged = True
    End Sub

    Private Sub DGVPackDisplays_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPackDisplays.CellDoubleClick

        DisplayDoffIndex = DGVPackDisplays.CurrentCell.RowIndex

        'frmSortJobDetail.Show()

    End Sub



End Class

