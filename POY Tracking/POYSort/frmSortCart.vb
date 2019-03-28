Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel


Public Class frmSortCart


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







    Private Sub frmSortCart_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        UpdateImageValues()  'Puts new numbering on the drum Display

        UpdateDrumVal()

        'Update screen Header
        Me.txtCartNum.Text = frmJobEntry.varCartNum
        Me.lblMcNum.Text = frmDGV.DGVdata.Rows(0).Cells("POYMCNAME").Value.ToString
        lblProdName.Text = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value.ToString
        lblTFNum.Text = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value.ToString
        lblDoffNum.Text = frmDGV.DGVdata.Rows(0).Cells("POYDOFFNUM").Value.ToString

        'Show updated count
        toAllocateCount = frmDGV.DGVdata.Rows.Count 'gets total number of Drums to be allocated including missing
        txtScannedDrums.Text = allocatedCount


        'HIDE CLEAR BUTTON WHEN FORM OPENS
        Me.btnClear.Visible = False

        'SET TEXT ON OVERIDE BUTTON TO OFF
        btnUnlock.Text = "UNLOCK OFF"

        'GET TOTAL VALUE OF DRUMS ON CART 
        rowendcount = frmDGV.DGVdata.Rows.Count


        ' SHOW SORT BUTTONS VISIBLE OR NOT 

        Me.btnNoDrum.Visible = True
        Me.btnDefect.Visible = True
        Me.btnShort.Visible = True

        btnFinishedJob.Enabled = True
        btnFinishedJob.BackColor = Color.Green

        'Me.KeyPreview = True  'Allows us to look for advance character from barcode
        Me.KeyPreview = True


        For i = 1 To rowendcount
            If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value > 0 Then allocatedCount += 1
        Next

        txtScannedDrums.Text = allocatedCount

        UpdateDrumVal()
        txtBoxUpdates()

    End Sub

    Public Sub timeUpdate()   'get current time and date

        todayTimeDate = time.Now.ToString(dateFormat)

    End Sub

    Private Sub UpdateDrumVal()

        Dim cellVal As String
        Dim reasonFound As Integer = 0

        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count 'Up to 16 Drum


            'Update Scanned Image
            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value) Then
                cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value.ToString
                'CHECK FOR SCANNED Drum AND SET TO GREEN
                If cellVal = 1 Then
                    Me.Controls("btn" & rw).BackgroundImage = My.Resources.Have_Drum
                    Me.Controls("btn" & rw).Enabled = True
                End If
                cellVal = Nothing
            End If




            'CHECK FOR SHORT AND UPDATE IMAGE
            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then
                cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value
                If cellVal > 0 Then

                    Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortDrum
                    Me.Controls("btn" & rw).Enabled = True
                    shortC(rw) = 1


                    If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                        If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value > 0 Then
                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortWithDefect
                            Me.Controls("btn" & rw).Enabled = True
                            shortC(rw) = 1
                        End If
                    End If

                    cellVal = Nothing

                End If
            End If


            'CHECK FOR MISSING DRUM AND UPDATE IMAGE
            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value) Then

                If frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value > 0 Then
                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value
                    If cellVal > 0 Then

                        Me.Controls("btn" & rw).BackgroundImage = My.Resources.MissingDrum
                        Me.Controls("btn" & rw).Enabled = False

                        cellVal = Nothing
                    End If
                End If
            End If

            'CHECK FOR DEFECT AND UPDATE IMAGE
            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value

                If cellVal > 0 Then

                    Me.Controls("btn" & rw).BackgroundImage = My.Resources.DefectDrum

                    If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then
                        If frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value > 0 Then
                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortWithDefect
                        End If
                    End If

                    cellVal = Nothing
                End If

            End If

        Next





        txtBoxUpdates()


    End Sub

    Public Sub txtBoxUpdates()

        Dim DRUMMissingID As String = Nothing
        Dim DRUMDefectID As String = Nothing
        Dim POYSHORTDRUMID As String = Nothing

        Dim fmt As String = "000"    'FORMAT STRING FOR NUMBER 
        Dim tmpDRUMNum = ""

        txtShort.Text = ""
        txtMissing.Text = ""
        txtDefect.Text = ""




        For rw As Integer = 1 To rowendcount

            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then
                If frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value > 0 Then
                    tmpDRUMNum = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSPINNUM").Value
                    POYSHORTDRUMID = POYSHORTDRUMID & tmpDRUMNum & ","
                    txtShort.Text = POYSHORTDRUMID
                End If
            End If

            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value) Then
                If frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value > 0 Then
                    tmpDRUMNum = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSPINNUM").Value
                    DRUMMissingID = DRUMMissingID & tmpDRUMNum & ","
                    txtMissing.Text = DRUMMissingID
                End If
            End If

            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value) Then
                If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDEFDRUM").Value > 0 Then
                    tmpDRUMNum = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSPINNUM").Value
                    DRUMDefectID = DRUMDefectID & tmpDRUMNum & ","
                    txtDefect.Text = DRUMDefectID
                End If
            End If

            tmpDRUMNum = Nothing
        Next



    End Sub

    Private Sub UpdateImageValues()

        'This section will check machine number and P1 and apply correct drum numbers on screen


        Select Case frmJobEntry.varCartNum
            Case "P1", "P5"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "01"
                Me.Controls("btn2").Text = "02"
                Me.Controls("btn3").Text = "03"
                Me.Controls("btn4").Text = "04"
                Me.Controls("btn5").Text = "09"
                Me.Controls("btn6").Text = "10"
                Me.Controls("btn7").Text = "11"
                Me.Controls("btn8").Text = "12"
                Me.Controls("btn9").Text = "17"
                Me.Controls("btn10").Text = "18"
                Me.Controls("btn11").Text = "19"
                Me.Controls("btn12").Text = "20"
                Me.Controls("btn13").Text = "25"
                Me.Controls("btn14").Text = "26"
                Me.Controls("btn15").Text = "27"
                Me.Controls("btn16").Text = "28"
            Case "P2", "P6"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "05"
                Me.Controls("btn2").Text = "06"
                Me.Controls("btn3").Text = "07"
                Me.Controls("btn4").Text = "08"
                Me.Controls("btn5").Text = "13"
                Me.Controls("btn6").Text = "14"
                Me.Controls("btn7").Text = "15"
                Me.Controls("btn8").Text = "16"
                Me.Controls("btn9").Text = "21"
                Me.Controls("btn10").Text = "22"
                Me.Controls("btn11").Text = "23"
                Me.Controls("btn12").Text = "24"
                Me.Controls("btn13").Text = "29"
                Me.Controls("btn14").Text = "30"
                Me.Controls("btn15").Text = "31"
                Me.Controls("btn16").Text = "32"

            Case "P3", "P7"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "33"
                Me.Controls("btn2").Text = "34"
                Me.Controls("btn3").Text = "35"
                Me.Controls("btn4").Text = "36"
                Me.Controls("btn5").Text = "41"
                Me.Controls("btn6").Text = "42"
                Me.Controls("btn7").Text = "43"
                Me.Controls("btn8").Text = "44"
                Me.Controls("btn9").Text = "49"
                Me.Controls("btn10").Text = "50"
                Me.Controls("btn11").Text = "51"
                Me.Controls("btn12").Text = "52"
                Me.Controls("btn13").Text = "57"
                Me.Controls("btn14").Text = "58"
                Me.Controls("btn15").Text = "59"
                Me.Controls("btn16").Text = "60"

            Case "P4", "P8"
                'Put new Drum numbers on images
                Me.Controls("btn1").Text = "37"
                Me.Controls("btn2").Text = "38"
                Me.Controls("btn3").Text = "39"
                Me.Controls("btn4").Text = "40"
                Me.Controls("btn5").Text = "45"
                Me.Controls("btn6").Text = "46"
                Me.Controls("btn7").Text = "47"
                Me.Controls("btn8").Text = "48"
                Me.Controls("btn9").Text = "53"
                Me.Controls("btn10").Text = "54"
                Me.Controls("btn11").Text = "55"
                Me.Controls("btn12").Text = "56"
                Me.Controls("btn13").Text = "61"
                Me.Controls("btn14").Text = "62"
                Me.Controls("btn15").Text = "63"
                Me.Controls("btn16").Text = "64"
        End Select

        UpdateDrumVal()

    End Sub



    Private Sub prgContinue()


        bcodeScan = txtDrumBcode.Text
        Dim curDRUM As String
        Dim DRUMCount As Integer = 0
        ' Dim today As String = DateAndTime.Now
        timeUpdate()







        For i = 1 To rowendcount



            If frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = 0 Then

                Me.Controls("btn" & i.ToString).BackgroundImage = My.Resources.Have_Drum  'DRUM HAS BEEN SCANNED IN
                frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = 1
                frmDGV.DGVdata.Rows(i - 1).Cells("POYSORTNAME").Value = frmJobEntry.SortOP



                allocatedCount = allocatedCount + 1
                txtScannedDrums.Text = allocatedCount 'SHOW COUNT ON SCREEN
                curDRUM = Nothing

                Exit For


            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = 1 Then
                lblMessage.Visible = True
                lblMessage.Text = "Drum already allocated"
                DelayTM()
                lblMessage.Visible = False
                Exit For



            ElseIf i = rowendcount Then
                lblMessage.Visible = True
                lblMessage.Text = ("CHECK Drum BARCODE" & vbCrLf & "    WRONG NUMBER")
                DelayTM()
                lblMessage.Visible = False
                'Exit For


            End If


            txtDrumBcode.Clear()
            txtDrumBcode.Focus()
            txtDrumBcode.Refresh()

        Next


        txtDrumBcode.Clear()
        txtDrumBcode.Focus()
        txtDrumBcode.Refresh()
        UpdateDrumVal()
        'endCheck()

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

    '===========================================================  BUTTON CONTROL ================================================
    Private Sub btnUnlock_Click(sender As Object, e As EventArgs) Handles btnUnlock.Click

        'SET ALL DRUM KEYS TO EDIT
        Unlock()

    End Sub

    Private Sub Unlock()

        If btnUnlock.Text = "UNLOCK OFF" Then     'ENABLE ALL LOCKED BUTTONS
            btnUnlock.Text = "UNLOCK ON"
            btnUnlock.ForeColor = Color.Green

            For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count

                Me.Controls("btn" & rw).Enabled = True
                btnDelete.Visible = True
            Next

        ElseIf btnUnlock.Text = "UNLOCK ON" Then  'LOCK ALL LOCKED BUTTONS
            btnUnlock.Text = "UNLOCK OFF"
            btnUnlock.ForeColor = Color.Red
            btnDelete.Visible = False
        End If


    End Sub


    Private Sub btnNoDrum_Click(sender As Object, e As EventArgs) Handles btnNoDrum.Click
        If varDRUMNum > 0 Then

            NoDRUM = 1
            Me.btnShort.Enabled = False
            Me.btnNoDrum.Enabled = False
            Me.btnDefect.Enabled = False
            Me.btnDefectSave.Visible = True
            Me.btnClear.Visible = True
            shortC(varDRUMNum) = Nothing
            defect = Nothing

            'FAULTS FROM POY-DTY Dept
            Me.chk_DAB.Visible = False
            Me.chk_FG.Visible = False
            Me.chk_O.Visible = False
            Me.chk_SL.Visible = False
            Me.chk_PTS.Visible = False
            Me.chk_PTB.Visible = False
            Me.chk_YAB.Visible = False
            Me.chk_CAB.Visible = False
            Me.chk_RW.Visible = False
            Me.chk_PAB.Visible = False
            Me.chk_DO.Visible = False
            Me.chk_CNC.Visible = False
            Me.chk_H.Visible = False
            Me.chk_CBC.Visible = False

            reFocus()

        Else
            MsgBox("You must select a Drum number first")
            reFocus()
        End If

    End Sub

    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click


        Dim fltDrow = (varDRUMNum) - 1






        If varDRUMNum > 0 Then

            'If frmDGV.DGVdata.Rows(fltDrow).Cells("POYDRUMSTATE").Value = 0 Then
            '    MsgBox("Please scan Drum first")
            '    reFocus()
            '    Exit Sub
            'End If


            defect = 1
            Me.Controls("btn" & varDRUMNum).BackgroundImage = My.Resources.DefectDrum     'Yellow Strip

            Me.btnDefectSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens
            Me.btnDefect.Enabled = False
            Me.btnNoDrum.Visible = False
            Me.btnShort.Visible = False
            Me.btnNoDrum.Enabled = False
            Me.btnShort.Enabled = False

            'FAULTS FROM POY-DTY Dept
            Me.lblFaultCodes.Visible = True
            Me.chk_DAB.Visible = True
            Me.chk_FG.Visible = True
            Me.chk_O.Visible = True
            Me.chk_SL.Visible = True
            Me.chk_PTS.Visible = True
            Me.chk_PTB.Visible = True
            Me.chk_YAB.Visible = True

            Me.chk_CAB.Visible = True
            Me.chk_RW.Visible = True
            Me.chk_PAB.Visible = True
            Me.chk_DO.Visible = True
            Me.chk_CNC.Visible = True
            Me.chk_H.Visible = True
            Me.chk_CBC.Visible = True





            'THIS WILL CALL BACK THE FAULT DATA FROM THE DATAGRID
            'FAULTS FROM POY-DTY Dept
            chk_DAB.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_DAB").Value.ToString
            chk_FG.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_FG").Value.ToString
            chk_O.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_O").Value.ToString
            chk_SL.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_SL").Value.ToString
            chk_PTS.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_PTS").Value.ToString
            chk_PTB.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_PTB").Value.ToString

            chk_YAB.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_YAB").Value.ToString
            chk_CAB.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_CAB").Value.ToString
            chk_RW.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_RW").Value.ToString
            chk_PAB.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_PAB").Value.ToString
            chk_DO.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_DO").Value.ToString
            chk_CNC.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_CNC").Value.ToString
            chk_H.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_H").Value.ToString
            chk_CBC.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_CBC").Value.ToString



            Me.btnDefectSave.Visible = True 'Show Save button when form opens

            reFocus()
        Else
            MsgBox("You must select a Drum number first")
            reFocus()
        End If


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If varDRUMNum > 0 Then



            Dim result = MessageBox.Show("ERASE Information for Drum #" & varDRUMNum, "ERASE Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then
                Dim result2 = MessageBox.Show("ARE YOU SURE YOU WANT TO ERASE Drum #" & varDRUMNum & "  INFORMATION", "CONFIRM ERASE", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If result2 = DialogResult.Yes Then
                    'ERASE DRUM VALUES

                    frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSHORTDRUM").Value = Nothing 'POYSHORTDRUM

                    If Not IsDBNull(frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value) Then
                        If frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value > 0 Then
                            frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_X").Value = "False"
                        End If
                    End If

                    frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value = Nothing 'missingDRUM
                    frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDEFDRUM").Value = Nothing 'defectDRUM
                    frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDRUMSTATE").Value = 0
                        'FAULTS FROM POY-DTY Dept
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_DAB").Value = "False"   'KEBA Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_FG").Value = "False"  'DIRTY Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_O").Value = "False"     'FORM AB Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_SL").Value = "False"     'OVERTHROWN Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_PTS").Value = "False"    'TENSION AB. Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_PTB").Value = "False"    'PAPER TUBE AB. Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_YAB").Value = "False"          'SHORT Drum Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_CAB").Value = "False"          'No HAVE Drum Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_RW").Value = "False"     'NO TAIL & ABNORMAL Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_PAB").Value = "False"   'WASTE Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_DO").Value = "False"    'HITTING Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_CNC").Value = "False"    'TARUMI Fault  TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_H").Value = "False"    'B- GRADE BY M/C  Fault   TODO
                        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_CBC").Value = "False"    'C- GRADE BY M/C  Fault  TODO



                        shortC(varDRUMNum) = Nothing
                        ' Me.Controls("btn" & varDRUMNum).BackColor = SystemColors.ControlDark
                        Me.Controls("btn" & varDRUMNum).BackgroundImage = My.Resources.NotScan

                        'Reduce allocated by 1 and increase to allocate by 1
                        If allocatedCount > 0 Then allocatedCount = allocatedCount - 1
                        txtScannedDrums.Text = allocatedCount 'SHOW COUNT ON SCREEN


                        UpdateDrumVal()


                        varDRUMNum = Nothing
                        txtDrumNum.Text = ""



                        btnUnlock.Text = "UNLOCK OFF"
                        btnUnlock.ForeColor = Color.Red
                        btnDelete.Visible = False
                        reFocus()
                        Exit Sub
                    End If

                    If result2 = DialogResult.No Then
                    varDRUMNum = Nothing
                    txtDrumNum.Text = ""
                    reFocus()
                    Exit Sub
                End If
            End If

            If result = DialogResult.No Then
                varDRUMNum = Nothing
                txtDrumNum.Text = ""
                reFocus()
                Exit Sub
            End If
        Else
            MsgBox("You must select a Drum number first")
            reFocus()
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If varDRUMNum > 0 Then

            NoDRUM = Nothing
            defect = Nothing
            POYSHORTDRUM = Nothing
            varDRUMNum = Nothing

            txtDrumNum.Text = ""



            Me.btnShort.Visible = True
            Me.btnShort.Enabled = True

            Me.btnNoDrum.Visible = True
            Me.btnNoDrum.Enabled = True
            Me.btnDefect.Enabled = True
            ' Me.btnSave.Visible = False
            Me.btnDefectSave.Visible = False



            'FAULTS FROM POY Dept
            Me.btnClear.Visible = False
            Me.chk_DAB.Visible = False
            Me.chk_FG.Visible = False
            Me.chk_O.Visible = False
            Me.chk_SL.Visible = False
            Me.chk_PTS.Visible = False
            Me.chk_PTB.Visible = False
            Me.chk_YAB.Visible = False
            Me.chk_CAB.Visible = False
            Me.chk_RW.Visible = False
            Me.chk_PAB.Visible = False
            Me.chk_DO.Visible = False
            Me.chk_CNC.Visible = False
            Me.chk_H.Visible = False
            Me.chk_CBC.Visible = False
            reFocus()
        Else
            MsgBox("You must select a  Drum number first")
            reFocus()
        End If
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click


        frmJobEntry.txtCartNum.Clear()
        frmJobEntry.txtCartNum.Focus()
        frmJobEntry.txtCartNum.Refresh()

        Me.Close()
        frmJobEntry.Show()
    End Sub



    Private Sub btnShort_Click(sender As Object, e As EventArgs) Handles btnShort.Click



        If varDRUMNum > 0 Then




            POYSHORTDRUM = 2



            Me.btnDefectSave.Visible = True 'Show Save button when form opens
                Me.btnClear.Visible = True  'Show Cancel button when form opens
                Me.btnDefect.Enabled = False
                Me.btnNoDrum.Enabled = False
                Me.btnShort.Enabled = False


                reFocus()
            Else
                MsgBox("You must select a  Drum number first")
            reFocus()
        End If

    End Sub



    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click

        varDRUMNum = "1"

        txtDrumNum.Text = btn1.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click

        varDRUMNum = "2"              'Sets the cone Number
        txtDrumNum.Text = btn2.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        varDRUMNum = "3"               'Sets the cone Number
        txtDrumNum.Text = btn3.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        varDRUMNum = "4"             'Sets the cone Number
        txtDrumNum.Text = btn4.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        varDRUMNum = "5"               'Sets the cone Number
        txtDrumNum.Text = btn5.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        varDRUMNum = "6"              'Sets the cone Number
        txtDrumNum.Text = btn6.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click
        varDRUMNum = "7"               'Sets the cone Number
        txtDrumNum.Text = btn7.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        varDRUMNum = "8"               'Sets the cone Number
        txtDrumNum.Text = btn8.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click
        varDRUMNum = "9"            'Sets the cone Number
        txtDrumNum.Text = btn9.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn10_Click(sender As Object, e As EventArgs) Handles btn10.Click
        varDRUMNum = "10"           'Sets the cone Number
        txtDrumNum.Text = btn10.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn11_Click(sender As Object, e As EventArgs) Handles btn11.Click
        varDRUMNum = "11"        'Sets the cone Number
        txtDrumNum.Text = btn11.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn12_Click(sender As Object, e As EventArgs) Handles btn12.Click
        varDRUMNum = "12"       'Sets the cone Number
        txtDrumNum.Text = btn12.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn13_Click(sender As Object, e As EventArgs) Handles btn13.Click
        varDRUMNum = "13"            'Sets the cone Number
        txtDrumNum.Text = btn13.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn14_Click(sender As Object, e As EventArgs) Handles btn14.Click
        varDRUMNum = "14"           'Sets the cone Number
        txtDrumNum.Text = btn14.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn15_Click(sender As Object, e As EventArgs) Handles btn15.Click
        varDRUMNum = "15"     'Sets the cone Number
        txtDrumNum.Text = btn15.Text

        Me.txtDrumNum.Refresh()
    End Sub

    Private Sub btn16_Click(sender As Object, e As EventArgs) Handles btn16.Click
        varDRUMNum = "16"         'Sets the cone Number
        txtDrumNum.Text = btn16.Text

        Me.txtDrumNum.Refresh()
    End Sub






    Private Sub btnNoDRUMSave_Click(sender As Object, e As EventArgs)

        readsave()
    End Sub

    Private Sub btnDefectSave_Click(sender As Object, e As EventArgs) Handles btnDefectSave.Click


        'FAULTS FROM POY Dept
        Me.lblFaultCodes.Visible = False
        Me.btnClear.Visible = False
        Me.chk_DAB.Visible = False
        Me.chk_FG.Visible = False
        Me.chk_O.Visible = False
        Me.chk_SL.Visible = False
        Me.chk_PTS.Visible = False
        Me.chk_PTB.Visible = False
        Me.chk_YAB.Visible = False
        Me.chk_CAB.Visible = False
        Me.chk_RW.Visible = False
        Me.chk_PAB.Visible = False
        Me.chk_DO.Visible = False
        Me.chk_CNC.Visible = False
        Me.chk_H.Visible = False
        Me.chk_CBC.Visible = False



        If btnUnlock.Text = "UNLOCK ON" Then
            btnUnlock.Text = "UNLOCK OFF"
            btnUnlock.ForeColor = Color.Red
            btnDelete.Visible = False

        End If
        reFocus()
        readsave()
    End Sub

    Private Sub btnShortSave_Click(sender As Object, e As EventArgs)

        reFocus()
        readsave()

    End Sub

    Private Sub readsave()

        'NO CONE Update Cone button to colour of nodrum And add the cone number to the coneMissingID string so we have a full list of missing cones
        If NoDRUM Then

            Fault_X = True  'Sets the nodrum fault flag
            If varDRUMNum = 1 Then
                btn1.Enabled = False
                btn1.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 2 Then
                btn2.Enabled = False
                btn2.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 3 Then
                btn3.Enabled = False
                btn3.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 4 Then
                btn4.Enabled = False
                btn4.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 5 Then
                btn5.Enabled = False
                btn5.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 6 Then
                btn6.Enabled = False
                btn6.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 7 Then
                btn7.Enabled = False
                btn7.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 8 Then
                btn8.Enabled = False
                btn8.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 9 Then
                btn9.Enabled = False
                btn9.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 10 Then
                btn10.Enabled = False
                btn10.BackgroundImage = My.Resources.NoDrum
            ElseIf varDRUMNum = 11 Then
                btn11.Enabled = False
                btn11.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 12 Then
                btn12.Enabled = False
                btn12.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 13 Then
                btn13.Enabled = False
                btn13.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 14 Then
                btn14.Enabled = False
                btn14.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 15 Then
                btn15.Enabled = False
                btn15.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text
            ElseIf varDRUMNum = 16 Then
                btn16.Enabled = False
                btn16.BackgroundImage = My.Resources.NoDrum
                NoDRUM = txtDrumNum.Text

            End If
            frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value = 1
            allocatedCount = allocatedCount + 1
            txtScannedDrums.Text = allocatedCount 'SHOW COUNT ON SCREEN
        End If

        If defect Then  'Routine to Set Cone color to defect and update cone numbers with defects
            frmDGV.DGVdata.Rows((varDRUMNum - 1)).Cells("POYDEFDRUM").Value = txtDrumNum.Text 'POYDEFDRUM
            frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value = 0
            Fault_X = False

            If varDRUMNum = 1 Then
                btn1.Enabled = False
                btn1.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 2 Then
                btn2.Enabled = False
                btn2.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 3 Then
                btn3.Enabled = False
                btn3.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 4 Then
                btn4.Enabled = False
                btn4.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 5 Then
                btn5.Enabled = False
                btn5.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 6 Then
                btn6.Enabled = False
                btn6.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 7 Then
                btn7.Enabled = False
                btn7.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 8 Then
                btn8.Enabled = False
                btn8.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 9 Then
                btn9.Enabled = False
                btn9.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 10 Then
                btn10.Enabled = False
                btn10.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 11 Then
                btn11.Enabled = False
                btn11.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 12 Then
                btn12.Enabled = False
                btn12.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 13 Then
                btn13.Enabled = False
                btn13.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 14 Then
                btn14.Enabled = False
                btn14.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 15 Then
                btn15.Enabled = False
                btn15.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text
            ElseIf varDRUMNum = 16 Then
                btn16.Enabled = False
                btn16.BackgroundImage = My.Resources.DefectDrum
                defect = txtDrumNum.Text

            End If

            If frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDRUMSTATE").Value = 0 Then
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDRUMSTATE").Value = 1
            End If
        End If

        If POYSHORTDRUM Then    'THIS IS THE SHORT CONE TEMP UPDATE ALL OTHER CONES ARE FINISHED WHEN SAVED BUT SHORT CONE NEEDS A TEMP UPDATE TO WORK FOR SORTING DEPT

            frmDGV.DGVdata.Rows((varDRUMNum - 1)).Cells("POYSHORTDRUM").Value = txtDrumNum.Text 'POYSHORTDRUM
            frmDGV.DGVdata.Rows((varDRUMNum - 1)).Cells("FLT_S").Value = "True" 'Sets the SHORT fault flag
            frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value = 0
            Fault_X = False
            ' txtBoxUpdates()

            If varDRUMNum = 1 Then
                btn1.Enabled = True
                btn1.BackgroundImage = My.Resources.ShortDrum
                shortC(1) = 1
            ElseIf varDRUMNum = 2 Then
                btn2.Enabled = True
                btn2.BackgroundImage = My.Resources.ShortDrum
                shortC(2) = 1
            ElseIf varDRUMNum = 3 Then
                btn3.Enabled = True
                btn3.BackgroundImage = My.Resources.ShortDrum
                shortC(3) = 1
            ElseIf varDRUMNum = 4 Then
                btn4.Enabled = True
                btn4.BackgroundImage = My.Resources.ShortDrum
                shortC(4) = 1
            ElseIf varDRUMNum = 5 Then
                btn5.Enabled = True
                btn5.BackgroundImage = My.Resources.ShortDrum
                shortC(5) = 1
            ElseIf varDRUMNum = 6 Then
                btn6.Enabled = True
                btn6.BackgroundImage = My.Resources.ShortDrum
                shortC(6) = 1
            ElseIf varDRUMNum = 7 Then
                btn7.Enabled = True
                btn7.BackgroundImage = My.Resources.ShortDrum
                shortC(7) = 1
            ElseIf varDRUMNum = 8 Then
                btn8.Enabled = True
                btn8.BackgroundImage = My.Resources.ShortDrum
                shortC(8) = 1
            ElseIf varDRUMNum = 9 Then
                btn9.Enabled = True
                btn9.BackgroundImage = My.Resources.ShortDrum
                shortC(9) = 1
            ElseIf varDRUMNum = 10 Then
                btn10.Enabled = True
                btn10.BackgroundImage = My.Resources.ShortDrum
                shortC(10) = 1
            ElseIf varDRUMNum = 11 Then
                btn11.Enabled = True
                btn11.BackgroundImage = My.Resources.ShortDrum
                shortC(11) = 1
            ElseIf varDRUMNum = 12 Then
                btn12.Enabled = True
                btn12.BackgroundImage = My.Resources.ShortDrum
                shortC(12) = 1
            ElseIf varDRUMNum = 13 Then
                btn13.Enabled = True
                btn13.BackgroundImage = My.Resources.ShortDrum
                shortC(13) = 1
            ElseIf varDRUMNum = 14 Then
                btn14.Enabled = True
                btn14.BackgroundImage = My.Resources.ShortDrum
                shortC(14) = 1
            ElseIf varDRUMNum = 15 Then
                btn15.Enabled = True
                btn15.BackgroundImage = My.Resources.ShortDrum
                shortC(15) = 1
            ElseIf varDRUMNum = 16 Then
                btn16.Enabled = True
                btn16.BackgroundImage = My.Resources.ShortDrum
                shortC(16) = 1

            End If


            If frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDRUMSTATE").Value = 0 Then
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDRUMSTATE").Value = 1
            End If
        End If


        Me.btnShort.Visible = True
        Me.btnShort.Enabled = True
        Me.btnNoDrum.Visible = True
        Me.btnNoDrum.Enabled = True
        Me.btnDefect.Enabled = True
        ' Me.btnSave.Visible = False
        Me.btnDefectSave.Visible = False
        Me.btnClear.Visible = False
        DRUMCount = DRUMCount + 1  'if Short being set do not add to DRUM count







        'If POYSHORTDRUM = 1 Then
        '    NoDRUM = Nothing
        '    defect = Nothing
        '    POYSHORTDRUM = Nothing
        '    varDRUMNum = Nothing
        '    txtDrumNum.Text = ""


        'Else
        varCartEndTime = time.ToString(dateFormat)
            If POYSHORTDRUM = 2 Then POYSHORTDRUM = txtDrumNum.Text
        If POYSHORTDRUM > 0 Then Fault_S = "True"

        txtBoxUpdates()
        jobArrayUpdate()


            NoDRUM = Nothing
            defect = Nothing
            POYSHORTDRUM = Nothing
            varDRUMNum = Nothing
            BackgroundImage = Nothing
            'FAULTS FROM POY Dept
            Me.btnClear.Visible = False
            Me.chk_DAB.Visible = False
            Me.chk_FG.Visible = False
            Me.chk_O.Visible = False
            Me.chk_SL.Visible = False
            Me.chk_PTS.Visible = False
            Me.chk_PTB.Visible = False
            Me.chk_YAB.Visible = False
            Me.chk_CAB.Visible = False
            Me.chk_RW.Visible = False
            Me.chk_PAB.Visible = False
            Me.chk_DO.Visible = False
            Me.chk_CNC.Visible = False
            Me.chk_H.Visible = False
            Me.chk_CBC.Visible = False
            Fault_S = "False"
            Fault_X = "False"
            ''SORT Dept FAULTS
            'Me.chk_DO.Visible = False
            'Me.chk_DH.Visible = False
            'Me.chk_CL.Visible = False
            'Me.chk_FI.Visible = False
            'Me.chk_YN.Visible = False
            'Me.chk_HT.Visible = False
            'Me.chk_LT.Visible = False


            txtDrumNum.Text = ""

        'End If

    End Sub



    Private Sub btnFinishedJob_Click(sender As Object, e As EventArgs) Handles btnFinishedJob.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        endJob()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ END OF BUTTON SECTION ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Private Sub jobArrayUpdate()

        timeUpdate()

        varCartEndTime = todayTimeDate



        If IsDBNull(frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSORTENDTM").Value) Then
            'For i As Integer = 1 To frmDGV.DGVdata.Rows.Count
            frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSORTENDTM").Value = todayTimeDate  'CREATE END TIME
            ' Next
        End If


        'list of Array Feilds to Update

        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSORTNAME").Value = frmJobEntry.SortOP   'operatorName   fron entry screen



        If Not IsDBNull(frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSHORTDRUM").Value) Then
            If frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSHORTDRUM").Value > 0 And POYSHORTDRUM > 0 Then
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSHORTDRUM").Value = POYSHORTDRUM  'shortCone
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_S").Value = Fault_S       'SHORT Drum Fault
            End If
        End If


        If Not IsDBNull(frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value) Then
            If frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value > 0 And NoDRUM > 0 Then
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYMISSDRUM").Value = NoDRUM  'NoDrum
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSHORTDRUM").Value = 0
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDEFDRUM").Value = 0
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_X").Value = True

            End If
        End If

        If Not IsDBNull(frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDEFDRUM").Value) Then
            If frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDEFDRUM").Value > 0 And defect > 0 Then
                frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYDEFDRUM").Value = defect  'defect drum
            End If
        End If





        'frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("CARTSTARTTM").Value = varCartStartTime  'cartStratTime
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("POYSORTENDTM").Value = varCartEndTime 'cartEndTime

        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_DAB").Value = chk_DAB.Checked      'DRUM FORM AB
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_FG").Value = chk_FG.Checked     'FLUFF GUIDE
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_O").Value = chk_O.Checked        'OVER THROWN
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_SL").Value = chk_SL.Checked      'SPIRAL LOOP
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_PTS").Value = chk_PTS.Checked        'P.T. SCRATCH
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_PTB").Value = chk_PTB.Checked       ' P.T. BURST



        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_X").Value = Fault_X      'MISSING
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_YAB").Value = chk_YAB.Checked        'YARN AB
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_CAB").Value = chk_CAB.Checked       ' COLOUR AB
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_RW").Value = chk_RW.Checked      'RIBBON WINDING
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_PAB").Value = chk_PAB.Checked        'PUSHER AB
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_DO").Value = chk_DO.Checked      'DIRTY OIL
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_CNC").Value = chk_CNC.Checked        'CUTTER NOT CUT
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_H").Value = chk_H.Checked        'HITTING
        frmDGV.DGVdata.Rows(varDRUMNum - 1).Cells("FLT_CBC").Value = chk_CBC.Checked        'CUTTING BY CUTTER


        UpdateDrumVal()


    End Sub




    Private Sub endJob()

        'UPDATE DATABASE WITH CHANGES



        'ONLY PRINT IF COLOUR SELECTED

        timeUpdate()

        Dim tempdrumnum As String

        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count


            If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = 0 Then

                'get current cone number
                tempdrumnum = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSPINNUM").Value

                frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value = tempdrumnum
                frmDGV.DGVdata.Rows(rw - 1).Cells("FLT_X").Value = True

                frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = 1

            End If


            frmDGV.DGVdata.Rows(rw - 1).Cells("POYSORTNAME").Value = frmJobEntry.SortOP
            frmDGV.DGVdata.Rows(rw - 1).Cells("POYSORTENDTM").Value = todayTimeDate



        Next

        UpdateDatabase()

        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.Show()
        frmJobEntry.txtCartNum.Clear()
        frmJobEntry.txtCartNum.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()

    End Sub


    '







    Public Sub endCheck()

        'If toAllocateCount = allocatedCount Then
        'ONLY PRINT IF COLOUR SELECTED
        ' Dim today As String = DateAndTime.Today
        timeUpdate()

            Dim tempDRUMnum As String

            For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count


                If frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = 0 Then

                    'get current DRUM number
                    tempDRUMnum = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSPINNUM").Value

                    frmDGV.DGVdata.Rows(rw - 1).Cells("POYMISSDRUM").Value = tempDRUMnum

                    frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value = 1

                    'frmDGV.DGVdata.Rows(rw - 1).Cells("POYSORTENDTM").Value = todayTimeDate
                End If


                frmDGV.DGVdata.Rows(rw - 1).Cells("OPCREATECART").Value = frmJobEntry.SortOP
                frmDGV.DGVdata.Rows(rw - 1).Cells("POYSORTENDTM").Value = todayTimeDate



            Next

            UpdateDatabase()

        'End If
        btnFinishedJob.Enabled = True
        btnFinishedJob.BackColor = Color.Green

    End Sub

    Public Sub tsbtnSave()

        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
        'Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState


    End Sub

    Private Sub UpdateDatabase()

        tsbtnSave()


        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If frmJobEntry.LDS.HasChanges Then

                frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try


    End Sub





    Private Sub frmCartRead_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown


        If e.KeyCode = Keys.Return Then prgContinue()

    End Sub



    Private Sub reFocus()
        txtDrumBcode.Clear()
        txtDrumBcode.Focus()
        txtDrumBcode.Refresh()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        frmJobEntry.Show()
        frmJobEntry.txtCartNum.Clear()
        frmJobEntry.txtCartNum.Focus()

        Me.Close()
    End Sub
End Class