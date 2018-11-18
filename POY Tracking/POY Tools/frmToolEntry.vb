Imports System.Data.SqlClient
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmToolEntry
    Public SQL As New SQLConn
    Dim bcodescan As String
    Dim dateSearchString As String
    Dim traceFileLoc As String
    Dim SheetCodeString As String
    Dim savename As String


    Private Sub frmToolEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtTraceNum.Clear()
        txtTraceNum.Focus()


        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub

    Private Sub txtTraceNum_TextChanged(sender As Object, e As EventArgs) Handles txtTraceNum.TextChanged
        bcodescan = txtTraceNum.Text.ToString
    End Sub

    Private Sub traceCheck()
        Try


            If Not (txtTraceNum.TextLength = 10) Then  ' LENGTH OF BARCODE
                lblError.Visible = True
                lblError.Text = "This is not a TRACE barcode" & vbCrLf & "Please RE Scan"
                DelayTM()
                lblError.Visible = False
                txtTraceNum.Clear()
                txtTraceNum.Focus()
                Exit Sub
            Else

                SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "' and POYBCODEDRUM Is Not Null Order by POYPACKIDX ")

                If SQL.RecordCount = 0 Then

                    Me.KeyPreview = False  'Turn off Barcode entry

                    lblError.Visible = True
                    lblError.Text = "This TRACE is not in the system"
                    DelayTM()
                    lblError.Visible = False

                    Me.KeyPreview = True  'Allows us to look for advace character from barcode

                    txtTraceNum.Clear()
                    txtTraceNum.Focus()
                    Exit Sub

                Else
                    Try
                        If SQL.RecordCount > 0 Then
                            frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                            frmDGV.DGVdata.Rows(0).Selected = True
                            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)

                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            lblError.Text = ""
                            lblError.Visible = False
                        End If
                    Catch ex As Exception
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        MsgBox("Job creation Error" & vbNewLine & ex.Message)
                    End Try

                    Label3.Visible = True
                    Label5.Visible = True
                    Label7.Visible = True
                    Label9.Visible = True
                    Label11.Visible = True

                    lblProduct.Visible = True
                    lblMerge.Visible = True
                    lblDate.Visible = True
                    lblPalSize.Visible = True
                    lblDrums.Visible = True

                    lblProduct.Text = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value
                    lblMerge.Text = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value
                    lblDate.Text = frmDGV.DGVdata.Rows(0).Cells("POYPACKDATE").Value
                    lblPalSize.Text = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value
                    lblDrums.Text = SQL.RecordCount


                    btnChangeDrum.Enabled = True
                    btnChangeDrum.BackColor = Color.LightGreen
                    btnChangeSteps.Enabled = True
                    btnChangeSteps.BackColor = Color.LightGreen
                    btnChangeTrace.Enabled = True
                    btnChangeTrace.BackColor = Color.LightGreen
                    txtTraceNum.Enabled = False

                End If
            End If


        Catch ex As Exception
            'MsgBox("SQl Search Error " & vbNewLine & ex.Message)
            'txtTraceNum.Clear()
            'txtTraceNum.Focus()
            'Exit Sub
        End Try

    End Sub


    Private Sub DelayTM()
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()

    End Sub

    Private Sub btnChangeSteps_Click(sender As Object, e As EventArgs) Handles btnChangeSteps.Click
        lblComplete.Visible = False
        Dim result = MessageBox.Show("Edit Job Yes Or No", "Are you sure you wish to change all the STEP numbers", MessageBoxButtons.YesNo, MessageBoxIcon.Information)





        If result = DialogResult.Yes Then



            SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "'  Order by POYPACKIDX ")

            If SQL.RecordCount > 0 Then

                Try
                    If SQL.RecordCount > 0 Then
                        frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                        frmDGV.DGVdata.Rows(0).Selected = True
                        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)

                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        lblError.Text = ""
                        lblError.Visible = False
                    End If
                Catch ex As Exception
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    MsgBox("Job creation Error" & vbNewLine & ex.Message)
                End Try

            End If

            Dim tmpcount As Integer = frmDGV.DGVdata.Rows.Count
            Dim idxReverse As Integer = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value
            Dim fmt As String = "000"
            Dim modIdxNum As String
            Dim startcount As Integer
            Dim endcount As Integer
            Dim rcount As Integer
            Dim done1, done2, done3, done4, done5, done6 As Integer

            'Round 1 change to tmp1,2,3,4,5 & 6
            For i = 1 To tmpcount

                'Advance without writing a value if no Drum
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value) Then Continue For

                'Convert all steps to new tmp step numbers
                Select Case frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value.ToString
                    Case 1
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp1"
                    Case 2
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp2"
                    Case 3
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp3"
                    Case 4
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp4"
                    Case 5
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp5"
                    Case 6
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = "tmp6"
                End Select
            Next

            ''Now get start and end values for the drum number reversal
            For i = 1 To tmpcount
                Select Case frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value.ToString
                    Case "tmp1"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 6

                        If Not (done1 = 1) Then
                            If idxReverse = 120 Then
                                startcount = 101
                                endcount = 20
                            ElseIf idxReverse = 72 Then
                                startcount = 61
                                endcount = 12
                            ElseIf idxReverse = 48 Then
                                startcount = 41
                                endcount = 8
                            End If
                            done1 = 1
                        End If


                        modIdxNum = startcount.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value = modIdxNum
                        startcount = startcount + 1


                    Case "tmp2"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 5

                        If Not (done2 = 1) Then
                            If idxReverse = 120 Then
                                startcount = 81
                                endcount = 20
                            ElseIf idxReverse = 72 Then
                                startcount = 61
                                endcount = 12
                            ElseIf idxReverse = 48 Then
                                startcount = 33
                                endcount = 8
                            End If
                            done2 = 1
                        End If

                        modIdxNum = startcount.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value = modIdxNum
                        startcount = startcount + 1

                    Case "tmp3"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 4

                        If Not (done3 = 1) Then
                            If idxReverse = 120 Then
                                startcount = 61
                                endcount = 20
                            ElseIf idxReverse = 72 Then
                                startcount = 61
                                endcount = 12
                            ElseIf idxReverse = 48 Then
                                startcount = 25
                                endcount = 8
                            End If
                            done3 = 1
                        End If


                        modIdxNum = startcount.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value = modIdxNum
                        startcount = startcount + 1


                    Case "tmp4"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 3


                        If Not (done4 = 1) Then
                            If idxReverse = 120 Then
                                startcount = 41
                                endcount = 20
                            ElseIf idxReverse = 72 Then
                                startcount = 61
                                endcount = 12
                            ElseIf idxReverse = 48 Then
                                startcount = 17
                                endcount = 8
                            End If
                            done4 = 1
                        End If


                        modIdxNum = startcount.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value = modIdxNum
                        startcount = startcount + 1


                    Case "tmp5"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 2

                        If Not (done5 = 1) Then
                            If idxReverse = 120 Then
                                startcount = 21
                            ElseIf idxReverse = 72 Then
                                startcount = 61
                                endcount = 12
                            ElseIf idxReverse = 48 Then
                                startcount = 9
                                endcount = 8
                            End If
                            done5 = 1
                        End If



                        modIdxNum = startcount.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value = modIdxNum
                        startcount = startcount + 1


                    Case "tmp6"
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYSTEPNUM").Value = 1

                        If Not (done6 = 1) Then
                            If idxReverse = 120 Then
                                startcount = 1
                                endcount = 20
                            ElseIf idxReverse = 72 Then
                                startcount = 61
                                endcount = 12
                            ElseIf idxReverse = 48 Then
                                startcount = 1
                                endcount = 8
                            End If
                            done6 = 1
                        End If



                        modIdxNum = startcount.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("POYPACKIDX").Value = modIdxNum
                        startcount = startcount + 1


                End Select
            Next





            UpdateDatabase()
            chkPackingExists()

            lblComplete.Visible = True
            Exit Sub
        End If

        If result = DialogResult.No Then

            Exit Sub

        End If


    End Sub



    Private Sub btnChangeDrum_Click(sender As Object, e As EventArgs) Handles btnChangeDrum.Click
        Hide()
        frmChangeDrums.Show()



    End Sub


    Private Sub btnChangeTrace_Click(sender As Object, e As EventArgs) Handles btnChangeTrace.Click
        frmchangeTrace.txtNewTraceNum.Clear()
        frmchangeTrace.txtNewTraceNum.Focus()
        frmchangeTrace.Show()
        bcodescan = txtTraceNum.Text.ToString  'to get updated trace number
        smalldbUpdate()
        chkPackingExists()

    End Sub

    Public Sub smalldbUpdate()
        'This routine is to refresh DGV with data for new Trace Number assigned
        SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) and POYTRACENUM = '" & bcodescan & "' and POYBCODEDRUM Is Not Null Order by POYPACKIDX ")
        Try
            If SQL.RecordCount > 0 Then
                frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)

                Me.Cursor = System.Windows.Forms.Cursors.Default
                lblError.Text = ""
                lblError.Visible = False
            End If

        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("small update Error" & vbNewLine & ex.Message)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        txtTraceNum.Clear()
        txtTraceNum.Focus()

        frmJobEntry.Show()
        Close()



    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtTraceNum.Enabled = True
        txtTraceNum.Clear()
        txtTraceNum.Focus()

        Label3.Visible = False
        Label5.Visible = False
        Label7.Visible = False
        Label9.Visible = False
        Label11.Visible = False
        lblProduct.Visible = False
        lblMerge.Visible = False
        lblDate.Visible = False
        lblPalSize.Visible = False
        lblDrums.Visible = False
        lblTraceComplete.Visible = False

        btnChangeDrum.Enabled = False
        btnChangeDrum.BackColor = Color.LightGray
        btnChangeSteps.Enabled = False
        btnChangeSteps.BackColor = Color.LightGray
        btnChangeTrace.Enabled = False
        btnChangeTrace.BackColor = Color.LightGray

    End Sub

    Public Sub chkPackingExists()  'Routine to see if packing form already exists



        dateSearchString = bcodescan.Substring(5, 2) & "_" & bcodescan.Substring(3, 2) & "_20" & bcodescan.Substring(1, 2) 'creates the directory name to look for
        traceFileLoc = My.Settings.dirPacking & "\" & dateSearchString & "\"   'creates the full file name to look for
        'MsgBox(traceFileLoc)

        Dim existName As String = traceFileLoc & "\" & bcodescan & ".xlsx"
        Dim reName As String = bcodescan & "OLD.xlsx"

        'create the save name of the file
        savename = (traceFileLoc & bcodescan & ".xlsx").ToString

        'fileOpenChk()

        If File.Exists(traceFileLoc & bcodescan & ".xlsx") Then
            'Check to see if file is open and wait for it to be closed
            ' fileOpenChk()
            'rename existing file
            My.Computer.FileSystem.DeleteFile(existName)
        End If
        ' End If

        reportCreate()


    End Sub


    Private Sub fileOpenChk()
        Dim openExcel As String = traceFileLoc & "\" & bcodescan & ".xlsx"
        Dim exOpen As Boolean
        Dim fs As FileStream


        Try
            fs = File.Open(openExcel, FileMode.Open, FileAccess.Read, FileShare.None)
        Catch ex As Exception
            exOpen = True
        Finally
            If Not IsNothing(fs) Then : fs.Close() : End If
        End Try

        MsgBox("string = " & openExcel & "  " & exOpen.ToString)

        If exOpen = True Then
            MsgBox("Excel file is already open, please check all computers and close file before preesing OK." & vbCrLf & "If you press OK before closing the file data will be lost")
            chkLoop()
        End If


    End Sub

    Private Sub chkLoop()

        fileOpenChk()
        Exit Sub

    End Sub


    Private Sub reportCreate()
        ' Get template file open and populate
        Dim MyUpdateExcel As New Excel.Application
        Dim xlUpdateWorkbook As Excel.Workbook
        Dim xlUpdatesheets As Excel.Worksheet
        Dim nfree As Integer
        Dim ncfree As Integer




        'OPEN A NEW WORKSHEET
        xlUpdateWorkbook = MyUpdateExcel.Workbooks.Open(My.Settings.dirTemplate & "\" & "tmpTraceDrumPerPall.xlsx")

        '  xlTodayWorkbook = MyUpdateExcel.Workbooks.Open(savename)
        'mycount = xlTodyWorkbook.Worksheets.Count


        Dim colCount As Integer = 2
        Dim sheetName As String
        Dim productName As String = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value.ToString & "_" & frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value.ToString
        Dim drumInfo As String
        Dim spinNum As String

        'Create the sheet name
        Select Case frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value
            Case "48"
                sheetName = productName & "_48"

            Case "72"
                sheetName = productName & "_72"

            Case "120"
                sheetName = productName & "_120"

        End Select


        nfree = 11
        ncfree = 2
        colCount = 2


        'ReName the work sheet 
        CType(MyUpdateExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = sheetName
        MyUpdateExcel.Visible = True
        'Product Name
        MyUpdateExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value
        'Product Merge Num
        MyUpdateExcel.Cells(5, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C5
        'DATE
        MyUpdateExcel.Cells(3, 11) = Date.Now.ToString("dd MM yyyy")              'K3
        'PACKING TYPE K value
        MyUpdateExcel.Cells(4, 9) = frmJobEntry.varKNum                  'I4
        'CHEESE WEIGHT
        MyUpdateExcel.Cells(6, 9) = frmJobEntry.varProdWeight                 'I6
        'Packer Name
        MyUpdateExcel.Cells(31, 11) = frmDGV.DGVdata.Rows(0).Cells("POYPACKNAME").Value
        'PALLET NUMBER = Trace Number
        MyUpdateExcel.Cells(6, 3) = frmTraceEntry.txtTraceNum.Text
        'Add Barcode to Sheet
        createBarcode()
        MyUpdateExcel.Cells(2, 1) = SheetCodeString
        MyUpdateExcel.Cells(3, 1) = txtTraceNum.Text



        Try

            Select Case frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value
                Case "48"
                    For i = 1 To frmDGV.DGVdata.Rows.Count
                        If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                        If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then


                            drumInfo = (frmJobEntry.varMachineName & " " & frmJobEntry.varMonth & " " & frmJobEntry.varDoffingNum & " " &
                                frmDGV.DGVdata.Rows(i - 1).Cells("POYSPINNUM").Value.ToString)



                            'WRITE CONE NUMBER TO SHEET
                            'MyUpdateExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value
                            MyUpdateExcel.Cells(nfree, ncfree) = drumInfo
                            nfree = nfree + 1
                            'Increment the Col Number
                            If nfree = 19 And ncfree < 12 Then
                                ncfree = ncfree + 2
                                nfree = 11
                            End If
                        End If
                    Next

                Case "72"
                    For i = 1 To frmDGV.DGVdata.Rows.Count
                        If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                        If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then

                            'WRITE CONE NUMBER TO SHEET
                            MyUpdateExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value

                            nfree = nfree + 1
                            'Increment the Col Number
                            If nfree = 23 And ncfree < 12 Then
                                ncfree = ncfree + 2
                                nfree = 11
                            End If
                        End If
                    Next

                Case "120"
                    For i = 1 To frmDGV.DGVdata.Rows.Count
                        If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                        If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then

                            'WRITE CONE NUMBER TO SHEET
                            MyUpdateExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value

                            nfree = nfree + 1
                            If nfree = 31 And ncfree < 12 Then
                                ncfree = ncfree + 2
                                nfree = 11
                            End If
                        End If
                    Next

            End Select
        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyUpdateExcel.DisplayAlerts = False
            MsgBox(savename)
            xlUpdateWorkbook.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox("save As Area" & ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlUpdateWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox("Close Template Area " & ex.Message)
        End Try


        MyUpdateExcel.Quit()
        releaseObject(xlUpdatesheets)
        releaseObject(xlUpdateWorkbook)
        releaseObject(MyUpdateExcel)
        frmPacking48.UpdateDatabase()  'Update the database with changes and then close and go back to Job Entry screen
        Me.Close()
    End Sub

    Private Sub releaseObject(ByVal obj As Object)

        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub createBarcode()

        SheetCodeString = ("*" & bcodescan & "*")

    End Sub


    Public Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If SQL.SQLDS.HasChanges Then


                'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                SQL.SQLDA.Update(SQL.SQLDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try




        'Reload the DGV with new data that was written to the Database
        SQL.ExecQuery("Select * from POYTrack where (POYTRACENUM Is Not Null) And POYTRACENUM = '" & bcodescan & "' and POYBCODEDRUM Is Not Null Order by POYPACKIDX ")


        frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
        frmDGV.DGVdata.Rows(0).Selected = True
        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(SQL.SQLDA)





    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows

        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState



    End Sub






    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmTraceEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then


            traceCheck()


        End If

    End Sub



End Class