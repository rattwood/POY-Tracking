Imports System.Data.SqlClient

Public Class PalletCreate
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    ' Private SQL As New SQLConn
    Private writeerrorLog As New writeError

    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Private SQL As New SQLConn

    'TIME
    Dim time As New DateTime
    Public todayTimeDate As String
    Dim dateFormat As String = "yyyy-MM-dd HH:mm:ss"

    Public Sub Create()




        Dim varProdGrade
        frmJobEntry.timeUpdate()




        ''THIS IS FOR THE PRINTED REPORT
        'SQL.ExecQuery("Select * FROM POYPRODUCT WHERE POYPRNUM = '" & frmJobEntry.varProductCode & "' ")
        'If SQL.RecordCount > 0 Then
        '    frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
        '    frmDGV.DGVdata.Rows(0).Selected = True

        '    frmJobEntry.varProductName = frmDGV.DGVdata.Rows(0).Cells("POYPRNAME").Value
        '    frmJobEntry.mergeNum = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value

        '    If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value) Then
        '        varProdGrade = frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value.ToString

        '    Else
        '        varProdGrade = "NA"
        '    End If




        '    If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value) Then
        '        frmJobEntry.varProdWeight = frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value
        '        frmJobEntry.varProdWeight = frmJobEntry.varProdWeight / 100
        '    Else
        '        frmJobEntry.varProdWeight = "0.00"
        '    End If

        '    If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("POYWEIGHTCODE").Value) Then
        '        frmJobEntry.varKNum = frmDGV.DGVdata.Rows(0).Cells("POYWEIGHTCODE").Value
        '    Else
        '        frmJobEntry.varKNum = "K00"
        '    End If



        'Else
        '    'MsgBox("This product is not in Product table, please check product table in SETTINGS ")
        '    If frmJobEntry.thaiLang Then MsgBox("โปรดักส์นี้ไม่มีในตารางสินค้า กรุณาตรวจสอบตารางสินค้าในการตั้งค่า") Else _
        '        MsgBox("This product is not in Product table, please check product table in SETTINGS ")
        '    frmJobEntry.Show()
        '    frmJobEntry.cancelRoutine()

        '    Exit Sub

        'End If






        Dim fmt As String = "000"
        Dim modIdxNum As String
        Dim tmpToday As String = Date.Now.ToString("yyMMdd")


        Dim tmpTraceNum As String = frmJobEntry.varCartBCode


        MsgBox("we are here")

        'For i As Integer = 1 To frmJobEntry.drumPerPal

        '    modIdxNum = i.ToString(fmt) 'Format the index number to three digits

        '    'moddrumNum = i.ToString(fmt)   ' FORMATS THE drum NUMBER TO 3 DIGITS
        '    '  drumBarcode = modLotStr & moddrumNum   'CREATE THE drum BARCODE NUMBER
        '    '  JobBarcode = modLotStr

        '    'Parameters List for full db

        '    'ADD SQL PARAMETERS & RUN THE COMMAND

        '    LAddParam("@poypackidx", modIdxNum)
        '    LAddParam("@poystationnum", My.Settings.packStationID)
        '    LAddParam("@poydrumperpal", frmJobEntry.drumPerPal)
        '    LAddParam("@poytmptracenum", tmpTraceNum)



        '    LExecQuery("INSERT INTO POYPackTrace (POYPACKIDX, POYPACKSTATION, POYDRUMPERPAL, POYTMPTRACENUM ) " _
        '           & " VALUES (@poypackidx,@poystationnum, @poydrumperpal,@poytmptracenum ) ")


        'Next



        'LExecQuery("Select * FROM PoyTrack WHERE POYTMPTRACE = '" & frmJobEntry.dbBarcode & "' ORDER BY POYPACKIDX")

        'Try
        '    If LRecordCount > 0 Then
        '        frmDGV.DGVdata.DataSource = LDS.Tables(0)
        '        frmDGV.DGVdata.Rows(0).Selected = True
        '        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

        '        Me.Cursor = System.Windows.Forms.Cursors.Default
        '        Label3.Text = ""
        '        Label3.Visible = False
        '    End If
        'Catch ex As Exception
        '    Me.Cursor = System.Windows.Forms.Cursors.Default
        '    ' MsgBox("Job creation Error" & vbNewLine & ex.Message)
        '    If frmJobEntry.thaiLang Then MsgBox("สร้างงานผิดพลาด " & vbNewLine & ex.Message) Else _
        '      MsgBox("Job creation Error" & vbNewLine & ex.Message)
        '    writeerrorLog.writelog("ExecQuery Error:", ex.Message, False, "System_Fault")
        '    writeerrorLog.writelog("ExecQuery Error:", ex.ToString, False, "System_Fault")
        'End Try






    End Sub

End Class
