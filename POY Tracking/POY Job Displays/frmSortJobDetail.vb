﻿Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms


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
    Public dgvCreated As Integer = 0

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

            If dgvCreated = 0 Then
                SetUpDGV()
                DGVNewDoff.Rows.Add(localRowCount)
                dgvCreated = 1
            Else
                DGVNewDoff.AllowUserToDeleteRows = True
                DGVNewDoff.SelectAll()

                lblMessage.Visible = True
                For i As Integer = DGVNewDoff.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                    DGVNewDoff.Rows.RemoveAt(DGVNewDoff.SelectedRows(i).Index)

                Next

                DGVNewDoff.Refresh()

                DGVNewDoff.DataSource = Nothing
                DGVNewDoff.Rows.Add(localRowCount)
                DGVNewDoff.AllowUserToDeleteRows = False

            End If



            DGVDoffTmp1.DataSource = LDS.Tables(0)
            DGVDoffTmp1.Rows(0).Selected = True




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

                        DGVNewDoff.Rows(i - 1).Cells("poymccode").Value = tmpMCCode
                        DGVNewDoff.Rows(i - 1).Cells("poymcnum").Value = tmpMCNUM
                        DGVNewDoff.Rows(i - 1).Cells("poyprodname").Value = tmpProdName
                        DGVNewDoff.Rows(i - 1).Cells("poymergenum").Value = tmpTFNum
                        ' DGVMcDoffInfo.Rows(i - 1).Cells("poyprodweight").Value = tmpProdWeight
                        DGVNewDoff.Rows(i - 1).Cells("poydoffnum").Value = tmpDOFFNum
                        DGVNewDoff.Rows(i - 1).Cells("poycartnum").Value = tmpCartName






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



                        DGVNewDoff.Rows(i - 1).Cells("poyGradeA").Value = tmpACount
                        DGVNewDoff.Rows(i - 1).Cells("poyGradeAB").Value = tmpABCount
                        DGVNewDoff.Rows(i - 1).Cells("gradeShort").Value = tmpShortCount
                        DGVNewDoff.Rows(i - 1).Cells("gradeShortAB").Value = tmpShortABCount
                        DGVNewDoff.Rows(i - 1).Cells("missing").Value = tmpmissing

                        'DGVNewDoff.Rows(i - 1).Cells("poySortStartTM").Value = tmpStartTime
                        DGVNewDoff.Rows(i - 1).Cells("poySortEndTM").Value = tmpEndTime

                        ''Set State colour
                        Dim tmpDrumState = DGVDoffTmp1.Rows(i - 1).Cells("POYDRUMSTATE").Value

                        Select Case tmpDrumState
                            Case 1
                                DGVNewDoff.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Orange
                            Case 2


                            Case 3
                                DGVNewDoff.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Green

                            Case 4
                                DGVNewDoff.Rows(i - 1).Cells("poystate").Style.BackColor = Color.Red

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
                    DGVNewDoff.ClearSelection()
                    DGVNewDoff.SelectionMode = DataGridViewSelectionMode.FullRowSelect


                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

            Else

                MsgBox("No Data Found")
        End If




    End Sub

    Private Sub SetUpDGV()

        Try

            'With DGVNewDoff.ColumnHeadersDefaultCellStyle
            '    .BackColor = Color.Navy
            '    .ForeColor = Color.White
            '    .Font = New Font(DGVNewDoff.Font, FontStyle.Bold)
            'End With

            '.AutoSizeRowsMode =
            '    DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
            '    .ColumnHeadersBorderStyle =
            '    DataGridViewHeaderBorderStyle.Single
            '    .CellBorderStyle = DataGridViewCellBorderStyle.Single
            '    .GridColor = Color.Black
            '    .RowHeadersVisible = False



            Dim STATEColumn As New DataGridViewColumn
            Dim MCCodeColumn As New DataGridViewColumn
            Dim MCNoColumn As New DataGridViewColumn
            Dim CartNoColumn As New DataGridViewColumn
            Dim ProdKindColumn As New DataGridViewColumn
            Dim TFColumn As New DataGridViewColumn
            Dim DoffColumn As New DataGridViewColumn
            Dim AColumn As New DataGridViewColumn
            Dim ABColumn As New DataGridViewColumn
            Dim ShortColumn As New DataGridViewColumn
            Dim ShortABColumn As New DataGridViewColumn
            Dim MissColumn As New DataGridViewColumn
            Dim SortEndTMColumn As New DataGridViewColumn

            If My.Settings.chkUseSort Then
                'Setting the Properties for the STATEColumn
                STATEColumn.Name = "poystate"
                STATEColumn.ValueType = GetType(Color)
                STATEColumn.HeaderText = "STATE"
                STATEColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the MCCodeColumn
                MCCodeColumn.Name = "poymccode"
                MCCodeColumn.ValueType = GetType(String)
                MCCodeColumn.HeaderText = "MC Code"
                MCCodeColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the MCNoColumn
                MCNoColumn.Name = "poymcnum"
                MCNoColumn.ValueType = GetType(String)
                MCNoColumn.HeaderText = "MC No."
                MCNoColumn.CellTemplate = New DataGridViewTextBoxCell



                'Setting the Properties for the CartNoColumn 
                CartNoColumn.Name = "poycartnum"
                CartNoColumn.ValueType = GetType(String)
                CartNoColumn.HeaderText = "Cart No."
                CartNoColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ProdKindColumn
                ProdKindColumn.Name = "poyprodname"
                ProdKindColumn.ValueType = GetType(String)
                ProdKindColumn.HeaderText = "Product Kind"
                ProdKindColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the TFColumn
                TFColumn.Name = "poymergenum"
                TFColumn.ValueType = GetType(String)
                TFColumn.HeaderText = "TF"
                TFColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the  DoffColumn 
                DoffColumn.Name = "poydoffnum"
                DoffColumn.ValueType = GetType(String)
                DoffColumn.HeaderText = "Doff"
                DoffColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the  AColumn
                AColumn.Name = "poygradeA"
                AColumn.ValueType = GetType(String)
                AColumn.HeaderText = "Amount"
                AColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ABColumn
                ABColumn.Name = "poygradeAB"
                ABColumn.ValueType = GetType(String)
                ABColumn.HeaderText = "AB"
                ABColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ShortColumn
                ShortColumn.Name = "gradeshort"
                ShortColumn.ValueType = GetType(String)
                ShortColumn.HeaderText = "Short"
                ShortColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ShortABColumn
                ShortABColumn.Name = "gradeshortAB"
                ShortABColumn.ValueType = GetType(String)
                ShortABColumn.HeaderText = "Short AB"
                ShortABColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the MissColumn
                MissColumn.Name = "missing"
                MissColumn.ValueType = GetType(String)
                MissColumn.HeaderText = "Miss"
                MissColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the SortEndTMColumn
                SortEndTMColumn.Name = "poysortendtm"
                SortEndTMColumn.ValueType = GetType(String)
                SortEndTMColumn.HeaderText = "Sort End Time"
                SortEndTMColumn.CellTemplate = New DataGridViewTextBoxCell


                With DGVNewDoff
                    'Adding the Column to the DataGridView
                    .Columns.Add(STATEColumn)
                    .Columns.Add(MCCodeColumn)
                    .Columns.Add(MCNoColumn)
                    .Columns.Add(CartNoColumn)
                    .Columns.Add(ProdKindColumn)
                    .Columns.Add(TFColumn)
                    .Columns.Add(DoffColumn)
                    .Columns.Add(AColumn)
                    .Columns.Add(ABColumn)
                    .Columns.Add(ShortColumn)
                    .Columns.Add(ShortABColumn)
                    .Columns.Add(MissColumn)
                    .Columns.Add(SortEndTMColumn)

                    'Making the DataGridView ReadOnly since we don't want the user to edit the grid at the moment
                    .ReadOnly = True
                    .MultiSelect = True
                    'restricting user capabilities on the DataGridView
                    .AllowUserToAddRows = False
                    .AllowUserToDeleteRows = False
                    .RowHeadersVisible = False
                    .GridColor = Color.Black
                    '.Dock = DockStyle.Fill
                End With

            Else

                'Setting the Properties for the STATEColumn
                STATEColumn.Name = "poystate"
                STATEColumn.ValueType = GetType(Color)
                STATEColumn.HeaderText = "STATE"
                STATEColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the MCCodeColumn
                MCCodeColumn.Name = "poymccode"
                MCCodeColumn.ValueType = GetType(String)
                MCCodeColumn.HeaderText = "MC Code"
                MCCodeColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the MCNoColumn
                MCNoColumn.Name = "poymcnum"
                MCNoColumn.ValueType = GetType(String)
                MCNoColumn.HeaderText = "MC No."
                MCNoColumn.CellTemplate = New DataGridViewTextBoxCell



                'Setting the Properties for the CartNoColumn 
                CartNoColumn.Name = "poycartnum"
                CartNoColumn.ValueType = GetType(String)
                CartNoColumn.HeaderText = "Cart No."
                CartNoColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ProdKindColumn
                ProdKindColumn.Name = "poyprodname"
                ProdKindColumn.ValueType = GetType(String)
                ProdKindColumn.HeaderText = "Product Kind"
                ProdKindColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the TFColumn
                TFColumn.Name = "poymergenum"
                TFColumn.ValueType = GetType(String)
                TFColumn.HeaderText = "TF"
                TFColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the  DoffColumn 
                DoffColumn.Name = "poydoffnum"
                DoffColumn.ValueType = GetType(String)
                DoffColumn.HeaderText = "Doff"
                DoffColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the  AColumn
                AColumn.Name = "poygradeA"
                AColumn.ValueType = GetType(String)
                AColumn.HeaderText = "A"
                AColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ABColumn
                'ABColumn.Name = "poygradeAB"
                'ABColumn.ValueType = GetType(String)
                'ABColumn.HeaderText = "AB"
                'ABColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ShortColumn
                'ShortColumn.Name = "gradeshort"
                'ShortColumn.ValueType = GetType(String)
                'ShortColumn.HeaderText = "Short"
                'ShortColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the ShortABColumn
                'ShortABColumn.Name = "gradeshortAB"
                'ShortABColumn.ValueType = GetType(String)
                'ShortABColumn.HeaderText = "Short AB"
                'ShortABColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the MissColumn
                'MissColumn.Name = "missing"
                'MissColumn.ValueType = GetType(String)
                'MissColumn.HeaderText = "Miss"
                'MissColumn.CellTemplate = New DataGridViewTextBoxCell


                'Setting the Properties for the SortEndTMColumn
                SortEndTMColumn.Name = "poysortendtm"
                SortEndTMColumn.ValueType = GetType(String)
                SortEndTMColumn.HeaderText = "Sort End Time"
                SortEndTMColumn.CellTemplate = New DataGridViewTextBoxCell


                With DGVNewDoff
                    'Adding the Column to the DataGridView
                    .Columns.Add(STATEColumn)
                    .Columns.Add(MCCodeColumn)
                    .Columns.Add(MCNoColumn)
                    .Columns.Add(CartNoColumn)
                    .Columns.Add(ProdKindColumn)
                    .Columns.Add(TFColumn)
                    .Columns.Add(DoffColumn)
                    .Columns.Add(AColumn)
                    .Columns.Add(ABColumn)
                    .Columns.Add(ShortColumn)
                    .Columns.Add(ShortABColumn)
                    .Columns.Add(MissColumn)
                    .Columns.Add(SortEndTMColumn)

                    'Making the DataGridView ReadOnly since we don't want the user to edit the grid at the moment
                    .ReadOnly = True
                    .MultiSelect = True
                    'restricting user capabilities on the DataGridView
                    .AllowUserToAddRows = False
                    .AllowUserToDeleteRows = False
                    .RowHeadersVisible = False
                    .GridColor = Color.Black
                    '.Dock = DockStyle.Fill
                End With

            End If










        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

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
    Private Sub DGVMcDoffInfo_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVNewDoff.CellContentClick
        If Not selectionChanged Then
            DGVNewDoff.ClearSelection()
            selectionChanged = True
        Else
            selectionChanged = False
        End If
    End Sub

    REM Fires first
    Private Sub DGVMcDoffInfo_SelectionChanged(sender As Object, e As EventArgs)
        selectionChanged = True
    End Sub

    Private Sub DGVMcDoffInfo_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVNewDoff.CellDoubleClick

        DisplayDoffIndex = DGVNewDoff.CurrentCell.RowIndex

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
        Dim selectedCount As Integer = DGVNewDoff.Rows.GetRowCount(DataGridViewElementStates.Selected)    'COUNT NUMBER OF SELECTED ROWS
        Dim poycartbcode As String
        Dim poyDrumState As String
        timeUpdate()

        If selectedCount > 0 Then


            For i = 1 To selectedCount

                selectIndex = DGVNewDoff.SelectedRows(i - 1).Index.ToString



                poycartbcode = DGVDoffTmp1.Rows(selectIndex).Cells("poybcodecart").Value
                poyDrumState = DGVDoffTmp1.Rows(selectIndex).Cells("poydrumstate").Value

                Select Case poyDrumState
                    Case 1  'check to see if this is a fresh release to packing

                        LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                          & "poybcodecart = '" & poycartbcode & "' ")

                    Case 4  'Check if on hold if it is then change state bback to Packing

                        LExecQuery("update poytrack2 Set POYHOLDRELEASETM = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                          & "poybcodecart = '" & poycartbcode & "' ")

                End Select

                DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION
            Next
        Else

            MsgBox("You must select carts before RELEASE")


        End If

        mcDoffDisplay()  'REFRESH DISPLAY
        DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION











    End Sub

    Private Sub btnHold_Click(sender As Object, e As EventArgs) Handles btnHold.Click

        Dim selectIndex As String
        'Find all selected rows and get info for SQL Update to DB
        Dim selectedCount As Integer = DGVNewDoff.Rows.GetRowCount(DataGridViewElementStates.Selected)    'COUNT NUMBER OF SELECTED ROWS
        Dim poycartbcode As String
        Dim poyDrumState As String
        timeUpdate()

        If selectedCount > 0 Then


            For i = 1 To selectedCount

                selectIndex = DGVNewDoff.SelectedRows(i - 1).Index.ToString



                poycartbcode = DGVDoffTmp1.Rows(selectIndex).Cells("poybcodecart").Value
                poyDrumState = DGVDoffTmp1.Rows(selectIndex).Cells("poydrumstate").Value



                Select Case poyDrumState

                    Case 2   'check for release from sorting to packing
                        LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                           & "poybcodecart = '" & poycartbcode & "' ")

                    Case 3
                        LExecQuery("update poytrack2 Set POYHOLDSTARTTM = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '4' Where " _
                          & "poybcodecart = '" & poycartbcode & "' ")




                        'LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                        '  & "poybcodecart = '" & poycartbcode & "' ")

                        'DGVMcDoffInfo.ClearSelection()  'CLEAR ONSCREEN SELECTION
                End Select
            Next
        Else

            MsgBox("You must select carts before RELEASE")


        End If

        mcDoffDisplay()  'REFRESH DISPLAY
        DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION


    End Sub
End Class