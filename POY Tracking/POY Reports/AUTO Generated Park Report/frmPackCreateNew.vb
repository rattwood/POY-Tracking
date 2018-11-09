Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackCreateNew
    'Dim MyCrExcel As New Excel.Application
    Dim SheetCodeString As String
    Dim modBarcode As String


    Public Sub CreateNew()
        Dim MyPakExcel As New Excel.Application
        Dim boxCount As Integer = 1
        Dim nfree As Integer  'This will be container for the next row free  
        Dim ncfree As Integer 'This will be container for the next column free  
        Dim colcount As Integer
        Dim xlWorkbook As Excel.Workbook
        Dim xlSheets As Excel.Worksheet


        'OPEN A NEW WORKSHEET
        xlWorkbook = MyPakExcel.Workbooks.Open(frmPackRepMain.template)
        'ReName the work sheet 
        CType(MyPakExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName


        'CREATE CORRECT HEADER FOR SHEET
        Select Case frmJobEntry.drumPerPal


            Case "48"

                nfree = 11
                'PRODUCT NAME
                MyPakExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value  'C4
                'Product Merge Num
                MyPakExcel.Cells(5, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C5
                'Product Code
                'MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L7
                'DATE
                MyPakExcel.Cells(3, 11) = Date.Now.ToString("dd MM yyyy")              'K3
                'PACKING TYPE K value
                MyPakExcel.Cells(4, 9) = frmJobEntry.varKNum                  'I4
                'CHEESE WEIGHT
                MyPakExcel.Cells(6, 9) = frmJobEntry.varProdWeight                 'I6
                'PACKER NAME
                MyPakExcel.Cells(32, 11) = frmJobEntry.PackOp      'K32
                'Barcode = Trace Number
                createBarcode()
                MyPakExcel.Cells(2, 1) = SheetCodeString
                MyPakExcel.Cells(3, 1) = modBarcode
                'PALLET NUMBER = Trace Number
                MyPakExcel.Cells(6, 3) = frmTraceEntry.txtTraceNum.Text

            Case "72"
                nfree = 10
                'PRODUCT NAME
                MyPakExcel.Cells(3, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value  'C3
                'Product Merge Num
                MyPakExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C4
                'Product Code
                'MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L6
                'DATE
                MyPakExcel.Cells(2, 11) = Date.Now.ToString("dd MM yyyy")              'K2
                'CHEESE WEIGHT
                MyPakExcel.Cells(5, 9) = frmJobEntry.varProdWeight                   'I5
                'PACKER NAME
                MyPakExcel.Cells(11, 4) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'K31
                'Barcode = Trace Number
                createBarcode()
                MyPakExcel.Cells(2, 1) = SheetCodeString
                'PALLET NUMBER = Trace Number
                MyPakExcel.Cells(5, 3) = frmTraceEntry.txtTraceNum.Text


                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                'Select Case frmPackPrvGet.ncfree
                '    Case 12
                '        'This will write date to the first two cone columns
                '        colcount = 4
                '        For ccount = 1 To 2
                '            For rcount = 11 To 51
                '                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                '            Next
                '            colcount = colcount + 4
                '        Next

                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 12 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                '            Next

                '        End If
                '    Case 8
                '        'This will write date to the first One cone columns
                '        For rcount = 12 To 51
                '            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                '        Next


                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 12 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                '            Next

                '        End If
                '    Case 4

                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 12 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                '            Next
                '        End If
                'End Select




            Case "120"
                nfree = 10
                'PRODUCT NAME
                MyPakExcel.Cells(3, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value  'C3
                'Product Merge Num
                MyPakExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C4
                'Product Code
                'MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L6
                'DATE
                MyPakExcel.Cells(2, 11) = Date.Now.ToString("dd MM yyyy")              'K2
                'CHEESE WEIGHT
                MyPakExcel.Cells(5, 9) = frmJobEntry.varProdWeight                   'I5
                'PACKER NAME
                MyPakExcel.Cells(11, 4) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'K31
                'Barcode = Trace Number
                createBarcode()
                MyPakExcel.Cells(2, 1) = SheetCodeString
                'PALLET NUMBER = Trace Number
                MyPakExcel.Cells(5, 3) = frmTraceEntry.txtTraceNum.Text



                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                'Select Case frmPackPrvGet.ncfree
                '    Case 12
                '        'This will write date to the first three cone columns
                '        colcount = 4
                '        For ccount = 1 To 3
                '            For rcount = 14 To 52
                '                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                '            Next
                '            colcount = colcount + 4
                '        Next

                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 10 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                '            Next

                '        End If

                '    Case 10
                '        colcount = 8
                '        For ccount = 1 To 2
                '            For rcount = 14 To 65
                '                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                '            Next
                '            colcount = colcount + 4
                '        Next

                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 14 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                '            Next

                '        End If


                '    Case 8
                '        'This will write date to the first One cone columns
                '        For rcount = 10 To 29
                '            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                '        Next


                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 14 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                '            Next

                '        End If
                '    Case 6
                '        'This will write date to the first One cone columns
                '        For rcount = 10 To 29
                '            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                '        Next


                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 14 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                '            Next

                '        End If
                '    Case 4
                '        'This will write date to the first One cone columns
                '        For rcount = 10 To 29
                '            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                '        Next


                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 14 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                '            Next

                '        End If
                '    Case 2

                '        If frmPackPrvGet.nfree > 0 Then
                '            nfree = frmPackPrvGet.nfree
                '            For usedrow = 10 To nfree - 1
                '                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                '            Next
                '        End If
                'End Select



        End Select


        'If boxCount = 0 Then boxCount = 1


        Try

            'Save changes to new file in Paking Dir
            MyPakExcel.DisplayAlerts = False
            xlWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'CLOSE THE TEMPLATE FILE 
        Try
            'Save changes to new file in Paking Dir
            MyPakExcel.DisplayAlerts = False
            xlWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'CLEAN UP
        MyPakExcel.Quit()
        releaseObject(xlSheets)
        releaseObject(xlWorkbook)
        releaseObject(MyPakExcel)


        Select Case frmJobEntry.drumPerPal

            Case "48"
                frmPackTodayUpdate.TodatUpdate48()
            Case "72"
                frmPackTodayUpdate.TodayUpdate72()
            Case "120"
                frmPackTodayUpdate.TodayUpdate120()

        End Select






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
        Dim gradeTxt As String

        'Routine to get date brocken down
        today = Convert.ToDateTime(today).ToString("dd MM yyyy")
        day = today.Substring(0, 2)
        month = today.Substring(3, 2)
        year = today.Substring(8, 2)


        'Select Case frmJobEntry.txtGrade.Text
        '    Case "A"
        '        gradeTxt = "A" 'A Grade
        '    Case "B"
        '        gradeTxt = "B" 'B Grade
        '    Case "AL"
        '        gradeTxt = "AL" 'AL Grade
        '    Case "AD"
        '        gradeTxt = "AD" 'AD Grade
        '    Case "P35 AS"
        '        gradeTxt = "P35AS" 'P35 AS Grade
        '    Case "P35 BS"
        '        gradeTxt = "P35BS" 'P35 BS Grade
        '    Case "P25 AS"
        '        gradeTxt = "P25AS" 'P25 AS Grade
        '    Case "P30 BS"
        '        gradeTxt = "P30BS" 'P30 BS Grade
        '    Case "P15 AS"
        '        gradeTxt = "P15AS" 'P15 AS Grade
        '    Case "P20 BS"
        '        gradeTxt = "P20BS" 'P20 BS Grade
        '    Case "ReCheck"
        '        gradeTxt = "RECHECK" 'ReCheck Grade
        '    Case "Round1"
        '        gradeTxt = "R1" 'ReCheck Grade
        '    Case "Round2"
        '        gradeTxt = "R2" 'ReCheck Grade
        '    Case "Round3"
        '        gradeTxt = "R3" 'ReCheck Grade
        '    Case "STD"
        '        gradeTxt = "STD" 'ReCheck Grade
        '    Case "Pilot 6Ch"
        '        gradeTxt = "PI06" 'A Grade 6 Cheese per box
        '    Case "Pilot 15Ch"
        '        gradeTxt = "PI15" 'A Grade 15 Cheese per box
        '    Case "Pilot 20Ch"
        '        gradeTxt = "PI20" 'A Grade 20 Cheese per box

        'End Select



        SheetCodeString = ("*" & frmTraceEntry.txtTraceNum.Text & "*")
        modBarcode = SheetCodeString.Replace("*", "")
    End Sub

    Private Sub frmPackCreateNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class