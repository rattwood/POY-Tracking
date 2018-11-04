
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Text



Public Class frmPacking72
    ' Private SQL As New SQLConn

    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
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




    Dim psorterror As String = 0

    Dim btnImage As Image
    Dim keepDefcodes As Integer

    Dim POYDrums As Integer
    Dim nextFree As Integer
    Public bcodeScan As String = ""
    Dim clr As String = ""
    Public curcone As String = 0
    Public tooAllocateCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned
    Public itemCount As Integer = 0
    'ReCheck Params
    Dim reChecked, ReCheckTime As String
    Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Dim incoming As String
    Public measureOn As String
    Public NoCone As Integer
    Public defect As Integer

    Public varCartStartTime As String   'Record time that we started measuring
    Public varCartEndTime As String
    Public coneNumOffset As Integer
    Dim varConeBCode As String
    Dim fileActive As Integer
    Public varConeNum As Integer
    Private coneCount As Integer
    Public coneState As String
    Public packingActive = 0
    Dim fmt As String = "00"
    Dim modIdxNum As String

    Dim machineCode As String
    Dim machineName As String
    Dim productCode As String
    Dim Year As String
    Dim Month As String
    Dim doffingNum As String
    Dim spinNum As String
    Dim mergeNum As String
    Dim stepNum As String


    Private Sub frmPacking72_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        POYDrums = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value

        lblCartNo.Text = frmJobEntry.varCartNum
        lblJobNum.Text = frmJobEntry.varJobNum
        lblProduct.Text = frmJobEntry.varProductName
        lblMerge.Text = frmJobEntry.mergeNum

        Dim totDrum As Integer
        Dim tmpNum As String = 0
        Dim drumToAllCount As Integer = frmJobEntry.LRecordCount



        'IF THIS IS AN EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        UpdateConeVal()

        'GET NUMBER OF CONES THAT NEED ALLOCATING Count against Job Barcode
        totDrum = POYDrums

        'toAllocatedCount = totDrum - frmJobEntry.drumToAllcount

        txtboxTotal.Text = totDrum
        txtboxAllocated.Text = allocatedCount

        If My.Settings.debugSet Then
            Label14.Visible = True
            Label16.Visible = True
            Label18.Visible = True
        End If


        Me.KeyPreview = True  'Allows us to look for advace character from barcode


    End Sub

    Public Sub UpdateConeVal()
        If My.Settings.debugSet Then frmDGV.Show()

        allocatedCount = 0

        For rw As Integer = 1 To POYDrums

            If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYBCODEDRUM").Value) Then
                nextFree = rw  'This gets the next free location for new drum to be entered in to db
                Exit For 'This will get Next Free location
            Else
                If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value < "15" Then
                    Controls("Button" & rw).BackgroundImage = My.Resources.NoDrum    'To allocate
                    Controls("Button" & rw).ForeColor = Color.Black
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = "15" Then
                    Controls("Button" & rw).BackgroundImage = My.Resources.Have_Drum        'Already allocated
                    Controls("Button" & rw).ForeColor = Color.Black
                    Me.Controls("Button" & rw).Enabled = False
                    allocatedCount = allocatedCount + 1

                End If
            End If
        Next




    End Sub



    'Private Sub txtConeBcode_TextChanged(sender As Object, e As EventArgs) Handles txtConeBcode.TextChanged

    Private Sub prgContinue()




        bcodeScan = txtConeBcode.Text


        Dim curDrum As String = nextFree  'so index for DGV works
        Dim drumCount As Integer = 0
        Dim today As String = DateAndTime.Today
        ' today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")


        Try

            getMCName()
            machineCode = bcodeScan.Substring(0, 2)
            productCode = bcodeScan.Substring(2, 3)
            Year = bcodeScan.Substring(5, 2)
            Month = bcodeScan.Substring(7, 2)
            doffingNum = bcodeScan.Substring(9, 3)
            spinNum = bcodeScan.Substring(12, 2)
            mergeNum = bcodeScan.Substring(9, 3)
            getMCName()



            If Not (txtConeBcode.TextLength = 14) Then  ' LENGTH OF BARCODE
                MsgBox("This is not a DRUM barcode Please RE Scan")
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Exit Sub
            ElseIf Not (productCode = frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value) Then
                MsgBox("This DRUM is the wrong product code ")
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Exit Sub
            End If

            For i = 1 To POYDrums
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value) Then Continue For
                If frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value.ToString = bcodeScan Then
                    Label1.Visible = True
                    Label1.Text = "Drum already allocated"
                    DelayTM()
                    Label1.Visible = False
                    txtConeBcode.Clear()
                    txtConeBcode.Focus()
                    txtConeBcode.Clear()
                    txtConeBcode.Focus()
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            txtConeBcode.Clear()
            txtConeBcode.Focus()
            Exit Sub
        End Try




        Try
            If IsDBNull(frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYDRUMSTATE").Value) Then

                ' If frmDGV.DGVdata.Rows(nextFree).Cells("POYDRUMSTATE").Value.ToString < "15" Then
                curDrum = nextFree
                Controls("Button" & curDrum).BackgroundImage = My.Resources.Have_Drum        'Grade A Cone
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYMCNUM").Value = machineCode
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYYY").Value = Year
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYPRMM").Value = Month
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYDOFFNUM").Value = doffingNum
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYSPINNUM").Value = spinNum
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYPACKNAME").Value = frmJobEntry.PackOp
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYDRUMSTATE").Value = "15"
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYPACKDATE").Value = today
                getStepNum()
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYSTEPNUM").Value = stepNum
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYBCODEDRUM").Value = bcodeScan
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYMCNAME").Value = machineName

                allocatedCount = allocatedCount + 1
                txtboxAllocated.Text = allocatedCount
                nextFree = nextFree + 1
                endCheck()

            End If

        Catch ex As Exception
            MsgBox("Please re scan Drum" & vbNewLine & ex.Message)
        End Try

        If My.Settings.debugSet Then
            Label14.Text = nextFree
            Label16.Text = allocatedCount
            Label18.Text = curDrum - 1
            curDrum = 0
        End If


        txtConeBcode.Clear()
        txtConeBcode.Focus()

    End Sub

    Private Sub btnDefect_Click(sender As Object, e As EventArgs)
        Me.Hide()
        packingActive = 1

        frmPackingFault.Show()


    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.btnNewPallet.Enabled = True
        frmJobEntry.btnOldPallet.Enabled = True

        frmJobEntry.txtDrumNum.Visible = False
        frmJobEntry.comBoxDrumPal.Visible = False
        frmJobEntry.comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select
        frmJobEntry.Label2.Visible = False
        frmJobEntry.Label4.Visible = False
        frmJobEntry.txtDrumNum.Visible = False
        frmJobEntry.comBoxDrumPal.Enabled = True

        frmJobEntry.Show()
        frmJobEntry.txtDrumNum.Clear()
        frmJobEntry.txtDrumNum.Focus()



        Me.Close()
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click

        UpdateDatabase()

    End Sub

    Public Sub endCheck()

        If POYDrums = allocatedCount Then
            curcone = 0
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            MsgBox("ready for reports")
            'frmPackReport.packPrint() 'Print the packing report and go back to Job Entry for the next cart
            ' frmPackRepMain.PackRepMainSub()
            'frmPackRepMain.Close()
            UpdateDatabase()

        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub



    Private Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If frmJobEntry.LDS.HasChanges Then


                'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try



        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.txtDrumNum.Clear()
        frmJobEntry.txtDrumNum.Focus()
        frmJobEntry.Show()
        Me.Close()



    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
        'Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState



    End Sub

    Private Sub getMCName()

        Select Case machineCode
            Case 51
                machineName = 111
            Case 52
                machineName = 112
            Case 53
                machineName = 121
            Case 54
                machineName = 122
            Case 55
                machineName = 130
            Case 56
                machineName = 141
            Case 57
                machineName = 142
            Case 58
                machineName = 151
            Case 59
                machineName = 152
            Case 60
                machineName = 210
            Case 61
                machineName = 220
            Case 62
                machineName = 230
            Case 63
                machineName = 241
            Case 64
                machineName = 242
            Case 65
                machineName = 250
            Case 66
                machineName = 310
            Case 67
                machineName = 321
            Case 68
                machineName = 322
            Case 69
                machineName = 330
            Case 70
                machineName = 341
            Case 71
                machineName = 342
            Case 72
                machineName = 350
            Case 73
                machineName = 361
            Case 74
                machineName = 362
            Case 75
                machineName = 410
            Case 76
                machineName = 420
            Case 77
                machineName = 430
            Case 78
                machineName = 441
            Case 79
                machineName = 442
            Case 80
                machineName = 450
            Case 81
                machineName = 460
        End Select


    End Sub

    Private Sub getStepNum()

        Select Case nextFree
            Case 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
                stepNum = 1

            Case 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24
                stepNum = 2

            Case 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36
                stepNum = 3

            Case 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48
                stepNum = 4

            Case 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60
                stepNum = 5

            Case 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72
                stepNum = 6
        End Select


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

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub


End Class