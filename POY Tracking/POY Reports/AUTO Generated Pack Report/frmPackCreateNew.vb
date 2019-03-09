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

                '---------------- HEADER SECTION OF EXCEL SHEET ---------------------------------------------

                'PRODUCT NAME
                MyPakExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value  'C4

                'Product Merge Num
                MyPakExcel.Cells(5, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C5

                'Product Code
                'MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L7

                'DATE
                MyPakExcel.Cells(3, 12) = Date.Now.ToString("DATE : " & "dd MM yyyy")              'L2

                'PACKING TYPE K value
                MyPakExcel.Cells(4, 9) = frmJobEntry.varKNum                  'I4

                'Product Grade
                MyPakExcel.Cells(5, 9) = frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value   'I5

                'Drum WEIGHT
                MyPakExcel.Cells(6, 9) = frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value              'I6

                'PACKER NAME
                MyPakExcel.Cells(32, 11) = frmJobEntry.PackOp      'K32

                'Barcode = Trace Number
                createBarcode()
                ' MyPakExcel.Cells(2, 1) = SheetCodeString
                MyPakExcel.Cells(6, 3) = SheetCodeString
                'MyPakExcel.Cells(3, 1) = modBarcode

                'PALLET NUMBER = Trace Number
                MyPakExcel.Cells(8, 3) = "TRACE NO. " & modBarcode

            Case "72"
                nfree = 11

                '---------------- HEADER SECTION OF EXCEL SHEET ---------------------------------------------

                'PRODUCT NAME
                MyPakExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value  'C4

                'Product Merge Num
                MyPakExcel.Cells(5, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C5

                'Product Code
                'MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L7

                'DATE
                MyPakExcel.Cells(3, 12) = Date.Now.ToString("DATE : " & "dd MM yyyy")              'L2

                'PACKING TYPE K value
                MyPakExcel.Cells(4, 9) = frmJobEntry.varKNum                  'I4

                'Product Grade
                MyPakExcel.Cells(5, 9) = frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value   'I5

                'Drum WEIGHT
                MyPakExcel.Cells(6, 9) = frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value              'I6

                'PACKER NAME
                MyPakExcel.Cells(32, 11) = frmJobEntry.PackOp      'K32

                'Barcode = Trace Number
                createBarcode()
                ' MyPakExcel.Cells(2, 1) = SheetCodeString
                MyPakExcel.Cells(6, 3) = SheetCodeString
                'MyPakExcel.Cells(3, 1) = modBarcode

                'PALLET NUMBER = Trace Number
                MyPakExcel.Cells(8, 3) = "TRACE NO. " & modBarcode


            Case "120"
                nfree = 11

                '---------------- HEADER SECTION OF EXCEL SHEET ---------------------------------------------

                'PRODUCT NAME
                MyPakExcel.Cells(4, 3) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value  'C4

                'Product Merge Num
                MyPakExcel.Cells(5, 3) = frmDGV.DGVdata.Rows(0).Cells("POYMERGENUM").Value  'C5

                'Product Code
                'MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L7

                'DATE
                MyPakExcel.Cells(3, 12) = Date.Now.ToString("DATE : " & "dd MM yyyy")              'L2

                'PACKING TYPE K value
                MyPakExcel.Cells(4, 9) = frmJobEntry.varKNum                  'I4

                'Product Grade
                MyPakExcel.Cells(5, 9) = frmDGV.DGVdata.Rows(0).Cells("POYPRODGRADE").Value   'I5

                'Drum WEIGHT
                MyPakExcel.Cells(6, 9) = frmDGV.DGVdata.Rows(0).Cells("POYPRODWEIGHT").Value              'I6

                'PACKER NAME
                MyPakExcel.Cells(32, 11) = frmJobEntry.PackOp      'K32

                'Barcode = Trace Number
                createBarcode()
                ' MyPakExcel.Cells(2, 1) = SheetCodeString
                MyPakExcel.Cells(6, 3) = SheetCodeString
                'MyPakExcel.Cells(3, 1) = modBarcode

                'PALLET NUMBER = Trace Number
                MyPakExcel.Cells(8, 3) = "TRACE NO. " & modBarcode



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


        SheetCodeString = ("*" & frmTraceEntry.txtTraceNum.Text & "*")
        modBarcode = SheetCodeString.Replace("*", "")
    End Sub

    Private Sub frmPackCreateNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class