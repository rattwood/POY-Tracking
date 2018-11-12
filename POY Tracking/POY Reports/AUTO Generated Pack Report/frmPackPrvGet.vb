

Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackPrvGet

    'Dim MyPrevExcel As New Excel.Application
    Public nfree As Integer
    Public ncfree As Integer

    Public Sub PrvGet()

        Dim MyPrevExcel As New Excel.Application
        Dim xpPrevWoorkbook As Excel.Workbook
        Dim xpPrevSheets As Excel.Worksheet

        xpPrevWoorkbook = MyPrevExcel.Workbooks.Open(frmPackRepMain.prevDaysName)


        'FIND NEXT BLANK ROW FOR CONES
        Select Case frmJobEntry.drumPerPal

            Case "48"
                'WE NEED TO CHECK ROW D12 TO D41, THEN H12 TO H41 THEN L12 TO L41

                Dim colCount As Integer = 2
                Dim endloop As Integer = 0

                For ccount = 1 To 4

                    For rcount = 10 To 21
                        If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then  'C10 to C29
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
                        If colCount < 8 Then colCount = colCount + 2
                    End If

                Next



            Case "72"
                'WE NEED TO CHECK ROW D12 TO D51, THEN H12 TO H51 THEN L12 TO L51
                Dim colCount As Integer = 2
                Dim endloop As Integer = 0

                For ccount = 1 To 6

                    For rcount = 10 To 21
                        If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then  'C9-C40
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
                        If colCount < 12 Then colCount = colCount + 2
                    End If

                Next


            Case "120"

                Dim colCount As Integer = 2
                Dim endloop As Integer

                For ccount = 1 To 46 'Three sets of columns
                    If ccount < 4 Then
                        For rcount = 10 To 29
                            If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then
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
                        If colCount < 12 Then colCount = colCount + 2
                    End If
                Next

        End Select



        Try
            'Close template file but do not save updates to it
            xpPrevWoorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        'CLEAN UP
        MyPrevExcel.Quit()
        releaseObject(xpPrevSheets)
        releaseObject(xpPrevWoorkbook)
        releaseObject(MyPrevExcel)

        progress()
        Me.Close()
    End Sub


    Private Sub progress()

        frmPackCreateNew.CreateNew()


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




End Class