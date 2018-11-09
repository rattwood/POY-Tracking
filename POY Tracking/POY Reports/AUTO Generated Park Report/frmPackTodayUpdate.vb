'Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackTodayUpdate

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
    Public prtError As Integer



    Public Sub TodatUpdate48()

        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet

        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        createBarcode()
        boxCount = mycount

        Dim totCount As Integer = 0
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 2
        Dim endloop As Integer

        Try
            For ccount = 1 To 12  'Three sets of columns
                For rcount = 11 To 18
                    If Not (MyTodyExcel.Cells(rcount, colCount).value = " ") Then  'C9-C40
                        totCount = totCount + 1
                        Continue For
                    Else
                        nfree = rcount
                        ncfree = colCount
                        endloop = 1
                        Exit For
                    End If
                Next
                If endloop Then

                    Exit For
                Else
                    If colCount < 12 Then
                        colCount = colCount + 2
                    End If
                End If
            Next
        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try





        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 48 Then

            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 11


            'Product Name
            MyTodyExcel.Cells(3, 2) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value

            'Packer Name
            MyTodyExcel.Cells(31, 11) = frmJobEntry.PackOp

            'Add Barcode to Sheet
            boxCount = boxCount + 1
            createBarcode()
            MyTodyExcel.Cells(2, 1) = SheetCodeString
            MyTodyExcel.Cells(3, 1) = modBarcode



            colCount = 2
            For ccount = 1 To 6
                For i = 11 To 18
                    MyTodyExcel.Cells(i, colCount) = "" 'Clear the contents of cone cells
                    ' MyTodyExcel.Cells(i, colCount - 2) = "" 'Clear the contents of Carton cells
                Next
                If colCount < 8 Then colCount = colCount + 2
            Next
            'boxCount = boxCount + 1
            nfree = 11
            ncfree = 2
        End If


        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try

            'Packer Name
            MyTodyExcel.Cells(32, 11) = frmJobEntry.PackOp


            For i = 1 To frmDGV.DGVdata.Rows.Count
                If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value) Then Continue For


                If frmJobEntry.drumPerPal = "48" And frmDGV.DGVdata.Rows(i - 1).Cells("POYDRUMSTATE").Value = "15" Then



                    'WRITE CONE NUMBER TO SHEET
                    MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("POYBCODEDRUM").Value



                    'WRITE CARTON NUMBER TO SHEET AND PUT IN DGV
                    ' MyTodyExcel.Cells(cellNum, ncfree - 2) = cartonNum
                    'frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1
                    'Increment the Col Number
                    If nfree = 19 And ncfree < 12 Then
                        ncfree = ncfree + 2
                        nfree = 11
                    End If

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 19 And ncfree = 12 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(3, 2) = frmDGV.DGVdata.Rows(0).Cells("POYPRODNAME").Value
                        ''Product Code
                        'MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value
                        'Packer Name
                        MyTodyExcel.Cells(32, 11) = frmJobEntry.PackOp



                        'Add Barcode to Sheet
                        boxCount = boxCount + 1
                        createBarcode()
                        MyTodyExcel.Cells(2, 1) = SheetCodeString
                        MyTodyExcel.Cells(3, 1) = modBarcode


                        ncfree = 2
                        For nCol = 2 To 12
                            For x = 11 To 18
                                MyTodyExcel.Cells(x, ncfree) = "" 'Clear the contents of cone cells
                                ' MyTodyExcel.Cells(x, ncfree - 2) = "" 'Clear the contents of Carton cells
                            Next
                            ncfree = ncfree + 2
                        Next
                        'REST ROW AND COLUMN TO DEFAULT VALUES
                        nfree = 11
                        ncfree = 2

                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        frmPacking48.UpdateDatabase()  'Update the database with changes and then close and go back to Job Entry screen
        Me.Close()
    End Sub

    Public Sub TodayUpdate72()

        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet


        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        createBarcode()
        boxCount = mycount

        Dim totCount As Integer
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 4
        Dim endloop As Integer

        For ccount = 1 To 3  'Three sets of columns
            For rcount = 12 To 51
                If MyTodyExcel.Cells(rcount, colCount).Value > 0 Then  'C9-C40
                    totCount = totCount + 1
                    Continue For
                Else
                    nfree = rcount
                    ncfree = colCount
                    endloop = 1
                    Exit For
                End If
            Next
            If endloop Then

                Exit For
            Else
                colCount = colCount + 4
            End If
        Next






        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 120 Then

            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            'CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 12


            'Product Name
            MyTodyExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value
            'Product Code
            MyTodyExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value

            'Packer Name
            MyTodyExcel.Cells(43, 4) = frmJobEntry.PackOp



            'Add Barcode to Sheet
            boxCount = boxCount + 1
            createBarcode()
            MyTodyExcel.Cells(1, 4) = SheetCodeString


            colCount = 4
            For ccount = 1 To 3
                For i = 12 To 51
                    MyTodyExcel.Cells(i, colCount) = "" 'Clear the contents of cone cells
                    MyTodyExcel.Cells(i, colCount - 2) = "" 'Clear the contents of Carton cells
                Next
                If colCount < 12 Then colCount = colCount + 4
            Next

            nfree = 12
            ncfree = 4
        End If


        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try
            'Packer Name
            MyTodyExcel.Cells(43, 4) = frmJobEntry.PackOp
            For i = 1 To frmDGV.DGVdata.Rows.Count

                If frmJobEntry.drumPerPal = "72" And frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then ' Or
                    'frmJobEntry.txtGrade.Text = "P25 AS" And frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "9" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then    RJA
                    frmDGV.DGVdata.Rows(i - 1).Cells("PACKSHEETBCODE").Value = modBarcode




                    'USED TO ALLOCATE BOX NUMBER USED WHEN PACKED
                    Select Case nfree
                        Case 12 To 19
                            If ncfree = 4 Then
                                cartonNum = 1
                                cellNum = 12
                            ElseIf ncfree = 8 Then
                                cartonNum = 6
                                cellNum = 12
                            ElseIf ncfree = 12 Then
                                cartonNum = 11
                                cellNum = 12
                            End If
                        Case 20 To 27
                            If ncfree = 4 Then
                                cartonNum = 2
                                cellNum = 20
                            ElseIf ncfree = 8 Then
                                cartonNum = 7
                                cellNum = 20
                            ElseIf ncfree = 12 Then
                                cartonNum = 12
                                cellNum = 20
                            End If
                        Case 28 To 35
                            If ncfree = 4 Then
                                cartonNum = 3
                                cellNum = 28
                            ElseIf ncfree = 8 Then
                                cartonNum = 8
                                cellNum = 28
                            ElseIf ncfree = 12 Then
                                cartonNum = 13
                                cellNum = 28
                            End If
                        Case 36 To 43
                            If ncfree = 4 Then
                                cartonNum = 4
                                cellNum = 36
                            ElseIf ncfree = 8 Then
                                cartonNum = 9
                                cellNum = 36
                            ElseIf ncfree = 12 Then
                                cartonNum = 14
                                cellNum = 36
                            End If
                        Case 44 To 51
                            If ncfree = 4 Then
                                cartonNum = 5
                                cellNum = 44
                            ElseIf ncfree = 8 Then
                                cartonNum = 10
                                cellNum = 44
                            ElseIf ncfree = 12 Then
                                cartonNum = 15
                                cellNum = 44
                            End If

                    End Select

                    'cartonNum = (cartonNum & "-" & boxCount).ToString  'Box then sheet number
                    cartonNum = (boxCount & "-" & cartonNum).ToString  'Sheet then Box number

                    'WRITE CONE NUMBER TO SHEET

                    MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value





                    'WRITE CARTON NUMBER TO SHEET AND PUT IN DGV
                    MyTodyExcel.Cells(cellNum, ncfree - 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1
                    'Increment the Col Number
                    If nfree = 52 And ncfree < 12 Then
                        ncfree = ncfree + 4
                        nfree = 12
                    End If

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 52 And ncfree = 12 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value
                        'Product Code
                        MyTodyExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value
                        'Packer Name
                        MyTodyExcel.Cells(43, 4) = frmJobEntry.PackOp



                        'Add Barcode to Sheet
                        boxCount = boxCount + 1
                        createBarcode()
                        MyTodyExcel.Cells(1, 4) = SheetCodeString

                        ncfree = 4
                        For nCol = 1 To 3
                            For x = 12 To 51
                                MyTodyExcel.Cells(x, ncfree) = "" 'Clear the contents of cone cells
                                MyTodyExcel.Cells(x, ncfree - 2) = "" 'Clear the contents of Carton cells
                            Next
                            ncfree = ncfree + 4
                        Next
                        'REST ROW AND COLUMN TO DEFAULT VALUES
                        nfree = 12
                        ncfree = 4

                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        Me.Close()

    End Sub

    Public Sub TodayUpdate120()

        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet


        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        createBarcode()
        boxCount = mycount

        Dim totCount As Integer
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 4
        Dim endloop As Integer



        For ccount = 1 To 4  'Three sets of columns
            If ccount < 4 Then
                For rcount = 14 To 65
                    If MyTodyExcel.Cells(rcount, colCount).Value > 0 Then
                        totCount = totCount + 1
                        Continue For
                    Else
                        nfree = rcount
                        ncfree = colCount
                        endloop = 1
                        Exit For
                    End If
                Next
            Else
                For rcount = 12 To 52
                    If MyTodyExcel.Cells(rcount, colCount).Value > "0" Then
                        totCount = totCount + 1
                        Continue For
                    Else
                        nfree = rcount
                        ncfree = colCount
                        endloop = 1
                        Exit For
                    End If
                Next
            End If


            If endloop Then
                Exit For
            Else
                If colCount < 16 Then colCount = colCount + 4
            End If
        Next




        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 195 Then

            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 14


            'Product Name
            MyTodyExcel.Cells(7, 9) = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value
            'Product Code
            MyTodyExcel.Cells(7, 14) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value

            'Packer Name
            MyTodyExcel.Cells(54, 17) = frmJobEntry.PackOp

            'Add Barcode to Sheet
            boxCount = boxCount + 1
            createBarcode()

            MyTodyExcel.Cells(1, 4) = SheetCodeString



            ncfree = 4
            For nCol = 1 To 4  'Three sets of columns
                If nCol < 4 Then
                    For rcount = 14 To 65
                        MyTodyExcel.Cells(rcount, ncfree) = "" 'Clear the contents of cone cells
                        MyTodyExcel.Cells(rcount, ncfree - 2) = "" 'Clear the contents of Carton cells
                    Next
                    ncfree = ncfree + 4
                Else
                    For rcount = 14 To 52
                        MyTodyExcel.Cells(rcount, ncfree) = "" 'Clear the contents of cone cells
                        MyTodyExcel.Cells(rcount, ncfree - 2) = "" 'Clear the contents of Carton cells
                    Next
                End If

            Next

            nfree = 14
            ncfree = 4

        End If

        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try
            'Packer Name
            MyTodyExcel.Cells(54, 17) = frmJobEntry.PackOp
            For i = 1 To frmDGV.DGVdata.Rows.Count

                'If frmJobEntry.txtGrade.Text = "P20 BS" And frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Or
                '    frmJobEntry.txtGrade.Text = "P15 AS" And frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "9" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then
                '    frmDGV.DGVdata.Rows(i - 1).Cells("PACKSHEETBCODE").Value = modBarcode


                'USED TO ALLOCATE BOX NUMBER USED WHEN PACKED
                Select Case nfree
                        Case 14 To 26
                            If ncfree = 4 Then
                                cartonNum = 1
                                cellNum = 14
                            ElseIf ncfree = 8 Then
                                cartonNum = 5
                                cellNum = 14
                            ElseIf ncfree = 12 Then
                                cartonNum = 9
                                cellNum = 14
                            ElseIf ncfree = 16 Then
                                cartonNum = 13
                                cellNum = 14
                            End If
                        Case 27 To 39
                            If ncfree = 4 Then
                                cartonNum = 2
                                cellNum = 27
                            ElseIf ncfree = 8 Then
                                cartonNum = 6
                                cellNum = 27
                            ElseIf ncfree = 12 Then
                                cartonNum = 10
                                cellNum = 27
                            ElseIf ncfree = 16 Then
                                cartonNum = 14
                                cellNum = 27
                            End If
                        Case 40 To 52
                            If ncfree = 4 Then
                                cartonNum = 3
                                cellNum = 40
                            ElseIf ncfree = 8 Then
                                cartonNum = 7
                                cellNum = 40
                            ElseIf ncfree = 12 Then
                                cartonNum = 11
                                cellNum = 40
                            ElseIf ncfree = 16 Then
                                cartonNum = 15
                                cellNum = 40
                            End If

                        Case 53 To 65
                            If ncfree = 4 Then
                                cartonNum = 4
                                cellNum = 53
                            ElseIf ncfree = 8 Then
                                cartonNum = 8
                                cellNum = 53
                            ElseIf ncfree = 12 Then
                                cartonNum = 12
                                cellNum = 53
                            End If
                    End Select

                    'cartonNum = (cartonNum & "-" & boxCount).ToString  'Box then sheet number
                    cartonNum = (boxCount & "-" & cartonNum).ToString  'Sheet then Box number

                    'WRITE CONE NUMBER TO SHEET

                    MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value






                    'WRITE CARTON NUMBER TO SHEET AND PUT IN DGV
                    MyTodyExcel.Cells(cellNum, ncfree - 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1
                    'Increment the Col Number
                    If nfree = 66 And ncfree < 16 Then
                        ncfree = ncfree + 4
                        nfree = 14
                    End If

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 53 And ncfree = 16 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(9, 7) = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value
                        'Product Code
                        MyTodyExcel.Cells(14, 7) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value
                        'Packer Name
                        MyTodyExcel.Cells(54, 17) = frmJobEntry.PackOp

                        'Add Barcode to Sheet
                        boxCount = boxCount + 1
                        createBarcode()
                        MyTodyExcel.Cells(1, 4) = SheetCodeString



                        ncfree = 4

                        For nCol = 1 To 4  'Three sets of columns
                            If nCol < 4 Then
                                For rcount = 12 To 65
                                    MyTodyExcel.Cells(rcount, ncfree) = "" 'Clear the contents of cone cells
                                    MyTodyExcel.Cells(rcount, ncfree - 2) = "" 'Clear the contents of Carton cells
                                Next
                                ncfree = ncfree + 4
                            Else
                                For rcount = 12 To 52
                                    MyTodyExcel.Cells(rcount, ncfree) = "" 'Clear the contents of cone cells
                                    MyTodyExcel.Cells(rcount, ncfree - 2) = "" 'Clear the contents of Carton cells
                                Next
                            End If

                        Next

                        nfree = 12
                        ncfree = 4

                    End If

                'End If

            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
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