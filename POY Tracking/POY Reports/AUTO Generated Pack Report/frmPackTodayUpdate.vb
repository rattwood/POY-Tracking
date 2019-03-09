Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackTodayUpdate
    Private writeerrorLog As New writeError

    Dim MyTodyExcel As New Excel.Application
    Dim xlRowCount As Integer
    Dim mycount As Integer = 0
    Dim boxCount As Integer = 0
    Dim nfree As Integer = 11
    Dim toAlocate As Integer
    Dim nCol As Integer
    Dim ncfree As Integer
    Dim SheetCodeString As String
    Dim modBarcode As String
    Dim drumInfo As String
    Public prtError As Integer



    Public Sub TodatUpdate48()

        Dim xlTodyWorkbook As Excel.Workbook




        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        createBarcode()
        boxCount = mycount

        Dim totCount As Integer = 0
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 2
        ncfree = 2
        nfree = 11

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try

            'Packer Name
            MyTodyExcel.Cells(32, 11) = frmJobEntry.PackOp


            For i = 1 To frmDGV.DGVdata.Rows.Count
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then

                    drumInfo = (frmDGV.DGVdata.Rows(i - 1).Cells("POYMCNAME").Value.ToString() & " " &
                           frmDGV.DGVdata.Rows(i - 1).Cells("POYPRMM").Value.ToString() & " " &
                          frmDGV.DGVdata.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString() & " " &
                          frmDGV.DGVdata.Rows(i - 1).Cells("POYSPINNUM").Value.ToString)


                    'WRITE VALUES TOO THE SHEET
                    '   MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value

                    MyTodyExcel.Cells(nfree, ncfree) = drumInfo
                    MyTodyExcel.Cells(nfree, ncfree + 1) = frmDGV.DGVdata.Rows(i - 1).Cells("POYPRODWEIGHT").Value


                    nfree = nfree + 1

                    'Increment the Col Number
                    If nfree = 19 And ncfree < 12 Then
                        ncfree = ncfree + 2
                        nfree = 11
                    End If

                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)
            writeerrorLog.writelog("48 Excel Create Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("48 Excel Create Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try


        Try

            Dim defPrinter As String
            defPrinter = MyTodyExcel.ActivePrinter


            'Printout results of Pack Form
            xlTodyWorkbook.PrintOutEx(
            From:=1,
            To:=1,
            Copies:=1,
            Preview:=False,
            Collate:=True,
            IgnorePrintAreas:=True)

            MyTodyExcel.ActivePrinter = defPrinter

        Catch ex As Exception
            MsgBox(ex.Message)
            writeerrorLog.writelog("48 Excel Print", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("48 Excel Print", ex.ToString, True, "System_Fault")
        End Try








        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)
            writeerrorLog.writelog("48 Excel Save Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("48 Excel Save Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
            writeerrorLog.writelog("48 Excel Close Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("48 Excel Close Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try


        MyTodyExcel.Quit()
        'releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        frmPacking48.UpdateDatabase()  'Update the database with changes and then close and go back to Job Entry screen
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()
    End Sub

    Public Sub TodayUpdate72()

        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet

        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        createBarcode()
        boxCount = mycount

        Dim totCount As Integer = 0
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 2
        ncfree = 2
        nfree = 11


        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try

            'Packer Name
            MyTodyExcel.Cells(32, 11) = frmJobEntry.PackOp


            For i = 1 To frmDGV.DGVdata.Rows.Count
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then


                    drumInfo = (frmDGV.DGVdata.Rows(i - 1).Cells("POYMCNAME").Value.ToString() & " " &
                           frmDGV.DGVdata.Rows(i - 1).Cells("POYPRMM").Value.ToString() & " " &
                          frmDGV.DGVdata.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString() & " " &
                          frmDGV.DGVdata.Rows(i - 1).Cells("POYSPINNUM").Value.ToString)


                    'WRITE VALUES TOO THE SHEET
                    '   MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value

                    MyTodyExcel.Cells(nfree, ncfree) = drumInfo
                    MyTodyExcel.Cells(nfree, ncfree + 1) = frmDGV.DGVdata.Rows(i - 1).Cells("POYPRODWEIGHT").Value

                    nfree = nfree + 1
                    'Increment the Col Number
                    If nfree = 23 And ncfree < 12 Then
                        ncfree = ncfree + 2
                        nfree = 11
                    End If


                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)
            writeerrorLog.writelog("72 Excel Create Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("72 Excel Create Error", ex.ToString, True, "System_Fault")

            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception
            writeerrorLog.writelog("72 Excel Save Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("72 Excel Save Error", ex.ToString, True, "System_Fault")
            MsgBox(ex.Message)

            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

        Try
            Dim defPrinter As String
            defPrinter = MyTodyExcel.ActivePrinter




            'Printout results of Pack Form
            xlTodyWorkbook.PrintOutEx(
            From:=1,
            To:=1,
            Copies:=1,
            Preview:=False,
            Collate:=True,
            IgnorePrintAreas:=True)

            MyTodyExcel.ActivePrinter = defPrinter
        Catch ex As Exception
            MsgBox(ex.Message)
            writeerrorLog.writelog("72 Excel Print", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("72 Excel Print", ex.ToString, True, "System_Fault")
        End Try




        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
            writeerrorLog.writelog("72 Excel Close Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("72 Excel Close Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        frmPacking72.UpdateDatabase()  'Update the database with changes and then close and go back to Job Entry screen
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()

    End Sub

    Public Sub TodayUpdate120()
        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet

        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        createBarcode()
        boxCount = mycount

        Dim totCount As Integer = 0
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 2
        ncfree = 2
        nfree = 11

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try

            'Packer Name
            MyTodyExcel.Cells(32, 11) = frmJobEntry.PackOp


            For i = 1 To frmDGV.DGVdata.Rows.Count
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                If frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then

                    drumInfo = (frmDGV.DGVdata.Rows(i - 1).Cells("POYMCNAME").Value.ToString() & " " &
                           frmDGV.DGVdata.Rows(i - 1).Cells("POYPRMM").Value.ToString() & " " &
                          frmDGV.DGVdata.Rows(i - 1).Cells("POYDOFFNUM").Value.ToString() & " " &
                          frmDGV.DGVdata.Rows(i - 1).Cells("POYSPINNUM").Value.ToString)


                    'WRITE VALUES TOO THE SHEET
                    '   MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value

                    MyTodyExcel.Cells(nfree, ncfree) = drumInfo
                    MyTodyExcel.Cells(nfree, ncfree + 1) = frmDGV.DGVdata.Rows(i - 1).Cells("POYPRODWEIGHT").Value


                    nfree = nfree + 1
                    'Increment the Col Number
                    If nfree = 31 And ncfree < 12 Then
                        ncfree = ncfree + 2
                        nfree = 11
                    End If


                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)
            writeerrorLog.writelog("120 Excel Create Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("120 Excel Create Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)
            writeerrorLog.writelog("120 Excel Save Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("120 Excel Save Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

        Try
            Dim defPrinter As String
            defPrinter = MyTodyExcel.ActivePrinter


            'Printout results of Pack Form
            xlTodyWorkbook.PrintOutEx(
            From:=1,
            To:=1,
            Copies:=1,
            Preview:=False,
            Collate:=True,
            IgnorePrintAreas:=True)

            MyTodyExcel.ActivePrinter = defPrinter

        Catch ex As Exception
            MsgBox(ex.Message)
            writeerrorLog.writelog("120 Excel Print", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("120 Excel Print", ex.ToString, True, "System_Fault")
        End Try


        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)

            writeerrorLog.writelog("120 Excel Close Error", ex.Message, True, "System_Fault")
            writeerrorLog.writelog("120 Excel Close Error", ex.ToString, True, "System_Fault")
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        frmPacking120.UpdateDatabase()        'Update the database with changes and then close and go back to Job Entry screen
        Me.Cursor = System.Windows.Forms.Cursors.Default
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




    Public Sub createBarcode()

        Dim today As String = Date.Now
        Dim day As String
        Dim month As String
        Dim year As String
        Dim drumTxt As String

        'Routine to get date brocken down
        today = Convert.ToDateTime(today).ToString("dd MM yyyy")
        day = today.Substring(0, 2)
        month = today.Substring(3, 2)
        year = today.Substring(8, 2)

        Select Case frmJobEntry.drumPerPal
            Case "48"
                drumTxt = "48" '48 Pallet
            Case "72"
                drumTxt = "72" '72 Pallet
            Case "120"
                drumTxt = "120" '120 Pallet


        End Select



        SheetCodeString = ("*" & frmTraceEntry.txtTraceNum.Text & "*")
        modBarcode = SheetCodeString.Replace("*", "")




    End Sub

    Private Sub frmPackTodayUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



End Class