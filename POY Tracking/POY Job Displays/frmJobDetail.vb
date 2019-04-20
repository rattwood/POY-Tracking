Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms


Public Class frmJobDetail

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
    Dim LocalDisplayDoffIndex As Integer
    Dim localRowCount As Integer
    Public dgvCreated As Integer = 0
    Dim tmpCartNum As String

    'TIME
    Dim time As New DateTime
    Dim dateFormat As String = "yyyy-MM-dd HH:mm:ss"
    Public todayTimeDate As String


    'DATA FROM REFERENCE PAGE
    Dim localrowindx As Integer
    Dim tmpMCNUM
    Dim tmpProdName
    Dim tmpDOFFNum
    Dim tmpTFNum
    Dim tmpProdWeight
    Dim tmpMCCode
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    Private Sub frmSortJobDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.chkUseSort Then

            localrowindx = frmSortJobDisplay.DisplayDoffIndex

            tmpMCCode = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMCCODE").Value.ToString()
            tmpMCNUM = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMCNUM").Value.ToString()
            tmpProdName = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYPRODNAME").Value.ToString()
            tmpDOFFNum = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYDOFFNUM").Value.ToString()
            tmpTFNum = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMERGENUM").Value.ToString()
            tmpProdWeight = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYPRODWEIGHT").Value.ToString()
            mcSortDoffDisplay()
        Else
            'Load values on first landing
            localrowindx = frmPackDisplay.DisplayDoffIndex

            tmpMCNUM = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYMCNUM").Value.ToString()
            tmpProdName = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYPRODNAME").Value.ToString()
            tmpDOFFNum = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYDOFFNUM").Value.ToString()
            tmpTFNum = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYMERGENUM").Value.ToString()
            tmpProdWeight = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYPRODWEIGHT").Value.ToString()
            mcPackDoffDisplay()
        End If







    End Sub

    Public Sub timeUpdate()   'get current time and date

        ' todayTimeDate = time.Now.ToString(dateFormat)
        todayTimeDate = DateTime.Now.ToString(New System.Globalization.CultureInfo("en-us"))
    End Sub



    Private Sub mcSortDoffDisplay()


        'Dim localrowindx = frmSortJobDisplay.DisplayDoffIndex

        'Dim tmpMCCode = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMCCODE").Value.ToString()
        'Dim tmpMCNUM = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMCNUM").Value.ToString()
        'Dim tmpProdName = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYPRODNAME").Value.ToString()
        'Dim tmpDOFFNum = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYDOFFNUM").Value.ToString()
        'Dim tmpTFNum = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYMERGENUM").Value.ToString()
        'Dim tmpProdWeight = frmSortJobDisplay.DGVDisplays.Rows(localrowindx).Cells("POYPRODWEIGHT").Value.ToString()





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
                DGVNewDoff.MultiSelect = True
                DGVNewDoff.AllowUserToDeleteRows = True
                DGVNewDoff.SelectAll()

                ' lblMessage.Visible = True
                For i As Integer = DGVNewDoff.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                    DGVNewDoff.Rows.RemoveAt(DGVNewDoff.SelectedRows(i).Index)

                Next

                DGVNewDoff.Refresh()
                DGVNewDoff.MultiSelect = False
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
                    DGVNewDoff.Rows(i - 1).Cells("poygradeAB").Value = tmpABCount
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


        End If





    End Sub


    Private Sub mcPackDoffDisplay()

        ' frmPackDisplay.ScreenReportCreate()


        'Dim localrowindx = frmPackDisplay.DisplayDoffIndex

        ''  Dim tmpMCCode = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYMCCODE").Value.ToString()
        'Dim tmpMCNUM = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYMCNUM").Value.ToString()
        '    Dim tmpProdName = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYPRODNAME").Value.ToString()
        '    Dim tmpDOFFNum = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYDOFFNUM").Value.ToString()
        '    Dim tmpTFNum = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYMERGENUM").Value.ToString()
        '    Dim tmpProdWeight = frmPackDisplay.DGVPackDisplays.Rows(localrowindx).Cells("POYPRODWEIGHT").Value.ToString()



        LExecQuery("Select distinct poycartname,poybcodecart,poydrumstate FROM  POYTRACK2 Where POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) and poymcname = '" & tmpMCNUM & "'" _
                    & " and poyprodname = '" & tmpProdName & "' and poymergenum = '" & tmpTFNum & "' and poydoffnum = '" & tmpDOFFNum & "' " _
                    & "order by poycartname  ")





        'Try

        If LRecordCount > 0 Then

                'CREATE THE ROWN ON DGV 
                localRowCount = LRecordCount

                Try
                    If dgvCreated = 0 Then
                        SetUpDGV()
                        DGVNewDoff.Rows.Add(localRowCount)
                        dgvCreated = 1
                    ElseIf dgvCreated > 0 Then
                        DGVNewDoff.MultiSelect = True
                        DGVNewDoff.AllowUserToDeleteRows = True
                        DGVNewDoff.SelectAll()


                        For i As Integer = DGVNewDoff.SelectedRows.Count - 1 To 0 Step -1                'DGVDisplays.Rows.RemoveAt(i - 1)
                            DGVNewDoff.Rows.RemoveAt(DGVNewDoff.SelectedRows(i).Index)
                        Next

                        DGVNewDoff.Refresh()
                        DGVNewDoff.MultiSelect = False
                        DGVNewDoff.DataSource = Nothing
                        DGVNewDoff.Rows.Add(localRowCount)
                        DGVNewDoff.AllowUserToDeleteRows = False

                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try


                DGVDoffTmp1.DataSource = LDS.Tables(0)
                DGVDoffTmp1.Rows(0).Selected = False




                'Define temp variables
                Dim tmpACount As Integer
                Dim tmpCartCount As Integer
                Dim tmpStartTime As String
                Dim tmpEndTime As String








                For i = 1 To localRowCount

                    Dim tmpCartName = DGVDoffTmp1.Rows(i - 1).Cells("POYCARTNAME").Value.ToString()


                    DGVNewDoff.Rows(i - 1).Cells("poymcnum").Value = tmpMCNUM
                    DGVNewDoff.Rows(i - 1).Cells("poyprodname").Value = tmpProdName
                    DGVNewDoff.Rows(i - 1).Cells("poymergenum").Value = tmpTFNum
                    ' DGVMcDoffInfo.Rows(i - 1).Cells("poyprodweight").Value = tmpProdWeight
                    DGVNewDoff.Rows(i - 1).Cells("poydoffnum").Value = tmpDOFFNum
                    DGVNewDoff.Rows(i - 1).Cells("poycartnum").Value = tmpCartName





                    'GET "A" COUNT
                    LExecQuery("Select * FROM POYTRACK2 Where POYMCNAME = '" & tmpMCNUM & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & "and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) ")


                    If LRecordCount > 0 Then tmpACount = LRecordCount



                    'GET "ENDTIME

                    LExecQuery("Select poysortendtm,poysortstart FROM POYTRACK2 Where POYMCNAME = '" & tmpMCNUM & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and POYDOFFNUM = '" & tmpDOFFNum & "'     " _
                                       & "and poycartname = '" & tmpCartName & "' AND   POYDRUMSTATE Between 1 and 14 And (POYSORTENDTM Is Not Null )  ")
                    If LRecordCount > 0 Then tmpCartCount = LRecordCount


                    If LRecordCount > 0 Then
                        DGVDoffTmp2.DataSource = LDS.Tables(0)
                        DGVDoffTmp2.Rows(0).Selected = True
                    End If



                    tmpStartTime = DGVDoffTmp2.Rows(0).Cells("POYSORTSTART").Value   '.ToString("yy-MM-dd hh:mm")
                    tmpEndTime = DGVDoffTmp2.Rows(0).Cells("POYSORTENDTM").Value   '.ToString("yy-MM-dd hh:mm")



                    DGVNewDoff.Rows(i - 1).Cells("poyGradeA").Value = tmpACount

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
                    tmpCartCount = 0
                    tmpStartTime = ""
                    tmpEndTime = ""
                Next
                'Clear up Selects
                DGVNewDoff.ClearSelection()
                DGVNewDoff.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                listBoxUpdate()



            End If

        ''Catch ex As Exception
        '  MsgBox(ex.ToString)
        ' End Try



    End Sub

    Private Sub SetUpDGV()

        Try




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
                AColumn.HeaderText = "A"
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
                    .MultiSelect = False
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
                    '.Columns.Add(ABColumn)
                    '.Columns.Add(ShortColumn)
                    '.Columns.Add(ShortABColumn)
                    '.Columns.Add(MissColumn)
                    .Columns.Add(SortEndTMColumn)

                    'Making the DataGridView ReadOnly since we don't want the user to edit the grid at the moment
                    .ReadOnly = True
                    .MultiSelect = False
                    'restricting user capabilities on the DataGridView
                    .AllowUserToAddRows = False
                    .AllowUserToDeleteRows = False
                    .RowHeadersVisible = False
                    .GridColor = Color.Black
                    .Columns("poymccode").Visible = False

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

        LocalDisplayDoffIndex = DGVNewDoff.CurrentCell.RowIndex

        If My.Settings.chkUseSort Then
            mcSortDoffDisplay()
        Else
            mcPackDoffDisplay()
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        If My.Settings.chkUseSort Then
            frmSortJobDisplay.DGVDisplays.ClearSelection()
            frmSortJobDisplay.DGVDisplays.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            frmSortJobDisplay.Show()
            frmSortJobDisplay.ScreenReportCreate()
            Close()

        Else
            frmPackDisplay.DGVPackDisplays.ClearSelection()
            frmPackDisplay.DGVPackDisplays.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            frmPackDisplay.Show()
            frmPackDisplay.ScreenReportCreate()
            Close()
        End If
    End Sub

    Private Sub btnReleaseJob_Click(sender As Object, e As EventArgs) Handles btnReleaseJob.Click

        Dim selectIndex As String
        'Find all selected rows and get info for SQL Update to DB
        Dim selectedCount As Integer = DGVNewDoff.Rows.GetRowCount(DataGridViewElementStates.Selected)    'COUNT NUMBER OF SELECTED ROWS
        Dim poycartbcode As String
        Dim poyDrumState As String
        timeUpdate()

        btnHold.Visible = True
        btnHoldCart.Visible = False
        btnHoldDrums.Visible = False

        If selectedCount = 0 Then
            MsgBox("You Must select cart that you wish to release")
            Exit Sub
        End If

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

                'DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION

            Next

            dgvCreated = 1
            DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION
            DGVDrumList.ClearSelection()  'CLEAR ONSCREEN SELECTION
            DGVDrumList.Visible = False
        End If

        If My.Settings.chkUseSort Then


            mcSortDoffDisplay()  'REFRESH DISPLAY

        Else

            mcPackDoffDisplay()  'REFRESH DISPLAY

        End If











    End Sub

    Private Sub btnHold_Click(sender As Object, e As EventArgs) Handles btnHold.Click

        'Dim selectIndex As String
        'Find all selected rows and get info for SQL Update to DB
        Dim selectedCount As Integer = DGVNewDoff.Rows.GetRowCount(DataGridViewElementStates.Selected)    'COUNT NUMBER OF SELECTED ROWS
        Dim numOfJobs = DGVNewDoff.Rows.Count
        Dim selectindex As Integer

        'Dim poycartbcode As String
        'Dim poyDrumState As String
        timeUpdate()

        If selectedCount = 0 Then
            MsgBox("You Must select cart you wish to HOLD")
            Exit Sub
        End If

        'GET THE CART NUMBER WE WANT THE DRUM INFO FOR LIST VIEW

        Try






            selectindex = DGVNewDoff.SelectedRows(0).Index.ToString





            tmpCartNum = DGVNewDoff.Rows(selectindex).Cells("poycartnum").Value
            DGVDrumList.Visible = True
            listBoxUpdate()


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        btnHold.Visible = False
        btnHoldCart.Visible = True
        btnHoldDrums.Visible = True




    End Sub


    Private Sub listBoxUpdate()   'Section to populate the List box with all Drums in diplayed jobs

        'Dim tmpMCNUM = DGVNewDoff.Rows(0).Cells("POYMCNUM").Value.ToString()
        'Dim tmpProdName = DGVNewDoff.Rows(0).Cells("POYPRODNAME").Value.ToString()
        'Dim tmpTFNum = DGVNewDoff.Rows(0).Cells("POYMERGENUM").Value.ToString()





        'SQL SEARCH FOR ALL DRUMS FOR DISPLAYED CARTS
        'GET "A" COUNT
        LExecQuery("Select POYBCODEDRUM As 'DRUM Barcode',POYDRUMSTATE FROM POYTRACK2 Where POYMCNAME = '" & tmpMCNUM & "' and  POYPRODNAME = '" & tmpProdName & "' and POYMERGENUM = '" & tmpTFNum & "' and poycartname = '" & tmpCartNum & "'   " _
                                   & " AND   POYDRUMSTATE Between 3 and 4 And (POYSORTENDTM Is Not Null ) AND  (POYDEFDRUM = 0 OR POYDEFDRUM is NULL) And (POYSHORTDRUM = 0 Or POYSHORTDRUM is Null) AND (POYMISSDRUM = 0 OR POYMISSDRUM is NULL) Order By POYBCODEDRUM ")


        Try



            If LRecordCount > 0 Then




                DGVDrumList.DataSource = LDS.Tables(0)
                DGVDrumList.Rows(0).Selected = True
                DGVDrumList.Columns("POYDRUMSTATE").Visible = False



                With DGVDrumList
                    .Font = New Font(DGVNewDoff.Font, FontStyle.Bold)
                    .Width = 200
                    .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .ReadOnly = True
                    .MultiSelect = False

                End With

                DGVDrumList.Columns("Drum Barcode").SortMode = DataGridViewColumnSortMode.NotSortable
                DGVDrumList.Columns("Drum Barcode").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                For i = 1 To LRecordCount
                    If DGVDrumList.Rows(i - 1).Cells("POYDRUMSTATE").Value = 4 Then
                        DGVDrumList.Rows(i - 1).Cells("DRUM BARCODE").Style.BackColor = Color.Red
                    End If
                Next


            End If
            DGVDrumList.ClearSelection()  'CLEAR ONSCREEN SELECTION
        Catch ex As Exception
            MsgBox(ex.ToString)
            DGVDrumList.ClearSelection()  'CLEAR ONSCREEN SELECTION
        End Try






    End Sub

    Private Sub btnHoldCart_Click(sender As Object, e As EventArgs) Handles btnHoldCart.Click

        Dim selectIndex As String

        'Find all selected rows and get info for SQL Update to DB
        ' Dim selectedCount As Integer = DGVDrumList.Rows.Count   'COUNT NUMBER OF SELECTED ROWS
        Dim selectedCount As Integer = DGVNewDoff.Rows.GetRowCount(DataGridViewElementStates.Selected)
        Dim localCount As Integer = DGVDrumList.Rows.Count

        Dim poycartbcode As String
        Dim poyDrumState As String
        timeUpdate()

        Try



            If selectedCount > 0 Then


                For i = 1 To selectedCount

                    selectIndex = DGVNewDoff.SelectedRows(i - 1).Index.ToString



                    poycartbcode = DGVDoffTmp1.Rows(selectIndex).Cells("poybcodecart").Value
                    ' poyDrumState = DGVDoffTmp1.Rows(selectIndex).Cells("poydrumstate").Value

                    For x = 1 To localCount
                        poyDrumState = DGVDrumList.Rows(x - 1).Cells("poydrumstate").Value
                        Select Case poyDrumState

                            Case 2   'check for release from sorting to packing
                                LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                               & "poybcodecart = '" & poycartbcode & "' ")

                            Case 3
                                LExecQuery("update poytrack2 Set POYHOLDSTARTTM = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '4' Where " _
                              & "poybcodecart = '" & poycartbcode & "' ")



                        End Select
                    Next
                Next

                    'Button and select update
                    DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION
                DGVDrumList.ClearSelection()  'CLEAR ONSCREEN SELECTION
                btnHoldCart.Visible = False
                btnHoldDrums.Visible = False
                btnHold.Visible = True
                DGVDrumList.Visible = False
                dgvCreated = 1

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try



        If My.Settings.chkUseSort Then
            mcSortDoffDisplay()  'REFRESH DISPLAY
        Else
            mcPackDoffDisplay()  'REFRESH DISPLAY
        End If
    End Sub

    Private Sub btnHoldDrums_Click(sender As Object, e As EventArgs) Handles btnHoldDrums.Click

        btnHoldDrums.Visible = False
        btnSave.Visible = True
        btnCancelHoldDrum.Visible = True
        btnReleaseJob.Visible = False
        btnHoldCart.Visible = False
        btnCancel.Visible = False
        DGVDrumList.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVDrumList.MultiSelect = True  ' Turn on Multiselect for Displayed DRUMS

        txtBoxMessage.Visible = True
        txtBoxMessage.Text = "Please Select The Drums" & vbCrLf & "you wish to HOLD." & vbCrLf & vbCrLf & "For Multiple DRUMS use Ctrl + Click to select." _
            & vbCrLf & vbCrLf & "Press Save to Save your Selection."


    End Sub

    Private Sub btnCancelHoldDrum_Click(sender As Object, e As EventArgs) Handles btnCancelHoldDrum.Click
        DGVDrumList.MultiSelect = False ' Turn on Multiselect for Displayed DRUMS
        DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION
        DGVDrumList.ClearSelection()   'CLEAR ONSCREEN SELECTION
        txtBoxMessage.Visible = False
        btnHold.Visible = True
        btnCancel.Visible = True
        btnReleaseJob.Visible = True
        btnHoldCart.Visible = False
        btnHoldDrums.Visible = False
        btnSave.Visible = False
        btnCancelHoldDrum.Visible = False
        DGVDrumList.Visible = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim selectIndex As Integer
        Dim tmpindex As Integer

        'Find all selected rows and get info for SQL Update to DB

        Dim DrumCount As Integer = DGVDrumList.Rows.GetRowCount(DataGridViewElementStates.Selected)    'COUNT NUMBER OF SELECTED ROWS

        Dim poycartbcode As String
        Dim poyDrumState As String
        Dim poybcodeDrum As String
        timeUpdate()


        Try


            If DrumCount = 0 Then

                MsgBox("No Drums Selected")
                Exit Sub

            ElseIf DrumCount > 0 Then


                For i As Integer = 0 To DGVDrumList.SelectedCells.Count - 1

                    selectIndex = DGVDrumList.SelectedCells.Item(i).RowIndex



                    'poycartbcode = DGVDoffTmp1.Rows(selectIndex).Cells("poybcodecart").Value




                    poyDrumState = DGVDrumList.Rows(selectIndex).Cells("poydrumstate").Value.ToString
                    poybcodeDrum = DGVDrumList.Rows(selectIndex).Cells("DRUM Barcode").Value.ToString


                    Select Case poyDrumState

                        Case 2   'check for release from sorting to packing
                            LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                           & "poybcodedrum = '" & poybcodeDrum & "' ")

                        Case 3
                            LExecQuery("update poytrack2 Set POYHOLDSTARTTM = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '4' Where " _
                          & "poybcodedrum = '" & poybcodeDrum & "' ")




                            'LExecQuery("update poytrack2 Set POYSORTRELEASE = '" & todayTimeDate & "', POYRELEASENAME = '" & frmJobEntry.varUserName & "', POYDRUMSTATE = '3' Where " _
                            '  & "poybcodecart = '" & poycartbcode & "' ")

                            'DGVMcDoffInfo.ClearSelection()  'CLEAR ONSCREEN SELECTION
                    End Select
                Next


            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            txtBoxMessage.Visible = False
        End Try

        'CLEAN UP


        btnHold.Visible = True
        btnCancel.Visible = True
        btnReleaseJob.Visible = True
        btnHoldCart.Visible = False
        btnHoldDrums.Visible = False
        btnSave.Visible = False
        btnCancelHoldDrum.Visible = False
        DGVDrumList.MultiSelect = False  ' Turn OFF Multiselect for Displayed DRUMS
        DGVDrumList.Visible = False
        txtBoxMessage.Visible = False
        DGVNewDoff.ClearSelection()  'CLEAR ONSCREEN SELECTION
        DGVDrumList.ClearSelection()
        dgvCreated = 1


        If My.Settings.chkUseSort Then
            mcSortDoffDisplay()  'REFRESH DISPLAY
        Else
            mcPackDoffDisplay()  'REFRESH DISPLAY
        End If
    End Sub


End Class