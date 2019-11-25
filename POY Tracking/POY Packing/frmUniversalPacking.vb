
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel
Imports System.Globalization
Imports System.Data.SqlClient


Public Class frmUniversalPacking
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    ' Private SQL As New SQLConn
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


    'TIME
    Dim time As New DateTime
    Public todayTimeDate As String
    Dim dateFormat As String = "yyyy-MM-dd HH:mm:ss"


    'Manual assesment variables
    Dim btnImage As Image
    Dim keepDefcodes As Integer

    'Faults
    Dim Fault_S As String
    Dim Fault_X As String
    Dim shortC(16) As String
    Dim bcodeScan As String
    Private rowendcount As Integer
    Private allocatedCount As Integer 'count of DRUMs scanned
    Private toAllocateCount As Integer
    Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Dim saveB As Integer
    Dim saveS As Integer
    Public NoDRUM As Integer

    Public defect As Integer
    Public POYSHORTDRUM As Integer
    Public varCartStartTime As String   'Record time that we started measuring
    Public varCartEndTime As String
    Public DRUMNumOffset As Integer
    Dim varDRUMBCode As String
    Dim fileActive As Integer
    Public varDRUMNum As Integer

    Public DRUMCount As Integer
    ' Public DRUMState As String
    Dim fltDRUMNum As String
    Dim POYDrums As Integer
    Dim nextFree As Integer
    Dim machineCode As String
    Dim machineName As String
    Dim productCode As String
    Dim Year As String
    Dim Month As String
    Dim doffingNum As String
    Dim spinNum As String
    Dim mergeNum As String
    Dim stepNum As String




    Private Sub frmUniversalPacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        POYDrums = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value


        Dim totDrum As Integer
        Dim tmpNum As String = 0
        Dim drumToAllCount As Integer = frmJobEntry.LRecordCount





        'GET NUMBER OF CONES THAT NEED ALLOCATING Count against Job Barcode
        totDrum = POYDrums

        'toAllocatedCount = totDrum - frmJobEntry.drumToAllcount

        txtBoxPalletSize.Text = totDrum
        txtboxAllocated.Text = allocatedCount

        'lblCartNum.Text = tmpCartNum  'show cart Number
        lblTraceNum.Text = frmJobEntry.varCartBCode  'Drum number of first drum scanner which then becomes the tmp Trace number
        lblProduct.Text = frmJobEntry.varProductName
        lblMerge.Text = frmJobEntry.mergeNum
        txtBoxPalletSize.Text = frmJobEntry.drumPerPal





        'Check to see if this is existing Pallet or new Pallet


        UpdateImageValues()


    End Sub


    'Private Sub txtConeBcode_TextChanged(sender As Object, e As EventArgs) Handles txtConeBcode.TextChanged

    Private Sub prgContinue()




        bcodeScan = txtDrumBcode.Text


        Dim curDrum As String = nextFree  'so index for DGV works
        Dim drumCount As Integer = 0




        Try


            machineCode = bcodeScan.Substring(0, 2)
            productCode = bcodeScan.Substring(2, 3)
            Year = bcodeScan.Substring(5, 2)
            Month = bcodeScan.Substring(7, 2)
            doffingNum = bcodeScan.Substring(9, 3)
            spinNum = bcodeScan.Substring(12, 2)
            mergeNum = bcodeScan.Substring(9, 3)




            If Not (txtDrumBcode.TextLength = 14) Or txtDrumBcode.Text.Substring(12, 1) = "P" Then  ' LENGTH OF BARCODE
                MsgBox("This is not a DRUM barcode Please RE Scan")
                txtDrumBcode.Clear()
                txtDrumBcode.Focus()
                Exit Sub
            ElseIf Not (productCode = frmCartDGV.DGVCart.Rows(0).Cells("POYPRNUM").Value) Then
                MsgBox("This DRUM is the wrong product code ")
                txtDrumBcode.Clear()
                txtDrumBcode.Focus()
                Exit Sub
            End If

            For i = 1 To POYDrums
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value) Then Continue For
                If frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value.ToString = bcodeScan Then
                    Label1.Visible = True
                    Label1.Text = "Drum already allocated"
                    DelayTM()
                    Label1.Visible = False
                    txtDrumBcode.Clear()
                    txtDrumBcode.Focus()
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            txtDrumBcode.Clear()
            txtDrumBcode.Focus()
            Exit Sub
        End Try




        Try
            If IsDBNull(frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYDRUMSTATE").Value) Then


                curDrum = nextFree
                GroupBox5.Controls("btnPacked" & curDrum).BackgroundImage = My.Resources.Have_Drum        'Grade A Cone


                'POYTRACE DGV UPDATES
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYBCODEDRUM").Value = bcodeScan




                'POYPACK DGV UPDATES

                frmCartDgv.DGVCart.Rows(curDrum - 1).Cells("POYDRUMSTATE").Value = "15"
                frmCartDgv.DGVCart.Rows(curDrum - 1).Cells("POYPACKNAME").Value = frmJobEntry.PackOp
                frmCartDgv.DGVCart.Rows(curDrum - 1).Cells("POYPACKDATE").Value = frmJobEntry.time

                getStepNum()

                frmCartDgv.DGVCart.Rows(curDrum - 1).Cells("POYSTEPNUM").Value = stepNum
                frmDGV.DGVdata.Rows(curDrum - 1).Cells("POYBCODEDRUM").Value = bcodeScan


                allocatedCount = allocatedCount + 1
                txtboxAllocated.Text = allocatedCount
                nextFree = nextFree + 1
                endCheck()

            End If

        Catch ex As Exception
            MsgBox("Please re scan Drum" & vbNewLine & ex.Message)
        End Try




        txtDrumBcode.Clear()
        txtDrumBcode.Focus()

    End Sub

    Public Sub endCheck()

        If POYDrums = allocatedCount Then
            EndJob()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub EndJob()

        Try
            'curcone = 0
            frmTraceEntry.Show()
            Hide()

        Catch ex As Exception
            MsgBox("Update Error " & vbNewLine & ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try


    End Sub

    Private Sub getStepNum()

        Select Case nextFree
            Case 1, 2, 3, 4, 5, 6, 7, 8
                stepNum = 1

            Case 9, 10, 11, 12, 13, 14, 15, 16
                stepNum = 2

            Case 17, 18, 19, 20, 21, 22, 23, 24
                stepNum = 3

            Case 25, 26, 27, 28, 29, 30, 31, 32
                stepNum = 4

            Case 33, 34, 35, 36, 37, 38, 39, 40
                stepNum = 5

            Case 41, 42, 43, 44, 45, 46, 47, 48
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


    Private Sub UpdateImageValues()

        'This section will check machine number and P1 and apply correct drum numbers on screen


        Select Case frmJobEntry.varCartNum
            Case "P1"
                'Put new Drum numbers on images


                For I = 1 To 16
                    btn1.Text = "01"
                    btn2.Text = "02"
                    btn3.Text = "03"
                    btn4.Text = "04"
                    btn5.Text = "09"
                    btn6.Text = "10"
                    btn7.Text = "11"
                    btn8.Text = "12"
                    btn9.Text = "17"
                    btn10.Text = "18"
                    btn11.Text = "19"
                    btn12.Text = "20"
                    btn13.Text = "25"
                    btn14.Text = "26"
                    btn15.Text = "27"
                    btn16.Text = "28"
                Next
            Case "P2"
                'Put new Drum numbers on images
               btn1.Text = "05"
                btn2.Text = "06"
                btn3.Text = "07"
                btn4.Text = "08"
                btn5.Text = "13"
                btn6.Text = "14"
                btn7.Text = "15"
                btn8.Text = "16"
                btn9.Text = "21"
                btn10.Text = "22"
                btn11.Text = "23"
                btn12.Text = "24"
                btn13.Text = "29"
                btn14.Text = "30"
                btn15.Text = "31"
                btn16.Text = "32"

            Case "P5"
                'Put new Drum numbers on images
               btn1.Text = "33"
                btn2.Text = "34"
                btn3.Text = "35"
                btn4.Text = "36"
                btn5.Text = "41"
                btn6.Text = "42"
                btn7.Text = "43"
                btn8.Text = "44"
                btn9.Text = "49"
                btn10.Text = "50"
                btn11.Text = "51"
                btn12.Text = "52"
                btn13.Text = "57"
                btn14.Text = "58"
                btn15.Text = "59"
                btn16.Text = "60"

            Case "P6"
                'Put new Drum numbers on images
               btn1.Text = "37"
                btn2.Text = "38"
                btn3.Text = "39"
                btn4.Text = "40"
                btn5.Text = "45"
                btn6.Text = "46"
                btn7.Text = "47"
                btn8.Text = "48"
                btn9.Text = "53"
                btn10.Text = "54"
                btn11.Text = "55"
                btn12.Text = "56"
                btn13.Text = "61"
                btn14.Text = "62"
                btn15.Text = "63"
                btn16.Text = "64"
        End Select
        updatePackGrid()
        UpdateDrumVal()

    End Sub

    Private Sub UpdateDrumVal()



        allocatedCount = 0



        '"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        Dim cellVal As String
        Dim reasonFound As Integer = 0
        Dim tmpCartNum As String



        LExecQuery("Select POYBCODECART from POYTRACK where POYBCODEDRUM = '" & frmJobEntry.varCartBCode & "' ")
        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmCartDgv.DGVCart.DataSource = LDS.Tables(0)
            frmCartDgv.DGVCart.Rows(0).Selected = True
            tmpCartNum = frmCartDgv.DGVCart.Rows(0).Cells("POYBCODECART").Value
        End If




        LExecQuery("SELECT * FROM POYTrack WHERE POYBCODECART = '" & tmpCartNum & "'  ")
        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmCartDgv.DGVCart.DataSource = LDS.Tables(0)
            frmCartDgv.DGVCart.Rows(0).Selected = True

            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)
        End If

        If frmJobEntry.varNewPal = 1 Then
            frmCartDgv.DGVCart.Rows(0).Cells("POYBCODEDRUM").Value = frmJobEntry.varCartBCode

        End If

        lblCartNum.Text = tmpCartNum  'show cart Number
        lblTraceNum.Text = frmJobEntry.varCartBCode  'Drum number of first drum scanner which then becomes the tmp Trace number
        lblProduct.Text = frmJobEntry.varProductName
        lblMerge.Text = frmJobEntry.mergeNum
        txtBoxPalletSize.Text = frmJobEntry.drumPerPal



        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   ROUTINE TO POULATE CART @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Try
            For rw As Integer = 1 To 16 'Pallet count Drum on each cart

                'Update Scanned Image
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDRUMSTATE").Value) Then

                    cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDRUMSTATE").Value.ToString
                    'CHECK FOR SCANNED Drum AND SET TO GREEN
                    If cellVal = 3 Then
                        GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.Have_Drum
                        GroupBox4.Controls("btn" & rw).Enabled = True
                    ElseIf cellVal = 15 Then
                        GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.Packed_Drum
                        GroupBox4.Controls("btn" & rw).Enabled = False
                    End If
                    ' cellVal = Nothing
                End If


                'CHECK FOR SHORT AND UPDATE IMAGE
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then

                    If Val(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYSHORTDRUM").Value) > 0 Then
                        cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYSHORTDRUM").Value

                        If cellVal > 0 Then

                            GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.NotScan
                            GroupBox4.Controls("btn" & rw).Enabled = True

                        End If
                    End If
                End If

                'CHECK FOR MISSING DRUM AND UPDATE IMAGE
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYMISSDRUM").Value) Then

                    If frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYMISSDRUM").Value > 0 Then
                        cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYMISSDRUM").Value
                        If cellVal > 0 Then
                            GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.NotScan
                            GroupBox4.Controls("btn" & rw).Enabled = False
                        End If
                    End If
                End If

                'CHECK FOR DEFECT AND UPDATE IMAGE
                If Not IsDBNull(frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                    cellVal = frmCartDgv.DGVCart.Rows(rw - 1).Cells("POYDEFDRUM").Value
                    If cellVal > 0 Then
                        GroupBox4.Controls("btn" & rw).BackgroundImage = My.Resources.NotScan
                    End If
                End If
                cellVal = Nothing
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        '******************************************************************** Routine to update Drums *****************************************************************
        For rw As Integer = 1 To POYDrums

            If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYBCODEDRUM").Value) Then
                nextFree = rw  'This gets the next free location for new drum to be entered in to db
                Exit For 'This will get Next Free location
            Else
                'If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = "3" Then
                '    GroupBox5.Controls("Button" & rw).BackgroundImage = My.Resources.NoDrum    'To allocate
                '    GroupBox5.Controls("Button" & rw).ForeColor = Color.Black
                'ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = "15" Then
                GroupBox5.Controls("btnPacked" & rw).BackgroundImage = My.Resources.Have_Drum        'Already allocated
                GroupBox5.Controls("btnPacked" & rw).ForeColor = Color.Black
                GroupBox5.Controls("btnPacked" & rw).Enabled = False
                allocatedCount = allocatedCount + 1

            End If
        Next







    End Sub

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

            LException = "ExecQuery Error:    " & vbNewLine & ex.Message
            MsgBox(LException)
            writeerrorLog.writelog("ExecQuery Error:", ex.Message, False, "System_Fault")
            writeerrorLog.writelog("ExecQuery Error:", ex.ToString, False, "System_Fault")

        End Try

    End Sub

    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub



    Private Sub updatePackGrid()




        'Select Case tmpPalletDrums

        Select Case frmJobEntry.drumPerPal

            Case 48
                'Hide unwanted drum locations
                For i = 1 To 120
                    Select Case i
                        Case 9 To 20 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 29 To 40 'Hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 49 To 60 'Hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 69 To 80 'Hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 89 To 100 'hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False
                        Case 109 To 120 'hide
                            GroupBox5.Controls("btnPacked" & i.ToString).Visible = False

                    End Select
                Next

                Dim tmpbtnnum As Integer = 1

                For i = 1 To 108
                    Select Case i
                        Case 1 To 8
                            GroupBox5.Controls("btnPacked" & i.ToString).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 21 To 28 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 41 To 48 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 61 To 68 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 81 To 88 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 101 To 108 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                    End Select
                Next

            Case 72
                'Hide unwanted drum locations
                For i = 1 To 120
                    Select Case i
                        Case 13 To 20 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 33 To 40 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 53 To 60 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 73 To 80 'Hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 93 To 100 'hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                        Case 113 To 120 'hide
                            GroupBox5.Controls("btnPacked" & i).Visible = False
                    End Select
                Next

                Dim tmpbtnnum As Integer = 1

                For i = 1 To 112
                    Select Case i
                        Case 1 To 12
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 21 To 32 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 41 To 52 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 61 To 72 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 81 To 92 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                        Case 101 To 112 'show and re number
                            GroupBox5.Controls("btnPacked" & i).Text = tmpbtnnum.ToString("00")
                            tmpbtnnum = tmpbtnnum + 1
                    End Select
                Next

        End Select

    End Sub

    Public Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try


            'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

            frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            If frmJobEntry.LDS.HasChanges Then
            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try

        Try


            'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

            frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            If frmJobEntry.LDS.HasChanges Then
            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try



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

        frmJobEntry.btnNewPallet.BackColor = Color.LightGray
        frmJobEntry.btnNewPallet.Enabled = True
        frmJobEntry.btnOldPallet.BackColor = Color.LightGray
        frmJobEntry.btnOldPallet.Enabled = True
        frmJobEntry.newJobFlag = 0
        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtDrumNum.Clear()


    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows

        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState







    End Sub

    Public Sub CartTblSave()


        'For CART 
        Dim cAddState As Boolean = frmCartDgv.DGVCart.AllowUserToAddRows

        frmCartDgv.DGVCart.AllowUserToAddRows = True
        frmCartDgv.DGVCart.CurrentCell = frmCartDgv.DGVCart.Rows(frmCartDgv.DGVCart.Rows.Count - 1).Cells(0) ' move to add row
        frmCartDgv.DGVCart.CurrentCell = frmCartDgv.DGVCart.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmCartDgv.DGVCart.AllowUserToAddRows = cAddState



    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.cancelRoutine()
        frmJobEntry.Show()
        Me.Close()
    End Sub

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub
End Class