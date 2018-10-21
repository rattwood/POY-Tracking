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
    Public varCartBCode
    Public varCartNameA As String
    Public varCartNameB As String
    Public mergeNum As String
    Public dbBarcode As String
    Public POYValUpdate As Integer
    Public JobBarcode As String
    Public varProdWeight As String
    Public varweightcode As String
    Public drumPerPal As String
    Public ExistingProd As String

    Dim machineName As String = ""
    Dim machineCode As String
    Dim productCode As String
    Dim year As String
    Dim month As String
    Dim doffingNum As String
    Dim cartNum As String
    Dim quit As Integer
    Public cartReport As Integer
    Dim palNum As String
    Dim tracePassed As String = 0

    Public SortOP As String
    Public PackOp As String
    Public ColorOP As String
    Public PackSortOP As String
    Public changedrum As Integer
    Public time As DateTime = DateTime.Now
    Public Format As String = "dd mm yyyy  HH:mm"



    Private Sub frmJobEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.chkUseThai Then
            ChangeLanguage("th-TH")
        Else
            ChangeLanguage("en")
        End If


        Me.txtTraceNum.Visible = False




        'Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'Set Form Header text

        Me.Text = "POY Packing"


        'Me.btnCancelReport.Visible = False
        If My.Settings.debugSet Then frmDGV.Show()

    End Sub



    Private Sub ChangeLanguage(ByVal lang As String)
        For Each c As Control In Me.Controls
            Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(frmJobEntry))
            resources.ApplyResources(c, c.Name, New CultureInfo(lang))
        Next c
    End Sub



    Public Sub txtOperator_TextChanged(sender As Object, e As EventArgs) Handles txtOperator.TextChanged

        txtTraceNum.Visible = False
        comBoxDrumPal.Visible = True
        comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select


        PackOp = txtOperator.Text
        varUserName = txtOperator.Text

    End Sub


    Private Sub comBoxDrumPal_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles comBoxDrumPal.SelectedIndexChanged
        drumPerPal = comBoxDrumPal.Text
        txtTraceNum.Visible = True
        txtTraceNum.Focus()

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub


    Private Sub checkTraceNo()

        dbBarcode = txtTraceNum.Text


        Try


            If txtTraceNum.TextLength = 10 Then  ' LENGTH OF BARCODE
                palNum = txtTraceNum.Text
            Else
                MsgBox("This is not a TRACE number Please RE Scan")
                Me.txtTraceNum.Clear()
                Me.txtTraceNum.Focus()
                Me.txtTraceNum.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox("Trace BarCode Is Not Valid 1")
            Me.txtTraceNum.Clear()
            Me.txtTraceNum.Focus()
            Me.txtTraceNum.Refresh()
            Exit Sub
        End Try

        comBoxDrumPal.Enabled = False

        '*************************  CHECK TO SEE IF JOB ALREADY EXISITS IF NOT CREATE JOB
        LExecQuery("SELECT * FROM POYTrack WHERE POYTRACENUM = '" & dbBarcode & "' Order By POYPACKIDX")

        Try
            If LRecordCount > 0 Then

                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True



                Dim count As String

                For i = 1 To LRecordCount
                    If Not IsDBNull((frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value)) Then
                        If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = 15 Then
                            count = count + 1
                        End If
                    End If
                Next

                If count = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value Then
                    MsgBox("This PALETTE is already Finished")
                    Me.txtTraceNum.Clear()
                    Me.txtTraceNum.Focus()
                    Me.txtTraceNum.Refresh()
                    Exit Sub
                End If

                drumPerPal = frmDGV.DGVdata.Rows(0).Cells("POYDRUMPERPAL").Value

                'Reads in original DRUM PER PALETTE VALUE AND UPDATES DISPLAY IF WRONG VALUE WAS SELECTED BY OPERATOR
                Select Case drumPerPal
                    Case "48"
                        If comBoxDrumPal.Text = "72" Then
                            comBoxDrumPal.SelectedIndex = 0
                            comBoxDrumPal.Refresh()
                            lblAutoCorrect.Visible = True
                        End If
                    Case "72"
                        If comBoxDrumPal.Text = "48" Then

                            comBoxDrumPal.SelectedIndex = 1
                            comBoxDrumPal.Refresh()
                            lblAutoCorrect.Visible = True
                        End If
                End Select

            Else
                'go and create new pallette
                POYPaletteCreate()
            End If

        Catch ex As Exception
            MsgBox("Job Creation Fault")
            Me.txtTraceNum.Clear()
            Me.txtTraceNum.Focus()
            Me.txtTraceNum.Refresh()
            Exit Sub
        End Try


        tracePassed = 1
        txtBoxCartBcode.Visible = True
        txtBoxCartBcode.Focus()
        dbBarcode = ""

    End Sub


    Private Sub prgContinue()


        Try

            If txtBoxCartBcode.TextLength = 14 And txtBoxCartBcode.Text.Substring(12, 1) = "P" Then ' LENGTH OF BARCODE and that is a cart P number

                getMCName()
                machineCode = txtBoxCartBcode.Text.Substring(0, 2)
                productCode = txtBoxCartBcode.Text.Substring(2, 3)
                year = txtBoxCartBcode.Text.Substring(5, 2)
                month = txtBoxCartBcode.Text.Substring(7, 2)
                doffingNum = txtBoxCartBcode.Text.Substring(9, 3)
                cartNum = txtBoxCartBcode.Text.Substring(12, 2)

                varCartBCode = txtTraceNum.Text

                varMachineCode = machineCode
                getMCName()
                varMachineName = machineName
                varProductCode = productCode
                varYear = year
                varMonth = month
                varDoffingNum = doffingNum
                varCartNum = cartNum
                varCartSelect = cartSelect


                varJobNum = txtBoxCartBcode.Text


                dbBarcode = txtTraceNum.Text    '.Replace(varCartNum, varCartNameA)

            Else
                MsgBox("This is not a CART number Please RE Scan")
                Me.txtBoxCartBcode.Clear()
                Me.txtBoxCartBcode.Focus()
                Me.txtBoxCartBcode.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox("Cart BarCode Is Not Valid")
            Me.txtBoxCartBcode.Clear()
            Me.txtBoxCartBcode.Focus()
            Me.txtBoxCartBcode.Refresh()
            Exit Sub
        End Try


        If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value) Then
            ExistingProd = frmDGV.DGVdata.Rows(0).Cells("POYPRNUM").Value
            If String.Equals(productCode, ExistingProd) = False Then
                MsgBox("This cart is for Product # " & productCode.ToString & " and Palette Product is " & ExistingProd.ToString & " Please check")
                Me.txtBoxCartBcode.Clear()
                Me.txtBoxCartBcode.Focus()
                Me.txtBoxCartBcode.Refresh()
                Exit Sub
            End If
        Else
            'Set Product code based on current cart scanned only on new job creation
            LExecQuery("Update POYTrack Set POYPRNUM = '" & productCode & "' Where POYTRACENUM = '" & dbBarcode & "' ")

        End If

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

            LException = "ExecQuery Error:   " & vbNewLine & ex.Message
            MsgBox(LException)

        End Try

    End Sub

    Private Sub PackCheck()



        'Main Search select all drums on allocated and not allocated
        LExecQuery("SELECT * FROM POYTrack WHERE POYTRACENUM = '" & dbBarcode & "' ORDER BY POYPACKIDX ")

        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        frmDGV.DGVdata.DataSource = LDS.Tables(0)
        frmDGV.DGVdata.Rows(0).Selected = True
        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)
        Try
            If LRecordCount > 0 Then

                Select Case drumPerPal
                    Case "72"
                        If LRecordCount = 72 Then
                            POYValUpdate = 1
                            Me.Hide()
                            frmPacking.Show()

                        End If
                    Case "48"
                        If LRecordCount = 48 Then
                            POYValUpdate = 1
                            MsgBox("Not 48 pack yet")
                            Me.Hide()
                        End If
                End Select


                txtBoxCartBcode.Visible = True
                txtBoxCartBcode.Focus()
                dbBarcode = ""
            End If
        Catch ex As Exception
            MsgBox("Cart BarCode Is Not Valid")
            Me.txtBoxCartBcode.Clear()
            Me.txtBoxCartBcode.Focus()
            Me.txtBoxCartBcode.Refresh()
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




        Label3.Text = "Creating New Palette"
        Label3.Visible = True
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor







        For i As Integer = 1 To drumPerPal

            'moddrumNum = i.ToString(fmt)   ' FORMATS THE drum NUMBER TO 3 DIGITS
            '  drumBarcode = modLotStr & moddrumNum   'CREATE THE drum BARCODE NUMBER
            '  JobBarcode = modLotStr

            'Parameters List for full db

            'ADD SQL PARAMETERS & RUN THE COMMAND
            ' LAddParam("@poymcnum", varMachineCode)
            'LAddParam("@poyprodnum", productCode)
            ' LAddParam("@yy", varYear)
            ' LAddParam("@mm", varMonth)
            ' LAddParam("@doff", varDoffingNum)
            ' LAddParam("@drum", moddrumNum)
            ' LAddParam("@merge", mergeNum)
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
            'LAddParam("@poypackidx", "0")
            LAddParam("@poytracenum", dbBarcode)
            LAddParam("@poydrumperpal", drumPerPal)



            'LExecQuery("INSERT INTO POYTrack (POYMCNUM, POYPRNUM, POYYY, POYMM, POYDOFFNUM, POYSPINNUM, POYMERGENUM, POYPACKNAME,POYSHIPNAME," _
            '       & "POYDRUMSTATE, POYFULLDRUM, POYSHORTDRUM, POYPACKDATE, POYSHIPDATE, POYSTEPNUM, POYBCODEDRUM, POYPALNUM, POYPACKIDX, POYTRACENUM," _
            '       & "VALUES (@poymcnum, @poyprodnum,@yy,@mm,@doff,@drum,@merge,@poypackname,@poyshipname,@poydrumstate,@poyfulldrum,@poyshortdrum,@poypackdate,@poyshipdate,@poystepnum," _
            '       & "@poybcodedrum,@poypalnum,@poypackidx,@poytracenum) ")

            LExecQuery("INSERT INTO POYTrack (POYTRACENUM,POYDRUMPERPAL) VALUES (@poytracenum,@poydrumperpal)")



        Next

        LExecQuery("Select * FROM PoyTrack WHERE POYTRACENUM = '" & dbBarcode & "' ORDER BY POYPACKIDX")

        Label3.Text = ""
        Label3.Visible = True
        Me.Cursor = System.Windows.Forms.Cursors.Default


        If LRecordCount > 1 Then
            Label3.Text = ""
            Label3.Visible = True
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgBox("Records Not created")
        End If

        Label3.Text = ""
        Label3.Visible = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub


    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub


    Private Sub btnSettings_Click_1(sender As Object, e As EventArgs) Handles btnSettings.Click
        frmPassword.Show()
    End Sub




    Private Sub btnCancelReport_Click(sender As Object, e As EventArgs) Handles btnCancelReport.Click





        comBoxDrumPal.SelectedIndex = -1 'Blank the value so operater has to select
        comBoxDrumPal.Select()
        'Me.txtPalletNum.Visible = False
        Me.txtTraceNum.Clear()
        'Me.txtPalletNum.Focus()
        comBoxDrumPal.Enabled = True
        txtBoxCartBcode.Visible = False
        txtBoxCartBcode.Clear()
        tracePassed = 0
        lblAutoCorrect.Visible = False


    End Sub

    Private Sub btnExChangedrum_Click(sender As Object, e As EventArgs)

        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            changedrum = 1
            Me.Hide()
            frmExChangedrum.Show()
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
        frmPackReports.Show()
    End Sub


    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown





        If e.KeyCode = Keys.Return Then

            If tracePassed Then
                prgContinue()
            Else
                checkTraceNo()
            End If
        End If



    End Sub


End Class