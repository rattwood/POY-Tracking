
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




    Private Sub frmUniversalPacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblCartNum.Text = frmJobEntry.varCartNum

        'Check to see if this is existing Pallet or new Pallet


        UpdateImageValues()

        updatePackGrid()




    End Sub

    Private Sub POYPaletteCreate()

        ' RESET QUERY STATISTCIS
        'RecordCount = 0
        'LException = ""
        'If LConn.State = ConnectionState.Open Then LConn.Close()

        Dim varProdGrade
        frmJobEntry.timeUpdate()


        'THIS IS FOR THE PRINTED REPORT
        LExecQuery("Select * FROM POYPRODUCT WHERE POYPRNUM = '" & frmJobEntry.varProductCode & "' ")
        If LRecordCount > 0 Then
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True

            frmJobEntry.varProductName = frmDGV.DGVdata.Rows(0).Cells("POYPRNAME").Value
            frmJobEntry.mergeNum = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value

            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value) Then
                varProdGrade = frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value.ToString

            Else
                varProdGrade = "NA"
            End If




            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value) Then
                frmJobEntry.varProdWeight = frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value
                frmJobEntry.varProdWeight = frmJobEntry.varProdWeight / 100
            Else
                frmJobEntry.varProdWeight = "0.00"
            End If

            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYWEIGHTCODE").Value) Then
                frmJobEntry.varKNum = frmDGV.DGVdata.Rows(0).Cells("POYWEIGHTCODE").Value
            Else
                frmJobEntry.varKNum = "K00"
            End If




        Else
            'MsgBox("This product is not in Product table, please check product table in SETTINGS ")
            If frmJobEntry.thaiLang Then MsgBox("โปรดักส์นี้ไม่มีในตารางสินค้า กรุณาตรวจสอบตารางสินค้าในการตั้งค่า") Else _
                MsgBox("This product is not in Product table, please check product table in SETTINGS ")
            frmJobEntry.Show()
            frmJobEntry.cancelRoutine()
            Me.Close()
            Exit Sub

        End If


        If frmJobEntry.thaiLang Then
            Label3.Text = "สร้างพาเลทใหม่"
            Label3.Visible = True
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Else
            Label3.Text = "Creating New Pallet"
            Label3.Visible = True
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        End If




        Dim fmt As String = "000"
        Dim modIdxNum As String
        Dim tmpToday As String = Date.Now.ToString("yyMMdd")


        Dim tmpTraceNum As String = frmJobEntry.varProductCode & tmpToday & "_" & frmJobEntry.drumPerPal



        For i As Integer = 1 To frmJobEntry.drumPerPal

            modIdxNum = i.ToString(fmt) 'Format the index number to three digits

            'moddrumNum = i.ToString(fmt)   ' FORMATS THE drum NUMBER TO 3 DIGITS
            '  drumBarcode = modLotStr & moddrumNum   'CREATE THE drum BARCODE NUMBER
            '  JobBarcode = modLotStr

            'Parameters List for full db

            'ADD SQL PARAMETERS & RUN THE COMMAND

            LAddParam("@poypackidx", modIdxNum)
            LAddParam("@poystationnum", My.Settings.packStationID)
            LAddParam("@poydrumperpal", frmJobEntry.drumPerPal)
            LAddParam("@poytmptracenum", tmpTraceNum)



            LExecQuery("INSERT INTO POYPackTrace (POYPACKIDX, POYPACKSTATION, POYDRUMPERPAL, POYTMPTRACENUM ) " _
                   & " VALUES (@poypackidx,@poystationnum, @poydrumperpal,@poytmptracenum ) ")


        Next



        LExecQuery("Select * FROM PoyTrack WHERE POYTMPTRACE = '" & frmJobEntry.dbBarcode & "' ORDER BY POYPACKIDX")

        Try
            If LRecordCount > 0 Then
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

                Me.Cursor = System.Windows.Forms.Cursors.Default
                Label3.Text = ""
                Label3.Visible = False
            End If
        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ' MsgBox("Job creation Error" & vbNewLine & ex.Message)
            If frmJobEntry.thaiLang Then MsgBox("สร้างงานผิดพลาด " & vbNewLine & ex.Message) Else _
              MsgBox("Job creation Error" & vbNewLine & ex.Message)
            writeerrorLog.writelog("ExecQuery Error:", ex.Message, False, "System_Fault")
            writeerrorLog.writelog("ExecQuery Error:", ex.ToString, False, "System_Fault")
        End Try






    End Sub





    Private Sub UpdateImageValues()

        'This section will check machine number and P1 and apply correct drum numbers on screen


        Select Case frmJobEntry.varCartNum
            Case "P1"
                'Put new Drum numbers on images

                Me.btn1.Text = "01"
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
        'UpdateDrumVal()

    End Sub

    Private Sub UpdateDrumVal()

        Dim cellVal As String
        Dim reasonFound As Integer = 0


        Try
            For rw As Integer = 1 To 16 '16 Drum on each cart


                'Update Scanned Image
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value) Then

                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYDRUMSTATE").Value.ToString
                    'CHECK FOR SCANNED Drum AND SET TO GREEN
                    If cellVal = 3 Then
                        Me.Controls("btn" & rw).BackgroundImage = My.Resources.Have_Drum
                        Me.Controls("btn" & rw).Enabled = True
                    ElseIf cellVal = 15 Then
                        Me.Controls("btn" & rw).BackgroundImage = My.Resources.Packed_Drum
                        Me.Controls("btn" & rw).Enabled = False
                    End If
                    cellVal = Nothing
                End If




                'CHECK FOR SHORT AND UPDATE IMAGE
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value) Then

                    If frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value > 0 Then
                        cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("POYSHORTDRUM").Value

                        If cellVal = 3 Then

                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.ShortDrum
                            Me.Controls("btn" & rw).Enabled = True
                            shortC(rw) = 1
                        End If

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

                            Me.Controls("btn" & rw).BackgroundImage = My.Resources.NoDrum
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
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


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

        Dim tmpPalletDrums = frmJobEntry.drumPerPal
        Dim tmpButton As Button

        If tmpPalletDrums = 48 Then
            For i As Integer = 1 To 120
                If i > 8 AndAlso i < 21 Then
                    btnPacked9.Visible = False
                    btnPacked10.Visible = False
                    btnPacked11.Visible = False
                    btnPacked12.Visible = False
                    btnPacked13.Visible = False
                    btnPacked14.Visible = False
                    btnPacked15.Visible = False
                    btnPacked16.Visible = False
                    btnPacked17.Visible = False
                    btnPacked18.Visible = False
                    btnPacked19.Visible = False
                    btnPacked20.Visible = False


                    'ElseIf i >= 29 AndAlso i <= 40 Then 'Hide
                    '    Controls("btnPacked" & i.ToString).Visible = False
                    'ElseIf i >= 49 AndAlso i <= 60 Then 'Hide
                    '    Controls("btnPacked" & i.ToString).Visible = False
                    'ElseIf i >= 69 AndAlso i <= 80 Then 'Hide
                    '    Controls("btnPacked" & i.ToString).Visible = False
                    'ElseIf i >= 89 AndAlso i <= 100 Then 'hide
                    '    Controls("btnPacked" & i.ToString).Visible = False
                    'ElseIf i >= 109 AndAlso i <= 120 Then 'hide
                    '    Controls("btnPacked" & i.ToString).Visible = False
                End If
            Next

            Dim tmpbtnnum As Integer = 1

            For i = 1 To 108

                If i > 0 AndAlso i < 9 Then
                    Me.Controls("btnPacked" & i.ToString).Text = tmpbtnnum.ToString("00")
                    tmpbtnnum = tmpbtnnum + 1
                ElseIf i > 20 AndAlso i < 29 Then 'show and re number
                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                    tmpbtnnum = tmpbtnnum + 1
                ElseIf i > 40 AndAlso i < 49 Then 'show and re number
                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                    tmpbtnnum = tmpbtnnum + 1
                ElseIf i > 60 AndAlso i < 69 Then 'show and re number
                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                    tmpbtnnum = tmpbtnnum + 1
                ElseIf i > 80 AndAlso i < 89 Then 'show and re number
                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                    tmpbtnnum = tmpbtnnum + 1
                ElseIf i >= 100 AndAlso i <= 109 Then 'show and re number
                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
                    tmpbtnnum = tmpbtnnum + 1
                End If
            Next

        End If













        'Select Case tmpPalletDrums

        '    Case 48
        '        'Hide unwanted drum locations
        '        For i = 1 To 120
        '            Select Case i
        '                Case 9 To 20 'Hide
        '                    btnPacked9.Visible = False

        '                    ' tmpButton = "btnPacked" & i.ToString
        '                    ' Controls(tmpButton).Visible = False
        '                    'Case 29 To 40 'Hide
        '                    '    Controls("btnPacked" & i.ToString).Visible = False
        '                    'Case 49 To 60 'Hide
        '                    '    Controls("btnPacked" & i.ToString).Visible = False
        '                    'Case 69 To 80 'Hide
        '                    '    Controls("btnPacked" & i.ToString).Visible = False
        '                    'Case 89 To 100 'hide
        '                    '    Controls("btnPacked" & i.ToString).Visible = False
        '                    'Case 109 To 120 'hide
        '                    '    Controls("btnPacked" & i.ToString).Visible = False

        '            End Select
        '        Next

        '        Dim tmpbtnnum As Integer = 1

        '        For i = 1 To 108
        '            Select Case i
        '                Case 1 To 8
        '                    Me.Controls("btnPacked" & i.ToString).Text = tmpbtnnum.ToString("00")
        '                    tmpbtnnum = tmpbtnnum + 1
        '                    'Case 21 To 28 'show and re number
        '                    '    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    '    tmpbtnnum = tmpbtnnum + 1
        '                    'Case 41 To 48 'show and re number
        '                    '    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    '    tmpbtnnum = tmpbtnnum + 1
        '                    'Case 61 To 68 'show and re number
        '                    '    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    '    tmpbtnnum = tmpbtnnum + 1
        '                    'Case 81 To 88 'show and re number
        '                    '    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    '    tmpbtnnum = tmpbtnnum + 1
        '                    'Case 101 To 108 'show and re number
        '                    '    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    '    tmpbtnnum = tmpbtnnum + 1
        '            End Select
        '        Next

        '    Case 72
        '        'Hide unwanted drum locations
        '        For i = 1 To 120
        '            Select Case i
        '                Case 16 To 20 'Hide
        '                    Me.Controls("btn" & i).Visible = False
        '                Case 36 To 40 'Hide
        '                    Me.Controls("btn" & i).Visible = False
        '                Case 53 To 60 'Hide
        '                    Me.Controls("btn" & i).Visible = False
        '                Case 73 To 80 'Hide
        '                    Me.Controls("btn" & i).Visible = False
        '                Case 93 To 100 'hide
        '                    Me.Controls("btn" & i).Visible = False
        '                Case 113 To 120 'hide
        '                    Me.Controls("btn" & i).Visible = False
        '            End Select
        '        Next

        '        Dim tmpbtnnum As Integer = 1

        '        For i = 1 To 108
        '            Select Case i
        '                Case 1 To 12
        '                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    tmpbtnnum = tmpbtnnum + 1
        '                Case 21 To 32 'show and re number
        '                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    tmpbtnnum = tmpbtnnum + 1
        '                Case 41 To 52 'show and re number
        '                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    tmpbtnnum = tmpbtnnum + 1
        '                Case 61 To 72 'show and re number
        '                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    tmpbtnnum = tmpbtnnum + 1
        '                Case 81 To 92 'show and re number
        '                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("00")
        '                    tmpbtnnum = tmpbtnnum + 1
        '                Case 101 To 112 'show and re number
        '                    Me.Controls("btn" & i).Text = tmpbtnnum.ToString("000")
        '                    tmpbtnnum = tmpbtnnum + 1
        '            End Select
        '        Next

        'End Select

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.cancelRoutine()
        frmJobEntry.Show()
        Me.Close()
    End Sub
End Class