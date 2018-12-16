'Imports System.Data.DataTable
Imports System.Data.SqlClient
Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports System.Globalization
Imports System.Threading


Public Class frmJobEntry
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    'Private SQL As New SQLConn


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


    Public cartSelect
    Public varCartSelect
    Public varUserName
    Public varJobNum
    Public varMachineCode
    Public varMachineName
    Public varProductCode
    Public varYear
    Public varMonth
    Public varDoffingNum
    Public varCartNum
    Public varProductName
    Public varSpNums
    Public varTmpTrace
    Public varCartBCode
    Public varCartNameA As String
    Public varCartNameB As String
    Public varKNum As String
    Public mergeNum As String
    Public dbBarcode As String
    Public POYValUpdate As Integer
    Public JobBarcode As String
    Public varProdWeight As String
    Public varweightcode As String
    Public drumPerPal As String
    Public ExistingProd As String
    Public drumToAllcount As String

    Dim machineName As String = ""
    Dim machineCode As String
    Dim productCode As String
    Dim year As String
    Dim month As String
    Dim doffingNum As String
    Dim spinNum As String
    Dim quit As Integer
    Public cartReport As Integer
    Dim palNum As String
    Dim tracePassed As String = 0
    Public newJobFlag As Integer = 0
    Dim todayTimeDate As String
    Dim traceExists As Integer = 0
    Public SortOP As String
    Public PackOp As String
    Public ColorOP As String
    Public PackSortOP As String
    Public changedrum As Integer
    Public time As DateTime = DateTime.Now
    Public dateFormat As String = "yyyy MM dd HH:mm"
    Public thaiLang As Boolean



    Private Sub frmJobEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        todayTimeDate = time.ToString(dateFormat)



        If My.Settings.chkUseThai Then
            ChangeLanguage("th-TH")
            thaiLang = True
        Else
            ChangeLanguage("en")
            thaiLang = False
        End If


        Me.txtDrumNum.Visible = False


        Me.Text = "POY Packing"
        updateButtons()

        If My.Settings.debugSet Then frmDGV.Show()


    End Sub

    Private Sub updateButtons()
        If thaiLang Then
            btnCancelReport.Text = "ยกเลิก"
            btnNewPallet.Text = "เริ่มพาเลทใหม่"
            btnOldPallet.Text = "จบพาเลทเก่า"
        End If




    End Sub

    Private Sub ChangeLanguage(ByVal lang As String)
        For Each c As Control In Me.Controls
            Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(frmJobEntry))
            resources.ApplyResources(c, c.Name, New CultureInfo(lang))
        Next c
    End Sub



    Public Sub txtOperator_TextChanged(sender As Object, e As EventArgs) Handles txtOperator.TextChanged

        btnNewPallet.Enabled = True
        btnOldPallet.Enabled = True

        txtDrumNum.Visible = False
        comBoxDrumPal.Visible = False
        comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select


        PackOp = txtOperator.Text
        varUserName = txtOperator.Text

    End Sub


    Private Sub comBoxDrumPal_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles comBoxDrumPal.SelectedIndexChanged
        drumPerPal = comBoxDrumPal.Text

        Label2.Visible = True
        txtDrumNum.Visible = True
        txtDrumNum.Focus()

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub


    Private Sub createnewPallet()

        dbBarcode = txtDrumNum.Text 'actualy this is now the drumbarcode number


        Try

            If Not (txtDrumNum.TextLength = 14) Then  ' LENGTH OF BARCODE
                If thaiLang Then MsgBox("หมายเลขนี้ไม่ใช่หมายเลขของดรัม กรุณาสแกนใหม่") Else _
                    MsgBox("This is not a DRUM number Please RE Scan")
                Me.txtDrumNum.Clear()
                Me.txtDrumNum.Focus()
                Me.txtDrumNum.Refresh()
                Exit Sub
            End If

        Catch ex As Exception

            If thaiLang Then MsgBox("ไม่มีหมายเลขดรัมนี้ " & vbNewLine & ex.Message) Else _
                MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            Me.txtDrumNum.Clear()
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
            Exit Sub
        End Try

        comBoxDrumPal.Enabled = False

        '*************************  CHECK TO SEE IF JOB ALREADY EXISITS IF NOT CREATE JOB
        LExecQuery("SELECT * FROM POYTrack WHERE POYBCODEDRUM = '" & dbBarcode & "' Order By POYPACKIDX")

        Try
            If LRecordCount > 0 Then  'If it exists then 
                ' MsgBox("This Drum is allready allocated, " & vbCrLf & " Please use the FINISH OLD PALLET Option")
                If thaiLang Then MsgBox("หมายเลขดรัมนี้ถูกใช้วางตำแหน่งแล้ว กรุณาใช้ option จบพาเลทเก่า") Else _
                    MsgBox("This Drum is allready allocated, " & vbCrLf & " Please use the FINISH OLD PALLET Option")
                cancelRoutine()
                Exit Sub

            Else
                'go and create new pallette
                POYPaletteCreate()

            End If

        Catch ex As Exception
            ' MsgBox("Job Creation Fault" & vbNewLine & ex.Message)
            If thaiLang Then MsgBox("สร้างงานผิดพลาด " & vbNewLine & ex.Message) Else _
                     MsgBox("Job Creation Fault" & vbNewLine & ex.Message)
            Me.txtDrumNum.Clear()
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
            Exit Sub
        End Try


        txtDrumNum.Visible = True
        txtDrumNum.Focus()
        'dbBarcode = ""

    End Sub


    Private Sub oldPallet()

        dbBarcode = txtDrumNum.Text



        '*************************  LOAD THE DRUM DATA SO WE CAN GET THE TMPTRACE NUMBER TO FIND ALL ASSOCIATED DRUMS

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            Dim tmpTracefind As String = frmDGV.DGVdata.Rows(0).Cells("POYTMPTRACE").Value.ToString



            '*************************  get all drum data for pallet
            LExecQuery("SELECT * FROM POYTrack WHERE POYTMPTRACE = '" & tmpTracefind & "' Order By POYPACKIDX")
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYTRACENUM").Value) Then
                ' MsgBox("This Pallet is already Finished " & vbCrLf & "using TRACE NUMBER " & frmDGV.DGVdata.Rows(0).Cells("POYTRACENUM").Value.ToString)
                If thaiLang Then MsgBox("โปรดักส์นี้ไม่มีในตารางสินค้า กรุณาตรวจสอบตารางสินค้าในการตั้งค่า " & frmDGV.DGVdata.Rows(0).Cells("POYTRACENUM").Value.ToString) Else _
                    MsgBox("This Pallet is already Finished " & vbCrLf & "using TRACE NUMBER " & frmDGV.DGVdata.Rows(0).Cells("POYTRACENUM").Value.ToString)
                frmDGV.DGVdata.ClearSelection()
                newJobFlag = 0
                traceExists = 1
                cancelRoutine()
                Exit Sub

            End If

        Else
            ' MsgBox("This Drum is not in the system, please scan any Drum already on the Pallet")
            If thaiLang Then MsgBox("ดรัมนี้ไม่มีในระบบ กรุณาสแกนดรัมในพาเลท") Else _
                    MsgBox("This Drum is not in the system, please scan any Drum already on the Pallet")


            Me.txtDrumNum.Clear()
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
        End If





        drumPerPal = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value



    End Sub


    Private Sub prgContinue()



        dbBarcode = txtDrumNum.Text 'actualy this is now the drumbarcode number

        getMCName()
        machineCode = txtDrumNum.Text.Substring(0, 2)
        productCode = txtDrumNum.Text.Substring(2, 3)
        year = txtDrumNum.Text.Substring(5, 2)
        month = txtDrumNum.Text.Substring(7, 2)
        doffingNum = txtDrumNum.Text.Substring(9, 3)
        spinNum = txtDrumNum.Text.Substring(12, 2)
        mergeNum = txtDrumNum.Text.Substring(9, 3)
        varCartBCode = txtDrumNum

        varMachineCode = machineCode
        getMCName()
        varMachineName = machineName
        varProductCode = productCode
        varYear = year
        varMonth = month
        varDoffingNum = doffingNum
        varCartNum = spinNum
        varCartSelect = cartSelect





        Try

            If Not (txtDrumNum.TextLength = 14) Then  ' LENGTH OF BARCODE
                'MsgBox("This is not a DRUM number Please RE Scan")
                If thaiLang Then MsgBox("หมายเลขนี้ไม่ใช่หมายเลขของดรัม กรุณาสแกนใหม่") Else _
                    MsgBox("This is not a DRUM number Please RE Scan")
                Me.txtDrumNum.Clear()
                Me.txtDrumNum.Focus()
                Me.txtDrumNum.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            'MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            If thaiLang Then MsgBox("ไม่มีหมายเลขดรัมนี้ " & vbNewLine & ex.Message) Else _
                MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            Me.txtDrumNum.Clear()
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
            Exit Sub
        End Try

        comBoxDrumPal.Enabled = False




        '*************************  CHECK TO SEE IF JOB ALREADY EXISITS IF NOT CREATE JOB
        'LExecQuery("SELECT * FROM POYTrack WHERE POYTMPTRACE = '" & dbBarcode & "' Order By POYPACKIDX")

                If newJobFlag Then
            '*************************  CHECK TO SEE IF JOB ALREADY EXISITS IF NOT CREATE JOB
            LExecQuery("SELECT * FROM POYTrack WHERE POYBCODEDRUM = '" & dbBarcode & "' Order By POYPACKIDX")
            Try
                If LRecordCount > 0 Then  'If it exists then 
                    ' MsgBox("This Drum is allready allocated, " & vbCrLf & " Please use the FINISH OLD PALLET Option")
                    If thaiLang Then MsgBox("หมายเลขดรัมนี้ถูกใช้วางตำแหน่งแล้ว กรุณาใช้ option จบพาเลทเก่า") Else _
                    MsgBox("This Drum is allready allocated, " & vbCrLf & " Please use the FINISH OLD PALLET Option")
                    frmDGV.DGVdata.ClearSelection()
                    newJobFlag = 0
                    cancelRoutine()
                    Exit Sub

                Else
                    'go and create new pallette
                    POYPaletteCreate()

                End If

            Catch ex As Exception
                'MsgBox("Job Creation Fault" & vbNewLine & ex.Message)
                If thaiLang Then MsgBox("สร้างงานผิดพลาด " & vbNewLine & ex.Message) Else _
                     MsgBox("Job Creation Fault" & vbNewLine & ex.Message)
                Me.txtDrumNum.Clear()
                Me.txtDrumNum.Focus()
                Me.txtDrumNum.Refresh()
                Exit Sub
            End Try

            txtDrumNum.Visible = True
            txtDrumNum.Focus()

        Else
            '*************************  CHECK TO SEE IF JOB ALREADY EXISITS IF NOT CREATE JOB
            LExecQuery("SELECT * FROM POYTrack WHERE POYBCODEDRUM = '" & dbBarcode & "' Order By POYPACKIDX")

            Try
                If LRecordCount > 0 Then  'If it exists then 

                    oldPallet()
                    If traceExists Then
                        traceExists = 0
                        frmDGV.DGVdata.ClearSelection()
                        newJobFlag = 0
                        cancelRoutine()
                        Exit Sub
                    End If

                Else
                    ' MsgBox("This Drum is not in the system, please scan any Drum already on the Pallet")
                    If thaiLang Then MsgBox("ดรัมนี้ไม่มีในระบบ กรุณาสแกนดรัมในพาเลท") Else _
                    MsgBox("This Drum is not in the system, please scan any Drum already on the Pallet")
                    cancelRoutine()
                    Me.txtDrumNum.Clear()
                    Me.txtDrumNum.Focus()
                    Me.txtDrumNum.Refresh()
                    Exit Sub
                End If

            Catch ex As Exception
                'MsgBox("Job Find Fault" & vbNewLine & ex.Message)
                If thaiLang Then MsgBox("พบงานผิดพลาด" & vbNewLine & ex.Message) Else _
                    MsgBox("Job Find Fault" & vbNewLine & ex.Message)
                Me.txtDrumNum.Clear()
                Me.txtDrumNum.Focus()
                Me.txtDrumNum.Refresh()
                Exit Sub
            End Try
        End If

        txtDrumNum.Visible = True
            txtDrumNum.Focus()
        'dbBarcode = ""




        Try

            If newJobFlag = 0 Then  'We are in Old Pallete routine if newjobflag is zero
                If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value) Then
                    ExistingProd = frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value.ToString
                    If String.Equals(productCode, ExistingProd) = False Then
                        ' MsgBox("This cart Is for Product # " & productCode.ToString & " And Palette Product Is " & ExistingProd.ToString & " Please check")
                        If thaiLang Then MsgBox("ดรัมนี้คือโปรดักส์ " & productCode.ToString & " และพาเลทโปรดักส์คือ " & ExistingProd.ToString & " กรุณาตรวจสอบ") Else _
                    MsgBox("This cart Is for Product # " & productCode.ToString & " And Palette Product Is " & ExistingProd.ToString & " Please check")
                        Me.txtDrumNum.Clear()
                        Me.txtDrumNum.Focus()
                        Me.txtDrumNum.Refresh()
                        Exit Sub
                    End If
                End If
            End If




        Catch ex As Exception
            'MsgBox("Drum BarCode Is Not Valid " & vbNewLine & ex.Message)
            If thaiLang Then MsgBox("ไม่มีหมายเลขดรัมนี้ " & vbNewLine & ex.Message) Else _
                MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            Me.txtDrumNum.Clear()
            Me.txtDrumNum.Focus()
            Me.txtDrumNum.Refresh()
            Exit Sub
        End Try



        PackCheck()

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

        End Try

    End Sub

    Private Sub PackCheck()
        Try
            Dim tmpcount As Integer = 0

            If newJobFlag = 0 Then
                For i = 1 To CInt(drumPerPal)
                    If Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then

                        If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then
                            tmpcount = tmpcount + 1
                        End If
                    End If
                Next



            End If

            If LRecordCount > 0 Then
                    Select Case drumPerPal
                        Case "120"
                            If LRecordCount = 120 Then
                                POYValUpdate = 1
                                dbBarcode = ""
                            Hide()
                            frmPacking120.Show()

                        End If

                        Case "72"
                            If LRecordCount = 72 Then
                                POYValUpdate = 1
                                dbBarcode = ""
                            Hide()
                            frmPacking72.Show()

                            End If
                        Case "48"
                            If LRecordCount = 48 Then
                                POYValUpdate = 1
                                dbBarcode = ""
                            Hide()
                            frmPacking48.Show()

                        End If
                    End Select

                End If

        Catch ex As Exception
            'MsgBox("Drum BarCode Is Not Valid " & vbNewLine & ex.Message)
            If thaiLang Then MsgBox("ไม่มีหมายเลขดรัมนี้ " & vbNewLine & ex.Message) Else _
                MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)
            Me.txtDrumNum.Clear()
            Me.txtDrumNum.Refresh()
            Me.txtDrumNum.Focus()
            Exit Sub
        End Try



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


    Private Sub DelayTM()
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()

    End Sub

    Private Sub POYPaletteCreate()

        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""
        If LConn.State = ConnectionState.Open Then LConn.Close()

        LExecQuery("Select * FROM POYPRODUCT WHERE POYPRNUM = '" & productCode & "' ")
        If LRecordCount > 0 Then
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True

            varProductName = frmDGV.DGVdata.Rows(0).Cells("POYPRNAME").Value
            mergeNum = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value



            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value) Then
                varProdWeight = frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value
            Else
                varProdWeight = "0.0"
            End If

            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYWEIGHTCODE").Value) Then
                varKNum = frmDGV.DGVdata.Rows(0).Cells("POYWEIGHTCODE").Value
            Else
                varKNum = "K0"
            End If




            If LConn.State = ConnectionState.Open Then LConn.Close()





        Else
            'MsgBox("This product is not in Product table, please check product table in SETTINGS ")
            If thaiLang Then MsgBox("โปรดักส์นี้ไม่มีในตารางสินค้า กรุณาตรวจสอบตารางสินค้าในการตั้งค่า") Else _
                MsgBox("This product is not in Product table, please check product table in SETTINGS ")
            cancelRoutine()
            Exit Sub

        End If


        If thaiLang Then
            Label3.Text = "สร้างพาเลทใหม่"
            Label3.Visible = True
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Else
            Label3.Text = "Creating New Pallet"
            Label3.Visible = True
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        End If

        'Label3.Text = "Creating New Pallet"
        'Label3.Visible = True
        'Me.Cursor = System.Windows.Forms.Cursors.WaitCursor


        Dim fmt As String = "000"
        Dim modIdxNum As String




        For i As Integer = 1 To drumPerPal

            modIdxNum = i.ToString(fmt)

            'moddrumNum = i.ToString(fmt)   ' FORMATS THE drum NUMBER TO 3 DIGITS
            '  drumBarcode = modLotStr & moddrumNum   'CREATE THE drum BARCODE NUMBER
            '  JobBarcode = modLotStr

            'Parameters List for full db

            'ADD SQL PARAMETERS & RUN THE COMMAND
            ' LAddParam("@poymcnum", varMachineCode)
            LAddParam("@poyprodnum", productCode)
            ' LAddParam("@yy", varYear)
            ' LAddParam("@mm", varMonth)
            ' LAddParam("@doff", varDoffingNum)
            ' LAddParam("@drum", moddrumNum)
            LAddParam("@merge", mergeNum)
            ' LAddParam("@poypackname", "")
            ' LAddParam("@poyshipname", "0")
            ' LAddParam("@poydrumstate", "0")
            ' LAddParam("@poyfulldrum", "0")
            ' LAddParam("@poyshortdrum", "0")
            ' LAddParam("@poypackdate", varCartSelect)
            ' LAddParam("@poyshipdate", cartName)
            ' LAddParam("@poystepnum", "0")
            ' LAddParam("@poybcodedrum", "0")
            'LAddParam("@poypalnum", 0)
            LAddParam("@poypackidx", modIdxNum)
            LAddParam("@poytmptrace", dbBarcode)
            LAddParam("@poydrumperpal", drumPerPal)
            LAddParam("@poyprodname", varProductName)
            LAddParam("@poyprodweight", varProdWeight)


            'LExecQuery("INSERT INTO POYTrack (POYMCNUM, POYPRNUM, POYYY, POYMM, POYDOFFNUM, POYSPINNUM, POYMERGENUM, POYPACKNAME,POYSHIPNAME," _
            '       & "POYDRUMSTATE, POYFULLDRUM, POYSHORTDRUM, POYPACKDATE, POYSHIPDATE, POYSTEPNUM, POYBCODEDRUM, POYPALNUM, POYPACKIDX, POYTRACENUM," _
            '       & "VALUES (@poymcnum, @poyprodnum,@yy,@mm,@doff,@drum,@merge,@poypackname,@poyshipname,@poydrumstate,@poyfulldrum,@poyshortdrum,@poypackdate,@poyshipdate,@poystepnum," _
            '       & "@poybcodedrum,@poypalnum,@poypackidx,@poytracenum) ")

            LExecQuery("INSERT INTO POYTrack (POYPRNUM,POYDRUMPERPAL,POYPACKIDX,POYTMPTRACE,POYPRODNAME,POYMERGENUM,POYPRODWEIGHT) VALUES (@poyprodnum,@poydrumperpal,@poypackidx,@poytmptrace,@poyprodname,@merge,@poyprodweight)")

        Next



        Try
            'Writes the scanned drum in to DB
            LExecQuery("UPDATE POYTRACK SET POYBCODEDRUM = '" & dbBarcode & "', POYPACKNAME = '" & txtOperator.Text & "', POYPACKDATE = '" & time & "', " _
                       & "POYMCNUM = '" & varMachineCode.ToString & "', POYMCNAME = '" & machineName & "', POYYY = '" & varYear.ToString & "', POYPRMM = '" & varMonth.ToString & "' , " _
                       & "POYDOFFNUM = '" & varDoffingNum.ToString & "', POYSPINNUM = '" & spinNum.ToString & "', POYDRUMSTATE = '15', POYSTEPNUM = '1' " _
                       & "WHERE POYPACKIDX = '001' and POYTMPTRACE = '" & dbBarcode & "' ")
        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ' MsgBox("Job Update Error" & vbNewLine & ex.Message)
            If thaiLang Then MsgBox("อัพเดทงานผิดพลาด " & vbNewLine & ex.Message) Else _
               MsgBox("Job Update Error" & vbNewLine & ex.Message)
        End Try




        LExecQuery("Select * FROM PoyTrack WHERE POYTMPTRACE = '" & dbBarcode & "' ORDER BY POYPACKIDX")

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
            If thaiLang Then MsgBox("สร้างงานผิดพลาด " & vbNewLine & ex.Message) Else _
              MsgBox("Job creation Error" & vbNewLine & ex.Message)
        End Try



    End Sub


    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub


    'Private Sub btnSettings_Click_1(sender As Object, e As EventArgs)
    '    frmPassword.Show()
    'End Sub




    Private Sub btnCancelReport_Click(sender As Object, e As EventArgs) Handles btnCancelReport.Click

        cancelRoutine()

    End Sub

    Private Sub cancelRoutine()

        Label4.Visible = False
        comBoxDrumPal.Visible = False
        comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select


        Label2.Visible = False
        txtDrumNum.Visible = False
        Me.txtDrumNum.Clear()
        txtDrumNum.Refresh()
        ' txtOperator.Clear()
        ' txtOperator.Focus()


        btnNewPallet.BackColor = Color.LightBlue
        btnNewPallet.Enabled = True
        btnOldPallet.BackColor = Color.LightBlue
        btnOldPallet.Enabled = True
        newJobFlag = 0


        tracePassed = 0
        lblAutoCorrect.Visible = False
        comBoxDrumPal.Enabled = True

    End Sub

    Private Sub btnExChangedrum_Click(sender As Object, e As EventArgs)

        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            changedrum = 1
            Me.Hide()
            'frmExChangedrum.Show()
        End If

    End Sub

    Private Sub btnSearchdrum_Click(sender As Object, e As EventArgs)
        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            Me.Hide()
            frmdrumSearch.Show()
        End If
    End Sub



    Private Sub btnReports_Click(sender As Object, e As EventArgs)
        'frmPackReports.Show()
    End Sub


    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Return Then

            prgContinue()

        End If
    End Sub

    Private Sub btnNewPallet_Click(sender As Object, e As EventArgs) Handles btnNewPallet.Click
        btnNewPallet.BackColor = Color.LightGreen
        btnNewPallet.Enabled = False
        btnOldPallet.BackColor = Color.LightBlue
        btnOldPallet.Enabled = False
        txtDrumNum.Visible = False
        Label4.Visible = True
        comBoxDrumPal.Visible = True
        comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select
        newJobFlag = 1

    End Sub

    Private Sub btnOldPallet_Click(sender As Object, e As EventArgs) Handles btnOldPallet.Click
        btnNewPallet.BackColor = Color.LightBlue
        btnNewPallet.Enabled = False
        btnOldPallet.BackColor = Color.LightGreen
        btnOldPallet.Enabled = False
        Label2.Visible = True
        txtDrumNum.Visible = True

        Label4.Visible = False
        comBoxDrumPal.Visible = False
        newJobFlag = 0
        Label2.Visible = True
        txtDrumNum.Visible = True
        txtDrumNum.Focus()

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        frmPassword.Show()
    End Sub


    Private Sub EditPalletToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditPalletToolStripMenuItem.Click
        Hide()
        frmToolEntry.Show()
    End Sub

    Private Sub DRUMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DRUMToolStripMenuItem.Click
        Hide()
        frmdrumSearch.Show()
    End Sub

    Private Sub TraceNumberToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TraceNumberToolStripMenuItem1.Click
        Hide()
        frmTraceSearch.Show()

    End Sub

    Private Sub ChangePalletToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePalletToolStripMenuItem.Click
        Hide()
        frmPalletSplit.Show()
    End Sub
End Class