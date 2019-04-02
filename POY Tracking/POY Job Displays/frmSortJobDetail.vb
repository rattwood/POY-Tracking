Imports System.Data.SqlClient
Imports System.ComponentModel



Public Class frmSortJobDetail

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
    Dim DisplayDoffIndex As Integer
    Dim localRowCount As Integer


    'TIME
    Dim time As New DateTime
    Dim dateFormat As String = "yyyy-MM-dd HH:mm:ss"
    Public todayTimeDate As String

    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    Private Sub frmSortJobDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mcDoffDisplay()
    End Sub

    Public Sub timeUpdate()   'get current time and date

        todayTimeDate = time.Now.ToString(dateFormat)

    End Sub



    Private Sub mcDoffDisplay()


        Dim localrowindx = frmSortJobDisplay.DisplayDoffIndex

        Dim tmpMCCode = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMCCODE").Value.ToString()
        Dim tmpMCNUM = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMCNUM").Value.ToString()
        Dim tmpProdName = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYPRODNAME").Value.ToString()
        Dim tmpDOFFNum = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYDOFFNUM").Value.ToString()
        Dim tmpTFNum = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMERGENUM").Value.ToString()
        Dim tmpProdWeight = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYPRODWEIGHT").Value.ToString()





        LExecQuery("Select distinct poycartname,poybcodecart,poydrumstate FROM  POYTRACK2 Where POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) and poymcname = '" & tmpMCNUM & "'" _
                    & " and poyprodname = '" & tmpProdName & "' and poymergenum = '" & tmpTFNum & "' and poydoffnum = '" & tmpDOFFNum & "' " _
                    & "order by poycartname  ")





        If LRecordCount > 0 Then

            'CREATE THE ROWN ON DGV 
            localRowCount = LRecordCount

            DGVMcDoffInfo.AllowUserToDeleteRows = True
            DGVMcDoffInfo.SelectAll()

            ' lblMessage.Visible = True
            For i As Integer = DGVMcDoffInfo.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                DGVMcDoffInfo.Rows.RemoveAt(DGVMcDoffInfo.SelectedRows(i).Index)

            Next

            DGVMcDoffInfo.Refresh()

            DGVMcDoffInfo.DataSource = Nothing
            DGVMcDoffInfo.Rows.Add(localRowCount)
            DGVMcDoffInfo.AllowUserToDeleteRows = False





            ' DGVMcDoffInfo.Rows.Add(localRowCount)

            DGVDoffTmp1.DataSource = LDS.Tables(0)
            DGVDoffTmp1.Rows(0).Selected = True

            ' DGVMcDoffInfo.Visible = True







            'Define temp variables
            Dim tmpACount As Integer
            Dim tmpABCount As Integer
            Dim tmpShortCount As Integer
            Dim tmpShortABCount As Integer
            Dim tmpmissing As Integer
            Dim tmpCartCount As Integer
            Dim tmpStartTime As String
            Dim tmpEndTime As String





            Try


                For i = 1 To localRowCount

                    Dim tmpCartName = DGVDoffTmp1.Rows(i - 1).Cells("POYCARTNAME").Value.ToString()

                    DGVMcDoffInfo.Rows(i - 1).Cells("poymccode").Value = tmpMCCode
                    DGVMcDoffInfo.Rows(i - 1).Cells("poymcnum").Value = tmpMCNUM
                    DGVMcDoffInfo.Rows(i - 1).Cells("poyprodname").Value = tmpProdName
                    DGVMcDoffInfo.Rows(i - 1).Cells("poymergenum").Value = tmpTFNum
                    ' DGVMcDoffInfo.Rows(i - 1).Cells("poyprodweight").Value = tmpProdWeight
                    DGVMcDoffInfo.Rows(i - 1).Cells("poydoffnum").Value = tmpDOFFNum
                    DGVMcDoffInfo.Rows(i - 1).Cells("poycartnum").Value = tmpCartName






                    'GET ALL MISSING DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMCCode & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & "and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) AND poymissdrum > 0 ")

                    If LRecordCount > 0 Then tmpmissing = LRecordCount



                    'GET ALL "SHORT" DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMCCode & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) AND POYSHORTDRUM > 0 and (POYDEFDRUM = 0 Or POYDEFDRUM is Null) ")

                    If LRecordCount > 0 Then tmpShortCount = LRecordCount




                    'GET ALL "SHORTAB" DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMCCode & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & " and poycartname = '" & tmpCartName & "' AND    POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null) AND (POYSHORTDRUM > 0 and POYDEFDRUM >  0) ")

                    If LRecordCount > 0 Then tmpShortABCount = LRecordCount


                    'GET ALL "Defect" DRUMS IN THIS DOFF
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMCCode & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & "and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) AND  POYDEFDRUM > 0 And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) ")

                    If LRecordCount > 0 Then tmpABCount = LRecordCount


                    'GET "A" COUNT
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNUM = '" & tmpMCCode & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & "and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")


                    If LRecordCount > 0 Then tmpACount = LRecordCount



                    'GET "ENDTIME

                    LExecQuery("Select poysortendtm,poysortstart FROM POYTRACK2 Where POYMCNUM = '" & tmpMCCode & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                   & "and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null )  ")
                    If LRecordCount > 0 Then tmpCartCount = LRecordCount


                    If LRecordCount > 0 Then
                        DGVDoffTmp2.DataSource = LDS.Tables(0)
                        DGVDoffTmp2.Rows(0).Selected = True
                    End If



                    tmpStartTime = DGVDoffTmp2.Rows(0).Cells("POYSORTSTART").Value   '.ToString("yy-MM-dd hh:mm")
                    tmpEndTime = DGVDoffTmp2.Rows(0).Cells("POYSORTENDTM").Value   '.ToString("yy-MM-dd hh:mm")



                    DGVMcDoffInfo.Rows(i - 1).Cells("poyGradeA").Value = tmpACount
                    DGVMcDoffInfo.Rows(i - 1).Cells("poyGradeAB").Value = tmpABCount
                    DGVMcDoffInfo.Rows(i - 1).Cells("gradeShort").Value = tmpShortCount
                    DGVMcDoffInfo.Rows(i - 1).Cells("gradeShortAB").Value = tmpShortABCount
                    DGVMcDoffInfo.Rows(i - 1).Cells("missing").Value = tmpmissing

                    ' DGVMcDoffInfo.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                    DGVMcDoffInfo.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime

                    'Set State colour
                    Dim tmpDrumState = DGVDoffTmp1.Rows(i - 1).Cells("POYDRUMSTATE").Value

                    Select Case tmpDrumState
                        Case 1
                            DGVMcDoffInfo.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Orange
                        Case 2


                        Case 3
                            DGVMcDoffInfo.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Green

                        Case 4


                    End Select






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
                DGVMcDoffInfo.ClearSelection()
                DGVMcDoffInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect



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







    REM Keeps track of selection status
    Private selectionChanged As Boolean

    REM Fires Second
    Private Sub DGVMcDoffInfo_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVMcDoffInfo.CellContentClick
        If Not selectionChanged Then
            DGVMcDoffInfo.ClearSelection()
            selectionChanged = True
        Else
            selectionChanged = False
        End If
    End Sub

    REM Fires first
    Private Sub DGVMcDoffInfo_SelectionChanged(sender As Object, e As EventArgs)
        selectionChanged = True
    End Sub

    Private Sub DGVMcDoffInfo_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVMcDoffInfo.CellDoubleClick

        DisplayDoffIndex = DGVMcDoffInfo.CurrentCell.RowIndex

        mcDoffDisplay()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmSortJobDisplay.DGVDisplays.ClearSelection()
        frmSortJobDisplay.DGVDisplays.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        frmSortJobDisplay.Show()
        frmSortJobDisplay.ScreenReportCreate()
        Close()
    End Sub

    Private Sub btnReleaseJob_Click(sender As Object, e As EventArgs) Handles btnReleaseJob.Click

        Dim selectIndex As String
        'Find all selected rows and get info for SQL Update to DB
        Dim selectedCount As Integer = DGVMcDoffInfo.Rows.GetRowCount(DataGridViewElementStates.Selected)    'COUNT NUMBER OF SELECTED ROWS
        Dim poycartbcode As String
        timeUpdate()

        If selectedCount > 0 Then


            For i = 1 To selectedCount

                selectIndex = DGVMcDoffInfo.SelectedRows(i - 1).Index.ToString



                poycartbcode = DGVDoffTmp1.Rows(selectIndex).Cells("poybcodecart").Value

                LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                          & "poybcodecart = '" & poycartbcode & "' ")

                DGVMcDoffInfo.ClearSelection()  'CLEAR ONSCREEN SELECTION

            Next
        Else

            MsgBox("You must select carts before RELEASE")


        End If

        mcDoffDisplay()  'REFRESH DISPLAY
        DGVMcDoffInfo.ClearSelection()  'CLEAR ONSCREEN SELECTION











    End Sub
End Class